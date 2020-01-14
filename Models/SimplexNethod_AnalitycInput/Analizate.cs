using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;

namespace ЧисленныМетоды.Models.SimplexNethod_AnalitycInput
{
    //TODO:Допилить XML файл
    public class Analizate
    {
        private System.Xml.XmlDocument xmlData;
        public Analizate()
        {
            
        }

        private DataSet tableDataSet;
        internal DataSet TableDataSet
        {
            get => tableDataSet;
            set => tableDataSet = value;
        }

      
    }
}
