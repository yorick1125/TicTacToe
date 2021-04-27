using System;
using TicTacToeGame;
using TicTacToeGuiMVP;
using System.Windows;
using MVPTicTacToe;

namespace TicTacToeGuiMVP
{
    public class Presenter
    {
        UIInterface IView;
        TicTacToeModel game;

        public Presenter(UIInterface view)
        {
            IView = view;
            game = new TicTacToeModel();
            StartGame();
        }

        // =========================================================================
        // User clicked one of the tic tac toe cells;
        // =========================================================================
        public void OnCellClicked(int row, int col)
        {
            var player = game.NextPlayer;

            if (game.MakePlay(player, row, col))
            {
                IView.DrawTicTacToeSymbol(player, row, col);
                switch (game.status)
                {
                    case TicTacToeModel.PlayStatus.won:
                        IView.UpdateGameFinished($"Player {player} WON!!");
                        break;
                    case TicTacToeModel.PlayStatus.draw:
                        IView.UpdateGameFinished("The game ended in a draw");
                        break;
                    default:
                        IView.ShowNextPlayer(game.NextPlayer);
                        break;
                }
                IView.ShowError("");
            }
            else
            {
                IView.ShowError(Enum.GetName(typeof(TicTacToeModel.ErrorCodes), game.GameError));
            }
            IView.UpdateStatus("status: " + Enum.GetName(typeof(TicTacToeModel.PlayStatus), game.status));
        }

        // =========================================================================
        // User clicked the StartGame button
        // =========================================================================
        public  void StartGame()
        {
            game.NewBoard();
            IView.ResetBoard();
            IView.UpdateStatus("status: " + Enum.GetName(typeof(TicTacToeModel.PlayStatus), game.status));
            IView.ShowNextPlayer(game.NextPlayer);
            IView.ShowError("");
        }

    }
}
