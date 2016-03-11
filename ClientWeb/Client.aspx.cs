using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClientWeb.ServiceAgence;

namespace ClientWeb
{
    public partial class Client : Page
    {
        private const int NbBiens = 12;

        private const int MaxValue = 1000;

        protected long PrixMax = MaxValue;
        protected long PrixMin = 0;
        protected long NbPieceMax = MaxValue;
        protected long NbPieceMin = 0;
        protected int ChargesMax = MaxValue;
        protected int ChargesMin = 0;
        protected int NbEtagesMax = MaxValue;
        protected int NbEtagesMin = 0;
        protected int SurfaceMax = MaxValue;
        protected int SurfaceMin = 0;


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
            if (Page.IsPostBack) return;
            BindDataIntoRepeater();
        }

        private ListeBiensImmobiliers GetListFromDb()
        {
            using (var client = new AgenceClient())
            {
                var criteres = new CriteresRechercheBiensImmobiliers
                {
                    DateMiseEnTransaction1 = null,
                    DateMiseEnTransaction2 = null,
                    DateTransaction1 = null,
                    DateTransaction2 = null,
                    EnergieChauffage = (Request.QueryString.Get("energie") == "1" ? BienImmobilierBase.eEnergieChauffage.Aucun
                            : Request.QueryString.Get("energie") == "2" ? BienImmobilierBase.eEnergieChauffage.Fioul
                            : Request.QueryString.Get("energie") == "3" ? BienImmobilierBase.eEnergieChauffage.Gaz
                            : Request.QueryString.Get("energie") == "4" ? BienImmobilierBase.eEnergieChauffage.Electrique
                            : Request.QueryString.Get("energie") == "5" ? BienImmobilierBase.eEnergieChauffage.Bois
                            : (object)DBNull.Value) as BienImmobilierBase.eEnergieChauffage?,
                    MontantCharges1 = Request.QueryString["charges-min"] != null && Request.QueryString["charges-min"] != "" ? Convert.ToInt32(float.Parse(Request.QueryString["charges-min"])) : -1,
                    MontantCharges2 = Request.QueryString["charges-max"] != null && Request.QueryString["charges-max"] != "" ? Convert.ToInt32(float.Parse(Request.QueryString["charges-max"])) : -1,
                    NbEtages1 = Request.QueryString["etages-min"] != null && Request.QueryString["etages-min"] != "" ? Convert.ToInt32(float.Parse(Request.QueryString["etages-min"])) : -1,
                    NbEtages2 = Request.QueryString["etages-max"] != null && Request.QueryString["etages-max"] != "" ? Convert.ToInt32(float.Parse(Request.QueryString["etages-max"])) : -1,
                    NbPieces1 = Request.QueryString["piece-min"] != null && Request.QueryString["piece-min"] != "" ? Convert.ToInt32(float.Parse(Request.QueryString["piece-min"])) : -1,
                    NbPieces2 = Request.QueryString["piece-max"] != null && Request.QueryString["piece-max"] != "" ? Convert.ToInt32(float.Parse(Request.QueryString["piece-max"])) : -1,
                    NumEtage1 = -1,
                    NumEtage2 = -1,
                    Prix1 = Request.QueryString["prix-min"] != null && Request.QueryString["prix-min"] != "" ? double.Parse(Request.QueryString["prix-min"]) : -1,
                    Prix2 = Request.QueryString["prix-max"] != null && Request.QueryString["prix-max"] != "" ? double.Parse(Request.QueryString["prix-max"]) : -1,
                    Surface1 = Request.QueryString["superficie-min"] != null && Request.QueryString["superficie-min"] != "" ? Convert.ToInt32(float.Parse(Request.QueryString["superficie-min"])) : -1,
                    Surface2 = Request.QueryString["superficie-max"] != null && Request.QueryString["superficie-max"] != "" ? Convert.ToInt32(float.Parse(Request.QueryString["superficie-max"])) : -1,
                    TransactionEffectuee = false,
                    TypeBien = (Request.QueryString.Get("bien") == "1" ? BienImmobilierBase.eTypeBien.Appartement
                            : Request.QueryString.Get("bien") == "2" ? BienImmobilierBase.eTypeBien.Maison
                            : Request.QueryString.Get("bien") == "3" ? BienImmobilierBase.eTypeBien.Garage
                            : Request.QueryString.Get("bien") == "4" ? BienImmobilierBase.eTypeBien.Terrain
                                : (object)DBNull.Value) as BienImmobilierBase.eTypeBien?,
                    TypeChauffage = (Request.QueryString.Get("chauffage") == "1" ? BienImmobilierBase.eTypeChauffage.Aucun
                            : Request.QueryString.Get("chauffage") == "2" ? BienImmobilierBase.eTypeChauffage.Individuel
                            : Request.QueryString.Get("chauffage") == "3" ? BienImmobilierBase.eTypeChauffage.Collectif
                                : (object)DBNull.Value) as BienImmobilierBase.eTypeChauffage?,
                    TypeTransaction = 
                        (Request.QueryString.Get("transaction") == "1" ? BienImmobilierBase.eTypeTransaction.Vente
                            : Request.QueryString.Get("transaction") == "2" ? BienImmobilierBase.eTypeTransaction.Location
                                : (object) DBNull.Value) as BienImmobilierBase.eTypeTransaction?,
                    TitreContient = Request.QueryString.Get("name"),
                };
                var resultat = client.LireListeBiensImmobiliers(criteres, null, null);
                
                return resultat.SuccesExecution ? resultat.Liste : new ListeBiensImmobiliers();
            }
        }

        private void BindDataIntoRepeater()
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
            rpResultats.DataSource = _pgsource;
            rpResultats.DataBind();

            if (liste.List != null && liste.List.Count != 0)
            {
                PrixMin = ((long) Math.Truncate(liste.List.Min(b => b.Prix)));
                PrixMax = ((long) Math.Truncate(liste.List.Max(b => b.Prix))) + 1;
                var client = new AgenceClient();
                NbPieceMin = ((long) liste.List.Min(b => client.LireDetailsBienImmobilier(b.Id.ToString()).Bien.NbPieces));
                NbPieceMax = ((long) liste.List.Max(b => client.LireDetailsBienImmobilier(b.Id.ToString()).Bien.NbPieces));
                ChargesMin = ((int) liste.List.Min(b => client.LireDetailsBienImmobilier(b.Id.ToString()).Bien.MontantCharges));
                ChargesMax = ((int) liste.List.Max(b => client.LireDetailsBienImmobilier(b.Id.ToString()).Bien.MontantCharges));
                NbEtagesMin = ((int) liste.List.Min(b => client.LireDetailsBienImmobilier(b.Id.ToString()).Bien.NbEtages));
                NbEtagesMax = ((int) liste.List.Max(b => client.LireDetailsBienImmobilier(b.Id.ToString()).Bien.NbEtages));
                SurfaceMin = ((int) liste.List.Min(b => client.LireDetailsBienImmobilier(b.Id.ToString()).Bien.Surface));
                SurfaceMax = ((int) liste.List.Max(b => client.LireDetailsBienImmobilier(b.Id.ToString()).Bien.Surface));
            }

            Page.DataBind();

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
            BindDataIntoRepeater();
        }
        protected void lbLast_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            BindDataIntoRepeater();
        }
        protected void lbPrevious_Click(object sender, EventArgs e)
        {
            CurrentPage -= 1;
            BindDataIntoRepeater();
        }
        protected void lbNext_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            BindDataIntoRepeater();
        }

        protected void rptPaging_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            BindDataIntoRepeater();
        }

        protected void rptPaging_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var lnkPage = (Button)e.Item.FindControl("lbPaging");
            if (lnkPage.CommandArgument != CurrentPage.ToString()) return;
            //lnkPage.Enabled = false;
            lnkPage.CssClass = "mdl-button mdl-js-button mdl-button--raised mdl-button--accent";
            //lnkPage.BackColor = Color.FromName("#00FF00");
        }
    }
}