using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AndDigitalApis.Entities
{
    /// <summary>
    /// Data contract class to return error messages in case of any failure
    /// </summary>
    [MessageContract(IsWrapped =false)]
    [DataContract(Name = "ErrorDetails")]
    public class ErrorDetails
    {
        [DataMember]
        public string Message { get; set; }

        public ErrorDetails(string message)
        {
            Message = message;
        }
    }
}
