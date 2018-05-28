using System.Windows;
/*	
 *	Jiwant Singh
    Nihal Ahmed Gesudraz
    Apoorva Solanki
    Kiranpreet Kaur
    Harkirat Kaur
*/

namespace Condorcet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
		CondorcetVM cvm = new CondorcetVM();

		public MainWindow()
        {
            InitializeComponent();
			DataContext = cvm;
		}

		private void FindWinner(object sender, RoutedEventArgs e)
		{
			cvm.FindWinner();
		}

		private void Reset(object sender, RoutedEventArgs e)
		{
			cvm.Reset();
		}
	}
}
