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
    /// Interaction logic for HelpDiaglog.xaml
    /// </summary>
    public partial class HelpDiaglog : Window
    {
        public HelpDiaglog()
        {
            InitializeComponent();
            header.Text = "Danh sách các chức năng";
            cn1.Text =
                        "\tBước 1: Chọn folder chứa những file ta muốn thay thế.\n"
                        + "\tBước 2: Tích vào Replace, sau đó điền vào tên cần thay thế và kết quả tên mong muốn. Bấm ok.\n"
                        + "\tBước 3: Bấm START BATCH.";
            cn2.Text =
                        "\tBước 1:Chọn file/ folder cần đổi kiểu chữ.\n"
                        + "\tBước 2: Chọn cách đặt tên trong cửa sổ hiện lên khi tích vào New Case.\n"
                        + "\tBước 3: Bấm vào START BATCH.";
            cn3.Text =
                        "\tBước 1:Chọn folder chứa những file mà ta cần chuẩn hoá.\n"
                        + "\tBước 2:Tích vào tính năng Trim.\n"
                        + "\tBước 3: Bấm vào START BATCH.";
            cn4.Text =
                        "\tBước 1: Chọn folder chứa những file ta cần move.\n"
                        + "\tBước 2:Tích vào tính năng Move, sau đó chọn kiểu move(Before hoặc Afer).\n"
                        + "\tBước 3: Bấm vào START BATCH.";
            cn5.Text =
                        "\tBước 1: Chọn folder chứa những file ta cần đổi tên thành duy nhất.\n"
                        + "\tBước 2: Tích vào tính năng New Name.\n"
                        + "\tBước 3: Bấm vào START BATCH.";
            cn6.Text =
                        "\tBước 1: Chọn folđer chứa file ta muốn xoá một phần tên.\n"
                        + "\tBước 2: Tích vào Remove Pattern, sau đó điền vào tên cần xoá.\n"
                        + "\tBước 3: Bấm vào START BATCH.";
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
