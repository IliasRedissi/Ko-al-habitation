using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ClientWPF.Models;
using Template_ListBox;
using ClientWPF.ServiceAgence;

namespace ClientWPF.ViewModel
{
    class FilterViewModel : BaseNotifyPropertyChanged
    {

        #region Propriété
        public CriteresRechercheBiensImmobiliers Criteres
        {
            get { return (CriteresRechercheBiensImmobiliers)GetField(); }
            set { SetField(value); }
        }
        public string SelectedTypeTransaction
        {

            get { return GetField() != null ? (string)GetField() : "Vente"; }
            set { SetField(value); }
        }
        public ObservableCollection<string> ListTransactions
        {
            get { return (ObservableCollection<string>)GetField(); }
            set { SetField(value); }
        }
        public ObservableCollection<string> ListTypeBien
        {
            get { return (ObservableCollection<string>)GetField(); }
            set { SetField(value); }
        }
        public ObservableCollection<string> ListTypeChauffage
        {
            get { return (ObservableCollection<string>)GetField(); }
            set { SetField(value); }
        }
        public string SelectedTypeBien
        {

            get { return GetField() != null ? (string)GetField() : "Appartement"; }
            set { SetField(value); }
        }
        public string SelectedTypeChauffage
        {

            get { return GetField() != null ? (string)GetField() : "Aucun"; }
            set { SetField(value); }
        }
        public Double MontantChargeMin
        {
            get { return GetField() != null ? (Double)GetField() : 0; }
            set { SetField(value); }
        }
        public Double MontantChargeMax
        {
            get { return GetField() != null ? (Double)GetField() : 0; }
            set { SetField(value); }
        }
        public bool MontantChargeMinChecked
        {
            get { return GetField() != null && (bool)GetField(); }
            set { SetField(value); }
        }
        public bool MontantChargeMaxChecked
        {
            get { return GetField() != null && (bool)GetField(); }
            set { SetField(value); }
        }
        public string MessageErreur
        {

            get { return GetField() != null ? (string)GetField() : ""; }
            set { SetField(value); }
        }

        #region Price

        public int MinPrice
        {
            get { return GetField() != null ? (int)GetField() : 0; }
            set { SetField(value); }
        }
        public int MaxPrice
        {
            get { return GetField() != null ? (int)GetField() : 0; }
            set { SetField(value); }
        }
        public int MinValuePrice
        {
            get { return GetField() != null ? (int)GetField() : 0; }
            set { SetField(value); }
        }
        public int MaxValuePrice
        {
            get { return GetField() != null ? (int)GetField() : 0; }
            set { SetField(value); }
        }
        #endregion

        #endregion

        #region Command
        public EventBindingCommand<EventArgs> LaunchCommand
        {
            get { return new EventBindingCommand<EventArgs>(Launch); }
        }

        public GenericBaseCommand<Window> FilterCommand
        {
            get { return new GenericBaseCommand<Window>(Filtrer);}
        } 

        #endregion

        #region Command Function

        private void Launch(EventBindingArgs<EventArgs> args)
        {

            #region criteres
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
            };
            #endregion
            using (var client = new AgenceClient())
            {
                var resultat = client.LireListeBiensImmobiliers(criteres, null, null);

                ObservableCollection<BienImmobilierBase> bienImmobiliers = resultat.SuccesExecution ? resultat.Liste.List : new ObservableCollection<BienImmobilierBase>();
                bienImmobiliers.Min(b => b.Prix);
                MinPrice = (int)bienImmobiliers.Min(b => b.Prix);
                MaxPrice = (int)bienImmobiliers.Max(b => b.Prix);
                MinValuePrice = (int)bienImmobiliers.Min(b => b.Prix);
                MaxValuePrice = (int)bienImmobiliers.Max(b => b.Prix);
            }

            ListTransactions = new ObservableCollection<string>();
            ListTransactions.Add("Tous");
            ListTransactions.Add(BienImmobilierBase.eTypeTransaction.Vente.ToString());
            ListTransactions.Add(BienImmobilierBase.eTypeTransaction.Location.ToString());
            ListTypeBien = new ObservableCollection<string>();
            ListTypeBien.Add("Tous");
            ListTypeBien.Add(BienImmobilierBase.eTypeBien.Appartement.ToString());
            ListTypeBien.Add(BienImmobilierBase.eTypeBien.Garage.ToString());
            ListTypeBien.Add(BienImmobilierBase.eTypeBien.Maison.ToString());
            ListTypeBien.Add(BienImmobilierBase.eTypeBien.Terrain.ToString());
            ListTypeChauffage = new ObservableCollection<string>();
            ListTypeChauffage.Add("Tous");
            ListTypeChauffage.Add(BienImmobilierBase.eTypeChauffage.Aucun.ToString());
            ListTypeChauffage.Add(BienImmobilierBase.eTypeChauffage.Collectif.ToString());
            ListTypeChauffage.Add(BienImmobilierBase.eTypeChauffage.Individuel.ToString());


        }

        private void Filtrer(Window win)
        {
            if (MontantChargeMin > MontantChargeMax && MontantChargeMaxChecked && MontantChargeMinChecked)
            {
                MessageErreur = "Le montant charge Minimum est plus élevé que le montant charge Maximum";
            }
            else
            {
                Criteres = new CriteresRechercheBiensImmobiliers
                {
                    DateMiseEnTransaction1 = null,
                    DateMiseEnTransaction2 = null,
                    DateTransaction1 = null,
                    DateTransaction2 = null,
                    EnergieChauffage = null,
                    MontantCharges1 = MontantChargeMinChecked ? MontantChargeMin : -1,
                    MontantCharges2 = MontantChargeMaxChecked ? MontantChargeMax : -1,
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
                    TypeBien = ConvertTypeBien(SelectedTypeBien),
                    TypeChauffage = ConvertTypeChauffage(SelectedTypeChauffage),
                    TypeTransaction = ConvertTransaction(SelectedTypeTransaction),
                };
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

        #region Convert enum function

        private BienImmobilierBase.eTypeTransaction? ConvertTransaction(string transaction)
        {
            if (transaction == BienImmobilierBase.eTypeTransaction.Vente.ToString())
            {
                return BienImmobilierBase.eTypeTransaction.Vente;
            }
            else if (transaction == BienImmobilierBase.eTypeTransaction.Location.ToString())
            {
                return BienImmobilierBase.eTypeTransaction.Vente;
            }
            return null;
        }
        private BienImmobilierBase.eTypeBien? ConvertTypeBien(string typeBien)
        {
            if (typeBien == BienImmobilierBase.eTypeBien.Appartement.ToString())
            {
                return BienImmobilierBase.eTypeBien.Appartement;
            }
            else if (typeBien == BienImmobilierBase.eTypeBien.Garage.ToString())
            {
                return BienImmobilierBase.eTypeBien.Garage;
            }
            else if (typeBien == BienImmobilierBase.eTypeBien.Maison.ToString())
            {
                return BienImmobilierBase.eTypeBien.Maison;
            }
            else if (typeBien == BienImmobilierBase.eTypeBien.Terrain.ToString())
            {
                return BienImmobilierBase.eTypeBien.Terrain;
            }
            return null;
        }
        private BienImmobilierBase.eTypeChauffage? ConvertTypeChauffage(string typeBien)
        {
            if (typeBien == BienImmobilierBase.eTypeChauffage.Aucun.ToString())
            {
                return BienImmobilierBase.eTypeChauffage.Aucun;
            }
            else if (typeBien == BienImmobilierBase.eTypeChauffage.Collectif.ToString())
            {
                return BienImmobilierBase.eTypeChauffage.Collectif;
            }
            else if (typeBien == BienImmobilierBase.eTypeChauffage.Individuel.ToString())
            {
                return BienImmobilierBase.eTypeChauffage.Individuel;
            }
            return null;
        }


        #endregion
    }
}
