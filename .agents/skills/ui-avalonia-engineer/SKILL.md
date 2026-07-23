# Skill: Diseño de UI y Arquitectura Base en Avalonia (MVVM Nativo - .NET 10)

## 1. Propósito y Alcance

Garantizar la consistencia visual, la reutilización de componentes y la separación estricta de responsabilidades en la interfaz de usuario (UI). Este documento define las reglas obligatorias de diseño visual, la identidad gráfica, el manejo de estilos y profundidad, y la jerarquía de vistas que debe seguir todo desarrollador o IA al crear ventanas, diálogos y componentes interactivos dentro del ecosistema Avalonia con MVVM nativo sobre .NET 10.

---

## 2. Arquitectura de Vistas (Jerarquía de Ventanas)

Avalonia soporta MVVM de forma nativa mediante `DataContext`, `Binding`, `ICommand` y `CommunityToolkit.Mvvm`. No se requiere un framework MVVM externo ni propietario.

### 2.1 Enlace MVVM Nativo

**Responsabilidad**:

Gestionar el ciclo de vida MVVM de la ventana mediante las capacidades nativas de Avalonia.

**Funciones**:

- Asignar el `DataContext` (ViewModel) mediante Inyección de Dependencias de .NET 10 (Microsoft.Extensions.DependencyInjection).
- Utilizar `{Binding}` y `{CompiledBinding}` para el enlazado de datos.
- Utilizar `CommunityToolkit.Mvvm` para `ObservableObject`, `RelayCommand` y `ObservableProperty`.
- Utilizar el sistema de mensajería de `CommunityToolkit.Mvvm` (`WeakReferenceMessenger`) para comunicación entre ViewModels.

**Restricción**:

No se debe usar ningún framework MVVM propietario. Avalonia + CommunityToolkit.Mvvm es la base.

---

### 2.2 MainLayoutWindow (Ventana Principal con Layout Dashboard)

**Responsabilidad**:

Proveer el layout maestro de la aplicación estilo Dashboard Moderno.

**Estructura estándar (AXAML)**:

- `NavigationPanel` (Izquierda): Menú lateral fijo con íconos vectoriales minimalistas y selector activo destacado. Implementado con `StackPanel` o `ListBox` con estilo personalizado.
- `HeaderPanel` (Arriba): Barra superior para el Título de la sección activa y acciones globales de la vista.
- `ContentArea` (Centro): `ContentControl` con `Content="{Binding CurrentView}"` donde se inyectan dinámicamente los UserControls de negocio.

**Implementación recomendada**:

```xml
<DockPanel>
  <Border DockPanel.Dock="Left" Classes="navigation-panel">
	<!-- Menú lateral -->
  </Border>
  <Border DockPanel.Dock="Top" Classes="header-panel">
	<!-- Barra superior -->
  </Border>
  <ContentControl Content="{Binding CurrentView}" Classes="content-area" />
</DockPanel>
```

---

### 2.3 BaseModalWindow (Ventana Modal Base)

**Responsabilidad**:

Proveer una apariencia limpia, enmarcada y enfocada para ventanas emergentes.

**Configuración obligatoria**:

```xml
<Window CanResize="False"
		SystemDecorations="None"
		WindowStartupLocation="CenterOwner"
		SizeToContent="WidthAndHeight">
```

---

## 3. Identidad Visual (Design System) y Profundidad

La identidad visual está inspirada en la costa Caribe colombiana, transmitiendo confianza, profesionalismo y estabilidad financiera mediante una estética moderna y limpia.

**Regla de Textura y Profundidad**: La interfaz debe adoptar un estilo Soft-UI / Glassmorphism sutil. Los controles deben poseer relieve mediante sutiles gradientes de fondo, sombras (`BoxShadow`) y bordes redondeados (`CornerRadius`), emulando capas físicas superpuestas.

---

### 3.1 Paleta Institucional

| Token | Color | Hex | Uso |
| ------ | ------ | ------ | ------ |
| Primary | Azul Petróleo Caribe | #0F4C5C | Color institucional principal. Navegación, indicadores de gráficos, encabezados destacados. |
| Secondary | Turquesa Caribe | #147D8A | Componentes interactivos, enlaces, iconografía de tarjetas secundarias y barras de progreso. |
| Accent | Coral Suave | #E07A5F | Call To Action (CTA) especiales, indicadores importantes y pequeños elementos de énfasis (Uso moderado). |
| Background | Arena Clara | #F7F3EE | Fondo general de la aplicación (ContentArea y ventanas base). |
| Surface | Blanco | #FFFFFF | Tarjetas (Cards), paneles flotantes y contenedores de formularios. Deben poseer sombra difuminada. |
| Text Primary | Cacao | #5A3E36 | Texto principal, cifras numéricas grandes y títulos. Sustituye al negro puro para aportar calidez. |
| Text Secondary | Gris Neutro | #6B7280 | Texto descriptivo, ayudas visuales y etiquetas secundarias (Ej. "Duración", "Interés"). |
| Border | Arena Oscura | #D6C9B8 | Bordes sutiles para inputs, separadores y líneas divisorias. |

### 3.2 Colores Semánticos

Los estados del sistema deben utilizar colores universalmente reconocidos y no deben mezclarse con los colores institucionales.

| Token | Color | Hex |
| ------ | ------ | ------ |
| Success | Verde | #2E8B57 |
| Warning | Ámbar | #F59E0B |
| Error | Rojo | #E11D48 |
| Information | Azul | #3B82F6 |

Estos colores únicamente representan estados del sistema. Nunca deben reemplazar los colores institucionales.

---

## 4. Sistema de Estilos y Recursos (AXAML)

Los estilos deben definirse como recursos globales en `App.axaml` o en archivos de recursos separados (`Styles/`) e importados mediante `StyleInclude`.

### 4.1 Definición de Recursos de Color

```xml
<Application.Resources>
  <ResourceDictionary>
	<!-- Paleta Institucional -->
	<Color x:Key="PrimaryColor">#0F4C5C</Color>
	<Color x:Key="SecondaryColor">#147D8A</Color>
	<Color x:Key="AccentColor">#E07A5F</Color>
	<Color x:Key="BackgroundColor">#F7F3EE</Color>
	<Color x:Key="SurfaceColor">#FFFFFF</Color>
	<Color x:Key="TextPrimaryColor">#5A3E36</Color>
	<Color x:Key="TextSecondaryColor">#6B7280</Color>
	<Color x:Key="BorderColor">#D6C9B8</Color>

	<!-- Colores Semánticos -->
	<Color x:Key="SuccessColor">#2E8B57</Color>
	<Color x:Key="WarningColor">#F59E0B</Color>
	<Color x:Key="ErrorColor">#E11D48</Color>
	<Color x:Key="InformationColor">#3B82F6</Color>

	<!-- Brushes -->
	<SolidColorBrush x:Key="PrimaryBrush" Color="{StaticResource PrimaryColor}" />
	<SolidColorBrush x:Key="SecondaryBrush" Color="{StaticResource SecondaryColor}" />
	<SolidColorBrush x:Key="AccentBrush" Color="{StaticResource AccentColor}" />
	<SolidColorBrush x:Key="BackgroundBrush" Color="{StaticResource BackgroundColor}" />
	<SolidColorBrush x:Key="SurfaceBrush" Color="{StaticResource SurfaceColor}" />
	<SolidColorBrush x:Key="TextPrimaryBrush" Color="{StaticResource TextPrimaryColor}" />
	<SolidColorBrush x:Key="TextSecondaryBrush" Color="{StaticResource TextSecondaryColor}" />
	<SolidColorBrush x:Key="BorderBrush" Color="{StaticResource BorderColor}" />
  </ResourceDictionary>
</Application.Resources>
```

### 4.2 Tarjetas (Cards)

Todo contenedor de información debe renderizarse siguiendo este estándar mediante estilos de Avalonia:

```xml
<Style Selector="Border.card">
  <Setter Property="Background" Value="{StaticResource SurfaceBrush}" />
  <Setter Property="CornerRadius" Value="12" />
  <Setter Property="Padding" Value="20" />
  <Setter Property="BoxShadow" Value="0 4 12 0 #0D5A3E36" />
  <Setter Property="BorderBrush" Value="{StaticResource BorderBrush}" />
  <Setter Property="BorderThickness" Value="1" />
</Style>
```

- Estructura Interna:
  - Ícono en la esquina superior izquierda dentro de un `Border` circular con fondo semitransparente del color del estado.
  - Texto descriptivo en color Text Secondary.
  - Valor numérico principal destacado en tamaño grande (24pt a 28pt) en color Text Primary.

### 4.3 Indicadores Gráficos (Gauges y Barras de Score)

Las visualizaciones de datos deben renderizarse utilizando el sistema de dibujo de Avalonia:

- Implementar mediante controles personalizados que sobrescriban `Render(DrawingContext context)`.
- Usar `Arc` con `StrokeLineCap="Round"` para terminaciones suaves.
- **Canal base**: Color Arena Oscura muy tenue (#EADFCF).
- **Progreso**: Renderizado con `LinearGradientBrush` que transicione de Turquesa Caribe (#147D8A) a Azul Petróleo Caribe (#0F4C5C).

### 4.4 Botones

```xml
<Style Selector="Button.primary">
  <Setter Property="Background">
	<Setter.Value>
	  <LinearGradientBrush StartPoint="0%,0%" EndPoint="100%,100%">
		<GradientStop Color="{StaticResource SecondaryColor}" Offset="0" />
		<GradientStop Color="{StaticResource PrimaryColor}" Offset="1" />
	  </LinearGradientBrush>
	</Setter.Value>
  </Setter>
  <Setter Property="Foreground" Value="White" />
  <Setter Property="CornerRadius" Value="8" />
  <Setter Property="Padding" Value="16,10" />
  <Setter Property="FontWeight" Value="SemiBold" />
  <Setter Property="BorderThickness" Value="0" />
</Style>

<Style Selector="Button.primary:pointerover">
  <Setter Property="Opacity" Value="0.9" />
  <Setter Property="Cursor" Value="Hand" />
</Style>

<Style Selector="Button.secondary">
  <Setter Property="Background" Value="Transparent" />
  <Setter Property="Foreground" Value="{StaticResource SecondaryBrush}" />
  <Setter Property="BorderBrush" Value="{StaticResource SecondaryBrush}" />
  <Setter Property="BorderThickness" Value="1.5" />
  <Setter Property="CornerRadius" Value="8" />
  <Setter Property="Padding" Value="16,10" />
  <Setter Property="FontWeight" Value="SemiBold" />
</Style>

<Style Selector="Button.accent">
  <Setter Property="Background" Value="{StaticResource AccentBrush}" />
  <Setter Property="Foreground" Value="White" />
  <Setter Property="CornerRadius" Value="8" />
  <Setter Property="Padding" Value="16,10" />
  <Setter Property="FontWeight" Value="SemiBold" />
  <Setter Property="BorderThickness" Value="0" />
</Style>
```

### 4.5 Inputs (TextBox)

```xml
<Style Selector="TextBox.form-input">
  <Setter Property="Background" Value="{StaticResource SurfaceBrush}" />
  <Setter Property="BorderBrush" Value="{StaticResource BorderBrush}" />
  <Setter Property="BorderThickness" Value="1" />
  <Setter Property="CornerRadius" Value="8" />
  <Setter Property="Padding" Value="12,10" />
  <Setter Property="Foreground" Value="{StaticResource TextPrimaryBrush}" />
  <Setter Property="FontSize" Value="14" />
</Style>

<Style Selector="TextBox.form-input:focus">
  <Setter Property="BorderBrush" Value="{StaticResource SecondaryBrush}" />
  <Setter Property="BorderThickness" Value="1.5" />
</Style>
```

### 4.6 Panel de Navegación

```xml
<Style Selector="Border.navigation-panel">
  <Setter Property="Background" Value="{StaticResource PrimaryBrush}" />
  <Setter Property="Width" Value="220" />
  <Setter Property="Padding" Value="0,20" />
</Style>

<Style Selector="Button.nav-item">
  <Setter Property="Background" Value="Transparent" />
  <Setter Property="Foreground" Value="#FFFFFFB3" />
  <Setter Property="HorizontalAlignment" Value="Stretch" />
  <Setter Property="HorizontalContentAlignment" Value="Left" />
  <Setter Property="Padding" Value="20,12" />
  <Setter Property="CornerRadius" Value="0" />
  <Setter Property="BorderThickness" Value="0" />
</Style>

<Style Selector="Button.nav-item.active">
  <Setter Property="Background" Value="#FFFFFF1A" />
  <Setter Property="Foreground" Value="White" />
  <Setter Property="BorderBrush" Value="{StaticResource AccentBrush}" />
  <Setter Property="BorderThickness" Value="3,0,0,0" />
</Style>
```

---

## 5. Tipografía

| Rol | Familia | Tamaño | Peso | Color |
| ------ | ------ | ------ | ------ | ------ |
| Título de Sección (H1) | Inter / Segoe UI | 22pt – 26pt | Bold | Text Primary |
| Subtítulo (H2) | Inter / Segoe UI | 16pt – 18pt | SemiBold | Text Primary |
| Cuerpo | Inter / Segoe UI | 14pt | Regular | Text Primary |
| Etiqueta / Caption | Inter / Segoe UI | 12pt | Regular | Text Secondary |
| Valor Numérico Grande | Inter / Segoe UI | 24pt – 28pt | Bold | Text Primary |

---

## 6. Navegación y Transiciones

Avalonia soporta animaciones nativas. Las transiciones entre vistas dentro del `ContentControl` deben implementarse con `PageSlide` o `CrossFade`:

```xml
<ContentControl Content="{Binding CurrentView}">
  <ContentControl.ContentTransition>
	<CrossFade Duration="0:0:0.2" />
  </ContentControl.ContentTransition>
</ContentControl>
```

---

## 7. Estructura de Proyecto Recomendada

```text
Project.App.Avalonia/
├── App.axaml                    # Recursos globales y estilos
├── App.axaml.cs
├── Program.cs
├── Views/
│   ├── MainWindow.axaml         # Layout principal (Dashboard)
│   ├── MainWindow.axaml.cs
│   ├── Controls/                # UserControls reutilizables
│   │   ├── CardControl.axaml
│   │   └── ScoreGauge.axaml
│   └── Pages/                   # Páginas/Vistas de contenido
│       ├── DashboardView.axaml
│       └── LoansView.axaml
├── ViewModels/
│   ├── MainWindowViewModel.cs
│   ├── DashboardViewModel.cs
│   └── LoansViewModel.cs
├── Styles/
│   ├── Colors.axaml             # Recursos de colores
│   ├── Controls.axaml           # Estilos de controles
│   └── Typography.axaml         # Estilos tipográficos
├── Converters/
│   └── BoolToVisibilityConverter.cs
└── Assets/
	├── Fonts/
	└── Icons/
```

---

## 8. Reglas de Implementación

1. **No usar code-behind para lógica de negocio**: El `.axaml.cs` solo debe contener inicialización del componente y navegación mínima que no pueda resolverse con bindings.
2. **Preferir CompiledBindings**: Usar `x:DataType` para habilitar compiled bindings y detectar errores en compilación.
3. **Separar estilos**: Los estilos deben estar en archivos `.axaml` separados dentro de la carpeta `Styles/`, no inline.
4. **Usar clases CSS-like**: Avalonia soporta `Classes` en controles. Usar clases descriptivas (`.card`, `.primary`, `.nav-item`).
5. **Responsive Design**: Usar `Grid` con `ColumnDefinitions` y `RowDefinitions` proporcionales. Evitar tamaños fijos excepto para el panel de navegación.
6. **Accesibilidad**: Todo control interactivo debe tener `AutomationProperties.Name` definido.
7. **Inyección de dependencias**: Los ViewModels se resuelven mediante DI y se asignan como `DataContext` en el constructor del View o mediante un `ViewLocator`.
