using System.Collections.Generic;


namespace TicTacToe.Entities
{
    public class Game
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public GameType GameType { get; set; }
        public GameStatus Status { get; set; }
        public bool IsPlayer1Turn { get; set; }

        private uint _moveCounter;
        public  List<int> _winLine;
        private static List<int> _gameBoard;
        private static List<int> _winCombinations;

        public Game(Player player1, Player player2, GameType gameType)
        {
            Player1 = player1;
            Player2 = player2;
            GameType = gameType;
            Status = GameStatus.NoWin;
            IsPlayer1Turn = true;

            _moveCounter = 0;
            _winLine = new List<int>(3) { 0, 0, 0 };
            _gameBoard = new List<int>(9) { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            _winCombinations = new List<int> {  0,1,2, 3,4,5,
                                                6,7,8, 0,3,6,
                                                1,4,7, 2,5,8,
                                                0,4,8, 2,4,6 };

        }

        public void PlayMove(int buttonNumber, uint playerId)
        {
            ++(_moveCounter);
            _gameBoard[buttonNumber] = (int)playerId;
            Status = CheckGameStatus();
            IsPlayer1Turn ^= true;
        }

        private bool CheckCombination(int index)
        {
            if ((_gameBoard[_winCombinations[index]] != 0) &&
                (_gameBoard[_winCombinations[index]] == _gameBoard[_winCombinations[index + 1]]) &&
                (_gameBoard[_winCombinations[index + 1]] == _gameBoard[_winCombinations[index + 2]]))
            {
                return true;
            }
            return false;
        }

        private void SetWinningLine(int index)
        {
            _winLine[0] = _winCombinations[index];
            _winLine[1] = _winCombinations[index + 1];
            _winLine[2] = _winCombinations[index + 2];
        }

        private void UpdateScore(Player player)
        {
            ++(player.Score);
        }

        public string GetPlayersTurnText()
        {
            return (IsPlayer1Turn) ? $"{Player1.Name}'s turn..." : $"{Player2.Name}'s turn...";
        }

        private GameStatus CheckGameStatus()
        {
            int listLength = _winCombinations.Count;
            
            // Player can't win before 4th move so there is no need for checking
            if (_moveCounter > 4)
            {
                for (int index = 0; index < listLength; index += 3)
                {
                    if (CheckCombination(index))
                    {
                        SetWinningLine(index);
                        UpdateScore((IsPlayer1Turn) ? Player1 : Player2);
                        return GameStatus.Win;
                    }
                }

                foreach (var button in _gameBoard)
                {
                    if (button == 0)
                    {
                        return GameStatus.NoWin;
                    }
                }
                return GameStatus.Draw;
            }
            return GameStatus.NoWin;
        }

        private void Reset(List<int> list)
        {
            int length = list.Count;

            for (int i = 0; i < length; ++i)
            {
                list[i] = 0;
            }
        }
        public void ResetGame()
        {
            _moveCounter = 0;
            Status = GameStatus.NoWin;
            Reset(_winLine);
            Reset(_gameBoard);
        }
    }
}
