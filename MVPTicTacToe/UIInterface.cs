using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPTicTacToe
{
    public interface UIInterface
    {
        void DrawTicTacToeSymbol(int player, int row, int col);
        void UpdateGameFinished(string v);
        void ShowNextPlayer(int nextPlayer);
        void ShowError(string v);
        void UpdateStatus(string v);
        void ResetBoard();
    }
}
