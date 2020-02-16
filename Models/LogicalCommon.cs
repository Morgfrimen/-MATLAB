using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathWorks.MATLAB.NET.Utility;
using MathWorks.MATLAB.NET.Arrays;
using ds=DoubleSimplexMethod.DoubleSimplexMethod;
using DoubleSimplexMethodNative;

namespace ЧисленныМетоды.Models
{
    public class LogicalCommon
    {
        //public readonly ds.DoubleSimplexMethod doubleSimplexMethod;
        public MWArray resultArray = null;
        public MWNumericArray[] resultNumericArrays = null;

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
            
            MWArray res = dsss.DualSimplex(C, A, B, Aeq, Beq, lb, new MWNumericArray());
            var test = res.IsNumericArray;
            var test1 = res.NumberOfElements;
            var test2 = res.NumberofDimensions;

            dsss.Dispose();
            C.Dispose();
            A.Dispose();
            B.Dispose();
            Aeq.Dispose();
            Beq.Dispose();
            lb.Dispose();
        }
    }
}
