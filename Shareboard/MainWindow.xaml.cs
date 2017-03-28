using MahApps.Metro.Controls;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Shareboard
{
	public partial class MainWindow : MetroWindow
	{

		private System.Windows.Point _startPos;
		private Path _currentPath;

		public MainWindow()
		{
			InitializeComponent();
		}

		#region Painting Mouse Events
		
		private void Paint(object sender, MouseEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				if (_currentPath == null)
				{
					return;
				}

				PolyLineSegment pls =
					(PolyLineSegment)((PathGeometry)_currentPath.Data).Figures.Last().Segments.Last();
				pls.Points.Add(e.GetPosition(whiteboard));
			}
		}
		
		private void BeginPaint(object sender, MouseButtonEventArgs e)
		{
			if (e.ButtonState == MouseButtonState.Pressed)
			{
				_startPos = e.GetPosition(whiteboard);


				_currentPath = new Path
				{
					Data = new PathGeometry
					{
						Figures = {
							new PathFigure {
								StartPoint = _startPos,
								Segments = {new PolyLineSegment()}
							}
						}
					},
					Stroke = new SolidColorBrush(Colors.Red),
					StrokeThickness = 4
				};

				whiteboard.Children.Add(_currentPath);
			}
		}
		
		private void StopPaint(object sender, MouseButtonEventArgs e)
		{
			_currentPath = null;
		}


		private void CtrlZ()
		{
			if (whiteboard.Children.Count > 0)
			{
				whiteboard.Children.RemoveAt(whiteboard.Children.Count - 1);
			}
		}

		#endregion

		private void whiteboard_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.Z:
					if ((System.Windows.Forms.Control.ModifierKeys & System.Windows.Forms.Keys.Control) == System.Windows.Forms.Keys.Control)
						CtrlZ();
					break;
			}
		}
	}
}
