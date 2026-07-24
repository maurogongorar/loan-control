using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace Cocosoft.Finance.LoanControl.App.Avalonia.Views.Charts;

/// <summary>
/// A custom control that renders a semicircular gauge chart displaying
/// a percentage value with configurable track and value brushes.
/// </summary>
/// <seealso cref="Avalonia.Controls.Control" />
public class GaugeChart : Control
{
    /// <summary>
    /// Defines the <see cref="Percentage"/> styled property.
    /// </summary>
    public static readonly StyledProperty<double> PercentageProperty =
        AvaloniaProperty.Register<GaugeChart, double>(nameof(Percentage), 50.0);

    /// <summary>
    /// Defines the <see cref="StrokeWidth"/> styled property.
    /// </summary>
    public static readonly StyledProperty<double> StrokeWidthProperty =
        AvaloniaProperty.Register<GaugeChart, double>(nameof(StrokeWidth), 16.0);

    /// <summary>
    /// Defines the <see cref="TrackBrush"/> styled property.
    /// </summary>
    public static readonly StyledProperty<IBrush?> TrackBrushProperty =
        AvaloniaProperty.Register<GaugeChart, IBrush?>(nameof(TrackBrush), Brushes.LightGray);

    /// <summary>
    /// Defines the <see cref="ValueBrush"/> styled property.
    /// </summary>
    public static readonly StyledProperty<IBrush?> ValueBrushProperty =
        AvaloniaProperty.Register<GaugeChart, IBrush?>(nameof(ValueBrush), Brushes.DodgerBlue);

    /// <summary>
    /// Gets or sets the percentage value displayed by the gauge (0–100).
    /// </summary>
    public double Percentage
    {
        get => this.GetValue(PercentageProperty);
        set => this.SetValue(PercentageProperty, value);
    }

    /// <summary>
    /// Gets or sets the stroke width of the gauge arc.
    /// </summary>
    public double StrokeWidth
    {
        get => this.GetValue(StrokeWidthProperty);
        set => this.SetValue(StrokeWidthProperty, value);
    }

    /// <summary>
    /// Gets or sets the brush used to paint the background track arc.
    /// </summary>
    public IBrush? TrackBrush
    {
        get => this.GetValue(TrackBrushProperty);
        set => this.SetValue(TrackBrushProperty, value);
    }

    /// <summary>
    /// Gets or sets the brush used to paint the value arc.
    /// </summary>
    public IBrush? ValueBrush
    {
        get => this.GetValue(ValueBrushProperty);
        set => this.SetValue(ValueBrushProperty, value);
    }

    static GaugeChart()
    {
        AffectsRender<GaugeChart>(PercentageProperty, TrackBrushProperty, ValueBrushProperty, StrokeWidthProperty);
    }

    /// <inheritdoc />
    public override void Render(DrawingContext context)
    {
        base.Render(context);

        var width = this.Bounds.Width;
        var height = this.Bounds.Height;

        if (width <= 0 || height <= 0)
        {
            return;
        }

        var stroke = this.StrokeWidth;
        var radius = Math.Min(width / 2, height) - stroke / 2 - 2;

        if (radius <= 0)
        {
            return;
        }

        var center = new Point(width / 2, height - 2);

        // Draw track (full semicircle)
        DrawArc(context, center, radius, 180.0, 180.0, this.TrackBrush, stroke);

        // Draw value arc
        var pct = Math.Clamp(this.Percentage, 0, 100);
        var sweepAngle = pct / 100.0 * 180.0;

        if (sweepAngle > 0.5)
        {
            DrawArc(context, center, radius, sweepAngle, 180.0, this.ValueBrush, stroke);
        }
    }

    private static void DrawArc(
        DrawingContext context,
        Point center,
        double radius,
        double sweepDegrees,
        double totalDegrees,
        IBrush? brush,
        double strokeWidth)
    {
        if (brush == null)
        {
            return;
        }

        var startAngle = 180.0; // left side
        var endAngle = startAngle + sweepDegrees;

        var startRad = startAngle * Math.PI / 180.0;
        var endRad = endAngle * Math.PI / 180.0;

        var startPoint = new Point(center.X + radius * Math.Cos(startRad), center.Y + radius * Math.Sin(startRad));
        var endPoint = new Point(center.X + radius * Math.Cos(endRad), center.Y + radius * Math.Sin(endRad));

        var figure = new PathFigure { StartPoint = startPoint, IsClosed = false, IsFilled = false };
        figure.Segments!.Add(new ArcSegment
        {
            Point = endPoint,
            Size = new Size(radius, radius),
            IsLargeArc = sweepDegrees > 180,
            SweepDirection = SweepDirection.Clockwise
        });

        var geometry = new PathGeometry();
        geometry.Figures!.Add(figure);

        var pen = new Pen(brush, strokeWidth, lineCap: PenLineCap.Round);
        context.DrawGeometry(null, pen, geometry);
    }
}
