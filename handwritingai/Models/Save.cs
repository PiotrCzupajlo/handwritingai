using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace handwritingai.Models
{
    public static class Save
    {
        

        public static int[,] returnnew(int[,] ints,int size)
        {
            int[,] newints = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    newints[i, j] = ints[i, j];
                
                }
            
            }


            return newints;
        }


    }
}
