using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeGame
{
    /// <summary>
    /// The data and state of a tic tac toe game
    /// </summary>
    public class TicTacToeModel
    {
        /// <summary>
        /// Status of the game
        /// </summary>
        public enum PlayStatus
        {
            /// <summary>
            /// Game is ready to be played
            /// </summary>
            ready,
            /// <summary>
            /// game is in play
            /// </summary>
            inPlay,
            /// <summary>
            ///  the game has been won
            /// </summary>
            won,
            /// <summary>
            ///  not definable status
            /// </summary>
            undefined,
            /// <summary>
            ///  the game has resulted in a draw
            /// </summary>
            draw,
        }

        /// <summary>
        /// Errors relating to the changing of the data state
        /// </summary>
        public enum ErrorCodes
        {
            /// <summary>
            /// An attempt was made to access an invalid row/col
            /// </summary>
            InvalidRowColumn,
            /// <summary>
            /// An attemp to 'click' on a cell that has already been taken
            /// </summary>
            CellIsAlreadyAssigned,
            /// <summary>
            /// The person making the move is invalid
            /// </summary>
            WrongPersonMakingMove,
            /// <summary>
            /// There are currently no errors
            /// </summary>
            NoError,
            /// <summary>
            /// An attemp was made to make a 'move', but the game is not in
            /// a playable state
            /// </summary>
            GameStatusDoesNotAllowAMoveAtThisPoint_Check_Status,
        }

        /// <summary>
        /// Who is the next play who should move
        /// </summary>
        /// <value> '1' for player 1, '2' for player 2 </value>
        public int NextPlayer { get => _NextPlayer; }
        private int _NextPlayer;

        private ErrorCodes _gameError;
        /// <summary>
        /// Error Codes after a play has been made
        /// <see cref="ErrorCodes"/>
        /// </summary>
        public ErrorCodes GameError { get => _gameError; }

        /// <summary>
        /// The current status of the game
        /// <see cref="PlayStatus"/>
        /// </summary>
        public PlayStatus status { get => _status; }
        PlayStatus _status = new PlayStatus();

        List<List<int>> board = new List<List<int>> {
            new List<int> { 0, 0, 0 },
            new List<int> { 0, 0, 0 },
            new List<int> { 0, 0, 0 }
        };

        /// <summary>
        /// Constructor
        /// </summary>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// var game = new TicTacToeModel();
        /// while (true) {
        ///    game.NewBoard();
        ///    while (game.status != TicTacToeModel.PlayStatus.won && 
        ///           game.status != TicTacToeModel.PlayStatus.draw) {
        ///        var player = game.NextPlayer;
        ///        Console.WriteLine($"Player {player}, it is your turn to play");
        ///        Console.WriteLine("Enter row number: ");
        ///        var row = Int32.Parse(Console.ReadLine());
        ///        Console.WriteLine("Enter col number: ");
        ///        var col = Int32.Parse(Console.ReadLine());
        ///        game.MakePlay(player, row, col);
        ///        if (game.GameError != TicTacToeModel.ErrorCodes.NoError) {
        ///           Console.Write("There was an error: ");
        ///           Console.WriteLine(Enum.GetName(typeof(TicTacToeModel.ErrorCodes),
        ///                game.GameError));
        ///        }
        ///    }
        /// 
        ///    if (game.status == TicTacToeModel.PlayStatus.won) {
        ///       Console.WriteLine($"Player {game.NextPlayer} won the game");
        ///    }
        ///    if (game.status == TicTacToeModel.PlayStatus.draw) {
        ///       Console.WriteLine($"The game ended in a draw");
        ///    }
        ///    Console.WriteLine("Hit Return for Next Game");
        ///    Console.ReadLine();
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public TicTacToeModel()
        {
            _status = PlayStatus.undefined;
        }

        /// <summary>
        /// Resest the tic-tac-toe board, and the game state
        /// </summary>
        public void NewBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i][j] = 0;
                }
            }
            _status = PlayStatus.ready;
            _NextPlayer = 1;
            _gameError = ErrorCodes.NoError;
        }

        /// <summary>
        /// Make a Move
        /// </summary>
        /// <param name="playerNum">The Player who is making the move (1 or 2)</param>
        /// <param name="row">The row (zero-based) of the chosen cell</param>
        /// <param name="col">The column (zero-based) fo the chosen cell</param>
        /// <returns>true if a successful move, false if there was
        /// an error <see cref="ErrorCodes"/></returns>
        public Boolean MakePlay(int playerNum, int row, int col)
        {

            if (status != PlayStatus.ready && status != PlayStatus.inPlay)
            {
                _gameError = ErrorCodes.GameStatusDoesNotAllowAMoveAtThisPoint_Check_Status;
                return false;
            }

            _status = PlayStatus.inPlay;

            if (playerNum != _NextPlayer)
            {
                _gameError = ErrorCodes.WrongPersonMakingMove;
                return false;
            }
            if (row > 2 || col > 2)
            {
                _gameError = ErrorCodes.InvalidRowColumn;
                return false;
            }
            if (board[row][col] != 0)
            {
                _gameError = ErrorCodes.CellIsAlreadyAssigned;
                return false;
            }

            board[row][col] = playerNum;
            _gameError = ErrorCodes.NoError;

            if (CheckForWinning())
            {
                _status = PlayStatus.won;
                return true;
            }
            if (CheckForDraw())
            {
                _status = PlayStatus.draw;
                return true;
            }

            _NextPlayer = _NextPlayer % 2 + 1;
            return true;
        }

        private Boolean CheckForDraw()
        {
            Boolean drawflag = true;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i][j] == 0)
                    {
                        drawflag = false;
                    }
                }
            }
            return drawflag;

        }
        private Boolean CheckForWinning()
        {
            Boolean winflag = false;
            int winner = 0;
            for (int i = 0; i < 3; i++)
            {
                if (board[i][0] == board[i][1] && board[i][0] == board[i][2] && board[i][0] != 0)
                { winflag = true; winner = board[i][0]; }
            }
            for (int j = 0; j < 3; j++)
            {
                if (board[0][j] == board[1][j] && board[0][j] == board[2][j] && board[0][j] != 0)
                { winflag = true; winner = board[0][j]; }
            }
            if (board[1][1] == board[2][2] && board[1][1] == board[0][0] && board[1][1] != 0)
            { winflag = true; winner = board[1][1]; }
            if (board[1][1] == board[0][2] && board[1][1] == board[2][0] && board[1][1] != 0)
            { winflag = true; winner = board[1][1]; }

            return winflag;
        }
    }

}
