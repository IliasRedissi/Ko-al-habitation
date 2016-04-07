using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClientWeb.ServiceAgence;

namespace ClientWeb
{

    public partial class Admin : System.Web.UI.Page
    {
        private const int NbBiens = 8;

        readonly PagedDataSource _pgsource = new PagedDataSource();

        int _firstIndex, _lastIndex;

        private int CurrentPage
        {
            get
            {
                if (ViewState["CurrentPage"] == null)
                {
                    return 0;
                }
                return ((int)ViewState["CurrentPage"]);
            }
            set
            {
                ViewState["CurrentPage"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            charger();
        }

        private void charger()
        {
                BindDataIntoGridView();
        }

        private ListeBiensImmobiliers GetListFromDb()
        {
            using (var client = new ServiceAgence.AgenceClient())
            {
                var criteres = new ServiceAgence.CriteresRechercheBiensImmobiliers
                {
                    DateMiseEnTransaction1 = null,
                    DateMiseEnTransaction2 = null,
                    DateTransaction1 = null,
                    DateTransaction2 = null,
                    EnergieChauffage = null,
                    MontantCharges1 = -1,
                    MontantCharges2 = -1,
                    NbEtages1 = -1,
                    NbEtages2 = -1,
                    NbPieces1 = -1,
                    NbPieces2 = -1,
                    NumEtage1 = -1,
                    NumEtage2 = -1,
                    Prix1 = -1,
                    Prix2 = -1,
                    Surface1 = -1,
                    Surface2 = -1,
                    TransactionEffectuee = null,
                    TypeBien = null,
                    TypeChauffage = null,
                    TypeTransaction = null,
                };
                var resultat = client.LireListeBiensImmobiliers(criteres, null, null);

                return resultat.SuccesExecution ? resultat.Liste : new ListeBiensImmobiliers();
            }
        }

        private void BindDataIntoGridView()
        {
            var liste = GetListFromDb();
            _pgsource.DataSource = liste.List;
            _pgsource.AllowPaging = true;
            // Number of items to be displayed in the Repeater
            _pgsource.PageSize = NbBiens;
            _pgsource.CurrentPageIndex = CurrentPage;
            // Keep the Total pages in View State
            ViewState["TotalPages"] = _pgsource.PageCount;
            // Enable First, Last, Previous, Next buttons
            PrevPage.Enabled = !_pgsource.IsFirstPage;
            NextPage.Enabled = !_pgsource.IsLastPage;
            FirstPage.Enabled = !_pgsource.IsFirstPage;
            LastPage.Enabled = !_pgsource.IsLastPage;

            // Bind data into repeater
            gvResultats.DataSource = _pgsource;
            gvResultats.DataBind();

            // Call the function to do paging
            HandlePaging();
        }

        private void HandlePaging()
        {
            var dt = new DataTable();
            dt.Columns.Add("PageIndex"); //Start from 0
            dt.Columns.Add("PageText"); //Start from 1

            _firstIndex = CurrentPage - 5;
            if (CurrentPage > 6)
                _lastIndex = CurrentPage + 5;
            else
                _lastIndex = 10;

            // Check last page is greater than total page then reduced it 
            // to total no. of page is last index
            if (_lastIndex > Convert.ToInt32(ViewState["TotalPages"]))
            {
                _lastIndex = Convert.ToInt32(ViewState["TotalPages"]);
                _firstIndex = _lastIndex - 10;
            }

            if (_firstIndex < 0)
                _firstIndex = 0;

            // Now creating page number based on above first and last page index
            for (var i = _firstIndex; i < _lastIndex; i++)
            {
                var dr = dt.NewRow();
                dr[0] = i;
                dr[1] = i + 1;
                dt.Rows.Add(dr);
            }

            rptPaging.DataSource = dt;
            rptPaging.DataBind();
        }

        protected void lbFirst_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            BindDataIntoGridView();
        }
        protected void lbLast_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            BindDataIntoGridView();
        }
        protected void lbPrevious_Click(object sender, EventArgs e)
        {
            CurrentPage -= 1;
            BindDataIntoGridView();
        }
        protected void lbNext_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            BindDataIntoGridView();
        }

        protected void rptPaging_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            BindDataIntoGridView();
        }

        protected void rptPaging_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var lnkPage = (Button)e.Item.FindControl("lbPaging");
            if (lnkPage.CommandArgument != CurrentPage.ToString()) return;
            //lnkPage.Enabled = false;
            lnkPage.CssClass = "mdl-button mdl-js-button mdl-button--raised mdl-button--accent";
            //lnkPage.BackColor = Color.FromName("#00FF00");
        }

        private void SupprimerBien(string id)
        {
            using (ServiceAgence.AgenceClient client = new ServiceAgence.AgenceClient())
            {
                ServiceAgence.ResultatBool r = client.SupprimerBienImmobilier(id);
                if (!r.SuccesExecution)
                {
                    lblErreurs.Text = convertErrorToString(r.ErreursBloquantes);
                }
            }
        }

        private string convertErrorToString(List<ServiceAgence.ResultatOperation.Erreur> liste)
        {
            string resultat = "";
            foreach (ServiceAgence.ResultatOperation.Erreur err in liste)
            {
                if (resultat != "") resultat += "<br/>";
                resultat += err.Message;
            }
            return resultat;
        }

        protected void btDelete_OnClick(object sender, EventArgs e)
        {
            // throw new NotImplementedException();

            //var niah = $("#table");
            //var id = 
            foreach (GridViewRow row in gvResultats.Rows)
            {
                CheckBox chb = (CheckBox)row.Cells[0].Controls[1];
                if (chb.Checked)
                {
                    String id = chb.Attributes["idBien"];
                    SupprimerBien(id);
                    CurrentPage = 0;
                    charger();
                }
            }
        }

        protected void bt_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/EditImmobilier.aspx?id=" + ((Button)sender).Attributes["idBien"].ToString());
        }

    }
}