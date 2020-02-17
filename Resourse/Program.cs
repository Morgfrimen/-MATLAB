using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xceed.Words.NET;

namespace Simplex2._0
{
    class Program
    {
       
        static void ПУНКТ17(ref int n,ref int m,ref double[,] A,ref double[] B,ref int[] Базис,ref double Z,ref double[] C, int N,string named,int formated)
        {
          //  int H = 20;
           
            //СИМПЛЕКСТАБЛИЦА(Базис, B, A, C, n, m, Z);
            int Цбаз = МИНИМУММАССИВА(C);
            for (int h = 0; ; h++)
            {
                
                if (C[Цбаз] >= 0)
                {
                    ПЕЧАТЬРЕЗУЛЬТАТОВ(n, m, A, B, Базис, Z, C, named,formated);
                    break;
                }
                else
                {
                    double[] dB = new double[m];
                    for (int i = 0; i < m; i++)
                    {
                        if (A[Цбаз, i] > 0)
                        {
                            dB[i] = B[i] / A[Цбаз, i];
                        }
                        if (dB[i] <= 0)
                        {
                            dB[i] = double.PositiveInfinity;
                        }

                    }
                    int Опорный = МИНИМУММАССИВА(dB);
                    ВЫВОДМАССИВ(dB);
                    Console.WriteLine($"Z={Z}");
                    Базис[Опорный] = Цбаз + 1;
                    for (int i = 0; i < m; i++)
                    {
                        if (i != Опорный)
                        {
                            B[i] -= dB[Опорный] * A[Цбаз, i];
                        }
                        else
                        {
                            B[i] = dB[i];
                        }
                    }
                    for (int j = 0; j < n; j++)
                    {
                        if (j != Цбаз)
                        {
                            A[j, Опорный] /= A[Цбаз, Опорный];
                        }
                    }
                    for (int j = 0; j < n; j++)
                    {
                        for (int i = 0; i < m; i++)
                        {
                            if (j != Цбаз & i != Опорный)
                            {
                                A[j, i] -= A[j, Опорный] * A[Цбаз, i];
                            }
                        }
                    }
                    for (int j = 0; j < n; j++)
                    {
                        if (j != Цбаз)
                        {
                            C[j] -= A[j, Опорный] * C[Цбаз];

                        }
                    }
                    Z =Z+(1)* C[Цбаз] * dB[Опорный];

                    C[Цбаз] = 0;

                    A[Цбаз, Опорный] = 1;
                    for (int i = 0; i < m; i++)
                    {
                        if (i != Опорный)
                        {
                            A[Цбаз, i] = 0;
                        }
                    }
                    Console.WriteLine("СНАЧАЛА 17 пункт");

                }
               
            }
           // NEWПУНКТ17(ref n, ref m, ref A, ref B, ref Базис, ref Z, ref C, 0);
            //TheaDer17(n, m, A, B, Базис, Z, C, 0);





        }
        public static void ПУНКТ8(ref int n, ref int m, ref double[,] A, ref double[] B, ref int[] Базис, ref double Z, ref double W, ref double[] d, ref double[] C, int N,string named,int formated)
        {
           // int H = 20;
            n = n - (n - N);

            double[] У1МЕНЬШЕНИЕРАЗМЕРА = new double[n];
            for (int i = 0; i < n; i++)
            {
                У1МЕНЬШЕНИЕРАЗМЕРА[i] = d[i];
            }
            d = new double[n];
            for (int i = 0; i < n; i++)
            {
                d[i] = У1МЕНЬШЕНИЕРАЗМЕРА[i];
            }
            for (int h=0; ;h++ )
            {
                int Дбаз = 0;
                Дбаз = МИНИМУММАССИВА(d);
                ///////////////////////////////
                //ЕСЛИ ДБАЗ БОЛЬШЕ 0 И W=0 переход к пункту 17
                ВЫВОДМАССИВ(d);
                Console.WriteLine($"W={W}");
                Console.WriteLine($"Z={Z}");
                СИМПЛЕКСТАБЛИЦА(Базис, B, A, C, d, n, m, Z, W);
                if (d[Дбаз] >= 0 & W >= 0)
                {
                    Console.WriteLine("Переход к пункту 17 из-за того,что W>=0");
                    n =n-(n-N);

                    double[] УМЕНЬШЕНИЕРАЗМЕРА = new double[n];
                    for (int i = 0; i < n; i++)
                    {
                        УМЕНЬШЕНИЕРАЗМЕРА[i] = C[i];
                    }
                    C = new double[n];
                    for (int i = 0; i < n; i++)
                    {
                        C[i] = УМЕНЬШЕНИЕРАЗМЕРА[i];
                    }
                    ПУНКТ17(ref n, ref m, ref A, ref B, ref Базис, ref Z, ref C, 0, named,formated);
                    //ПЕЧАТЬРЕЗУЛЬТАТОВ(n, m, A, B, Базис, Z, C);
                    return;

                }
                else
                {

                    /////////////////////////////////////////////////
                    //Алгоритм с пунта 10
                    double[] dB = new double[m];
                    for (int i = 0; i < m; i++)
                    {
                        if (A[Дбаз, i] > 0)
                        {
                            dB[i] = B[i] / A[Дбаз, i];
                        }
                        if (dB[i] <= 0)
                        {
                            dB[i] = double.PositiveInfinity;
                        }

                    }
                    ВЫВОДМАССИВ(dB);
                    int Опорный = МИНИМУММАССИВА(dB);//где-то получается,что dB у меня не число
                    
                    Базис[Опорный] = Дбаз + 1;
                    for (int i = 0; i < m; i++)
                    {
                        if (i != Опорный)
                        {
                            B[i] -= dB[Опорный] * A[Дбаз, i];
                        }
                        else
                        {
                            B[i] = dB[i];
                        }
                    }
                    
                    
                    for (int j = 0; j < n; j++)
                    {
                        if (j != Дбаз)
                        {
                            A[j, Опорный] /= A[Дбаз, Опорный];
                            if(A[j, Опорный] == double.PositiveInfinity | A[j, Опорный] == double.NegativeInfinity | double.IsNaN(A[j, Опорный])==true)
                            {
                                A[j, Опорный] = 0;
                            }
                        }
                    }
                    for (int j = 0; j < n; j++)
                    {
                        for (int i = 0; i < m; i++)
                        {
                            if (j != Дбаз & i != Опорный)
                            {
                                A[j, i] -= A[j, Опорный] * A[Дбаз, i];
                            }
                        }
                    }
                    for (int j = 0; j < n; j++)
                    {
                        if (j != Дбаз)
                        {
                            C[j] -= A[j, Опорный] * C[Дбаз];
                            d[j] -= A[j, Опорный] * d[Дбаз];
                        }
                    }
                    Z =Z+C[Дбаз] * dB[Опорный];
                    double W1 = W;
                    W = W - d[Дбаз] * dB[Опорный];
                    if (W == W1)
                    {
                        // ПЕЧАТЬРЕЗУЛЬТАТОВ(n, m, A, B, Базис, Z, C);
                        ПУНКТ17(ref n, ref m, ref A, ref B, ref Базис, ref Z, ref C, 0,named,formated);
                    }
                    C[Дбаз] = 0;
                    d[Дбаз] = 0;
                    A[Дбаз, Опорный] = 1;
                    for (int i = 0; i < m; i++)
                    {
                        if (i != Опорный)
                        {
                            A[Дбаз, i] = 0;

                        }
                    }
                    //if (h == h-1)
                    //{
                    //    Console.WriteLine("Процесс зациклился пробуем через пункт 17");
                    //    ПУНКТ17(ref n, ref m, ref A, ref B, ref Базис, ref Z, ref C, N);
                    //}
                    Console.WriteLine($"СНАЧАЛА 8 пункт, итерация {h+1}");
                    

                }
            }

        }





        static void ПЕЧАТЬРЕЗУЛЬТАТОВ(int n, int m, double[,] A, double[] B, int[] Базис, double Z, double[] C,string named,int formated)
        {
            Console.WriteLine("Печать результатов:");
            СИМПЛЕКСТАБЛИЦА(Базис, B, A, C, n, m, Z);

            //ЗАПИСЬ ФАЙЛА
            СИМПЛЕКСТАБЛИЦАSTRING(Базис, B, A, C, n, m, Z, named,formated);

            /////////
            Console.ReadLine();
            Environment.Exit(0);
            return;
        }





        static void СИМПЛЕКСТАБЛИЦА(int[] БАЗИС,double[] B,double[,] A,double[] C,double[] D,int n,int m,double Z,double W)
        {
            Console.WriteLine("Симлекс таблица");
            Console.Write("Xбаз  |b     |");
            for(int i=0;i<n; i++)
            {
                if (i != n - 1)
                {
                    Console.Write($"X{i+1}    |");
                }
                else
                {
                    Console.Write($"X{i + 1}\n");
                }
            }
            for(int M = 0; M < m;M++)
            {
                Console.Write($"{БАЗИС[M]}     |{B[M]:f3}  |");
                for(int N = 0; N < n; N++)
                {
                    Console.Write($"{A[N,M]:f3}  |");
                }
                Console.Write("\n");
            }
            Console.Write($"Z   |{Z:f3}    |");
            for(int N = 0; N < n;N++)
            {
                Console.Write($"{C[N]:f3}   |");
            }
            Console.Write("\n");
            Console.Write($"W   |{W:f3}   |");
            for (int N = 0; N < n; N++)
            {
                Console.Write($"{D[N]:f3}   |");
            }
            Console.Write("\n");

        }

        static void СИМПЛЕКСТАБЛИЦА(int[] БАЗИС, double[] B, double[,] A, double[] C, int n, int m, double Z)
        {
            
            Console.WriteLine("Симлекс таблица");
            Console.Write("Xбаз  |b     |");
            for (int i = 0; i < n; i++)
            {
                if (i != n - 1)
                {
                    Console.Write($"X{i + 1}    |");
                }
                else
                {
                    Console.Write($"X{i + 1}\n");
                }
            }
            for (int M = 0; M < m; M++)
            {
                Console.Write($"{БАЗИС[M]}     |{B[M]:f3}  |");
                for (int N = 0; N < n; N++)
                {
                    Console.Write($"{A[N, M]:f3}  |");
                }
                Console.Write("\n");
            }
            Console.Write($"Z   |{Z:f3}    |");
            for (int N = 0; N < n; N++)
            {
                Console.Write($"{C[N]:f3}   |");
            }
            Console.Write("\n");
            Console.Write("\n");

        }

        //TODO: Вытащить этот метод и адаптировать к моей программе
        static void СИМПЛЕКСТАБЛИЦАSTRING(int[] БАЗИС, double[] B, double[,] A, double[] C, int n, int m, double Z,string named,int formated)
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
                File.WriteAllText(named,res, Encoding.Unicode);

            }
            else
            {
                //тут если вывод в ворде должен быть
                DocX doc = DocX.Create(named);
                doc.InsertParagraph("Результаты расчета: ");
                var table = doc.AddTable(m+2,n+2);
                for(int i = 0; i < m+1; i++)
                {
                    for(int j = 0; j < n+2; j++)
                    {
                        if (i == 0 & j == 0)
                        {
                            table.Rows[i].Cells[j].InsertEquation("X_баз");
                            
                        }
                        else if (i == 0 & j == 1)
                        {
                            table.Rows[i].Cells[j].InsertEquation("b");
                            for(int J = 2; J < n+2; J++)
                            {
                                table.Rows[i].Cells[J].InsertEquation($"X_{i}");
                            }
                        }
                        else if (i == 0 & j>1)
                        {
                            table.Rows[i].Cells[j].InsertEquation($"X_{i+2}");
                           // continue;
                        }
                        else if (j == 0 & i!=0)
                        {
                            table.Rows[i].Cells[0].InsertEquation($"X_{i}");
                            table.Rows[i].Cells[1].InsertEquation($"{B[i-1]}");
                            for (int J = 2; J < n+2; J++)
                            {
                                for(int M = 0; M < m; M++)
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
                table.Rows[m+1].Cells[0].InsertEquation("Z");
                table.Rows[m+1].Cells[1].InsertEquation($"{Z}");
                for (int j = 2; j < m+1; j++)
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





        static int МИНИМУММАССИВА(double[] D)
        {
            int min = 0;
            
            for (int i = 1; i < D.Length; i++)
            {
                if (D[i] < D[min])
                {
                    min = i;
                }
            }
            return min;
        }
        
        static double[,] ЗАПИСЬМАТРИЦЫ(double[,] A,int n,int m)
        {
            double[,] a = new double[n, m];
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    a[i, j] = A[i, j];
                }
            }
            return a;
        }
        static void ВЫВОДМАТРИЦЫ(double[,] A,int n,int m)
        {
            Console.WriteLine($"Матрица размерностью [{n},{m}]");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (j == m - 1)
                    {
                        Console.Write($" {A[i, j]:f3}\n");
                    }
                    else
                    {
                        Console.Write($" {A[i, j]:f3} ");
                    }
                    
                    
                }
            }
        }
        static void ВЫВОДМАССИВ(double[] A)
        {
            Console.WriteLine($"Массив размерностью [{A.Length}]");
            for(int i = 0; i < A.Length; i++)
            {
                if (i == A.Length - 1)
                {
                    Console.Write($" {A[i]:f3}\n");
                }
                else
                {
                    Console.Write($" {A[i]:f3} ");
                }

            }
            
        }
        static double[] УВЕЛИЧИСТЬРАЗМЕРМАССИВА(double[] C,int Size)
        {
            double[] vs = new double[Size];
            for(int i = 0; i < C.Length; i++)
            {
                vs[i] = C[i];
            }
            return vs;
        }





        static void ВЫВОДМАССИВ(int[] A)
        {
            Console.WriteLine($"Массив размерностью [{A.Length}]");
            for (int i = 0; i < A.Length; i++)
            {
                if (i == A.Length - 1)
                {
                    Console.Write($" {A[i]}\n");
                }
                else
                {
                    Console.Write($" {A[i]} ");
                }

            }

        }




        static void Main(string[] args)
        {
            //добавка для записи в ворд или тхт + имя файла
            //Console.WriteLine(@"Формат записи (1-docx\ 2-txt)>>>>> ");
            //int fornated = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("Имя файла>>>");
            //string named = Console.ReadLine();


            ////ВВОД ДАННЫХ
            //Console.Write("n>>");
            //int n = Convert.ToInt32(Console.ReadLine());
            //int БЫЛО = n;
            //Console.Write("m>>");
            //int m = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("B>>>");
            //double[] B = new double[m];
            //for (int j = 0; j < m; j++)
            //{
            //    Console.Write($"B[{j + 1}] = ");

            //    B[j] = Convert.ToDouble(Console.ReadLine());
            //}
            //Console.WriteLine($"Матрица А размерностью [{n},{m}]");
            //double[,] A = new double[n, m];
            //for (int i = 0; i < n; i++)
            //{
            //    for (int j = 0; j < m; j++)
            //    {
            //        Console.Write($"A[{i + 1},{j + 1}] = ");

            //        A[i, j] = Convert.ToDouble(Console.ReadLine());
            //    }
            //}
            //Console.WriteLine($"Массив коэффициентов С[{n}]");
            //double Z = 0;
            //double[] C = new double[n];
            //for (int j = 0; j < n; j++)
            //{
            //    Console.Write($"C[{j + 1}] = ");
            //    C[j] = Convert.ToDouble(Console.ReadLine());
            //}
            //string[] Znaki = new string[m];
            //for (int i = 0; i < m; i++)
            //{
            //    Console.Write($"Знак уравнения {i + 1} -----> ");
            //    Znaki[i] = Console.ReadLine();
            //}
            //int n = 4;
            //int БЫЛО = n;
            //int m = 6;
            //double[] B = new double[m];
            //B[0] = 800;
            //B[1] = 700;
            //B[2] = 500;
            //B[3] = 400;
            //B[4] = 1900;
            //B[5] = 10545;
            //double[,] A = new double[n, m];
            //A[0, 0] = 1; A[0, 1] = 0; A[0, 2] = 0; A[0, 3] = 0; A[0, 4] = 1; A[0, 5] = 1.95;
            //A[1, 0] = 0; A[1, 1] = 1; A[1, 2] = 0; A[1, 3] = 0; A[1, 4] = 1; A[1, 5] = 5.15;
            //A[2, 0] = 0; A[2, 1] = 0; A[2, 2] = 1; A[2, 3] = 0; A[2, 4] = 1; A[2, 5] = 5;
            //A[3, 0] = 0; A[3, 1] = 0; A[3, 2] = 0; A[3, 3] = 1; A[3, 4] = 1; A[3, 5] = 5.55;

            //double[] C = new double[n];
            //C[0] = 11.52; C[1] = 71.07; C[2] = 52.8; C[3] = 59.25;

            //string[] Znaki = new string[m];
            //Znaki[0] = "<="; Znaki[1] = "<="; Znaki[2] = "<="; Znaki[3] = "<="; Znaki[4] = ">="; Znaki[5] = "=";
            //double Z = 0;


            Console.WriteLine(@"Формат записи (1-docx\ 2-txt)>>>>> ");
            int fornated = 1;
            Console.WriteLine("Имя файла>>>");
            string named = "test.docx";
            int n = 2;
            int БЫЛО = n;
            int m = 4;
            double[] B = new double[m];
            B[0] = 5000;
            B[1] = 200;
            B[2] = 1000;
            B[3] = 900;
            double[,] A = new double[n, m];
            A[0, 0] = 2.5; A[0, 1] = 1; A[0, 2] = 0; A[0, 3] = 1;
            A[1, 0] = 6; A[1, 1] = 0; A[1, 2] = 1; A[1, 3] = 1;
            double[] C = new double[n];
            C[0] = 24; C[1] = 24;
            string[] Znaki = new string[m];
            Znaki[0] = "="; Znaki[1] = "<="; Znaki[2] = "<="; Znaki[3] = ">=";
            double Z = 0;



            Console.WriteLine("Матрица исходная:");
            ВЫВОДМАТРИЦЫ(A, n, m);
            /////////////////////////////////////////
            //ПЕРЕВОД ВСЕХ ОГРАНИЧЕНИЙ НЕРАВЕНСТВ
            
            for (int i = 0; i < m; i++)
            {
              
                if (Znaki[i] == "<="| Znaki[i]== "≤")//добавляем Х
                {
                    double[,] a = new double[n, m];
                    a = ЗАПИСЬМАТРИЦЫ(A,n,m);
                    n +=1;
                   
                    A = new double[n, m];
                    for(int j = 0; j < n - 1; j++)
                    {
                        for(int index = 0; index < m; index++)
                        {
                            A[j, index] = a[j, index];
                        }
                    }
                    A[n-1,i] = 1;
                    
                }
                else if (Znaki[i] == ">=" | Znaki[i]== "≥")
                {
                    double[,] a = new double[n, m];
                    a = ЗАПИСЬМАТРИЦЫ(A, n, m);
                    n += 1;
                   
                    A = new double[n, m];
                    for (int j = 0; j < n - 1; j++)
                    {
                        for (int index = 0; index < m; index++)
                        {
                            A[j, index] = a[j, index];
                        }
                    }
                    A[n - 1, i] = -1;
                }
                
            }
           
            ///////////////////////////////////////////////////////////////////////////////////////////
            //ОПРЕДЕЛЕНИЕ НАЧАЛЬНОГО БАЗИСА
            int[] Базис = new int[m];
            for(int N = 0; N < n; N++)
            {
                int Счечки = 0;
                int Уравнение = 0;
                int Индекс = 0;
                for(int M = 0; M < m; M++)
                {
                    if (A[N, M] == 1 )
                    {
                        if(M>=1 & M < (m-1) )
                        {
                            if(A[N,M-1]==0 | A[N, M + 1] == 0)
                            {
                                Счечки += 1;
                                Уравнение = M;
                                Индекс = N;
                            }
                        }
                        else if (M == 0)
                        {
                            if (A[N, M + 1] == 0)
                            {
                                Счечки += 1;
                                Уравнение = M;
                                Индекс = N;
                            }
                        }
                        else if (M == m - 1)
                        {
                            if (A[N, M - 1] == 0)
                            {
                                Счечки += 1;
                                Уравнение = M;
                                Индекс = N;
                            }
                        }
                       
                    }
                }
                if (Счечки == 1)
                {
                    //for(int i = 0; i < n; i++)
                    //{
                    //    A[i, Уравнение] /= A[Индекс, Уравнение];
                    //}
                    Базис[Уравнение] = Индекс+1;
                }
            }
            
            ВЫВОДМАССИВ(Базис);
            int XXXYYY = 0;
            for(int i = 0; i < m; i++)
            {
                if (Базис[i] != 0)
                {
                    XXXYYY += 1;
                }
            }
            if (XXXYYY == Базис.Length)
            {
                Console.WriteLine("Переходим к пункту 17");
               
                ПУНКТ17(ref n, ref m, ref A, ref B, ref Базис, ref Z, ref C, 0,named, fornated);
               
            }
            //тут метка на 17 пункт,в ветветлении метка на пункт 5
            ///////////////////////////////////////////////////////
            ///ИНАЧЕ В УРАВНЕНИЕ БЕЗ БАЗИСНОЙ ПЕРЕМЕННОЙ ДОБАВЛЯЕТСЯ ИССКУСТВЕННАЯ БАЗИСНАЯ ПЕРЕМЕННАЯ+ФОРМИРОВАНИЕ НОВОЙ ЦЕЛЕВОЙ ФУНКЦИИ
            double W = 0;
            List<int> ИсскуственныеБазисы = new List<int>();
            for (int i = 0; i < m; i++)
            {
                if (Базис[i] == 0)
                {
                    double[,] a = new double[n, m];
                    a = ЗАПИСЬМАТРИЦЫ(A, n, m);
                    n += 1;
                    A = new double[n, m];
                    for (int J = 0; J < n - 1; J++)
                    {
                        for (int index = 0; index < m; index++)
                        {
                            A[J, index] = a[J, index];
                        }
                    }
                    A[n - 1, i] = 1;
                    W -= B[i];
                    Базис[i] = n;
                    ИсскуственныеБазисы.Add(n);
                }
            }
            
            Console.WriteLine("Преобразованная Матрица А для в пункте 5");
            ВЫВОДМАТРИЦЫ(A, n, m);
            ВЫВОДМАССИВ(Базис);
            Console.Write($"Новая целевая функция-->> W= ");
            for(int i = 0; i < ИсскуственныеБазисы.Count; i++)
            {
                if (i == 0)
                {
                    Console.Write($"X[{ИсскуственныеБазисы[i]}] + ");
                }
                else if (i == ИсскуственныеБазисы.Count - 1)
                {
                    Console.Write($"X[{ИсскуственныеБазисы[i]}]={W}\n");
                }
                else
                {
                    Console.Write($"X[{ИсскуственныеБазисы[i]}] + ");
                }
            }
            //////////////////////////////////////////////////////////////////////////////////////////
            //ИСКЛЮЧЕНИЕ ИССКУСТВЕННЫХ БАЗИСОВ ИЗ W И ФОРМИРОВАНИЕ d
            double[] d = new double[n];
            C = УВЕЛИЧИСТЬРАЗМЕРМАССИВА(C, n);
            double[,] ПРОМЕЖУТОЧНАЯМАТРИЦА = new double[n, ИсскуственныеБазисы.Count];
            for (int i = 0; i < ИсскуственныеБазисы.Count; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (Базис[j] == ИсскуственныеБазисы[i])
                    {
                        for (int N = 0; N < n; N++)
                        {
                            ПРОМЕЖУТОЧНАЯМАТРИЦА[N, i] = A[N, j];
                        }
                    }
                    
                }
            }
            ВЫВОДМАТРИЦЫ(A, n, m);
            ВЫВОДМАТРИЦЫ(ПРОМЕЖУТОЧНАЯМАТРИЦА, n, ИсскуственныеБазисы.Count);
            for(int i = 0; i < n-ИсскуственныеБазисы.Count; i++)
            {
                if (ИсскуственныеБазисы.Count > 1)
                {
                    for (int j = 1; j < ИсскуственныеБазисы.Count; j++)
                    {
                        d[i] = -ПРОМЕЖУТОЧНАЯМАТРИЦА[i, j - 1] - ПРОМЕЖУТОЧНАЯМАТРИЦА[i, j];
                    }
                }
                else
                {
                    for (int j = 0; j < ИсскуственныеБазисы.Count; j++)
                    {
                        d[i] = -ПРОМЕЖУТОЧНАЯМАТРИЦА[i, j];
                    }
                }
            }
            ВЫВОДМАССИВ(d);
            ///////////////////////////////////////////////////////////
            //СОСТАВЛЕНИЕ СИМПЛЕКС ТАБЛИЦЫ
            СИМПЛЕКСТАБЛИЦА(Базис, B, A, C, d, n, m,Z,W);
            /////////////////////////////////////////////////////////
            ///ОПРЕДЕЛЕНИЕ МИНИМАЛЬНОГО d
            
            //ПУНКТ8(n, m, A, B, Базис, Z, W, d, C, ИсскуственныеБазисы.Count);
            ///////////////////
            ///

            ПУНКТ8(ref n, ref m, ref A, ref B, ref Базис, ref Z, ref W, ref d, ref C, БЫЛО,named,fornated);
          // ПУНКТ8(n, m, A, B, Базис, Z, W, d, C, ИсскуственныеБазисы.Count);
            Console.ReadLine();

          

            ////////////////////////////////////////////
        }
       
      
    }
}
