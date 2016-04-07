using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using ClientWPF.Models;
using ClientWPF.ServiceAgence;
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

        #endregion

    }
}
