using DSS.Business.Category;
using DSS.Data.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace DSS.Wpf.UI
{
    /// <summary>
    /// Interaction logic for wDiamondShell.xaml
    /// </summary>
    public partial class wDiamondShell : Window
    {
        private readonly DiamondShellBusiness _business;

        public wDiamondShell()
        {
            InitializeComponent();
            this._business??= new DiamondShellBusiness();
            LoadGrdDiamondShells();
        }

        private async void LoadGrdDiamondShells()
        {
            var result = await _business.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                grdDiamondShell.ItemsSource = result.Data as List<DiamondShell>;
            }
            else
            {
                grdDiamondShell.ItemsSource = new List<DiamondShell>();
            }
        }
        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int diamondShellIdTmp = -1;
                int.TryParse(DiamondShellId.Text, out diamondShellIdTmp);
                var item = await _business.GetById(diamondShellIdTmp);

                if (item.Data == null)
                {
                    var diamondShell = new DiamondShell()
                    {
                        Name = Name.Text,
                        Price = Convert.ToDouble(Price.Text),
                        Origin = Origin.Text,
                        Status = (bool)Status.IsChecked!
                    };

                    var result = await _business.Save(diamondShell);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var diamondShell = item.Data as DiamondShell;
                    diamondShell.Name = Name.Text;
                    diamondShell.Origin = Origin.Text;
                    diamondShell.Status = (bool)Status.IsChecked;
                    diamondShell.Price = Convert.ToDouble(Price.Text);


                    var result = await _business.Update(diamondShell);
                    MessageBox.Show(result.Message, "Update");
                }

                DiamondShellId.Text = string.Empty;
                Name.Text = string.Empty;
                Origin.Text = string.Empty;
                Price.Text = string.Empty;
                Status.IsChecked = false;
                this.LoadGrdDiamondShells();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private async void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private async void grdDiamondShell_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as DiamondShell;
                    if (item != null)
                    {
                        var diamondShellResult = await _business.GetById(item.DiamondShellId);

                        if (diamondShellResult.Status > 0 && diamondShellResult.Data != null)
                        {
                            item = diamondShellResult.Data as DiamondShell;
                            DiamondShellId.Text = item.DiamondShellId.ToString();
                            Name.Text = item.Name;
                            Origin.Text = item.Origin;
                            Price.Text = Convert.ToString(item.Price);
                            Status.IsChecked = item.Status;
                        }
                    }
                }
            }
        }
        private async void grdDiamondShell_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            int diamondShellId = int.Parse(btn.CommandParameter.ToString());

            //MessageBox.Show(currencyCode);

            if (diamondShellId != 0)
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _business.DeleteById(diamondShellId);
                    MessageBox.Show($"{result.Message}", "Delete");
                    this.LoadGrdDiamondShells();
                }
            }
        }
    }
}
