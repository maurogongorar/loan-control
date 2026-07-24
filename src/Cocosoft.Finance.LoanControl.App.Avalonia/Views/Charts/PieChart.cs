using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace Cocosoft.Finance.LoanControl.App.Avalonia.Views.Charts;

/// <summary>
/// A custom control that renders a two-slice pie chart with configurable
/// angle and brush properties for each slice.
/// </summary>
/// <seealso cref="Avalonia.Controls.Control" />
public class PieChart : Control
{
    /// <summary>
    /// Defines the <see cref="Slice1Angle"/> styled property.
    /// </summary>
    public static readonly StyledProperty<double> Slice1AngleProperty =
        AvaloniaProperty.Register<PieChart, double>(nameof(Slice1Angle), 180.0);

    /// <summary>
    /// Defines the <see cref="Slice1Brush"/> styled property.
    /// </summary>
    public static readonly StyledProperty<IBrush?> Slice1BrushProperty =
        AvaloniaProperty.Register<PieChart, IBrush?>(nameof(Slice1Brush), Brushes.DodgerBlue);

    /// <summary>
    /// Defines the <see cref="Slice2Brush"/> styled property.
    /// </summary>
    public static readonly StyledProperty<IBrush?> Slice2BrushProperty =
        AvaloniaProperty.Register<PieChart, IBrush?>(nameof(Slice2Brush), Brushes.Orange);

    /// <summary>
    /// Gets or sets the sweep angle in degrees for the first slice.
    /// </summary>
    public double Slice1Angle
    {
        get => this.GetValue(Slice1AngleProperty);
        set => this.SetValue(Slice1AngleProperty, value);
    }

    /// <summary>
    /// Gets or sets the brush used to paint the first slice.
    /// </summary>
    public IBrush? Slice1Brush
    {
        get => this.GetValue(Slice1BrushProperty);
        set => this.SetValue(Slice1BrushProperty, value);
    }

    /// <summary>
    /// Gets or sets the brush used to paint the second slice.
    /// </summary>
    public IBrush? Slice2Brush
    {
        get => this.GetValue(Slice2BrushProperty);
        set => this.SetValue(Slice2BrushProperty, value);
    }

    static PieChart()
    {
        AffectsRender<PieChart>(Slice1AngleProperty, Slice1BrushProperty, Slice2BrushProperty);
    }

    /// <inheritdoc />
    public override void Render(DrawingContext context)
    {
        base.Render(context);

        var size = Math.Min(this.Bounds.Width, this.Bounds.Height);

        if (size <= 0)
        {
            return;
        }

        var center = new Point(this.Bounds.Width / 2, this.Bounds.Height / 2);
        var radius = size / 2 - 2;
        var angle1 = this.Slice1Angle;

        // Draw slice 2 (full circle background)
        var fullGeometry = new EllipseGeometry(new Rect(center.X - radius, center.Y - radius, radius * 2, radius * 2));
        context.DrawGeometry(this.Slice2Brush, null, fullGeometry);

        // Draw slice 1
        if (angle1 > 0 && angle1 < 360)
        {
            var startAngle = -90.0;
            var endAngle = startAngle + angle1;

            var startRad = startAngle * Math.PI / 180.0;
            var endRad = endAngle * Math.PI / 180.0;

            var startPoint = new Point(center.X + radius * Math.Cos(startRad), center.Y + radius * Math.Sin(startRad));
            var endPoint = new Point(center.X + radius * Math.Cos(endRad), center.Y + radius * Math.Sin(endRad));

            var figure = new PathFigure { StartPoint = center, IsClosed = true, IsFilled = true };
            figure.Segments!.Add(new LineSegment { Point = startPoint });
            figure.Segments.Add(new ArcSegment
            {
                Point = endPoint,
                Size = new Size(radius, radius),
                IsLargeArc = angle1 > 180,
                SweepDirection = SweepDirection.Clockwise
            });

            var geometry = new PathGeometry();
            geometry.Figures!.Add(figure);
            context.DrawGeometry(this.Slice1Brush, null, geometry);
        }
        else if (angle1 >= 360)
        {
            context.DrawGeometry(this.Slice1Brush, null, fullGeometry);
        }
    }
}
