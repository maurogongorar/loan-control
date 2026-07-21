# Skill: Diseño de UI y Arquitectura Base en Windows Forms (MVVM Framework)

## 1. Propósito y Alcance

Garantizar la consistencia visual, la reutilización de componentes y la separación estricta de responsabilidades en la interfaz de usuario (UI). Este documento define las reglas obligatorias de diseño visual, la identidad gráfica y la jerarquía de herencia técnica que debe seguir todo desarrollador o diseñador al crear pantallas, diálogos y componentes interactivos dentro del ecosistema del framework.

---

## 2. Arquitectura de Herencia (Jerarquía de Formularios)

Para evitar que la lógica de infraestructura se mezcle con el diseño visual, el framework implementa una jerarquía de herencia en dos niveles independientes:

```text
            [ System.Windows.Forms.Form ]
                        ▲
                        │
                [ MvvmForm ]
        (Capa de Infraestructura MVVM)
                        ▲
        ┌───────────────┴───────────────┐
        │                               │
[ BaseLayoutForm ]              [ BaseModalForm ]
(Layout Principal)             (Ventanas Modales)
```

### 2.1 MvvmForm (Clase Base de Infraestructura)

**Responsabilidad**

Gestionar exclusivamente el ciclo de vida MVVM de la ventana.

**Funciones**

* Inyectar o enlazar el `DataContext` (ViewModel).
* Registrar suscripciones al sistema de mensajería (`Messenger` / `EventAggregator`).
* Liberar correctamente bindings y recursos durante `Dispose()`.

**Restricción**

Queda estrictamente prohibido agregar:

* Controles visuales.
* Paneles.
* Layouts.
* Colores.
* Estilos visuales.

Toda responsabilidad estética pertenece exclusivamente a las clases derivadas.

---

### 2.2 BaseLayoutForm (Clase Base de Diseño Principal)

**Responsabilidad**

Proveer el layout maestro de la aplicación.

**Herencia**

Hereda directamente de `MvvmForm`.

**Estructura estándar**

* `pnlNavigation`

  * Menú lateral fijo.
* `pnlHeader`

  * Barra superior.
* `pnlContent`

  * Contenedor principal.
  * `Dock = DockStyle.Fill`

---

### 2.3 BaseModalForm (Clase Base de Diálogos)

**Responsabilidad**

Proveer una apariencia limpia para ventanas emergentes.

**Herencia**

Hereda directamente de `MvvmForm`.

**Configuración obligatoria**

```csharp
MinimizeBox = false;
MaximizeBox = false;
FormBorderStyle = FormBorderStyle.FixedDialog;
StartPosition = FormStartPosition.CenterParent;
```

---

# 3. Identidad Visual (Design System)

La identidad visual está inspirada en la costa Caribe colombiana.

El objetivo es transmitir simultáneamente:

* Confianza.
* Profesionalismo.
* Cercanía.
* Calidez.
* Estabilidad financiera.
* Alegría y energía de la región Caribe sin caer en una apariencia turística.

La interfaz debe diferenciarse de las aplicaciones bancarias tradicionales evitando el uso predominante del clásico azul corporativo, pero conservando una percepción sólida y profesional.

La inspiración proviene del mar Caribe, la arena, la arquitectura colonial y los atardeceres de la región, manteniendo una estética moderna, minimalista y elegante.

---

## 3.1 Paleta Institucional

| Token          | Color                | Hex       | Uso                                                                                                            |
| -------------- | -------------------- | --------- | -------------------------------------------------------------------------------------------------------------- |
| Primary        | Azul Petróleo Caribe | `#0F4C5C` | Color institucional principal. Navegación, encabezados, botones principales, elementos destacados.             |
| Secondary      | Turquesa Caribe      | `#147D8A` | Componentes interactivos, enlaces, iconografía y acciones secundarias.                                         |
| Accent         | Coral Suave          | `#E07A5F` | Call To Action (CTA), indicadores importantes y pequeños elementos de énfasis. Debe utilizarse con moderación. |
| Background     | Arena Clara          | `#F7F3EE` | Fondo general de la aplicación.                                                                                |
| Surface        | Blanco               | `#FFFFFF` | Tarjetas, paneles, formularios y contenedores.                                                                 |
| Text Primary   | Cacao                | `#5A3E36` | Texto principal. Sustituye el negro puro para aportar mayor calidez.                                           |
| Text Secondary | Gris Neutro          | `#6B7280` | Texto descriptivo, ayudas y etiquetas secundarias.                                                             |
| Border         | Arena Oscura         | `#D6C9B8` | Bordes, separadores y líneas divisorias.                                                                       |

---

## 3.2 Colores Semánticos

Los estados del sistema deben utilizar colores universalmente reconocidos y no deben mezclarse con los colores institucionales.

| Estado      | Color | Hex       |
| ----------- | ----- | --------- |
| Success     | Verde | `#2E8B57` |
| Warning     | Ámbar | `#D97706` |
| Error       | Rojo  | `#C2410C` |
| Information | Azul  | `#2563EB` |

Estos colores únicamente representan estados del sistema.

Nunca deben reemplazar los colores institucionales.

---

## 3.3 Principios de Uso

### Dominancia del color

* El Azul Petróleo representa la identidad de la aplicación.
* Debe ser el color dominante de toda la interfaz.

### Color secundario

El Turquesa funciona como apoyo visual.

Nunca debe reemplazar completamente al color Primary.

### Color de acento

El Coral únicamente debe utilizarse para:

* Botones CTA.
* Notificaciones importantes.
* Badges.
* Indicadores destacados.

No debe utilizarse como fondo principal de pantallas.

### Fondos

El fondo general debe utilizar Arena Clara.

Las tarjetas y paneles siempre deben permanecer blancos.

### Texto

Evitar el uso de negro puro (`#000000`).

Todo texto principal utilizará el color Cacao.

### Saturación

Evitar colores excesivamente brillantes que hagan parecer la aplicación una plataforma turística o de entretenimiento.

---

## 3.4 Sensación Visual Esperada

Cada pantalla debe transmitir:

* Profesionalismo.
* Confianza.
* Cercanía.
* Modernidad.
* Elegancia.
* Simplicidad.
* Calidez.

La inspiración caribeña debe percibirse únicamente mediante la paleta de colores y la atmósfera visual.

Nunca mediante elementos gráficos relacionados con playas, palmeras, olas, cocos u otros recursos decorativos.

---

## 3.5 Iconografía

Toda la iconografía debe cumplir:

* Estilo Outline.
* Grosor uniforme.
* Esquinas ligeramente redondeadas.
* Diseño minimalista.
* Uso preferente de Primary o Secondary.
* Evitar iconos caricaturescos.

---

## 3.6 Bordes

Valores estándar:

* Inputs: `6px`
* Botones: `8px`
* Cards: `12px`
* Modales: `12px`

No utilizar bordes completamente cuadrados salvo en tablas.

---

## 3.7 Sombras

Las sombras deben ser extremadamente sutiles.

Referencia:

```text
Offset Y : 4px
Blur     : 12px
Opacity  : 8% - 12%
```

Evitar:

* Sombras fuertes.
* Neumorphism.
* Glow.
* Sombras múltiples.

---

# 4. Patrones de Navegación y Estilos de Ventana

## 4.1 Ventanas Contenidas (UserControls Dinámicos)

Toda pantalla de negocio debe implementarse como un `UserControl` orientado a MVVM.

Se cargará dinámicamente dentro de `pnlContent`.

Antes de cargar una nueva vista:

```csharp
pnlContent.Controls.Clear();
activeUserControl?.Dispose();
```

---

## 4.2 Ventanas Emergentes (Modals)

Los formularios derivados de `BaseModalForm` deben abrirse exclusivamente mediante:

```csharp
.ShowDialog();
```

Esto garantiza un flujo de interacción enfocado.

---

## 4.3 Side Drawers

Los paneles laterales deben:

* utilizar `Dock.Left` o `Dock.Right`;
* animarse mediante un `Timer`;
* modificar progresivamente la propiedad `Width`;
* evitar cambios bruscos de tamaño.

---

# 5. Estilos Estrictos de Componentes

## 5.1 Botones (`Button`)

### Acción Primaria

* Fondo: Primary (`#0F4C5C`)
* Texto blanco
* Sin borde
* `FlatStyle = Flat`
* `BorderSize = 0`

### Hover

Utilizar una variante aproximadamente un 10% más clara del color Primary.

### Pressed

Utilizar una variante aproximadamente un 10% más oscura del color Primary.

### Acción Secundaria

* Fondo blanco
* Borde Secondary
* Texto Primary

### CTA Especial

Acciones como:

* Solicitar préstamo
* Realizar pago
* Confirmar desembolso

pueden utilizar Accent (`#E07A5F`) de forma puntual.

---

## 5.2 TextBox

* Fondo blanco.
* Borde de 1px.
* Placeholder mediante eventos `Enter` y `Leave`.
* Altura entre 28 y 32 px.
* Fuente recomendada:

```text
Segoe UI
10 pt
```

---

## 5.3 DataGridView

### Estilo

* `BorderStyle = None`
* Filas alternadas.
* Cabecera oscura.
* Texto blanco.
* Fuente Bold.
* `EnableHeadersVisualStyles = false`

---

# 6. Layout, Espaciado y Grid

## Padding

Todo contenedor debe mantener un padding uniforme de:

* `16px`
* `20px`

Nunca deben existir controles pegados a los bordes.

---

## Botones

En formularios:

* esquina inferior derecha;
* botón principal siempre a la derecha;
* botón secundario inmediatamente antes.

---

## Responsive

Todo control debe definir correctamente:

* `Anchor`
* `Dock`

para mantener una interfaz armónica durante el redimensionamiento.

---

# 7. Principios para Generación de Interfaces

Toda pantalla generada para este framework debe cumplir las siguientes reglas:

* Priorizar simplicidad sobre cantidad de elementos.
* Mantener abundante espacio en blanco.
* No utilizar más de un color de acento por pantalla.
* Evitar más de tres niveles visuales de jerarquía.
* Las acciones principales deben identificarse inmediatamente.
* La información financiera debe ser fácilmente escaneable.
* Priorizar tarjetas limpias sobre tablas cuando sea posible.
* Utilizar tipografía consistente y con buena legibilidad.
* Mantener una experiencia uniforme entre todas las pantallas.
* Evitar gradientes llamativos, texturas o fondos decorativos.
* La inspiración en la costa Caribe debe percibirse por la combinación de colores, la calidez de la interfaz y el equilibrio visual, nunca mediante ilustraciones turísticas.
* Toda nueva pantalla debe sentirse parte de un único sistema de diseño, independientemente del desarrollador que la implemente.