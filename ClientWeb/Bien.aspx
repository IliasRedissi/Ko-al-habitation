<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="Bien.aspx.cs" Inherits="ClientWeb.Bien" %>

<%@ Import Namespace="System.Globalization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderSearch" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="grid-container">
            <style>
                .bien-card-wide.mdl-card {
                    width: 100%;
                    max-width: 900px;
                }

                .bien-card-wide > .mdl-card__title {
                    color: #fff;
                    height: 400px;
                    background: url('<%#(string)bien.PhotoPrincipaleBase64 != "" && bien.PhotoPrincipaleBase64 != null ? "data:img/png;base64," + bien.PhotoPrincipaleBase64 : "./res/noImage.jpg"%>') center / cover;
                }

                .bien-card-wide > .mdl-card__menu {
                    color: #fff;
                }
            </style>
            <div class="bien-card-wide mdl-card mdl-shadow--4dp">
                <div class="mdl-card__title">
                    <h2 class="mdl-card__title-text"><%# bien.Titre %></h2>
                </div>
                <div class="mdl-card__supporting-text">
                    <%# bien.Description %>
                </div>
                <div class="flex">
                    <ul class="demo-list-icon mdl-list">
                    <li class="mdl-list__item">
                        <span class="mdl-list__item-primary-content">
                            <i class="material-icons mdl-list__item-icon">date_range</i>
                            <%# bien.DateMiseEnTransaction %>
                        </span>
                    </li>
                    <li class="mdl-list__item">
                        <span class="mdl-list__item-primary-content">
                            <i class="material-icons mdl-list__item-icon">attach_money</i>
                            Prix : <%# bien.Prix.ToString("C", new CultureInfo("fr-FR")) %>
                        </span>
                    </li>
                    <li class="mdl-list__item">
                        <span class="mdl-list__item-primary-content">
                            <i class="material-icons mdl-list__item-icon">attach_money</i>
                            Montant des charges : <%# bien.MontantCharges.ToString("C", new CultureInfo("fr-FR")) %>
                        </span>
                    </li>
                    <li class="mdl-list__item">
                        <span class="mdl-list__item-primary-content">
                            <i class="material-icons mdl-list__item-icon">location_city</i>
                            Ville : <%# bien.Ville + " " + bien.CodePostal %>
                        </span>
                    </li>
                    <li class="mdl-list__item">
                        <span class="mdl-list__item-primary-content">
                            <i class="material-icons mdl-list__item-icon">home</i>
                            <%# bien.TypeBien %>
                        </span>
                    </li>
                    <li class="mdl-list__item">
                        <span class="mdl-list__item-primary-content">
                            <i class="material-icons mdl-list__item-icon">local_offer</i>
                            <%# bien.TypeTransaction %>
                        </span>
                    </li>
                    <li class="mdl-list__item">
                        <span class="mdl-list__item-primary-content">
                            <i class="material-icons mdl-list__item-icon">layers</i>
                            Surface : <%# bien.Surface %> m²
                        </span>
                    </li>
                    <li class="mdl-list__item">
                        <span class="mdl-list__item-primary-content">
                            <i class="material-icons mdl-list__item-icon">format_list_numbered</i>
                            Nombre de pièces : <%# bien.NbPieces %>
                        </span>
                    </li>
                    <li class="mdl-list__item">
                        <span class="mdl-list__item-primary-content">
                            <i class="material-icons mdl-list__item-icon">format_list_numbered</i>
                            Numéro de l'étage : <%# bien.NumEtage %>
                        </span>
                    </li>
                    <li class="mdl-list__item">
                        <span class="mdl-list__item-primary-content">
                            <i class="material-icons mdl-list__item-icon">format_list_numbered</i>
                            Nombre d'étages : <%# bien.NbEtages %>
                        </span>
                    </li>
                    <li class="mdl-list__item">
                        <span class="mdl-list__item-primary-content">
                            <i class="material-icons mdl-list__item-icon">whatshot</i>
                            Type de chauffage : <%# bien.TypeChauffage %>
                        </span>
                    </li>
                    <li class="mdl-list__item">
                        <span class="mdl-list__item-primary-content">
                            <i class="material-icons mdl-list__item-icon">whatshot</i>
                            Energie pour le chauffage : <%# bien.EnergieChauffage %>
                        </span>
                    </li>
                </ul>
                    <div class="flex-col">
                        <% foreach (var image in bien.PhotosBase64)
                            { %>
                            <img src="<%= "data:img/png;base64," + image %>" width="300" class=" mdl-shadow--4dp"/>      
                         
                           <% } %>
                    </div>
                </div>
                <div class="mdl-card__actions mdl-card--border">
                    <a class="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect mdl-button--raised mdl-button--accent">Contacter le vendeur
                    </a>
                </div>
                <div class="mdl-card__menu">
                    <button class="mdl-button mdl-button--icon mdl-js-button mdl-js-ripple-effect">
                        <i class="material-icons">share</i>
                    </button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
