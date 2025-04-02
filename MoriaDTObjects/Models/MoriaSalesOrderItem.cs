using System;
using System.Runtime.Serialization;
using MoriaDTObjects.Models.Interfaces;

namespace MoriaDTObjects.Models
{
    [DataContract]
    public class MoriaSalesOrderItem : ISubiektBaseObject
    {
        [DataMember]
        public int Id
        {
            get;
            set;
        }

        [DataMember]
        public int Index
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

        [DataMember]
        public decimal NetAmount
        {
            get;
            set;
        }

        [DataMember]
        public MoriaProduct Product
        {
            get;
            set;
        }

        [DataMember]
        public string Remarks
        {
            get; set;
        }

        [DataMember]
        public DateTime DueDate
        {
            get; set;
        }
    }
}
