using System.Runtime.Serialization;

namespace MoriaDTObjects.Models
{
    [DataContract]
    public class MoriaSalesOrderItem
    {
        [DataMember]
        public int Id
        {
            get;
            set;
        }

        public int Index
        {
            get;
            set;
        }

        public decimal Quantity
        {
            get;
            set;
        }
        public decimal NetAmount
        {
            get;
            set;
        }

        public MoriaProduct Product
        {
            get;
            set;
        }
        public string Remarks
        {
            get; set;
        }
    }
}
