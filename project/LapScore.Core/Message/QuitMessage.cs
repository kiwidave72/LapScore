using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LapScore.Core.Message.Payload;
using System.Xml.Linq;

namespace LapScore.Core.Message
{
    public sealed class ClockMessage : AbstractLapScoreMessage<ClockMessagePayload>
    {

        public void Init(Guid TrustedAccountID) 
        {
            base.Init(TrustedAccountID) ;

            this.Payload = new ClockMessagePayload();

        }

        public XDocument AsXml()
        {

            string xml = @"
            <ClockMessage >
            <ID>{0}</ID>
            <DateTimeStamp>{1}</DateTimeStamp>
            <Payload>
                <Elapsed>{2}</Elapsed>
                <Remaining>{3}</Remaining>
                <Loop>{4}</Loop>
            </Payload>
            </ClockMessage >
            ";
            return XDocument.Parse(string.Format(xml,
                    this.ID,
                    this.DateTimeStampUTC,
                    this.Payload.Elapsed,
                    this.Payload.Remaining,
                    this.Payload.Loop
                    ));
        }
    }
}
