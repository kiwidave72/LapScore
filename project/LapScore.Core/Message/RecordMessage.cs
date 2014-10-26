using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LapScore.Core.Message.Payload;
using System.Xml.Linq;

namespace LapScore.Core.Message
{
    public sealed class RecordMessage : AbstractLapScoreMessage<RecordMessagePayload>
    {

        public void Init(Guid TrustedAccountID) 
        {
            base.Init(TrustedAccountID) ;

            this.Payload = new RecordMessagePayload();

        }

        public XDocument AsXml()
        {

            string xml = @"
            <RecordMessage>
            <ID>{0}</ID>
            <DateTimeStamp>{1}</DateTimeStamp>
            <Payload>
            </Payload>
            </RecordMessage>
            ";
            return XDocument.Parse(string.Format(xml,
                    this.ID,
                    this.DateTimeStampUTC
                    ));
        }
    }
}
