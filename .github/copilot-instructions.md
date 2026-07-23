# Copilot Instructions

## Carga de Skills por Contexto

Al inicio de cada sesión o cuando recibas una solicitud, **debes**:

1. Revisar los skills disponibles en `.agents/skills/` y leer su contenido.
2. Identificar cuáles skills aplican según la tarea solicitada.
3. Cargar y aplicar esos skills como reglas obligatorias durante toda la ejecución.

### Mapeo de skills por tipo de tarea

| Tipo de tarea                                      | Skills a cargar                                  |
|----------------------------------------------------|--------------------------------------------------|
| Crear o modificar ventanas/vistas Avalonia          | `ui-avalonia-engineer`, `coding-standards`       |
| Crear o modificar ventanas/vistas Windows Forms     | `ui-engineer`, `coding-standards`                |
| Escribir o modificar código C# (cualquier contexto) | `coding-standards`                               |

> Si una tarea involucra múltiples áreas (por ejemplo, crear una vista Avalonia con lógica de negocio), se deben cargar **todos** los skills relevantes de forma combinada.

### Reglas

- **Nunca** generes o modifiques código sin haber revisado primero los skills aplicables.
- Si no existe un skill para el contexto solicitado, aplica las mejores prácticas estándar de .NET 10 y C#.
- A medida que se agreguen nuevos skills, este mapeo debe extenderse automáticamente.