<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="ClientWeb.Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content">
        <div id="btnEdit">
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/CreateImmobilier.aspx" CssClass="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent">
                Create
            </asp:HyperLink>
            <button class=" mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent">
                Delete
            </button>
        </div>
        <asp:Label ID="lblErreurs" runat="server" Text=""></asp:Label>

        <table class="mdl-data-table mdl-js-data-table mdl-data-table--selectable mdl-shadow--2dp">
            <thead>
                <tr>
                    <th class="mdl-data-table__cell--non-numeric">Titre</th>
                    <th>TypeBien</th>
                    <th>Prix</th>
                    <th>Image</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rpResultats" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td class="mdl-data-table__cell--non-numeric"><%# Eval("Titre")%> :</td>
                            <td><%# Eval("TypeBien")%></td>
                            <td><%# Eval("Prix")%></td>
                            <td>
                                <img height="200" width="250" src="<%#(String)Eval("PhotoPrincipaleBase64") != "" ? "data:img/png;base64," + Eval("PhotoPrincipaleBase64") : "./res/noImage.jpg"%>" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <SeparatorTemplate>
                    </SeparatorTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
</asp:Content>
