---
name: ui-engineer
description: Usa este skill para crear o modificar las vistas del proyecto `scr/Cocosoft.Finance.LoanControl.App`
---

# Diseño de UI y Arquitectura Base en Windows Forms (MVVM Framework - .NET 10)

## Propósito y Alcance

Garantizar la consistencia visual, la reutilización de componentes y la separación estricta de responsabilidades en la interfaz de usuario (UI). Este documento define las reglas obligatorias de diseño visual, la identidad gráfica, el manejo de texturas y relieves, y la jerarquía de herencia técnica que debe seguir todo desarrollador o IA al crear pantallas, diálogos y componentes interactivos dentro del ecosistema del framework MVVM sobre .NET 10.

---

## Arquitectura de Herencia (Jerarquía de Formularios)

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

### MvvmForm (Clase Base de Infraestructura)

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

### BaseLayoutForm (Clase Base de Diseño Principal)

**Responsabilidad**:

Proveer el layout maestro de la aplicación estilo Dashboard Moderno.

**Estructura estándar**:

- `pnlNavigation` (Izquierda): Menú lateral fijo con íconos vectoriales minimalistas y selector activo destacado.
- `pnlHeader` (Arriba): Barra superior para el Título de la sección activa (Ej. "Saldos y deudas") y acciones globales de la vista (Ej. Botón "Exportar").
- `pnlContent`  (Centro): Contenedor principal con Dock = DockStyle.Fill y Padding uniforme. Aquí se inyectarán y destruirán los UserControls de negocio dinámicamente.

---

### BaseModalForm (Clase Base de Diálogos)

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

## Identidad Visual (Design System) y Profundidad

La identidad visual transmite confianza, profesionalismo y estabilidad financiera mediante una estética moderna, oscura y limpia inspirada en interfaces financieras contemporáneas.

**Regla de Textura y Profundidad**: Para alejarse del aspecto plano (flat) tradicional o desactualizado de WinForms, la interfaz debe adoptar un estilo Soft-UI / Glassmorphism sutil. Los controles deben poseer relieve mediante sutiles gradientes de fondo, sombras perimetrales difuminadas (DropShadow) y bordes redondeados (Radius), emulando capas físicas superpuestas.

---

### Paleta Institucional

| Token | Color | Hex | Uso |
| ------ | ------ | ------ | ------ |
| Primary | Azul Naval Profundo | #1B2A4A | Color institucional principal. Navegación, header, encabezados destacados. |
| PrimaryDark | Azul Naval Oscuro | #111D35 | Variante oscura para fondos profundos y estados pressed. |
| PrimaryLight | Azul Naval Claro | #2A3F6A | Hover states en navegación y elementos interactivos sobre fondo oscuro. |
| Accent | Azul Brillante | #3B82F6 | Botones activos, indicadores de selección, CTAs principales. |
| AccentHover | Azul Brillante Claro | #60A5FA | Estado hover de elementos accent. |
| SidebarBackground | Azul Noche | #0F1729 | Fondo del panel de navegación lateral (pnlNavigation). |
| HeaderBackground | Azul Naval Profundo | #1B2A4A | Fondo de la barra superior (pnlHeader). |
| ContentBackground | Gris Hielo | #F1F5F9 | Fondo general del área de contenido (pnlContent y formularios base). |
| CardBackground | Blanco | #FFFFFF | Tarjetas (Cards), paneles flotantes y contenedores. Deben poseer sombra sutil. |
| TextPrimary | Azul Tinta | #1E293B | Texto principal, cifras numéricas grandes y títulos. |
| TextSecondary | Gris Pizarra | #64748B | Texto descriptivo, etiquetas secundarias y ayudas visuales. |
| TextOnDark | Gris Claro | #E2E8F0 | Texto principal sobre fondos oscuros (sidebar, header). |
| TextOnDarkMuted | Gris Azulado | #94A3B8 | Texto secundario sobre fondos oscuros. |
| Border | Gris Hielo | #E2E8F0 | Bordes sutiles para inputs, separadores y líneas divisorias. |
| BorderDark | Azul Profundo | #1E3A5F | Bordes sobre fondos oscuros (separadores en sidebar). |

### Colores semánticos

Los estados del sistema deben utilizar colores universalmente reconocidos y no deben mezclarse con los colores institucionales.

| Token | Color | Hex |
| ------ | ------ | ------ |
| Success | Verde | #22C55E |
| Warning | Ámbar | #F59E0B |
| Danger | Rojo | #EF4444 |

Estos colores únicamente representan estados del sistema.

Nunca deben reemplazar los colores institucionales.

---

## Diseño de Componentes con Textura (GDI+)

Para asegurar que la aplicación .NET 10 WinForms se comporte y visualice como una interfaz moderna, las vistas y los controles personalizados deben interceptar el evento OnPaint utilizando System.Drawing.Drawing2D.

### Tarjetas (Cards / Panels)

Todo contenedor de información (Ej. Tarjeta de Saldos, Tarjeta de Deudas) debe renderizarse siguiendo este estándar:

- Fondo: Blanco (#FFFFFF).
- Bordes: Redondeados con un radio de 12px a 16px utilizando `GraphicsPath`.
- Sombra (Drop Shadow): Renderizar una sombra sutil perimetral (Offset Y: 4px, Blur: 12px, Color: #1E293B o Negro con una opacidad del 5% al 8%).
- Estructura Interna:
  - Ícono en la esquina superior izquierda dentro de un contenedor circular/cuadrado redondeado con fondo semitransparente (Alfa ~30) del color del estado (Azul para saldos, Rojo para deudas).
  - Texto descriptivo en color Text Secondary.
  - Valor numérico principal destacado en tamaño grande (24pt a 28pt) en color Text Primary.

### Indicadores Gráficos (Gauges y Barras de Score)

Las visualizaciones de datos (como el medidor semicircular de Score de Crédito) deben ser dinámicas:

- Dibujadas mediante `Graphics.DrawArc` con `LineCap.Round` para asegurar terminaciones suaves.
- **Canal base**: Color Gris Hielo muy tenue (#E2E8F0).
- **Progreso**: Renderizado con un LinearGradientBrush que transicione suavemente de Azul Brillante (#3B82F6) a Azul Naval Profundo (#1B2A4A).

### Botones (`Button`)

Los botones deben poseer texturas visuales diferenciadas según su estado de interacción:

- **Acción Primaria**:
  - Fondo: Gradiente sutil (`LinearGradientBrush`) desde Azul Naval Claro (#2A3F6A) hasta Azul Naval Profundo (#1B2A4A).
  - Bordes: Redondeados de 8px.
  - Texto: Blanco, centrado, fuente semibold.
  - Relieve: Sombra inferior sutil de 2px a 4px.
  - Hover/Pressed: Variación de luminosidad del gradiente en +/- 10% respectivamente.
- **Acción Secundaria (Ej. Botón "Exportar")**:
  - Fondo: Blanco brillante con relieve sutil.
  - Borde: 1px sólido en color Gris Hielo (#E2E8F0).
  - Ícono y Texto: Color Azul Tinta (#1E293B) o Azul Naval Profundo.
- **CTA Especial (Acciones Críticas)**:
  - Fondo: Gradiente basado en Azul Brillante (#3B82F6).

#### Estado Deshabilitado (`Enabled = false`)
- **Regla Estricta**: Debe ser evidente a primera vista que el botón **no está disponible**.
- **Fondo**: Cancelar cualquier gradiente o color institucional. Se debe pintar un fondo plano y mate en color Gris Claro Neutro (#E5E7EB o #D1D5DB).
- **Texto e Íconos**: Color Gris Neutro Apagado (#9CA3AF), perdiendo todo contraste llamativo.
- **Relieve**: Eliminar por completo la sombra perimetral (**DropShadow**) y efectos de relieve, forzando un aspecto totalmente plano y "hundido" en la superficie.
- **Interacción**: Ignorar por completo los eventos Hover y Pressed. El cursor debe mantenerse por defecto (`Cursors.Default`), impidiendo el cambio a mano interactiva (`Cursors.Hand`).

### Inputs y Controles de Texto (`TextBox`)

- **Estilo Visual**: Altura de 36px con Padding interior para evitar textos pegados al borde.
- **Borde**: 1px sólido en color Gris Hielo (#E2E8F0). Al ganar el foco (Enter), el borde cambia a Azul Brillante (#3B82F6) con un sutil efecto de resplandor exterior ( Glow ).
- Fuente: Segoe UI, 10pt.

---

## Patrones de Layout, Espaciado y Grid

Toda pantalla generada por Copilot debe acatar estrictamente este modelo de distribución responsive:

- **Márgenes y Padding**: El fondo del **BaseLayoutForm** siempre será Gris Hielo (#F1F5F9). Las tarjetas deben mantener un espaciado de separación ( Gutter ) constante de 20px a 24px.
- **Dimensionamiento Dinámico**: Utilizar `TableLayoutPanel` o cálculos manuales en el evento OnResize del formulario para recalcular el tamaño de las tarjetas de forma proporcional, evitando el solapamiento de componentes.

---

## Textos en las vistas

Los textos en la aplicacion se deben manejar mediante recursos pues la aplicacion se debe construir para soportar diversos idiomas.

---

## Reglas para el Agente en la Creación de Vistas

Al solicitar a la IA la creación o modificación de una vista (`UserControl` o `Form`):

1. **NO usar controles planos estándar de WinForms**: La IA tiene la obligación de extender los componentes o utilizar los eventos Paint e incorporar `GraphicsPath` para aplicar bordes redondeados, texturas de gradiente y sombras realistas.

2. **Implementar el Estado Deshabilitado Explícito**: Al sobreescribir o diseñar componentes interactivos customizados, validar siempre la propiedad `Enabled` en el método de dibujo para aplicar la estética opaca, plana e inactiva descrita en la sección 4.3.1.

3. **Mapeo de Datos y Formato**: Seguir la arquitectura base conectando los bindings a través de `OnBindViewModel`. Los formatos de moneda, porcentajes y plazos se aplican en la capa de Vista para mantener el ViewModel limpio con tipos primitivos de datos.