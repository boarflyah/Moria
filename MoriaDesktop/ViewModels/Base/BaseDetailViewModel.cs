using System.Reflection;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using MoriaBaseServices;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Contacts;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaDesktopServices.Interfaces.ViewModels;
using MoriaModelsDo.Attributes;

namespace MoriaDesktop.ViewModels.Base;
public abstract class BaseDetailViewModel : ViewModelBase, INavigationAware
{
    #region fields

    readonly IApiLockService _apiLockService;

    protected int objectId;
    protected bool isNew;

    //set of properties names, which are decorated with ObjectChangedValidateAttribute 
    private readonly Lazy<HashSet<string>> _objectChangingProperties;

    #endregion

    protected BaseDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, IApiLockService apiLockService, INavigationService navigationService) : base(logger, appStateService, navigationService)
    {
        _apiLockService = apiLockService;

        _objectChangingProperties = new Lazy<HashSet<string>>(() =>
        new HashSet<string>(
                GetType()
                .GetProperties()
                .Where(x => x.GetCustomAttribute<ObjectChangedValidateAttribute>() != null)
                .Select(x => x.Name)
            )
        );

        SaveCommand = new AsyncRelayCommand(Save, CanSave);
        SaveAndCloseCommand = new AsyncRelayCommand(SaveAndClose, CanSave);
        CloseCommand = new RelayCommand(Close);
        EditCommand = new AsyncRelayCommand(Edit, CanEdit);
        UnlockCommand = new(Unlock, CanUnlock);
    }

    #region properties

    private bool _IsLocked;
    public bool IsLocked
    {
        get => _IsLocked;
        set
        {
            _IsLocked = value;
            RaisePropertyChanged(value);
            SaveCommand.NotifyCanExecuteChanged();
            SaveAndCloseCommand.NotifyCanExecuteChanged();
        }
    }


    private string _LockedBy;
    public string LockedBy
    {
        get => _LockedBy;
        set
        {
            _LockedBy = value;
            RaisePropertyChanged(value);
        }
    }


    private bool _HasObjectChanged;
    public bool HasObjectChanged
    {
        get => _HasObjectChanged;
        set
        {
            _HasObjectChanged = value;
            RaisePropertyChanged(value);
            SaveCommand.NotifyCanExecuteChanged();
            SaveAndCloseCommand.NotifyCanExecuteChanged();
        }
    }


    private bool _IsAdminViewing;
    public bool IsAdminViewing
    {
        get => _IsAdminViewing;
        set
        {
            _IsAdminViewing = value;
            RaisePropertyChanged(value);
        }
    }

    #endregion

    #region Commands

    public AsyncRelayCommand SaveCommand
    {
        get;
    }
    public AsyncRelayCommand SaveAndCloseCommand
    {
        get;
    }
    public ICommand CloseCommand
    {
        get;
    }
    public AsyncRelayCommand EditCommand
    {
        get;
    }

    public AsyncRelayCommand UnlockCommand
    {
        get;
    }

    protected async virtual Task Save()
    {
        try
        {
            _appStateService.SetupLoading(true);
            var succeeded = false;
            if (isNew)
                succeeded = await SaveNewObject();
            else
                succeeded = await UpdateExistingObject();

            if (succeeded)
            {
                IsLocked = true;
                HasObjectChanged = false;
                isNew = false;
                await Load();
            }
        }
        catch (MoriaAppException mae) when (mae.Reason == MoriaAppExceptionReason.ReAuthorizationCancelled)
        {
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Warning, "Anulowano ponowną autoryzację", true);
        }
        catch (Exception ex)
        {
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Error, ex.Message, true);
        }
        finally
        {
            _appStateService.SetupLoading();
        }
    }

    protected async virtual Task SaveAndClose()
    {
        try
        {
            _appStateService.SetupLoading(true);
            var succeeded = false;
            if (isNew)
                succeeded = await SaveNewObject();
            else
                succeeded = await UpdateExistingObject();

            if (succeeded)
            {
                if (_navigationService.CanGoBack)
                    _navigationService.GoBack();
                else
                    _navigationService.NavigateTo(typeof(EmployeeListViewModel), true);
            }
        }
        catch (MoriaAppException mae) when (mae.Reason == MoriaAppExceptionReason.ReAuthorizationCancelled)
        {
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Warning, "Anulowano ponowną autoryzację", true);
        }
        catch (Exception ex)
        {
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Error, ex.Message, true);
        }
        finally
        {
            _appStateService.SetupLoading();
        }
    }

    protected virtual bool CanSave() => !IsLocked && HasObjectChanged;

    protected virtual void Close()
    {
        if (_navigationService.CanGoBack)
            _navigationService.GoBack();
        else
            _navigationService.NavigateTo(typeof(EmployeeListViewModel), true);
    }

    protected async virtual Task Edit()
    {
        try
        {
            _appStateService.SetupLoading(true);

            var editable = await ExecuteApiRequest(_apiLockService.Lock, _appStateService.LoggedUser.Username, GetModelType(), objectId);
            if (editable)
                IsLocked = false;
            else
                _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Warning, "Nie udało się włączyć edycji obiektu", true);

            await Load();
        }
        catch (MoriaAppException mae) when (mae.Reason == MoriaAppExceptionReason.ReAuthorizationCancelled)
        {
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Warning, "Anulowano ponowną autoryzację", true);
        }
        catch (MoriaApiException apiException)
        {
            if (apiException.Reason == MoriaApiExceptionReason.ObjectIsLocked)
                _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Warning, $"Obiekt jest w edycji przez innego użytkownika: {apiException.AdditionalMessage}", true);
            else if (apiException.Reason == MoriaApiExceptionReason.ObjectNotFound)
                _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Warning, $"Obiektu nie znaleziono", true);
            else
                _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Warning, "Nieznany błąd", true);
        }
        catch (Exception ex)
        {
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Error, ex.Message, true);
        }
        finally
        {
            _appStateService.SetupLoading();
        }
    }

    protected virtual bool CanEdit() => !isNew;

    protected async virtual Task Unlock()
    {
        try
        {
            _appStateService.SetupLoading(true);
            await _apiLockService.Unlock(_appStateService.LoggedUser.Username, _appStateService.CurrentDetailViewObjectType, _appStateService.CurrentDetailViewObjectId);
        }
        catch (Exception ex)
        {
        }
        finally
        {
            _appStateService.SetupLoading();
        }
    }

    protected virtual bool CanUnlock() => IsAdminViewing && IsLocked;

    #endregion

    #region events

    protected virtual void BaseDetailViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        //if (CheckPropertyName(e.PropertyName))
        if (!HasObjectChanged && CheckHasObjectChanged(e.PropertyName))
            HasObjectChanged = true;
    }

    #endregion

    #region methods

    public async virtual Task Load()
    {
        PropertyChanged -= BaseDetailViewModel_PropertyChanged;

        if (!isNew)
            try
            {
                _appStateService.SetupLoading(true);

                await LoadObject();
            }
            catch (MoriaAppException mae) when (mae.Reason == MoriaAppExceptionReason.ReAuthorizationCancelled)
            {
                _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Warning, "Anulowano ponowną autoryzację", true);
            }
            catch (Exception ex)
            {
                _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Error, ex.Message, true);
            }
            finally
            {
                _appStateService.SetupLoading();
                PropertyChanged += BaseDetailViewModel_PropertyChanged;
            }
    }

    /// <summary>
    /// Method used in view's code behind - when indication of ability to save to database occurs in view (e.g. PasswordBox has changed it's value)
    /// </summary>
    /// <param name="value"></param>
    public void SetHasObjectChanged(bool value) => HasObjectChanged = value;

    /// <summary>
    /// 
    /// </summary>
    /// <returns>Type of BaseDo related with table in database</returns>
    protected abstract Type GetModelType();

    /// <summary>
    /// Clear view properties and send get request to obtain object from database, assign values to view properties
    /// </summary>
    /// <returns></returns>
    protected abstract Task LoadObject();

    /// <summary>
    /// Prepare body object, send post request to create new object in database, if succeeded - assign <see cref="objectId"/> and values to view properties
    /// </summary>
    /// <returns>true - object created in database, false - post request failed, object not created in database</returns>
    protected abstract Task<bool> SaveNewObject();

    /// <summary>
    /// Prepare body object, send put request to update object in database
    /// </summary>
    /// <returns>true - object updated in database, false - put request failed, object not updated in database</returns>
    protected abstract Task<bool> UpdateExistingObject();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="propertyName"></param>
    /// <returns>true - if changed property enables ability to save object to database</returns>
    protected virtual bool CheckHasObjectChanged(string propertyName)
    {
        return _objectChangingProperties.Value.Contains(propertyName);
    }

    #region INavigationAware implementation

    public virtual void OnNavigatedTo(params object[] parameters)
    {
        if (parameters.Any() && parameters.First() is int id)
        {
            IsLocked = true;
            objectId = id;
            _appStateService.CurrentDetailViewObjectType = GetModelType();
            _appStateService.CurrentDetailViewObjectId = objectId;
        }
        else
            isNew = true;

        IsAdminViewing = _appStateService.LoggedUser?.Admin ?? false;

        UnlockCommand.NotifyCanExecuteChanged();
        EditCommand.NotifyCanExecuteChanged();
    }

    public async virtual Task OnNavigatingFrom()
    {
        _appStateService.CurrentDetailViewObjectId = 0;
        _appStateService.CurrentDetailViewObjectType = null;
        if (!IsLocked && !isNew)
            try
            {
                _appStateService.SetupLoading(true);

                await ExecuteApiRequest(_apiLockService.Unlock, _appStateService.LoggedUser.Username, GetModelType(), objectId);
            }
            catch (MoriaAppException mae) when (mae.Reason == MoriaAppExceptionReason.ReAuthorizationCancelled)
            {
                _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Warning, "Anulowano ponowną autoryzację", true);
            }
            catch (MoriaApiException apiException)
            {
                if (apiException.Reason == MoriaApiExceptionReason.ObjectIsLocked)
                    _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Warning, $"Obiekt jest w edycji przez innego użytkownika: {apiException.AdditionalMessage}", true);
                else if (apiException.Reason == MoriaApiExceptionReason.ObjectNotFound)
                    _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Warning, $"Obiektu nie znaleziono", true);
                else
                    _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Warning, "Nieznany błąd", true);
            }
            catch (Exception ex)
            {
                _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Error, ex.Message, true);
            }
            finally
            {
                _appStateService.SetupLoading();
            }
    }

    #endregion

    #endregion
}
