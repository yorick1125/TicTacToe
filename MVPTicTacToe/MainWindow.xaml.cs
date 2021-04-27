using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MVPTicTacToe;

namespace TicTacToeGuiMVP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : UIInterface
    {
        Presenter Presenter;
        public MainWindow()
        {
            InitializeComponent();
            // create the presenter, let it know that we are the
            // view interface
            
            Presenter = new Presenter(this);
        }

        // =============================================================
        // View Interface
        // =============================================================
        #region view interface
        public void DrawTicTacToeSymbol(int player, int row, int col)
        {
            String text = "X";
            if (player == 2) text = "O";
            foreach (UIElement uie in TicTacToeGrid.Children)
            {
                if (Grid.GetColumn(uie) == col && Grid.GetRow(uie) == row)
                {
                    var b = uie as Button;
                    b.Content = text;
                }
            }
        }
        public void UpdateGameFinished(String text)
        {
            StatusLabel.Text = text;
        }
        public void ShowNextPlayer(int player)
        {
            NextLabel.Text = $"Player {player}'s Turn";
        }
        public void ShowError(String text)
        {
            ErrorLabel.Text = text;
        }
        public void UpdateStatus(String text)
        {
            StatusLabel.Text = text;
        }
        public void ResetBoard()
        {
            foreach (UIElement uie in TicTacToeGrid.Children)
            {
                var b = uie as Button;
                b.Content = "";
            }
        }
        #endregion

        // ========================================================================
        // View (responds to events from the view)
        // ========================================================================
        #region View (event handlers)
        private void Start_Game_Clicked(object sender, RoutedEventArgs e)
        {
            Presenter.StartGame();
        }

        private void CellClicked(object sender, RoutedEventArgs e)
        {
            Presenter.OnCellClicked(Grid.GetRow(sender as Button ), Grid.GetColumn(sender as Button));
        }
        #endregion
    }
}
