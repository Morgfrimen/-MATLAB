using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using ЧисленныМетоды.Models.SimplexNethod_AnalitycInput;
using ЧисленныМетоды.Models.SinplexMethod_GraphicInput;

namespace ЧисленныМетоды.ViewModels
{
    //TODO: Перенести события и методы в другой файл - так я думаю будет лучше и не каша
    public class ViewModels : INotifyPropertyChanged
    {
        private EventAndMethodViewModels eventAndMethodViewModels;

        private const byte MaxCountX = 10;
        private const double ChangeSizeSimplexCanvas = 100;

        public static ViewModels ViewModel =>App.Current.MainWindow.DataContext as ViewModels;
        private ResourceDictionary resourceDictionary;

        private ICollection resourseCollection;
        

        private MainWindow mainWindow;
        private static Config config = Config.Default;
        private Analizate analizate;

        private Canvas simplexCanvas;

        private byte? _countX;
        public ViewModels()
        {
            resourceDictionary = App.Current.Resources;
            CultureInfo cultureInfo = CultureInfo.CurrentCulture;
            switch (cultureInfo.ToString().ToLower())
            {
                case "ru-ru":
                    resourceDictionary.Source = new Uri($"Style/LanguageRu.xaml",UriKind.Relative);
                    break;
                case "es-es":
                    resourceDictionary.Source = new Uri($"Style/LanguageEn.xaml", UriKind.Relative);
                    break;
            }
            mainWindow = App.Current.MainWindow as MainWindow;
            analizate=new Analizate();
            mainWindow.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            simplexCanvas.Height = mainWindow.ActualHeight - ChangeSizeSimplexCanvas;
            for (int lineIndex = 0; lineIndex < this.nag.CointLine; lineIndex++)
            {
                LineConnect line = nag.LineConnectGenerationIndex((byte) lineIndex);
                line.UpDatePoint();
            }
        }

        private Nagruzca nag;

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            mainWindow.SizeChanged += MainWindow_SizeChanged;
            mainWindow.WhileTheme.Checked += WhileTheme_Checked;
            mainWindow.BlackTheme.Checked += BlackTheme_Checked;
            mainWindow.WhileTheme.IsChecked = true;
            mainWindow.CountGeneration.TextChanged += CountGeneration_TextChanged;

            simplexCanvas = mainWindow.CanvasSimplexMethod;
            simplexCanvas.Height = mainWindow.ActualHeight - ChangeSizeSimplexCanvas;
            simplexCanvas.Width = mainWindow.ActualWidth - ChangeSizeSimplexCanvas;
            simplexCanvas.SizeChanged += SimplexCanvas_SizeChanged;
            simplexCanvas.MouseLeave += SimplexCanvas_MouseLeave;

            this.nag = new Nagruzca(mainWindow.FindResource("simplexMethod_Nagruzka").ToString());
            Canvas.SetLeft(this.nag.CanvasElement,  (250));
            Canvas.SetTop(this.nag.CanvasElement, (250));
            Canvas.SetZIndex(nag.CanvasElement,1);

            eventAndMethodViewModels = new EventAndMethodViewModels();

        }

        private void SimplexCanvas_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (Mouse.GetPosition(simplexCanvas).X<=0 || Mouse.GetPosition(simplexCanvas).X>=simplexCanvas.ActualWidth||
                Mouse.GetPosition(simplexCanvas).Y <= 0 || Mouse.GetPosition(simplexCanvas).Y >= simplexCanvas.ActualHeight)
            {
                foreach (UIElement simplexCanvasChild in simplexCanvas.Children)
                {
                    if (simplexCanvasChild is Canvas)
                    {
                        if (((simplexCanvasChild as Canvas).ToolTip as Popup) != null)
                        {
                            ((simplexCanvasChild as Canvas).ToolTip as Popup).IsOpen = false;
                        }
                    }
                }
            }
        }

        private void BlackTheme_Checked(object sender, RoutedEventArgs e)
        {

            config.BackGround = System.Drawing.Color.White;
            config.ForeGround = System.Drawing.Color.Black;
            config.Save();
            ColorThemeBackground = new SolidColorBrush(Color.FromArgb(config.ForeGround.A,
                config.ForeGround.R, config.ForeGround.G, config.ForeGround.B));
            ColorThemeForeground = new SolidColorBrush(Color.FromArgb(config.BackGround.A,
                config.BackGround.R, config.BackGround.G, config.BackGround.B));
        }

        private void WhileTheme_Checked(object sender, RoutedEventArgs e)
        {
            config.BackGround = System.Drawing.Color.Black;
            config.ForeGround = System.Drawing.Color.White;
            config.Save();
            ColorThemeBackground = new SolidColorBrush(Color.FromArgb(config.ForeGround.A,
                          config.ForeGround.R, config.ForeGround.G, config.ForeGround.B));
            ColorThemeForeground = new SolidColorBrush(Color.FromArgb(config.BackGround.A,
                config.BackGround.R, config.BackGround.G, config.BackGround.B));
        }

        private Brush _brushBackground = new SolidColorBrush(Color.FromArgb(config.BackGround.A,
            config.BackGround.R, config.BackGround.G, config.BackGround.B));

        private Brush _brushForeground = new SolidColorBrush(Color.FromArgb(config.ForeGround.A,
            config.ForeGround.R, config.ForeGround.G, config.ForeGround.B));

        public Brush ColorThemeBackground
        {
            get => _brushBackground;
            set { _brushBackground = value;OnPropertyChanged(nameof(ColorThemeBackground)); }
        }

        public Brush ColorThemeForeground
        {
            get => _brushForeground;
            set { _brushForeground = value; OnPropertyChanged(nameof(ColorThemeForeground)); }
        }

        public byte? CountX
        {
            get => _countX;
            set
            {
                if (value >= 0)
                {
                    _countX = ReturnValid(value);
                    OnPropertyChanged(nameof(CountX));
                }
            }
        }

        internal MainWindow MainWindow => mainWindow;

        private List<IElements> _elementses = new List<IElements>();

        /// <summary>
        /// Возвращает генераторы и нагрузка, причем нагрузка в конце
        /// </summary>
        public List<IElements> IElementses
        {
            get => _elementses;
        }

        public Canvas SimplexCanvas => simplexCanvas;
        //public DataGrid SimplexDataGrid => mainWindow.DataGridAnaliticView;


        internal Analizate SimplexAnalizate => analizate;

        private byte? ReturnValid(byte? value)
        {
            if (value <= MaxCountX)
            {
                return value;
            }
            else
            {
                Task.Run(() => MessageBox.Show(mainWindow.FindResource("ViewModels_NOT_ValidCountX").ToString() +MaxCountX
                    , mainWindow.FindResource("ViewModels_Cartion_MessageBox").ToString(),
                    MessageBoxButton.OK, MessageBoxImage.Exclamation));
                return 0;
            }
        }

        private void SimplexCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double point = 0;
            foreach (UIElement simplexCanvasChild in simplexCanvas.Children)
            {
                if (Canvas.GetLeft(simplexCanvasChild)+this.nag.CanvasElement.ActualWidth >= simplexCanvas.ActualWidth)
                {
                    point = simplexCanvas.ActualWidth-this.nag.CanvasElement.ActualHeight*2;
                   
                    Canvas.SetLeft(simplexCanvasChild, point);
                }
                else if (Canvas.GetTop(simplexCanvasChild)+this.nag.CanvasElement.ActualHeight >= simplexCanvas.ActualHeight)
                {
                    point = simplexCanvas.ActualHeight - this.nag.CanvasElement.ActualHeight*2;
                    Canvas.SetTop(simplexCanvasChild, point);
                }
            }
            for (byte line = 0; line < this.nag.CointLine; line++)
            {
                LineConnect lineConnect = this.nag.LineConnectGenerationIndex(line);
                lineConnect.UpDatePoint();
            }
        }


        private void CountGeneration_TextChanged(object sender, TextChangedEventArgs e)
        {
            //TODO:Запилить поддержку удаления только нескольких генераторов без сброса значений?
            mainWindow.CanvasSimplexMethod.Children.RemoveRange(1,mainWindow.CanvasSimplexMethod.Children.Count-1);

            _elementses.Clear();
            
            for (byte i = 0; i < _countX; i++)
            {
                var generation = new Generator(mainWindow.FindResource("simplexMethod_Generator").ToString()+(i+1));
                Canvas.SetLeft(generation.CanvasElement, i * (generation.CanvasElement.Width*2 + 2));
                Canvas.SetTop(generation.CanvasElement, (2));
                generation.LineConnectGeneration=new LineConnect(generation,this.nag);
                this.nag.LineConnectAdd(generation.LineConnectGeneration);
                Canvas.SetZIndex(generation.CanvasElement,1);

                _elementses.Add(generation);
            }

            eventAndMethodViewModels.Create_Analiticx_Simplex_Form();

            _elementses.Add(nag);
        }

       

        public string FindObjResourseLanguage(string key) => mainWindow.FindResource(key).ToString();
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
