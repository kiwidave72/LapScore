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
        private DateTime _DateTimeStampUTC;




        public LapRegistrationPayload(string TransponderNumber,int CarNumber,DateTime DateTimeStampUTC)
        {
           _Name = "LapRegistrationPayload";
           _TransponderNumber = TransponderNumber;
           _CarNumber = CarNumber;
           _DateTimeStampUTC = DateTimeStampUTC;

        }

        public string TransponderNumber { get { return _TransponderNumber; } }
        public int CarNumber { get { return _CarNumber; } }
        public DateTime DateTimeStampUTC { get { return _DateTimeStampUTC; } }

        #region IMessagePayload Members

        public string Name
        {
            get{ return _Name;}
        }


        #endregion

      
    }
}
