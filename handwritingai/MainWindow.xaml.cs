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
using System.Security.Policy;

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
            

        }
        public (int,decimal) CallAi(int size,List<OutputPerceptron> outputPerceptrons,Bitmap mybitmap) {


            List<PerceptronBasicFuncionalities> inputs = new List<PerceptronBasicFuncionalities>();
            var task = Task.CompletedTask;
            for (int i = 9; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    PerceptronBasicFuncionalities inputperceptorom = new PerceptronBasicFuncionalities(mybitmap.GetPixel(i, j), outputPerceptrons, i, j);
                    inputs.Add(inputperceptorom);
                    task = inputperceptorom.Howmuchtrue();

                }
            }

            task.Wait();
            decimal max = -1;
            int detectednumber = 0;
            decimal error_cost = 0;
            foreach (OutputPerceptron item in outputPerceptrons)
            {
                item.percentageofbeing = item.sum / (size * size);
                if (max < item.percentageofbeing)
                {
                    max = item.percentageofbeing;
                    detectednumber = item.number;
                }
                error_cost += (1 - item.percentageofbeing) * (1 - item.percentageofbeing);
            }

            Lresult.Content = detectednumber.ToString();

            return (detectednumber, error_cost);
        }

        private void BStart_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                setup();
            }
        }

        private void BStartMore_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 1000; i++)
            {
                setup();
            }
        }
        public List<OutputPerceptron> setupPerceptrons(int size) {

            List<OutputPerceptron> outputPerceptrons = new List<OutputPerceptron>();

            outputPerceptrons.Add(new OutputPerceptron("1.jpg", size, 1));
            outputPerceptrons.Add(new OutputPerceptron("2.jpg", size, 2));
            outputPerceptrons.Add(new OutputPerceptron("3.jpg", size, 3));
            outputPerceptrons.Add(new OutputPerceptron("4.jpg", size, 4));
            outputPerceptrons.Add(new OutputPerceptron("5.jpg", size, 5));


            return outputPerceptrons;
        }
        public void setup() {
            int size = 28;
            List<OutputPerceptron> outputPerceptrons = setupPerceptrons(size);
            (List<Bitmap> bitmap,List<int> ints) tuple = setupbitmapimage();
            List<Bitmap> b = tuple.bitmap;
            List<int> expectations = tuple.ints;
            int tries = 0;
            int wins = 0;


            int k = b.Count;
            for (int i = 0; i < k; i++)
            {
                (int result, decimal error) result = CallAi(size, outputPerceptrons,b.ElementAt(i));
                Current_Error.Content = result.error.ToString();
                tries++;
                if (expectations.ElementAt(i) == result.result)
                {
                    wins++;
                }
                decimal winratio = wins/tries;
                Lresult.Content = winratio.ToString();



            }



        
        }
        public (List<Bitmap>,List<int> expected) setupbitmapimage() {
            List<Bitmap> bitmaps = new List<Bitmap>();
            List<int> ints = new List<int>();
            string workingDirectory = Environment.CurrentDirectory;
            Bitmap mybitmap = new Bitmap(workingDirectory + "\\1.png"); bitmaps.Add(mybitmap); ints.Add(1);
            mybitmap = new Bitmap(workingDirectory + "\\1a.jpg"); bitmaps.Add(mybitmap); ints.Add(1);


            return (bitmaps,ints);
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