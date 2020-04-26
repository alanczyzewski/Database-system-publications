<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Authors.aspx.cs" Inherits="Authors" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="PanelHeader" runat="server" HorizontalAlign ="Center">
        <asp:Label ID = "LabelHeader" runat = "server"  CssClass="header" Text = "Autorzy" ></asp:Label>
        <br />
        <br />
    </asp:Panel>
    <asp:Panel ID="PanelDisplayMethod" runat="server" HorizontalAlign="Center">
            <br />
            <asp:Label ID="LabelNumberResults" runat="server" Text="Liczba wyników na stronę"></asp:Label>
            &ensp;
            <asp:DropDownList ID="DDListNumberResults" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDListNumberResults_SelectedIndexChanged" CssClass="dropDownList">
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem Selected="True">25</asp:ListItem>
                <asp:ListItem>50</asp:ListItem>
                <asp:ListItem>100</asp:ListItem>
                <asp:ListItem>200</asp:ListItem>
            </asp:DropDownList>
                
            <br />
            <br />
    </asp:Panel>
    <asp:Panel ID="PanelMain" runat="server">
        <asp:GridView ID="GridViewAuthors" Width="1200px" runat="server" AllowPaging="True" AutoGenerateSelectButton="True" BackColor="#CCFFFF" 
            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="Id" 
            OnPageIndexChanging="GridViewAuthors_PageIndexChanging" OnSelectedIndexChanged="GridViewAuthors_SelectedIndexChanged" PageSize="25">
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
    <asp:Panel ID="PanelEdit" runat="server" HorizontalAlign="Center" Visible="False">
        <asp:Button ID="ButtonEdit" Width="140px" runat="server" Text="Edytuj" CssClass="button" OnClick="ButtonEdit_Click" />
        <br />
        <br />
    </asp:Panel>
    <asp:Panel ID="PanelChangeName" runat="server" HorizontalAlign="Center" Visible="false">
        <asp:Label ID="LabelName" runat="server" Text="Imię i nazwisko autora:"></asp:Label>
            &emsp;
            <asp:TextBox ID="TextBoxName" CssClass="textBoxInput" runat="server" Width="500px"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="ButtonSave" runat="server" CssClass="button" OnClick="ButtonSave_Click" Text="Zapisz" Width="140px" />
            &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;
            <asp:Button ID="ButtonCancel" runat="server" Text="Cofnij" CssClass="button" Width="140px"/>
            <br />
            <br />
    </asp:Panel>

    <asp:Panel ID="PanelPublications" runat="server" Visible="false" >
        <asp:TextBox ID="TextBoxPublications" CssClass="textBoxOnlyRead" runat="server" Height="162px" Rows="1000" TextMode="MultiLine" ReadOnly="True" Font-Size="Medium"></asp:TextBox>
    </asp:Panel>
</asp:Content>

