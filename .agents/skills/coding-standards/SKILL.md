---
name: coding-standards
description: Usa este skill para garantizar la uniformidad, legibilidad y mantenibilidad del código fuente C# en todo el proyecto. Este documento define las reglas obligatorias de estilo, estructura y convenciones de nombres que debe seguir todo desarrollador o IA al escribir, modificar o revisar código dentro del ecosistema .NET 10.
metadata:
  short-description: estándar de codificación para manetener uniformidad en todo el código y evitar code smells
---

# Estándares de Código C# (.NET 10)

## Propósito y Alcance

Garantizar la uniformidad, legibilidad y mantenibilidad del código fuente C# en todo el proyecto. Este documento define las reglas obligatorias de estilo, estructura y convenciones de nombres que debe seguir todo desarrollador o IA al escribir, modificar o revisar código dentro del ecosistema .NET 10.

---

## Estructura de Bloques de Código

### Llaves Obligatorias

Todos los bloques de código (`if`, `else`, `for`, `foreach`, `while`, `do`, `using`, `lock`, etc.) **deben** incluir llaves `{}`, incluso cuando contengan una sola línea.

**Correcto**:

```csharp
if (condition)
{
	DoSomething();
}
```

**Incorrecto**:

```csharp
if (condition)
	DoSomething();
```

---

## Organización Interna de una Clase

Los miembros de una clase deben organizarse en el siguiente orden estricto, con una **línea en blanco de separación** entre cada miembro:

1. **Constantes** (`const`)
2. **Campos y propiedades estáticos** (`static`)
3. **Campos de instancia**
4. **Propiedades de instancia**
5. **Eventos**
6. **Constructores**
7. **Destructores / Finalizadores**
8. **Métodos**

Dentro de cada grupo, los miembros deben estar ordenados **alfabéticamente** por nombre.

**Ejemplo**:

```csharp
public class LoanService
{
	private const int MaxRetries = 3;

	private const string ServiceName = "LoanService";

	private static readonly ILogger Logger = LoggerFactory.CreateLogger<LoanService>();

	private static int InstanceCount = 0;

	private readonly ILoanRepository _loanRepository;

	private readonly INotificationService _notificationService;

	public decimal InterestRate { get; set; }

	public string Name { get; set; }

	public LoanService(ILoanRepository loanRepository, INotificationService notificationService)
	{
		this._loanRepository = loanRepository;
		this._notificationService = notificationService;
	}

	~LoanService()
	{
	}

	public void Calculate()
	{
		this._loanRepository.GetAll();
	}

	public void Notify()
	{
		this._notificationService.Send();
	}
}
```

---

## Convenciones de Nombres

Los nombres de cualquier elemento debe ser en inglés y lo suficientemente descriptivo para que cualquier persona, incluso sin ser desarrollador, sepa de que se trata ese elemento.

| Elemento               | Convención       | Ejemplo                          |
|------------------------|------------------|----------------------------------|
| Clases                 | PascalCase       | `LoanService`                    |
| Interfaces             | I + PascalCase   | `ILoanRepository`                |
| Propiedades            | PascalCase       | `InterestRate`                   |
| Métodos                | PascalCase       | `CalculateLoan()`                |
| Eventos                | PascalCase       | `LoanApproved`                   |
| Campos de instancia    | `_` + camelCase  | `_loanRepository`                |
| Constantes             | PascalCase       | `MaxRetries`                     |
| Campos estáticos       | PascalCase       | `InstanceCount`                  |
| Variables              | camelCase        | `monthlyInterest`

---

## Documentación en código

Se debe agregar documentación en código de manera obligatoria a algunos elementos en el código. Esta documentación debe ser en español y debe explicar el objetivo del elemento y como se puede usar, ademas de otras indicaciones dependiendo del tipo de elemento.

### Documentación de miembros públicos

Todo miembro de una clase que su modificador de acceso sea público debe estar documentado. Esta documentación debe ser acorde al miembro que se esta documentando.

```csharp
/// <summary>
/// Gets or sets the search text used for filtering loan search results.
/// </summary>
/// <value>
/// The search text as a string. It is initialized to an empty string.
/// </value>
[ObservableProperty]
[NotifyCanExecuteChangedFor(nameof(SearchCommand))]
public partial string SearchText { get; set; } = string.Empty;

/// <summary>
/// Calculates the interest based on the current debt and the interest rate.
/// </summary>
/// <param name="currentDebt">The current debt.</param>
/// <param name="interest">The interest rate.</param>
/// <returns>The calculated interest as a decimal.</returns>
public decimal CalculateInterest(decimal currentDebt, double interest)
{
    return currentDebt * (decimal)interest;
}

/// <summary>
/// Asynchronously waits for the search results to be fetched and returns the count of results.
/// </summary>
/// <returns>
/// A <see cref="Task{T}"/> that represents the asynchronous operation.
/// The task result contains the count of search results.
/// </returns>
/// <exception cref="InvalidOperationException">No search results found.</exception>
public async Task<int> WaitForSearchResultsAsync()
{
    // Simulate a delay for fetching search results
    await Task.Delay(1000);

    if (this.SearchResults.Count == 0)
    {
        throw new InvalidOperationException("No search results found.");
    }

    return this.SearchResults.Count;
}
```

### Documentación de clases, enumeraciones, interfaces y estructuras

Toda clase, estructura, interfaz, enumeración o similar que sea definida a nivel de namespace debe tener su documentación.

** Excepción **: Si este elemento es creado anidado en una clase o similar, será obligatoria la documentación sólo si es declarado como público.

Algunos ejemplos:

```csharp
/// <summary>
/// This class represents the view model for managing loans in the application.
/// It provides properties and commands for searching, creating, and viewing loan details.
/// </summary>
/// <seealso cref="Cocosoft.Finance.LoanControl.App.Avalonia.ViewModels.ViewModelBase" />
public partial class LoansViewModel : ViewModelBase
{
}

/// <summary>
/// This class represents the view model for the dashboard view in the loan control application.
/// It provides properties and commands to manage and display financial data related to loans,
/// such as collected capital, interest, pending due amounts, and total loans granted.
/// The view model also includes navigation actions for creating new loans and searching existing loans.
/// </summary>
/// <param name="serviceProvider">The service provider used for dependency injection and service resolution.</param>
/// <seealso cref="Cocosoft.Finance.LoanControl.App.Avalonia.ViewModels.ViewModelBase" />
public partial class DashboardViewModel(IServiceProvider serviceProvider) : ViewModelBase
{
}

/// <summary>
/// The <see cref="MvvmForm{TViewModel}"/> class is a base class for Windows Forms that follows the MVVM pattern.
/// </summary>
/// <typeparam name="TViewModel">The type of the view model.</typeparam>
/// <seealso cref="System.Windows.Forms.Form" />
public abstract class MvvmForm<TViewModel> : Form where TViewModel : ViewModelBase
{
}

/// <summary>
/// The interface IMvvmView represents a view in the Model-View-ViewModel (MVVM) pattern that is associated with a
/// specific view model of type TViewModel.
/// </summary>
/// <typeparam name="TViewModel">The type of the view model.</typeparam>
/// <seealso cref="System.IDisposable" />
public interface IMvvmView<TViewModel> : IDisposable where TViewModel : ViewModelBase
{
    /// <summary>
    /// Gets the view model associated with the view.
    /// </summary>
    /// <value>
    /// The view model of type TViewModel that is associated with the view.
    /// </value>
    TViewModel ViewModel { get; }
}
```

### Comentarios

** TODO ** comentario que se realice en el codigo (C#, XAML, XML, cualqueira) debe ser en inglés.

---

## Uso de `this`

Para llamar a cualquier miembro de la propia clase (campos, propiedades, métodos o eventos) se **debe** usar siempre el calificador `this.` para distinguir claramente los accesos internos de los externos.

**Correcto**:

```csharp
public void Process()
{
	this._loanRepository.Save(this.CurrentLoan);
	this.OnLoanProcessed();
}
```

**Incorrecto**:

```csharp
public void Process()
{
	_loanRepository.Save(CurrentLoan);
	OnLoanProcessed();
}
```

---

## Longitud Máxima de Línea

La longitud máxima de una línea de código es de **120 caracteres**. Si una línea excede este límite, debe dividirse de forma legible.

**Ejemplo de división**:

```csharp
public void RegisterLoan(
	string borrowerName,
	decimal amount,
	decimal interestRate,
	int termInMonths)
{
	this._loanRepository.Register(
		borrowerName,
		amount,
		interestRate,
		termInMonths);
}
```

---

## ViewModels y [ObservableProperty]

En los ViewModels, el atributo `[ObservableProperty]` **debe** aplicarse únicamente a propiedades parciales (`partial`), **nunca** a campos privados.

**Correcto**:

```csharp
public partial class ClientsViewModel : ViewModelBase
{
	[ObservableProperty]
	public partial string ClientName { get; set; } = string.Empty;
}
```

**Incorrecto**:

```csharp
public partial class ClientsViewModel : ViewModelBase
{
	[ObservableProperty]
	private string _clientName = string.Empty;
}
```

---

## Prohibición de APIs Obsoletas

**No** se deben usar propiedades, métodos, atributos, clases o cualquier miembro marcado como `[Obsolete]`. Siempre se debe utilizar el reemplazo recomendado.

**Ejemplos conocidos**:

| Obsoleto                                        | Reemplazo                                        |
|-------------------------------------------------|--------------------------------------------------|
| `Avalonia.Controls.Window.SystemDecorations`    | `Avalonia.Controls.Window.WindowDecorations`     |

> Esta tabla debe extenderse a medida que se identifiquen más APIs obsoletas en el proyecto.
