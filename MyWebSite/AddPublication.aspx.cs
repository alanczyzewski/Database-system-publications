using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddPublication : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetInfo();
            ClearAllTextBoxes();
            SetInvisible();
            SetVisible();
        }
    }
    protected void ListBoxType_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetInfo();
        ClearAllTextBoxes();
        SetInvisible();
        SetVisible();
    }

    private void SetInfo()
    {
        TextBoxTypeInfo.Text = "Pola wymagane:\n";
        if (ListBoxType.SelectedValue == "article")
            TextBoxTypeInfo.Text += "Autor\nTytuł\nCzasopismo\nRok";
        else if (ListBoxType.SelectedValue == "book")
            TextBoxTypeInfo.Text += "Autor lub Redaktor\nTytuł\nWydawca\nRok";
        else if (ListBoxType.SelectedValue == "booklet")
            TextBoxTypeInfo.Text += "Tytuł";
        else if (ListBoxType.SelectedValue == "inbook")
            TextBoxTypeInfo.Text += "Autor lub Redaktor\nTytuł\nNumer rozdziału lub Strony\nWydawca\nRok";
        else if (ListBoxType.SelectedValue == "incollection")
            TextBoxTypeInfo.Text += "Autor\nTytuł\nTytuł książki\nWydawca\nRok";
        else if (ListBoxType.SelectedValue == "inproceedings")
            TextBoxTypeInfo.Text += "Autor\nTytuł\nTytuł książki\nRok";
        else if (ListBoxType.SelectedValue == "manual")
            TextBoxTypeInfo.Text += "Tytuł";
        else if (ListBoxType.SelectedValue == "mastersthesis")
            TextBoxTypeInfo.Text += "Autor\nTytuł\nInstytucja\nRok";
        else if (ListBoxType.SelectedValue == "misc")
            TextBoxTypeInfo.Text += "brak";
        else if (ListBoxType.SelectedValue == "phdthesis")
            TextBoxTypeInfo.Text += "Autor\nTytuł\nInstytucja\nRok";
        else if (ListBoxType.SelectedValue == "proceedings")
            TextBoxTypeInfo.Text += "Tytuł\nRok";
        else if (ListBoxType.SelectedValue == "techreport")
            TextBoxTypeInfo.Text += "Autor\nTytuł\nInstytucja\nRok";
        else if (ListBoxType.SelectedValue == "unpublished")
            TextBoxTypeInfo.Text += "Autor\nTytuł\nDodatkowe informacje";
    }
    private void ClearAllTextBoxes()
    {
        TextBoxTitle.Text = ""; TextBoxBooktitle.Text = ""; TextBoxAuthor.Text = ""; TextBoxSchool.Text = ""; TextBoxEditor.Text = ""; TextBoxYear.Text = "";
        TextBoxMonth.Text = ""; TextBoxChapter.Text = ""; TextBoxPages.Text = ""; TextBoxEdition.Text = ""; TextBoxHowpublished.Text = "";
        TextBoxInstitution.Text = ""; TextBoxJournal.Text = ""; TextBoxOrganization.Text = ""; TextBoxPublisher.Text = ""; TextBoxAddress.Text = "";
        TextBoxSeries.Text = ""; TextBoxNumber.Text = ""; TextBoxVolume.Text = ""; TextBoxNote.Text = "";
    }
    private void SetInvisible()
    {
        PanelBooktitle.Visible = false; PanelAuthor.Visible = false; PanelSchool.Visible = false;PanelEditor.Visible = false; PanelChapter.Visible = false;
        PanelPages.Visible = false; PanelEdition.Visible = false; PanelHowpublished.Visible = false; PanelInstitution.Visible = false;
        PanelJournal.Visible = false; PanelOrganization.Visible = false; PanelPublisher.Visible = false; PanelAddress.Visible = false;
        PanelSeries.Visible = false; PanelNumber.Visible = false; PanelVolume.Visible = false;
    }
    private void SetVisible()
    {
        if (ListBoxType.SelectedValue == "article"){
            PanelAuthor.Visible = true; PanelJournal.Visible = true; PanelVolume.Visible = true; PanelNumber.Visible = true; PanelPages.Visible = true;}
        else if (ListBoxType.SelectedValue == "book"){
            PanelAuthor.Visible = true; PanelEditor.Visible = true; PanelEdition.Visible = true; PanelPublisher.Visible = true; PanelAddress.Visible = true;
            PanelSeries.Visible = true; PanelNumber.Visible = true; PanelVolume.Visible = true;}
        else if (ListBoxType.SelectedValue == "booklet"){
            PanelAuthor.Visible = true; PanelHowpublished.Visible = true; PanelAddress.Visible = true;}
        else if (ListBoxType.SelectedValue == "inbook"){
            PanelAuthor.Visible = true; PanelEditor.Visible = true; PanelChapter.Visible = true; PanelPages.Visible = true; PanelEdition.Visible = true;
            PanelPublisher.Visible = true; PanelAddress.Visible = true; PanelSeries.Visible = true; PanelNumber.Visible = true; PanelVolume.Visible = true;}
        else if (ListBoxType.SelectedValue == "incollection"){
            PanelAuthor.Visible = true; PanelBooktitle.Visible = true; PanelPublisher.Visible = true; PanelEditor.Visible = true; PanelVolume.Visible = true;
            PanelNumber.Visible = true; PanelSeries.Visible = true; PanelChapter.Visible = true; PanelPages.Visible = true; PanelEdition.Visible = true;
            PanelAddress.Visible = true;}
        else if (ListBoxType.SelectedValue == "inproceedings"){
            PanelBooktitle.Visible = true; PanelAuthor.Visible = true; PanelEditor.Visible = true; PanelPages.Visible = true; PanelOrganization.Visible = true;
            PanelPublisher.Visible = true; PanelAddress.Visible = true; PanelSeries.Visible = true; PanelNumber.Visible = true; PanelVolume.Visible = true;}
        else if (ListBoxType.SelectedValue == "manual"){
            PanelAuthor.Visible = true; PanelEdition.Visible = true; PanelOrganization.Visible = true; PanelAddress.Visible = true;}
        else if (ListBoxType.SelectedValue == "mastersthesis"){
            PanelAuthor.Visible = true; PanelSchool.Visible = true; PanelAddress.Visible = true;}
        else if (ListBoxType.SelectedValue == "misc"){
            PanelAuthor.Visible = true; PanelHowpublished.Visible = true;}
        else if (ListBoxType.SelectedValue == "phdthesis"){
            PanelAuthor.Visible = true; PanelSchool.Visible = true; PanelAddress.Visible = true;}
        else if (ListBoxType.SelectedValue == "proceedings"){
            PanelEditor.Visible = true; PanelOrganization.Visible = true; PanelPublisher.Visible = true; PanelAddress.Visible = true;
            PanelSeries.Visible = true; PanelNumber.Visible = true; PanelVolume.Visible = true;}
        else if (ListBoxType.SelectedValue == "techreport"){
            PanelAuthor.Visible = true; PanelInstitution.Visible = true; PanelAddress.Visible = true; PanelNumber.Visible = true;}
        else if (ListBoxType.SelectedValue == "unpublished"){
            PanelAuthor.Visible = true;}
    }
    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        string missingString = CheckRequiredFields();
        if (missingString != "")
        {
            Response.Write("<script>alert('" + missingString + "')</script>");
            return;
        }
        Dictionary<string, string> pairs = new Dictionary<string, string>();
        pairs.Add("type", ListBoxType.SelectedValue);
        if (TextBoxTitle.Text != "") { pairs.Add("title", TextBoxTitle.Text); }
        if (TextBoxBooktitle.Text != "") { pairs.Add("booktitle", TextBoxBooktitle.Text); }
        if (TextBoxAuthor.Text != "") { pairs.Add("author", TextBoxAuthor.Text); }
        if (TextBoxSchool.Text != "") { pairs.Add("school", TextBoxSchool.Text); }
        if (TextBoxEditor.Text != "") { pairs.Add("editor", TextBoxEditor.Text); }
        if (TextBoxYear.Text != "") { pairs.Add("year", TextBoxYear.Text); }
        if (TextBoxMonth.Text != "") { pairs.Add("month", TextBoxMonth.Text); }
        if (TextBoxChapter.Text != "") { pairs.Add("chapter", TextBoxChapter.Text); }
        if (TextBoxPages.Text != "") { pairs.Add("pages", TextBoxPages.Text); }
        if (TextBoxEdition.Text != "") { pairs.Add("edition", TextBoxEdition.Text); }
        if (TextBoxHowpublished.Text != "") { pairs.Add("howpublished", TextBoxHowpublished.Text); }
        if (TextBoxInstitution.Text != "") { pairs.Add("institution", TextBoxInstitution.Text); }
        if (TextBoxJournal.Text != "") { pairs.Add("journal", TextBoxJournal.Text); }
        if (TextBoxOrganization.Text != "") { pairs.Add("organization", TextBoxOrganization.Text); }
        if (TextBoxPublisher.Text != "") { pairs.Add("publisher", TextBoxPublisher.Text); }
        if (TextBoxAddress.Text != "") { pairs.Add("address", TextBoxAddress.Text); }
        if (TextBoxSeries.Text != "") { pairs.Add("series", TextBoxSeries.Text); }
        if (TextBoxNumber.Text != "") { pairs.Add("number", TextBoxNumber.Text); }
        if (TextBoxVolume.Text != "") { pairs.Add("volume", TextBoxVolume.Text); }
        if (TextBoxNote.Text != "") { pairs.Add("note", TextBoxNote.Text); }
        PublicationsDAL.AddPublication(new Publication(pairs));
        ClearAllTextBoxes();
        Response.Write("<script>alert('Dodawanie nowej publikacji powiodło się')</script>");
    }
    private string CheckRequiredFields()
    {
        bool missing = false;
        if (ListBoxType.SelectedValue == "misc"){
            if (TextBoxAuthor.Text == "" && TextBoxTitle.Text == "" && TextBoxNote.Text == "" && TextBoxHowpublished.Text == "" && TextBoxMonth.Text == ""
                && TextBoxYear.Text == "") { return "Nie możesz pozostawić wszystkich pustych pól"; } }
        string result = "Nie zostały podane następujące wymagane pola: "; //"Nie udało się dodać publikacji";
        if (ListBoxType.SelectedValue == "article") {
            if (TextBoxAuthor.Text == "") { result += "Autor. "; missing = true; }
            if (TextBoxTitle.Text == "") { result += "Tytuł. "; missing = true; }
            if (TextBoxJournal.Text == "") { result += "Czasopismo. "; missing = true; }
            if (TextBoxYear.Text == "") { result += "Rok. "; missing = true; } }
        else if (ListBoxType.SelectedValue == "book") {
            if (TextBoxAuthor.Text == "" && TextBoxEditor.Text == "") { result += "Autor lub Redaktor. "; missing = true; }
            if (TextBoxTitle.Text == "") { result += "Tytuł. "; missing = true; }
            if (TextBoxPublisher.Text == "") { result += "Wydawca. "; missing = true; }
            if (TextBoxYear.Text == "") { result += "Rok. ";  missing = true; } }
        else if (ListBoxType.SelectedValue == "booklet") {
            if (TextBoxTitle.Text == "") { result += "Tytuł. "; missing = true; } }
        else if (ListBoxType.SelectedValue == "inbook"){
            if (TextBoxAuthor.Text == "" && TextBoxEditor.Text == "") { result += "Autor lub Redaktor. "; missing = true; }
            if (TextBoxTitle.Text == "") { result += "Tytuł. "; missing = true; }
            if (TextBoxChapter.Text == "" && TextBoxPages.Text == "") { result += "Numer rozdziału lub Strony. "; missing = true; }
            if (TextBoxPublisher.Text == "") { result += "Wydawca. "; missing = true; }
            if (TextBoxYear.Text == "") { result += "Rok. ";  missing = true; } }
        else if (ListBoxType.SelectedValue == "incollection"){
            if (TextBoxAuthor.Text == "") { result += "Autor. "; missing = true; }
            if (TextBoxTitle.Text == "") { result += "Tytuł. "; missing = true; }
            if (TextBoxBooktitle.Text == "") { result += "Tytuł książki. "; missing = true; }
            if (TextBoxPublisher.Text == "") { result += "Wydawca. "; missing = true; }
            if (TextBoxYear.Text == "") { result += "Rok. ";  missing = true; } }
        else if (ListBoxType.SelectedValue == "inproceedings"){
            if (TextBoxAuthor.Text == "") { result += "Autor. "; missing = true; }
            if (TextBoxTitle.Text == "") { result += "Tytuł. "; missing = true; }
            if (TextBoxBooktitle.Text == "") { result += "Tytuł książki. "; missing = true; }
            if (TextBoxYear.Text == "") { result += "Rok. ";  missing = true; } }
        else if (ListBoxType.SelectedValue == "manual"){
            if (TextBoxTitle.Text == "") { result += "Tytuł. "; missing = true; } }
        else if (ListBoxType.SelectedValue == "mastersthesis"){
            if (TextBoxAuthor.Text == "") { result += "Autor. "; missing = true; }
            if (TextBoxTitle.Text == "") { result += "Tytuł. "; missing = true; }
            if (TextBoxSchool.Text == "") { result += "Instytucja. "; missing = true; }
            if (TextBoxYear.Text == "") { result += "Rok. ";  missing = true; } }
        else if (ListBoxType.SelectedValue == "phdthesis"){
            if (TextBoxAuthor.Text == "") { result += "Autor. "; missing = true; }
            if (TextBoxTitle.Text == "") { result += "Tytuł. "; missing = true; }
            if (TextBoxSchool.Text == "") { result += "Instytucja. "; missing = true; }
            if (TextBoxYear.Text == "") { result += "Rok. ";  missing = true; } }
        else if (ListBoxType.SelectedValue == "proceedings"){
            if (TextBoxTitle.Text == "") { result += "Tytuł. "; missing = true; }
            if (TextBoxYear.Text == "") { result += "Rok. ";  missing = true; } }
        else if (ListBoxType.SelectedValue == "techreport"){
            if (TextBoxAuthor.Text == "") { result += "Autor. "; missing = true; }
            if (TextBoxTitle.Text == "") { result += "Tytuł. "; missing = true; }
            if (TextBoxInstitution.Text == "") { result += "Instytucja. "; missing = true; }
            if (TextBoxYear.Text == "") { result += "Rok. ";  missing = true; } }
        else if (ListBoxType.SelectedValue == "unpublished"){
            if (TextBoxAuthor.Text == "") { result += "Autor. "; missing = true; }
            if (TextBoxTitle.Text == "") { result += "Tytuł. "; missing = true; }
            if (TextBoxNote.Text == "") { result += "Dodatkowe informacje. ";  missing = true; } }
        if (missing)
            return result;
        return "";
    }

    
}