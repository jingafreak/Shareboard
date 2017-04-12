using System.Windows.Input;
using System.Windows.Shapes;
using Brushes = System.Windows.Media.Brushes;
using Canvas = System.Windows.Controls.Canvas;

namespace Shareboard
{
	class Whiteboard
	{
		#region Attributes

		#region General

		public enum Tool
		{
			Brush,
			Eraser,
			Scissors
		}

		public Canvas DrawingCanvas;
		public Tool CurrentTool;
		public double InitX = -1;
		public double InitY = -1;

		#endregion

		#region Painting

		public bool PaintOn;
		public double BrushThickness;

		#endregion

		#region Erasing

		public bool EraseOn;
		public double EraserThickness;

		#endregion

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
			if (PaintOn)
			{
				double X = e.GetPosition(DrawingCanvas).X;
				double Y = e.GetPosition(DrawingCanvas).Y;

				Line line = new Line()
				{
					Stroke = Brushes.Red,
					StrokeThickness = BrushThickness,
					X1 = InitX,
					Y1 = InitY,
					X2 = X,
					Y2 = Y
				};

				if (InitX == -1 || InitY == -1)
				{
					line.X1 = X;
					line.Y1 = Y;
				}

				DrawingCanvas.Children.Add(line);

				InitX = X;
				InitY = Y;
			}
		}

		public void BeginPaint(object sender, MouseButtonEventArgs e)
		{
			PaintOn = true;
		}

		public void StopPaint(object sender, MouseButtonEventArgs e)
		{
			PaintOn = false;

			InitX = -1;
			InitY = -1;
		}

		#endregion

		#region Erasing

		public void Erase(object sender, MouseEventArgs e)
		{
			if (EraseOn)
			{
				double X = e.GetPosition(DrawingCanvas).X;
				double Y = e.GetPosition(DrawingCanvas).Y;

				Line line = new Line()
				{
					Stroke = Brushes.Red,
					StrokeThickness = EraserThickness,
					X1 = InitX,
					Y1 = InitY,
					X2 = X,
					Y2 = Y
				};

				DrawingCanvas.Children.Add(line);

				InitX = X;
				InitY = Y;
			}
		}

		public void BeginErase(object sender, MouseButtonEventArgs e)
		{
		}

		public void StopErase(object sender, MouseButtonEventArgs e)
		{
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