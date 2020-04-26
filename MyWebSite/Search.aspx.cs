using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Search : System.Web.UI.Page
{
    private static Publication selectedPublication = new Publication(new Dictionary<string, string>());
    private static Author selectedAuthor = new Author(0, "");
    private static List<Publication> PublicationsSearched { get; set; } = new List<Publication>();
    private static List<Author> AuthorsSearched { get; set; } = new List<Author>();
    private static string order = "title ASC";
    private static string searchedTable = "publications";
    private static string searchPhrase = "";
    private static List<string> listWords = new List<string>();
    protected void Page_Load(object sender, EventArgs e)
    {
        ValidationSettings.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
        GridViewResults.PageSize = int.Parse(DDListNumberResults.SelectedValue);
        ButtonExportBibtex.Visible = false;
        PanelChangeName.Visible = false;
        if (searchedTable == "publications")
        {
            GridViewResults.DataKeyNames = new string[] { "Klucz" };
            GridViewResults.DataSource = PublicationsSearched;
            if (PublicationsSearched.Count != 0)
                ButtonExportBibtex.Visible = true;
            if (PublicationsSearched.Count > 100)
                LabelExportBibtex.Visible = true;
            else
                LabelExportBibtex.Visible = false;
        }
        else
        {
            GridViewResults.DataKeyNames = new string[] { "Id" };
            GridViewResults.DataSource = AuthorsSearched;
            LabelExportBibtex.Visible = false;
        }
        GridViewResults.DataBind();
        PanelFields.Visible = false;
    }
    protected void ButtonSearch_Click(object sender, EventArgs e)
    {
        Find();
    }
    protected void ButtonShowBibtex_Click(object sender, EventArgs e)
    {
        if (TextBoxBibtex.Visible == false)
        {
            TextBoxBibtex.Visible = true;
            ButtonShowBibtex.Text = "Ukryj zapis Bibtex";
        }
        else
        {
            TextBoxBibtex.Visible = false;
            ButtonShowBibtex.Text = "Pokaż zapis Bibtex";
        }
    }
    protected void GridViewResults_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (searchedTable == "publications")
        {
            PanelEditDelete.Visible = true;
            PanelPublicationInfo.Visible = true;
            foreach (var publication in PublicationsSearched)
            {
                if (publication.Klucz == (string)GridViewResults.SelectedValue)
                {
                    selectedPublication = new Publication(publication.PairsFieldValue);
                    ShowInfo(selectedPublication);
                    break;
                }
            }
        }
        else
        {
            PanelEdit.Visible = true;
            PanelPublications.Visible = true;
            foreach (var author in AuthorsSearched)
            {
                if (author.Id == (int)GridViewResults.SelectedValue)
                {
                    selectedAuthor = new Author(author.Id, author.Autor);
                    ShowPublications(selectedAuthor);
                    break;
                }
            }
        }
    }
    protected void GridViewResults_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridViewResults.PageIndex = e.NewPageIndex;
        GridViewResults.SelectedIndex = -1;
        PanelPublicationInfo.Visible = false;
        PanelPublications.Visible = false;
        PanelEditDelete.Visible = false;
        PanelEdit.Visible = false;
        ButtonExportBibtex.Visible = false;
        if (searchedTable == "publications")
        {
            GridViewResults.DataKeyNames = new string[] { "Klucz" };
            GridViewResults.DataSource = PublicationsSearched;
            if (PublicationsSearched.Count != 0)
                ButtonExportBibtex.Visible = true;
            if (PublicationsSearched.Count > 100)
                LabelExportBibtex.Visible = true;
            else
                LabelExportBibtex.Visible = false;
        }
        else
        {
            GridViewResults.DataKeyNames = new string[] { "Id" };
            GridViewResults.DataSource = AuthorsSearched;
            LabelExportBibtex.Visible = false;
        }
        GridViewResults.DataBind();
    }
    protected void DDListNumberResults_SelectedIndexChanged(object sender, EventArgs e)
    {
        PanelPublicationInfo.Visible = false;
        PanelPublications.Visible = false;
        PanelEditDelete.Visible = false;
        PanelEdit.Visible = false;
    }
    protected void ButtonExportBibtex_Click(object sender, EventArgs e)
    {
        const int max = 100;
        int index = 0;
        string output = "";
        foreach (var publication in PublicationsSearched)
        {
            ++index;
            output += $"{publication.GetBibtex()}\n\n\n";
            if (index == max)
                break;
        }
        string fileName = "bibtex_publications_";
        for (int i = 0; i < listWords.Count; i++)
        {
            fileName += listWords[i];
            if (i != listWords.Count - 1)
                fileName += "-";
        }
        Response.Clear();
        Response.AppendHeader("Content-Length", output.Length.ToString());
        Response.ContentType = "text/plain";
        Response.AppendHeader("content-disposition", $"attachment; filename={fileName}.bib");
        Response.Flush();
        Response.Write(output);
        Response.End();
    }
    protected void ButtonExportBibtexAuthor_Click(object sender, EventArgs e)
    {
        const int max = 100;
        int index = 0;
        string output = "";
        string fileName = "bibtex_author_";
        if (selectedAuthor.Id != 0)
        {
            List<Publication> publications = PublicationsDAL.GetPublications($"SELECT * FROM publications WHERE idauthors LIKE '%;{selectedAuthor.Id};%' ORDER BY title");
            foreach (var publication in publications)
            {
                ++index;
                output += $"{publication.GetBibtex()}\n\n\n";
                if (index == max)
                    break;
            }
            string name = "";
            foreach (char ch in selectedAuthor.Autor)
            {
                if (char.IsWhiteSpace(ch))
                    name += "-";
                else
                    name += ch;
            }
            fileName += name;
        }
        Response.Clear();
        Response.AppendHeader("Content-Length", output.Length.ToString());
        Response.ContentType = "text/plain";
        Response.AppendHeader("content-disposition", $"attachment; filename={fileName}.bib");
        Response.Flush();
        Response.Write(output);
        Response.End();
    }
    protected void DDListSortMethod_SelectedIndexChanged(object sender, EventArgs e)
    {
        order = DDListSortMethod.SelectedValue;
        if (searchedTable == "publications" && TextBoxSearch.Text != "")
            Find();
    }
    protected void ButtonEditPublication_Click(object sender, EventArgs e)
    {
        PanelFields.Visible = true;
        ClearAllTextBoxes();
        SetInvisible();
        SetVisible(selectedPublication.PairsFieldValue["type"]);
        SetCurrentData(selectedPublication);
    }
    protected void ButtonSavePublication_Click(object sender, EventArgs e)
    {
        if (searchedTable == "publications")
        {
            Dictionary<string, string> changes = GetChanges(selectedPublication);
            if (changes.Count != 0)
            {
                PublicationsDAL.UpdatePublication(selectedPublication.PairsFieldValue["tag"], changes);
                Find();
                foreach (var publication in PublicationsSearched)
                {
                    if (publication.Klucz == (string)GridViewResults.SelectedValue)
                    {
                        selectedPublication = new Publication(publication.PairsFieldValue);
                        ShowInfo(selectedPublication);
                        break;
                    }
                }
                Response.Write("<script>alert('Publikacja zaktualizowana')</script>");
            }
        }
    }
    protected void ButtonDeletePublication_Click(object sender, EventArgs e)
    {
        PublicationsDAL.DeletePublication(selectedPublication.PairsFieldValue["tag"]);
        Find();
        GridViewResults.SelectedIndex = 0;
        foreach (var publication in PublicationsDAL.PublicationsAll)
        {
            if (publication.Klucz == (string)GridViewResults.SelectedValue)
            {
                selectedPublication = new Publication(publication.PairsFieldValue);
                ShowInfo(selectedPublication);
                break;
            }
        }
        Response.Write("<script>alert('Publikacja została usunięta')</script>");
    }
    protected void ButtonEditName_Click(object sender, EventArgs e)
    {
        if (selectedAuthor.Id != 0)
        {
            PanelChangeName.Visible = true;
            TextBoxName.Text = selectedAuthor.Autor;
        }
    }
    protected void ButtonSaveName_Click(object sender, EventArgs e)
    {
        if (selectedAuthor.Id != 0 && TextBoxName.Text != selectedAuthor.Autor) //if something has changed
        {
            PublicationsDAL.UpdateAuthor(selectedAuthor.Id, TextBoxName.Text);
            //update view
            GridViewResults.DataSource = PublicationsDAL.AuthorsAll = PublicationsDAL.GetAuthors();
            GridViewResults.DataBind();
            foreach (var author in PublicationsDAL.AuthorsAll)
            {
                if (author.Id == (int)GridViewResults.SelectedValue)
                {
                    selectedAuthor = new Author(author.Id, author.Autor);
                    ShowPublications(selectedAuthor);
                    break;
                }
            }
            Response.Write("<script>alert('Imię i nazwisko autora zaktualizowane')</script>");
        }
    }
    private void Find()
    {
        searchPhrase = TextBoxSearch.Text;
        GridViewResults.EmptyDataText = "Przykro mi, nie znaleziono pasujących wyników.";
        GridViewResults.PageIndex = 0;
        GridViewResults.SelectedIndex = -1;
        searchedTable = DDListTable.SelectedValue;
        PanelPublicationInfo.Visible = false;
        PanelPublications.Visible = false;
        ButtonExportBibtex.Visible = false;
        PanelEditDelete.Visible = false;
        PanelEdit.Visible = false;
        int numberResults = 0;
        string word = "";
        listWords.Clear();
        foreach (char ch in searchPhrase)
        {
            if (!char.IsWhiteSpace(ch))
                word += ch;
            else
            {
                if (word != " ")
                    listWords.Add(word);
                word = "";
            }
        }
        if (word != "")
            listWords.Add(word);
        if (searchedTable == "publications")
        {
            GridViewResults.DataKeyNames = new string[] { "Klucz" };
            PublicationsSearched = PublicationsDAL.GetPublications(listWords, order);
            GridViewResults.DataSource = PublicationsSearched;
            if (PublicationsSearched.Count != 0)
                ButtonExportBibtex.Visible = true;
            numberResults = PublicationsSearched.Count;
            if (numberResults > 100)
                LabelExportBibtex.Visible = true;
            else
                LabelExportBibtex.Visible = false;
        }
        else
        {
            GridViewResults.DataKeyNames = new string[] { "Id" };
            AuthorsSearched = PublicationsDAL.GetAuthors(listWords);
            GridViewResults.DataSource = AuthorsSearched;
            numberResults = AuthorsSearched.Count;
            LabelExportBibtex.Visible = false;
        }
        GridViewResults.DataBind();
        LabelResults.Text = $"Wyniki wyszukiwania dla: <strong>{searchPhrase}</strong><br/>Znaleziono <strong>{numberResults}</strong> pasujących wyników.";
    }
    private void ShowInfo(Publication publication)
    {
        TextBoxInfo.Text = publication.GetInfo();
        TextBoxBibtex.Text = publication.GetBibtex();
    }
    private void ShowPublications(Author author)
    {
        List<Publication> publications = PublicationsDAL.GetPublications($"SELECT * FROM publications WHERE idauthors LIKE '%;{author.Id};%' ORDER BY {order}");
        TextBoxPublications.Text = $"Publikacje autora {author.Autor}:\n";
        int index = 0;
        foreach (var publication in publications)
        {
            if (publication.Tytul != "")
                TextBoxPublications.Text += $"{++index}. \"{publication.Tytul}\"\n";
        }
    }
    private void ClearAllTextBoxes()
    {
        TextBoxTitle.Text = ""; TextBoxBooktitle.Text = ""; TextBoxSchool.Text = ""; TextBoxEditor.Text = ""; TextBoxYear.Text = "";
        TextBoxMonth.Text = ""; TextBoxChapter.Text = ""; TextBoxPages.Text = ""; TextBoxEdition.Text = ""; TextBoxHowpublished.Text = "";
        TextBoxInstitution.Text = ""; TextBoxJournal.Text = ""; TextBoxOrganization.Text = ""; TextBoxPublisher.Text = ""; TextBoxAddress.Text = "";
        TextBoxSeries.Text = ""; TextBoxNumber.Text = ""; TextBoxVolume.Text = ""; TextBoxNote.Text = "";
    }
    private void SetInvisible()
    {
        PanelBooktitle.Visible = false; PanelSchool.Visible = false; PanelEditor.Visible = false; PanelChapter.Visible = false;
        PanelPages.Visible = false; PanelEdition.Visible = false; PanelHowpublished.Visible = false; PanelInstitution.Visible = false;
        PanelJournal.Visible = false; PanelOrganization.Visible = false; PanelPublisher.Visible = false; PanelAddress.Visible = false;
        PanelSeries.Visible = false; PanelNumber.Visible = false; PanelVolume.Visible = false;
    }
    private void SetVisible(string publicationType)
    {
        if (publicationType == "article"){
            PanelJournal.Visible = true; PanelVolume.Visible = true; PanelNumber.Visible = true; PanelPages.Visible = true;}
        else if (publicationType == "book"){
            PanelEditor.Visible = true; PanelEdition.Visible = true; PanelPublisher.Visible = true; PanelAddress.Visible = true; PanelSeries.Visible = true;
            PanelNumber.Visible = true; PanelVolume.Visible = true;}
        else if (publicationType == "booklet"){
            PanelHowpublished.Visible = true; PanelAddress.Visible = true;}
        else if (publicationType == "inbook"){
            PanelEditor.Visible = true; PanelChapter.Visible = true; PanelPages.Visible = true; PanelEdition.Visible = true; PanelPublisher.Visible = true;
            PanelAddress.Visible = true; PanelSeries.Visible = true; PanelNumber.Visible = true; PanelVolume.Visible = true;}
        else if (publicationType == "incollection"){
            PanelBooktitle.Visible = true; PanelPublisher.Visible = true; PanelEditor.Visible = true; PanelVolume.Visible = true; PanelNumber.Visible = true;
            PanelSeries.Visible = true; PanelChapter.Visible = true; PanelPages.Visible = true; PanelEdition.Visible = true; PanelAddress.Visible = true;}
        else if (publicationType == "inproceedings"){
            PanelBooktitle.Visible = true; PanelEditor.Visible = true; PanelPages.Visible = true; PanelOrganization.Visible = true;
            PanelPublisher.Visible = true; PanelAddress.Visible = true; PanelSeries.Visible = true; PanelNumber.Visible = true; PanelVolume.Visible = true;}
        else if (publicationType == "manual"){
            PanelEdition.Visible = true; PanelOrganization.Visible = true; PanelAddress.Visible = true;}
        else if (publicationType == "mastersthesis"){
            PanelSchool.Visible = true; PanelAddress.Visible = true;}
        else if (publicationType == "misc"){
            PanelHowpublished.Visible = true;}
        else if (publicationType == "phdthesis"){
            PanelSchool.Visible = true; PanelAddress.Visible = true;}
        else if (publicationType == "proceedings"){
            PanelEditor.Visible = true; PanelOrganization.Visible = true; PanelPublisher.Visible = true; PanelAddress.Visible = true;
            PanelSeries.Visible = true; PanelNumber.Visible = true; PanelVolume.Visible = true;}
        else if (publicationType == "techreport"){
            PanelInstitution.Visible = true; PanelAddress.Visible = true; PanelNumber.Visible = true;}
    }
    private void SetCurrentData(Publication pub)
    {
        if (pub.PairsFieldValue.ContainsKey("title")) TextBoxTitle.Text = pub.PairsFieldValue["title"];
        if (pub.PairsFieldValue.ContainsKey("booktitle")) TextBoxBooktitle.Text = pub.PairsFieldValue["booktitle"];
        if (pub.PairsFieldValue.ContainsKey("school")) TextBoxSchool.Text = pub.PairsFieldValue["school"];
        if (pub.PairsFieldValue.ContainsKey("editor")) TextBoxEditor.Text = pub.PairsFieldValue["editor"];
        if (pub.PairsFieldValue.ContainsKey("year")) TextBoxYear.Text = pub.PairsFieldValue["year"];
        if (pub.PairsFieldValue.ContainsKey("month")) TextBoxMonth.Text = pub.PairsFieldValue["month"];
        if (pub.PairsFieldValue.ContainsKey("chapter")) TextBoxChapter.Text = pub.PairsFieldValue["chapter"];
        if (pub.PairsFieldValue.ContainsKey("pages")) TextBoxPages.Text = pub.PairsFieldValue["pages"];
        if (pub.PairsFieldValue.ContainsKey("edition")) TextBoxEdition.Text = pub.PairsFieldValue["edition"];
        if (pub.PairsFieldValue.ContainsKey("howpublished")) TextBoxHowpublished.Text = pub.PairsFieldValue["howpublished"];
        if (pub.PairsFieldValue.ContainsKey("institution")) TextBoxInstitution.Text = pub.PairsFieldValue["institution"];
        if (pub.PairsFieldValue.ContainsKey("journal")) TextBoxJournal.Text = pub.PairsFieldValue["journal"];
        if (pub.PairsFieldValue.ContainsKey("organization")) TextBoxOrganization.Text = pub.PairsFieldValue["organization"];
        if (pub.PairsFieldValue.ContainsKey("publisher")) TextBoxPublisher.Text = pub.PairsFieldValue["publisher"];
        if (pub.PairsFieldValue.ContainsKey("address")) TextBoxAddress.Text = pub.PairsFieldValue["address"];
        if (pub.PairsFieldValue.ContainsKey("series")) TextBoxSeries.Text = pub.PairsFieldValue["series"];
        if (pub.PairsFieldValue.ContainsKey("number")) TextBoxNumber.Text = pub.PairsFieldValue["number"];
        if (pub.PairsFieldValue.ContainsKey("volume")) TextBoxVolume.Text = pub.PairsFieldValue["volume"];
        if (pub.PairsFieldValue.ContainsKey("note")) TextBoxNote.Text = pub.PairsFieldValue["note"];
    }
    private Dictionary<string, string> GetChanges(Publication pub)
    {
        Dictionary<string, string> changes = new Dictionary<string, string>();
        if (pub.PairsFieldValue.ContainsKey("title")){
            if (TextBoxTitle.Text != pub.PairsFieldValue["title"]) changes.Add("title", TextBoxTitle.Text);}
        else{
            if (TextBoxTitle.Text != "") changes.Add("title", TextBoxTitle.Text);}

        if (pub.PairsFieldValue.ContainsKey("booktitle")){
            if (TextBoxBooktitle.Text != pub.PairsFieldValue["booktitle"]) changes.Add("booktitle", TextBoxBooktitle.Text);}
        else{
            if (TextBoxBooktitle.Text != "") changes.Add("booktitle", TextBoxBooktitle.Text);}
        
        if (pub.PairsFieldValue.ContainsKey("school")){
            if (TextBoxSchool.Text != pub.PairsFieldValue["school"]) changes.Add("school", TextBoxSchool.Text);}
        else{
            if (TextBoxSchool.Text != "") changes.Add("school", TextBoxSchool.Text);}
        
        if (pub.PairsFieldValue.ContainsKey("editor")){
            if (TextBoxEditor.Text != pub.PairsFieldValue["editor"]) changes.Add("editor", TextBoxEditor.Text);}
        else{
            if (TextBoxEditor.Text != "") changes.Add("editor", TextBoxEditor.Text);}

        if (pub.PairsFieldValue.ContainsKey("year")){
            if (TextBoxYear.Text != pub.PairsFieldValue["year"].ToString()) changes.Add("year", TextBoxYear.Text);}
        else{
            if (TextBoxYear.Text != "") changes.Add("year", TextBoxYear.Text);}

        if (pub.PairsFieldValue.ContainsKey("month")){
            if (TextBoxMonth.Text != pub.PairsFieldValue["month"].ToString()) changes.Add("month", TextBoxMonth.Text);}
        else{
            if (TextBoxMonth.Text != "") changes.Add("month", TextBoxMonth.Text);}

        if (pub.PairsFieldValue.ContainsKey("chapter")){
            if (TextBoxChapter.Text != pub.PairsFieldValue["chapter"].ToString()) changes.Add("chapter", TextBoxChapter.Text);}
        else{
            if (TextBoxChapter.Text != "") changes.Add("chapter", TextBoxChapter.Text);}

        if (pub.PairsFieldValue.ContainsKey("pages")){
            if (TextBoxPages.Text != pub.PairsFieldValue["pages"].ToString()) changes.Add("pages", TextBoxPages.Text);}
        else{
            if (TextBoxPages.Text != "") changes.Add("pages", TextBoxPages.Text);}
        
        if (pub.PairsFieldValue.ContainsKey("edition")){
            if (TextBoxEdition.Text != pub.PairsFieldValue["edition"].ToString()) changes.Add("edition", TextBoxEdition.Text);}
        else{
            if (TextBoxEdition.Text != "") changes.Add("edition", TextBoxEdition.Text);}
        
        if (pub.PairsFieldValue.ContainsKey("howpublished")){
            if (TextBoxHowpublished.Text != pub.PairsFieldValue["howpublished"].ToString()) changes.Add("howpublished", TextBoxHowpublished.Text);}
        else{
            if (TextBoxHowpublished.Text != "") changes.Add("howpublished", TextBoxHowpublished.Text);}
        
        if (pub.PairsFieldValue.ContainsKey("institution")){
            if (TextBoxInstitution.Text != pub.PairsFieldValue["institution"].ToString()) changes.Add("institution", TextBoxInstitution.Text);}
        else{
            if (TextBoxInstitution.Text != "") changes.Add("institution", TextBoxInstitution.Text);}
        
        if (pub.PairsFieldValue.ContainsKey("journal")){
            if (TextBoxJournal.Text != pub.PairsFieldValue["journal"].ToString()) changes.Add("journal", TextBoxJournal.Text);}
        else{
            if (TextBoxJournal.Text != "") changes.Add("journal", TextBoxJournal.Text);}
        
        if (pub.PairsFieldValue.ContainsKey("organization")){
            if (TextBoxOrganization.Text != pub.PairsFieldValue["organization"].ToString()) changes.Add("organization", TextBoxOrganization.Text);}
        else{
            if (TextBoxOrganization.Text != "") changes.Add("organization", TextBoxOrganization.Text);}
        
        if (pub.PairsFieldValue.ContainsKey("publisher")){
            if (TextBoxPublisher.Text != pub.PairsFieldValue["publisher"].ToString()) changes.Add("publisher", TextBoxPublisher.Text);}
        else{
            if (TextBoxPublisher.Text != "") changes.Add("publisher", TextBoxPublisher.Text);}
        
        if (pub.PairsFieldValue.ContainsKey("address")){
            if (TextBoxAddress.Text != pub.PairsFieldValue["address"].ToString()) changes.Add("address", TextBoxAddress.Text);}
        else{
            if (TextBoxAddress.Text != "") changes.Add("address", TextBoxAddress.Text);}
        
        if (pub.PairsFieldValue.ContainsKey("series")){
            if (TextBoxSeries.Text != pub.PairsFieldValue["series"].ToString()) changes.Add("series", TextBoxSeries.Text);}
        else{
            if (TextBoxSeries.Text != "") changes.Add("series", TextBoxSeries.Text);}
        
        if (pub.PairsFieldValue.ContainsKey("number")){
            if (TextBoxNumber.Text != pub.PairsFieldValue["number"].ToString()) changes.Add("number", TextBoxNumber.Text);}
        else{
            if (TextBoxNumber.Text != "") changes.Add("number", TextBoxNumber.Text);}
        
        if (pub.PairsFieldValue.ContainsKey("volume")){
            if (TextBoxVolume.Text != pub.PairsFieldValue["volume"].ToString()) changes.Add("volume", TextBoxVolume.Text);}
        else{
            if (TextBoxVolume.Text != "") changes.Add("volume", TextBoxVolume.Text);}
        
        if (pub.PairsFieldValue.ContainsKey("note")){
            if (TextBoxNote.Text != pub.PairsFieldValue["note"].ToString()) changes.Add("note", TextBoxNote.Text);}
        else{
            if (TextBoxNote.Text != "") changes.Add("note", TextBoxNote.Text);}

        return changes;
    }
}