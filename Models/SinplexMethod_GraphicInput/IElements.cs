using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Controls;

namespace ЧисленныМетоды.Models.SinplexMethod_GraphicInput
{
    public interface IElements
    {
        Canvas CanvasElement { get; }

        Color GetColor();

        string GetName();




    }
}
