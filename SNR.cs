using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SNR
{
    class SNR
    {
        double E = 1E-10;
        double c1 = 1;
        double c2 = 1;
        int M = 5;
        double [][]Y1;
        double []U;
        double []B;
        double []dU;
        int iter = 0;
        public void run()
        {
            //Y1 yakobi
            Y1 = new double[M][];
            for (int i = 0; i < M; i++)
                Y1[i] = new double[M];
            //B
            B = new double[M];
            for (int i = 0; i < M; i++)
                B[i] = 0;
            Console.WriteLine("Um-1+c1*Um+Um+1+c2*Um^3=0\n");
            Console.WriteLine("M = " + M + "\nc1 = " + c1 + "\nc2 = " + c2 + "\neps = " + E+"\n");
            nabl();
            while (anyW())
            {
                iter++;
                initY();
                //print(Y1);
                calc_dU();
                calc_U();
            }
            print(U);
            Console.WriteLine("podstanovka v system: \n");
            test();
            Console.WriteLine("~0");
            Console.WriteLine("\niteration count: "+iter);
        }


        private void test()
        {
            for (int i = 1; i < M+1; i++)
            {
                double r = U[i - 1] + c1 * U[i] + U[i + 1] + c2 * U[i] * U[i] * U[i];
                Console.Write(r + " = ");
            }
        }

        private bool anyW()
        {
            for (int i = 0; i < M; i++)
                if (w(i+1) > E)
                    return true;
            return false;
        }

        private void nabl()
        {
            U = new double[M+2];
            dU = new double[M+2];
            for (int i = 1; i < M+1; i++)
            {
                U[i] = 1;
                dU[i] = 1;
            }
            U[0] = 0;
            U[M+1] = 0;
            dU[0] = 0;
            dU[M + 1] = 0;
        }

        private void calc_U()
        {
            for (int i = 1; i < M+1; i++)
                U[i] += dU[i];

        }
        //SLAR
        //yakobi
        private void calc_dU()
        {
            for (int i = 0; i < M; i++)
                B[i] = -w(i + 1);
       
            //calc dU
            double[] c = new double[M];
            double[] a = new double[M-1];
            double[] b = new double[M-1];
            for (int i = 0; i < M;i++ )
            {
                c[i] = Y1[i][i];
                if (i < M - 1) 
                    a[i]=Y1[i][i+1];
                if (i > 0)
                    b[i-1] = Y1[i][i-1];
            }
            double []r =   MetodProgonki.solveMatrix(M, a, c, b, B);
            for(int i = 0; i < M; i++)
                dU[i+1]=r[i];
        }


        private void initY()
        {
            for (int i = 0; i < M; i++)
                for (int j = 0; j < M; j++)
                    Y1[i][j] = 0;

            for (int i = 0; i < M; i++)
            {
                Y1[i][i] = dk(i);//c1+3*c2*ui^2
                if(i<M-1)
                    Y1[i][i + 1] = 1;
                if (i > 0)
                    Y1[i][i - 1] = 1;
            }
        }
        //w nevazki 
        // i >= 1
        private double w(int i)
        {
            return U[i - 1] + c1 * U[i] + U[i + 1] + c2 * U[i] * U[i] * U[i];
        }
        
        //c1+3*c2*ui^2
        private double dk(int i)
        {
            return c1 + 3 * c2 * U[i + 1] * U[i + 1];
        }



        public void print(double[][] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < a[i].Length; j++)
                    Console.Write(a[i][j] + " ");
                Console.WriteLine();
            }
        }
        public void print(double[] a)
        {
            for (int i = 1; i < a.Length-1; i++)
                Console.Write("U"+i+"= " + a[i] + "\n");
            Console.WriteLine();
        }
    }
}
