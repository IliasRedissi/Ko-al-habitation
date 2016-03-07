<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="ClientWeb.Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script>
        $(document).ready(function () {
            // Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function () {
            $("[id*=chkSelectionnerTout]").bind("click", function () {
                var checked = $(this).is(":checked");
                $("[id*=gvResultats] input[type=checkbox]").not("#chkSelectionnerTout").prop("checked", checked);
            });
            $("[id*=gvResultats] input[type=checkbox]").not("#chkSelectionnerTout").forEach(bind("click", function () {
                if (!$(this).is(":checked")) {
                    $("#chkSelectionnerTout").prop("checked", $(this).is(":checked"));
                }
            }));
            // });
        });

    </script>

    <div id="content">
        <div id="btnEdit">
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/CreateImmobilier.aspx" CssClass="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent">
                Create
            </asp:HyperLink>
            <asp:Button OnClick="btDelete_OnClick" ID="btDelete" runat="server" Text="Delete" CssClass="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent" />
        </div>
        <asp:Label ID="lblErreurs" runat="server" Text=""></asp:Label>

        <asp:GridView ID="gvResultats" runat="server" AutoGenerateColumns="False" CssClass="mdl-data-table mdl-js-data-table mdl-shadow--2dp">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkSelectionnerTout" runat="server" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox runat="server" idBien='<%#Eval("Id")%>'/>
                        <%--Eval("Id")--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Titre" ReadOnly="True"
                    SortExpression="Titre" HeaderText="Titre" />
                <asp:BoundField DataField="TypeBien" ReadOnly="True"
                    SortExpression="TypeBien" HeaderText="Type de bien" />
                <asp:BoundField DataField="Prix" ReadOnly="True"
                    SortExpression="Prix" HeaderText="Prix" />
                <asp:TemplateField>
                    <HeaderTemplate>Image</HeaderTemplate>
                    <ItemTemplate>
                        <img height="200" width="250" src="<%#(String) Eval("PhotoPrincipaleBase64") != "" ? "data:img/png;base64," + Eval("PhotoPrincipaleBase64") : "./res/noImage.jpg" %>" alt="" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>Modifier</HeaderTemplate>
                    <ItemTemplate>
                        <asp:Button  ID="bt" runat="server" OnClick="bt_OnClick" Text="Modifier" idBien='<%#Eval("Id")%>' CssClass="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent" />
                        <%--Eval("Id")--%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
