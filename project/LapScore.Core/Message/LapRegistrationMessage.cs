using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LapScore.Core.Message.Payload;

namespace LapScore.Core.Message
{
    public sealed class LapRegistrationMessage : AbstractLapScoreMessage<LapRegistrationPayload> 
    {

        public LapRegistrationMessage(Guid TrustedAccount,string TransponderNumber,int CarNumber,DateTime DateTimeStampUTC) : base (TrustedAccount)
        {
            this.Payload  = new LapRegistrationPayload(TransponderNumber,CarNumber,DateTimeStampUTC);
        }
    }
}
