using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using ClientWPF.ServiceAgence;

namespace ClientWPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        private Dictionary<string, object> _values = new Dictionary<string, object>();
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        protected object GetField([CallerMemberName] string propertyName = null)
        {
            if (_values.ContainsKey(propertyName)) return _values[propertyName];
            return null;
        }
        protected bool SetField<T>(T value, [CallerMemberName] string propertyName = null)
        {
            T field = default(T);

            if (_values.ContainsKey(propertyName))
            {
                field = (T)_values[propertyName];
                _values[propertyName] = value;
            }
            else
            {
                _values.Add(propertyName, value);
            }

            return SetField(ref field, value, propertyName);
        }
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion

        public string TextSelectionne
        {
            get { return GetField().ToString(); }
            set { SetField(value); }
        }

        public ObservableCollection<BienImmobilierBase> BienImmobiliers
        {
            get { return (ObservableCollection<BienImmobilierBase>)GetField(); }
            set { SetField(value); }
        }

        public MainWindow()
        {
            this.DataContext = this;
            this.TextSelectionne = null;
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

        public void Charger(CriteresRechercheBiensImmobiliers criteres)
        {
            using (var client = new AgenceClient())
            {
                var resultat = client.LireListeBiensImmobiliers(criteres, null, null);

                BienImmobiliers = resultat.SuccesExecution ? resultat.Liste.List : new ObservableCollection<BienImmobilierBase>();
            }
        }

        private void ListBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
            //ToDO afficher l'immobilier concerné
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Filter win2 = new Filter();
            win2.ShowDialog();
            //this.Close();
        }

        private void BtSearch_OnClick(object sender, RoutedEventArgs e)
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
                TitreContient = ((string.IsNullOrEmpty(TxtSearchName.Text) ? "" : TxtSearchName.Text)),
                //TitreContient = (TxtSearchName == null ? "" : (string.IsNullOrEmpty(TxtSearchName.Text) ? "" : TxtSearchName.Text)),
            };
            Charger(criteres);

        }
    }
}
