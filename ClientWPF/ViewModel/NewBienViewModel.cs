using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
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
    class NewBienViewModel : BaseNotifyPropertyChanged, IEntityViewModel<BienImmobilier>
    {

        #region Propriété
        public BitmapImage Image
        {
            get { return (BitmapImage)GetField(); }
            set { SetField(value); }
        }

        public string MessageErreur
        {

            get { return GetField() != null ? (string)GetField() : ""; }
            set { SetField(value); }
        }

        public BienImmobilier BienImmobilier
        {
            get {
                if (GetField() == null)
                {
                    BienImmobilier bien = new BienImmobilier();

                    bien.EnergieChauffage = BienImmobilierBase.eEnergieChauffage.Aucun;
                    bien.TypeChauffage = BienImmobilierBase.eTypeChauffage.Aucun;
                    bien.TypeBien = BienImmobilierBase.eTypeBien.Appartement;
                    bien.TypeTransaction = BienImmobilierBase.eTypeTransaction.Vente;
                    SetField(bien);
                    return bien;
                }
                else
                {
                    return (BienImmobilier) GetField();
                }
            }
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

        public EventBindingCommand<EventArgs> LaunchCommand
        {
            get { return new EventBindingCommand<EventArgs>(Launch); }
        }

        #endregion

        #region Command Function

        private void Add(Window win)
        {
            if (BienImmobilier != null)
            {
                if (String.IsNullOrEmpty(BienImmobilier.Titre))
                    MessageErreur = "Veuillez renseigner un nom";
                else if (String.IsNullOrEmpty(BienImmobilier.Ville))
                    MessageErreur = "Veuillez renseigner une ville";
                else
                {
                    using (var client = new AgenceClient())
                    {
                        if (BienImmobilier.Id == 0)
                            client.AjouterBienImmobilier(BienImmobilier);
                        else
                            client.ModifierBienImmobilier(BienImmobilier);
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
                if (BienImmobilier.PhotosBase64 == null)
                {
                    BienImmobilier.PhotosBase64 = new ObservableCollection<string> { Convert.ToBase64String(bitmapdata) };
                }
                else
                {
                    BienImmobilier.PhotosBase64.Add(Convert.ToBase64String(bitmapdata));
                }

            }
        }

        private void Launch(EventBindingArgs<EventArgs> args)
        {
            //todo cod pour launch   
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

        public void SetCurrentEntity(BienImmobilier entity)
        {
            BienImmobilier = entity;
        }
    }
}
