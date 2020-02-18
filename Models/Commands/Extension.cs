using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using ЧисленныМетоды.Models.SinplexMethod_GraphicInput;

namespace ЧисленныМетоды.Models.Commands
{
    /// <summary>
    /// Класс для методов расширения общей логики
    /// </summary>
    public static class Extension
    {
        /// <summary>
        /// Метод собирает из списка элементов ArrayList
        /// </summary>
        /// <param name="resultArrayList">Результирующий ArrayList</param>
        /// <param name="elementses">Список элементов</param>
        /// <returns></returns>
        public static ArrayList GetArrayListing(this ArrayList resultArrayList, List<IElements> elementses)
        {
            int lev = elementses.Count;
            if (lev <= 1)
            {
                MessageBox.Show("Не заданны генераторы!");
                return null;
            }
            //Обработка коэффициентов целевой функции
            if (ViewModels.ViewModels.ViewModel.ZArrays == null)
            {
                ViewModels.ViewModels.ViewModel.ZArrays = new double[elementses.Count];
            }
            resultArrayList.Add(ViewModels.ViewModels.ViewModel.ZArrays);

            //Обработка A
            double[] A = new double[lev - 1];
            for (var index = 0; index < A.Length; index++)
            {
                A[index] = -1;
            }
            resultArrayList.Add(A);

            //Обработка В
            double[] B = new double[1];
            foreach (IElements elements in elementses)
            {
                if (elements is Nagruzca)
                    B[0] = (elements as Nagruzca).PMax * -1;
            }
            resultArrayList.Add(B);

            //Обработка Aeq
            double[] Aeq = new double[lev - 1];
            for (int i = 0; i < elementses.Count; i++)
            {
                IElements elements = elementses[i];
                if (elements is Generator)
                    Aeq[i] = (elements as Generator).T / 1000;
            }
            resultArrayList.Add(Aeq);

            //Обработка Beq
            double[] Beq = new double[1];
            for (int i = 0; i < elementses.Count; i++)
            {
                IElements elements = elementses[i];
                if (elements is Nagruzca)
                    Beq[0] = (elements as Nagruzca).T;
            }
            resultArrayList.Add(Beq);


            //Обработка коэффициентов lb (нижняя граница)
            double[] lb = new double[lev - 1];
            resultArrayList.Add(lb);

            //Обработка коэффициентов ub (верхняя граница)
            double[] ub = new double[lev - 1];
            for (var index = 0; index < elementses.Count; index++)
            {
                IElements elements = elementses[index];
                if (elements is Generator)
                {
                    ub[index] = (elements as Generator).PMax;
                }
            }
            resultArrayList.Add(ub);

            return resultArrayList;



        }
    }
}
