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

namespace Project1_BatchRename
{
    /// <summary>
    /// Interaction logic for ReplaceDialog.xaml
    /// </summary>
    public partial class ReplaceDialog : Window
    {
        public delegate void DimensionDelegate(string from, string to);
        public event DimensionDelegate DimensionChanged = null;

        public ReplaceDialog(string from, string to)
        {
            InitializeComponent();
            fromTb.Text = from;
            toTb.Text = to;
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void DataChange(object sender, TextChangedEventArgs e)
        {
            string from = fromTb.Text;
            string to = toTb.Text;

            DimensionChanged?.Invoke(from, to);
        }
    }
}
