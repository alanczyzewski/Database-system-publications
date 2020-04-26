using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    private static Publication selectedPublication = new Publication(new Dictionary<string, string>());
    protected void Page_Load(object sender, EventArgs e)
    {
        GridViewPublications.PageSize = int.Parse(DDListNumberResults.SelectedValue);
        if (!IsPostBack) //only at the beginning
            PublicationsDAL.PublicationsAll = PublicationsDAL.GetPublications($"SELECT * FROM publications ORDER BY {DDListSortMethod.SelectedValue}");
        GridViewPublications.DataSource = PublicationsDAL.PublicationsAll;
        GridViewPublications.DataBind();
        PanelFields.Visible = false;
    }
    protected void GridViewPublications_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridViewPublications.PageIndex = e.NewPageIndex;
        GridViewPublications.SelectedIndex = -1;
        PanelPublicationInfo.Visible = false;
        PanelEditDelete.Visible = false;
        GridViewPublications.DataSource = PublicationsDAL.PublicationsAll;
        GridViewPublications.DataBind();
        //Find();
    }
    protected void GridViewPublications_SelectedIndexChanged(object sender, EventArgs e)
    {
        PanelPublicationInfo.Visible = true;
        PanelEditDelete.Visible = true;
        foreach (var publication in PublicationsDAL.PublicationsAll)
        {
            if (publication.Klucz == (string)GridViewPublications.SelectedValue)
            {
                selectedPublication = new Publication(publication.PairsFieldValue);
                ShowInfo(selectedPublication);
                break;
            }
        }
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
    protected void DDListNumberResults_SelectedIndexChanged(object sender, EventArgs e)
    {
        PanelPublicationInfo.Visible = false;
        PanelEditDelete.Visible = false;
    }
    protected void DDListSortMethod_SelectedIndexChanged(object sender, EventArgs e)
    {
        PanelPublicationInfo.Visible = false;
        PanelEditDelete.Visible = false;
        GridViewPublications.DataSource = PublicationsDAL.GetPublications($"SELECT * FROM publications ORDER BY {DDListSortMethod.SelectedValue}");
        GridViewPublications.DataBind();
    }
    protected void ButtonEdit_Click(object sender, EventArgs e)
    {
        PanelFields.Visible = true;
        ClearAllTextBoxes();
        SetInvisible();
        SetVisible(selectedPublication.PairsFieldValue["type"]);
        SetCurrentData(selectedPublication);
    }
    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> changes = GetChanges(selectedPublication);
        if (changes.Count != 0)
        {
            PublicationsDAL.UpdatePublication(selectedPublication.PairsFieldValue["tag"], changes);
            //update view
            GridViewPublications.DataSource = PublicationsDAL.PublicationsAll = PublicationsDAL.GetPublications($"SELECT * FROM publications " +
              $"ORDER BY {DDListSortMethod.SelectedValue}");
            GridViewPublications.DataBind();
            foreach (var publication in PublicationsDAL.PublicationsAll)
            {
                if (publication.Klucz == (string)GridViewPublications.SelectedValue)
                {
                    selectedPublication = new Publication(publication.PairsFieldValue);
                    ShowInfo(selectedPublication);
                    break;
                }
            }
            Response.Write("<script>alert('Publikacja zaktualizowana')</script>");
        }
    }
    protected void ButtonDelete_Click(object sender, EventArgs e)
    {
        PublicationsDAL.DeletePublication(selectedPublication.PairsFieldValue["tag"]);
        GridViewPublications.DataSource = PublicationsDAL.PublicationsAll = PublicationsDAL.GetPublications($"SELECT * FROM publications " +
              $"ORDER BY {DDListSortMethod.SelectedValue}");
        GridViewPublications.DataBind();
        GridViewPublications.SelectedIndex = 0;
        foreach (var publication in PublicationsDAL.PublicationsAll)
        {
            if (publication.Klucz == (string)GridViewPublications.SelectedValue)
            {
                selectedPublication = new Publication(publication.PairsFieldValue);
                ShowInfo(selectedPublication);
                break;
            }
        }
        Response.Write("<script>alert('Publikacja została usunięta')</script>");
    }
    private void ShowInfo(Publication publication)
    {
        TextBoxInfo.Text = publication.GetInfo();
        TextBoxBibtex.Text = publication.GetBibtex();
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