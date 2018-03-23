using AndDigitalApis.Entities;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace AndDigitalApis
{
    public class ErrorHandlerAttribute : Attribute, IServiceBehavior, IErrorHandler
    {    
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
            // do nothing
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach(var channelDispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                var dispatcher = (ChannelDispatcher)channelDispatcherBase;
                dispatcher.ErrorHandlers.Add(this);
            }
        }

        public bool HandleError(Exception error)
        {
            // Logging on errors can go here
            return false;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            BodyWriter bodyWriter = new ErrorBodyWriter(new ErrorDetails(error.Message));
            fault = Message.CreateMessage(version, null, bodyWriter);
            HttpResponseMessageProperty prop = new HttpResponseMessageProperty();
            prop.StatusCode = HttpStatusCode.BadRequest;
            prop.Headers[HttpResponseHeader.ContentType] = "application/xml; charset=utf-8";
            fault.Properties.Add(HttpResponseMessageProperty.Name, prop);
            fault.Properties.Add(WebBodyFormatMessageProperty.Name, new WebBodyFormatMessageProperty(WebContentFormat.Xml));          
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            // do nothing
        }
    }
}
