using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MDS.Client;
using LapScore.Core.Model;
using System.Xml.Linq;
using System.Xml.XPath;

using LapScore.Core.Message;
using System.IO;
using System.Xml;

namespace LapScore.Server
{
    class Program
    {
        public static Dictionary<int, Driver> drivers = new Dictionary<int, Driver>();

        private static bool KeepALive = true;
        
        static void Main(string[] args)
        {


            //Create MDSClient object to connect to DotNetMQ
            //Name of this application: Application1
            var mdsClient = new MDSClient("LapScore.Server");
            //Connect to DotNetMQ server
            //mdsClient.Connect();

            //Register to MessageReceived event to get messages.
            mdsClient.MessageReceived += MDSClient_MessageReceived;


            //Connect to DotNetMQ server
            mdsClient.Connect();

            //Wait user to press enter to terminate application
            //Console.WriteLine("Press enter to exit...");
            //Console.ReadLine();

            //Disconnect from DotNetMQ server
            

            while (KeepALive==true)
           {

           }
           mdsClient.Disconnect();



        }

        static void MDSClient_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            //Get message
            var messageText = Encoding.UTF8.GetString(e.Message.MessageData );

            //Acknowledge that message is properly handled
            //and processed. So, it will be deleted from queue.
            e.Message.Acknowledge();

            XDocument doc = XDocument.Parse(messageText);

            var recordMessage = doc.XPathSelectElement("//RecordMessage");
            if (recordMessage != null)
            {
                Console.WriteLine("Record Message");
            }

            var quiteMessage = doc.XPathSelectElement("//QuitMessage");
            if (quiteMessage != null)
            {
                Console.WriteLine("Quite Message");
                KeepALive = false;
                
            }

            var carRegistrationMessage = doc.XPathSelectElement("//CarRegistrationMessage");
            if (carRegistrationMessage != null)
            {
                ProcessCarRegisitration(doc);

  
            }

            var lapRegistrationMessage = doc.XPathSelectElement("//LapRegistrationMessage");
            if (lapRegistrationMessage != null)
            {
                ProcessLapRegisitration(doc);


            }




            //var carNumberNode  = doc.XPathSelectElement("//CarNumber");
            
            //if (carNumberNode == null)
            //    return;
            //var carNumber = Convert.ToInt16( carNumberNode.Value);
            


            //Driver foundDriver = null;
            //foreach (KeyValuePair<int, Driver> d in drivers)
            //{
            //    if (d.Key == carNumber)
            //    {
            //        foundDriver = d.Value;
            //    }
            //}


            //if(foundDriver == null)
            //{
            //    var driver = new Driver();
            //    driver.Name="Car "+carNumber;
            //    driver.Laps =1;
            //    drivers.Add(carNumber,driver);

            //}
            //else
            //{
            //    foundDriver.Laps=foundDriver.Laps+1;
            //}


            Console.Clear();
           

            foreach (KeyValuePair<int, Driver> d in drivers)
            {
                Console.WriteLine(string.Format("{0} {1}" , d.Value.Laps,d.Value.Name));
            }

        }

        static void ProcessCarRegisitration(XDocument doc)
        {
            //  <Payload>
            //    <Name>{3}</Name>
            //    <Car><Number>{4}</Number></Car>
            //</Payload>


            var carNumberNode = doc.XPathSelectElement("//Number");
            var nameNode = doc.XPathSelectElement("//Name");

            if (carNumberNode == null)
                return;
            var carNumber = Convert.ToInt16(carNumberNode.Value);



            Driver foundDriver = null;
            foreach (KeyValuePair<int, Driver> d in drivers)
            {
                if (d.Key == carNumber)
                {
                    foundDriver = d.Value;
                }
            }


            if (foundDriver == null)
            {
                var driver = new Driver();
                driver.Name = nameNode.Value;
                driver.Laps = 0;
                drivers.Add(carNumber, driver);
            }

        }
        static void ProcessLapRegisitration(XDocument doc)
        {
            var carNumberNode = doc.XPathSelectElement("//CarNumber");

            if (carNumberNode == null)
                return;
            var carNumber = Convert.ToInt16(carNumberNode.Value);



            Driver foundDriver = null;
            foreach (KeyValuePair<int, Driver> d in drivers)
            {
                if (d.Key == carNumber)
                {
                    foundDriver = d.Value;
                }
            }


            if (foundDriver != null)
            {
                foundDriver.Laps = foundDriver.Laps+1;

            }
        }
    }



}
