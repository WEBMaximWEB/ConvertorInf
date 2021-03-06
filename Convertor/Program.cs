﻿using System;
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
                                                "e", "f", "g", "h", "i", "j", "k", "l", "<", ">",
                                             };

            Console.WriteLine("Если хотите перевести число в произвольную СС, нажмите 1." +
                              "Если хотите перевести число в римские числа, нажмите 2");
            int flag;
            while (!Int32.TryParse(Console.ReadLine(), out flag) || (flag != 1 && flag != 2))
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
                {
                    wholePart = j;
                    Console.WriteLine("Находим целую часть. Она равна " + wholePart + " знакам");
                }
            }
            if (wholePart == 0)
            {
                wholePart = strNumber.Length;
                Console.WriteLine("Это целое число.");
            }
            for (int i = 0; i < strNumber.Length; i++)
            {
                if (strNumber[i] == ',' || strNumber[i] == '.')
                {
                    k++;
                    continue;
                }

                int intNumber = Array.IndexOf(alphabet, strNumber[i].ToString());

                if (intNumber > 9)
                    Console.WriteLine("Символ " + strNumber[i] + "соответствует" + intNumber + 
                        " в десятичной СС");

                double x = Convert.ToDouble(intNumber) * Convert.ToDouble(Math.Pow(nBase, wholePart - 1 - i - k));

                double t = wholePart - 1 - i + k;
                Console.WriteLine(num10 + " = " + num10 + " + " + Convert.ToDouble(intNumber) + " * " + nBase + 
                                 "^" + t);
                num10 += x;
            }
            Console.WriteLine("Получаем десятичное число " + num10);

            if (wholePart == strNumber.Length)
            {
                return WholeNumber(num10, nBase, nOutBase, alphabet);
            }
            else
            {
                string WH;
                if (Math.Floor(num10) != 0)
                {
                    WH = WholeNumber(Convert.ToDouble(Math.Floor(num10)), nBase, nOutBase, alphabet);
                    WH += ",";
                    Console.WriteLine("Это целая часть:" + WH);
                }
                else
                {
                    WH = "0,";
                    Console.WriteLine("Целая часть равна нулю.");
                }
                num10 -= Math.Floor(num10);
                int flag = 0;
                string fractionalPart = "";

                Console.WriteLine("Далее умножаем число на " + nOutBase + 
                                " и выписываем целую часть в конечной СС");
                while (num10 - Math.Floor(num10) != 0 && flag < 10)
                {
                    Console.Write(fractionalPart + " + ");
                    fractionalPart += alphabet[Convert.ToInt32(Math.Floor(num10 * nOutBase))];
                    Console.WriteLine(fractionalPart);
                    num10 = (num10 * nOutBase) - Math.Floor(num10 * nOutBase);
                    flag++;
                }

                str = WH + fractionalPart;
                return str;
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
                    Console.WriteLine(num + 1000 + " - 1000 = " + num);
                    Console.WriteLine("Добавляем 'M'");
                }
                else if (num >= 900)
                {
                    num -= 900;
                    str += "CM";
                    Console.WriteLine(num + 900 + " - 900 = " + num);
                    Console.WriteLine("Добавляем 'CM'");
                }
                else if (num >= 500)
                {
                    num -= 500;
                    str += "D";
                    Console.WriteLine(num + 500 + " - 500 = " + num);
                    Console.WriteLine("Добавляем 'D'");
                }
                else if (num >= 400)
                {
                    num -= 400;
                    str += "CD";
                    Console.WriteLine(num + 400 + " - 400 = " + num);
                    Console.WriteLine("Добавляем 'CD'");
                }
                else if (num >= 100)
                {
                    num -= 100;
                    str += "C";
                    Console.WriteLine(num + 100 + " - 100 = " + num);
                    Console.WriteLine("Добавляем 'C'");
                }
                else if (num >= 90)
                {
                    num -= 90;
                    str += "XC";
                    Console.WriteLine(num + 90 + " - 90 = " + num);
                    Console.WriteLine("Добавляем 'XC'");
                }
                else if (num >= 50)
                {
                    num -= 50;
                    str += "L";
                    Console.WriteLine(num + 50 + " - 50 = " + num);
                    Console.WriteLine("Добавляем 'L'");
                }
                else if (num >= 40)
                {
                    num -= 40;
                    str += "XL";
                    Console.WriteLine(num + 40 + " - 40 = " + num);
                    Console.WriteLine("Добавляем 'XL'");
                }
                else if (num >= 10)
                {
                    num -= 10;
                    str += "X";
                    Console.WriteLine(num + 10 + " - 10 = " + num);
                    Console.WriteLine("Добавляем 'X'");
                }
                else if (num >= 9)
                {
                    num -= 9;
                    str += "IX";
                    Console.WriteLine(num + 9 + " - 9 = " + num);
                    Console.WriteLine("Добавляем 'IX'");
                }
                else if (num >= 5)
                {
                    num -= 5;
                    str += "V";
                    Console.WriteLine(num + 5 + " - 5 = " + num);
                    Console.WriteLine("Добавляем 'V'");
                }
                else if (num >= 4)
                {
                    num -= 4;
                    str += "IV";
                    Console.WriteLine(num + 4 + " - 4 = " + num);
                    Console.WriteLine("Добавляем 'IV'");
                }
                else if (num >= 3)
                {
                    num -= 3;
                    str += "III";
                    Console.WriteLine(num + 3 + " - 3 = " + num);
                    Console.WriteLine("Добавляем 'III'");
                }
                else if (num >= 2)
                {
                    num -= 2;
                    str += "II";
                    Console.WriteLine(num + 2 + " - 2 = " + num);
                    Console.WriteLine("Добавляем 'II'");
                }
                else if (num >= 1)
                {
                    num -= 1;
                    str += "I";
                    Console.WriteLine(num + 1 + " - 1 = " + num);
                    Console.WriteLine("Добавляем 'I'");
                }
            }
            return str;
        }

        static string WholeNumber( double num10, int nBase, int nOutBase, string[] alphabet)
        {
            int intNum10 = Convert.ToInt32(num10);
            string str = "";

            Console.WriteLine("Выполняем перевод десятичного числа в число с основанием " + nOutBase);

            while (intNum10 != 0)
            {
                Console.WriteLine(str + " + остаток от деления числа " + intNum10 + " на " + nOutBase);
                str += alphabet[(intNum10 % nOutBase)];
                decimal x = (intNum10 - (intNum10 % nOutBase)) / nOutBase;
                intNum10 = Convert.ToInt32(Math.Floor(x));
            }
            return str = new string(str.Reverse().ToArray());
        }
    }
}
