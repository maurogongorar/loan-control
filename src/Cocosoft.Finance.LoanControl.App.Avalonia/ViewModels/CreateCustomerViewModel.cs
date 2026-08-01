using Cocosoft.Finance.LoanControl.App.Avalonia.Services;
using Cocosoft.Finance.LoanControl.Core.Services.V2;
using Cocosoft.Finance.LoanControl.Domain.Customers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Cocosoft.Finance.LoanControl.App.Avalonia.ViewModels;

public partial class CreateCustomerViewModel(
    IDialogService dialogService,
    ICustomerService customerService,
    ILogger<CreateCustomerViewModel> logger)
    : ViewModelBase
{
    /// <summary>
    /// Gets or sets the address.
    /// </summary>
    /// <value>
    /// The address.
    /// </value>
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
    public partial string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the city.
    /// </summary>
    /// <value>
    /// The city.
    /// </value>
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
    public partial string City { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the document number.
    /// </summary>
    /// <value>
    /// The document number.
    /// </value>
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
    public partial string DocumentNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email.
    /// </summary>
    /// <value>
    /// The email.
    /// </value>
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
    public partial string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the first name.
    /// </summary>
    /// <value>
    /// The first name.
    /// </value>
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
    public partial string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets a value indicating whether any form field has been modified.
    /// </summary>
    public bool IsDirty =>
        !string.IsNullOrEmpty(this.DocumentNumber)
        || !string.IsNullOrEmpty(this.FirstName)
        || !string.IsNullOrEmpty(this.LastName)
        || !string.IsNullOrEmpty(this.Address)
        || !string.IsNullOrEmpty(this.City)
        || !string.IsNullOrEmpty(this.Email)
        || !string.IsNullOrEmpty(this.PhoneNumber);

    /// <summary>
    /// Gets or sets the last name.
    /// </summary>
    /// <value>
    /// The last name.
    /// </value>
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
    public partial string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the phone number.
    /// </summary>
    /// <value>
    /// The phone number.
    /// </value>
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
    public partial string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// Occurs when a customer has been successfully created after user confirmation.
    /// </summary>
    public event Action? CustomerCreated;

    private bool CanSubmit()
    {
        return !string.IsNullOrWhiteSpace(this.Address) && !string.IsNullOrWhiteSpace(this.City)
            && !string.IsNullOrWhiteSpace(this.DocumentNumber) && !string.IsNullOrWhiteSpace(this.Email)
            && !string.IsNullOrWhiteSpace(this.FirstName) && !string.IsNullOrWhiteSpace(this.LastName)
            && !string.IsNullOrWhiteSpace(this.PhoneNumber);
    }

    /// <summary>
    /// Resets all form fields to their default empty values.
    /// </summary>
    public void Reset()
    {
        this.Address = string.Empty;
        this.City = string.Empty;
        this.DocumentNumber = string.Empty;
        this.Email = string.Empty;
        this.FirstName = string.Empty;
        this.LastName = string.Empty;
        this.PhoneNumber = string.Empty;
    }

    [RelayCommand(CanExecute = nameof(CanSubmit))]
    private async Task SubmitAsync()
    {
        var summary = $"Identificaci\u00f3n: {this.DocumentNumber}\n"
            + $"Nombres: {this.FirstName}\n"
            + $"Apellidos: {this.LastName}\n";

        var confirmed = await dialogService.ShowConfirmationAsync(
            "Confirmar Cliente", summary);

        if (confirmed)
        {
            var customer = new CustomerDto
            {
                Address = this.Address,
                City = this.City,
                IdentificationNumber = this.DocumentNumber,
                Email = this.Email,
                FirstName = this.FirstName,
                LastName = this.LastName,
                PhoneNumber = this.PhoneNumber
            };
            
            if (await customerService.AddCustomerAsync(customer) != null)
            {
                this.OnCustomerCreated();
                return;
            }

            // TODO: Show error dialog if customer creation fails
            //await dialogService.ShowErrorAsync(
            //    "Error al crear cliente",
            //    "Ocurri\u00f3 un error al crear el cliente. Por favor, intente nuevamente.");
            logger.LogWarning("Failed to create customer with ID {DocumentNumber}", this.DocumentNumber);
        }
    }

    private void OnCustomerCreated() => this.CustomerCreated?.Invoke();
}
