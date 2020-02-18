using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Words.NET;
using ЧисленныМетоды.ViewModels;

namespace ЧисленныМетоды
{
    /// <summary>
    /// Логика взаимодействия для Result.xaml
    /// </summary>
    public partial class Result : Page
    {
        private readonly ViewModels.ViewModels viewModels;

        public Result()
        {
            InitializeComponent();
            viewModels = ViewModels.ViewModels.ViewModel;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        public static void СИМПЛЕКСТАБЛИЦАSTRING(int[] БАЗИС, double[] B, double[,] A, double[] C, int n, int m, double Z, string named, int formated)
        {
            if (formated == 2)
            {
                string res = "Симлекс таблица" + "\r\n";
                res += "Xбаз  |b     |";
                for (int i = 0; i < n; i++)
                {
                    if (i != n - 1)
                    {

                        res += $"X{i + 1}    |";
                    }
                    else
                    {

                        res += $"X{i + 1}" + "\r\n";
                    }
                }
                for (int M = 0; M < m; M++)
                {
                    res += $"{БАЗИС[M]}     |{B[M]:f3}  |";

                    for (int N = 0; N < n; N++)
                    {
                        res += $"{A[N, M]:f3}  |";

                    }
                    res += "\r\n";

                }
                res += $"Z     |{Z:f3}    |";

                for (int N = 0; N < n; N++)
                {
                    res += $"{C[N]:f3}   |";

                }
                res += "\r\n";
                res += "\r\n";
                Console.WriteLine(res);
                //  using (StreamWriter sw = new StreamWriter(named, false, System.Text.Encoding.Default))
                //{
                //  string text = res;
                // string text = "1\r\n2\r\n";
                //  sw.Write(text);
                //}
                File.WriteAllText(named, res, Encoding.Unicode);

            }
            else
            {
                //тут если вывод в ворде должен быть
                DocX doc = DocX.Create(named);
                doc.InsertParagraph("Результаты расчета: ");
                var table = doc.AddTable(m + 2, n + 2);
                for (int i = 0; i < m + 1; i++)
                {
                    for (int j = 0; j < n + 2; j++)
                    {
                        if (i == 0 & j == 0)
                        {
                            table.Rows[i].Cells[j].InsertEquation("X_баз");

                        }
                        else if (i == 0 & j == 1)
                        {
                            table.Rows[i].Cells[j].InsertEquation("b");
                            for (int J = 2; J < n + 2; J++)
                            {
                                table.Rows[i].Cells[J].InsertEquation($"X_{i}");
                            }
                        }
                        else if (i == 0 & j > 1)
                        {
                            table.Rows[i].Cells[j].InsertEquation($"X_{i + 2}");
                            // continue;
                        }
                        else if (j == 0 & i != 0)
                        {
                            table.Rows[i].Cells[0].InsertEquation($"X_{i}");
                            table.Rows[i].Cells[1].InsertEquation($"{B[i - 1]}");
                            for (int J = 2; J < n + 2; J++)
                            {
                                for (int M = 0; M < m; M++)
                                {
                                    table.Rows[i].Cells[J].InsertEquation($"{A[i, M]}");
                                }
                            }
                        }
                        //else if (j == 1)
                        //{
                        //    table.Rows[i].Cells[j].InsertEquation($"{B[i]}");
                        //    continue;
                        //}
                        //else
                        //{
                        //    table.Rows[i].Cells[j].InsertEquation($"{A[i,j]}");

                        //}  
                    }
                }
                table.Rows[m + 1].Cells[0].InsertEquation("Z");
                table.Rows[m + 1].Cells[1].InsertEquation($"{Z}");
                for (int j = 2; j < m + 1; j++)
                {

                }
                doc.InsertTable(table);
                try
                {
                    doc.Save();
                    Process.Start("WINWORD.EXE", named);
                }
                catch
                {
                    Process[] ps1 = System.Diagnostics.Process.GetProcessesByName("WINWORD"); //Имя процесса
                    foreach (Process p1 in ps1)
                    {
                        p1.Kill();
                    }


                }
                finally
                {
                    doc.Save();
                    Process.Start("WINWORD.EXE", named);
                }



            }


        }

    }
}
