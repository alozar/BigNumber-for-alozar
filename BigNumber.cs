using System;
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

        public BigNumber(string x)
        {
            digits = ParseDigits(x);
        }

        public BigNumber(int[] digits)
        {
            this.digits = digits;
        }


        public int Length { get => digits.Length; }

        public int this[int index]
        {
            get => digits[index];
        }

        public override string ToString()
        {
            var builder = new StringBuilder(digits.Length);
            for (var i = digits.Length - 1; i >= 0; i--)
            {
                builder.Append(digits[i].ToString());
            }
            return builder.ToString();
        }

        public static BigNumber operator +(BigNumber a, BigNumber b)
        {
            //Больший из двух разрядов чисел
            var maxNumberLength = a.Length < b.Length ? b.Length : a.Length;
            var digits = new int[maxNumberLength];
            var ind = 0;
            var curSum = 0;
            while (true)
            {
                if (ind < a.Length)
                {
                    curSum += a[ind];
                }

                if (ind < b.Length)
                {
                    curSum += b[ind];
                }

                digits[ind] = curSum % 10;
                ind++;

                // Если больше 9, то 1 в уме и переносим ее в следующий разряд
                curSum = curSum > 9 ? 1 : 0;

                if (ind >= maxNumberLength)
                {
                    if (curSum == 1)
                    {
                        Array.Resize(ref digits, digits.Length + 1);
                        digits[ind] = 1;
                    }
                    break;
                }
            }

            return new BigNumber(digits);
        }

        private int[] ParseDigits(string number)
        {
            var digits = new int[number.Length];
            // Считываем строку с послденего индекса
            for (var i = number.Length - 1; i >= 0; i--)
            {
                // Записываем в массив с 0 индекса
                digits[number.Length - 1 - i] = number[i] - '0';
            }
            return digits;
        }
    }
}