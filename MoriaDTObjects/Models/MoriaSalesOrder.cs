using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MoriaDTObjects.Models.Interfaces;

namespace MoriaDTObjects.Models
{
    [DataContract]
    public class MoriaSalesOrder : ISubiektBaseObject
    {
        public MoriaSalesOrder()
        {
            SalesOrderItems = new List<MoriaSalesOrderItem>();
        }

        [DataMember]
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// Zamawiający
        /// </summary>
        [DataMember]
        public MoriaEntity Client
        {
            get;
            set;
        }

        /// <summary>
        /// Odbiorca
        /// </summary>
        [DataMember]
        public MoriaEntity Recipient
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
        public string StateName
        {
            get;
            set;
        }

        [DataMember]
        public DateTime DocumentDate
        {
            get;
            set;
        }

        /// <summary>
        /// Termin realizacji
        /// </summary>
        [DataMember]
        public DateTime DueDate
        {
            get;
            set;
        }

        /// <summary>
        /// Uwagi
        /// </summary>
        [DataMember]
        public string Remarks
        {
            get;
            set;
        }

        [DataMember]
        public MoriaWarehouse Warehouse
        {
            get;
            set;
        }

        [DataMember]
        public List<MoriaSalesOrderItem> SalesOrderItems
        {
            get;
            set;
        }
    }
}
