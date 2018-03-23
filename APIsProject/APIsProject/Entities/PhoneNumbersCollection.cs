using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace AndDigitalApis.Entities
{
    /// <summary>
    /// Data contract collection class to return phone numbers
    /// </summary>
    [CollectionDataContract(Name ="PhoneNumbers")]
    public class PhoneNumbersCollection : Collection<PhoneNumber>
    {
        public PhoneNumbersCollection(IEnumerable<PhoneNumber> phoneNumbers) 
            : this()
        {
            foreach(PhoneNumber phonenumber in phoneNumbers)
            {
                this.Add(phonenumber);
            }
        }

        public PhoneNumbersCollection()
        { }
    }
}
