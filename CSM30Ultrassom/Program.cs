using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Data.Text;


namespace CSM30Trabalho2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var gFile = @"C:\Users\alexismayfire\Desktop\CSM30\Trabalho 2\Imagem-A\g-1.txt";
                var hFile = @"C:\Users\alexismayfire\Desktop\CSM30\Trabalho 2\Imagem-A\H-1.txt";

                var M = Matrix<double>.Build;
                var V = Vector<double>.Build;

                int rows = 50816;
                int columns = 3600;

                StreamReader sr = new StreamReader(gFile);

                try
                {
                    Console.WriteLine(DateTime.Now);

                    double[] temp = new double[rows];
                    using (sr)
                    {
                        string line;
                        int i = 0;
                        while ((line = sr.ReadLine()) != null)
                        {
                            temp[i] = Double.Parse(line);
                            i++;
                        }
                    }

                    var v = V.Dense(temp);
                    Console.WriteLine("Arquivo g lido");
                    Console.WriteLine(DateTime.Now);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }

                sr.Dispose();
                sr = new StreamReader(hFile);

                try
                {
                    Console.WriteLine(DateTime.Now);
                    Matrix<double> matrix = M.Sparse(rows, columns);
                    matrix = DelimitedReader.Read<double>(sr, false, ",", false, null, null);
                    Console.WriteLine("Arquivo H lido");
                    Console.WriteLine(DateTime.Now);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }

                sr.Dispose();
                Console.Read();
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
    }
}