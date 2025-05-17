using System.Collections.Generic;
using InsERT.Moria.ModelDanych;
using MoriaDTObjects.Models;
using MoriaServices.Interfaces;

namespace MoriaServices.Services
{
    public class DictionariesService : IDictionariesService
    {
        public MoriaEntity CreateEntity(Podmiot entity)
        {
            if (entity == null) return null;

            var me = new MoriaEntity()
            {
                Id = entity.Id,
                ShortName = entity.NazwaSkrocona,
                NIP = entity.NIP
            };

            return me;
        }

        public MoriaWarehouse CreateWarehouse(Magazyn warehouse)
        {
            if (warehouse == null) return null;

            return new MoriaWarehouse() { Id = warehouse.Id, Name = warehouse.Nazwa, Symbol = warehouse.Symbol };
        }

        public MoriaSalesOrderItem CreateSalesOrderItem(PozycjaDokumentu item)
        {
            if (item == null) return null;

            var msoi = new MoriaSalesOrderItem()
            {
                Id = item.Id,
                Quantity = item.Ilosc,
                Index = item.LP,
                NetAmount = item.Cena.NettoPoRabacie,
                Product = CreateProduct(item.AsortymentAktualny),
                Remarks = item.Opis,
                DueDate = item.Termin.GetValueOrDefault(),
            };
            if (item.PolaWlasneAdv2 != null)
            {
                msoi.SerialNumber = item.PolaWlasneAdv2?.S0 ?? string.Empty;
                msoi.ProductionYear = item.PolaWlasneAdv2?.S1 ?? string.Empty;
                msoi.Power = item.PolaWlasneAdv2?.D0.GetValueOrDefault(0) ?? 0;
                msoi.Weight = item.PolaWlasneAdv2?.I0.GetValueOrDefault(0) ?? 0;
            }
            return msoi;
        }

        public MoriaProduct CreateProduct(Asortyment product)
        {
            if (product == null) return null;

            return new MoriaProduct()
            {
                Id = product.Id,
                Name = product.Nazwa,
                Symbol = product.Symbol,
                Components = CreateComponents(product)
                //TODO co z numerem seryjnym?
                //SerialNumber = product.Numer.ToString(),
            };
        }

        IList<MoriaComponent> CreateComponents(Asortyment product)
        {
            if (product == null) return null;

            var result = new List<MoriaComponent>();
            foreach (var component in product.SkladnikiKompletu)
            {
                var mc = CreateComponent(component);
                if (mc != null)
                    result.Add(mc);
            }

            return result;
        }

        MoriaComponent CreateComponent(SkladnikKompletu component)
        {
            if (component == null) return null;

            return new MoriaComponent()
            {
                Product = CreateProduct(component.Skladnik),
                Quantity = component.Ilosc,
                Id = component.Id
            };
        }

    }
}
