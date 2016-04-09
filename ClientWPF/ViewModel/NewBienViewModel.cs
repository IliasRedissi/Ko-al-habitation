using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using ClientWPF.Models;
using ClientWPF.ServiceAgence;
using Microsoft.Win32;
using Template_ListBox;

namespace ClientWPF.ViewModel
{
    class NewBienViewModel : BaseNotifyPropertyChanged
    {

        #region Propriété

        #region Enum
        public BienImmobilierBase.eTypeTransaction SelectedTypeTransaction
        {

            get { return GetField() != null ? (BienImmobilierBase.eTypeTransaction)GetField() : BienImmobilierBase.eTypeTransaction.Vente; }
            set { SetField(value); }
        }
        public BienImmobilierBase.eTypeBien SelectedTypeBien
        {

            get { return GetField() != null ? (BienImmobilierBase.eTypeBien)GetField() : BienImmobilierBase.eTypeBien.Appartement; }
            set { SetField(value); }
        }
        public BienImmobilierBase.eTypeChauffage SelectedTypeChauffage
        {

            get { return GetField() != null ? (BienImmobilierBase.eTypeChauffage)GetField() : BienImmobilierBase.eTypeChauffage.Aucun; }
            set { SetField(value); }
        }
        public BienImmobilierBase.eEnergieChauffage SelectedEnergieChauffage
        {

            get { return GetField() != null ? (BienImmobilierBase.eEnergieChauffage)GetField() : BienImmobilierBase.eEnergieChauffage.Bois; }
            set { SetField(value); }
        }

        #endregion

        public string Title
        {
            get { return (string)GetField(); }
            set { SetField(value); }
        }

        public Double Price
        {
            get { return GetField() != null ? (Double)GetField() : 0; }
            set { SetField(value); }
        }
        public Double MontantCharge
        {
            get { return GetField() != null ? (Double)GetField() : 0; }
            set { SetField(value); }
        }
        public string CodePostal
        {
            get { return GetField() != null ? (string)GetField() : "01"; }
            set { SetField(value); }
        }

        public string Ville
        {
            get { return (string)GetField(); }
            set { SetField(value); }
        }

        public string Description
        {
            get { return GetField()!=null?(string)GetField():"votre description"; }
            set { SetField(value); }
        }public string Adresse
        {
            get { return (string)GetField(); }
            set { SetField(value); }
        }
        public BitmapImage Image
        {
            get { return (BitmapImage)GetField(); }
            set { SetField(value); }
        }
        public ObservableCollection<string> ImageBase64
        {

            get { return (ObservableCollection<string>)GetField(); }
            set { SetField(value); }
        }

        public string MessageErreur
        {

            get { return GetField() != null ? (string)GetField() : ""; }
            set { SetField(value); }
        }

        #endregion

        #region Command
        public GenericBaseCommand<Window> AddCommand
        {
            get { return new GenericBaseCommand<Window>(Add); }
        }
        public BaseCommand UploadCommand
        {
            get { return new BaseCommand(Upload); }
        }

        #endregion

        #region Command Function

        private void Add(Window win)
        {
            if (String.IsNullOrEmpty(Title))
                MessageErreur = "Veuillez renseigner un nom";
            else if (String.IsNullOrEmpty(Ville))
                MessageErreur = "Veuillez renseigner une ville";
            else
            {
                BienImmobilier bien = new BienImmobilier();
                bien.Titre = Title;
                bien.Ville = Ville;
                bien.Prix = Price;
                bien.CodePostal = CodePostal;
                bien.MontantCharges = MontantCharge;
                bien.TypeBien = SelectedTypeBien;
                bien.TypeTransaction = SelectedTypeTransaction;
                bien.TypeChauffage = SelectedTypeChauffage;
                bien.EnergieChauffage = SelectedEnergieChauffage;
                bien.DateMiseEnTransaction = DateTime.Now;
                bien.Adresse = Adresse;
                bien.Description = Description;
                if (ImageBase64 != null)
                {
                    bien.PhotosBase64 = ImageBase64;
                }
                using (var client = new AgenceClient())
                {
                    client.AjouterBienImmobilier(bien);
                }
                if (win != null)
                {
                    win.Close();
                }
                else
                {
                    MessageErreur = "Probleme de fermeture de la fenetre";
                }
            }
        }

        private void Upload()
        {
            BitmapImage no = new BitmapImage();
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Choisir la photo";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(op.FileName);
                double[] sizeImage = getRatioImage(bmp);
                if (sizeImage == null)
                {
                    return;
                }
                bmp = new System.Drawing.Bitmap(bmp, new System.Drawing.Size((int)sizeImage[0], (int)sizeImage[1]));
                System.IO.MemoryStream stream = new System.IO.MemoryStream();
                bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                no.BeginInit();
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                no.StreamSource = stream;
                no.EndInit();
                Image = no;
                // Convertis une Bitmapimage en StringBase64
                MemoryStream ms = new MemoryStream();
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(no));
                encoder.Save(ms);
                byte[] bitmapdata = ms.ToArray();
                if (ImageBase64 == null)
                {
                    ImageBase64 = new ObservableCollection<string> { Convert.ToBase64String(bitmapdata) };
                }
                else
                {
                    ImageBase64.Add(Convert.ToBase64String(bitmapdata));
                }

            }
        }

        #endregion

        private double[] getRatioImage(System.Drawing.Bitmap bmp)
        {
            double[] tmp = new double[2];
            double ratio = ((double)bmp.Width / (double)bmp.Height);
            double targetHeight;
            /*le 250 c'est la valeur min que vous voulez en taille de photo(250*250)*/
            if (Math.Max(bmp.Width, bmp.Height) <= 250)
            {
                return null;
            }
            double targetWidth = targetHeight = Math.Max(bmp.Width, bmp.Height);
            if (ratio == 1)
            {
                targetHeight = 250;
                targetWidth = 250;
            }
            else if (ratio < 1)
            {
                targetHeight = 250;
                targetWidth = 250 * ratio;
            }
            else
            {
                targetHeight = 250 / ratio;
                targetWidth = 250;
            }
            tmp[0] = targetWidth;
            tmp[1] = targetHeight;
            return tmp;
        }
    }
}
