using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSM30Ultrassom
{
    public class GeraBitmap
    {
        public Bitmap ToBitmap(double[] rawImage)
        {
            int width = 60;
            int height = 60;

            Bitmap Image = new Bitmap(width, height);

            for (int i = 0; i < width * height; i++)
            {
                if (rawImage[i] < 0)
                    rawImage[i] *= -1;
                Color color = Color.FromArgb((int)rawImage[i]%255, (int)rawImage[i]%255, (int)rawImage[i]%255);
                Image.SetPixel(i / width, i % height, color);
            }

            Image.Save(@"C:\Users\alexismayfire\Desktop\CSM30\Trabalho 2\Imagem-A\Teste.bmp");
            return Image;
        }
    }
}
