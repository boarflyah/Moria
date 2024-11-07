using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MoriaDTObjects.Models
{
    [DataContract]
    public class MoriaSalesOrder
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
        public MoriaEntity Recipient
        {
            get;
            set;
        }

        public string Symbol
        {
            get;
            set;
        }

        public string StateName
        {
            get;
            set;
        }

        public DateTime DocumentDate
        {
            get;
            set;
        }

        /// <summary>
        /// Termin realizacji
        /// </summary>
        public DateTime DueDate
        {
            get;
            set;
        }

        /// <summary>
        /// Uwagi
        /// </summary>
        public string Remarks
        {
            get;
            set;
        }

        public MoriaWarehouse Warehouse
        {
            get;
            set;
        }

        public List<MoriaSalesOrderItem> SalesOrderItems
        {
            get;
            set;
        }
    }
}
