using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LapScore.Core.Interfaces;

namespace LapScore.Core.Message.Payload
{
    public sealed class QuitMessagePayload:  IMessagePayload
    {
        private string _Name = "QuitMessagePayload";

        #region IMessagePayload Members

        public string Name
        {
            get { return _Name; }
        }

        #endregion
    }
}
