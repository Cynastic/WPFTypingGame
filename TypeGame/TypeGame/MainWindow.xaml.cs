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
using System.Windows.Media.Animation;

namespace TypeGame
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			TextManager.Initialize();
			ChangeText(TextManager.currentText);
			WriteTextBox.Focus();
			Dispatcher.BeginInvoke(AlignTextBoxes, System.Windows.Threading.DispatcherPriority.ContextIdle);
		}

		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			
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

		void TextTyped()
		{
			WriteTextBox.Text = "";
			TextManager.TextWritten();
			ChangeText(TextManager.currentText);
			Dispatcher.BeginInvoke(AlignTextBoxes, System.Windows.Threading.DispatcherPriority.ContextIdle);
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

		public void ChangeText(string text)
		{
			ShowTextBox.Text = text;
		}

		private void Window_ContentRendered(object sender, EventArgs e)
		{

		}

		private void WriteTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Tab) TextTyped();
		}
	}
}