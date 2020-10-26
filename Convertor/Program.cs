using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Convertor
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] alphabet = new string[] {
                                                "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
                                                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J",
                                                "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T",
                                                "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d",
                                                "e", "f", "g", "h", "i", "j", "k", "l", "m", "n",
                                             };

            Console.WriteLine("Если хотите перевести число в произвольную СС, нажмите 1." +
                              "Если хотите перевести число в римские числа, нажмите 2");
            int flag;
            while (!Int32.TryParse(Console.ReadLine(), out flag))
                Console.Write("Введите одну цыфру. '1' или '2': ");
            
            if (flag == 1)
            {
                Console.Write("Введите число, которое неободимо конвертировать:");
                string number = Console.ReadLine();

                int nBase;
                Console.Write("Введите его основание:");
                while (!Int32.TryParse(Console.ReadLine(), out nBase))
                    Console.WriteLine("Основание не может быть дробью. Необходимо ввести целое число:");

                int nOutBase;
                Console.Write("Укажите основание результата:");
                while (!Int32.TryParse(Console.ReadLine(), out nOutBase))
                    Console.WriteLine("Необходимо ввести целое число:");

                Console.WriteLine(Convertor(number, nBase, nOutBase, alphabet));
            }
            else if (flag == 2)
            {
                Console.Write("Введите целое число:");
                int number;
                while (!Int32.TryParse(Console.ReadLine(), out number))
                    Console.WriteLine("Введите целое число:");
                Console.WriteLine(Caesar(number));
            }
        }

        static string Convertor(string strNumber, int nBase, int nOutBase, string[] alphabet)
        {
            double num10 = 0, wholePart = 0;
            string str = "";
            int k = 0;

            for (int j = 1; j < strNumber.Length; j++)
            {
                if (strNumber[j] == ',' || strNumber[j] == '.')
                    wholePart = j;
            }
            if (wholePart == 0)
                wholePart = strNumber.Length;
            for (int i = 0; i < strNumber.Length; i++)
            {
                if (strNumber[i] == ',' || strNumber[i] == '.')
                {
                    k++;
                    continue;
                }

                int intNumber = Array.IndexOf(alphabet, strNumber[i].ToString());
                double x = Convert.ToDouble(intNumber) * Convert.ToDouble(Math.Pow(nBase, wholePart - 1 - i + k));
                num10 += x;
            }
            if (nOutBase == 10)
                return num10.ToString();


            if (wholePart == strNumber.Length)
            {
                return WholeNumber(num10, nBase, nOutBase, alphabet);
            }
            else
            {
                int WH;
                if (Math.Floor(num10) != 0)
                {
                    WH = Int32.Parse(WholeNumber(Convert.ToDouble(Math.Floor(num10)),
                                                    nBase, nOutBase, alphabet));
                }
                else WH = 0;
                num10 -= Math.Floor(num10);
                int flag = 0;
                string fractionalPart = "0,";

                while (num10 - Math.Floor(num10) != 0 && flag < 10)
                {
                    fractionalPart +=  Math.Floor(num10 * nOutBase).ToString();
                    num10 = (num10 * nOutBase) - Math.Floor(num10 * nOutBase);
                    flag++;
                }

                double result = WH + Convert.ToDouble(fractionalPart);
                return result.ToString();
            }
        }

        static string Caesar(int num)
        {
            string str = "";
            while (num > 0)
            {
                if (num >= 1000)
                {
                    num -= 1000;
                    str += "M";
                }
                else if (num >= 900)
                {
                    num -= 900;
                    str += "CM";
                }
                else if (num >= 500)
                {
                    num -= 500;
                    str += "D";
                }
                else if (num >= 400)
                {
                    num -= 400;
                    str += "CD";
                }
                else if (num >= 100)
                {
                    num -= 100;
                    str += "C";
                }
                else if (num >= 90)
                {
                    num -= 90;
                    str += "XC";
                }
                else if (num >= 50)
                {
                    num -= 50;
                    str += "L";
                }
                else if (num >= 40)
                {
                    num -= 40;
                    str += "XL";
                }
                else if (num >= 10)
                {
                    num -= 10;
                    str += "X";
                }
                else if (num >= 9)
                {
                    num -= 9;
                    str += "IX";
                }
                else if (num >= 5)
                {
                    num -= 5;
                    str += "V";
                }
                else if (num >= 4)
                {
                    num -= 4;
                    str += "IV";
                }
                else if (num >= 3)
                {
                    num -= 3;
                    str += "III";
                }
                else if (num >= 2)
                {
                    num -= 2;
                    str += "II";
                }
                else if (num >= 1)
                {
                    num -= 1;
                    str += "I";
                }
            }
            return str;
        }

        static string WholeNumber( double num10, int nBase, int nOutBase, string[] alphabet)
        {
            int intNum10 = Convert.ToInt32(num10);
            string str = "";
            while (intNum10 != 0)
            {
                str += alphabet[(intNum10 % nOutBase)];
                decimal x = (intNum10 - (intNum10 % nOutBase)) / nOutBase;
                intNum10 = Convert.ToInt32(Math.Floor(x));
            }
            return str = new string(str.Reverse().ToArray());
        }
    }
}
