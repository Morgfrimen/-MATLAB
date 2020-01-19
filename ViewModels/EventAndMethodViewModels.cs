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

            ComboBox comboBoxIElements = new ComboBox();
            foreach (var element in listIElements)
            {
                comboBoxIElements.Items.Add(new TextBlock(){Text = element.GetName()});
            }
            comboBoxIElements.SelectedIndex = 0;

            stackPanelAnaliticForms.Children.Add(comboBoxIElements);

            //TODO: Генерация формы под опделенные IElements

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
