using Cocosoft.Finance.LoanControl.App.Avalonia.Services;
using Cocosoft.Finance.LoanControl.Core.Services.V2;
using Cocosoft.Finance.LoanControl.Domain.Customers;
using Cocosoft.Finance.LoanControl.Domain.Loans;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace Cocosoft.Finance.LoanControl.App.Avalonia.ViewModels;

public partial class CustomersViewModel : ViewModelBase
{
    private CancellationTokenSource? _cancellationTokenSource;

    private readonly IDictionary<string, CancellationTokenSource?> _ctSources =
        new Dictionary<string, CancellationTokenSource?>
        {
            [nameof(CustomersViewModel.GetNewCustomersCountAsync)] = null,
            [nameof(CustomersViewModel.GetTotalCustomersCountAsync)] = null,
            [nameof(CustomersViewModel.GetCustomersWithActiveLoanCountAsync)] = null
        };

    private readonly ICustomerService _customerService;
    
    private readonly IDialogService _dialogService;
    
    private readonly ILogger _logger;

    private CustomerSearchResult? _selectedSearchResult;

    public CreateCustomerViewModel CreateCustomerForm { get; }

    public Task<int> CustomersWithActiveLoan => this.GetCustomersWithActiveLoanCountAsync();

    [ObservableProperty]
    public partial bool IsCreatingCustomer { get; set; }

    [ObservableProperty]
    public partial bool IsLoading { get; set; }

    [ObservableProperty]
    public partial bool IsShowingDetail { get; set; }

    [ObservableProperty]
    public partial bool IsShowingResults { get; set; }

    public Task<int> NewCustomersCount => this.GetNewCustomersCountAsync();

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SearchCommand))]
    public partial string SearchText { get; set; } = string.Empty;

    [ObservableProperty]
    public partial CustomerDto? SelectedCustomerDetail { get; set; }

    /// <summary>
    /// Gets or sets the currently selected search result. Setting a non-null value
    /// triggers navigation to the loan detail view.
    /// </summary>
    public CustomerSearchResult? SelectedSearchResult
    {
        get => this._selectedSearchResult;
        set
        {
            if (this.SetProperty(ref this._selectedSearchResult, value)
                && value is not null)
            {
                _ = this.SelectCustomerAsync(value);
            }
        }
    }

    public Task<int> TotalCustomersCount => this.GetTotalCustomersCountAsync();

    /// <summary>
    /// Gets the collection of loan search results.
    /// </summary>
    public ObservableCollection<CustomerSearchResult> SearchResults { get; } = [];

    public CustomersViewModel(
        IDialogService dialogService,
        ICustomerService customerService,
        CreateCustomerViewModel createCustomerForm,
        ILogger<CustomersViewModel> logger)
    {
        this._dialogService = dialogService;
        this._customerService = customerService;
        this.CreateCustomerForm = createCustomerForm;
        this.CreateCustomerForm.CustomerCreated += this.OnCustomerCreated;
        this._logger = logger;
    }

    /// <inheritdoc/>
    public override void Reset()
    {
        this.SearchText = string.Empty;
        this.SearchResults.Clear();
        this._selectedSearchResult = null;
        this.OnPropertyChanged(nameof(this.SelectedSearchResult));
        this.SelectedCustomerDetail = null;
        this.IsCreatingCustomer = false;
        this.IsShowingDetail = false;
        this.IsShowingResults = false;
        this.CreateCustomerForm.Reset();
    }

    private bool CanSearch()
    {
        if (!string.IsNullOrWhiteSpace(this.SearchText))
        {
            return true;
        }

        return false;
    }

    [RelayCommand]
    private void CreateCustomer()
    {
        this.IsCreatingCustomer = true;
        this.IsShowingResults = false;
        this.IsShowingDetail = false;
    }

    private async Task<int> GetCustomersWithActiveLoanCountAsync()
    {
        this._ctSources[nameof(this.GetCustomersWithActiveLoanCountAsync)]?.Cancel();
        var cts = new CancellationTokenSource();
        this._ctSources[nameof(this.GetCustomersWithActiveLoanCountAsync)] = cts;
        var cancellationToken = cts.Token;

        try
        {
            return await this._customerService.GetCustomersWithActiveLoanCountAsync(cancellationToken);
        }
        catch (OperationCanceledException)
        {
            this._logger.LogInformation("GetCustomersWithActiveLoanCountAsync operation was canceled.");
            return 0;
        }
        catch (Exception ex)
        {
            // TODO: Handle the exception appropriately (e.g., show an error message to the user)
            this._logger.LogError(ex, "An error occurred while getting the customers with active loan count.");
            return 0;
        }
    }

    private async Task<int> GetNewCustomersCountAsync()
    {
        this._ctSources[nameof(this.GetNewCustomersCountAsync)]?.Cancel();
        var cts = new CancellationTokenSource();
        this._ctSources[nameof(this.GetNewCustomersCountAsync)] = cts;
        var cancellationToken = cts.Token;

        try
        {
            return await this._customerService.GetNewCustomersCountAsync(cancellationToken);
        }
        catch (OperationCanceledException)
        {
            this._logger.LogInformation("GetNewCustomersCountAsync operation was canceled.");
            return 0;
        }
        catch (Exception ex)
        {
            // TODO: Handle the exception appropriately (e.g., show an error message to the user)
            this._logger.LogError(ex, "An error occurred while getting the new customers count.");
            return 0;
        }
    }

    private async Task<int> GetTotalCustomersCountAsync()
    {
        this._ctSources[nameof(this.GetTotalCustomersCountAsync)]?.Cancel();
        var cts = new CancellationTokenSource();
        this._ctSources[nameof(this.GetTotalCustomersCountAsync)] = cts;
        var cancellationToken = cts.Token;

        try
        {
            return await this._customerService.GetCustomersCountAsync(cancellationToken);
        }
        catch (OperationCanceledException)
        {
            this._logger.LogInformation("GetTotalCustomersCountAsync operation was canceled.");
            return 0;
        }
        catch (Exception ex)
        {
            // TODO: Handle the exception appropriately (e.g., show an error message to the user)
            this._logger.LogError(ex, "An error occurred while getting the total customers count.");
            return 0;
        }
    }

    [RelayCommand]
    private async Task GoBackAsync()
    {
        if (this.IsShowingDetail)
        {
            this._selectedSearchResult = null;
            this.OnPropertyChanged(nameof(this.SelectedSearchResult));
            this.IsShowingDetail = false;
            this.IsShowingResults = true;
        }
        else if (this.IsCreatingCustomer)
        {
            if (this.CreateCustomerForm.IsDirty)
            {
                var confirmed = await this._dialogService.ShowConfirmationAsync(
                    "Abandonar formulario",
                    "¿Desea abandonar el formulario? Los datos ingresados se perderán.");

                if (!confirmed)
                {
                    return;
                }
            }

            this.CreateCustomerForm.Reset();
            this.SearchText = string.Empty;
            this.IsCreatingCustomer = false;
        }
    }

    private void OnCustomerCreated()
    {
        this.CreateCustomerForm.Reset();
        this.IsCreatingCustomer = false;
    }

    [RelayCommand(CanExecute = nameof(CanSearch))]
    private async Task Search()
    {
        this.IsLoading = true;
        var loansResult = await this._customerService.SearchByDocumentAsync(this.SearchText);
        this.IsLoading = false;

        this.SearchResults.Clear();

        foreach (var loan in loansResult)
        {
            this.SearchResults.Add(loan);
        }

        this.IsShowingResults = true;
        this.IsCreatingCustomer = false;
        this.IsShowingDetail = false;
    }

    private async Task SelectCustomerAsync(CustomerSearchResult? customer)
    {
        if (customer is null)
        {
            return;
        }

        this._cancellationTokenSource?.Cancel();
        this._cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = this._cancellationTokenSource.Token;

        try
        {
            this.IsLoading = true;
            var customerDetail = await this._customerService.FindCustomerAsync(customer.Id, cancellationToken);

            if (customerDetail is null)
            {
                return;
            }

            this.SelectedCustomerDetail = customerDetail;
            this.IsShowingDetail = true;
            this.IsCreatingCustomer = false;
            this.IsShowingResults = false;
        }
        catch (OperationCanceledException)
        {
            // The operation was canceled, no action needed.
            this._logger.LogInformation("Customer selection operation was canceled.");
        }
        catch (Exception ex)
        {
            // TODO: Handle the exception (e.g., log it, show an error message to the user)
            this._logger.LogError(ex, "An error occurred while selecting the customer.");
        }
        finally
        {
            this.IsLoading = false;
        }
    }
}
