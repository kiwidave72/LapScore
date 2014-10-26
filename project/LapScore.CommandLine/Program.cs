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
using LapScore.Core.Model;
using System.Xml;
using System.Threading;

namespace LapScore.CommandLine
{
    class Program
    {



        static void Main(string[] args)
        {
             Thread.Sleep(1000);

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            bool _IsRecording = false;
            string _Title = @"Lap Score v0.1";

            //Create MDSClient object to connect to DotNetMQ
            //Name of this application: Application1
            var mdsClient = new MDSClient("LapScore.CommandLine");
            //Connect to DotNetMQ server
            mdsClient.Connect();
            Guid testAccount = Guid.NewGuid();



            while (true)
            {
                if (_IsRecording)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write(_Title + "...");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Recording");
                }
                else
                {
                    Console.BackgroundColor= ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write(_Title + "...");
                    Console.WriteLine("Not Recording");
                }

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("Commands are.......");
                Console.WriteLine("Press (R) to record.");
                Console.WriteLine("Press (Q) to quite.");
                Console.WriteLine("Press (0-9) for car numbers.");


                Car car = new Car
                {
                    Driver = new Driver { 
                    Name="David"
                    },
                    Number=1,
                    Transponder="123121"

                };
                CarRegistrationMessage carmessage = new CarRegistrationMessage();
                carmessage.Init(testAccount,car);
                
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(carmessage.GetType());
                
                StringWriter writer = new StringWriter();
                x.Serialize(writer , carmessage);

                SendMessage(mdsClient, "LapScore.MessageService.Listener", carmessage.AsXml().ToString(), MDS.Communication.Messages.MessageTransmitRules.NonPersistent);
                SendMessage(mdsClient, "LapScore.Server", carmessage.AsXml().ToString(), MDS.Communication.Messages.MessageTransmitRules.StoreAndForward);

                Car car1 = new Car
                {
                    Driver = new Driver
                    {
                        Name = "Ed"
                    },
                    Number = 2,
                    Transponder = "123121"

                };


                carmessage = new CarRegistrationMessage();
                carmessage.Init(testAccount, car1);
                x = new System.Xml.Serialization.XmlSerializer(carmessage.GetType());
                
                writer = new StringWriter();
                x.Serialize(writer , carmessage);

                SendMessage(mdsClient, "LapScore.MessageService.Listener", carmessage.AsXml().ToString(), MDS.Communication.Messages.MessageTransmitRules.NonPersistent);
                SendMessage(mdsClient, "LapScore.Server", carmessage.AsXml().ToString(), MDS.Communication.Messages.MessageTransmitRules.StoreAndForward);

                

                var keypress = Console.ReadKey();
                //Get a message from user
                if (keypress.Key == ConsoleKey.Q)
                {
                    QuitMessage record = new QuitMessage();
                    record.Init(testAccount);
                    x = new System.Xml.Serialization.XmlSerializer(record.GetType());
                    writer = new StringWriter();
                    x.Serialize(writer, record);

                    SendMessage(mdsClient, "LapScore.MessageService.Listener", writer.ToString(), MDS.Communication.Messages.MessageTransmitRules.NonPersistent);
                    SendMessage(mdsClient, "LapScore.Server", writer.ToString(), MDS.Communication.Messages.MessageTransmitRules.StoreAndForward);

                    break;
                }
                else if (keypress.Key == ConsoleKey.R)
                {
                    RecordMessage record = new RecordMessage();
                    record.Init(testAccount);
                    x = new System.Xml.Serialization.XmlSerializer(record.GetType());
                    writer = new StringWriter();
                    x.Serialize(writer, record);

                    SendMessage(mdsClient, "LapScore.MessageService.Listener", writer.ToString(), MDS.Communication.Messages.MessageTransmitRules.NonPersistent);
                    SendMessage(mdsClient, "LapScore.Server", writer.ToString(), MDS.Communication.Messages.MessageTransmitRules.StoreAndForward);

                    _IsRecording = !_IsRecording;

                }
                else if ((keypress.KeyChar >= 48) && (keypress.KeyChar <= 58))
                {

                    var carNumber = keypress.KeyChar - 48;
                    DateTime laptime = DateTime.UtcNow;
                    LapRegistrationMessage newMessage = new LapRegistrationMessage();
                    newMessage.Init(testAccount, "111111", carNumber, laptime);
                    SendMessage(mdsClient, "LapScore.MessageService.Listener", newMessage.AsXml().ToString(), MDS.Communication.Messages.MessageTransmitRules.NonPersistent);
                    SendMessage(mdsClient, "LapScore.Server", newMessage.AsXml().ToString(), MDS.Communication.Messages.MessageTransmitRules.StoreAndForward);

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
