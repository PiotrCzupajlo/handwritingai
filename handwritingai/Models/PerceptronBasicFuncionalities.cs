
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace handwritingai.Models
{
    public class PerceptronBasicFuncionalities 
    {
        public System.Drawing.Color color { get; set; }
        public List<OutputPerceptron> outputPerceptrons { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public decimal howmuchiamthis { get;set; }
        public PerceptronBasicFuncionalities(System.Drawing.Color c, List<OutputPerceptron> outputPerceptrons, int x, int y)
        {
            color = c;
            this.outputPerceptrons = outputPerceptrons;
            this.x = x;
            this.y = y;
        }
        public void Howmuchtrue() {
            decimal red = color.R;
            howmuchiamthis = Decimal.Divide(red, 255);
            CallOutputLayer();
            
        }
        public void CallOutputLayer() {

            foreach (OutputPerceptron item in outputPerceptrons)
            {

            item.sum += Decimal.Divide(howmuchiamthis*item.wages[x,y],255);
            }
        
        
        }

    }
}
