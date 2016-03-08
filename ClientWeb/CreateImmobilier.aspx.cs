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
        }

        protected void AddImmobilier_Click(object sender, EventArgs e)
        {
            using (ServiceAgence.AgenceClient client = new ServiceAgence.AgenceClient())
            {
                BienImmobilier bien = new BienImmobilier();
                bien.Titre = this.txtTitle.Text;
                double price;
                /*
                if(!Double.TryParse(this.txtPrice.Text, out price))
                {
                    price = resultat.Bien.Prix;
                }
                */
                var style = NumberStyles.Float | NumberStyles.AllowThousands;
                var culture = CultureInfo.InvariantCulture;
                if (double.TryParse(this.txtPrice.Text, style, culture, out price))
                {
                    bien.Prix = price;
                }
                else
                {
                    bien.Prix = 0;
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
                bien.TypeBien = typeBien;
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
