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
                //Get a message from user
                var carNumber = Console.ReadKey();
                if (carNumber.Key == ConsoleKey.Q)
                {
                    break;
                }
                

                //Create a DotNetMQ Message to send to Application2
                var message = mdsClient.CreateMessage();
                //Set destination application name
                message.DestinationApplicationName = "LapScore.MessageService.Listener";
                
                //Set message data
                DateTime laptime = DateTime.UtcNow;
                LapRegistrationMessage newMessage = new LapRegistrationMessage(testAccount, "111111", Convert.ToInt32(carNumber.Key) , laptime);
                message.MessageData = Encoding.UTF8.GetBytes(newMessage.AsXml().ToString() );
                //Send message
                message.Send();
            }

            //Disconnect from DotNetMQ server
            mdsClient.Disconnect(); 
           
        }
        public static MemoryStream SerializeToStream(object o)
        {
            MemoryStream stream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, o);
            return stream;
        }
        public static object DeserializeFromStream(MemoryStream stream)
    {
        IFormatter formatter = new BinaryFormatter();
        stream.Seek(0, SeekOrigin.Begin);
        object o = formatter.Deserialize(stream);
        return o;
    }
    }
}
