using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LapScore.Core.Interfaces;
using LapScore.Core.Model;

namespace LapScore.Core.Message.Payload
{
     
    public sealed class CarRegistrationPayload : IMessagePayload
    {
        private string _Name = "CarRegistrationMessagePayload";

        public Car Car {get; set;}

        public void Init(Car Car)
        {
            this.Car = Car;
        }
        #region IMessagePayload Members

        public string Name
        {
            get { return _Name; }
        }

        #endregion
    }
}
