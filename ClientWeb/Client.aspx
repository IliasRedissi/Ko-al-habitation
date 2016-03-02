<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="Client.aspx.cs" Inherits="ClientWeb.Client" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="js/mdl-select.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolderSearch">
    <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
        <input class="mdl-textfield__input" type="text" id="name" name="name">
        <label class="mdl-textfield__label" for="name">Nom</label>
    </div>
    <div id="insert-here"></div>

<script>
    var onSelect = function () {
        this.button.innerHTML = this.innerHTML;
    }

    var insertPoint = 'insert-here';
    var numberOfDropdowns = 0;
    function makeDropdown(name, options){
        // create the button
        var button = document.createElement('LABEL');
        button.id = numberOfDropdowns; // this is how Material Design associates option/button
        button.setAttribute('class', 'dropdown');
        button.innerHTML = name;
        document.getElementById(insertPoint).appendChild(button);

        // add the options to the button (unordered list)
        var ul = document.createElement('UL');
        ul.setAttribute('class', 'mdl-menu mdl-js-menu');
        ul.setAttribute('for', numberOfDropdowns); // associate button
        for(var index in options) {
            // add each item to the list
            var li = document.createElement('LI');
            li.setAttribute('class', 'mdl-menu__item');
            li.innerHTML = options[index];
            li.button = button;
            li.onclick = onSelect;
            ul.appendChild(li);
        }
        document.getElementById(insertPoint).appendChild(ul);
        // and finally add the list to the HTML
        numberOfDropdowns++;
    }

    var optionsA = ["Achat", "Location"];
    makeDropdown("Type de transactions", optionsA);
    var optionsB = ["Appartement", "Maison", "Garage", "Terrain"];
    makeDropdown("Type de biens", optionsB);
    var optionsC = ["Aucun", "Individuel", "Collectif"];
    makeDropdown("Type de chauffage", optionsC);
    var optionsD = ["Aucun", "Fioul", "Gaz", "Electrique", "Bois"];
    makeDropdown("Energie de chaufage", optionsD);
</script>
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
