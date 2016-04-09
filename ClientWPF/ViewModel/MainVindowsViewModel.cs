using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ClientWPF.Models;
using ClientWPF.ServiceAgence;
using ClientWPF.Views;
using Template_ListBox;

namespace ClientWPF.ViewModel
{
    class MainVindowsViewModel : BaseNotifyPropertyChanged
    {

        #region Propriétés
        public string TextSelectionne
        {
            get { return (string)GetField(); }
            set { SetField(value); }
        }
        public string TextSearch
        {
            get { return (string)GetField(); }
            set { SetField(value); }
        }

        public ObservableCollection<BienImmobilierBase> BienImmobiliers
        {
            get { return (ObservableCollection<BienImmobilierBase>)GetField(); }
            set { SetField(value); }
        }

        public BienImmobilier SelectedItem
        {
            get { return (BienImmobilier)GetField(); }
            set { SetField(value); }
        }
        public BienImmobilierBase SelectedItemBase
        {
            get { return (BienImmobilierBase)GetField(); }
            set
            {
                if (SetField(value))
                {
                    ShowImmoDesc();
                }
            }
        }

        public BitmapImage Image
        {
            get { return (BitmapImage)GetField(); }
            set{ SetField(value); }
        }

        public bool AfficherBouton
        {
            get
            {
                if (GetField() == null)
                    return false;
                return (bool) GetField();
            }
            set { SetField(value); }
        }

        public bool AfficherDesc
        {
            get
            {
                if (GetField() == null)
                    return false;
                return (bool) GetField();
            }
            set { SetField(value); }
        }

        private int IndexImage = 0;

        #endregion

        #region Commands

        public EventBindingCommand<EventArgs> LaunchCommand
        {
            get { return new EventBindingCommand<EventArgs>(Launch); }
        }
        public EventBindingCommand<EventArgs> LoadImageCommand
        {
            get { return new EventBindingCommand<EventArgs>(LoadImage); }
        }

        public BaseCommand OnClickSearchCommand
        {
            get { return new BaseCommand(BtSearch_OnClick); }
        }
        public BaseCommand OnClickFilterCommand
        {
            get { return new BaseCommand(MenuItem_OnClickFilter); }
        }
        public BaseCommand OnClickRestartFilterCommand
        {
            get { return new BaseCommand(MenuItem_OnClickRestartFilter); }
        }
        public BaseCommand OnClickAddBienCommand
        {
            get { return new BaseCommand(MenuItem_OnClickAddBien); }
        }


        public BaseCommand OnClickPrecCommand
        {
            get { return new BaseCommand(Prec_OnClick); }
        }
        public BaseCommand OnClickSuivCommand
        {
            get { return new BaseCommand(Suiv_OnClick); }
        }

        public BaseCommand OnClickDeleteCommand
        {
            get { return new BaseCommand(Delete_OnClick);}
        }

        #endregion

        #region FonctionCommand
        private void Launch(EventBindingArgs<EventArgs> args)
        {
            //todo cod pour launch   
            this.AfficherBouton = false;
            this.AfficherDesc = false;
            this.TextSelectionne = null;
            Charger();

        }

        private void BtSearch_OnClick()
        {
            CriteresRechercheBiensImmobiliers criteres = new CriteresRechercheBiensImmobiliers
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
                TransactionEffectuee = false,
                TypeBien = null,
                TypeChauffage = null,
                TypeTransaction = null,
                TitreContient = ((string.IsNullOrEmpty(TextSearch) ? "" : TextSearch)),
                //TitreContient = (TxtSearchName == null ? "" : (string.IsNullOrEmpty(TxtSearchName.Text) ? "" : TxtSearchName.Text)),
            };
            Charger(criteres);

        }

        private void MenuItem_OnClickFilter()
        {
            Filter filter = new Filter();
            //filter.Owner = this;
            if (filter.ShowDialog() == true)
            {
                
            }
            Charger(((FilterViewModel)filter.DataContext).Criteres);
            //this.Close();
        }
        private void MenuItem_OnClickAddBien()
        {
            NewBien filter = new NewBien();
            if (filter.ShowDialog() == true)
            {

            }
            Charger();
        }
        private void MenuItem_OnClickRestartFilter()
        {
            Charger();
        }

        private void Prec_OnClick()
        {
            ImagePrec();
        }

        private void Suiv_OnClick()
        {
            ImageSuiv();
        }

        private void Delete_OnClick()
        {
            DeleteBien();
        }

        #endregion

        public void Charger(CriteresRechercheBiensImmobiliers criteres = null)
        {
            if (criteres == null)
            {
                this.TextSearch = "";
                criteres = new CriteresRechercheBiensImmobiliers
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
                    TransactionEffectuee = false,
                    TypeBien = null,
                    TypeChauffage = null,
                    TypeTransaction = null,
                };
            }
            using (var client = new AgenceClient())
            {
                var resultat = client.LireListeBiensImmobiliers(criteres, null, null);

                BienImmobiliers = resultat.SuccesExecution ? resultat.Liste.List : new ObservableCollection<BienImmobilierBase>();
            }
        }

        public void LoadImage(EventBindingArgs<EventArgs> eventBindingArgs)
        {
            
        }

        private void ShowImmoDesc()
        {
            //ToDO Affichage de l'image
            using (var client = new AgenceClient())
            {
                if (SelectedItemBase == null)
                {
                    clearDisplay();
                    return;
                };
                var lireDetailsBienImmobilier = client.LireDetailsBienImmobilier(SelectedItemBase.Id.ToString());
                if (lireDetailsBienImmobilier != null)
                {
                    SelectedItem = lireDetailsBienImmobilier.Bien;
                    Image = SelectedItem.PhotosBase64 != null && SelectedItem.PhotosBase64.Count > 0
                        ? Base64ToImage(SelectedItem.PhotosBase64[0])
                        : GetDefaultImage();
                    AfficherDesc = true;
                    if (SelectedItem.PhotosBase64 != null) AfficherBouton = SelectedItem.PhotosBase64.Count > 1;
                    else AfficherBouton = false;
                }
                else
                {
                    clearDisplay();
                }
            }
        }

        private void ImagePrec()
        {
            if (SelectedItem != null && SelectedItem.PhotosBase64 != null && SelectedItem.PhotosBase64.Count > 1)
            {
                if (IndexImage == 0)
                    IndexImage = SelectedItem.PhotosBase64.Count - 1;
                else
                    IndexImage--;
                Image = SelectedItem.PhotosBase64 != null && SelectedItem.PhotosBase64.Count > 0 ? Base64ToImage(SelectedItem.PhotosBase64[IndexImage]) : GetDefaultImage();
            }
        }

        private void ImageSuiv()
        {
            if (SelectedItem != null && SelectedItem.PhotosBase64 != null && SelectedItem.PhotosBase64.Count > 1)
            {
                if (IndexImage == SelectedItem.PhotosBase64.Count - 1)
                    IndexImage = 0;
                else
                    IndexImage++;
                Image = SelectedItem.PhotosBase64 != null && SelectedItem.PhotosBase64.Count > 0 ? Base64ToImage(SelectedItem.PhotosBase64[IndexImage]) : GetDefaultImage();
            }
        }

        private void DeleteBien()
        {
            using (ServiceAgence.AgenceClient client = new ServiceAgence.AgenceClient())
            {
                if (SelectedItemBase == null) return;
                ServiceAgence.ResultatBool r = client.SupprimerBienImmobilier(SelectedItemBase.Id.ToString());
                BienImmobiliers.Remove(SelectedItemBase);
                clearDisplay();
                if (!r.SuccesExecution)
                {
                    
                }
            }
        }

        private void clearDisplay()
        {
            SelectedItemBase = null;
            SelectedItem = null;
            AfficherBouton = false;
            AfficherDesc = false;
            Image = null;
        }

        public BitmapImage Base64ToImage(string base64String)
        {
            if (base64String != "")
            {
                byte[] binaryData = Convert.FromBase64String(base64String);
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = new MemoryStream(binaryData);
                bi.EndInit();

                return bi;
            }
            return null;
        }

        private BitmapImage GetDefaultImage()
        {
            return new BitmapImage(new Uri(@"pack://application:,,,/res/noImage.jpg"));
        }
    }
}
