using System;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Threading.Tasks;
using System.Xml;

namespace Core.AuditLog
{
    public class WcfAuditMessageInspector : IClientMessageInspector
    {
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            var buffer = reply.CreateBufferedCopy(Int32.MaxValue);
            reply = buffer.CreateMessage();

            Task.Run(() => {
                this.LogMessage(buffer, false);
            });
        }

        private void LogMessage(MessageBuffer buffer, bool isRequest)
        {
            var originalMessage = buffer.CreateMessage();
            string messageContent;

            using (StringWriter stringWriter = new StringWriter())
            {
                using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
                {
                    originalMessage.WriteMessage(xmlTextWriter);
                    xmlTextWriter.Flush();
                    xmlTextWriter.Close();
                }
                messageContent = stringWriter.ToString();
            }

            // log messageContent to the database
        }


        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            return null;
        }
    }
    public class WcfAuditBehaviorExtensionElement : BehaviorExtensionElement
    {
        protected override object CreateBehavior()
        {
            return new WcfAuditBehavior();
        }

        public override Type BehaviorType {
            get { return typeof(WcfAuditBehavior); }
        }
    }
    public class WcfAuditBehavior : IEndpointBehavior
    {
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            return;
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            WcfAuditMessageInspector inspector = new WcfAuditMessageInspector();
            clientRuntime.ClientMessageInspectors.Add(inspector);
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            return;
        }

        public void Validate(ServiceEndpoint endpoint)
        {
            return;
        }
    }
}
