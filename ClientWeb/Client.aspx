<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="Client.aspx.cs" Inherits="ClientWeb.Client" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> 
</asp:Content> 
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="grid-container">
        <asp:Repeater ID="rpResultats" runat="server"> 
            <ItemTemplate> 
                <div class="card-square mdl-card mdl-shadow--2dp"> 
                  <div class="mdl-card__title mdl-card--expand" style="background: url('<%#(String)Eval("PhotoPrincipaleBase64") != "" && Eval("PhotoPrincipaleBase64") != null ? "data:img/png;base64," + Eval("PhotoPrincipaleBase64") : "./res/noImage.jpg"%>'); background-size: cover; color: #fff;"> 
                    
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
</asp:Content> 