using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SNR
{
    class Program
    {

        static void Main(string[] args)
        {
            SNR a = new SNR();
        a.run();
            Console.Read();
        }
        static void print(double[] a)
        {
            for (int i = 0; i < a.Length; i++)
                Console.Write(a[i] + "\n");
            Console.WriteLine();
        }
    }
}
