using System.Runtime.Serialization;
using MoriaDTObjects.Models.Interfaces;

namespace MoriaDTObjects.Models
{
    public class MoriaComponent : ISubiektBaseObject
    {
        [DataMember]
        public int Id
        {
            get; set;
        }

        [DataMember]
        public MoriaProduct Product
        {
            get;
            set;
        }

        [DataMember]
        public decimal Quantity
        {
            get;
            set;
        }
    }
}
