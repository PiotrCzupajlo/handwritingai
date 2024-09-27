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
        public string filename { get; set; }
        public decimal sum { get; set; }
        public decimal percentageofbeing { get; set; }
        public int[,] wages { get; set; }
        public OutputPerceptron(string file,int size) {
            wages = new int[size, size];

            Bitmap mybitmap = new Bitmap(file);

            for (int i = 1; i < size; i++)
            {
                for (int j = 1; j < size; j++)
                {
                    wages[i,j] = mybitmap.GetPixel(i, j).R;

                }
            }


        }
    }
}
