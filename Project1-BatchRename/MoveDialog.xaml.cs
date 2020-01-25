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
    /// Interaction logic for MoveDialog.xaml
    /// </summary>
    public partial class MoveDialog : Window
    {
        public delegate void DimensionDelegate(TypeMove type);
        public event DimensionDelegate DimensionChanged = null;

        public MoveDialog(TypeMove type)
        {
            InitializeComponent();

            if (type == TypeMove.Start)
            {
                before.IsChecked = true;
            }
            else
            {
                after.IsChecked = true;
            }
        }

        private void ChangeTypeMove(object sender, RoutedEventArgs e)
        {
            RadioButton rdbtn = sender as RadioButton;
            string name = rdbtn.Name;
            TypeMove type = TypeMove.Start;

            if (name == "before")
            {
                type = TypeMove.Start;
            }
            else
            {
                type = TypeMove.End;
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
