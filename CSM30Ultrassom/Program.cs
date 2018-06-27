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
                //var gFile = @"C:\Users\alexismayfire\Desktop\CSM30\Trabalho 2\Imagem-B\g-1.txt";
                //var hFile = @"C:\Users\alexismayfire\Desktop\CSM30\Trabalho 2\Imagem-B\H-1.txt";


                var M = Matrix<double>.Build;
                var V = Vector<double>.Build;

                //int rows = 50816;
                //int columns = 3600;
                int rows = 50816;
                int columns = 3600;

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
                
                /*
                 * Aqui seriam as operações de inicialização lá.
                */
                var r = g;
                Vector<double> r_aux;
                var p = h.Transpose().Multiply(r);
                Vector<double> p_aux;

                Vector<double> f = V.Dense(columns);
                Vector<double> f_aux = V.Dense(columns);

                for(int i = 0; i < 15; i++)
                {
                    var r_T = r.ToRowMatrix();
                    var alfa_upper = r_T.Multiply(r);
                    var p_T = p.ToRowMatrix();
                    var alfa_down = p_T.Multiply(p);

                    var alfa = alfa_upper.PointwiseDivide(alfa_down);

                    //f = f_aux;
                    double alfa_scalar = alfa.Single();
                    f_aux += f.Add(p.Multiply(alfa_scalar));
                    var temp = h.Multiply(alfa_scalar);
                    r_aux = r.Subtract(temp.Multiply(p));

                    var r_auxT = r_aux.ToRowMatrix();
                    var beta_upper = r_auxT.Multiply(r_aux);

                    var beta = beta_upper.PointwiseDivide(alfa_upper);

                    double beta_scalar = beta.Single();
                    p_aux = h.Transpose().Multiply(r_aux);
                    var pplus = p_aux.Add(p.Multiply(beta_scalar));
                    p = pplus;
                    r = r_aux;

                }
                 
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