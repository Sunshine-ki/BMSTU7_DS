using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_04
{
    class RSA
    {
        static Random r = new Random(1);

        const int start = 1000;
        const int stop = 9000;

        int publicKey;
        int privateKey;
        int n;

        public RSA()
        {
            int p = GetRandomPrimeNum();
            int q = GetRandomPrimeNum();
            
            n = p * q; // модуль

            // Вычисляем функцию Эйлера от числа n.
            // Значение этой функции Fi(n) равно количеству натуральных чисел, не превышающих n и взаимно простых с ним.
            int fi = (p - 1) * (q - 1);

            // Выбираем открытую экспененту (e).
            // e и n публичный ключ. 
            publicKey = ComputePublicKey(fi);
            
            // Вычисляем секретную экспоненту (d).
            // d и n закрытый ключ.
            privateKey = ComputePrivateKey(publicKey, fi);
        }

        /// <summary>
        /// Шифрование
        /// </summary>
        /// <param name="m">Message</param>
        /// <returns>c</returns>
        public int Encrypt(int m)
        {
            // ( m ^ publicKey ) mod n
            return (int)BigInteger.ModPow(m, publicKey, n);
        }

        /// <summary>
        /// Расшифровка
        /// </summary>
        /// <param name="c"></param>
        /// <returns>m</returns>
        public int Decrypt(int c)
        {
            // ( c ^ privateKey ) mod n
            return (int)BigInteger.ModPow(c, privateKey, n);
        }

        static int GetRandomPrimeNum()
        {
            int num = r.Next(start, stop);

            while (!MyMath.IsNumberPrimePrecise(num))
                num = r.Next(start, stop);

            return num;
        }

        /// <summary>
        /// Выбирает целое число e, такое что: 1 < e < fi(n) и 
        /// НОД(fi, e) == 1 
        /// </summary>
        /// <param name="fi"></param>
        /// <returns>Взаимно простое с fi число</returns>
        static int ComputePublicKey(int fi)
        {
            int nod = 0;
            int num = -1;
            while (nod != 1)
            {
                num = r.Next(2, fi);
                nod = MyMath.GreatestCommonDivisor(fi, num); // нод
            }
            return num;
        }

        /// <summary>
        /// (e * d) mod(fi) = 1
        /// </summary>
        /// <returns>d</returns>
        static int ComputePrivateKey(int e, int fi)
        {
            return MyMath.Inverse(e, fi);
        }
    }
}
