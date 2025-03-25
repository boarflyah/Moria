using System.Collections.Generic;
using System.Runtime.Serialization;
using MoriaDTObjects.Models.Interfaces;

namespace MoriaDTObjects.Models
{
    public class MoriaProduct : ISubiektBaseObject
    {
        public MoriaProduct()
        {
            Components = new List<MoriaComponent>();
        }

        [DataMember]
        public int Id
        {
            get;
            set;
        }

        [DataMember]
        public string Name
        {
            get;
            set;
        }

        [DataMember]
        public string Symbol
        {
            get;
            set;
        }

        [DataMember]
        public string SerialNumber
        {
            get;
            set;
        }

        [DataMember]
        public IList<MoriaComponent> Components
        {
            get;
            set;
        }
    }
}
