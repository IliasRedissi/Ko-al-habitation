<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="CreateImmobilier.aspx.cs" Inherits="ClientWeb.CreateImmobilier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="./css/StyleCreateimmobilier.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="content_create" class="block">
        <div id="content_create_in" class="margin_auto">

            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label width_inherit">
                <asp:TextBox ID="txtTitle" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                <label class="mdl-textfield__label mdl-color-text--accent" for="txtTitle">Nom de l'immobilier</label>
            </div>
            <br />
            Type de mobilier : 
        <asp:DropDownList ID="listBien" runat="server" CssClass="mdl-color--accent"/>
            <br />
            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label width_inherit">
                <asp:TextBox ID="txtPrice" runat="server" CssClass="mdl-textfield__input" TextMode="SingleLine" pattern="-?[0-9]*(\.[0-9]+)?"></asp:TextBox>
                <label class="mdl-textfield__label mdl-color-text--accent" for="txtPrice">Prix</label>
                <span class="mdl-textfield__error">Ce n'est pas un entier!</span>
            </div>
            <p>Image :</p>
            <div class="boutton">
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Admin.aspx" CssClass="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent">
        Annuler
                </asp:HyperLink>
                <asp:Button OnClick="AddImmobilier_Click" ID="AddImmobilier" runat="server" Text="Ajouter" CssClass="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent" />
            </div>
            <br />
            <br />
        </div>
    </div>
</asp:Content>
