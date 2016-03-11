using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Expressions;
using ClientWeb.ServiceAgence;

namespace ClientWeb
{
    public partial class Bien : System.Web.UI.Page
    {
        protected BienImmobilier bien;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString.Get("id") == null || Request.QueryString.Get("id") == "")
                Response.Redirect("Client.aspx", true);

            int id = int.Parse(Request.QueryString.Get("id"));

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
                    TitreContient = null,
                };
                var resultat = client.LireListeBiensImmobiliers(criteres, null, null);

                BienImmobilierBase bienBase = resultat.Liste.List.Find(b => b.Id == id);

                if(bienBase == null)
                    Response.Redirect("Client.aspx", true);

                
                bien = client.LireDetailsBienImmobilier(bienBase.Id.ToString()).Bien;

                Page.DataBind();
            }
        }
    }
}