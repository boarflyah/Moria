using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using MoriaBaseModels.Models;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Base;

public class LookupWindowViewModel : ViewModelBase
{
    readonly IApiLookupService _apiService;

    Type currentType;

    public LookupWindowViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, INavigationService navigationService,
        IApiLookupService apiService) : base(logger, appStateService, navigationService)
    {
        _apiService = apiService;

        Objects = new();
        SearchCommand = new(Search);
    }

    #region properties

    int lastId;
    bool hasNext;
    int pageSize = 100;
    bool createNew;

    public ObservableCollection<LookupModel> Objects
    {
        get;
        set;
    }

    private LookupHeadersMetadata _LookupMetadata;
    public LookupHeadersMetadata LookupMetadata
    {
        get => _LookupMetadata;
        set
        {
            _LookupMetadata = value;
            RaisePropertyChanged(value);
        }
    }

    private LookupModel _Selected;
    public LookupModel Selected
    {
        get => _Selected;
        set
        {
            _Selected = value;
            RaisePropertyChanged(value);
        }
    }


    private bool _Accepted;
    public bool Accepted
    {
        get => _Accepted;
        set
        {
            _Accepted = value;
            RaisePropertyChanged(value);
        }
    }


    private string _SearchText;
    public string SearchText
    {
        get => _SearchText;
        set
        {
            _SearchText = value;
            RaisePropertyChanged(value);
        }
    }

    #endregion

    #region

    public AsyncRelayCommand SearchCommand
    {
        get;
    }

    #endregion

    #region Methods



    public async Task LoadNext()
    {
        try
        {
            Objects.Clear();
            //TODO use ExecuteApiRequest and wrap with try catch
            var pagedList = await ExecuteApiRequest(_apiService.GetObjects, _appStateService.LoggedUser.Username, currentType, 0, pageSize);

            lastId = pagedList.LastId;
            hasNext = pagedList.HasNext;
            LookupMetadata = pagedList.LookupMetadata;
            foreach (var element in pagedList)
                Objects.Add(element);
        }
        catch (Exception ex)
        {
            ;
        }
    }

    public async Task Search()
    {
        try
        {
            Objects.Clear();
            //TODO use ExecuteApiRequest and wrap with try catch
            var pagedList = await ExecuteApiRequest(_apiService.GetFilteredObjects, _appStateService.LoggedUser.Username, currentType, SearchText);

            lastId = pagedList.LastId;
            hasNext = pagedList.HasNext;
            LookupMetadata = pagedList.LookupMetadata;
            foreach (var element in pagedList)
                Objects.Add(element);
        }
        catch (Exception ex)
        {
            ;
        }
    }

    public void SetSelected(LookupModel lookup, bool createNew)
    {
        Selected = lookup;
        this.createNew = createNew;
    }

    public void SetType<T>() where T : BaseDo
        => currentType = typeof(T);

    public LookupWrapper<T> OnClosed<T>() where T: BaseDo, new()
    {
        if (Selected != null)
        {
            var obj = new T();
            obj.SetObject(Selected);
            return new()
            {
                Selected = obj
            };
        }
        else
            return new()
            {
                CreateNew = createNew
            };
    }

    #endregion
}
