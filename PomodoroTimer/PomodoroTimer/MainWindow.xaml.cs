using Microsoft.VisualBasic;
using PomodoroTimer.Services;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PomodoroTimer;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private DispatcherTimer dispatchTimer;

    public MainWindow()
    {
        InitializeComponent();

        this.MouseLeftButtonDown += (sender, e) =>
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        };


        dispatchTimer = new DispatcherTimer();
        dispatchTimer.Interval = TimeSpan.FromMinutes(5);
        dispatchTimer.Tick += UpdateQuote;


        dispatchTimer.Start();
    }

    private void UpdateQuote(object sender, EventArgs e)
    {
        QuoteBlock.Text = QuoteProvider.GetStudyQuote();

    }
}