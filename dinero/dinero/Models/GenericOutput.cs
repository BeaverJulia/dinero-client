using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace dinero.Models
{
    public class GenericOutput<T>
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public List<T> Results { get; set; } 
    }
}
