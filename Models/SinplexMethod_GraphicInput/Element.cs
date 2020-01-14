using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Color = System.Drawing.Color;

namespace ЧисленныМетоды.Models.SinplexMethod_GraphicInput
{
    public abstract class Element : IElements
    {
        private Nagruzca Nag { get; set; }
        private Generator Generator { get; set; }
        protected ViewModels.ViewModels VM=ViewModels.ViewModels.ViewModel;
        protected const double Size = 30;

        public Element(string nameElement,Color color,Color borColor)
        {
            this.panel = VM.SimplexCanvas;
            CreateStackPanel(nameElement,color, borColor);
            panel.Children.Add(this._canvas);
        }

        private Canvas _canvas;
        private Canvas panel;

        private Popup popup;

        private Style popupTextBoxStyle=new Style(){
            Setters=
        {
            new Setter { Property = Control.VerticalAlignmentProperty, Value = VerticalAlignment.Center}
        }
        };
        private Timer timer = new Timer(5000);

        private LineConnect line;
        private List<LineConnect> lineList = new List<LineConnect>();

        public Canvas CanvasElement { get=>_canvas; }

        public LineConnect LineConnectGeneration
        {
            get => line;
            set { line = value; }
        }


        public int CointLine => lineList.Count;

        public LineConnect LineConnectGenerationIndex(byte index) => lineList[index];
        public void LineConnectAdd(LineConnect lines) => lineList.Add(lines);

        protected void CreateStackPanel(string nameElement,System.Drawing.Color color,System.Drawing.Color borderColor)
        {
            _canvas = new Canvas();
            Ellipse ellipse = new Ellipse();
            ellipse.Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B));
            ellipse.StrokeThickness = 2;
            ellipse.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromArgb(borderColor.A, borderColor.R, borderColor.G, borderColor.B));
            ellipse.Width = Size;
            ellipse.Height = ellipse.Width;
            Label label = new Label();
            label.Content = nameElement;
            _canvas.Children.Add(ellipse);
            _canvas.Children.Add(label);
            Canvas.SetLeft(label, Size / 10);
            _canvas.Width = Size;
            _canvas.Height = _canvas.Width;
            _canvas.MouseMove += _canvas_MouseMove;
            _canvas.MouseLeave += _canvas_MouseLeave;
            _canvas.MouseRightButtonDown += _canvas_MouseRightButtonDown;   
        }

        private void _canvas_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Canvas)
            {
                if (popup.IsOpen == true)
                {
                    this.timer.Elapsed += Timer_Elapsed;
                    timer.Enabled = true;
                    this.timer.Start();
                }
            }
            else if (sender is Popup)
            {
                if (popup.IsOpen == true)
                {
                    popup.IsOpen = false;
                }
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            popup.Dispatcher.BeginInvoke(new Action(() => { popup.IsOpen = false; }));
            timer.Stop();
        }

        private void _canvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (popup != null)
                popup.IsOpen = true;
        }

        private void _canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            MainWindow window = App.Current.MainWindow as MainWindow;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (Canvas.GetLeft(_canvas) < panel.ActualWidth && Canvas.GetTop(_canvas) < panel.ActualHeight
                                                                       && Canvas.GetLeft(_canvas) > -1 && Canvas.GetTop(_canvas) > -1)
                {
                    Mouse.Capture(_canvas);
                    Canvas.SetLeft(_canvas, e.GetPosition(window.CanvasSimplexMethod).X);
                    Canvas.SetTop(_canvas, e.GetPosition(window.CanvasSimplexMethod).Y);
                    line?.UpDatePoint();
                    UpDateAllLineConnect();
                }
                else
                {
                    switch (e.LeftButton)
                    {
                        case MouseButtonState.Pressed:
                        case MouseButtonState.Released:
                            Mouse.Capture(null);
                            if (Canvas.GetLeft(_canvas)>=0 && Canvas.GetTop(_canvas) >= 0)
                            {
                                Canvas.SetLeft(_canvas, Canvas.GetLeft(_canvas) - Size);
                                Canvas.SetTop(_canvas, Canvas.GetTop(_canvas) - Size);
                            }
                            else
                            {
                                if(Canvas.GetLeft(_canvas)<0)
                                    Canvas.SetLeft(_canvas, Canvas.GetLeft(_canvas)*-1);
                                if(Canvas.GetTop(_canvas)<0)
                                    Canvas.SetTop(_canvas, Canvas.GetTop(_canvas) *-1);
                            }
                           
                            line?.UpDatePoint();
                            UpDateAllLineConnect();
                            break;
                    }
                }
            }
            else
            {
                Mouse.Capture(null);
                line?.UpDatePoint();
                UpDateAllLineConnect();
            }
        }

        private void UpDateAllLineConnect()
        {
            foreach (LineConnect lineConnect in lineList)
            {
                lineConnect.UpDatePoint();
            }
        }

        protected virtual void CreatePopupCanvas( bool isGenerator)
        {
            StackPanel stackPanel=new StackPanel();
            stackPanel.Background=new SolidColorBrush(System.Windows.Media.Color.FromArgb(Color.White.A, Color.White.R, 
                Color.White.G, Color.White.B));
            DockPanel dockPanel1=new DockPanel();
            Label label1=new Label();
            label1.Content = VM.FindObjResourseLanguage("simplexMethod_Pmax");
            TextBox textBox1 = new TextBox(){Style = popupTextBoxStyle};

            dockPanel1.Children.Add(label1);
            dockPanel1.Children.Add(textBox1);

            DockPanel dockPanel2 = new DockPanel();
            Label label2 = new Label();
            label2.Content = VM.FindObjResourseLanguage("simplexMethod_T");
            TextBox textBox2 = new TextBox() { Style = popupTextBoxStyle };

            dockPanel2.Children.Add(label2);
            dockPanel2.Children.Add(textBox2);

            stackPanel.Children.Insert(0,dockPanel1);
            stackPanel.Children.Insert(1,dockPanel2);

            if (isGenerator)
            {
                DockPanel dockPanel3 = new DockPanel();
                Label label3 = new Label() { Content = VM.FindObjResourseLanguage("simplexMethod_K") };
                TextBox textBox3 = new TextBox() { Style = popupTextBoxStyle };
                dockPanel3.Children.Add(label3);
                dockPanel3.Children.Add(textBox3);
                stackPanel.Children.Add(dockPanel3);

                DockPanel dockPanel4 = new DockPanel();
                Label label4 = new Label() { Content = VM.FindObjResourseLanguage("simplexMethod_Y") };
                TextBox textBox4 = new TextBox() { Style = popupTextBoxStyle };
                dockPanel4.Children.Add(label4);
                dockPanel4.Children.Add(textBox4);
                stackPanel.Children.Add(dockPanel4);

                //TODO: Раставить биндинги
            }
          

            Border border=new Border();
            border.BorderThickness = new Thickness(1);
            border.BorderBrush=new SolidColorBrush(System.Windows.Media.Color.FromArgb(Color.Black.A, Color.Black.R,
                Color.Black.G, Color.Black.B));

            border.Child = stackPanel;

            popup = new Popup();
            popup.Child = border;
            popup.Placement = PlacementMode.Mouse;
            popup.Width = 150;
            popup.MouseLeave += _canvas_MouseLeave;
            popup.MouseEnter += Popup_MouseEnter;

            _canvas.ToolTip = popup;
        }

        private void Popup_MouseEnter(object sender, MouseEventArgs e)
        {
            timer.Enabled = false;
        }


        //TODO: Допилить цвет
        public Color GetColor()
        {
            throw new NotImplementedException();
        }


    }
}
