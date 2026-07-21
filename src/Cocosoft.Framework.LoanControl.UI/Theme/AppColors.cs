namespace Cocosoft.Framework.LoanControl.UI.Theme;

public static class AppColors
{
    // Paleta Institucional
    public static readonly Color Primary = ColorTranslator.FromHtml("#0F4C5C");
    public static readonly Color Secondary = ColorTranslator.FromHtml("#147D8A");
    public static readonly Color Accent = ColorTranslator.FromHtml("#E07A5F");
    public static readonly Color Background = ColorTranslator.FromHtml("#F7F3EE");
    public static readonly Color Surface = ColorTranslator.FromHtml("#FFFFFF");
    public static readonly Color TextPrimary = ColorTranslator.FromHtml("#5A3E36");
    public static readonly Color TextSecondary = ColorTranslator.FromHtml("#6B7280");
    public static readonly Color Border = ColorTranslator.FromHtml("#D6C9B8");

    // Colores Semánticos
    public static readonly Color Success = ColorTranslator.FromHtml("#2E8B57");
    public static readonly Color Warning = ColorTranslator.FromHtml("#D97706");
    public static readonly Color Error = ColorTranslator.FromHtml("#C2410C");
    public static readonly Color Information = ColorTranslator.FromHtml("#2563EB");

    // Variantes de Primary (hover/pressed)
    public static readonly Color PrimaryLight = ControlPaint.Light(Primary, 0.1f);
    public static readonly Color PrimaryDark = ControlPaint.Dark(Primary, 0.1f);
}
