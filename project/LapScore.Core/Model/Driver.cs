using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LapScore.Core.Model
{
    [Serializable]
    public sealed class Driver
    {

        public string Name {get;set;}
        public int Laps { get; set; }
        public decimal Seconds { get; set; }


    }
}
