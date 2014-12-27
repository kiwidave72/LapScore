using System.ComponentModel.Composition;
using System.Waf.Applications;
using LapScore.GUI.Applications.ViewModels;
using System.Threading;
using MDS.Client;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Threading;

namespace LapScore.GUI.Applications.Controllers
{
    [Export]
    internal class ApplicationController : Controller
    {
        private readonly ShellViewModel shellViewModel;

        private static bool KeepALive = true;

        [ImportingConstructor]
        public ApplicationController(ShellViewModel shellViewModel)
        {
            this.shellViewModel = shellViewModel;
        }



        public void Initialize()
        {
            Thread.Sleep(1000);


            //Create MDSClient object to connect to DotNetMQ
            //Name of this application:  LapScore.MessageService.Listener
            var mdsClient = new MDSClient("LapScore.MessageService.GUI");

            //Register to MessageReceived event to get messages.
            mdsClient.MessageReceived += MDSClient_MessageReceived;

            //Connect to DotNetMQ server
            mdsClient.Connect();

        }

        public void Run()
        {
            shellViewModel.Show();
        }

        public void Shutdown()
        {
        }

        void MDSClient_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            //Get message
            var messageText = Encoding.UTF8.GetString(e.Message.MessageData);
            //Acknowledge that message is properly handled
            //and processed. So, it will be deleted from queue.
            e.Message.Acknowledge();

            XDocument doc = XDocument.Parse(messageText);


            var quiteMessage = doc.XPathSelectElement("//QuitMessage");
            if (quiteMessage != null)
            {
                KeepALive = false;
                this.Shutdown();
            }




        }
    }
}
