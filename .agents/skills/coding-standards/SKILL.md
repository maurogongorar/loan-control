# Skill: Estándares de Código C# (.NET 10)

## 1. Propósito y Alcance

Garantizar la uniformidad, legibilidad y mantenibilidad del código fuente C# en todo el proyecto. Este documento define las reglas obligatorias de estilo, estructura y convenciones de nombres que debe seguir todo desarrollador o IA al escribir, modificar o revisar código dentro del ecosistema .NET 10.

---

## 2. Estructura de Bloques de Código

### 2.1 Llaves Obligatorias

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

## 3. Organización Interna de una Clase

Los miembros de una clase deben organizarse en el siguiente orden estricto, con una **línea en blanco de separación** entre cada miembro:

1. **Constantes** (`const`)
2. **Campos y propiedades estáticos** (`static`)
3. **Campos de instancia**
4. **Propiedades de instancia**
5. **Constructores**
6. **Destructores / Finalizadores**
7. **Métodos**

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

## 4. Convenciones de Nombres

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

---

## 5. Uso de `this`

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

## 6. Longitud Máxima de Línea

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

## 7. Prohibición de APIs Obsoletas

**No** se deben usar propiedades, métodos, atributos, clases o cualquier miembro marcado como `[Obsolete]`. Siempre se debe utilizar el reemplazo recomendado.

**Ejemplos conocidos**:

| Obsoleto                                        | Reemplazo                                        |
|-------------------------------------------------|--------------------------------------------------|
| `Avalonia.Controls.Window.SystemDecorations`    | `Avalonia.Controls.Window.WindowDecorations`     |

> Esta tabla debe extenderse a medida que se identifiquen más APIs obsoletas en el proyecto.
