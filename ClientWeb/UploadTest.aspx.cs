
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.Services.Protocols;
using ClientWeb.ServiceAgence;

namespace ClientWeb
{
    public partial class UploadTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string[] filePaths = Directory.GetFiles(Server.MapPath("~/ImgTmp/"));
                List<ListItem> files = new List<ListItem>();
                foreach (string filePath in filePaths)
                {
                    string fileName = Path.GetFileName(filePath);
                    files.Add(new ListItem(fileName, "~/ImgTmp/" + fileName));
                }
                GridView1.DataSource = files;
                GridView1.DataBind();
            }
        }
        protected void Upload(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/ImgTmp/") + fileName);
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }

        public static string bitmapToBase64String(System.Drawing.Bitmap img)
        {
            //sert à creer une bitmap
            new System.Drawing.Bitmap("chemin acces img");
            BienImmobilier bien = new BienImmobilier();
            //pour ajouter l'image
            //bien.PhotosBase64 = new List<string>();
            //bien.PhotosBase64.Add(bitmapToBase64String(new System.Drawing.Bitmap("chemin accès")));


            if (img != null) return "";
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                byte[] byteArray = stream.ToArray();
                return System.Convert.ToBase64String(byteArray);
            }
        }
    }
}
