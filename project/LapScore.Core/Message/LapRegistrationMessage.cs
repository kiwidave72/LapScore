using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LapScore.Core.Message.Payload;
using System.Xml.Linq;

namespace LapScore.Core.Message
{
    public sealed class LapRegistrationMessage : AbstractLapScoreMessage<LapRegistrationPayload> 
    {
        public LapRegistrationMessage(Guid TrustedAccount,string TransponderNumber,int CarNumber,DateTime DateTimeStampUTC) : base (TrustedAccount)
        {
            this.Payload  = new LapRegistrationPayload(TransponderNumber,CarNumber,DateTimeStampUTC);
        }
           public  XDocument AsXml()
        {

            string xml = @"
<Message>
<ID>{0}</ID>
<DateTimeStamp>{1}</DateTimeStamp>
<Ticks>{2}</Ticks>
<Payload>
<TransponderNumber>{3}</TransponderNumber>
<CarNumber>{4}</CarNumber>
</Payload>
</Message>
";


            return XDocument.Parse(string.Format(xml,this.ID,this.DateTimeStampUTC,this.Payload.DateTimeStampUTC.Ticks ,this.Payload.TransponderNumber,this.Payload.CarNumber));
        }

    }
}
