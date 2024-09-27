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

            Bitmap mybitmap = new Bitmap("3.png");
            System.Drawing.Color pixelcolor = mybitmap.GetPixel(6, 6);
            List<PerceptronBasicFuncionalities> inputs = new List<PerceptronBasicFuncionalities>();
            for (int i = 1; i < 29; i++)
            {
                for (int j = 0; j < 28; j++)
                {
                    inputs.Add(new PerceptronBasicFuncionalities(mybitmap.GetPixel(i,j))); ;
                }
            }







        }
    }
}