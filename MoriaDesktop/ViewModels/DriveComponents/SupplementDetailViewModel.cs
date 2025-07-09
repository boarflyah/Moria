using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MoriaDesktop.Attributes;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaDesktopServices.Services.API;
using MoriaModelsDo.Attributes;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.DriveComponents;

namespace MoriaDesktop.ViewModels.DriveComponents
{
    public class SupplementDetailViewModel : BaseDetailViewModel
    {
        readonly IApiSupplementService _apiSupplementService;
        public SupplementDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, IApiLockService apiLockService, INavigationService navigationService, IKeepAliveWorker keepAliveWorker, IApiSupplementService apiSupplementService) : base(logger, appStateService, apiLockService, navigationService, keepAliveWorker)
        {
            _apiSupplementService = apiSupplementService;
        }

        string _Size;
        [ObjectChangedValidate]
        public string Size
        {
            get => _Size;
            set
            {
                _Size = value;
                RaisePropertyChanged(value);
            }
        }

        string _Type;
        [ObjectChangedValidate]
        [DefaultProperty]
        public string Type
        {
            get => _Type;
            set
            {
                _Type = value;
                RaisePropertyChanged(value);
            }
        }


        string _IProperty;
        [ObjectChangedValidate]
        public string IProperty
        {
            get => _IProperty;
            set
            {
                _IProperty = value;
                RaisePropertyChanged(value);
            }
        }

        PermissionDo _Permission_Size;
        public PermissionDo Permission_Size
        {
            get => _Permission_Size;
            set
            {
                _Permission_Size = value;
                RaisePropertyChanged(value);
            }
        }

        PermissionDo _Permission_Type;
        public PermissionDo Permission_Type
        {
            get => _Permission_Type;
            set
            {
                _Permission_Type = value;
                RaisePropertyChanged(value);
            }
        }

        PermissionDo _Permission_IProperty;
        public PermissionDo Permission_IProperty
        {
            get => _Permission_IProperty;
            set
            {
                _Permission_IProperty = value;
                RaisePropertyChanged(value);
            }
        }

        public override void Clear()
        {
            Type = string.Empty;
            Size = string.Empty;
            IProperty = string.Empty;
            LastModified = string.Empty;
        }
        void Setup(SupplementDo pumpDo)
        {
            Type = pumpDo.Type;
            Size = pumpDo.Size;
            IProperty = pumpDo.IProperty;
            LastModified = pumpDo.LastModified;
        }
        public override BaseDo GetDo()
            => new SupplementDo()
            {
                Id = objectId,
                Type = this.Type,
                Size = this.Size,
                IProperty = this.IProperty,
                LastModified = _appStateService.LoggedUser.Username,
            };

        public override Type GetModelType() => typeof(SupplementDo);
        protected async override Task LoadObject()
        {
            Clear();

            var supp = await ExecuteApiRequest(_apiSupplementService.GetSupplement, _appStateService.LoggedUser.Username, objectId);
            if (supp != null)
                Setup(supp);
            else
                _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);
        }

        protected async override Task<bool> SaveNewObject()
        {
            var supp = GetDo() as SupplementDo;
            var newObject = await _apiSupplementService.CreateSupplement(_appStateService.LoggedUser.Username, supp);
            if (newObject != null)
            {
                objectId = newObject.Id;
                return true;
            }
            return false;
        }

        protected async override Task<bool> UpdateExistingObject()
        {
            var supp = GetDo() as SupplementDo;
            var updated = await _apiSupplementService.UpdateSupplement(_appStateService.LoggedUser.Username, supp);
            return updated != null;
        }
    }
}
