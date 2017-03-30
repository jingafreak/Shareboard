using System;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Shareboard
{
	class Whiteboard
	{
		#region Attributes

		private System.Windows.Controls.Canvas _drawingCanvas;
		public System.Windows.Controls.Canvas DrawingCanvas { get => _drawingCanvas; set => _drawingCanvas = value; }

		private System.Windows.Point _startPos;
		public System.Windows.Point StartPos { get => _startPos; set => _startPos = value; }

		private Path _path;
		public Path Path { get => _path; set => _path = value; }

		private double _brushThickness;
		public double BrushThickness {
			get
			{
				return _brushThickness;
			}
			set
			{
				_brushThickness = value;
				if (Path != null)
					Path.StrokeThickness = value;
			}
		}

		#endregion

		#region Constructors

		public Whiteboard(System.Windows.Controls.Canvas DrawingCanvas)
		{
			this.DrawingCanvas = DrawingCanvas;
		}
		public Whiteboard(System.Windows.Controls.Canvas DrawingCanvas, double BrushThickness)
		{
			this.DrawingCanvas = DrawingCanvas;
			this.BrushThickness = BrushThickness;
		}

		#endregion

		#region Painting

		public void Paint(object sender, MouseEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				if (Path == null)
				{
					return;
				}

				PolyLineSegment pls = (PolyLineSegment) ((PathGeometry) Path.Data).Figures.Last().Segments.Last();
				pls.Points.Add(e.GetPosition(DrawingCanvas));
			}
		}

		public void BeginPaint(object sender, MouseButtonEventArgs e)
		{
			if (e.ButtonState == MouseButtonState.Pressed)
			{
				StartPos = e.GetPosition(DrawingCanvas);


				Path = new Path
				{
					Data = new PathGeometry
					{
						Figures = {
							new PathFigure {
								StartPoint = StartPos,
								Segments = {new PolyLineSegment()}
							}
						}
					},
					Stroke = new SolidColorBrush(Colors.Red),
					StrokeThickness = BrushThickness
				};

				DrawingCanvas.Children.Add(Path);
			}
		}

		public void StopPaint(object sender, MouseButtonEventArgs e)
		{
			Path = null;
		}

		#endregion

		#region Misc

		public void Undo()
		{
			if (DrawingCanvas.Children.Count > 0)
			{
				DrawingCanvas.Children.RemoveAt(DrawingCanvas.Children.Count - 1);
			}
		}

		public void Clear()
		{
			DrawingCanvas.Children.Clear();
		}

		#endregion
	}
}