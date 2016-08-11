using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCFContracts
{
    [DataContract]
    public class AddResult
    {
        [DataMember]
        public int Result { get; set; }
    }

    [DataContract]
    public class AddRequest
    {
        [DataMember]
        public int FirstNumber { get; set; }

        [DataMember]
        public int SecondNumber { get; set; }

    }
}
