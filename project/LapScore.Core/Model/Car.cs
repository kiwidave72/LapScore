using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LapScore.Core.Model
{
    [Serializable]
    public sealed class Car
    {
        public Car()
        {
            Driver = new Driver();
        }

        public int Number { get; set; }
        public Driver Driver { get; set; }
        public String Transponder { get; set; }
    }
}
