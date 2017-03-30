using MahApps.Metro.Controls;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Shareboard
{
	public partial class MainWindow : MetroWindow
	{
		#region Attributes

		private Whiteboard Whiteboard;

		#endregion

		#region Constructors

		public MainWindow()
		{
			InitializeComponent();
			Whiteboard = new Whiteboard(DrawingCanvas, BrushSizeCalc(Sl_BrushThickness.Value));
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
						Whiteboard.Undo();
					break;
			}
		}

		#endregion

		#region DrawingCanvas

		private void DrawingCanvas_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Whiteboard.BeginPaint(sender, e);
		}

		private void DrawingCanvas_MouseMove(object sender, MouseEventArgs e)
		{
			Whiteboard.Paint(sender, e);
		}

		private void DrawingCanvas_MouseUp(object sender, MouseButtonEventArgs e)
		{
			Whiteboard.StopPaint(sender, e);
		}

		#endregion

		#region Draw Settings

		private void Btn_Brush_Click(object sender, System.Windows.RoutedEventArgs e)
		{

		}

		private void Btn_Delete_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Whiteboard.Clear();
		}

		private void Sl_BrushThickness_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
		{
			Slider slider = sender as Slider;
			if (Whiteboard != null)
			{
				Whiteboard.BrushThickness = BrushSizeCalc(slider.Value);
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

		#endregion
	}
}