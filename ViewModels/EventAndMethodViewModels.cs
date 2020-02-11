using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using JetBrains.Annotations;
using WpfMath;
using WpfMath.Controls;
using ЧисленныМетоды.Models.SinplexMethod_GraphicInput;

namespace ЧисленныМетоды.ViewModels
{
    //В качестве идеи - добавить в аналитиику новые компоненты с перебиндинком с выбранному в комбо боксе элементу
    //и потом уже ограничения в онлайн режиме городить
    //TODO: перепилить и оптимизировать код для чтения
    public class EventAndMethodViewModels : INotifyPropertyChanged
    {
        private const double FormulaFontSize = 14;
        private const double WidhtTextBox = 60;

        private ЧисленныМетоды.ViewModels.ViewModels viewModels;
        private List<StackPanel> _analiticView = new List<StackPanel>();
        private float _e= 0.12f;
        private List<IElements> listIElements;

        public float E
        {
            get => _e;
            set => _e = value;
        }

        private float? sumPmax = 0f;

        public float? SumPmax
        {
            get => sumPmax;
            set
            {
                sumPmax = value;
                OnPropertyChanged(nameof(SumPmax));
            }
        }

        private float? maxTPmax = 0f;

        public float? MaxTPmax
        {
            get => maxTPmax;
            set
            {
                sumPmax = value;
                OnPropertyChanged(nameof(MaxTPmax));
            }
        }

        public EventAndMethodViewModels()
        {
            viewModels = ЧисленныМетоды.ViewModels.ViewModels.ViewModel; 
            UpdateListElements();
        }

        /// <summary>
        /// Генерация формы для аналитического ввода данных симплекс-метода
        /// </summary>
        internal void Create_Analiticx_Simplex_Form()
        {
            UpdateListElements();
            StackPanel stackPanelAnaliticForms = viewModels.MainWindow.Analitic_StackPanenel_SimplexMethod;
            var countX = viewModels.CountX;
            var listIElements = viewModels.IElementses;

            stackPanelAnaliticForms.Children.RemoveRange(1,stackPanelAnaliticForms.Children.Count-1);

            ComboBox comboBoxIElements = new ComboBox();
            foreach (var element in listIElements)
            {
                comboBoxIElements.Items.Add(new TextBlock(){Text = element.GetName()});
            }
            comboBoxIElements.SelectedIndex = 0;

            comboBoxIElements.SelectionChanged += ComboBoxIElements_SelectionChanged;

            stackPanelAnaliticForms.Children.Add(comboBoxIElements);

            _analiticView.Clear();

            //TODO: сделать по-красивше аналитическое View
            for (int elemIndex = 0; elemIndex < this.listIElements.Count; elemIndex++)
            {
                StackPanel stackPanel = new StackPanel();
                if (this.listIElements[elemIndex] is Generator)
                {
                    Generator generator = (listIElements[elemIndex] as Generator);

                    TextBlock textBlock = new TextBlock(){Text = nameof(generator.PMax), 
                        Style = (Style)viewModels.MainWindow.FindResource("Analitic_Simplex_TextBlock") };
                    TextBox textBox = new TextBox() {Style = (Style)viewModels.MainWindow.FindResource("Analitic_Simplex") };

                    Binding bindingPmax = new Binding(){Source = generator,Path = new PropertyPath(nameof(generator.PMax)) };
                    textBox.SetBinding(TextBox.TextProperty, bindingPmax);

                    TextBlock textBlockK0 = new TextBlock()
                    {
                        Text = nameof(generator.K0),
                        Style = (Style)viewModels.MainWindow.FindResource("Analitic_Simplex_TextBlock")
                    };
                    TextBox textBoxK0 = new TextBox() { Style = (Style)viewModels.MainWindow.FindResource("Analitic_Simplex") };

                    Binding bindingK0 = new Binding() { Source = generator, Path = new PropertyPath(nameof(generator.K0)) };
                    textBoxK0.SetBinding(TextBox.TextProperty, bindingK0);

                    TextBlock textBlockT = new TextBlock()
                    {
                        Text = nameof(generator.T),
                        Style = (Style)viewModels.MainWindow.FindResource("Analitic_Simplex_TextBlock")
                    };
                    TextBox textBoxT = new TextBox() { Style = (Style)viewModels.MainWindow.FindResource("Analitic_Simplex") };

                    Binding bindingT = new Binding() { Source = generator, Path = new PropertyPath(nameof(generator.T)) };
                    textBoxT.SetBinding(TextBox.TextProperty, bindingT);

                    TextBlock textBlockY = new TextBlock()
                    {
                        Text = nameof(generator.Y),
                        Style = (Style)viewModels.MainWindow.FindResource("Analitic_Simplex_TextBlock")
                    };
                    TextBox textBoxY = new TextBox() { Style = (Style)viewModels.MainWindow.FindResource("Analitic_Simplex") };

                    Binding bindingY = new Binding() { Source = generator, Path = new PropertyPath(nameof(generator.Y)) };
                    textBoxY.SetBinding(TextBox.TextProperty, bindingY);

                    DockPanel stackPanelHorizontal1 = new DockPanel()
                    {
                        Children = {textBlock,textBox }
                    };

                    DockPanel stackPanelHorizontal2 = new DockPanel()
                    { 
                        Children = { textBlockK0, textBoxK0 }
                    };

                    DockPanel stackPanelHorizontal3 = new DockPanel()
                    {
                        Children = { textBlockT, textBoxT }
                    };

                    DockPanel stackPanelHorizontal4 = new DockPanel()
                    {
                        Children = { textBlockY, textBoxY }
                    };

                    stackPanel.Children.Add(stackPanelHorizontal1);
                    stackPanel.Children.Add(stackPanelHorizontal2);
                    stackPanel.Children.Add(stackPanelHorizontal3);
                    stackPanel.Children.Add(stackPanelHorizontal4);

                    _analiticView.Add(stackPanel);
                }
                else
                {
                    Nagruzca nagruzca = (listIElements[elemIndex] as Nagruzca);

                    TextBlock textBlock = new TextBlock()
                    {
                        Text = nameof(nagruzca.PMax),
                        Style = (Style)viewModels.MainWindow.FindResource("Analitic_Simplex_TextBlock")
                    };
                    TextBox textBox = new TextBox() { Style = (Style)viewModels.MainWindow.FindResource("Analitic_Simplex") };

                    Binding bindingPmax = new Binding() { Source = nagruzca, Path = new PropertyPath(nameof(nagruzca.PMax)) };
                    textBox.SetBinding(TextBox.TextProperty, bindingPmax);

                    TextBlock textBlockT = new TextBlock()
                    {
                        Text = nameof(nagruzca.T),
                        Style = (Style)viewModels.MainWindow.FindResource("Analitic_Simplex_TextBlock")
                    };
                    TextBox textBoxT = new TextBox() { Style = (Style)viewModels.MainWindow.FindResource("Analitic_Simplex") };

                    Binding bindingT = new Binding() { Source = nagruzca, Path = new PropertyPath(nameof(nagruzca.T)) };
                    textBoxT.SetBinding(TextBox.TextProperty, bindingT);

                    DockPanel stackPanelHorizontal1 = new DockPanel()
                    {
                        Children = { textBlock, textBox }
                    };

                    StackPanel stackPanelHorizontal2 = new StackPanel()
                    {
                        Orientation = Orientation.Horizontal,
                        Children = { textBlockT, textBoxT }
                    };

                    stackPanel.Children.Add(stackPanelHorizontal1);
                    stackPanel.Children.Add(stackPanelHorizontal2);

                    _analiticView.Add(stackPanel);
                }
            }
            foreach (var element in _analiticView)
            {
                element.Visibility = Visibility.Collapsed;
                stackPanelAnaliticForms.Children.Add(element);
            }

        }

        private void ComboBoxIElements_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            combo.IsEditable = false;
            StackPanel stackPanelAnaliticForms = viewModels.MainWindow.Analitic_StackPanenel_SimplexMethod;

            _analiticView[combo.SelectedIndex].Visibility = Visibility.Visible;

            stackPanelAnaliticForms.Children.RemoveRange(2, stackPanelAnaliticForms.Children.Count);
            stackPanelAnaliticForms.Children.Add(_analiticView[combo.SelectedIndex]);
        }

        private void TextBox_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            UpdateListElements();
            (sender as TextBox).Focusable = true;
        }

        private void TextBox_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            UpdateListElements();
            (sender as TextBox).Focusable = false;
        }

        private void TextBox_GotFocus_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdateListElements();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateListElements();


        }

        public void UpdateListElements()
        {
            listIElements = viewModels.IElementses;
            SumPmax = 0f;  //TODO:Подумать лучше
            for (int i = 0; i < listIElements.Count - 1; i++)
            {
                var generator = listIElements[i] is Generator ? listIElements[i] as Generator : throw new Exception("EventAndMethodViewModels is NULL");
                SumPmax += generator.PMax;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
