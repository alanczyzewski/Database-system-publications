<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="PanelHeader" runat="server" HorizontalAlign ="Center">
        <asp:Label ID = "LabelHeader" runat = "server" CssClass="header" Text = "Wszystkie publikacje" ></asp:Label>
        <br />
        <br />
    </asp:Panel>
    <asp:Panel ID="PanelDisplayMethod" runat="server" HorizontalAlign="Center">
            <br />
            <asp:Label ID="LabelNumberResults" runat="server" Text="Liczba wyników na stronę"></asp:Label>
            &ensp;
            <asp:DropDownList ID="DDListNumberResults" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDListNumberResults_SelectedIndexChanged" CssClass="dropDownList">
                <asp:ListItem Selected="True">10</asp:ListItem>
                <asp:ListItem>25</asp:ListItem>
                <asp:ListItem>50</asp:ListItem>
                <asp:ListItem>100</asp:ListItem>
                <asp:ListItem>200</asp:ListItem>
            </asp:DropDownList>
            &emsp;&emsp;
            <asp:Label ID="LabelSortMethod" runat="server" Text="Sortuj według"></asp:Label>
            &ensp;
            <asp:DropDownList ID="DDListSortMethod" runat="server" OnSelectedIndexChanged="DDListSortMethod_SelectedIndexChanged" CssClass="dropDownList" AutoPostBack="True">
                <asp:ListItem Value="title ASC" Selected="True">tytułu rosnąco</asp:ListItem>
                <asp:ListItem Value="title DESC">tytułu malejąco</asp:ListItem>
                <asp:ListItem Value="year ASC">roku rosnąco</asp:ListItem>
                <asp:ListItem Value="year DESC">roku malejąco</asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
    </asp:Panel>
    <asp:Panel ID="PanelMain" runat="server">
        <asp:GridView ID="GridViewPublications" Width="1200px" runat="server" AllowPaging="True" AutoGenerateSelectButton="True" BackColor="#CCFFFF" 
            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="Klucz" 
            OnPageIndexChanging="GridViewPublications_PageIndexChanging" OnSelectedIndexChanged="GridViewPublications_SelectedIndexChanged">
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>
        <br />
        <br />
    </asp:Panel>
    <asp:Panel ID="PanelEditDelete" runat="server" HorizontalAlign="Center" Visible="False">
        <asp:Button ID="ButtonEdit" Width="140px" runat="server" Text="Edytuj" CssClass="button" OnClick="ButtonEdit_Click" />
        &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;
        <asp:Button ID="ButtonDelete" runat="server" Text="Usuń" CssClass="button" Width="140px" OnClick="ButtonDelete_Click" />
        <br />
        <br />
    </asp:Panel>
    <asp:Panel ID="PanelFields" HorizontalAlign="Center" runat="server" Visible="false">
        <asp:Panel ID="PanelTitle" runat="server">
            <asp:Label ID="LabelTitle" runat="server" Text="Tytuł"></asp:Label>
            &emsp;
            <asp:TextBox ID="TextBoxTitle" CssClass="textBoxInput" runat="server" Width="500px" TextMode="MultiLine"></asp:TextBox>
            <br />
            <br />
        </asp:Panel>
        <asp:Panel ID="PanelBooktitle" runat="server" Visible="False">
            <asp:Label ID="LabelBooktitle" runat="server" Text="Tytuł książki"></asp:Label>
            &emsp;
            <asp:TextBox ID="TextBoxBooktitle" runat="server" TextMode="MultiLine" Width="453px" CssClass="textBoxInput"></asp:TextBox>
            <br />
            <br />
        </asp:Panel>
        <asp:Panel ID="PanelSchool" runat="server" Visible="False">
            <asp:Label ID="LabelSchool" runat="server" Text="Instytucja (szkoła wyższa)"></asp:Label>
            &emsp;
            <asp:TextBox ID="TextBoxSchool" runat="server" TextMode="MultiLine" Width="371px" CssClass="textBoxInput"></asp:TextBox>
            <br />
            <br />
        </asp:Panel>
        <asp:Panel ID="PanelEditor" runat="server" Visible="False">
            <asp:Label ID="LabelEditor" runat="server" Text="Redaktor"></asp:Label>
            &emsp;
            <asp:TextBox ID="TextBoxEditor" runat="server" TextMode="MultiLine" Width="471px" CssClass="textBoxInput"></asp:TextBox>
            <br />
            <br />    
        </asp:Panel>
        <asp:Panel ID="PanelPages" runat="server" Visible="False">
            <asp:Label ID="LabelPages" runat="server" Text="Strony"></asp:Label>
            &emsp;
            <asp:TextBox ID="TextBoxPages" runat="server" TextMode="MultiLine" Width="490px" CssClass="textBoxInput"></asp:TextBox>
            <br />
            <br />
        </asp:Panel>
        <asp:Panel ID="PanelHowpublished" runat="server" Visible="False">
            <asp:Label ID="LabelHowpublished" runat="server" Text="Sposób wydania"></asp:Label>
            &emsp;
            <asp:TextBox ID="TextBoxHowpublished" runat="server" TextMode="MultiLine" Width="427px" CssClass="textBoxInput"></asp:TextBox>
            <br />
            <br />
        </asp:Panel>
        <asp:Panel ID="PanelInstitution" runat="server" Visible="False">
            <asp:Label ID="LabelInstitution" runat="server" Text="Instytucja"></asp:Label>
            &emsp;
            <asp:TextBox ID="TextBoxInstitution" runat="server" TextMode="MultiLine" Width="468px" CssClass="textBoxInput"></asp:TextBox>
            <br />
            <br />
        </asp:Panel>
        <asp:Panel ID="PanelJournal" runat="server" Visible="False">
            <asp:Label ID="LabelJournal" runat="server" Text="Czasopismo"></asp:Label>
            &emsp;
            <asp:TextBox ID="TextBoxJournal" runat="server" TextMode="MultiLine" Width="453px" CssClass="textBoxInput"></asp:TextBox>
            <br />
            <br />
        </asp:Panel>
        <asp:Panel ID="PanelOrganization" runat="server" Visible="False">
            <asp:Label ID="LabelOrganization" runat="server" Text="Organizacja"></asp:Label>
            &emsp;
            <asp:TextBox ID="TextBoxOrganization" runat="server" TextMode="MultiLine" Width="456px" CssClass="textBoxInput"></asp:TextBox>
            <br />
            <br />
        </asp:Panel>
        <asp:Panel ID="PanelPublisher" runat="server" Visible="False">
            <asp:Label ID="LabelPublisher" runat="server" Text="Wydawca"></asp:Label>
            &emsp;
            <asp:TextBox ID="TextBoxPublisher" runat="server" TextMode="MultiLine" Width="468px" CssClass="textBoxInput"></asp:TextBox>
            <br />
            <br />
        </asp:Panel>
        <asp:Panel ID="PanelAddress" runat="server" Visible="False">
            <asp:Label ID="LabelAddress" runat="server" Text="Adres wydawcy"></asp:Label>
            &emsp;
            <asp:TextBox ID="TextBoxAddress" runat="server" TextMode="MultiLine" Width="425px" CssClass="textBoxInput"></asp:TextBox>
            <br />
            <br />
        </asp:Panel>
        <asp:Panel ID="PanelSeries" runat="server" Visible="False">
            <asp:Label ID="LabelSeries" runat="server" Text="Nazwa serii"></asp:Label>
            &emsp;
            <asp:TextBox ID="TextBoxSeries" runat="server" TextMode="MultiLine" Width="451px" CssClass="textBoxInput"></asp:TextBox>
            <br />
            <br />
        </asp:Panel>
        <asp:Panel ID="PanelNote" runat="server">
            <asp:Label ID="LabelNote" runat="server" Text="Dodatkowe informacje"></asp:Label>
            &emsp;
            <asp:TextBox ID="TextBoxNote" runat="server" TextMode="MultiLine" Width="386px" CssClass="textBoxInput"></asp:TextBox>
            <br />
            <br />
        </asp:Panel>
        <asp:Panel ID="PanelEdition" runat="server" Visible="False">
            <asp:Label ID="LabelEdition" runat="server" Text="Numer wydania"></asp:Label>
            &emsp;
            <asp:TextBox ID="TextBoxEdition" runat="server" CssClass="textBoxInput"></asp:TextBox>
            <br />
            <br />
        </asp:Panel>
        <asp:Panel ID="PanelChapter" runat="server" Visible="False">
            <asp:Label ID="LabelChapter" runat="server" Text="Numer rozdziału"></asp:Label>
            &emsp;
            <asp:TextBox ID="TextBoxChapter" runat="server" Width="119px" CssClass="textBoxInput"></asp:TextBox>
            <br />
            <br />
        </asp:Panel>
        <asp:Panel ID="PanelNumber" runat="server" Visible="False">
            <asp:Label ID="LabelNumber" runat="server" Text="Numer"></asp:Label>
            &emsp;
            <asp:TextBox ID="TextBoxNumber" runat="server" Width="177px" CssClass="textBoxInput"></asp:TextBox>
            <br />
            <br />
        </asp:Panel>
        <asp:Panel ID="PanelVolume" runat="server" Visible="False">
            <asp:Label ID="LabelVolume" runat="server" Text="Tom"></asp:Label>
            &emsp;
            <asp:TextBox ID="TextBoxVolume" runat="server" Width="192px" CssClass="textBoxInput"></asp:TextBox>
            <br />
            <br />
        </asp:Panel>
        <asp:Panel ID="PanelYear" runat="server">
            <asp:Label ID="LabelYear" runat="server" Text="Rok"></asp:Label>
            &emsp;
            <asp:TextBox ID="TextBoxYear" runat="server" Width="195px" MaxLength="4" TextMode="Number" CssClass="textBoxInput"></asp:TextBox>
            <br />
            <br />
        </asp:Panel>
        <asp:Panel ID="PanelMonth" runat="server">
            <asp:Label ID="LabelMonth" runat="server" Text="Miesiąc"></asp:Label>
            &emsp;
            <asp:TextBox ID="TextBoxMonth" runat="server" Width="173px" CssClass="textBoxInput"></asp:TextBox>
            <br />
            <br />
        </asp:Panel>
        <asp:Panel ID="PanelSaveCancel" runat="server" HorizontalAlign="Center">
            <asp:Button ID="ButtonSave" Width="140px" runat="server" Text="Zapisz" CssClass="button" OnClick="ButtonSave_Click" />
            &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;
            <asp:Button ID="ButtonCancel" runat="server" Text="Cofnij" CssClass="button" Width="140px"/>
            <br />
            <br />
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="PanelPublicationInfo" runat="server" Visible="false" HorizontalAlign="Center">
        <asp:TextBox ID="TextBoxInfo" runat="server" CssClass="textBoxOnlyRead" Height="162px" Rows="1000" TextMode="MultiLine" ReadOnly="True" Font-Size="Medium"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="ButtonShowBibtex" runat="server" Text="Pokaż zapis w Bibtex" OnClick="ButtonShowBibtex_Click" CssClass="button" />
        <br />
        <br />
        <asp:TextBox ID="TextBoxBibtex" runat="server" CssClass="textBoxOnlyRead" Height="162px" Rows="1000" TextMode="MultiLine" ReadOnly="True" Visible="false"></asp:TextBox>
    </asp:Panel>
</asp:Content>

