# Skill: Diseño de UI y Arquitectura Base en Windows Forms (MVVM Framework - .NET 10)

## 1. Propósito y Alcance

Garantizar la consistencia visual, la reutilización de componentes y la separación estricta de responsabilidades en la interfaz de usuario (UI). Este documento define las reglas obligatorias de diseño visual, la identidad gráfica, el manejo de texturas y relieves, y la jerarquía de herencia técnica que debe seguir todo desarrollador o IA al crear pantallas, diálogos y componentes interactivos dentro del ecosistema del framework MVVM sobre .NET 10.

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

**Responsabilidad**:

Gestionar exclusivamente el ciclo de vida MVVM de la ventana.

**Funciones**:

- Inyectar o enlazar el `DataContext` (ViewModel) mediante el sistema de Inyección de Dependencias de .NET 10 (Microsoft.Extensions.DependencyInjection).
- Configurar el motor de enlazado nativo (BindingSource y DataBindings).
- Registrar suscripciones al sistema de mensajería (`Messenger` / `EventAggregator`).
- Liberar correctamente bindings y recursos durante el método Dispose().

**Restricción**:

Queda estrictamente prohibido agregar controles visuales, paneles, layouts, colores o estilos visuales en esta clase.
Toda responsabilidad estética pertenece exclusivamente a las clases derivadas.

---

### 2.2 BaseLayoutForm (Clase Base de Diseño Principal)

**Responsabilidad**:

Proveer el layout maestro de la aplicación estilo Dashboard Moderno.

**Estructura estándar**:

- `pnlNavigation` (Izquierda): Menú lateral fijo con íconos vectoriales minimalistas y selector activo destacado.
- `pnlHeader` (Arriba): Barra superior para el Título de la sección activa (Ej. "Saldos y deudas") y acciones globales de la vista (Ej. Botón "Exportar").
- `pnlContent`  (Centro): Contenedor principal con Dock = DockStyle.Fill y Padding uniforme. Aquí se inyectarán y destruirán los UserControls de negocio dinámicamente.

---

### 2.3 BaseModalForm (Clase Base de Diálogos)

**Responsabilidad**:

Proveer una apariencia limpia, enmarcada y enfocada para ventanas emergentes.

**Configuración obligatoria**:

```csharp
MinimizeBox = false;
MaximizeBox = false;
FormBorderStyle = FormBorderStyle.None;
StartPosition = FormStartPosition.CenterParent;
```

---

## 3. Identidad Visual (Design System) y Profundidad

La identidad visual está inspirada en la costa Caribe colombiana, transmitiendo confianza, profesionalismo y estabilidad financiera mediante una estética moderna y limpia.

**Regla de Textura y Profundidad**: Para alejarse del aspecto plano (flat) tradicional o desactualizado de WinForms, la interfaz debe adoptar un estilo Soft-UI / Glassmorphism sutil. Los controles deben poseer relieve mediante sutiles gradientes de fondo, sombras perimetrales difuminadas (DropShadow) y bordes redondeados (Radius), emulando capas físicas superpuestas.

---

### 3.1 Paleta Institucional

| Token | Color | Hex | Uso |
| ------ | ------ | ------ | ------ |
| Primary | Azul Petróleo Caribe | #0F4C5C | Color institucional principal. Navegación, indicadores de gráficos, encabezados destacados. |
| Secondary | Turquesa Caribe | #147D8A | Componentes interactivos, enlaces, iconografía de tarjetas secundarias y barras de progreso. |
| Accent | Coral Suave | #E07A5F | Call To Action (CTA) especiales, indicadores importantes y pequeños elementos de énfasis (Uso moderado). |
| Background | Arena Clara | #F7F3EE | Fondo general de la aplicación (pnlContent y formularios base). |
| Surface | Blanco | #FFFFFF | Tarjetas (Cards), paneles flotantes y contenedores de formularios. Deben poseer sombra difuminada. |
| Text Primary | Cacao | #5A3E36 | Texto principal, cifras numéricas grandes y títulos. Sustituye al negro puro para aportar calidez. |
| Text Secondary | Gris Neutro | #6B7280 | Texto descriptivo, ayudas visuales y etiquetas secundarias (Ej. "Duración", "Interés"). |
| Border | Arena Oscura | #D6C9B8 | Bordes sutiles para inputs, separadores y líneas divisorias. |## 3.2 Colores Semánticos| Estado | Color | Hex | Uso en Sistema |

### 3.2 Colores semánticos

Los estados del sistema deben utilizar colores universalmente reconocidos y no deben mezclarse con los colores institucionales.

| Token | Color | Hex |
| ------ | ------ | ------ |
| Success | Verde | #2E8B57 |
| Warning | Ámbar | #F59E0B |
| Error | Rojo | #E11D48 |
| Information | Azul | #3B82F6 |

Estos colores únicamente representan estados del sistema.

Nunca deben reemplazar los colores institucionales.

---

## 4. Diseño de Componentes con Textura (GDI+)

Para asegurar que la aplicación .NET 10 WinForms se comporte y visualice como una interfaz moderna, las vistas y los controles personalizados deben interceptar el evento OnPaint utilizando System.Drawing.Drawing2D.

### 4.1 Tarjetas (Cards / Panels)

Todo contenedor de información (Ej. Tarjeta de Saldos, Tarjeta de Deudas) debe renderizarse siguiendo este estándar:

- Fondo: Blanco (#FFFFFF).
- Bordes: Redondeados con un radio de 12px a 16px utilizando `GraphicsPath`.
- Sombra (Drop Shadow): Renderizar una sombra sutil perimetral (Offset Y: 4px, Blur: 12px, Color: #5A3E36 o Negro con una opacidad del 5% al 8%).
- Estructura Interna:
  - Ícono en la esquina superior izquierda dentro de un contenedor circular/cuadrado redondeado con fondo semitransparente (Alfa ~30) del color del estado (Azul para saldos, Rojo para deudas).
  - Texto descriptivo en color Text Secondary.
  - Valor numérico principal destacado en tamaño grande (24pt a 28pt) en color Text Primary.

### 4.2 Indicadores Gráficos (Gauges y Barras de Score)

Las visualizaciones de datos (como el medidor semicircular de Score de Crédito) deben ser dinámicas:

- Dibujadas mediante `Graphics.DrawArc` con `LineCap.Round` para asegurar terminaciones suaves.
- **Canal base**: Color Arena Oscura muy tenue (#EADFCF).
- **Progreso**: Renderizado con un LinearGradientBrush que transicione suavemente de Turquesa Caribe (#147D8A) a Azul Petróleo Caribe (#0F4C5C).

### 4.3 Botones (`Button`)

Los botones deben poseer texturas visuales diferenciadas según su estado de interacción:

- **Acción Primaria**:
  - Fondo: Gradiente sutil (`LinearGradientBrush`) desde Turquesa Caribe (#147D8A) hasta Azul Petróleo Caribe (#0F4C5C).
  - Bordes: Redondeados de 8px.
  - Texto: Blanco, centrado, fuente semibold.
  - Relieve: Sombra inferior sutil de 2px a 4px.
  - Hover/Pressed: Variación de luminosidad del gradiente en +/- 10% respectivamente.
- **Acción Secundaria (Ej. Botón "Exportar")**:
  - Fondo: Blanco brillante con relieve sutil.
  - Borde: 1px sólido en color Arena Oscura (#D6C9B8).
  - Ícono y Texto: Color Cacao (#5A3E36) o Azul Petróleo Caribe.
- **CTA Especial (Acciones Críticas)**:
  - Fondo: Gradiente basado en Coral Suave (#E07A5F).

#### 4.3.1 Estado Deshabilitado (`Enabled = false`)
- **Regla Estricta**: Debe ser evidente a primera vista que el botón **no está disponible**.
- **Fondo**: Cancelar cualquier gradiente o color institucional. Se debe pintar un fondo plano y mate en color Gris Claro Neutro (#E5E7EB o #D1D5DB).
- **Texto e Íconos**: Color Gris Neutro Apagado (#9CA3AF), perdiendo todo contraste llamativo.
- **Relieve**: Eliminar por completo la sombra perimetral (**DropShadow**) y efectos de relieve, forzando un aspecto totalmente plano y "hundido" en la superficie.
- **Interacción**: Ignorar por completo los eventos Hover y Pressed. El cursor debe mantenerse por defecto (`Cursors.Default`), impidiendo el cambio a mano interactiva (`Cursors.Hand`).

### 4.4 Inputs y Controles de Texto (`TextBox`)

- **Estilo Visual**: Altura de 36px con Padding interior para evitar textos pegados al borde.
- **Borde**: 1px sólido en color Arena Oscura (#D6C9B8). Al ganar el foco (Enter), el borde cambia a Turquesa Caribe (#147D8A) con un sutil efecto de resplandor exterior ( Glow ).
- Fuente: Segoe UI, 10pt.

---

## 5. Patrones de Layout, Espaciado y Grid

Toda pantalla generada por Copilot debe acatar estrictamente este modelo de distribución responsive:

- **Márgenes y Padding**: El fondo del **BaseLayoutForm** siempre será Arena Clara (#F7F3EE). Las tarjetas deben mantener un espaciado de separación ( Gutter ) constante de 20px a 24px.
- **Dimensionamiento Dinámico**: Utilizar `TableLayoutPanel` o cálculos manuales en el evento OnResize del formulario para recalcular el tamaño de las tarjetas de forma proporcional, evitando el solapamiento de componentes.

---

## 6. Textos en las vistas

Los textos en la aplicacion se deben manejar mediante recursos pues la aplicacion se debe construir para soportar diversos idiomas.

---

## 7. Reglas para el Agente en la Creación de Vistas

Al solicitar a la IA la creación o modificación de una vista (`UserControl` o `Form`):

1. **NO usar controles planos estándar de WinForms**: La IA tiene la obligación de extender los componentes o utilizar los eventos Paint e incorporar `GraphicsPath` para aplicar bordes redondeados, texturas de gradiente y sombras realistas.

2. **Implementar el Estado Deshabilitado Explícito**: Al sobreescribir o diseñar componentes interactivos customizados, validar siempre la propiedad `Enabled` en el método de dibujo para aplicar la estética opaca, plana e inactiva descrita en la sección 4.3.1.

3. **Mapeo de Datos y Formato**: Seguir la arquitectura base conectando los bindings a través de `OnBindViewModel`. Los formatos de moneda, porcentajes y plazos se aplican en la capa de Vista para mantener el ViewModel limpio con tipos primitivos de datos.