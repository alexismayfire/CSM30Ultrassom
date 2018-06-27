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
                //var gFile = @"C:\Users\alexismayfire\Desktop\CSM30\Trabalho 2\Imagem-A\g-1.txt";
                //var hFile = @"C:\Users\alexismayfire\Desktop\CSM30\Trabalho 2\Imagem-A\H-1.txt";
                var gFile = @"C:\Users\alexismayfire\Desktop\CSM30\Trabalho 2\Imagem-B\g-1.txt";
                var hFile = @"C:\Users\alexismayfire\Desktop\CSM30\Trabalho 2\Imagem-B\H-1.txt";


                var M = Matrix<double>.Build;
                var V = Vector<double>.Build;

                //int rows = 50816;
                //int columns = 3600;
                int rows = 10;
                int columns = 6;

                StreamReader sr = new StreamReader(gFile);

                var g = V.Dense(rows);
                Matrix<double> h = M.Sparse(rows, columns);

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

                    g = V.Dense(temp);
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
                    h = DelimitedReader.Read<double>(sr, false, ",", false, null, null);
                    Console.WriteLine("Arquivo H lido");
                    Console.WriteLine(DateTime.Now);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }

                sr.Dispose();

                Vector<double> f = V.Dense(rows, 0);
                try
                {
                    var r0_temp = h.Multiply(f);
                }
                catch(Exception e)
                {
                    /* 
                     * A exceção gerada é "Matrix dimensions must agree",
                     * porque o operador é 10x6 e o operando 10x1.
                     * Só que tem uma sobrecarga que aceita Vector, como rightSide.
                     * E é exatamente o rightSide que aparece na mensagem, então wtf?? 
                    */
                    Console.WriteLine("Erro");
                    Console.WriteLine(e.Message);
                }
                
                /*
                 * Aqui seriam as operações de inicialização lá.
                */
                //var r0 = g.Subtract(h.Multiply(f));
                //var p0 = h.Transpose().Multiply(r0);

                //var r0_T = r0.ToRowMatrix();
                //var alfa_upper = r0_T.Multiply(r0);
                //var p0_T = p0.ToRowMatrix();
                //var alfa_down = p0_T.Multiply(p0);
                
                //var alfa = alfa_upper / alfa_down;

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