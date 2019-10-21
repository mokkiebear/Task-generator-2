using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static string[] Res = new string[10];

        static void Main(string[] args)
        {
            Random rand = new Random();
            /*for (int i = 0; i < 1000; ++i)
            {
                double[] arr = getProbs(rand, 6);
                Console.WriteLine(i + ") " + arr.Sum() + " | " + String.Join("  ", arr));

                double[] arr1 = getProbs(rand, 7);
                Console.WriteLine(i + ") " + arr1.Sum() + " | " + String.Join("  ", arr1));

                double[] arr2 = getProbs(rand, 8);
                Console.WriteLine(i + ") " + arr2.Sum() + " | " + String.Join("  ", arr2));
            }*/
            int n = rand.Next(6, 9); // случайное кол-во слов от 6 до 8
            Console.WriteLine("Даны " + n + " кодовых слов двоичного префиксного кода. Также указаны вероятности, с которыми эти слова появляются. Найти среднюю длину кода. ");

            double[] probs = getProbs(rand, n);
            Fano(0, n - 1, probs);
            Console.WriteLine();

            double avgLength = 0;
            for (int i = 0; i < n; i++)
            {
                avgLength += probs[i] * Res[i].Length;
                Console.WriteLine(probs[i] + " - " + Res[i]);

            }
            Console.WriteLine("Средняя длина кода: " + avgLength);
            Console.ReadLine();
        }

        static double[] getProbs(Random rand, int n)
        {
            double[] probs = new double[n];
            double temp;
            for (int i = 0; i < n; ++i)
            {
                temp = Math.Round(rand.NextDouble() * 0.5, 2);
                probs[i] = temp;
            }

            double[] result = new double[n];
            result[0] = probs[0];
            for (int i = 1; i < n - 1; ++i)
            {
                if (1 - result.Sum() > 0)
                {
                    double t = getNumber(rand, 1 - result.Sum());
                    result[i] = t;
                }
            }
            if (Math.Round(1 - result.Sum(), 1) <= 0)
            {
                result = getProbs(rand, n);
                //result[n - 2] = Math.Round(result[n - 2] / 2, 2);
            }
            else result[n - 1] = Math.Round(1 - result.Sum(), 2);
            Array.Sort(result);
            Array.Reverse(result);
            return result;
        }

        static double getNumber(Random rand, double a)
        {
            double n = Math.Round(rand.NextDouble() * (a / 2 - 0.01) + 0.01, 2);
            return n;
        }

        static int Delenie_Posledovatelnosty(int L, int R, double[] p)
        {
            double schet1 = 0;
            double schet2 = 0;
            int m = 0;
            for (int i = L; i <= R - 1; i++)
            {
                schet1 = schet1 + p[i];
            }

            schet2 = p[R];
            m = R;
            while (schet1 >= schet2)
            {
                m = m - 1;
                schet1 = schet1 - p[m];
                schet2 = schet2 + p[m];
            }
            return m;
        }

        static void Fano(int L, int R, double[] p)
        {
            int n;
            if (L < R)
            {
                n = Delenie_Posledovatelnosty(L, R, p);
                for (int i = L; i <= R; i++)
                {
                    if (i <= n)
                    {
                        Res[i] += Convert.ToByte(0);
                    }
                    else
                    {
                        Res[i] += Convert.ToByte(1);
                    }
                }



                Fano1(L, n, p);

                Fano(n + 1, R, p);
            }


        }

        static void Fano1(int L, int R, double[] p)
        {
            int n;

            if (L < R)
            {

                n = Delenie_Posledovatelnosty(L, R, p);
                for (int i = L; i <= R; i++)
                {
                    if (i <= n)
                    {
                        Res[i] += Convert.ToByte(0);
                    }
                    else
                    {
                        Res[i] += Convert.ToByte(1);
                    }
                }

                Fano(L, n, p);

                Fano1(n + 1, R, p);
            }
        }
    }
}
