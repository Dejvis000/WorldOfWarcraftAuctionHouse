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
using WoWAuctionHouse.Models;
using WoWAuctionHouse.ViewModel;

namespace WoWAuctionHouse.View
{
    /// <summary>
    /// Logika interakcji dla klasy AuctionsWindow.xaml
    /// </summary>
    public partial class AuctionsWindow : Window
    {
        public AuctionsWindowModel AuctionsWindowModel { get; set; }

        public AuctionsWindow(ItemModel selectedItem)
        {
            InitializeComponent();

            AuctionsWindowModel = this.DataContext as AuctionsWindowModel;
            AuctionsWindowModel.AuctionsWindow = this;
            AuctionsWindowModel.ItemId = selectedItem.Id;
        }
    }
}
