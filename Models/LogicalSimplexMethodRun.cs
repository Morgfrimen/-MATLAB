using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using MathWorks.MATLAB.NET.Utility;
using MathWorks.MATLAB.NET.Arrays;
using ds=DualSimplex.Class1;

namespace ЧисленныМетоды.Models
{
    public class LogicalSimplexMethodRun
    {
        //public readonly ds.DoubleSimplexMethod doubleSimplexMethod;
        public MWArray[] resultArray = new MWArray[] {} ; 

        /// <summary>
        /// Тестовый конструктор
        /// </summary>
        public LogicalSimplexMethodRun()
        {
            //MWNumericArray C = new MWNumericArray(new double[] { 24, 24 });
            //MWNumericArray A = new MWNumericArray(new double[,] { { 1, 0 }, { 0, 1 }, { -1, -1 } });
            //MWNumericArray B = new MWNumericArray(new double[] { 200, 1000, -900 });
            //MWNumericArray Aeq = new MWNumericArray(new double[] { 2.5, 6 });
            //MWNumericArray Beq = new MWNumericArray(new double[] { 5000 });
            //MWNumericArray lb = new MWNumericArray(new double[] { 0, 0 });
            ////resultArray = new MWNumericArray().DualSimplex(C,A,B, Aeq, Beq, lb,null);
            //var dsss = new ds();
            //resultArray = dsss.DualSimplex(C, A, B, Aeq, Beq, lb, new MWNumericArray());
            //var test = resultArray[1].ToArray();
            //var test1 = resultArray[2].ToArray();
        }

        /// <summary>
        /// Создаёт объект с решением симплекс задачи
        /// </summary>
        /// <param name="C">Коэффициенты целевой функции</param>
        /// <param name="A">Коэффициенты при неравентсве</param>
        /// <param name="B">Неравенство</param>
        /// <param name="Aeq">Коэффициенты при равенстве</param>
        /// <param name="Beq">Равенство</param>
        /// <param name="lb">Минимум</param>
        /// <param name="ub">Максимум</param>
        public void LogicalSimplexMethodRuns(double[] c, Array a, double[] b, Array aeq, double[] beq, double[] Lb, double[] Ub, out Array result)
        {
            ds doubleSimplex = new ds();
            // ReSharper disable once InvocationIsSkipped
            Debug.Print("Провалился в логику расчета");
            MWNumericArray C = new MWNumericArray(c);
            MWNumericArray A = new MWNumericArray(a as double[]);
            MWNumericArray B = new MWNumericArray(b);
            MWNumericArray Aeq = new MWNumericArray(aeq as double[]);
            MWNumericArray Beq = new MWNumericArray(beq);
            MWNumericArray lb = new MWNumericArray(Lb);
            MWNumericArray ub = new MWNumericArray(Ub);

            //MWNumericArray C = new MWNumericArray(new double[] { 24, 24 });
            //MWNumericArray A = new MWNumericArray(new double[] { -1, -1 } );
            //MWNumericArray B = new MWNumericArray(new double[] { -900 });
            //MWNumericArray Aeq = new MWNumericArray(new double[] { 2.5, 6 });
            //MWNumericArray Beq = new MWNumericArray(new double[] { 5000 });
            //MWNumericArray lb = new MWNumericArray(new double[] { 0, 0 });
            //MWNumericArray ub = new MWNumericArray(new double[] { 200, 1000 });
            try
            {
                resultArray = doubleSimplex.DualSimplex(4,C, A, B, Aeq, Beq, lb, ub);
            }
            catch (Exception)
            {
                MessageBox.Show("Проверте входные данные - в них ошибка");
                result = null;
                return;
            }

            result = resultArray.ToArray();


        }


        /// <summary>
        /// Создаёт объект с решением симплекс задачи
        /// </summary>
        /// <param name="C">Коэффициенты целевой функции</param>
        /// <param name="A">Коэффициенты при неравентсве</param>
        /// <param name="B">Неравенство</param>
        /// <param name="Aeq">Коэффициенты при равенстве</param>
        /// <param name="Beq">Равенство</param>
        /// <param name="lb">Минимум</param>
        /// <param name="ub">Максимум</param>
        public LogicalSimplexMethodRun(double[] C, double[,] A, double[] B, double[,] Aeq, double[] Beq, double[] lb, double[] ub )
        {

        }

        /// <summary>
        /// Создаёт объект с решением симплекс задачи
        /// </summary>
        /// <param name="C">Коэффициенты целевой функции</param>
        /// <param name="A">Коэффициенты при неравентсве</param>
        /// <param name="B">Неравенство</param>
        /// <param name="Aeq">Коэффициенты при равенстве</param>
        /// <param name="Beq">Равенство</param>
        /// <param name="lb">Минимум</param>
        /// <param name="ub">Максимум</param>
        public LogicalSimplexMethodRun(double[] C, double[] A, double[] B, double[] Aeq, double[] Beq, double[] lb, double[] ub)
        {

        }

        /// <summary>
        /// Создаёт объект с решением симплекс задачи
        /// </summary>
        /// <param name="C">Коэффициенты целевой функции</param>
        /// <param name="A">Коэффициенты при неравентсве</param>
        /// <param name="B">Неравенство</param>
        /// <param name="Aeq">Коэффициенты при равенстве</param>
        /// <param name="Beq">Равенство</param>
        /// <param name="lb">Минимум</param>
        /// <param name="ub">Максимум</param>
        public LogicalSimplexMethodRun(double[] C, double[,] A, double[] B, double[] Aeq, double[] Beq, double[] lb, double[] ub)
        {

        }

        /// <summary>
        /// Создаёт объект с решением симплекс задачи
        /// </summary>
        /// <param name="C">Коэффициенты целевой функции</param>
        /// <param name="A">Коэффициенты при неравентсве</param>
        /// <param name="B">Неравенство</param>
        /// <param name="Aeq">Коэффициенты при равенстве</param>
        /// <param name="Beq">Равенство</param>
        /// <param name="lb">Минимум</param>
        /// <param name="ub">Максимум</param>
        public LogicalSimplexMethodRun(double[] C, double[] A, double[] B, double[,] Aeq, double[] Beq, double[] lb, double[] ub)
        {

        }

     
    }
}
