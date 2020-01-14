using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
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

            Binding bindingE = new Binding(){Source = this,Path = new PropertyPath(nameof(E)),Mode = BindingMode.TwoWay};
            TextBox textBox1 = new TextBox() { Style = viewModels.MainWindow.FindResource("Analitic_Simplex") as Style ,
                Width = WidhtTextBox,
                Margin = new Thickness(0,0,10,0)};
            textBox1.SetBinding(TextBox.TextProperty, bindingE);
            DockPanel dockPanel1=new DockPanel()
            {
                Children =
                {
                    new Label(){Content = "Коэффициент E: "},
                    textBox1,
                    new FormulaControl(){Formula = $"x_{{1}}...x_{countX} > 0",FontSize = FormulaFontSize},
                }
            };

            stackPanelAnaliticForms.Children.Add(dockPanel1);

            for (int i = 0; i < countX; i++)
            {
                var generator = listIElements[i] is Generator ? listIElements[i] as Generator : throw new Exception("EventAndMethodViewModels is NULL");
                Binding binding=new Binding(){Source = listIElements[i],Path = new PropertyPath(nameof(generator.PMax))};
                TextBox textBox = new TextBox()
                {
                    Style = viewModels.MainWindow.FindResource("Analitic_Simplex") as Style,
                    Width = WidhtTextBox,
                    FontSize = FormulaFontSize
                };
                textBox.TextChanged += TextBox_TextChanged;
                textBox.GotFocus += TextBox_GotFocus_LostFocus;
                textBox.LostFocus += TextBox_GotFocus_LostFocus;
                textBox.MouseLeave += TextBox_MouseLeave;
                textBox.MouseEnter += TextBox_MouseEnter;
                textBox.SetBinding(TextBox.TextProperty, binding);

                FormulaControl formula = new FormulaControl()
                {
                    Formula = $"x_{i + 1} < ", FontSize = FormulaFontSize,Scale = 18 ,
                    HorizontalAlignment = HorizontalAlignment.Left,HorizontalContentAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,VerticalContentAlignment = VerticalAlignment.Bottom
                };
                DockPanel dockPanel = new DockPanel(){Children = {formula,textBox}};
                stackPanelAnaliticForms.Children.Add(dockPanel);
            }

            DockPanel dockPanelSumP = new DockPanel();
            for (int i = 0; i < countX; i++)
            {
                FormulaControl formula = new FormulaControl()
                {
                    FontSize = FormulaFontSize,
                    Scale = 18,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    HorizontalContentAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Bottom
                };
                if (i == countX - 1) formula.Formula += $"x_{i + 1} =";
                else formula.Formula += $"x_{i + 1} + ";
                dockPanelSumP.Children.Add(formula);
            }
            Binding bindingSumPmax = new Binding(){Source = this,Path = new PropertyPath(nameof(this.SumPmax)),
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged};
            Label textBoxSumPmax = new Label()
            {
                Style = viewModels.MainWindow.FindResource("Analitic_Simplex_TextBlock") as Style,
                Width = WidhtTextBox,
                FontSize = FormulaFontSize
            };
            textBoxSumPmax.SetBinding(Label.ContentProperty, bindingSumPmax);
            dockPanelSumP.Children.Add(textBoxSumPmax);

            stackPanelAnaliticForms.Children.Add(dockPanelSumP);

            DockPanel dockPanelTAndPnag=new DockPanel();
            for (int i = 0; i < countX; i++)
            {
                var generator = listIElements[i] is Generator ? listIElements[i] as Generator : throw new Exception("EventAndMethodViewModels is NULL");
                FormulaControl formula = new FormulaControl()
                {
                    FontSize = FormulaFontSize,
                    Scale = 18,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    HorizontalContentAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Bottom
                };
                if (generator.T != 0)
                {
                    if (i == countX - 1) formula.Formula += $"{generator.T / 1000}x_{i + 1} =";
                    else formula.Formula += $"{generator.T / 1000}x_{i + 1} + ";
                    dockPanelTAndPnag.Children.Add(formula);
                }
                else
                {
                    if (i == countX - 1) formula.Formula += $"{0}x_{i + 1} =";
                    else formula.Formula += $"{0}x_{i + 1} + ";
                    dockPanelTAndPnag.Children.Add(formula);
                }
            }

            Binding bindingTAndPnag = new Binding()
            {
                Source = this,
                Path = new PropertyPath(nameof(this.MaxTPmax)),
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            Label textBoxTAndPnag = new Label()
            {
                Style = viewModels.MainWindow.FindResource("Analitic_Simplex_TextBlock") as Style,
                Width = WidhtTextBox,
                FontSize = FormulaFontSize
            };
            textBoxTAndPnag.SetBinding(Label.ContentProperty, bindingTAndPnag);
            dockPanelTAndPnag.Children.Add(textBoxTAndPnag);

            stackPanelAnaliticForms.Children.Add(dockPanelTAndPnag);
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
