using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameMaker
{
    public class TalliedName
    {
        public string Name { get; set; }
        public int Count { get; set; } = 0;

        public override string ToString()
        {
            return Name;
        }
    }
}
