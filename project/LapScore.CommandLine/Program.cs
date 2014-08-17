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
            bool _IsRecording = false;

            //Create MDSClient object to connect to DotNetMQ
            //Name of this application: Application1
            var mdsClient = new MDSClient("LapScore.CommandLine");
            //Connect to DotNetMQ server
            mdsClient.Connect();
            Guid testAccount = Guid.NewGuid();



            while (true)
            {
                if(_IsRecording)
                    Console.WriteLine("Lap Score Running...Recording");
                else
                    Console.WriteLine("Lap Score Running...Not Recording");
                
                Console.WriteLine("Press (R) to record.");
                Console.WriteLine("Press (Q) to quite.");
                Console.WriteLine("Press (0-9) for car numbers.");


                var keypress = Console.ReadKey();
                //Get a message from user
                if (keypress.Key == ConsoleKey.Q)
                {
                    break;
                }
                else if (keypress.Key == ConsoleKey.R)
                {
                    _IsRecording = true;
                }
                else if ((keypress.KeyChar >= 48) && (keypress.KeyChar <= 58))
                {

                    var carNumber = keypress.KeyChar - 48;
                    DateTime laptime = DateTime.UtcNow;
                    LapRegistrationMessage newMessage = new LapRegistrationMessage(testAccount, "111111", carNumber, laptime);
                    SendMessage(mdsClient, "LapScore.MessageService.Listener", newMessage.AsXml().ToString(), MDS.Communication.Messages.MessageTransmitRules.NonPersistent);
                    SendMessage(mdsClient, "LapScore.MessageService.Server", newMessage.AsXml().ToString(), MDS.Communication.Messages.MessageTransmitRules.StoreAndForward);

                    if (_IsRecording)
                    {
                        SendMessage(mdsClient, "LapScore.MessageService.Recorder", newMessage.AsXml().ToString(), MDS.Communication.Messages.MessageTransmitRules.StoreAndForward);
                    }

                }
                Console.Clear();

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
