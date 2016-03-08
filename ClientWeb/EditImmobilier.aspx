
<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="EditImmobilier.aspx.cs" Inherits="ClientWeb.EditImmobilier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="./css/StyleEditImmobilier.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content_edit" class="block">
        <div id="content_edit_in">
            <asp:Label ID="lblErreurs" runat="server" Text=""></asp:Label>
            <br />
            <h4 class="mdl-color-text--accent">Modification de l'immobilier</h4>
            <asp:Image ID="imgImmobilier" Width="250" Height="150" runat="server" CssClass="imgModif"/>
            <div class="mdl-textfield mdl-js-textfield input">
                <asp:TextBox ID="txtTitle" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                <label class="mdl-textfield__label" for="txtTitle">Name</label>
            </div>
            <br />
            type d'Immobilier:
    <asp:DropDownList ID="listBien" runat="server" CssClass="mdl-color--accent" />
            <br />
            <div class="mdl-textfield mdl-js-textfield input">
                <asp:TextBox ID="txtPrice" runat="server" CssClass="mdl-textfield__input" TextMode="SingleLine" pattern="-?[0-9]*(\.[0-9]+)?"></asp:TextBox>
                <label class="mdl-textfield__label" for="txtPrice">Price</label>
                <span class="mdl-textfield__error">Ce n'est pas un nombre!</span>
            </div>
            <br/>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Admin.aspx" CssClass="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent">
        Annuler
            </asp:HyperLink>
            <asp:Button OnClick="EditImmobilier_OnClick" ID="ModifBien" runat="server" Text="Ajouter" CssClass="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent" />
        </div>
    </div>



</asp:Content>
