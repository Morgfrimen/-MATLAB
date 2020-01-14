using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Drawing;

namespace ЧисленныМетоды.Models.SinplexMethod_GraphicInput
{
    public class LineConnect
    {
        private ViewModels.ViewModels viewModels=ViewModels.ViewModels.ViewModel;
        internal LineConnect() { }

        private Nagruzca nag;
        private Generator generation;

        private const double PlusSize = 12;
        public LineConnect(IElements generat,IElements nag)
        {
            this.nag=nag as Nagruzca ?? throw new Exception("Nagruzca is NULL"); ;
            this.generation = generat as Generator ?? throw new Exception("Generator is NULL");
            line.X1 = Canvas.GetLeft(nag.CanvasElement)+PlusSize;
            line.Y1 = Canvas.GetTop(nag.CanvasElement)+PlusSize;
            line.X2 = Canvas.GetLeft(generation.CanvasElement) + PlusSize;
            line.Y2 = Canvas.GetTop(generation.CanvasElement) + PlusSize;
            viewModels.SimplexCanvas.Children.Add(line);
        }

        private Line line = new Line()
        {
            StrokeThickness = 1,
            Stroke = new SolidColorBrush(System.Windows.Media.Color.FromArgb(
                System.Drawing.Color.Black.A, System.Drawing.Color.Black.R, System.Drawing.Color.Black.G, System.Drawing.Color.Black.B))
        };

        public Line ConnectLine
        {
            get => line;
        }

        public Generator GeneratorConnect => generation;
        public Nagruzca NagruzcaConnect => nag;

        public void UpDatePoint()
        {
            if (generation is null)
            {
                return;
            }
            else
            {
                line.X1 = Canvas.GetLeft(nag.CanvasElement) + PlusSize;
                line.Y1 = Canvas.GetTop(nag.CanvasElement) + PlusSize;
                line.X2 = Canvas.GetLeft(generation.CanvasElement) + PlusSize;
                line.Y2 = Canvas.GetTop(generation.CanvasElement) + PlusSize;
            }
           
        }

    }
}
