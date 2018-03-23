using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.Xml;

namespace AndDigitalApis.Entities
{
    /// <summary>
    /// Class to write information on erorr message for APIs
    /// </summary>
    public class ErrorBodyWriter : BodyWriter
    {
        private readonly XmlObjectSerializer serializer;
        private readonly ErrorDetails errorDetails;

        public ErrorBodyWriter(ErrorDetails errorDetails)
            : base(true)
        {
            this.errorDetails = errorDetails;
            serializer = new DataContractSerializer(typeof(ErrorDetails));
        }

        protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
        {
            this.serializer.WriteObject(writer, this.errorDetails);
        }
    }
}
