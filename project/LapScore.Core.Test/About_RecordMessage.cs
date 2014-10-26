using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LapScore.Core.Message;

namespace LapScore.Core.Test
{
    [TestClass]
    public class About_RecordMessage
    {

        [TestMethod]
        public void can_create_recordmessage()
        {
            Guid testAccount = Guid.NewGuid();
            DateTime laptime = DateTime.UtcNow;

            var newMessage = new RecordMessage();
            newMessage.Init(testAccount);

            Assert.AreEqual(newMessage.Payload.Name, "RecordMessagePayload");
            Assert.AreEqual(testAccount, newMessage.TrustedAccountID);

        }
    }
}
