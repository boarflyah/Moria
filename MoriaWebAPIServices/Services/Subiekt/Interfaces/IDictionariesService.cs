using InsERT.Moria.ModelDanych;
using MoriaDTObjects.Models;

namespace MoriaWebAPIServices.Services.Subiekt.Interfaces;

public interface IDictionariesService
{
    MoriaEntity CreateEntity(Podmiot entity);
    MoriaProduct CreateProduct(InsERT.Moria.ModelDanych.Asortyment product);
    MoriaSalesOrderItem CreateSalesOrderItem(PozycjaDokumentu item);
    MoriaWarehouse CreateWarehouse(InsERT.Moria.ModelDanych.Magazyn warehouse);
}
