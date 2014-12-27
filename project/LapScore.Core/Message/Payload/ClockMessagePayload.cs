using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LapScore.Core.Interfaces;

namespace LapScore.Core.Message.Payload
{
    public sealed class ClockMessagePayload:  IMessagePayload
    {
        private string _Name = "ClockMessagePayload";

        private decimal _Elapsed =0;

        private decimal _Remaining = 0 ;

        private int _Loop = 0;


        #region IMessagePayload Members

        public string Name
        {
            get { return _Name; }
        }

        public decimal Elapsed
        {
            get { return _Elapsed ; }
            set { _Elapsed = value; }
        }
        public decimal Remaining
        {
            get { return _Remaining; }
            set { _Remaining = value; }
        }
        public int Loop
        {
            get { return _Loop; }
            set { _Loop = value; }
        }


        #endregion
    }
}
