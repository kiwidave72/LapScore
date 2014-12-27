using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LapScore.Core.Interfaces;
using System.Xml.Linq;

namespace LapScore.Core.Message.Payload
{
    public sealed class LapRegistrationPayload : IMessagePayload,ILapRegistrationPayload
    {
        private string _Name = "";
        
        
        private string _TransponderNumber = "";
        private int _CarNumber = 0;
        private decimal _LapRegistrationElapsedTime;




        public LapRegistrationPayload(string TransponderNumber, int CarNumber, decimal LapRegistrationElapsedTime)
        {
           _Name = "LapRegistrationPayload";
           _TransponderNumber = TransponderNumber;
           _CarNumber = CarNumber;
           _LapRegistrationElapsedTime = LapRegistrationElapsedTime;

        }

        public string TransponderNumber { get { return _TransponderNumber; } }
        public int CarNumber { get { return _CarNumber; } }
        public decimal LapRegistrationElapsedTime { get { return _LapRegistrationElapsedTime; } }

        #region IMessagePayload Members

        public string Name
        {
            get{ return _Name;}
        }


        #endregion

      
    }
}
