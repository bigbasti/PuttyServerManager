using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Remoting.Messaging;
using PuttyServerManager.Config;
using WeifenLuo.WinFormsUI.Docking;

namespace PuttyServerManager.Tools {
    public class SingleInstanceHelper : MarshalByRefObject {

        public const string ChannelName = "PuttyServerManager";
        public const string SingleInstanceServiceName = "SingleInstance";

        public static void RegisterRemotingService() {
            try {
                IpcChannel ipcCh = new IpcChannel(ChannelName);
                ChannelServices.RegisterChannel(ipcCh, false);
                RemotingConfiguration.RegisterWellKnownServiceType(
                    typeof(SingleInstanceHelper), SingleInstanceServiceName, WellKnownObjectMode.SingleCall);

                Program.LogWriter.Log("Registered IpcChannel and Service: [ipc://{0}/{1}]", ChannelName, SingleInstanceServiceName);
            } catch (Exception ex) {
                Program.LogWriter.Log("Unable to register ipcchannel for single instance support...feature unavailable", ex);
            }
        }

        public static void LaunchInExistingInstance(string[] args) {
            IpcChannel channel = new IpcChannel();
            ChannelServices.RegisterChannel(channel, false);
            string url = String.Format("ipc://{0}/{1}", ChannelName, SingleInstanceServiceName);

            // Register as client for remote object.
            WellKnownClientTypeEntry remoteType = new WellKnownClientTypeEntry(typeof(SingleInstanceHelper), url);
            RemotingConfiguration.RegisterWellKnownClientType(remoteType);

            // Create a message sink.
            string objectUri;
            IMessageSink messageSink = channel.CreateMessageSink(url, null, out objectUri);


            SingleInstanceHelper helper = new SingleInstanceHelper();
            helper.Run(args);
        }


        public void Run(String[] args) {
            Program.LogWriter.Log("Received remote Run command: [{0}]", String.Join(" ", args));
            try {
                if (args.Length == 1) {
                    //Session name übergeben
                    frmMainWindow main = (frmMainWindow)ApplicationPaths.ApplicationMainForm;
                    if (main.InvokeRequired) {
                        main.BeginInvoke(new Action<string, DockState>(main.frmSessions.StartPuttySession), args[0], DockState.Document);
                    }
                    //.frmSessions.StartPuttySession(args[0]);
                } else if (args.Length == 3) {
                    //protocol - host - port
                    frmMainWindow main = (frmMainWindow)ApplicationPaths.ApplicationMainForm;
                    if (main.InvokeRequired) {
                        main.BeginInvoke(new Action<string, string, string>(main.StartQuickSession), args[0], args[1], args[2]);
                    }
                }
            } catch (Exception ex) {
                Program.LogWriter.Log("Could not attach to existing PSM Process: {0}", ex.Message);
            }
        }
    }
}
