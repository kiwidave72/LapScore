using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MDS.Client;
using LapScore.Core.Model;
using System.Xml.Linq;
using System.Xml.XPath;

namespace LapScore.Server
{
    class Program
    {
        public static Dictionary<int, Driver> drivers = new Dictionary<int, Driver>();

        
        
        static void Main(string[] args)
        {


            //Create MDSClient object to connect to DotNetMQ
            //Name of this application: Application1
            var mdsClient = new MDSClient("LapScore.Server");
            //Connect to DotNetMQ server
            mdsClient.Connect();

            //Register to MessageReceived event to get messages.
            mdsClient.MessageReceived += MDSClient_MessageReceived;

            //Connect to DotNetMQ server
            mdsClient.Connect();

            //Wait user to press enter to terminate application
            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();

            //Disconnect from DotNetMQ server
            mdsClient.Disconnect();



        }

        static void MDSClient_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            //Get message
            var messageText = Encoding.UTF8.GetString(e.Message.MessageData);

            //Acknowledge that message is properly handled
            //and processed. So, it will be deleted from queue.
            e.Message.Acknowledge();

            XDocument doc = XDocument.Parse(messageText);
            var carNumber = Convert.ToInt16( doc.XPathSelectElement("//CarNumber").Value);

            Driver foundDriver = null;
            foreach (KeyValuePair<int, Driver> d in drivers)
            {
                if (d.Key == carNumber)
                {
                    foundDriver = d.Value;
                }
            }


            if(foundDriver == null)
            {
                var driver = new Driver();
                driver.Name="Car "+carNumber;
                driver.Laps =1;
                drivers.Add(carNumber,driver);

            }
            else
            {
                foundDriver.Laps=foundDriver.Laps+1;
            }


            Console.Clear();
           

            foreach (KeyValuePair<int, Driver> d in drivers)
            {
                Console.WriteLine(string.Format("{0} {1}" , d.Value.Laps,d.Value.Name));
            }

        }
    }
}
