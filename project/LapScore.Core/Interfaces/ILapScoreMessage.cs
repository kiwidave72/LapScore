using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LapScore.Core.Interfaces;

namespace LapScore.Core.Interfaces
{
    interface ILapScoreMessage
    {

        Guid ID { get; set;}
        Guid TrustedAccountID { get; set; }
        DateTime DateTimeStampUTC { get; set; }
    }
}
