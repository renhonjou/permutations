using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace permutationsWeb.Models
{
    public class PermutationsResponse
    {
        public Double Seconds { get; set; }
        public String Input { get; set; }
        public String[] Permutations { get; set; }
        public Int32 Count
        {
            get
            {
                return this.Permutations.Length;
            }
        }
    }
}
