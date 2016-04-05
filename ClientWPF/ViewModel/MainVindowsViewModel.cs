using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ClientWPF.Models;
using ClientWPF.ServiceAgence;
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
        
        public BienImmobilierBase SelectedItem
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

        #endregion

        #region Commands

        public EventBindingCommand<EventArgs> LaunchCommand
        {
            get { return new EventBindingCommand<EventArgs>(Launch); }
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

        #endregion

        #region FonctionCommand
        private void Launch(EventBindingArgs<EventArgs> args)
        {
            this.TextSelectionne = null;
            this.TextSearch = "Name";
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
            Charger(criteres);

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
                Charger(((FilterViewModel)filter.DataContext).Criteres);
            }
            
            //this.Close();
        }
        private void MenuItem_OnClickRestartFilter()
        {
            this.TextSearch = "";
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
            Charger(criteres);
        }
        #endregion

        public void Charger(CriteresRechercheBiensImmobiliers criteres)
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

        private void ShowImmoDesc()
        {
            //ToDO Affichage de l'image
        }
    }
}
