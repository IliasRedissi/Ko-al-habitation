<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="ClientWeb.Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content">
        <div class="margin_top_left to_the_right">
            <button class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent">
                Create
            </button>
            <button class=" mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent">
                Delete
            </button>
        </div>
        <asp:Label ID="lblErreurs" runat="server" Text=""></asp:Label>
        <asp:GridView ID="gvResultats" runat="server" AutoGenerateColumns="false" CssClass="mdl-data-table mdl-js-data-table mdl-data-table--selectable mdl-shadow--2dp">
            <Columns>
                <asp:BoundField DataField="Titre" ReadOnly="True"
                    SortExpression="Titre" HeaderText="Titre">
                    <ItemStyle CssClass="mdl-data-table__cell--non-numeric" />
                </asp:BoundField>
                <asp:BoundField DataField="TypeBien" ReadOnly="True"
                    SortExpression="TypeBien" HeaderText="TypeBien">
                    <ItemStyle CssClass="mdl-data-table__cell--non-numeric" />
                </asp:BoundField>
                <asp:BoundField DataField="Prix" ReadOnly="True"
                    SortExpression="Prix" HeaderText="Prix" />
                <asp:TemplateField>
                    <HeaderTemplate>Image</HeaderTemplate>
                    <ItemTemplate>
                        <img height="200" width="250" src="<%#(String)Eval("PhotoPrincipaleBase64") != "" ? "data:img/png;base64," + Eval("PhotoPrincipaleBase64") : "./res/noImage.jpg"%>" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
