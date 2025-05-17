using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using MoriaDesktop.Attributes;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModels.Models.DriveComponents;
using MoriaModelsDo.Attributes;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.Contacts;
using MoriaModelsDo.Models.Dictionaries;
using MoriaModelsDo.Models.Orders;
using MoriaModelsDo.Models.Products;

namespace MoriaDesktop.ViewModels.Orders
{
    public class ElectricalOrderItemDetailViewModel : BaseDetailWithNestedListViewModel
    {
        readonly IApiOrderService _orderService;
        OrderDo order;
        OrderItemDo currentOrderItem;

        public ElectricalOrderItemDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, IApiLockService apiLockService, INavigationService navigationService, IKeepAliveWorker keepAliveWorker, IApiOrderService apiOrderService) : base(logger, appStateService, apiLockService, navigationService, keepAliveWorker)
        {
            _orderService = apiOrderService;
        }

        string _Symbol;
        [ObjectChangedValidate]
        [DefaultProperty]
        public string Symbol
        {
            get => _Symbol;
            set
            {
                _Symbol = value;
                RaisePropertyChanged(value);
            }
        }

        PermissionDo _Permission_Symbol;
        public PermissionDo Permission_Symbol
        {
            get => _Permission_Symbol;
            set
            {
                _Permission_Symbol = value;
                RaisePropertyChanged(value);
            }
        }

        private ProductDo _Product;
        [ObjectChangedValidate]
        public ProductDo Product
        {
            get => _Product;
            set
            {
                _Product = value;
                RaisePropertyChanged(value);
            }
        }

        private PermissionDo _Permission_Product;
        public PermissionDo Permission_Product
        {
            get => _Permission_Product;
            set
            {
                _Permission_Product = value;
                RaisePropertyChanged(value);
            }
        }

        private ElectricalCabinetDo _ElectricalCabinet;
        [ObjectChangedValidate]
        public ElectricalCabinetDo ElectricalCabinet
        {
            get => _ElectricalCabinet;
            set
            {
                _ElectricalCabinet = value;
                RaisePropertyChanged(value);
            }
        }

        private PermissionDo _Permission_ElectricalCabinet;
        public PermissionDo Permission_ElectricalCabinet
        {
            get => _Permission_ElectricalCabinet;
            set
            {
                _Permission_ElectricalCabinet = value;
                RaisePropertyChanged(value);
            }
        }

        private DateTime? _ElectricaCabinetCompleted;
        [ObjectChangedValidate]
        public DateTime? ElectricaCabinetCompleted
        {
            get => _ElectricaCabinetCompleted;
            set
            {
                _ElectricaCabinetCompleted = value;
                RaisePropertyChanged(value);
            }
        }


        private PermissionDo _Permission_ElectricaCabinetCompleted;
        public PermissionDo Permission_ElectricaCabinetCompleted
        {
            get => _Permission_ElectricaCabinetCompleted;
            set
            {
                _Permission_ElectricaCabinetCompleted = value;
                RaisePropertyChanged(value);
            }
        }

        private DateTime? _ElectricalDiagramCompleted;
        public DateTime? ElectricalDiagramCompleted
        {
            get => _ElectricalDiagramCompleted;
            set
            {
                _ElectricalDiagramCompleted = value;
                RaisePropertyChanged(value);
            }
        }

        private PermissionDo _Permission_ElectricalDiagramCompleted;
        public PermissionDo Permission_ElectricalDiagramCompleted
        {
            get => _Permission_ElectricalDiagramCompleted;
            set
            {
                _Permission_ElectricalDiagramCompleted = value;
                RaisePropertyChanged(value);
            }
        }

        private DateTime? _ControlCabinetWorkStartDate;
        public DateTime? ControlCabinetWorkStartDate
        {
            get => _ControlCabinetWorkStartDate;
            set
            {
                _ControlCabinetWorkStartDate = value;
                RaisePropertyChanged(value);
            }
        }

        private PermissionDo _Permission_ControlCabinetWorkStartDate;
        public PermissionDo Permission_ControlCabinetWorkStartDate
        {
            get => _Permission_ControlCabinetWorkStartDate;
            set
            {
                _Permission_ControlCabinetWorkStartDate = value;
                RaisePropertyChanged(value);
            }
        }

        private EmployeeDo _Electrician;
        public EmployeeDo Electrician
        {
            get => _Electrician;
            set
            {
                _Electrician = value;
                RaisePropertyChanged(value);
            }
        }

        private PermissionDo _Permission_Electrician;
        public PermissionDo Permission_Electrician
        {
            get => _Permission_Electrician;
            set
            {
                _Permission_Electrician = value;
                RaisePropertyChanged(value);
            }
        }

        private DateTime? _MachineWiredAndTested;
        public DateTime? MachineWiredAndTested
        {
            get => _MachineWiredAndTested;
            set
            {
                _MachineWiredAndTested = value;
                RaisePropertyChanged(value);
            }
        }

        private PermissionDo _Permission_MachineWiredAndTested;
        public PermissionDo Permission_MachineWiredAndTested
        {
            get => _Permission_MachineWiredAndTested;
            set
            {
                _Permission_MachineWiredAndTested = value;
                RaisePropertyChanged(value);
            }
        }

        public override void Clear()
        {
            Objects?.Clear();
            Symbol = default;
            Product = default;
            ElectricalCabinet = default;
            ElectricaCabinetCompleted = default;
            ElectricalDiagramCompleted = default;
            ControlCabinetWorkStartDate = default;
            Electrician = default;
            MachineWiredAndTested = default;
        }

        public override BaseDo GetDo()
        {
            currentOrderItem.LastModified = _appStateService.LoggedUser.Username;
            currentOrderItem.ElectricalCabinet = ElectricalCabinet;
            currentOrderItem.ElectricaCabinetCompleted = ElectricaCabinetCompleted;
            currentOrderItem.ElectricalDiagramCompleted = ElectricalDiagramCompleted;
            currentOrderItem.ControlCabinetWorkStartDate = ControlCabinetWorkStartDate;
            currentOrderItem.Electrician = Electrician;
            currentOrderItem.MachineWiredAndTested = MachineWiredAndTested;
            return currentOrderItem;
        }

        public override Type GetModelType() => typeof(OrderItemDo);

        protected override string GetObjectsListViewTitle() => "Personalizacja";

        protected async override Task LoadObject()
        {
            Clear();

            var orderItem = await ExecuteApiRequest(_orderService.GetOrderItem, _appStateService.LoggedUser.Username, objectId);
            if (orderItem != null)
                Setup(orderItem);
            else
                _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);
        }

        void Setup(OrderItemDo orderItem)
        {
            ElectricalCabinet = orderItem.ElectricalCabinet;
            Electrician = orderItem.Electrician;
            ElectricalDiagramCompleted = orderItem.ElectricalDiagramCompleted;
            ControlCabinetWorkStartDate = orderItem.ControlCabinetWorkStartDate;
            ElectricaCabinetCompleted = orderItem.ElectricaCabinetCompleted;
            MachineWiredAndTested = orderItem.MachineWiredAndTested;
            LastModified = orderItem.LastModified;
        }

        protected override async Task<bool> SaveNewObject() => false;

        protected async override Task<bool> UpdateExistingObject()
        {
            var orderItem = GetDo() as OrderItemDo;
            var updated = await _orderService.UpdateElectricOrderItem(_appStateService.LoggedUser.Username, orderItem);
            return updated != null;
        }
    }
}
