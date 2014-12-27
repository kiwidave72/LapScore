using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LapScore.Core.Message;

namespace LapScore.Core.Test
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class About_LapRegistrationMessage
    {
        public About_LapRegistrationMessage()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void can_create_lapregistrationmessage()
        {
            //Guid testAccount = Guid.NewGuid();
            //DateTime laptime = DateTime.UtcNow;

            //LapRegistrationMessage newMessage = new LapRegistrationMessage();
            //newMessage.Init(testAccount,"111111",0,laptime);

            //Assert.AreEqual(newMessage.Payload.Name, "LapRegistrationPayload");
            //Assert.AreEqual(testAccount, newMessage.TrustedAccountID);
            //Assert.AreEqual(laptime, newMessage.Payload.DateTimeStampUTC);
            //Assert.AreEqual(0, newMessage.Payload.CarNumber);
            //Assert.IsNotNull(newMessage.ID);
           
        }
    }
}
