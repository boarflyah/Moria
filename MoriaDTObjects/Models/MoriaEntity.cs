using System.Runtime.Serialization;

namespace MoriaDTObjects.Models
{
    [DataContract]
    public class MoriaEntity
    {
        //[DataMember]
        public int Id
        {
            get;
            set;
        }

        //[DataMember]
        public string ShortName
        {
            get;
            set;
        }

        //[DataMember]
        public string LongName
        {
            get; set;
        }

        //[DataMember]
        public string Symbol
        {
            get;
            set;
        }

        public string NIP
        {
            get;
            set;
        }
    }
}
