using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClientWeb.ServiceAgence;

namespace ClientWeb
{
    public partial class CreateImmobilier : System.Web.UI.Page
    {
        public String FichierUpload
        {
            get { return (String)ViewState["fichierUpload"]; }
            set { ViewState["fichierUpload"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                //ImageResult.ImageUrl = FichierUpload;
                return;
            }
            List<String> liste = new List<String>();
            liste.Add("Appartement");
            liste.Add("Maison");
            liste.Add("Garage");
            liste.Add("Terrain");
            FichierUpload = "";
            ImageResult.ImageUrl = "~/res/noImage.jpg";
            this.listBien.DataSource = liste;
            this.listBien.DataBind();
            liste = new List<String>();
            liste.Add("Location");
            liste.Add("Vente");
            this.listeTypeLocation.DataSource = liste;
            this.listeTypeLocation.DataBind();
            liste = new List<String>();
            liste.Add("Aucun");
            liste.Add("Individuel");
            liste.Add("Collectif");
            this.listeChauffage.DataSource = liste;
            this.listeChauffage.DataBind();
            liste = new List<String>();
            liste.Add("Aucun");
            liste.Add("Fioul");
            liste.Add("Gaz");
            liste.Add("Electrique");
            liste.Add("Bois");
            this.listeEnergie.DataSource = liste;
            this.listeEnergie.DataBind();
        }

        protected void AddImmobilier_Click(object sender, EventArgs e)
        {
            using (ServiceAgence.AgenceClient client = new ServiceAgence.AgenceClient())
            {
                BienImmobilier bien = new BienImmobilier();
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
                switch (listeTypeLocation.Text)
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
                switch (listeEnergie.Text)
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
                switch (listeChauffage.Text)
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

                bien.PhotosBase64 = new List<string>();
                if (!String.IsNullOrEmpty(FichierUpload))
                {
                    //create a Bitmap
                    bien.PhotosBase64.Add(bitmapToBase64String(new System.Drawing.Bitmap(FichierUpload)));
                }
                else
                {
                    bien.PhotoPrincipaleBase64 = "";
                }
                client.AjouterBienImmobilier(bien);
                Response.Redirect("~/Admin.aspx");
            }
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
