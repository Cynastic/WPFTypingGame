using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Effects;
using System.Diagnostics;
using System.Windows.Media.Animation;

namespace TypeGame
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		bool timerRunning = false;
		Stopwatch stopwatch = new();

		public MainWindow()
		{
			InitializeComponent();
			TextManager.Initialize();
			UpdateText();
			WriteTextBox.Focus();
			Dispatcher.BeginInvoke(AlignTextBoxes, System.Windows.Threading.DispatcherPriority.ContextIdle);
		}

		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (!timerRunning) StartTimer();
			for(int i = 0; i < WriteTextBox.Text.Length; i++)
			{
				if (TextManager.currentText[i] != WriteTextBox.Text[i])
				{
					WriteTextBox.Text = WriteTextBox.Text.Remove(i, WriteTextBox.Text.Length - i);
					WriteTextBox.CaretIndex = i;
					WrongCharTyped();
				}
			}
			//check Win Condition
			if(WriteTextBox.Text == TextManager.currentText)
			{
				TextTyped();
			}
		}

		private void StartTimer()
		{
			stopwatch.Start();
			timerRunning = true;
		}

		private void EndTimer()
		{
			stopwatch.Stop();
			timerRunning = false;
		}

		void TextTyped()
		{
			EndTimer();
			double cpm = TextManager.currentText.Length / ((double)stopwatch.ElapsedMilliseconds / 60000d);
			MessageBox.Show((Math.Round(cpm)).ToString());
			WriteTextBox.Text = "";
			TextManager.TextWritten();
			UpdateText();
			Dispatcher.BeginInvoke(AlignTextBoxes, System.Windows.Threading.DispatcherPriority.ContextIdle);
			stopwatch = new();
		}

		void AlignTextBoxes()
		{
			WriteTextBox.MinWidth = ShowTextBox.ActualWidth;
			WriteTextBox.MinHeight = ShowTextBox.ActualHeight;
			WriteTextBox.Width = ShowTextBox.ActualWidth;
			WriteTextBox.Height = ShowTextBox.ActualHeight;
		}

		void WrongCharTyped()
		{
			ColorAnimation colorFade = new ColorAnimation(Colors.Red, Color.FromRgb(66, 66, 66), new Duration(TimeSpan.FromMilliseconds(400)));
			Storyboard.SetTarget(colorFade, ShowTextBox);
			Storyboard.SetTargetProperty(colorFade, new PropertyPath("Foreground.Color"));

			Storyboard sb = new();
			sb.Children.Add(colorFade);
			sb.Begin();
		}

		public void UpdateText()
		{
			ShowTextBox.Text = TextManager.currentText;
			authorLabel.Content = TextManager.currentAuthor;
		}

		private void Window_ContentRendered(object sender, EventArgs e)
		{

		}

		private void WriteTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			//Control is pressed:
			if((Keyboard.GetKeyStates(Key.LeftCtrl) & KeyStates.Down) > 0)
			{
				switch(e.Key)
				{
					case Key.OemPlus:
						IncreaseFontSize();
						break;
					case Key.OemMinus:
						DecreaseFontSize();
						break;
				}
			}

			switch(e.Key)
			{
				case Key.Tab:
					TextTyped();
					break;
			}
		}

		private void IncreaseFontSize()
		{
			ShowTextBox.FontSize++;
			WriteTextBox.FontSize++;
		}

		private void DecreaseFontSize()
		{
			if (ShowTextBox.FontSize <= 1) return;
			ShowTextBox.FontSize--;
			WriteTextBox.FontSize--;
		}

		private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			Dispatcher.BeginInvoke(AlignTextBoxes, System.Windows.Threading.DispatcherPriority.ContextIdle);
		}
	}
}