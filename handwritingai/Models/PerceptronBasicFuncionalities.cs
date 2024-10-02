using handwritingai.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace handwritingai.Models
{
    public class PerceptronBasicFuncionalities : IPerceptron
    {
        public System.Drawing.Color color { get; set; }
        public List<OutputPerceptron> outputPerceptrons { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public PerceptronBasicFuncionalities(System.Drawing.Color c, List<OutputPerceptron> outputPerceptrons, int x, int y)
        {
            color = c;
            this.outputPerceptrons = outputPerceptrons;
            this.x = x;
            this.y = y;
        }
        public async Task Howmuchtrue() {
            decimal red = color.R;

            CallOutputLayer(Decimal.Divide(red, 255));
        
        }
        public void CallOutputLayer(decimal howmuchiamthis) {

            foreach (OutputPerceptron item in outputPerceptrons)
            {

            item.sum += howmuchiamthis*item.wages[x,y];
            }
        
        
        }

    }
}
