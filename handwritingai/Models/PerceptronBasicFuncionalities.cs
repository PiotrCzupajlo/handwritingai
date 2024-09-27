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
        public PerceptronBasicFuncionalities(System.Drawing.Color c) {
            color = c;
        }
        public async Task Howmuchtrue() {
            int red = color.R;
            CallOutputLayer(red / 255);
        
        }
        public void CallOutputLayer(decimal howmuchiamthis) {
        
        
        
        }
    }
}
