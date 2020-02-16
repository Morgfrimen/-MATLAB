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
        private ViewModels.ViewModels viewModels;

        internal StackPanel stackPanel2 = new StackPanel(){Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Top};

        private List<TextBox> listTextBlocks = new List<TextBox>();

        private byte? countX = null;
        public AddZFuction(byte? countX,ViewModels.ViewModels vm)
        {
            InitializeComponent();
            viewModels = vm;
            this.countX = countX;
            DockPanelZFuction.Children.RemoveRange(2,2);
            listTextBlocks.Clear();
            StackPanel stackPanel1 = new StackPanel() {Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Top};
            for (int i = 0; i < countX; i++)
            {
                TextBlock z = new TextBlock() { Height = 25, Width = 35, 
                    Margin = new Thickness(5, 0, 0, 5), Text = $"X{i+1}", 
                    HorizontalAlignment = HorizontalAlignment.Left
                };
                stackPanel1.Children.Add(z);
            }
            DockPanelZFuction.Children.Add(stackPanel1);
            DockPanel.SetDock(stackPanel1,Dock.Top);
            for (int i = 0; i < countX; i++)
            {
               TextBox z = new TextBox() {Height = 25, Width = 35, Margin = new Thickness(5,0,0,5), Text = "0", HorizontalAlignment = HorizontalAlignment.Left};
               listTextBlocks.Add(z);
               stackPanel2.Children.Add(z);
            }
            DockPanelZFuction.Children.Add(stackPanel2);
            DockPanel.SetDock(stackPanel2,Dock.Top);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (countX != null)
            {
                double[] z = new double[(int)countX]; 
                for (int i = 0; i < stackPanel2.Children.Count; i++)
                {
                    z[i] = Double.Parse(listTextBlocks[i].Text);
                } 
                viewModels.ZArrays = z;  
            }
        }
        private void Window_Deactivated(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch
            {
                this.Hide();
                this.Close();
            }
        }

        private void CloseButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
