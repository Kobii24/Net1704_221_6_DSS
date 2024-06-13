using DSS.Business.Category;
using DSS.Data.Models;
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

namespace DSS.Wpf1.UI
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
                        //OrderId = tmp,
                        //CustomerId = int.Parse(CustomerId.Text),
                        //OrderDate = DateOnly.Parse(OrderDate.Text),
                        //TotalAmount = decimal.Parse(TotalAmount.Text),
                        //PaymentMethod = PaymenMethod.Text,
                        //PaymentStatus = PaymentStatus.Text
                        Name = Name.Text,
                        Price = int.Parse(Price.Text),
                        Quantity = int.Parse(Quantity.Text),    
                        Title = Title.Text,
                        Status = (bool)Status.IsChecked!
                    };

                    var result = await _business.Save(extraDiamond);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    //var order = item.Data as Order;
                    ////currency.CurrencyCode = txtCurrencyCode.Text;
                    //Order. = txtCurrencyName.Text;
                    //Order.NationCode = txtNationCode.Text;
                    //Order.IsActive = chkIsActive.IsChecked;

                    //var result = await _business.Update(currency);
                    //MessageBox.Show(result.Message, "Update");
                }

                //txtCurrencyCode.Text = string.Empty;
                //txtCurrencyName.Text = string.Empty;
                //txtNationCode.Text = string.Empty;
                //chkIsActive.IsChecked = false;
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
            //MessageBox.Show("Double Click on Grid");
            //DataGrid grd = sender as DataGrid;
            //if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            //{
            //    var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
            //    if (row != null)
            //    {
            //        var item = row.Item as ExtraDiamond;
            //        if (item != null)
            //        {
            //            var extraDiamondResult = await _business.GetById(Convert.ToInt32(item.ExtraDiamondId));

            //            if (extraDiamondResult.Status > 0 && extraDiamondResult.Data != null)
            //            {
            //                item = extraDiamondResult.Data as ExtraDiamond;
            //                ExtraDiamondId.Text = Convert.ToString(item.ExtraDiamondId);
            //                Name.Text = item.Name;
            //                Title.Text = item.Title;
            //                Price.Text = Convert.ToString(item.Price);
            //                Quantity.Text = Convert.ToString(item.Quantity);
            //                Status.IsChecked = item.Status;
            //            }
            //        }
            //    }
            //}
        }

        private async void grdExtraDiamond_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            //Button btn = (Button)sender;

            //long extraDiamondId = (long)btn.CommandParameter;

            ////MessageBox.Show(currencyCode);

            //if (extraDiamondId != 0)
            //{
            //    if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            //    {
            //        var result = await _business.DeleteById(Convert.ToInt32(extraDiamondId));
            //        MessageBox.Show($"{result.Message}", "Delete");
            //        this.LoadGrdExtraDiamonds();
            //    }
            //}
        }
    }
}
