using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Publication
{
    public Dictionary<string, string> PairsFieldValue { get; set; }
    public string Tytul { get; set; } = "";
    public string Autorzy { get; set; } = "";
    public int Rok { get; set; } = 0;
    public string Klucz { get; set; }
    public Publication(Dictionary<string, string> pairsFieldValue)
    {
        PairsFieldValue = new Dictionary<string, string>(pairsFieldValue);
        foreach (var pair in pairsFieldValue)
        {
            if (pair.Key == "tag")
                Klucz = pair.Value;
            else if (pair.Key == "title")
                Tytul = pair.Value;
            else if (pair.Key == "author")
                Autorzy = PairsFieldValue["author"] = pair.Value.Replace(" and ", ", ");
            else if (pair.Key == "editor")
                PairsFieldValue["editor"] = pair.Value.Replace(" and ", ", ");
            else if (pair.Key == "year")
            {
                if (int.TryParse(pair.Value, out int temp))
                    Rok = temp;
            }
        }
    }
    public string GetInfo()
    {
        string info = "";
        info += $"Typ publikacji: {ChangeToPolish(PairsFieldValue["type"])}\n";
        foreach (var pair in PairsFieldValue)
        {
            if (pair.Key != "tag" && pair.Key != "type" && pair.Key != "crossref")
                info += $"{ChangeToPolish(pair.Key)}: {pair.Value}\n";
        }
        return info;
    }
    public string GetBibtex()
    {
        string bibtex = "@" + PairsFieldValue["type"] + "{" + PairsFieldValue["tag"] + ",\n";
        foreach (var field in PairsFieldValue)
        {
            if (field.Key == "tag" || field.Key == "type")// || field.Key == "author" || field.Key == "title")
                continue;
            else
            {
                bibtex += "  " + field.Key;
                for (int i = 0; i < 12 - field.Key.Length; i++) bibtex += " ";
                bibtex += "= {";
                if (field.Key == "author")
                    bibtex += Autorzy.Replace(", ", " and\n                 ");
                else if (field.Key == "editor")
                    bibtex += field.Value.Replace(", ", " and\n                 ");
                else
                    bibtex += SetLimitCharactersPerLine(field.Value);

                if (field.Equals(PairsFieldValue.Last()))
                    bibtex += "}\n";
                else
                    bibtex += "},\n";
            }
        }
        bibtex += "}";
        return bibtex;
    }
    private string SetLimitCharactersPerLine(string text, int limit = 60)
    {
        string result = "";
        int currentCharacters = 0;
        for (int i = 0; i < text.Length; i++)
        {
            ++currentCharacters;
            if (currentCharacters > limit && text[i] == ' ')
            {
                result += "\n                 ";
                currentCharacters = 0;
            }
            else
                result += text[i];
        }
        return result;
    }
    private string ChangeToPolish(string s)
    {
        switch(s)
        {
            case "article": return "Artykuł";
            case "inproceedings": return "Artykuł w materiałach konferencyjnych";
            case "booklet": return "Broszura";
            case "inbook": return "Część książki (rozdział lub zakres stron)";
            case "incollection": return "Część książki z własnym tytułem";
            case "manual": return "Dokumentacja techniczna";
            case "unpublished": return "Dokument nieopublikowany";
            case "book": return "Książka";
            case "proceedings": return "Materiały konferencyjne";
            case "mastersthesis": return "Praca magisterska";
            case "techreport": return "Raport opublikowany przez uczelnię";
            case "phdthesis": return "Rozprawa doktorska";
            case "misc": return "Pozostałe";

            case "address": return "Adres wydawcy";
            case "author": return "Autorzy";
            case "booktitle": return "Tytuł książki";
            case "chapter": return "Numer rozdziału";
            case "edition": return "Numer wydania";
            case "editor": return "Redaktor";
            case "howpublished": return "Sposób wydania";
            case "institution": return "Instytucja sponsorująca wydanie raportu";
            case "journal": return "Czasopismo";
            case "month": return "Miesiąc publikacji";
            case "note": return "Dodatkowe informacje";
            case "number": return "Numer";
            case "organization": return "Organizacja";
            case "pages": return "Strony";
            case "publisher": return "Wydawca";
            case "school": return "Instytucja";
            case "series": return "Nazwa serii";
            case "title": return "Tytuł";
            case "volume": return "Tom";
            case "year": return "Rok publikacji";
            default: return "";
        }
    }
}