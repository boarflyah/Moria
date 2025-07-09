using System;
using System.ComponentModel.DataAnnotations;

namespace MoriaBaseServices.Services;

public class WebAPIEndpointsProvider
{
    #region responseheaders

    public const string LookupMetadataHeaderKey = "LookupMetadata";
    public const string PagedListHasNextHeaderKey = "HasNext";
    public const string PagedListLastId = "LastId";

    #endregion

    #region controllers

    public const string Token = "token";
    public const string Employee = "employee";
    public const string Warehouse = "warehouse";
    public const string SteelKind = "steelKind";
    public const string Position = "position";
    public const string MotorGear = "motorGear";
    public const string Motor = "motor";
    public const string Contact = "contact";
    public const string Color = "color";
    public const string Brake = "brake";
    public const string ExternalCooling = "externalCooling";
    public const string Inverter = "inverter";
    public const string Pump = "pump";
    public const string Supplement = "supplement";
    public const string Variator = "variator";
    public const string ElectricalCabinet = "electricalCabinet";
    public const string Lock = "lock";
    public const string Lookup = "lookup";
    public const string Product = "product";
    public const string Category = "category";
    public const string Drive = "drive";
    public const string Component = "component";
    public const string Order = "order";
    public const string ListView = "listview";

    #endregion

    #region endpoints

    #region Token

    public const string PostTokenPath = $"{Token}";

#if DEBUG

    public const string GetTokenGetTokenPath = $"{Token}/gettoken";

#endif

    #endregion

    #region Lock

    /// <summary>
    /// Body: object of type LockHelper
    /// <para>Return bool</para>
    /// <para>Throws: </para>
    /// </summary>
    public const string PutLockPath = $"{Lock}/lock";
    /// <summary>
    /// Body: object of type LockHelper
    /// <para>Return bool</para>
    /// <para>Throws: </para>
    /// </summary>
    public const string PutUnlockPath = $"{Lock}/unlock";
    /// <summary>
    /// Body: object of type LockHelper
    /// <para>Return bool</para>
    /// <para>Throws: </para>
    /// </summary>
    public const string KeepAlivePath = $"{Lock}/keepalive";
    /// <summary>
    /// Parameter from path: object.id
    /// <para>Return bool</para>
    /// <para>Throws: </para>
    /// </summary>
    public const string RemoveObjectKeepAlivePath = $"{Lock}/removeobjectkeepalive";

    #endregion

    #region Lookup

    /// <summary>
    /// Body: object of type LookupBodyHelper
    /// <para>Return paged list of LookupModel objects</para>
    /// <para>Throws: </para>
    /// </summary>
    public const string GetLookupPath = $"{Lookup}";


    /// <summary>
    /// Body: object of type SearchRequest
    /// </summary>
    public const string PutLookupFilteredPath = $"{Lookup}/filtered";

    #endregion

    #region Employee

    /// <summary>
    /// Body: object of type UserCredentials
    /// <para>Return EmployeeDo</para>
    /// <para>Throws: </para>
    /// </summary>
    public const string PostLoginPath = $"{Employee}/login";

    public const string GetEmployeesPath = $"{Employee}";

    /// <summary>
    /// Parameter from path: employee.id
    /// </summary>
    public const string GetEmployeePath = $"{Employee}";

    /// <summary>
    /// Body: EmployeeDo
    /// <para>Return EmployeeDo</para>
    /// </summary>
    public const string PostEmployeePath = $"{Employee}";

    /// <summary>
    /// Body: EmployeeDo
    /// <para>Return EmployeeDo</para>
    /// </summary>
    public const string PutEmployeePath = $"{Employee}";

    /// <summary>
    /// Parameter from path: employee.id
    /// <para>Return bool</para>
    /// </summary>
    public const string DeleteEmployeePath = $"{Employee}";

    #endregion

    #region Warehouse

    public const string GetWarehousesPath = $"{Warehouse}";
    /// <summary>
    /// Parameter from path: warehouse.id
    /// </summary>
    public const string GetWarehousePath = $"{Warehouse}";
    /// <summary>
    /// Body: WarehouseDo
    /// <para>Return WarehouseDo</para>
    /// </summary>
    public const string PostWarehousePath = $"{Warehouse}";

    /// <summary>
    /// Body: WarehouseDo
    /// <para>Return WarehouseDo</para>
    /// </summary>
    public const string PutWarehousePath = $"{Warehouse}";

    /// <summary>
    /// Parameter from path: warehouse.id
    /// <para>Return bool</para>
    /// </summary>
    public const string DeleteWarehousePath = $"{Warehouse}";
    #endregion

    #region SteelKind

    public const string GetSteelKindsPath = $"{SteelKind}";
    /// <summary>
    /// Parameter from path: steelKind.id
    /// </summary>
    public const string GetSteelKindPath = $"{SteelKind}";
    /// <summary>
    /// Body: SteelKindDo
    /// <para>Return SteelKindDo</para>
    /// </summary>
    public const string PostSteelKindPath = $"{SteelKind}";

    /// <summary>
    /// Body: SteelKindDo
    /// <para>Return SteelKindDo</para>
    /// </summary>
    public const string PutSteelKindPath = $"{SteelKind}";

    /// <summary>
    /// Parameter from path: steelKind.id
    /// <para>Return bool</para>
    /// </summary>
    public const string DeleteSteelKindPath = $"{SteelKind}";
    #endregion

    #region Position

    public const string GetPositionsPath = $"{Position}";
    /// <summary>
    /// Parameter from path: position.id
    /// </summary>
    public const string GetPositionPath = $"{Position}";
    /// <summary>
    /// Body: PositionDo
    /// <para>Return PositionDo</para>
    /// </summary>
    public const string PostPositionPath = $"{Position}";

    /// <summary>
    /// Body: PositionDo
    /// <para>Return PositionDo</para>
    /// </summary>
    public const string PutPositionPath = $"{Position}";

    /// <summary>
    /// Parameter from path: position.id
    /// <para>Return bool</para>
    /// </summary>
    public const string DeletePositionPath = $"{Position}";
    /// <summary>
    /// Parameter from path: latestId, pageSize
    /// </summary>
    public const string GetPositionLookupObjectsPath = $"{Position}/getlookupobjects";
    #endregion

    #region MotorGear

    public const string GetMotorGearsPath = $"{MotorGear}";
    /// <summary>
    /// Parameter from path: motorGear.id
    /// </summary>
    public const string GetMotorGearPath = $"{MotorGear}";
    /// <summary>
    /// Body: MotorGearDo
    /// <para>Return MotorGearDo</para>
    /// </summary>
    public const string PostMotorGearPath = $"{MotorGear}";

    /// <summary>
    /// Body: MotorGearDo
    /// <para>Return MotorGearDo</para>
    /// </summary>
    public const string PutMotorGearPath = $"{MotorGear}";

    /// <summary>
    /// Parameter from path: motorGear.id
    /// <para>Return bool</para>
    /// </summary>
    public const string DeleteMotorGearPath = $"{MotorGear}";
    #endregion

    #region Motor

    public const string GetMotorsPath = $"{Motor}";
    /// <summary>
    /// Parameter from path: motor.id
    /// </summary>
    public const string GetMotorPath = $"{Motor}";
    /// <summary>
    /// Body: MotorDo
    /// <para>Return MotorDo</para>
    /// </summary>
    public const string PostMotorPath = $"{Motor}";

    /// <summary>
    /// Body: MotorDo
    /// <para>Return MotorDo</para>
    /// </summary>
    public const string PutMotorPath = $"{Motor}";

    /// <summary>
    /// Parameter from path: motor.id
    /// <para>Return bool</para>
    /// </summary>
    public const string DeleteMotorPath = $"{Motor}";
    #endregion

    #region Contact

    public const string GetContactsPath = $"{Contact}";
    /// <summary>
    /// Parameter from path: contact.id
    /// </summary>
    public const string GetContactPath = $"{Contact}";
    /// <summary>
    /// Body: ContactDo
    /// <para>Return ContactDo</para>
    /// </summary>
    public const string PostContactPath = $"{Contact}";

    /// <summary>
    /// Body: ContactDo
    /// <para>Return ContactDo</para>
    /// </summary>
    public const string PutContactPath = $"{Contact}";

    /// <summary>
    /// Parameter from path: contact.id
    /// <para>Return bool</para>
    /// </summary>
    public const string DeleteContactPath = $"{Contact}";
    #endregion

    #region Color

    public const string GetColorsPath = $"{Color}";
    /// <summary>
    /// Parameter from path: color.id
    /// </summary>
    public const string GetColorPath = $"{Color}";
    /// <summary>
    /// Body: ColorDo
    /// <para>Return ColorDo</para>
    /// </summary>
    public const string PostColorPath = $"{Color}";

    /// <summary>
    /// Body: ContactDo
    /// <para>Return ContactDo</para>
    /// </summary>
    public const string PutColorPath = $"{Color}";

    /// <summary>
    /// Parameter from path: color.id
    /// <para>Return bool</para>
    /// </summary>
    public const string DeleteColorPath = $"{Color}";
    #endregion

    #region ElectricalCabinet

    public const string GetElectricalCabinetsPath = $"{ElectricalCabinet}";
    /// <summary>
    /// Parameter from path: cabinet.id
    /// </summary>
    public const string GetElectricalCabinetPath = $"{ElectricalCabinet}";
    /// <summary>
    /// Body: ElectricalCabinetDo
    /// <para>Return ElectricalCabinetDo</para>
    /// </summary>
    public const string PostElectricalCabinetPath = $"{ElectricalCabinet}";

    /// <summary>
    /// Body: ElectricalCabinetDo
    /// <para>Return ElectricalCabinetDo</para>
    /// </summary>
    public const string PutElectricalCabinetPath = $"{ElectricalCabinet}";

    /// <summary>
    /// Parameter from path: cabinet.id
    /// <para>Return bool</para>
    /// </summary>
    public const string DeleteElectricalCabinetPath = $"{ElectricalCabinet}";
    #endregion

    #region Product

    public const string GetProductsPath = $"{Product}";

    /// <summary>
    /// Parameter from path: product.id
    /// </summary>
    public const string GetProductPath = $"{Product}";

    /// <summary>
    /// Body: ProductDo
    /// <para>Return ProductDo</para>
    /// </summary>
    public const string PostProductPath = $"{Product}";

    /// <summary>
    /// Body: ProductDo
    /// <para>Return ProductDo</para>
    /// </summary>
    public const string PutProductPath = $"{Product}";

    /// <summary>
    /// Parameter from path: product.id
    /// <para>Return bool</para>
    /// </summary>
    public const string DeleteProductPath = $"{Product}";

    #endregion

    #region Category

    public const string GetCategoriesPath = $"{Category}";

    /// <summary>
    /// Parameter from path: category.id
    /// </summary>
    public const string GetCategoryPath = $"{Category}";

    /// <summary>
    /// Body: CategoryDo
    /// <para>Return CategoryDo</para>
    /// </summary>
    public const string PostCategoryPath = $"{Category}";

    /// <summary>
    /// Body: CategoryDo
    /// <para>Return CategoryDo</para>
    /// </summary>
    public const string PutCategoryPath = $"{Category}";

    /// <summary>
    /// Parameter from path: category.id
    /// <para>Return bool</para>
    /// </summary>
    public const string DeleteCategoryPath = $"{Category}";

    #endregion

    #region drive

    public const string GetDrivesPath = $"{Drive}";

    /// <summary>
    /// Parameter from path: drive.id
    /// </summary>
    public const string GetDrivePath = $"{Drive}";

    /// <summary>
    /// Body: DriveDo
    /// <para>Return DriveDo</para>
    /// </summary>
    public const string PostDrivePath = $"{Drive}";

    /// <summary>
    /// Body: DriveDo
    /// <para>Return DriveDo</para>
    /// </summary>
    public const string PutDrivePath = $"{Drive}";

    /// <summary>
    /// Parameter from path: drive.id
    /// <para>Return bool</para>
    /// </summary>
    public const string DeleteDrivePath = $"{Drive}";

    #endregion

    #region component

    /// <summary>
    /// Parameter from path: component.id
    /// </summary>
    public const string GetComponentPath = $"{Component}";

    #endregion

    #region order

    public const string GetOrdersPath = $"{Order}";

    /// <summary>
    /// Parameter from path: order.id
    /// </summary>
    public const string GetOrderPath = $"{Order}";

    /// <summary>
    /// Body: OrderDo
    /// <para>Return OrderDo</para>
    /// </summary>
    public const string PostOrderPath = $"{Order}";

    /// <summary>
    /// Body: OrderDo
    /// <para>Return OrderDo</para>
    /// </summary>
    public const string PutOrderPath = $"{Order}";

    /// <summary>
    /// Parameter from path: order.id
    /// <para>Return bool</para>
    /// </summary>
    public const string DeleteOrderPath = $"{Order}";
    public const string GetImportOrdersPath = $"{Order}/import";

    /// <summary>
    /// Parameter from path: weekNumber
    /// </summary>
    public const string GetCalendarOrdersPath = $"{Order}/calendar";
    public const string GetOrderItemsPath = $"{Order}/GetItems";
    /// <summary>
    /// Body: OrderItemDo
    /// <para>Return OrderItemDo</para>
    /// </summary>
    public const string PutOrderItemPath = $"{Order}/UpdateOrderItem";
    /// <summary>
    /// Parameter from path: orderitem.id
    /// </summary>
    public const string GetOrderItemPath = $"{Order}/OrderItem";
    #endregion

    #region Brake

    public const string GetBrakesPath = $"{Brake}";
    /// <summary>
    /// Parameter from path: brake.id
    /// </summary>
    public const string GetBrakePath = $"{Brake}";
    /// <summary>
    /// Body: BrakeDo
    /// <para>Return BrakeDo</para>
    /// </summary>
    public const string PostBrakePath = $"{Brake}";

    /// <summary>
    /// Body: BrakeDo
    /// <para>Return BrakeDo</para>
    /// </summary>
    public const string PutBrakePath = $"{Brake}";

    /// <summary>
    /// Parameter from path: Brake.id
    /// <para>Return bool</para>
    /// </summary>
    public const string DeleteBrakePath = $"{Brake}";
    #endregion

    #region ExternalCooling

    public const string GetExternalCoolingsPath = $"{ExternalCooling}";
    /// <summary>
    /// Parameter from path: ExternalCooling.id
    /// </summary>
    public const string GetExternalCoolingPath = $"{ExternalCooling}";
    /// <summary>
    /// Body: ExternalCoolingDo
    /// <para>Return ExternalCoolingDo</para>
    /// </summary>
    public const string PostExternalCoolingPath = $"{ExternalCooling}";

    /// <summary>
    /// Body: ExternalCoolingDo
    /// <para>Return ExternalCoolingDo</para>
    /// </summary>
    public const string PutExternalCoolingPath = $"{ExternalCooling}";

    /// <summary>
    /// Parameter from path: ExternalCooling.id
    /// <para>Return bool</para>
    /// </summary>
    public const string DeleteExternalCoolingPath = $"{ExternalCooling}";
    #endregion

    #region Inverter

    public const string GetInvertersPath = $"{Inverter}";
    /// <summary>
    /// Parameter from path: Inverter.id
    /// </summary>
    public const string GetInverterPath = $"{Inverter}";
    /// <summary>
    /// Body: InverterDo
    /// <para>Return InverterDo</para>
    /// </summary>
    public const string PostInverterPath = $"{Inverter}";

    /// <summary>
    /// Body: InvertergDo
    /// <para>Return InverterDo</para>
    /// </summary>
    public const string PutInverterPath = $"{Inverter}";

    /// <summary>
    /// Parameter from path: Inverter.id
    /// <para>Return bool</para>
    /// </summary>
    public const string DeleteInverterPath = $"{Inverter}";
    #endregion

    #region Pump

    public const string GetPumpsPath = $"{Pump}";
    /// <summary>
    /// Parameter from path: Pump.id
    /// </summary>
    public const string GetPumpPath = $"{Pump}";
    /// <summary>
    /// Body: PumpDo
    /// <para>Return PumpDo</para>
    /// </summary>
    public const string PostPumpPath = $"{Pump}";

    /// <summary>
    /// Body: PumpDo
    /// <para>Return PumpDo</para>
    /// </summary>
    public const string PutPumpPath = $"{Pump}";

    /// <summary>
    /// Parameter from path: Pump.id
    /// <para>Return bool</para>
    /// </summary>
    public const string DeletePumpPath = $"{Pump}";
    #endregion

    #region Supplement

    public const string GetSupplementsPath = $"{Supplement}";
    /// <summary>
    /// Parameter from path: Supplement.id
    /// </summary>
    public const string GetSupplementPath = $"{Supplement}";
    /// <summary>
    /// Body: SupplementDo
    /// <para>Return SupplementDo</para>
    /// </summary>
    public const string PostSupplementPath = $"{Supplement}";

    /// <summary>
    /// Body: SupplementDo
    /// <para>Return SupplementDo</para>
    /// </summary>
    public const string PutSupplementPath = $"{Supplement}";

    /// <summary>
    /// Parameter from path: Supplement.id
    /// <para>Return bool</para>
    /// </summary>
    public const string DeleteSupplementPath = $"{Supplement}";
    #endregion


    #region Variator

    public const string GetVariatorsPath = $"{Variator}";
    /// <summary>
    /// Parameter from path: Variator.id
    /// </summary>
    public const string GetVariatorPath = $"{Variator}";
    /// <summary>
    /// Body: VariatorDo
    /// <para>Return VariatorDo</para>
    /// </summary>
    public const string PostVariatorPath = $"{Variator}";

    /// <summary>
    /// Body: VariatorDo
    /// <para>Return VariatorDo</para>
    /// </summary>
    public const string PutVariatorPath = $"{Variator}";

    /// <summary>
    /// Parameter from path: Variator.id
    /// <para>Return bool</para>
    /// </summary>
    public const string DeleteVariatorPath = $"{Variator}";
    #endregion


    #region listview

    /// <summary>
    /// Body: DOListViewSetup
    /// </summary>
    public const string PutListViewSetupPath = $"{ListView}/listviewsetup";

    /// <summary>
    /// path: listViewId
    /// </summary>
    public const string GetListViewSetupPath = $"{ListView}/listviewsetup";

    /// <summary>
    /// Body: type and searchtext string
    /// </summary>
    public const string GetSearchPath = $"{ListView}/SearchText";

    #endregion

#if DEBUG
    public const string Test = "test";

    public const string GetTestPath = $"{Test}";

#endif

    #endregion
}
