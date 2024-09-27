using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace handwritingai.Interfaces
{
    internal interface IPerceptron
    {
        public System.Drawing.Color color { get; set; }
        public void howmuchtrue();
        public void CallNextHiddenLayer(decimal howmuchiamthis);


    }
}
