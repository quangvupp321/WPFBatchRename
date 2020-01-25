using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Project1_BatchRename
{
    /// <summary>
    /// Interaction logic for SaveDialog.xaml
    /// </summary>
    public partial class SaveDialog : Window
    {
        public SaveDialog()
        {
            InitializeComponent();
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Preset = key.Text;

            this.DialogResult = true;
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string absolute_path = $"{AppDomain.CurrentDomain.BaseDirectory}";
            string path = absolute_path + "Presets\\";

            DirectoryInfo d = new DirectoryInfo(path);//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles();

            BindingList<string> presets = new BindingList<string>();

            foreach (FileInfo file in Files)
            {
                string tail = "." + Utils.GetTailFile(file.Name);
                string name = file.Name.Replace(tail, "");
                presets.Add(name);
            }

            string str = "preset_1";

            int count = 2;
            for (int i = 0; i < presets.Count; i++)
            {
                if (str == presets[i])
                {
                    int index = str.LastIndexOf('_');
                    str = str.Remove(index) + '_' + (count++).ToString();
                }
            }

            key.Text = str;
        }
    }
}
