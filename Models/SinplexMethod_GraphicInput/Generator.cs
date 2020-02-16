using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
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
    /// <summary>
    /// Класс определяет генераторный узел
    /// </summary>
    public class Generator : Element
    {
        private StackPanel stackPanel=new StackPanel();
        public Generator(string name) : base(name, System.Drawing.Color.Red, System.Drawing.Color.Red)
        {
            this.name = name;
            this.CreatePopupCanvas(true);
        }

        private string name;

        private double _pMax = 0;
        private double _k0 = 0;
        private double _T = 0;
        private double _y = 0;

        public double PMax
        {
            get => _pMax;
            set => _pMax = value;
        }
        public double K0
        {
            get => _k0;
            set => _k0 = value;
        }
        public double T
        {
            get => _T;
            set => _T = value;
        }
        public double Y
        {
            get => _y;
            set => _y = value;
        }

        public new LineConnect LineConnectGeneration
        {
            get => base.LineConnectGeneration;
            set { base.LineConnectGeneration = value; }
        }

        public Generator ThisGenerator
        {
            get => this;
        }

        protected sealed override void CreatePopupCanvas(bool isGenerator)
        {
            base.CreatePopupCanvas(isGenerator);
            Popup popup = base.CanvasElement.ToolTip as Popup ?? throw new Exception("Popup is NULL in Nagruzca");
            Decorator border = popup.Child as Decorator ?? throw new Exception("Decorator (Border) is NULL in Nagruzca");
            Panel panelChild = border.Child as Panel ?? throw new Exception("Panel is NULL in Nagruzca");
            for (int stackPanelIndex = 0; stackPanelIndex < panelChild.Children.Count; stackPanelIndex++)
            {
                Panel docPanel = panelChild.Children[stackPanelIndex] as Panel ?? throw new Exception("PanelChild is NULL in Nagruzca");
                foreach (UIElement docPanelChild in docPanel.Children)
                {
                    if (docPanelChild is TextBox)
                    {
                        TextBox textBox = docPanelChild as TextBox ?? throw new Exception("TextBox is NULL in Nagruzca");
                        Binding binding = new Binding()
                        {
                            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                            Source = this
                        };
                        switch (stackPanelIndex)
                        {
                            case 0:
                                binding.Path = new PropertyPath(nameof(this.PMax));
                                break;
                            case 1:
                                binding.Path = new PropertyPath(nameof(this.T));
                                break;
                            case 2:
                                binding.Path = new PropertyPath(nameof(this.K0));
                                break;
                            case 3:
                                binding.Path = new PropertyPath(nameof(this.Y));
                                break;
                            default:
                                throw new Exception("Элемента вроде как 4, куда ушло-то в генераторе?");
                        }
                        textBox.SetBinding(TextBox.TextProperty, binding);
                    }
                }
            }
        }

        public override string GetName()
        {
            return name;
        }
    }
}
