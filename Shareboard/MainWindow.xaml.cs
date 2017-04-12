using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Brushes = System.Windows.Media.Brushes;

namespace Shareboard
{
	public partial class MainWindow : MetroWindow
	{
		#region Attributes

		private Whiteboard _whiteboard;

		#endregion
		
		#region Constructors

		public MainWindow()
		{
			InitializeComponent();

			_whiteboard = new Whiteboard(DrawingCanvas, BrushSizeCalc(Sl_Thickness.Value));

			ChangeTool(Whiteboard.Tool.Brush);
		}

		#endregion

		#region Listeners

		#region MainWindow

		private void MainWindow_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.Z:
					if ((System.Windows.Forms.Control.ModifierKeys & System.Windows.Forms.Keys.Control) == System.Windows.Forms.Keys.Control)
						_whiteboard.Undo();
					break;
			}
		}

		#endregion

		#region DrawingCanvas

		private void DrawingCanvas_MouseDown(object sender, MouseButtonEventArgs e)
		{
			switch (_whiteboard.CurrentTool)
			{
				case Whiteboard.Tool.Brush:
					_whiteboard.BeginPaint(sender, e);
					break;

				case Whiteboard.Tool.Eraser:
					_whiteboard.BeginErase(sender, e);
					break;
			}
		}

		private void DrawingCanvas_MouseMove(object sender, MouseEventArgs e)
		{
			switch (_whiteboard.CurrentTool)
			{
				case Whiteboard.Tool.Brush:
					_whiteboard.Paint(sender, e);
					break;

				case Whiteboard.Tool.Eraser:
					_whiteboard.Erase(sender, e);
					break;
			}
		}

		private void DrawingCanvas_MouseUp(object sender, MouseButtonEventArgs e)
		{
			switch (_whiteboard.CurrentTool)
			{
				case Whiteboard.Tool.Brush:
					_whiteboard.StopPaint(sender, e);
					break;

				case Whiteboard.Tool.Eraser:
					_whiteboard.StopErase(sender, e);
					break;
			}
		}

		private void DrawingCanvas_MouseLeave(object sender, MouseEventArgs e)
		{
			switch (_whiteboard.CurrentTool)
			{
				case Whiteboard.Tool.Brush:
					_whiteboard.StopPaint(sender, null);
					break;

				case Whiteboard.Tool.Eraser:
					_whiteboard.StopErase(sender, null);
					break;
			}
		}

		private void DrawingCanvas_MouseEnter(object sender, MouseEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				switch (_whiteboard.CurrentTool)
				{
					case Whiteboard.Tool.Brush:
						_whiteboard.BeginPaint(sender, null);
						break;

					case Whiteboard.Tool.Eraser:
						_whiteboard.BeginErase(sender, null);
						break;
				}
			}
		}

		#endregion

		#region Draw Settings

		private void Btn_Brush_Click(object sender, RoutedEventArgs e)
		{
			ChangeTool(Whiteboard.Tool.Brush);
		}

		private void Btn_Eraser_Click(object sender, RoutedEventArgs e)
		{
			ChangeTool(Whiteboard.Tool.Eraser);
		}

		private void Btn_Delete_Click(object sender, RoutedEventArgs e)
		{
			_whiteboard.Clear();
		}

		private void Sl_Thickness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			Slider slider = sender as Slider;
			if (_whiteboard != null)
			{
				_whiteboard.BrushThickness = BrushSizeCalc(slider.Value);
			}
		}
		
		#endregion

		#endregion Listeners

		#region Misc

		private static double BrushSizeCalc(double SliderValue)
		{
			switch (SliderValue)
			{
				case 1:
					return 1;

				case 2:
					return 5;

				case 3:
					return 10;

				case 4:
					return 25;

				case 5:
					return 50;
			}
			return 1;
		}

		private void ChangeTool(Whiteboard.Tool tool)
		{
			_whiteboard.CurrentTool = tool;

			foreach (UIElement control in DrawSettings.Children)
			{
				if (control is Border)
				{
					Border border = control as Border;
					border.BorderBrush = Brushes.Transparent;
				}
			}
			switch (tool)
			{
				case Whiteboard.Tool.Brush:
					Border_Btn_Brush.BorderBrush = Brushes.Red;
					break;
				case Whiteboard.Tool.Eraser:
					Border_Btn_Eraser.BorderBrush = Brushes.Red;
					break;
				case Whiteboard.Tool.Scissors:
					//Border_Btn_Brush.BorderBrush = Brushes.Red;
					break;

			}
		}

		#endregion

	}
}