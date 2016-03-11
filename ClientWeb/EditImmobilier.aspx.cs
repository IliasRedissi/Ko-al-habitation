
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClientWeb.ServiceAgence;

namespace ClientWeb
{
    public partial class EditImmobilier : System.Web.UI.Page
    {
        public String FichierUpload
        {
            get { return (String)ViewState["fichierUpload"]; }
            set { ViewState["fichierUpload"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            Charger();
        }

        private void Charger()
        {
            FichierUpload = "";
            List<ServiceAgence.BienImmobilierBase> liste = null;
            List<String> listeBien = new List<String>();
            List<String> listeTransaction = new List<String>();
            List<String> listeChauffage = new List<String>();
            List<String> listeEnergie = new List<String>();
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
                        ImageResult.ImageUrl = !String.IsNullOrEmpty(result.Bien.PhotoPrincipaleBase64)? "data:img/png;base64," + result.Bien.PhotoPrincipaleBase64: "~/res/noImage.jpg";
                        listeBien.Add("Appartement");
                        listeBien.Add("Maison");
                        listeBien.Add("Garage");
                        listeBien.Add("Terrain");
                        this.listBien.DataSource = listeBien;
                        this.listBien.DataBind();
                        listeTransaction.Add("Vente");
                        listeTransaction.Add("Location");
                        this.listTypeLocation.DataSource = listeTransaction;
                        this.listTypeLocation.DataBind();
                        listeChauffage.Add("Aucun");
                        listeChauffage.Add("Individuel");
                        listeChauffage.Add("Collectif");
                        this.listChauffage.DataSource = listeChauffage;
                        this.listChauffage.DataBind();
                        listeEnergie.Add("Aucun");
                        listeEnergie.Add("Fioul");
                        listeEnergie.Add("Gaz");
                        listeEnergie.Add("Electrique");
                        listeEnergie.Add("Bois");
                        this.listEnergie.DataSource = listeEnergie;
                        this.listEnergie.DataBind();
                        int position = (int)result.Bien.TypeBien;
                        this.listBien.SelectedIndex = position;
                        position = (int)result.Bien.TypeTransaction;
                        this.listTypeLocation.SelectedIndex = position;
                        position = (int)result.Bien.TypeChauffage;
                        this.listChauffage.SelectedIndex = position;
                        position = (int)result.Bien.EnergieChauffage;
                        this.listEnergie.SelectedIndex = position;

                        txtTitle.Attributes["value"] = result.Bien.Titre;
                        txtCodePostal.Attributes["value"] = result.Bien.CodePostal;
                        txtVille.Attributes["value"] = result.Bien.Ville;
                        txtDescription.Attributes["value"] = result.Bien.Description;
                        txtAdresse.Attributes["value"] = result.Bien.Adresse;
                        txtNbrePiece.Attributes["value"] = result.Bien.NbPieces.ToString();
                        txtNbrEtage.Attributes["value"] = result.Bien.NbEtages.ToString();
                        txtNumEtage.Attributes["value"] = result.Bien.NumEtage.ToString();

                        txtPrice.Text = result.Bien.Prix.ToString().Replace(',', '.');
                        txtMontantCharge.Text = result.Bien.MontantCharges.ToString().Replace(',', '.');
                        txtSurface.Text = result.Bien.Surface.ToString().Replace(',', '.');

                    }
                    else
                    {
                        Response.Redirect("~/Admin.aspx");
                    }
                }
                else
                {
                    Response.Redirect("~/Admin.aspx");

                }
            }
        }

        protected void EditImmobilier_OnClick(object sender, EventArgs e)
        {
            using (ServiceAgence.AgenceClient client = new ServiceAgence.AgenceClient())
            {
                ResultatBienImmobilier resultat = client.LireDetailsBienImmobilier(Request.QueryString["id"]);
                BienImmobilier bien = new BienImmobilier(); 
                resultToBien(bien, resultat);
                bien.Titre = this.txtTitle.Text;
                double price;
                var style = NumberStyles.Float | NumberStyles.AllowThousands;
                var culture = CultureInfo.InvariantCulture;
                bien.Prix = double.TryParse(this.txtPrice.Text, style, culture, out price) ? price : 0;
                bien.MontantCharges = double.TryParse(this.txtMontantCharge.Text, style, culture, out price) ? price : 0;
                int entier;
                bien.CodePostal = (int.TryParse(this.txtCodePostal.Text, out entier) ? entier : 1).ToString();
                bien.Ville = this.txtVille.Text;
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
                bien.TypeBien = typeBien;
                ServiceAgence.BienImmobilierBase.eTypeTransaction typeTransaction = BienImmobilierBase.eTypeTransaction.Location;
                switch (listTypeLocation.Text)
                {
                    case "Location":
                        typeTransaction = BienImmobilierBase.eTypeTransaction.Location;
                        break;
                    case "Vente":
                        typeTransaction = BienImmobilierBase.eTypeTransaction.Vente;
                        break;
                }
                bien.TypeTransaction = typeTransaction;

                bien.Description = this.txtDescription.Text;
                bien.Adresse = this.txtAdresse.Text;
                ServiceAgence.BienImmobilierBase.eEnergieChauffage typeEnergie = BienImmobilierBase.eEnergieChauffage.Aucun;
                switch (listEnergie.Text)
                {
                    case "Aucun":
                        typeEnergie = BienImmobilierBase.eEnergieChauffage.Aucun;
                        break;
                    case "Fioul":
                        typeEnergie = BienImmobilierBase.eEnergieChauffage.Fioul;
                        break;
                    case "Gaz":
                        typeEnergie = BienImmobilierBase.eEnergieChauffage.Gaz;
                        break;
                    case "Electrique":
                        typeEnergie = BienImmobilierBase.eEnergieChauffage.Electrique;
                        break;
                    case "Bois":
                        typeEnergie = BienImmobilierBase.eEnergieChauffage.Bois;
                        break;
                }
                bien.EnergieChauffage = typeEnergie;
                ServiceAgence.BienImmobilierBase.eTypeChauffage typeChauffage = BienImmobilierBase.eTypeChauffage.Aucun;
                switch (listChauffage.Text)
                {
                    case "Aucun":
                        typeChauffage = BienImmobilierBase.eTypeChauffage.Aucun;
                        break;
                    case "Individuel":
                        typeChauffage = BienImmobilierBase.eTypeChauffage.Individuel;
                        break;
                    case "Collectif":
                        typeChauffage = BienImmobilierBase.eTypeChauffage.Collectif;
                        break;
                }
                bien.TypeChauffage = typeChauffage;
                bien.Surface = double.TryParse(this.txtSurface.Text, style, culture, out price) ? price : 0;
                bien.NbPieces = int.TryParse(this.txtNbrePiece.Text, out entier) ? entier : 1;
                bien.NbEtages = int.TryParse(this.txtNbrEtage.Text, out entier) ? entier : 1;
                bien.NumEtage = int.TryParse(this.txtNumEtage.Text, out entier) ? entier : 1;

                //bien.PhotosBase64 = new List<string>();
                if (!String.IsNullOrEmpty(FichierUpload))
                {
                    //create a Bitmap
                    bien.PhotoPrincipaleBase64 = bitmapToBase64String(new System.Drawing.Bitmap(FichierUpload));
                }
                else
                {
                    bien.PhotoPrincipaleBase64 = "";
                }
                client.ModifierBienImmobilier(bien);
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

        protected void Upload(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/ImgTmp/") + fileName);
                FichierUpload = Server.MapPath("~/ImgTmp/") + fileName;
                lblUpload.Text = "Votre image a été upload";
                ImageResult.ImageUrl = "~/ImgTmp/" + fileName;
                //Response.Redirect("~/CreateImmobilier.aspx?file=" + fichierUpload);
            }
        }

        public static string bitmapToBase64String(System.Drawing.Bitmap img)
        {


            if (img == null) return "";
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                byte[] byteArray = stream.ToArray();
                return System.Convert.ToBase64String(byteArray);
            }
        }
    }
}
