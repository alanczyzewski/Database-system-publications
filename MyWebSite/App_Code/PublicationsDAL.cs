using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using MySql.Data.MySqlClient;

public static class PublicationsDAL
{
    private static readonly string connectionString = "server=localhost; port=3306; user id=root; database=publications; SslMode=none;";
    public static List<Publication> PublicationsAll { get; set; } = new List<Publication>();
    public static List<Author> AuthorsAll { get; set; } = new List<Author>();
    //************************************************************************* GET PUBLICATIONS **************************************************************
    public static List<Publication> GetPublications(string query = "SELECT * FROM publications ORDER BY title")
    {
        List<Publication> publications = new List<Publication>();
        using (MySqlConnection sqlConnection = new MySqlConnection(connectionString))
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, sqlConnection);
            DataTable table = new DataTable();
            adapter.Fill(table);
            Dictionary<string, string> pairsFieldValue = new Dictionary<string, string>();
            for (int curRow = 0; curRow < table.Rows.Count; curRow++)
            {
                pairsFieldValue.Clear();
                bool isCrossref = false;
                for (int curCol = 0; curCol < table.Columns.Count; curCol++)
                {
                    if (table.Rows[curRow][curCol].ToString().Trim() != "")
                    {
                        if (table.Columns[curCol].ColumnName == "idauthors")
                        {
                            string id = "";
                            List<string> listAuthors = new List<string>();
                            foreach (var ch in table.Rows[curRow][curCol].ToString().Trim())
                            {
                                if (ch == ';')
                                {
                                    if (id != "")
                                        listAuthors.Add(GetAuthorName(id));
                                    id = "";
                                }
                                else
                                    id += ch;
                            }
                            string author = "";
                            for (int i = 0; i < listAuthors.Count; i++)
                            {
                                author += listAuthors[i];
                                if (i != listAuthors.Count - 1)
                                    author += ", ";
                            }
                            pairsFieldValue.Add("author", author);
                        }
                        else
                        {
                            if (table.Columns[curCol].ColumnName == "crossref")
                                isCrossref = true;
                            pairsFieldValue.Add(table.Columns[curCol].ColumnName, table.Rows[curRow][curCol].ToString().Trim());
                        } 
                    }
                }
                if (isCrossref) //fill missing fields from crossref
                {
                    Publication refPublication = GetPublications($"SELECT * FROM publications WHERE tag = '{table.Rows[curRow]["crossref"].ToString().Trim()}'")[0];
                    foreach (var pair in refPublication.PairsFieldValue)
                    {
                        if (!pairsFieldValue.ContainsKey(pair.Key))
                            pairsFieldValue.Add(pair.Key, pair.Value);
                    }
                }
                publications.Add(new Publication(pairsFieldValue));
            }
        }
        return publications;
    }
    public static List<Publication> GetPublications(List<string> listWordsToSearch, string order = "title ASC")
    {
        string query = "SELECT * FROM publications WHERE (";
        List<string> listExpressions = GetExpressions(listWordsToSearch);
        for (int i = 0; i < listExpressions.Count; i++)
        {
            query += $" title LIKE '{listExpressions[i]}' ";
            if (i != listExpressions.Count - 1)
                query += "OR";
        }
        query += ") ORDER BY " + order;
        return GetPublications(query);
    }
    //************************************************************************* GET AUTHORS ******************************************************************
    public static List<Author> GetAuthors(string query = "SELECT * FROM authors ORDER BY name")
    {
        List<Author> authors = new List<Author>();
        using (MySqlConnection sqlConnection = new MySqlConnection(connectionString))
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, sqlConnection);
            DataTable tableAuthors = new DataTable();
            adapter.Fill(tableAuthors);
            Dictionary<string, string> pairsFieldValue = new Dictionary<string, string>();
            for (int curRow = 0; curRow < tableAuthors.Rows.Count; curRow++)
            {
                int id = int.Parse(tableAuthors.Rows[curRow]["id"].ToString().Trim());
                string name = tableAuthors.Rows[curRow]["name"].ToString().Trim();
                authors.Add(new Author(id, name));
            }
        }
        return authors;
    }
    public static List<Author> GetAuthors(List<string> listWordsToSearch)
    {
        string query = "SELECT * FROM authors WHERE (";
        List<string> listExpressions = GetExpressions(listWordsToSearch);
        for (int i = 0; i < listExpressions.Count; i++)
        {
            query += $" name LIKE '{listExpressions[i]}' ";
            if (i != listExpressions.Count - 1)
                query += "OR";
        }
        query += ") ORDER BY name";
        return GetAuthors(query);
    }
    public static string GetAuthorName(string id)
    {
        using (MySqlConnection sqlConnection = new MySqlConnection(connectionString))
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter($"SELECT name FROM authors WHERE id = {id}", sqlConnection);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table.Rows[0]["name"].ToString().Trim();
        }
    }
    private static string GetAuthorId(string name)
    {
        DataTable table = new DataTable();
        using (MySqlConnection sqlConnection = new MySqlConnection(connectionString))
        {
            while (true)
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter($"SELECT id FROM authors WHERE name = '{name}'", sqlConnection);
                adapter.Fill(table);
                if (table.Rows.Count != 0)
                    return table.Rows[0]["id"].ToString().Trim();
                else
                    AddAuthor(name);
            }
        }
    }
    //************************************************************************* GET PERMUTATIONS **************************************************************
    private static List<string> GetExpressions(List<string> listWords)
    {
        List<string> expressions = new List<string>(); //all expressions to search
        if (listWords.Count > 5)
        {
            string expression = "%";
            foreach (var word in listWords)
                expression += word + "%";
            expressions.Add(expression);
        }
        else
        {
            var permutations = GetPermutations(listWords, listWords.Count);
            foreach (var permutation in permutations)
            {
                string expression = "%";
                foreach (var word in permutation)
                    expression += word + "%";
                expressions.Add(expression);
            }
        }
        return expressions;
    }
    private static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> items, int count)
    {
        int i = 0;
        foreach (var item in items)
        {
            if (count == 1)
                yield return new T[] { item };
            else
            {
                foreach (var result in GetPermutations(items.Except(new[] { item }), count - 1))
                    yield return new T[] { item }.Concat(result);
            }
            ++i;
        }
    }
    //************************************************************************* ADD PUBLICATION ***************************************************************
    public static void AddPublication(Publication publication)
    {
        Dictionary<string, string> pairFieldFilling = new Dictionary<string, string>();
        foreach (var field in publication.PairsFieldValue)
        {
            if (field.Key != "author")
                pairFieldFilling.Add(field.Key, field.Value);
        }
        string authors = "";
        if (publication.PairsFieldValue.ContainsKey("author")) //Each author is saved as ;id; , e.g. ;1;35;92;
        {
            authors = ";";
            string authorsFull = publication.PairsFieldValue["author"].Replace(" and ", ",");
            string name = "";
            foreach (var ch in authorsFull)
            {
                if (name == "" && ch == ' ') continue;
                if (ch == ',')
                {
                    authors += GetAuthorId(name) + ";";
                    name = "";
                }
                else
                    name += ch;
            }
            authors += GetAuthorId(name) + ";";
        }
        if (!publication.PairsFieldValue.ContainsKey("tag")) //if publication doesn't have a tag (key)
            pairFieldFilling.Add("tag", CreateTag(publication.PairsFieldValue["type"]));
        //build query
        string commandString = "INSERT INTO publications(";
        foreach (var i in pairFieldFilling)
        {
            commandString += i.Key;
            if (!(i.Equals(pairFieldFilling.Last())))
                commandString += ",";
        }
        if (authors != "")
            commandString += ",idauthors";
        commandString += ") VALUES (";
        foreach (var i in pairFieldFilling)
        {
            commandString += $"'{i.Value}'";
            if (!(i.Equals(pairFieldFilling.Last())))
                commandString += ",";
        }
        if (authors != "")
            commandString += $",'{authors}'";
        commandString += ")";
        //add record to databse
        using (MySqlConnection sqlConnection = new MySqlConnection(connectionString)) //create connection
        {
            sqlConnection.Open(); //open connection
            MySqlCommand mySqlCommand = new MySqlCommand(commandString, sqlConnection);
            mySqlCommand.ExecuteNonQuery();
        }
    }
    private static string CreateTag(string publicationType)
    {
        int i = 0;
        string tag = "";
        while (true)
        {
            tag = $"My:{publicationType}{i}";
            if (GetPublications($"SELECT * FROM publications WHERE tag = '{tag}'").Count == 0) //if there is no publication in database with this tag
                break;
            ++i;
        }
        return tag;
    }
    //************************************************************************* ADD AUTHOR ********************************************************************
    private static void AddAuthor(string name)
    {
        using (MySqlConnection sqlConnection = new MySqlConnection(connectionString)) //create connection
        {
            sqlConnection.Open(); //open connection
            MySqlCommand mySqlCommand = new MySqlCommand($"INSERT INTO authors SET name = '{name}'", sqlConnection);
            mySqlCommand.ExecuteNonQuery();
        }
    }
    //****************************************************************** UPDATE PUBLICATION ********************************************************************
    public static void UpdatePublication(string tag, Dictionary<string, string> changes)
    {
        string query = "UPDATE publications SET ";
        foreach (var change in changes)
        {
            query += $"{change.Key} = '{change.Value}' ";
            if (!change.Equals(changes.Last()))
                query += ", ";
        }
        query += $"WHERE tag = '{tag}'";
        using (MySqlConnection sqlConnection = new MySqlConnection(connectionString)) //create connection
        {
            sqlConnection.Open(); //open connection
            MySqlCommand mySqlCommand = new MySqlCommand(query, sqlConnection);
            mySqlCommand.ExecuteNonQuery();
        }
    }
    //******************************************************************* UPDATE AUTHOR ************************************************************************
    public static void UpdateAuthor(int id, string name)
    {
        using (MySqlConnection sqlConnection = new MySqlConnection(connectionString)) //create connection
        {
            sqlConnection.Open(); //open connection
            MySqlCommand mySqlCommand = new MySqlCommand($"UPDATE authors SET name = '{name}' WHERE id = {id}", sqlConnection);
            mySqlCommand.ExecuteNonQuery();
        }
    }
    //****************************************************************** DELETE PUBLICATION ********************************************************************
    public static void DeletePublication(string tag)
    {
        using (MySqlConnection sqlConnection = new MySqlConnection(connectionString)) //create connection
        {
            sqlConnection.Open(); //open connection
            MySqlCommand mySqlCommand = new MySqlCommand($"DELETE FROM publications WHERE tag = '{tag}'", sqlConnection);
            mySqlCommand.ExecuteNonQuery();
        }
    }
}