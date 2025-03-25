using System.Runtime.Serialization;
using MoriaDTObjects.Models.Interfaces;

namespace MoriaDTObjects.Models
{
    [DataContract]
    public class MoriaEntity : ISubiektBaseObject
    {
        [DataMember]
        public int Id
        {
            get;
            set;
        }

        [DataMember]
        public string ShortName
        {
            get;
            set;
        }

        [DataMember]
        public string LongName
        {
            get; set;
        }

        [DataMember]
        public string Symbol
        {
            get;
            set;
        }

        [DataMember]
        public string NIP
        {
            get;
            set;
        }
    }
}
