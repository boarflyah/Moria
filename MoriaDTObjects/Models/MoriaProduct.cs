using System.Collections.Generic;

namespace MoriaDTObjects.Models
{
    public class MoriaProduct
    {
        public MoriaProduct()
        {
            Components = new List<MoriaComponent>();
        }

        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Symbol
        {
            get;
            set;
        }

        public string SerialNumber
        {
            get;
            set;
        }

        public IList<MoriaComponent> Components
        {
            get;
            set;
        }
    }
}
