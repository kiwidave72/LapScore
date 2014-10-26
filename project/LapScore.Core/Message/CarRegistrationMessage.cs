using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LapScore.Core.Message.Payload;
using LapScore.Core.Model;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace LapScore.Core.Message
{
    [XmlRoot("Message")]
     public sealed class CarRegistrationMessage : AbstractLapScoreMessage<CarRegistrationPayload> 
    {
        //public CarRegistrationMessage(Guid TrustedAccount,Car Car)
        //    : base(TrustedAccount)
        //{
           
        //}

        public CarRegistrationMessage()
        {
           Payload =new CarRegistrationPayload();
        }
        public void Init(Guid TrustedAccount, Car Car)
        {
            base.Init(TrustedAccount);
            this.Payload.Init(Car);
        }
        

    }
}
