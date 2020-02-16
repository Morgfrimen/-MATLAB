using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Color = System.Drawing.Color;
using Point = System.Drawing.Point;

namespace ЧисленныМетоды.Models.SinplexMethod_GraphicInput
{
    public class Nagruzca : Element
    {
        public Nagruzca(string nameElement) : base(nameElement, Color.White, Color.Black)
        {
            this.name = nameElement;
            this.CreatePopupCanvas(false);
        }

        private string name;

        private double _pMax = 0;
        private double _T = 0;
        public double PMax
        {
            get => _pMax;
            set => _pMax = value;
        }
        public double T
        {
            get => _T;
            set => _T = value;
        }

        public Nagruzca ThisNagruzca
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
                            default:
                                throw new Exception("Элемента вроде как 2, куда ушло-то в нагрузке?");
                                
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
