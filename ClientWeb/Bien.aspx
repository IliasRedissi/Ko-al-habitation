<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="Bien.aspx.cs" Inherits="ClientWeb.Bien" %>
<%@ Import Namespace="System.Globalization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderSearch" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="grid-container">
            <asp:Repeater ID="rpBien" runat="server">
                <ItemTemplate>
                    <style>
                        .bien-card-wide.mdl-card {
                            width: 100%;
                            max-width: 900px;
                        }

                        .bien-card-wide > .mdl-card__title {
                            color: #fff;
                            height: 400px;
                            background: url('<%#(string)Eval("PhotoPrincipaleBase64") != "" && Eval("PhotoPrincipaleBase64") != null ? "data:img/png;base64," + Eval("PhotoPrincipaleBase64") : "./res/noImage.jpg"%>') center / cover;
                        }

                        .bien-card-wide > .mdl-card__menu {
                            color: #fff;
                        }
                    </style>
                    <div class="bien-card-wide mdl-card mdl-shadow--4dp">
                        <div class="mdl-card__title">
                            <h2 class="mdl-card__title-text"><%# Eval("Titre") %></h2>
                        </div>
                        <div class="mdl-card__supporting-text">
                            <%# Eval("Description") %>
                        </div>
                        <ul class="demo-list-icon mdl-list">
                            <li class="mdl-list__item">
                                <span class="mdl-list__item-primary-content">
                                    <i class="material-icons mdl-list__item-icon">date_range</i>
                                    <%#  Eval("DateMiseEnTransaction") %>
                                </span>
                            </li>
                            <li class="mdl-list__item">
                                <span class="mdl-list__item-primary-content">
                                    <i class="material-icons mdl-list__item-icon">attach_money</i>
                                    Prix : <%# ((double) Eval("Prix")).ToString("C", new CultureInfo("fr-FR")) %>
                                </span>
                            </li>
                            <li class="mdl-list__item">
                                <span class="mdl-list__item-primary-content">
                                    <i class="material-icons mdl-list__item-icon">attach_money</i>
                                    Montant des charges : <%# ((double) Eval("MontantCharges")).ToString("C", new CultureInfo("fr-FR")) %>
                                </span>
                            </li>
                            <li class="mdl-list__item">
                                <span class="mdl-list__item-primary-content">
                                    <i class="material-icons mdl-list__item-icon">location_city</i>
                                    Montant des charges : <%# Eval("Ville") + " " + Eval("CodePostal") %>
                                </span>
                            </li>
                            <li class="mdl-list__item">
                                <span class="mdl-list__item-primary-content">
                                    <i class="material-icons mdl-list__item-icon">home</i>
                                    <%# Eval("TypeBien") %>
                                </span>
                            </li>
                            <li class="mdl-list__item">
                                <span class="mdl-list__item-primary-content">
                                    <i class="material-icons mdl-list__item-icon">local_offer</i>
                                    <%# Eval("TypeTransaction") %>
                                </span>
                            </li>
                            <li class="mdl-list__item">
                                <span class="mdl-list__item-primary-content">
                                    <i class="material-icons mdl-list__item-icon"></i>
                                    Nombre de pièces : <%# Eval("NbPieces") %>
                                </span>
                            </li>
                            <li class="mdl-list__item">
                                <span class="mdl-list__item-primary-content">
                                    <i class="material-icons mdl-list__item-icon"></i>
                                    Surface : <%# Eval("Surface") %> m²
                                </span>
                            </li>
                            <li class="mdl-list__item">
                                <span class="mdl-list__item-primary-content">
                                    <i class="material-icons mdl-list__item-icon"></i>
                                    Numéro de l'étage : <%# Eval("NumEtage") %>
                                </span>
                            </li>
                            <li class="mdl-list__item">
                                <span class="mdl-list__item-primary-content">
                                    <i class="material-icons mdl-list__item-icon"></i>
                                    Nombre d'étages : <%# Eval("NbEtages") %>
                                </span>
                            </li>
                            <li class="mdl-list__item">
                                <span class="mdl-list__item-primary-content">
                                    <i class="material-icons mdl-list__item-icon">whatshot</i>
                                    Type de chauffage : <%# Eval("TypeChauffage") %>
                                </span>
                            </li>
                            <li class="mdl-list__item">
                                <span class="mdl-list__item-primary-content">
                                    <i class="material-icons mdl-list__item-icon">whatshot</i>
                                    Energie pour le chauffage : <%# Eval("EnergieChauffage") %>
                                </span>
                            </li>
                        </ul>

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
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
