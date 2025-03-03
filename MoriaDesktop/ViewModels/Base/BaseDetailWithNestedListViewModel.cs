using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Base;

namespace MoriaDesktop.ViewModels.Base;
public abstract class BaseDetailWithNestedListViewModel : BaseDetailViewModel
{
    protected BaseDetailWithNestedListViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, IApiLockService apiLockService, INavigationService navigationService, IKeepAliveWorker keepAliveWorker) 
        : base(logger, appStateService, apiLockService, navigationService, keepAliveWorker)
    {
        Objects = new();
        NestedNewCommand = new(NestedNew, CanNestedNew);
        NestedDeleteCommand = new(NestedDelete, CanNestedDelete);
    }

    #region Properties

    public ObservableCollection<BaseDo> Objects
    {
        get; set;
    }


    private BaseDo _Selected;
    public BaseDo Selected
    {
        get => _Selected;
        set
        {
            _Selected = value;
            RaisePropertyChanged(value);
            NestedDeleteCommand?.NotifyCanExecuteChanged();
        }
    }

    public string LabelTitle => GetObjectsListViewTitle();

    #endregion

    #region Commands

    public AsyncRelayCommand NestedNewCommand
    {
        get;
    }

    public AsyncRelayCommand NestedDeleteCommand
    {
        get;
    }

    #endregion

    #region Commands methods

    protected async virtual Task NestedNew()
    {
    
    }

    /// <summary>
    /// Button not visible in UI when false
    /// </summary>
    /// <returns></returns>
    protected virtual bool CanNestedNew() => true;

    protected async virtual Task NestedDelete()
    {
        if (Selected != null)
        {
            Selected.ChangeType = MoriaModelsDo.Base.Enums.SystemChangeType.Deleted;
            HasObjectChanged = true;
        }
    }

    /// <summary>
    /// Button not visible in UI when false
    /// </summary>
    /// <returns></returns>
    protected virtual bool CanNestedDelete() => Selected != null;

    #endregion

    #region Methods

    protected abstract string GetObjectsListViewTitle();

    #endregion
}
