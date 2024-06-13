using DSS.Business.Category;
using DSS.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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
using System.Windows.Shapes;

namespace DSS.Wpf
{
    /// <summary>
    /// Interaction logic for wExtraDiamond.xaml
    /// </summary>
    public partial class wExtraDiamond : Window
    {
        private readonly ExtraDiamondBusiness _business;
        public wExtraDiamond()
        {
            InitializeComponent();
            this._business ??= new ExtraDiamondBusiness();
            LoadGrdExtraDiamonds();
        }

        private async void LoadGrdExtraDiamonds()
        {
            var result = await _business.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                grdExtraDiamond.ItemsSource = result.Data as List<ExtraDiamond>;
            }
            else
            {
                grdExtraDiamond.ItemsSource = new List<ExtraDiamond>();
            }
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int extraDiamondIdTmp = -1;
                int.TryParse(ExtraDiamondId.Text, out extraDiamondIdTmp);
                var item = await _business.GetById(extraDiamondIdTmp);

                if (item.Data == null)
                {
                    var extraDiamond = new ExtraDiamond()
                    {
                        Name = Name.Text,
                        Price = Convert.ToDecimal(Price.Text),
                        Quantity = int.Parse(Quantity.Text),
                        Title = Title.Text,
                        Status = (bool)Status.IsChecked!
                    };

                    var result = await _business.Save(extraDiamond);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var extraDiamond = item.Data as ExtraDiamond;
                    extraDiamond.Name = Name.Text;
                    extraDiamond.Title = Title.Text;
                    extraDiamond.Status = (bool)Status.IsChecked;
                    extraDiamond.Quantity = int.Parse(Quantity.Text);
                    extraDiamond.Price = Convert.ToDecimal(Price.Text);


                    var result = await _business.Update(extraDiamond);
                    MessageBox.Show(result.Message, "Update");
                }

                ExtraDiamondId.Text = string.Empty;
                Name.Text = string.Empty;
                Title.Text = string.Empty;
                Quantity.Text = string.Empty;
                Price.Text = string.Empty;
                Status.IsChecked = false;
                this.LoadGrdExtraDiamonds();
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

        private async void grdExtraDiamond_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Double Click on Grid");
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as ExtraDiamond;
                    if (item != null)
                    {
                        //int extraDiamondIdTmp = -1;
                        //int.TryParse(item.ExtraDiamondId.t, extraDiamondIdTmp)
                        var extraDiamondResult = await _business.GetById(item.ExtraDiamondId);

                        if (extraDiamondResult.Status > 0 && extraDiamondResult.Data != null)
                        {
                            item = extraDiamondResult.Data as ExtraDiamond;
                            ExtraDiamondId.Text = item.ExtraDiamondId.ToString();
                            Name.Text = item.Name;
                            Title.Text = item.Title;
                            Price.Text = Convert.ToString(item.Price);
                            Quantity.Text = Convert.ToString(item.Quantity);
                            Status.IsChecked = item.Status;
                        }
                    }
                }
            }
        }

        private async void grdExtraDiamond_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            int extraDiamondId = int.Parse(btn.CommandParameter.ToString());

            //MessageBox.Show(currencyCode);

            if (extraDiamondId != 0)
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _business.DeleteById(extraDiamondId);
                    MessageBox.Show($"{result.Message}", "Delete");
                    this.LoadGrdExtraDiamonds();
                }
            }
        }


    }
}
