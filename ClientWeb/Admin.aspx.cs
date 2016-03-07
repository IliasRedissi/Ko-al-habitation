using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClientWeb.ServiceAgence;

namespace ClientWeb
{

    public partial class Admin : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            charger();
        }

        private void charger()
        {
            List<ServiceAgence.BienImmobilierBase> liste = null;
            using (ServiceAgence.AgenceClient client = new ServiceAgence.AgenceClient())
            {

                ServiceAgence.CriteresRechercheBiensImmobiliers criteres =
                    new ServiceAgence.CriteresRechercheBiensImmobiliers();
                criteres.DateMiseEnTransaction1 = null;
                criteres.DateMiseEnTransaction2 = null;
                criteres.DateTransaction1 = null;
                criteres.DateTransaction2 = null;
                criteres.EnergieChauffage = null;
                criteres.MontantCharges1 = -1;
                criteres.MontantCharges2 = -1;
                criteres.NbEtages1 = -1;
                criteres.NbEtages2 = -1;
                criteres.NbPieces1 = -1;
                criteres.NbPieces2 = -1;
                criteres.NumEtage1 = -1;
                criteres.NumEtage2 = -1;
                criteres.Prix1 = -1;
                criteres.Prix2 = -1;
                criteres.Surface1 = -1;
                criteres.Surface2 = -1;
                criteres.TransactionEffectuee = null;
                criteres.TypeBien = null;
                criteres.TypeChauffage = null;
                criteres.TypeTransaction = null;
                ServiceAgence.ResultatListeBiensImmobiliers resultat = client.LireListeBiensImmobiliers(criteres,
                    null, null);

                //ServiceAgence.ResultatListeBiensImmobiliers resultat = client.LireListeBiensImmobiliers(null, null, null);

                if (resultat.SuccesExecution)
                {
                    liste = resultat.Liste.List;
                }
                else
                {
                    liste = new List<ServiceAgence.BienImmobilierBase>();
                    this.lblErreurs.Text = resultat.ErreursBloquantes.ToString();
                }
            }
            this.gvResultats.DataSource = liste;
            this.gvResultats.DataBind();
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