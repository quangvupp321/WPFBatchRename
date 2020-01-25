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
    /// Interaction logic for NewCaseDialog.xaml
    /// </summary>
    public partial class NewCaseDialog : Window
    {
        public delegate void DimensionDelegate(TypeCase type);
        public event DimensionDelegate DimensionChanged = null;

        public NewCaseDialog(TypeCase type)
        {
            InitializeComponent();
            if (type == TypeCase.AllCharUpperCase)
            {
                allcharupper.IsChecked = true;
            }
            else if (type == TypeCase.AllCharLowerCase)
            {
                allcharlower.IsChecked = true;
            }
            else
            {
                firstcharupper.IsChecked = true;
            }
        }

        private void ChangeTypeNewCase(object sender, RoutedEventArgs e)
        {
            RadioButton rdbtn = sender as RadioButton;
            string name = rdbtn.Name;
            TypeCase type = TypeCase.AllCharUpperCase;

            if (name == "allcharupper")
            {
                type = TypeCase.AllCharUpperCase;
            }
            else if (name == "allcharlower")
            {
                type = TypeCase.AllCharLowerCase;
            }
            else
            {
                type = TypeCase.FirstCharUpperCase;
            }


            DimensionChanged?.Invoke(type);
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
    }
}
