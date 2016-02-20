using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClientWeb.ServiceAgence;

namespace ClientWeb
{
    public partial class CreateImmobilier : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<String> liste = new List<String>();
            using (ServiceAgence.AgenceClient client = new ServiceAgence.AgenceClient())
            {

            }
            liste.Add("Appartement");
            liste.Add("Maison");
            liste.Add("Garage");
            liste.Add("Terrain");

            this.listBien.DataSource = liste;
            this.listBien.DataBind();
        }

        protected void AddImmobilier_Click(object sender, EventArgs e)
        {
            using (ServiceAgence.AgenceClient client = new ServiceAgence.AgenceClient())
            {
                BienImmobilier bien = new BienImmobilier();
                bien.Titre = this.txtTitle.Text;
                Double price;
                Double.TryParse(this.txtPrice.Text, out price);
                bien.Prix = price;
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
                client.AjouterBienImmobilier(bien);
                Response.Redirect("~/Admin.aspx");
            }
        }
    }
}