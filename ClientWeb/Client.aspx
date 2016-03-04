<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="Client.aspx.cs" Inherits="ClientWeb.Client" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="js/mdl-select.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolderSearch">
    <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
        <input class="mdl-textfield__input" type="text" id="name" name="name">
        <label class="mdl-textfield__label" for="name">Nom</label>
    </div>
    <label>Type de transactions</label>
    <label class="mdl-radio mdl-js-radio mdl-js-ripple-effect" for="achat">
      <input type="radio" id="achat" class="mdl-radio__button" name="transaction" value="1">
      <span class="mdl-radio__label">Achat</span>
    </label>
    <label class="mdl-radio mdl-js-radio mdl-js-ripple-effect" for="location">
        <input type="radio" id="location" class="mdl-radio__button" name="transaction" value="2">
        <span class="mdl-radio__label">Location</span>
    </label>
    <label>Type de biens</label>
    <label class="mdl-radio mdl-js-radio mdl-js-ripple-effect" for="appartement">
      <input type="radio" id="appartement" class="mdl-radio__button" name="bien" value="1">
      <span class="mdl-radio__label">Appartement</span>
    </label>
    <label class="mdl-radio mdl-js-radio mdl-js-ripple-effect" for="maison">
        <input type="radio" id="maison" class="mdl-radio__button" name="bien" value="2">
        <span class="mdl-radio__label">Maison</span>
    </label>
    <label class="mdl-radio mdl-js-radio mdl-js-ripple-effect" for="garage">
        <input type="radio" id="garage" class="mdl-radio__button" name="bien" value="3">
        <span class="mdl-radio__label">Garage</span>
    </label>
    <label class="mdl-radio mdl-js-radio mdl-js-ripple-effect" for="terrain">
        <input type="radio" id="terrain" class="mdl-radio__button" name="bien" value="4">
        <span class="mdl-radio__label">Terrain</span>
    </label>
    <label>Type de chauffage</label>
    <label class="mdl-radio mdl-js-radio mdl-js-ripple-effect" for="aucun">
      <input type="radio" id="aucun" class="mdl-radio__button" name="chauffage" value="1">
      <span class="mdl-radio__label">Aucun</span>
    </label>
    <label class="mdl-radio mdl-js-radio mdl-js-ripple-effect" for="individuel">
        <input type="radio" id="individuel" class="mdl-radio__button" name="chauffage" value="2">
        <span class="mdl-radio__label">Individuel</span>
    </label>
    <label class="mdl-radio mdl-js-radio mdl-js-ripple-effect" for="collectif">
        <input type="radio" id="collectif" class="mdl-radio__button" name="chauffage" value="3">
        <span class="mdl-radio__label">Collectif</span>
    </label>
    
    <button class="mdl-button mdl-js-button mdl-button--fab mdl-button--mini-fab mdl-button--colored  mdl-js-ripple-effect" id="search-button">
      <i class="material-icons">search</i>
    </button>
    <script type="text/javascript">
        $('#search-button').onclick = function() {
            $('#search').onsubmit();
        }
        
    </script>
    
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
                            <a class="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect">Voir l'offre
                            </a>
                            <div class="mdl-layout-spacer"></div>
                            <div class="mdl-color-text--accent price">
                                <%# Eval("Prix") %>€
                            </div>

                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="grid-container pagination">
            <asp:Button runat="server" ID="FirstPage" Text="<< First" OnClick="lbFirst_Click" CssClass="mdl-button mdl-js-button mdl-js-ripple-effect mdl-color-text--primary" />
            <asp:Button runat="server" ID="PrevPage" Text="< Prev" OnClick="lbPrevious_Click" CssClass="mdl-button mdl-js-button mdl-js-ripple-effect mdl-color-text--primary" />

            <asp:Repeater ID="rptPaging" OnItemCommand="rptPaging_ItemCommand" OnItemDataBound="rptPaging_ItemDataBound" runat="server">
                <ItemTemplate>
                    <asp:Button ID="lbPaging" CssClass="mdl-button mdl-js-button mdl-js-ripple-effect mdl-button--primary" runat="server" CommandArgument='<%# Eval("PageIndex") %>' CommandName="newPage" Text='<%# Eval("PageText") %> ' Width="20px" />
                </ItemTemplate>
            </asp:Repeater>

            <asp:Button runat="server" ID="NextPage" Text="Next >" OnClick="lbNext_Click" CssClass="mdl-button mdl-js-button mdl-js-ripple-effect mdl-color-text--primary" />
            <asp:Button runat="server" ID="LastPage" Text="Last >>" OnClick="lbLast_Click" CssClass="mdl-button mdl-js-button mdl-js-ripple-effect mdl-color-text--primary" />
        </div>
    </div>

</asp:Content>
