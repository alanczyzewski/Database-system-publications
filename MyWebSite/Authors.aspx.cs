using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Authors : System.Web.UI.Page
{
    private static Author selectedAuthor = new Author(0, "");
    protected void Page_Load(object sender, EventArgs e)
    {
        GridViewAuthors.PageSize = int.Parse(DDListNumberResults.SelectedValue);
        if (!IsPostBack) //only at the beginning
            PublicationsDAL.AuthorsAll = PublicationsDAL.GetAuthors();
        GridViewAuthors.DataSource = PublicationsDAL.AuthorsAll;
        GridViewAuthors.DataBind();
        PanelChangeName.Visible = false;
    }
    protected void GridViewAuthors_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridViewAuthors.PageIndex = e.NewPageIndex;
        GridViewAuthors.SelectedIndex = -1;
        PanelPublications.Visible = false;
        PanelEdit.Visible = false;
        GridViewAuthors.DataSource = PublicationsDAL.AuthorsAll;
        GridViewAuthors.DataBind();
    }
    protected void GridViewAuthors_SelectedIndexChanged(object sender, EventArgs e)
    {
        PanelPublications.Visible = true;
        PanelEdit.Visible = true;
        foreach (var author in PublicationsDAL.AuthorsAll)
        {
            if (author.Id == (int)GridViewAuthors.SelectedValue)
            {
                selectedAuthor = new Author(author.Id, author.Autor);
                ShowPublications(selectedAuthor);
                break;
            }
        }
    }
    protected void DDListNumberResults_SelectedIndexChanged(object sender, EventArgs e)
    {
        PanelPublications.Visible = false;
        PanelEdit.Visible = false;
    }
    protected void ButtonEdit_Click(object sender, EventArgs e)
    {
        if (selectedAuthor.Id != 0)
        {
            PanelChangeName.Visible = true;
            TextBoxName.Text = selectedAuthor.Autor;
        }
    }
    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        if (selectedAuthor.Id != 0 && TextBoxName.Text != selectedAuthor.Autor) //if something has changed
        {
            PublicationsDAL.UpdateAuthor(selectedAuthor.Id, TextBoxName.Text);
            //update view
            GridViewAuthors.DataSource = PublicationsDAL.AuthorsAll = PublicationsDAL.GetAuthors();
            GridViewAuthors.DataBind();
            foreach (var author in PublicationsDAL.AuthorsAll)
            {
                if (author.Id == (int)GridViewAuthors.SelectedValue)
                {
                    selectedAuthor = new Author(author.Id, author.Autor);
                    ShowPublications(selectedAuthor);
                    break;
                }
            }
            Response.Write("<script>alert('Imię i nazwisko autora zaktualizowane')</script>");
        }
    }
    private void ShowPublications(Author author)
    {
        List<Publication> publications = PublicationsDAL.GetPublications($"SELECT * FROM publications WHERE idauthors LIKE '%;{author.Id};%' ORDER BY title");
        TextBoxPublications.Text = $"Publikacje autora {author.Autor}:\n";
        int index = 0;
        foreach (var publication in publications)
        {
            if (publication.Tytul != "")
                TextBoxPublications.Text += $"{++index}. \"{publication.Tytul}\"\n";
        }
    }
}