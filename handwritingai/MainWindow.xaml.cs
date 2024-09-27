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
            outputPerceptrons.Add(new OutputPerceptron("1.jpg",size));
            outputPerceptrons.Add(new OutputPerceptron("2.jpg",size));
            outputPerceptrons.Add(new OutputPerceptron("3.jpg",size));



            Bitmap mybitmap = new Bitmap("3.png");
            List<PerceptronBasicFuncionalities> inputs = new List<PerceptronBasicFuncionalities>();
            var task = Task.CompletedTask;
            for (int i = 1; i < size; i++)
            {
                for (int j = 1; j < size; j++)
                {
                    PerceptronBasicFuncionalities inputperceptorom = new PerceptronBasicFuncionalities(mybitmap.GetPixel(i, j),outputPerceptrons,i,j);
                    inputs.Add(inputperceptorom);
                    task =  inputperceptorom.Howmuchtrue();
                    
                }
            }
            task.Wait();

            foreach (OutputPerceptron item in outputPerceptrons)
            {
                item.percentageofbeing = item.sum / (size * size);
            }

        }

    }
}