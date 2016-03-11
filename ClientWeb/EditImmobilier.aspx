
<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="EditImmobilier.aspx.cs" Inherits="ClientWeb.EditImmobilier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="./css/StyleCreateimmobilier.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        $(document).ready(function () {
            $("#moreContent").click(function () {
                if ($("#checkContent:hidden").length) {
                    $(".contentSupp").show("fast");
                    $("#moreContent").text("Moins de détails");
                } else {
                    $(".contentSupp").hide("fast");
                    $("#moreContent").text("Plus de détails");
                }
            });
        });
    </script>

    <div id="content_create" class="block">
        <h4 class="mdl-color-text--accent">Modifiaction d'un immobilier</h4>
        <asp:Label ID="lblErreurs" runat="server" Text=""></asp:Label>
        <asp:Image ID="ImageResult" runat="server" CssClass="imageUpload" />
        <div class="content_create_in">
            <h6 class="mdl-color-text--accent">image:</h6>
            <asp:FileUpload ID="FileUpload1" runat="server" accept="image/*" />
            <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="Upload" />
            <asp:Label ID="lblUpload" runat="server" Text="" CssClass="mdl-color-text--accent"></asp:Label>
        </div>
        <hr />
        <div class="content_create_in">
            <ul class="demo-list-icon mdl-list list_no_padding">
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <i class="material-icons mdl-list__item-icon">assignment</i>
                        <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label width_inherit">
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                            <label class="mdl-textfield__label mdl-color-text--accent" for="txtTitle">Nom de l'immobilier</label>
                        </div>
                    </span>
                </li>
                <li>
                    <span class="mdl-list__item-primary-content span_spe">
                        <i class="material-icons mdl-list__item-icon">home</i>
                        <span class="drop_down_list2">
                            <asp:DropDownList ID="listBien" runat="server" />
                            <asp:DropDownList ID="listTypeLocation" runat="server" />
                        </span>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <i class="material-icons mdl-list__item-icon">euro_symbol</i>
                        <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label width_inherit">
                            <asp:TextBox ID="txtPrice" runat="server" CssClass="mdl-textfield__input" TextMode="SingleLine" pattern="[0-9]*(\.[0-9]+)?"></asp:TextBox>
                            <label class="mdl-textfield__label mdl-color-text--accent" for="txtPrice">Prix</label>
                            <span class="mdl-textfield__error">Ce n'est pas un nombre!</span>
                        </div>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <i class="material-icons mdl-list__item-icon">euro_symbol</i>
                        <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label width_inherit">
                            <asp:TextBox ID="txtMontantCharge" runat="server" CssClass="mdl-textfield__input" TextMode="SingleLine" pattern="[0-9]*(\.[0-9]+)?"></asp:TextBox>
                            <label class="mdl-textfield__label mdl-color-text--accent" for="txtMontantCharge">Montant des charges</label>
                            <span class="mdl-textfield__error">Ce n'est pas un nombre!</span>
                        </div>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <i class="material-icons mdl-list__item-icon">location_city</i>
                        <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label width_inherit">
                            <asp:TextBox ID="txtCodePostal" runat="server" CssClass="mdl-textfield__input" TextMode="SingleLine" pattern="[0-9]*"></asp:TextBox>
                            <label class="mdl-textfield__label mdl-color-text--accent" for="txtCodePostal">Code Postal</label>
                            <span class="mdl-textfield__error">Ce n'est pas un entier!</span>
                        </div>
                    </span>
                </li>
                <li class="mdl-list__item">
                    <span class="mdl-list__item-primary-content">
                        <i class="material-icons mdl-list__item-icon">location_city</i>
                        <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label width_inherit">
                            <asp:TextBox ID="txtVille" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                            <label class="mdl-textfield__label mdl-color-text--accent" for="txtVille">Ville</label>
                        </div>
                    </span>
                </li>
            </ul>

            <button type="button" id="moreContent" class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent">Plus de détails</button>
        </div>
        <div class="contentSupp">
            <hr />
            <div class="content_create_in">
                <h5 id="checkContent" class="mdl-color-text--accent">Contenu Supplementaire</h5>
                <ul class="demo-list-icon mdl-list list_no_padding">
                    <li class="mdl-list__item">
                        <span class="mdl-list__item-primary-content">
                            <i class="material-icons mdl-list__item-icon">description</i>
                            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label width_inherit">
                                <asp:TextBox ID="txtDescription" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                                <label class="mdl-textfield__label mdl-color-text--accent" for="txtDescription">Description</label>
                            </div>
                        </span>
                    </li>
                    <li class="mdl-list__item">
                        <span class="mdl-list__item-primary-content">
                            <i class="material-icons mdl-list__item-icon">location_city</i>
                            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label width_inherit">
                                <asp:TextBox ID="txtAdresse" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                                <label class="mdl-textfield__label mdl-color-text--accent" for="txtAdresse">Adresse</label>
                            </div>
                        </span>
                    </li>
                    <li>
                        <span class="mdl-list__item-primary-content span_spe">
                            <i class="material-icons mdl-list__item-icon">whatshot</i>
                            <span class="drop_down_list2">
                                <asp:DropDownList ID="listChauffage" runat="server" />
                                <asp:DropDownList ID="listEnergie" runat="server" />
                            </span>
                        </span>
                    </li>
                    <li class="mdl-list__item">
                        <span class="mdl-list__item-primary-content">
                            <i class="material-icons mdl-list__item-icon">layers</i>
                            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label width_inherit">
                                <asp:TextBox ID="txtSurface" runat="server" CssClass="mdl-textfield__input" TextMode="SingleLine" pattern="[0-9]*(\.[0-9]+)?"></asp:TextBox>
                                <label class="mdl-textfield__label mdl-color-text--accent" for="txtSurface">Surface</label>
                                <span class="mdl-textfield__error">Ce n'est pas un nombre!</span>
                            </div>
                        </span>
                    </li>
                    <li class="mdl-list__item">
                        <span class="mdl-list__item-primary-content">
                            <i class="material-icons mdl-list__item-icon">format_list_numbered</i>
                            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label width_inherit">
                                <asp:TextBox ID="txtNbrePiece" runat="server" CssClass="mdl-textfield__input" TextMode="SingleLine" pattern="[0-9]*"></asp:TextBox>
                                <label class="mdl-textfield__label mdl-color-text--accent" for="txtNbrePiece">Nombre de pièce</label>
                                <span class="mdl-textfield__error">Ce n'est pas un entier!</span>
                            </div>
                        </span>
                    </li>
                    <li class="mdl-list__item">
                        <span class="mdl-list__item-primary-content">
                            <i class="material-icons mdl-list__item-icon">format_list_numbered</i>
                            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label width_inherit">
                                <asp:TextBox ID="txtNbrEtage" runat="server" CssClass="mdl-textfield__input" TextMode="SingleLine" pattern="[0-9]*"></asp:TextBox>
                                <label class="mdl-textfield__label mdl-color-text--accent" for="txtNbrEtage">Nombre d'étage</label>
                                <span class="mdl-textfield__error">Ce n'est pas un entier!</span>
                            </div>
                        </span>
                    </li>
                    <li class="mdl-list__item">
                        <span class="mdl-list__item-primary-content">
                            <i class="material-icons mdl-list__item-icon">format_list_numbered</i>
                            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label width_inherit">
                                <asp:TextBox ID="txtNumEtage" runat="server" CssClass="mdl-textfield__input" TextMode="SingleLine" pattern="[0-9]*"></asp:TextBox>
                                <label class="mdl-textfield__label mdl-color-text--accent" for="txtNumEtage">Numero de l'étage</label>
                                <span class="mdl-textfield__error">Ce n'est pas un entier!</span>
                            </div>
                        </span>
                    </li>
                </ul>
            </div>
        </div>

        <div class="boutton">
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Admin.aspx" CssClass="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent">
        Annuler
            </asp:HyperLink>
            <asp:Button OnClick="EditImmobilier_OnClick" ID="AddImmobilier" runat="server" Text="Modifier" CssClass="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent" />
        </div>
        <br />
        <br />
    </div>



</asp:Content>
