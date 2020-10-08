using Pra.Bieren.CORE.Services;
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
using Pra.Bieren.CORE.Services;
using Pra.Bieren.CORE.Entities; 


namespace Pra.Bieren.WPF
{

    public partial class winBieren : Window
    {
        bool isNew; // gaat het over een nieuw aangemaakt bier
        BierService bierService; //globaal zodat je list overal kan gebruiken
        BierSoortService bierSoortService; //globaal zodat je list overal kan gebruiken 

        public winBieren()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ViewDefault();
            PopulateCombobox();
            PopulateListBox(); 

        }


        private void ViewDefault()
        {
            grpBieren.IsEnabled = true;
            grpDetails.IsEnabled = false;

            btnSave.Visibility = Visibility.Hidden;
            btnCancel.Visibility = Visibility.Hidden;
        }

        private void ViewOperation()
        {
            grpBieren.IsEnabled = false;
            grpDetails.IsEnabled = false;

            btnSave.Visibility = Visibility.Visible;
            btnCancel.Visibility = Visibility.Visible;
        }

        private void InitializeControls()
        {
            txtNaam.Text = "";
            txtAlcohol.Text = "";
            cmbBiersoort.SelectedIndex = 0;
            sldScore.Value = 1;
        }
        private void PopulateListBox()
        {
            bierService = new BierService();
            lstBieren.ItemsSource = bierService.Bieren; 
        }

        private void PopulateCombobox()
        {
            bierSoortService = new BierSoortService();
            cmbBiersoort.ItemsSource = bierSoortService.Biersoorten;
        }

    }
}
