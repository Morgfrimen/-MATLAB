using System;
//using MathWorks.MATLAB.NET.Utility;
using MathWorks.MATLAB.NET.Arrays;
using ds=DoubleSimplexMethod.DoubleSimplexMethod;

namespace ЧисленныМетоды.Models
{
    public class LogicalCommon : IDisposable
    {
        //public readonly ds.DoubleSimplexMethod doubleSimplexMethod;
        public MWArray resultArray = null; 

        /// <summary>
        /// Тестовый конструктор
        /// </summary>
        public LogicalCommon()
        {
            //MathWorks.MATLAB.NET.Utility.MWMCR mwmcr = new MWMCR("DoubleSimplexMethod.dll", @"D:\Программирование\ЧисленныеМетоды\ЧисленныМетоды\Models\Matlab\application",true);
            
            MWNumericArray C = new MWNumericArray(new double[] { 24, 24 });
            MWNumericArray A = new MWNumericArray(new double[,] {{1,0},{0,1},{-1,-1}});
            MWNumericArray B = new MWNumericArray(new double[] { 200,1000,-900});
            MWNumericArray Aeq = new MWNumericArray(new double[] { 2.5,6 });
            MWNumericArray Beq = new MWNumericArray(new double[] { 5000});
            MWNumericArray lb = new MWNumericArray(new double[] { 0,0 });
            //resultArray = new MWNumericArray().DualSimplex(C,A,B, Aeq, Beq, lb,null);
            var dsss = new ds();
            resultArray = dsss.DualSimplex(C, A, B, Aeq, Beq, lb, new MWNumericArray());
            var test = resultArray[1].ToArray();
            var test1 = resultArray[2].ToArray();
        }

        public LogicalCommon(params double[] arrays)
        {

        }

        public void Dispose()
        {
            resultArray?.Dispose();
        }
    }
}
