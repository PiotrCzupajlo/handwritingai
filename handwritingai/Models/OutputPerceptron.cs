using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace handwritingai.Models
{
    public class OutputPerceptron
    {
        public int number { get; set; }
        public string filename { get; set; }
        public decimal sum { get; set; }
        public decimal percentageofbeing { get; set; }
        public int[,] wages { get; set; }
        public int[,] wagesold { get; set; }
        public int size;
        public OutputPerceptron(string file,int size,int n) {
            wages = new int[size, size];
            wagesold = new int[size, size];
            number = n;
            filename = file;
            this.size = size;
            using (Bitmap mybitmap = new Bitmap(file))
            {

                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        wages[i, j] = mybitmap.GetPixel(i, j).R;
                        wagesold[i, j] = wages[i,j];
                    }
                }
            }



        }

        
            public void MakeAChange(){
            Random random = new Random();
            int randomx = random.Next(0,28);
            int randomy = random.Next(0,28);
            int randomvaluechange = random.Next(1,10);
            int randommode = random.Next(0,2);
            make_equal();
            if(randommode==0)
            {
                if(wages[randomx,randomy]+randomvaluechange<255)
                wages[randomx,randomy]+=randomvaluechange;
            }
            if(randommode==1)
            {
                if(wages[randomx,randomy]-randomvaluechange>0)
                wages[randomx,randomy]-= randomvaluechange;
            }
        }
        public void reshuffle(int size)
        {
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    wages[i, j] = random.Next(0, 255);

                }

            }
            make_equal();
        }

        public void save(int size) {
            Bitmap bitmap = new Bitmap(size,size);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                { 
                System.Drawing.Color currentcolor = System.Drawing.Color.FromArgb(255, Convert.ToInt32(wages[i,j]),0,255);
                bitmap.SetPixel(i, j, currentcolor);
                    string workingDirectory = Environment.CurrentDirectory;
                    File.Delete(filename);
                    
                    bitmap.Save(number+"_templatenew.png",ImageFormat.Png);
                    File.Move(number + "_templatenew.png", number+"_template.png");


                }
            
            }
        }
        public void make_equal() {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    wagesold[i, j] = wages[i, j];
                
                }
            
            }
        
        
        }
        public void reset() {
            sum = 0;
            percentageofbeing = 0;
        
        }

/*
Bitmap testbitmap = new Bitmap("test.jpg");
System.Drawing.Color testcolor = testbitmap.GetPixel(0,0);
System.Drawing.Color newtestcolor = System.Drawing.Color.FromArgb(0, testcolor.R + 200,testcolor.G,testcolor.B);
testbitmap.SetPixel(0,0,newtestcolor);
testbitmap.Save("test1.png", ImageFormat.Png);
*/
    }
}
