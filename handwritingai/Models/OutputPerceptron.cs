using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace handwritingai.Models
{
    public class OutputPerceptron
    {
        public int number { get; set; }
        public string filename { get; set; }
        public decimal sum { get; set; }
        public decimal percentageofbeing { get; set; }
        public decimal[,] wages { get; set; }
        public OutputPerceptron(string file,int size,int n) {
            wages = new decimal[size, size];
            number = n;
            Bitmap mybitmap = new Bitmap(file);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    wages[i,j] = Decimal.Divide(mybitmap.GetPixel(i, j).R, 255);

                }
            }


        }
    }
}
