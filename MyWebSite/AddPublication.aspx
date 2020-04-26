<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddPublication.aspx.cs" Inherits="AddPublication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="PanelHeader" runat="server" HorizontalAlign ="Center">
        <asp:Label ID = "LabelHeader" runat = "server" CssClass="header" Text = "Dodaj do bazy nową publikację" ></asp:Label>
        <br />
        <br />
    </asp:Panel>
    <asp:Panel ID="PanelMain" runat="server">
        <br />
        <asp:Label ID = "LabelType" runat = "server" Text = "Wybierz typ publikacji" CssClass="header2" ></asp:Label>
        <br />
        <br />
        <asp:ListBox ID="ListBoxType" runat="server" Height="233px" Width="590px" AutoPostBack="True" OnSelectedIndexChanged="ListBoxType_SelectedIndexChanged" CssClass="listBox">
            <asp:ListItem Selected="True" Value="article">Artykuł</asp:ListItem>
            <asp:ListItem Value="inproceedings">Artykuł w materiałach konferencyjnych</asp:ListItem>
            <asp:ListItem Value="booklet">Broszura</asp:ListItem>
            <asp:ListItem Value="inbook">Część książki (rozdział lub zakres stron)</asp:ListItem>
            <asp:ListItem Value="incollection">Część książki z własnym tytułem</asp:ListItem>
            <asp:ListItem Value="manual">Dokumentacja techniczna</asp:ListItem>
            <asp:ListItem Value="unpublished">Dokument nieopublikowany</asp:ListItem>
            <asp:ListItem Value="book">Książka</asp:ListItem>
            <asp:ListItem Value="proceedings">Materiały konferencyjne</asp:ListItem>
            <asp:ListItem Value="mastersthesis">Praca magisterska</asp:ListItem>
            <asp:ListItem Value="techreport">Raport opublikowany przez uczelnię</asp:ListItem>
            <asp:ListItem Value="phdthesis">Rozprawa doktorska</asp:ListItem>
            <asp:ListItem Value="misc">Pozostałe</asp:ListItem>
        </asp:ListBox>
        <asp:TextBox ID="TextBoxTypeInfo" CssClass="textBoxOnlyRead" runat="server" Height="229px" Width="590px" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID = "LabelFields" runat = "server" Text = "Opisz publikację" CssClass="header2" ></asp:Label>
        <br />
        <br />
    </asp:Panel>
    <asp:Panel ID="PanelFields" HorizontalAlign="Center" runat="server">
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
    <asp:Panel ID="PanelAuthor" runat="server" Visible="False">
        <asp:Label ID="LabelAuthor" runat="server" Text="Autorzy (wymienić po przecinku)"></asp:Label>
        &emsp;
        <asp:TextBox ID="TextBoxAuthor" runat="server" Height="30px" TextMode="MultiLine" Width="316px" CssClass="textBoxInput"></asp:TextBox>
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
    <asp:Panel ID="PanelAdd" runat="server">
        <br />
        <asp:Button ID="ButtonAdd" runat="server" Text="Dodaj" Width="102px" OnClick="ButtonAdd_Click" CssClass="button"></asp:Button>
        <br />
        <br />
    </asp:Panel>
    </asp:Panel>
</asp:Content>

