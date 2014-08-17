using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MDS.Client;

namespace LapScore.MessageService.Listener
{
    class Program
    {
        static void Main(string[] args)
        {

             


            //Create MDSClient object to connect to DotNetMQ
            //Name of this application:  LapScore.MessageService.Listener
            var mdsClient = new MDSClient("LapScore.MessageService.Listener");

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

            //Process message
            Console.WriteLine();
            Console.WriteLine("Text message received : " + messageText);
            Console.WriteLine("Source application    : " + e.Message.SourceApplicationName);

            //Acknowledge that message is properly handled
            //and processed. So, it will be deleted from queue.
            e.Message.Acknowledge();
            
            
        }
    }
}
