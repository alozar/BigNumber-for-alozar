using System;
using System.Linq;
using System.Text;

namespace Interview
{
    /* Задача:
     * Необходимо реализовать класс BigNumber для работы с длинными числами:
     * - конструктор
     * - преобразование в строку
     * - оператор сложения

     * !Нельзя использовать готовые реализации длинных чисел

     * Требования к длинному числу:
     * - целое
     * - положительное
     * - произвольное число разрядов (может быть больше, чем допускает long)
     * Ограничения на строку - параметр конструктора BigNumber:
     * - содержит только цифры
     * - отсутствуют ведущие нули
     * 
     * Пример использования:
     * var a = new BigNumber("175872");
     * var b = new BigNumber("1234567890123456789012345678901234567890");
     * var r = a + b;
     * 
     * Для проверки решения необходимо запустить тесты.
     */

    public class BigNumber
    {
        // лучше хранить в обратном порядке
        private int[] digits;

        public int[] Digits {  get { return digits; } }

        public BigNumber(string x)
        {
            digits = ParseDigits(x);
        }

        public override string ToString()
        {
            var builder = new StringBuilder("");
            for (var i = digits.Length - 1;  0 < i + 1; i--)
            {
                builder.Append(digits[i].ToString());
            }
            return builder.ToString();
        }

        public static BigNumber operator +(BigNumber a, BigNumber b)
        {
            var builder = new StringBuilder("");
            var isFinal = false;
            var ind = 0;
            var tempDigit = 0;
            var curSum = 0;
            while (!isFinal)
            {
                curSum = tempDigit;

                if (ind < a.Digits.Length)
                {
                    curSum += a.Digits[ind];
                }

                if (ind < b.Digits.Length)
                {
                    curSum += b.Digits[ind];
                }

                ind++;
                isFinal = ind >= a.Digits.Length && ind >= b.Digits.Length;

                if (curSum > 9)
                {
                    var digit = curSum / 10;
                    builder.Append(digit);

                    tempDigit = curSum % 10;
                    if (isFinal)
                    {
                        builder.Append(tempDigit);
                    }
                }
                else
                {
                    builder.Append(curSum);
                }
            }
            return new BigNumber(builder.ToString().Reverse().ToString());
        }

        private int[] ParseDigits(string number)
        {
            if (number[0] == '0')
            {
                throw new Exception("Содержит ведущие нули!");
            }

            var digits = new int[number.Length];
            var digitInd = 0;
            for (var i = number.Length - 1; 0 < i + 1; i--, digitInd++)
            {
                if (int.TryParse(number[i].ToString(), out int digit))
                {
                    digits[digitInd] = digit;
                }
                else
                {
                    throw new Exception("Содержит не только цифры!");
                }
            }
            return digits;
        }
    }
}