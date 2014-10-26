using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LapScore.Core.Interfaces;
using System.Xml.Linq;

namespace LapScore.Core.Message
{
    abstract  public class AbstractLapScoreMessage<T> :ILapScoreMessage
    {
        private Guid _ID;
        private DateTime _DateTimeStampUTC;
        private Guid _TrustedAccountID;
        private T _PayLoad;

        //public  AbstractLapScoreMessage(Guid TrustedAccountID)
        //{    
        //}

        public void Init(Guid TrustedAccountID)
        {
            _TrustedAccountID = TrustedAccountID;
            _DateTimeStampUTC = DateTime.UtcNow;
            _ID = Guid.NewGuid();

        }

        #region ILapScoreMessage Members

        public Guid ID
        {
            get { return _ID; }
            set{ _ID = value;}
        }

        public Guid TrustedAccountID
        {
            get { return _TrustedAccountID; }
            set { _TrustedAccountID = value; }
        }

        public DateTime DateTimeStampUTC
        {
            get { return _DateTimeStampUTC; }
            set { _DateTimeStampUTC = value; }
        }

        #endregion

        #region ILapScoreMessage Members


        public T Payload
        {
            get { return _PayLoad; }
            set { _PayLoad = value; }
        }

        #endregion



    }
}
