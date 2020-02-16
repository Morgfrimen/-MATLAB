using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ЧисленныМетоды
{
    /// <summary>
    /// Логика взаимодействия для AddZFuction.xaml
    /// </summary>
    public partial class AddZFuction : Window
    {
        private byte? countX = null;
        public AddZFuction(byte? countX)
        {
            InitializeComponent();
            this.countX = countX;
            DockPanelZFuction.Children.Clear();
            for (int i = 0; i < countX; i++)
            {
               TextBox z = new TextBox() {Height = 25, Width = 35, Margin = new Thickness(5,0,0,5), Text = "0", HorizontalAlignment = HorizontalAlignment.Left};
               DockPanelZFuction.Children.Add(z);
            }
            

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (countX != null)
            {
                double[] z = new double[(int)countX];
                for (int i = 0; i < DockPanelZFuction.Children.Count; i++)
                {
                    TextBox zBox = DockPanelZFuction.Children[i] as TextBox;
                    z[i] = Double.Parse(zBox.Text);
                }
            }
        }

        private void exitBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
