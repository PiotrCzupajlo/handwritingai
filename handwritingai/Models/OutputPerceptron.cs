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
        public decimal[,] wagesold { get; set; }
        public OutputPerceptron(string file,int size,int n) {
            wages = new decimal[size, size];
            number = n;
            Bitmap mybitmap = new Bitmap(file);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    wages[i,j] =  Decimal.Divide(mybitmap.GetPixel(i, j).R, 255);
                    wagesold[i,j]=0;
                }
            }


        }
            public void MakeAChange(){
            int randomx = Random.Next(0,28);
            int randomy = Random.Next(0,28);
            int randomvaluechange = Random.Next(0,10);
            int randommode = Random.Next(0,2);
            wagesold[randomx,randomy]=wages[randomx,randomy];
            if(randommode==0)
            {
                if(wages[randomx,randomy]+randomvaluechange<255)
                wages[randomx,randomy]+=randomvaluechange;
            }
            if(randommode==1)
            {
                if(wages[randomx,randomy]-randomvaluechange>0)
                wages[randomx,randomy]-=randomvaluechange;
            }
        }
        public void reshuffle(int size){
            for(int i=0;i<size;i++)
            {
                for(int j=0;j<size;j++)
                {
                    wages[i,j]=Random.Next(0,255);

                }

            }
            public void redo(int size){
                for(int i=0;i<size;i++)
                {
                    for(int j=0;j<size;j++)
                    {
                    if(wagesold[i,j]!=0)
                    {
                        wages[i,j]=wagesold[i,j];
                        wagesold[i,j]=0;
                        j=size;
                        i=size;
                        
                    }
                    }

                }
            }



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
