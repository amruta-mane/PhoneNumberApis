using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AndDigitalApis.Entities
{
    /// <summary>
    /// Class to define phone entity
    /// </summary>
    [DataContract]
    public class PhoneNumber :IComparable
    {
        [DataMember]
        public int Identity { get; set; }

        [DataMember]
        public string Number { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int CustomerId { get; set; }

        [DataMember]
        public bool IsActive { get; private set; }

        public void ActivatePhoneNumber(bool activate)
        {
            this.IsActive = activate;
        }

        public int CompareTo(object obj)
        {
            PhoneNumber pn1 = (PhoneNumber)obj;
           
            if (this.Identity > pn1.Identity)
                return 1;
            if (this.Identity < pn1.Identity)
                return -1;
            else
                return 0;
        }
    }
}
