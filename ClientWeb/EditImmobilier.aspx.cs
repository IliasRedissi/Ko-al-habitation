
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClientWeb.ServiceAgence;

namespace ClientWeb
{
    public partial class EditImmobilier : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            Charger();
        }

        private void Charger()
        {
            List<ServiceAgence.BienImmobilierBase> liste = null;
            List<String> listeBien = new List<String>();
            ;
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
                if (Request.QueryString["id"] != null)
                {
                    ResultatBienImmobilier result = client.LireDetailsBienImmobilier(Request.QueryString["id"]);
                    if (result.SuccesExecution)
                    {
                        if(!String.IsNullOrEmpty(result.Bien.PhotoPrincipaleBase64))
                        imgImmobilier.ImageUrl = "data:img/png;base64," + result.Bien.PhotoPrincipaleBase64;
                        else
                        {
                            imgImmobilier.ImageUrl = "./res/noImage.jpg";
                        }
                        listeBien.Add("Appartement");
                        listeBien.Add("Maison");
                        listeBien.Add("Garage");
                        listeBien.Add("Terrain");
                        this.listBien.DataSource = listeBien;
                        this.listBien.DataBind();
                        int position = (int)result.Bien.TypeBien;
                        listBien.SelectedIndex = position;
                        txtTitle.Attributes["value"] = result.Bien.Titre;
                        txtPrice.Text = result.Bien.Prix.ToString().Replace(',', '.');

                    }
                    else {
                        Response.Redirect("~/Admin.aspx");
                    }
                }
                else {
                    Response.Redirect("~/Admin.aspx");

                }
            }
        }

        protected void EditImmobilier_OnClick(object sender, EventArgs e)
        {
            using (ServiceAgence.AgenceClient client = new ServiceAgence.AgenceClient())
            {
                ResultatBienImmobilier resultat = client.LireDetailsBienImmobilier(Request.QueryString["id"]);
                BienImmobilier bienModif = new BienImmobilier();


                double price;
                /*
                if(!Double.TryParse(this.txtPrice.Text, out price))
                {
                    price = resultat.Bien.Prix;
                }
                */
                var style = NumberStyles.Float | NumberStyles.AllowThousands;
                var culture = CultureInfo.InvariantCulture;
                if (double.TryParse(txtPrice.Text, style, culture, out price))
                {
                    // whatever
                }
                ServiceAgence.BienImmobilierBase.eTypeBien typeBien = BienImmobilierBase.eTypeBien.Terrain;
                switch (listBien.Text)
                {
                    case "Appartement":
                        typeBien = BienImmobilierBase.eTypeBien.Appartement;
                        break;
                    case "Maison":
                        typeBien = BienImmobilierBase.eTypeBien.Maison;
                        break;
                    case "Garage":
                        typeBien = BienImmobilierBase.eTypeBien.Garage;
                        break;
                    case "Terrain":
                        typeBien = BienImmobilierBase.eTypeBien.Terrain;
                        break;
                }
                resultToBien(bienModif, resultat);
                bienModif.Id = int.Parse(Request.QueryString["id"]);
                bienModif.Prix = price;
                bienModif.Titre = txtTitle.Text;
                bienModif.TypeBien = typeBien;
                client.ModifierBienImmobilier(bienModif);
                Response.Redirect("~/Admin.aspx");
            }
        }

        private void resultToBien(BienImmobilier bienModif, ResultatBienImmobilier result)
        {
            bienModif.Adresse = result.Bien.Adresse;
            bienModif.Description = result.Bien.Description;
            bienModif.EnergieChauffage = result.Bien.EnergieChauffage;
            bienModif.NbEtages = result.Bien.NbEtages;
            bienModif.NbPieces = result.Bien.NbPieces;
            bienModif.PhotosBase64 = result.Bien.PhotosBase64;
            bienModif.NumEtage = result.Bien.NumEtage;
            bienModif.Surface = result.Bien.Surface;
            bienModif.TypeChauffage = result.Bien.TypeChauffage;
            bienModif.CodePostal = result.Bien.CodePostal;
            bienModif.DateMiseEnTransaction = result.Bien.DateMiseEnTransaction;
            bienModif.DateTransaction = result.Bien.DateTransaction;
            bienModif.ExtensionData = result.Bien.ExtensionData;
            bienModif.Id = result.Bien.Id;
            bienModif.MontantCharges = result.Bien.MontantCharges;
            bienModif.PhotoPrincipaleBase64 = result.Bien.PhotoPrincipaleBase64;
            bienModif.Prix = result.Bien.Prix;
            bienModif.TransactionEffectuee = result.Bien.TransactionEffectuee;
            bienModif.Titre = result.Bien.Titre;
            bienModif.Ville = result.Bien.Ville;
            bienModif.TypeBien = result.Bien.TypeBien;
        }
    }
}
