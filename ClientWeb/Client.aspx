<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="Client.aspx.cs" Inherits="ClientWeb.Client" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> 
</asp:Content> 
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="grid-container">
            <asp:Repeater ID="rpResultats" runat="server"> 
                <ItemTemplate> 
                    <div class="card-square mdl-card mdl-shadow--2dp"> 
                      <div class="mdl-card__title mdl-card--expand" style="background: url('<%#(string)Eval("PhotoPrincipaleBase64") != "" && Eval("PhotoPrincipaleBase64") != null ? "data:img/png;base64," + Eval("PhotoPrincipaleBase64") : "./res/noImage.jpg"%>'); -ms-background-size: cover; background-size: cover; color: #fff;"> 
                    
                      </div>
                      <div class="mdl-card__title">
                        <asp:Label runat="server" ID="Label1" Text='<%# Eval("Titre") %>' />
                      </div>
                      <div class="mdl-card__actions mdl-card--border">
                        <a class="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect">
                          Voir l'offre
                        </a>
                      </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="grid-container pagination">
            <asp:Button runat="server" ID="FirstPage" Text="<< First" CssClass="mdl-button mdl-js-button mdl-js-ripple-effect mdl-color-text--primary" />
            <asp:Button runat="server" ID="PrevPage" Text="< Prev" CssClass="mdl-button mdl-js-button mdl-js-ripple-effect mdl-color-text--primary" />
    
            <asp:Repeater ID="rptPaging" OnItemCommand="rptPaging_ItemCommand" OnItemDataBound="rptPaging_ItemDataBound" runat="server">
                <ItemTemplate>
                    <asp:Button ID="lbPaging" CssClass="mdl-button mdl-js-button mdl-js-ripple-effect mdl-button--primary" runat="server" CommandArgument='<%# Eval("PageIndex") %>' CommandName="newPage" Text='<%# Eval("PageText") %> ' Width="20px"/>
                </ItemTemplate>
            </asp:Repeater>

            <asp:Button runat="server" ID="NextPage" Text="Next >" CssClass="mdl-button mdl-js-button mdl-js-ripple-effect mdl-color-text--primary" />
            <asp:Button runat="server" ID="LastPage" Text="Last >>" CssClass="mdl-button mdl-js-button mdl-js-ripple-effect mdl-color-text--primary" />
        </div>
    </div>
    
</asp:Content> 