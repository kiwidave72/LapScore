using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LapScore.Core.Message;
using MDS.Client;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace LapScore.CommandLine
{
    class Program
    {
        static void Main(string[] args)
        {

            //Create MDSClient object to connect to DotNetMQ
            //Name of this application: Application1
            var mdsClient = new MDSClient("LapScore.CommandLine");
            //Connect to DotNetMQ server
            mdsClient.Connect();
            Guid testAccount = Guid.NewGuid();

            while (true)
            {
                var keypress = Console.ReadKey();
                //Get a message from user
                if (keypress.Key == ConsoleKey.Q)
                {
                    break;
                }
                else if ((keypress.KeyChar>=48) && (keypress.KeyChar<=58))
                {

                var carNumber = keypress.KeyChar-48;
                DateTime laptime = DateTime.UtcNow;
                LapRegistrationMessage newMessage = new LapRegistrationMessage(testAccount, "111111", carNumber, laptime);
                SendMessage(mdsClient, "LapScore.MessageService.Listener", newMessage.AsXml().ToString(), MDS.Communication.Messages.MessageTransmitRules.NonPersistent);
                SendMessage(mdsClient, "LapScore.MessageService.Recorder", newMessage.AsXml().ToString(), MDS.Communication.Messages.MessageTransmitRules.StoreAndForward);
                SendMessage(mdsClient, "LapScore.MessageService.Server", newMessage.AsXml().ToString(), MDS.Communication.Messages.MessageTransmitRules.StoreAndForward);

                }
            }

            //Disconnect from DotNetMQ server
            mdsClient.Disconnect(); 
           
        }

        private static void SendMessage(MDSClient mdsClient,string DestinationApplicationName, string newMessage,MDS.Communication.Messages.MessageTransmitRules rules)
        {
            //Create a DotNetMQ Message to send to Application2
            var message = mdsClient.CreateMessage();
            //Set destination application name
            message.DestinationApplicationName = DestinationApplicationName;
            message.TransmitRule = rules;
            //Set message data
            message.MessageData = Encoding.UTF8.GetBytes(newMessage);
            //Send message
            message.Send();
        }
       
    }
}
