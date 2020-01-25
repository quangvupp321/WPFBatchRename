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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace Project1_BatchRename
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        //static List<string> Presets = new List<string>();
        static public string Preset = "";
        BindingList<Data> data = new BindingList<Data>();
        List<StringOperation> actions = new List<StringOperation>();
        List<StringOperation> really_action = new List<StringOperation>();
        List<string> lines_show = new List<string>();
        int type = 1; // 1 - file, 2 - folder


        public MainWindow()
        {
            InitializeComponent();

        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadActions();
            LoadIcon();
            LoadPresets();
            LoadTypeProcessTheSameName();
        }

        private void CheckBox_Feature_Checked(object sender, RoutedEventArgs e)
        {
            var Object = sender as System.Windows.Controls.CheckBox;
            var bc = new BrushConverter();

            switch (Object.Name)
            {
                case "newcase_cb":
                    {
                        if (newcase_cb.IsChecked == true)
                        {
                            NewCaseArea.Background = (Brush)bc.ConvertFrom("#CF7474");
                        }
                        else
                        {
                            NewCaseArea.Background = (Brush)bc.ConvertFrom("#BFC9C0");
                        }
                        break;
                    }
                case "move_cb":
                    {
                        if (move_cb.IsChecked == true)
                        {
                            MoveArea.Background = (Brush)bc.ConvertFrom("#CF7474");
                        }
                        else
                        {
                            MoveArea.Background = (Brush)bc.ConvertFrom("#BFC9C0");
                        }
                        break;
                    }
                case "remove_cb":
                    {
                        if (remove_cb.IsChecked == true)
                        {
                            RemoveArea.Background = (Brush)bc.ConvertFrom("#CF7474");
                        }
                        else
                        {
                            RemoveArea.Background = (Brush)bc.ConvertFrom("#BFC9C0");
                        }
                        break;
                    }
                case "replace_cb":
                    {
                        if (replace_cb.IsChecked == true)
                        {
                            ReplaceArea.Background = (Brush)bc.ConvertFrom("#CF7474");
                        }
                        else
                        {
                            ReplaceArea.Background = (Brush)bc.ConvertFrom("#BFC9C0");
                        }
                        break;
                    }
                case "trim_cb":
                    {
                        if (trim_cb.IsChecked == true)
                        {
                            TrimArea.Background = (Brush)bc.ConvertFrom("#CF7474");
                        }
                        else
                        {
                            TrimArea.Background = (Brush)bc.ConvertFrom("#BFC9C0");
                        }
                        break;
                    }
                case "newname_cb":
                    {
                        if (newname_cb.IsChecked == true)
                        {
                            NewNameArea.Background = (Brush)bc.ConvertFrom("#CF7474");
                        }
                        else
                        {
                            NewNameArea.Background = (Brush)bc.ConvertFrom("#BFC9C0");
                        }
                        break;
                    }
            }


        }

        private void addFilesBtn_Click(object sender, RoutedEventArgs e)
        {
            type = 1;
            string path = "";

            FolderBrowserDialog diag = new FolderBrowserDialog();
            if (diag.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = diag.SelectedPath;  //selected folder path

            }
            else return;
            batch_tb.DataContext = path;


            DirectoryInfo d = new DirectoryInfo(path);//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles();
            DirectoryInfo[] Folders = d.GetDirectories();

            data.Clear();
            data = new BindingList<Data>();


            foreach (FileInfo file in Files)
            {
                Data temp = new Data();
                temp.fileName = file.ToString();
                temp.newFilename = file.ToString(); ;
                temp.path = path + @"\";
                temp.error = "";
                data.Add(temp);
            }


            lvFiles.ItemsSource = data;

        }

        private void addFoldersBtn_Click(object sender, RoutedEventArgs e)
        {
            type = 2;
            string path = "";

            FolderBrowserDialog diag = new FolderBrowserDialog();
            if (diag.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = diag.SelectedPath;  //selected folder path

            }
            batch_tb.DataContext = path;


            DirectoryInfo d = new DirectoryInfo(path);//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles();
            DirectoryInfo[] Folders = d.GetDirectories();

            data.Clear();
            data = new BindingList<Data>();



            foreach (DirectoryInfo di in Folders)
            {
                Data temp = new Data();
                temp.fileName = di.ToString();
                temp.newFilename = di.ToString();
                temp.path = path + @"\";
                temp.error = "";
                data.Add(temp);
            }

            lvFolders.ItemsSource = data;
        }

        private void NewCaseArea_Click(object sender, RoutedEventArgs e)
        {
            actions[0].Config();
        }

        private void MoveArea_Click(object sender, RoutedEventArgs e)
        {
            actions[1].Config();
            System.Windows.MessageBox.Show(actions[1].Description);
        }

        private void RemoveArea_Click(object sender, RoutedEventArgs e)
        {
            actions[2].Config();
        }

        private void ReplaceArea_Click(object sender, RoutedEventArgs e)
        {
            actions[3].Config();
        }

        private void TrimArea_Click(object sender, RoutedEventArgs e)
        {
            actions[4].Config();
        }

        private void NewNameArea_Click(object sender, RoutedEventArgs e)
        {
            actions[5].Config();
        }

        private void Exec()
        {
            really_action.Clear();

            if (newcase_cb.IsChecked == true)
            {
                really_action.Add(actions[0]);
            }
            if (move_cb.IsChecked == true)
            {
                really_action.Add(actions[1]);
            }
            if (remove_cb.IsChecked == true)
            {
                really_action.Add(actions[2]);
            }
            if (replace_cb.IsChecked == true)
            {
                really_action.Add(actions[3]);
            }
            if (trim_cb.IsChecked == true)
            {
                really_action.Add(actions[4]);
            }
            if (newname_cb.IsChecked == true)
            {
                really_action.Add(actions[5]);
            }

            foreach (StringOperation action in really_action)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    data[i].newFilename = action.Operate(data[i].newFilename);
                }
                ActiveOperation(GetAreaName(action));
            }
        }

        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            Exec();

            if (really_action.Count == 0)
            {
                System.Windows.MessageBox.Show("Please select at least 1 feature and come back later!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (type == 1 && lvFiles.ItemsSource == lines_show)
            {
                System.Windows.MessageBox.Show("Please choose the folder which contains files you need chang name!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (type == 2 && lvFiles.ItemsSource == lines_show)
            {
                System.Windows.MessageBox.Show("Please choose the folder which contains folders you need chang name!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if (CheckSameName() == true)
            {
                System.Windows.MessageBox.Show("dsadsa");
                string typepc = typeCB.SelectedItem.ToString();
                if (typepc == "No change")
                {
                    ProcessNochange();
                }
                else if (typepc == "Suffixes")
                {
                    ProcessSuffixes();
                }
            }

            if (type == 1 & actions.Count != 0)
            {
                foreach (Data item in data)
                {
                    if (File.Exists(item.path + item.fileName))
                    {
                        if (item.fileName != item.newFilename)
                        {
                            System.IO.File.Move(item.path + item.fileName, item.path + item.newFilename);
                        }
                    }
                }
                lvFiles.ItemsSource = data;
            }

            if (type == 2 & actions.Count != 0)
            {
                foreach (Data item in data)
                {
                    if (Directory.Exists(item.path + item.fileName))
                    {
                        if (item.fileName != item.newFilename)
                        {
                            System.IO.Directory.Move(item.path + item.fileName, item.path + item.newFilename);
                        }
                    }
                }
                lvFolders.ItemsSource = data;
            }

            System.Windows.MessageBox.Show("Successful!", "MessageBox", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private string GetAreaName(StringOperation item)
        {
            string result = "";

            if (item.Name == "New Case")
            {
                result = "NewCaseArea";
            }
            else if (item.Name == "Replace")
            {
                result = "ReplaceArea";
            }
            else if (item.Name == "Remove")
            {
                result = "RemoveArea";
            }
            else if (item.Name == "Move")
            {
                result = "MoveArea";
            }
            else if (item.Name == "Unique Name")
            {
                result = "NewNameArea";
            }
            else
            {
                result = "TrimArea";
            }

            return result;
        }

        private void ActiveOperation(string btnName)
        {
            var bc = new BrushConverter();
            System.Windows.Controls.Button item = mainStackPanel.FindName(btnName) as System.Windows.Controls.Button;
            item.Background = (Brush)bc.ConvertFrom("SkyBlue");
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            var screen = new SaveDialog();

            if (screen.ShowDialog() == true)
            {
                System.Windows.MessageBox.Show("Done!");

                string absolute_path = $"{AppDomain.CurrentDomain.BaseDirectory}";
                string path = absolute_path + "Presets\\";

                // write file
                string lines = "";
                if (newcase_cb.IsChecked == true)
                {
                    int type = (int)(actions[0].Args as NewCaseArgs).Type;
                    lines += "0 " + type.ToString() + '\n';
                }
                if (move_cb.IsChecked == true)
                {
                    int type = (int)(actions[1].Args as MoveArgs).Type;
                    lines += "1 " + type.ToString() + '\n';
                }
                if (remove_cb.IsChecked == true)
                {
                    string from = (actions[2].Args as ReplaceArgs).From;
                    lines += "2 " + from + '\n';
                }
                if (replace_cb.IsChecked == true)
                {
                    string from = (actions[3].Args as ReplaceArgs).From;
                    string to = (actions[3].Args as ReplaceArgs).To;
                    lines += "3 " + from + ' ' + to + '\n';
                }
                if (trim_cb.IsChecked == true)
                {
                    lines += "4" + '\n';
                }
                if (newname_cb.IsChecked == true)
                {
                    lines += "5" + '\n';
                }

                string file = Preset + ".txt";
                File.WriteAllText(path + file, lines);
            }
            else
            {

            }

        }

        private void refreshBtn_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void LoadPresets()
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

            presets_cb.ItemsSource = presets;
        }

        private void LoadSavePreset(object sender, SelectionChangedEventArgs e)
        {
            Clear();
            really_action.Clear();

            string absolute_path = $"{AppDomain.CurrentDomain.BaseDirectory}";
            string path = absolute_path + "Presets\\";
            string name = (sender as System.Windows.Controls.ComboBox).SelectedItem.ToString();

            string[] lines = File.ReadAllLines(path + name + ".txt");

            foreach (string line in lines)
            {
                string[] p = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                if (p[0] == "0")
                {
                    TypeCase type = TypeCase.AllCharUpperCase;
                    if (p[1] == "0") type = TypeCase.AllCharUpperCase;
                    else if (p[1] == "1") type = TypeCase.AllCharLowerCase;
                    else type = TypeCase.FirstCharUpperCase;

                    (actions[0].Args as NewCaseArgs).Type = type;
                    newcase_cb.IsChecked = true;
                }
                else if (p[0] == "1")
                {
                    TypeMove type = TypeMove.Start;
                    if (p[1] == "0") type = TypeMove.Start;
                    else if (p[1] == "1") type = TypeMove.End;

                    (actions[1].Args as MoveArgs).Type = type;
                    move_cb.IsChecked = true;
                }
                else if (p[0] == "2")
                {
                    string from = p[1];

                    (actions[2].Args as ReplaceArgs).From = from;
                    (actions[2].Args as ReplaceArgs).To = "";
                    remove_cb.IsChecked = true;
                }
                else if (p[0] == "3")
                {
                    string from = p[1];
                    string to = p[2];

                    (actions[3].Args as ReplaceArgs).From = from;
                    (actions[3].Args as ReplaceArgs).To = to;
                    replace_cb.IsChecked = true;
                }
                else if (p[0] == "4")
                {
                    trim_cb.IsChecked = true;
                }
                else
                {
                    newname_cb.IsChecked = true;
                }
            }
        }

        private void LoadIcon()
        {
            // get Absolute Path
            string absolute_path = $"{AppDomain.CurrentDomain.BaseDirectory}";

            //// set icon
            // Refresh buuton
            var refresh_path = absolute_path + "Icon\\" + "refresh.png";
            var refresh_icon = new BitmapImage(new Uri(refresh_path, UriKind.Absolute));
            refreshIcon.Source = refresh_icon;
            refreshIcon.Width = 15;
            refreshIcon.Height = 15;

            // help button
            var help_path = absolute_path + "Icon\\" + "help.png";
            var help_icon = new BitmapImage(new Uri(help_path, UriKind.Absolute));
            helpIcon.Source = help_icon;
            helpIcon.Width = helpIcon.Height = 15;
            var dropdown_path = absolute_path + "Icon\\" + "dropdown.png";
            var dropdown_icon = new BitmapImage(new Uri(dropdown_path, UriKind.Absolute));
            dropdownIcon.Source = dropdown_icon;
            dropdownIcon.Width = dropdownIcon.Height = 14;

            // Start Icon
            var start_path = absolute_path + "Icon\\" + "start.png";
            var start_icon = new BitmapImage(new Uri(start_path, UriKind.Absolute));
            startIcon.Source = start_icon;
            startIcon.Width = startIcon.Height = 15;

            // Image add
            var add_path = absolute_path + "Icon\\" + "add.png";
            var add_icon = new BitmapImage(new Uri(add_path, UriKind.Absolute));
            image_add_1.Source = image_add_2.Source = image_add_3.Source = image_add_4.Source = image_add_5.Source = image_add_6.Source = add_icon;
            image_add_1.Width = image_add_1.Height = 16;

            // Image X
            var x_path = absolute_path + "Icon\\" + "x.png";
            var x_icon = new BitmapImage(new Uri(x_path, UriKind.Absolute));
            image_x_1.Source = image_x_2.Source = image_x_3.Source = image_x_4.Source = image_x_5.Source = image_x_6.Source = x_icon;


            // Method Icon
            var method_path = absolute_path + "Icon\\" + "method.png";
            var method_icon = new BitmapImage(new Uri(method_path, UriKind.Absolute));
            methodIcon.Source = method_icon; methodIcon.Width = methodIcon.Height = 18;
            dropdownIcon1.Source = dropdown_icon; dropdownIcon1.Width = dropdownIcon1.Height = 13;

            // Clear Icon
            var clear1_path = absolute_path + "Icon\\" + "clear1.png";
            var clear1_icon = new BitmapImage(new Uri(clear1_path, UriKind.Absolute));
            var clear2_path = absolute_path + "Icon\\" + "clear2.png";
            var clear2_icon = new BitmapImage(new Uri(clear2_path, UriKind.Absolute));
            clear1Icon.Source = clear1_icon; clear1Icon.Width = clear1Icon.Height = 15;
            clear2Icon.Source = clear2_icon; clear2Icon.Width = clear2Icon.Height = 15;

            // Save Icon
            var save_path = absolute_path + "Icon\\" + "save.png";
            var save_icon = new BitmapImage(new Uri(save_path, UriKind.Absolute));
            saveIcon.Source = save_icon; saveIcon.Width = saveIcon.Height = 18;

            // Add files 
            var files_path = absolute_path + "Icon\\" + "icon_add.png";
            var files_icon = new BitmapImage(new Uri(files_path, UriKind.Absolute));
            addfilesIcon.Source = files_icon; addFoldersIcon.Source = files_icon;
            addfilesIcon.Height = addfilesIcon.Width = 18; addFoldersIcon.Height = addFoldersIcon.Width = 18;

            // Type icon
            var type_path = absolute_path + "Icon\\" + "type.png";
            var type_icon = new BitmapImage(new Uri(type_path, UriKind.Absolute));
            typeIcon.Source = type_icon;
            typeIcon.Width = typeIcon.Height = 12;
        }

        private void LoadActions()
        {
            // actions
            actions.Add(new NewCaseOperation
            {
                Args = new NewCaseArgs
                {
                    Type = TypeCase.AllCharUpperCase
                }
            });

            actions.Add(new MoveOperation
            {
                Args = new MoveArgs
                {
                    Type = TypeMove.Start
                }
            });

            actions.Add(new RemoveOperation
            {
                Args = new ReplaceArgs
                {
                    From = "Pattern",
                    To = ""
                }
            });

            actions.Add(new ReplaceOperation
            {
                Args = new ReplaceArgs
                {
                    From = "From Patterm",
                    To = "To Pattern"
                }
            });


            actions.Add(new NormalizeOperation
            {
                Args = new NormalizeArgs()
            });

            actions.Add(new UniqueNameOperation
            {
                Args = new UniqueNameArgs()
            });

            for (int i = 0; i < 23; i++)
            {
                lines_show.Add(" ");
            }
            lvFiles.ItemsSource = lines_show;
            lvFolders.ItemsSource = lines_show;
        }

        private void LoadTypeProcessTheSameName()
        {
            List<string> temp = new List<string>();
            temp.Add("No change");
            temp.Add("Suffixes");
            typeCB.ItemsSource = temp;
            typeCB.SelectedIndex = 0;
        }

        private void Clear()
        {
            lvFiles.ItemsSource = lines_show;
            lvFolders.ItemsSource = lines_show;


            // uncheck feature
            newcase_cb.IsChecked = false;
            move_cb.IsChecked = false;
            remove_cb.IsChecked = false;
            replace_cb.IsChecked = false;
            trim_cb.IsChecked = false;
            newname_cb.IsChecked = false;

            // reset actions
            (actions[0].Args as NewCaseArgs).Type = TypeCase.AllCharUpperCase;
            (actions[1].Args as MoveArgs).Type = TypeMove.Start;
            (actions[2].Args as ReplaceArgs).From = "";
            (actions[3].Args as ReplaceArgs).From = ""; (actions[3].Args as ReplaceArgs).To = "";

        }

        private bool CheckSameName()
        {
            bool flag = false;

            for (int i = 0; i < data.Count - 1; i++)
            {
                for (int j = i + 1; j < data.Count; j++)
                {
                    if (data[i].newFilename == data[j].newFilename)
                    {
                        return true;
                    }
                }
            }

            return flag;
        }

        private void ProcessNochange()
        {
            for (int i = 0; i < data.Count - 1; i++)
            {
                for (int j = i + 1; j < data.Count; j++)
                {
                    if (data[i].newFilename == data[j].newFilename)
                    {
                        data[i].newFilename = data[i].fileName;
                    }
                }

            }
        }

        private void ProcessSuffixes()
        {
            while (CheckSameName())
            {
                // duyệt lần 1
                for (int i = 0; i < data.Count; i++)
                {
                    int count = 1;
                    for (int j = i + 1; j < data.Count; j++)
                    {
                        if (data[i].newFilename == data[j].newFilename)
                        {
                            int index = data[j].newFilename.LastIndexOf('.');
                            string tail = Utils.GetTailFile(data[j].newFilename);
                            string temp = data[j].newFilename.Remove(index);
                            string temp1 = temp + (count++).ToString();
                            data[j].newFilename = temp1 + "." + tail;
                        }
                    }
                }

                // duyệt lần 2
                for (int i = 0; i < data.Count; i++)
                {
                    for (int j = 0; j < data.Count; j++)
                    {
                        if (data[i].fileName == data[j].newFilename && i != j)
                        {
                            string temp = data[i].newFilename;
                            data[i].newFilename = data[j].newFilename;
                            data[j].newFilename = temp;
                        }
                    }
                }
            }
        }

        private void helpBtn_Click(object sender, RoutedEventArgs e)
        {
            var screen = new HelpDiaglog();

            if(screen.ShowDialog() == true)
            {

            }
            else
            {

            }
        }
    }


}
