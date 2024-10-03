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
        List<OutputPerceptron> outputsPerceptrons;
        List<OutputPerceptron> backup;
        int size;
        (List<Bitmap> bitmap, List<int> ints) tuple;
        public MainWindow()
        {
            InitializeComponent();
            tuple = setupbitmapimage();
            size = 28;
            outputsPerceptrons = setupPerceptrons(size);
            
        }
        public (int, decimal) CallAi(int size, Bitmap mybitmap)
        {


            List<PerceptronBasicFuncionalities> inputs = new List<PerceptronBasicFuncionalities>();
            var task = Task.CompletedTask;
            for (int i = 9; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    PerceptronBasicFuncionalities inputperceptorom = new PerceptronBasicFuncionalities(mybitmap.GetPixel(i, j), outputsPerceptrons, i, j);
                    inputs.Add(inputperceptorom);
                    task = inputperceptorom.Howmuchtrue();

                }
            }

            task.Wait();
            decimal max = -1;
            int detectednumber = 0;
            decimal error_cost = 0;
            foreach (OutputPerceptron item in outputsPerceptrons)
            {
                item.percentageofbeing = item.sum / (size * size);
                if (max < item.percentageofbeing)
                {
                    max = item.percentageofbeing;
                    detectednumber = item.number;
                }
                error_cost += (1 - item.percentageofbeing) * (1 - item.percentageofbeing);
            }
            foreach (OutputPerceptron output in outputsPerceptrons)
            {
                output.reset();
            }
            Lresult.Content = detectednumber.ToString();

            return (detectednumber, error_cost);
        }
        private void BStartLess_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 2; i++)
            {
                decimal error = setup();
                Current_Error.Content = error.ToString();
                int counter = Convert.ToInt32(L_counter.Content);
                L_counter.Content = counter + 1;
            }
        }
        private void BStart_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                decimal error = setup();
                Current_Error.Content = error.ToString();
                int counter=Convert.ToInt32(L_counter.Content);
                L_counter.Content = counter+1;
            }
        }

        private void BStartMore_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 1000; i++)
            {
                decimal error = setup();
                Current_Error.Content = error.ToString();
                int counter = Convert.ToInt32(L_counter.Content);
                L_counter.Content = counter + 1;
            }
        }
        public List<OutputPerceptron> setupPerceptrons(int size)
        {

            List<OutputPerceptron> outputPerceptrons = new List<OutputPerceptron>();

            outputPerceptrons.Add(new OutputPerceptron("1_template.png", size, 1));
            outputPerceptrons.Add(new OutputPerceptron("2_template.png", size, 2));
            outputPerceptrons.Add(new OutputPerceptron("3_template.png", size, 3));
            outputPerceptrons.Add(new OutputPerceptron("4_template.png", size, 4));
            outputPerceptrons.Add(new OutputPerceptron("5_template.png", size, 5));


            return outputPerceptrons;
        }
        public decimal setup()
        {
            

            
            List<Bitmap> b = tuple.bitmap;
            List<int> expectations = tuple.ints;
            int tries = 0;
            int wins = 0;


            int k = b.Count;
            decimal currenterror = 0;
            for (int i = 0; i < k; i++)
            {

                (int result, decimal error) result = CallAi(size, b.ElementAt(i));
                
                currenterror += result.error;
                tries++;
                if (expectations.ElementAt(i) == result.result)
                {
                    wins++;
                }
                decimal winratio = wins / tries;
                Lresult.Content = winratio.ToString();

            }
            backup = new List<OutputPerceptron>(outputsPerceptrons) ;

            foreach (OutputPerceptron output in outputsPerceptrons)
            {
                output.MakeAChange();


                decimal newerror = 0;

                for (int i = 0; i < k; i++)
                {
                    (int result, decimal error) result = CallAi(size, b.ElementAt(i));
                    newerror += result.error;
                }

                if (currenterror >= newerror)
                {
                    currenterror = newerror;
                    //backup = new List<OutputPerceptron>(outputsPerceptrons);
                    output.save(size);

                }
                else
                {
                    outputsPerceptrons = setupPerceptrons(size);
                }
            }
            return currenterror;
        }


        private void restart_btn_Click(object sender, RoutedEventArgs e)
        {
            
            List<OutputPerceptron> outputPerceptrons = outputsPerceptrons;
            foreach (OutputPerceptron output in outputPerceptrons)
            {
                output.reshuffle(size);
                
            }

        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            
            foreach (OutputPerceptron output in outputsPerceptrons)
            {
                output.save(size);
            }
        }

        private void check_btn_Click(object sender, RoutedEventArgs e)
        {
            (int result, decimal error) result = CallAi(size, tuple.bitmap.ElementAt(38));
            L_test.Content = result.error;
        }



        public (List<Bitmap>, List<int> expected) setupbitmapimage()
        {
            List<Bitmap> bitmaps = new List<Bitmap>();
            List<int> ints = new List<int>();
            string workingDirectory = Environment.CurrentDirectory;
            Bitmap mybitmap = new Bitmap(workingDirectory + "\\1.png"); bitmaps.Add(mybitmap); ints.Add(1);
            mybitmap = new Bitmap(workingDirectory + "\\1a.jpg"); bitmaps.Add(mybitmap); ints.Add(1);
            mybitmap = new Bitmap(workingDirectory + "\\1b.jpg"); bitmaps.Add(mybitmap); ints.Add(1);
            mybitmap = new Bitmap(workingDirectory + "\\1c.jpg"); bitmaps.Add(mybitmap); ints.Add(1);
            mybitmap = new Bitmap(workingDirectory + "\\1d.jpg"); bitmaps.Add(mybitmap); ints.Add(1);
            mybitmap = new Bitmap(workingDirectory + "\\1e.jpg"); bitmaps.Add(mybitmap); ints.Add(1);
            mybitmap = new Bitmap(workingDirectory + "\\1f.jpg"); bitmaps.Add(mybitmap); ints.Add(1);
            mybitmap = new Bitmap(workingDirectory + "\\1g.jpg"); bitmaps.Add(mybitmap); ints.Add(1);
            mybitmap = new Bitmap(workingDirectory + "\\1h.jpg"); bitmaps.Add(mybitmap); ints.Add(1);
            mybitmap = new Bitmap(workingDirectory + "\\1i.jpg"); bitmaps.Add(mybitmap); ints.Add(1);
            mybitmap = new Bitmap(workingDirectory + "\\2a.jpg"); bitmaps.Add(mybitmap); ints.Add(2);
            mybitmap = new Bitmap(workingDirectory + "\\2b.jpg"); bitmaps.Add(mybitmap); ints.Add(2);
            mybitmap = new Bitmap(workingDirectory + "\\2c.jpg"); bitmaps.Add(mybitmap); ints.Add(2);
            mybitmap = new Bitmap(workingDirectory + "\\2d.jpg"); bitmaps.Add(mybitmap); ints.Add(2);
            mybitmap = new Bitmap(workingDirectory + "\\2e.jpg"); bitmaps.Add(mybitmap); ints.Add(2);
            mybitmap = new Bitmap(workingDirectory + "\\2f.jpg"); bitmaps.Add(mybitmap); ints.Add(2);
            mybitmap = new Bitmap(workingDirectory + "\\2g.jpg"); bitmaps.Add(mybitmap); ints.Add(2);
            mybitmap = new Bitmap(workingDirectory + "\\2h.jpg"); bitmaps.Add(mybitmap); ints.Add(2);
            mybitmap = new Bitmap(workingDirectory + "\\2i.jpg"); bitmaps.Add(mybitmap); ints.Add(2);
            mybitmap = new Bitmap(workingDirectory + "\\3a.jpg"); bitmaps.Add(mybitmap); ints.Add(3);
            mybitmap = new Bitmap(workingDirectory + "\\3b.jpg"); bitmaps.Add(mybitmap); ints.Add(3);
            mybitmap = new Bitmap(workingDirectory + "\\3c.jpg"); bitmaps.Add(mybitmap); ints.Add(3);
            mybitmap = new Bitmap(workingDirectory + "\\3d.jpg"); bitmaps.Add(mybitmap); ints.Add(3);
            mybitmap = new Bitmap(workingDirectory + "\\3e.jpg"); bitmaps.Add(mybitmap); ints.Add(3);
            mybitmap = new Bitmap(workingDirectory + "\\3f.jpg"); bitmaps.Add(mybitmap); ints.Add(3);
            mybitmap = new Bitmap(workingDirectory + "\\3g.jpg"); bitmaps.Add(mybitmap); ints.Add(3);
            mybitmap = new Bitmap(workingDirectory + "\\3h.jpg"); bitmaps.Add(mybitmap); ints.Add(3);
            mybitmap = new Bitmap(workingDirectory + "\\3i.jpg"); bitmaps.Add(mybitmap); ints.Add(3);
            mybitmap = new Bitmap(workingDirectory + "\\4a.jpg"); bitmaps.Add(mybitmap); ints.Add(4);
            mybitmap = new Bitmap(workingDirectory + "\\4b.jpg"); bitmaps.Add(mybitmap); ints.Add(4);
            mybitmap = new Bitmap(workingDirectory + "\\4c.jpg"); bitmaps.Add(mybitmap); ints.Add(4);
            mybitmap = new Bitmap(workingDirectory + "\\4d.jpg"); bitmaps.Add(mybitmap); ints.Add(4);
            mybitmap = new Bitmap(workingDirectory + "\\4e.jpg"); bitmaps.Add(mybitmap); ints.Add(4);
            mybitmap = new Bitmap(workingDirectory + "\\4f.jpg"); bitmaps.Add(mybitmap); ints.Add(4);
            mybitmap = new Bitmap(workingDirectory + "\\4g.jpg"); bitmaps.Add(mybitmap); ints.Add(4);
            mybitmap = new Bitmap(workingDirectory + "\\4h.jpg"); bitmaps.Add(mybitmap); ints.Add(4);
            mybitmap = new Bitmap(workingDirectory + "\\4i.jpg"); bitmaps.Add(mybitmap); ints.Add(4);
            mybitmap = new Bitmap(workingDirectory + "\\5a.jpg"); bitmaps.Add(mybitmap); ints.Add(5);
            mybitmap = new Bitmap(workingDirectory + "\\5b.jpg"); bitmaps.Add(mybitmap); ints.Add(5);
            mybitmap = new Bitmap(workingDirectory + "\\5c.jpg"); bitmaps.Add(mybitmap); ints.Add(5);
            mybitmap = new Bitmap(workingDirectory + "\\5d.jpg"); bitmaps.Add(mybitmap); ints.Add(5);
            mybitmap = new Bitmap(workingDirectory + "\\5e.jpg"); bitmaps.Add(mybitmap); ints.Add(5);
            mybitmap = new Bitmap(workingDirectory + "\\5f.jpg"); bitmaps.Add(mybitmap); ints.Add(5);
            mybitmap = new Bitmap(workingDirectory + "\\5g.jpg"); bitmaps.Add(mybitmap); ints.Add(5);
            mybitmap = new Bitmap(workingDirectory + "\\5h.jpg"); bitmaps.Add(mybitmap); ints.Add(5);
            mybitmap = new Bitmap(workingDirectory + "\\5i.jpg"); bitmaps.Add(mybitmap); ints.Add(5);
            mybitmap = new Bitmap(workingDirectory + "\\2.png"); bitmaps.Add(mybitmap); ints.Add(2);
            mybitmap = new Bitmap(workingDirectory + "\\3.png"); bitmaps.Add(mybitmap); ints.Add(3);
            mybitmap = new Bitmap(workingDirectory + "\\4.png"); bitmaps.Add(mybitmap); ints.Add(4);
            mybitmap = new Bitmap(workingDirectory + "\\5.png"); bitmaps.Add(mybitmap); ints.Add(5);




            return (bitmaps, ints);
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