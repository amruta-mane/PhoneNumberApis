using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AndDigitalApis.Entities
{
    /// <summary>
    /// Class to represent customer data
    /// </summary>
    [DataContract]
    public class Customer
    {
        [DataMember]
        public int Identity { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public List<PhoneNumber> PhoneNumbers { get; set;}
    }
}
