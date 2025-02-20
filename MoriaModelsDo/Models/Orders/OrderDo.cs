using MoriaModelsDo.Base;

namespace MoriaModelsDo.Models.Orders;
public class OrderDo: BaseDo
{

    private string _OrderNumberSymbol;
    public string OrderNumberSymbol
    {
        get => _OrderNumberSymbol;
        set
        {
            _OrderNumberSymbol = value;
            RaisePropertyChanged(value);
        }
    }


    private string _Remarks;
    public string Remarks
    {
        get => _Remarks;
        set
        {
            _Remarks = value;
            RaisePropertyChanged(value);
        }
    }


    private string _CatalogLink;
    public string CatalogLink
    {
        get => _CatalogLink;
        set
        {
            _CatalogLink = value;
            RaisePropertyChanged(value);
        }
    }

    private string _ClientSymbol;
    public string ClientSymbol
    {
        get => _ClientSymbol;
        set
        {
            _ClientSymbol = value;
            RaisePropertyChanged(value);
        }
    }
}
