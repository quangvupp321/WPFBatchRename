﻿using System;
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
    /// Interaction logic for RemovePatternDialog.xaml
    /// </summary>
    public partial class RemovePatternDialog : Window
    {
        public delegate void DimensionDelegate(string pattern);
        public event DimensionDelegate DimensionChanged = null;


        public RemovePatternDialog(string pattern)
        {
            InitializeComponent();
            fromTb.Text = pattern;
        }

        private void DataChange(object sender, TextChangedEventArgs e)
        {
            string pattern = fromTb.Text;

            DimensionChanged?.Invoke(pattern);
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
