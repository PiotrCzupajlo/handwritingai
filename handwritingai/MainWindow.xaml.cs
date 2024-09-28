using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System;
using handwritingai.Models;
using System.IO;
using System.Drawing.Imaging;

namespace handwritingai
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            int size = 28;
            List<OutputPerceptron> outputPerceptrons = new List<OutputPerceptron>();

            outputPerceptrons.Add(new OutputPerceptron("1.jpg",size,1));
            outputPerceptrons.Add(new OutputPerceptron("2.jpg",size,2));
            outputPerceptrons.Add(new OutputPerceptron("3.jpg",size,3));
            outputPerceptrons.Add(new OutputPerceptron("4.jpg", size, 4));
            outputPerceptrons.Add(new OutputPerceptron("5.jpg", size, 5));


            /*
            Bitmap testbitmap = new Bitmap("test.jpg");
            System.Drawing.Color testcolor = testbitmap.GetPixel(0,0);
            System.Drawing.Color newtestcolor = System.Drawing.Color.FromArgb(0, testcolor.R + 200,testcolor.G,testcolor.B);
            testbitmap.SetPixel(0,0,newtestcolor);
            testbitmap.Save("test1.png", ImageFormat.Png);
            */


            string workingDirectory = Environment.CurrentDirectory;
            string imagetorecognize = workingDirectory + "\\5.png";
            Bitmap mybitmap = new Bitmap(imagetorecognize);

            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(imagetorecognize);
            bi.EndInit();
            IImagetodetection.Source = bi;

            List<PerceptronBasicFuncionalities> inputs = new List<PerceptronBasicFuncionalities>();
            var task = Task.CompletedTask;
            for (int i = 9; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    PerceptronBasicFuncionalities inputperceptorom = new PerceptronBasicFuncionalities(mybitmap.GetPixel(i, j),outputPerceptrons,i,j);
                    inputs.Add(inputperceptorom);
                    task =  inputperceptorom.Howmuchtrue();
                    
                }
            }
            task.Wait();
            decimal max = -1;
            int detectednumber = 0;
            foreach (OutputPerceptron item in outputPerceptrons)
            {
                item.percentageofbeing = item.sum / (size * size);
                if (max < item.percentageofbeing)
                {
                    max = item.percentageofbeing;
                    detectednumber = item.number;
                }
            }
            Lresult.Content = detectednumber.ToString();

        }

    }
}