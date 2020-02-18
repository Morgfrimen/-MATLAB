using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace ЧисленныМетоды
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog myDialog = new SaveFileDialog();
            myDialog.Filter = "Текстовой документ(*.docx;*.txt)|*.docx;*.txt" + "|Все файлы (*.*)|*.* ";
            myDialog.InitialDirectory = FileName.Text;
            if (CheckTXT.IsChecked == true)
            {
                myDialog.DefaultExt = ".txt";
            }
            else
            {
                myDialog.DefaultExt = ".docx";
            }
            if (myDialog.ShowDialog() == true)
            {
                FileName.Text = myDialog.FileName;
            }
        }
    }
}
