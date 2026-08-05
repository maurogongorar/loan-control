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
- Siempre antes de generar o modificar código C#, leer los skills en `.agents/skills/` (especialmente `coding-standards/SKILL.md` y `ui-avalonia-engineer/SKILL.md` según aplique) y seguir sus reglas obligatoriamente.
- Si no existe un skill para el contexto solicitado, aplica las mejores prácticas estándar de .NET 10 y C#.
- A medida que se agreguen nuevos skills, este mapeo debe extenderse automáticamente.
- Los métodos en clases e interfaces **deben** estar organizados alfabéticamente.

## Paleta de Colores Preferida

- Utiliza la paleta de colores moderna en tonos azul marino oscuro:
  - Color primario: `#1B2A4A`
  - Color de acento: `#3B82F6`
  - Fondo de contenido: `#F1F5F9`
- Evita la paleta cálida/terrosa, ya que se considera que los tonos cálidos se sienten "muertos".

## Comentarios y Documentación

- Todos los comentarios en el código **deben** estar en inglés.
- Las cadenas de interfaz de usuario (etiquetas, mensajes mostrados al usuario) **deben** permanecer en español.
- La documentación XML en los miembros públicos **es obligatoria** según los estándares de codificación.