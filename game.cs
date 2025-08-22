using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    class Game
    {
        #region Props
        private int[,] Board;
        private int boardLength;
        private bool CanMove {  get; set; }
        #endregion
        #region Constractor
        public Game()
        {
            Board = new int[4,4];
            boardLength = 4;
            firstCon();
            RandomSpawner(2);
            CanMove = true;
            PrintBoard();
        }
        #endregion
        #region Print Board
        private string FindS()
        {
            string s = "-";
            for (int i = 0; i < boardLength; i++) s += "------";
            return s;
        }
        public void PrintBoard()
        {
            Console.Clear();
            var s = FindS();
            string o = "                                                 ";
            Console.WriteLine("\n\n\n\n\n\n\n");
            Console.WriteLine(o + s);
            for (var j = 0; j < boardLength ; j++)
            {
                Console.Write(o);
                for (var i = 0; i < boardLength; i++)
                {
                    if (Board[j, i] < 10) { if (Board[j, i] == 0) Console.Write($"|     "); else Console.Write($"|  {Board[j, i]}  "); }
                    else if (Board[j, i] < 100) Console.Write($"| {Board[j, i]}  ");
                    else if (Board[j, i] < 1000) Console.Write($"| {Board[j, i]} ");
                    else if (Board[j, i] < 10000) Console.Write($"|{Board[j, i]} ");
                    else Console.Write($"|{Board[j, i]}");
                }
                Console.WriteLine("|");
                Console.WriteLine(o+s);
            }
        }
        public void PrintBoardWithoutClear()
        {
            var s = FindS();
            Console.WriteLine(s);
            for (var j = 0; j < boardLength; j++)
            {
                for (var i = 0; i < boardLength; i++)
                {
                    if (Board[j, i] < 10) { if (Board[j, i] == 0) Console.Write($"|     "); else Console.Write($"|  {Board[j, i]}  "); }
                    else if (Board[j, i] < 100) Console.Write($"| {Board[j, i]}  ");
                    else if (Board[j, i] < 1000) Console.Write($"| {Board[j, i]} ");
                    else if (Board[j, i] < 10000) Console.Write($"|{Board[j, i]} ");
                    else Console.Write($"|{Board[j, i]}");
                }
                Console.WriteLine("|");
                Console.WriteLine(s);
            }
        }
        #endregion
        #region GameEnds
        private void IsGameEnded()
        {
            if (!CanMoveUp() && !CanMoveDown() && !CanMoveLeft() && !CanMoveRight())
            {
                CanMove = false;
                PrintGameEnds();
            }
        }
        private void PrintGameEnds()
        {
            CanMove = false;
            Console.Clear();
            Console.WriteLine("                  ######      ###    ##     ## ########        ######   ##         ##  ######## #######  ");
            Console.WriteLine("                 ##    ##    ## ##   ###   ### ##             ##    ##   ##       ##   ##       ##    ## ");
            Console.WriteLine("                 ##         ##   ##  #### #### ##             ##    ##    ##     ##    ##       ##    ## ");
            Console.WriteLine("                 ##   #### ##     ## ## ### ## ######         ##    ##     ##   ##     ######   #######  ");
            Console.WriteLine("                 ##    ##  ######### ##     ## ##             ##    ##      ## ##      ##       ##   ##  ");
            Console.WriteLine("                 ##    ##  ##     ## ##     ## ##             ##    ##       ###       ##       ##    ## ");
            Console.WriteLine("                  ######   ##     ## ##     ## ########        ######         #        ######## ##     ##");
            PrintBoardWithoutClear();
        }
        #endregion
        #region CanMoveInDiraction
        private bool CanMoveUp()
        {
            var temp = new int[boardLength, boardLength];
            var changed = false;
            for (var i = 0; i < boardLength; i++)
            {
                for (var j = 0; j < boardLength; j++)
                {
                    temp[i, j] = Board[i, j];
                }
            }
            SimulateMoveUp(temp);
            for (var i = 0; i < boardLength; i++)
            {
                for (var j = 0; j < boardLength; j++)
                {
                    if (temp[i, j] != Board[i, j])
                    {
                        changed = true;
                        break;
                    }
                }
            }
            return changed;
        }
        private void SimulateMoveUp(int[,] tempBoard)
        {
            for (var j = 0; j < boardLength; j++)
            {
                for (var i = 1; i < boardLength; i++)
                {
                    if (tempBoard[i, j] != 0)
                    {
                        var row = i;
                        while (row > 0 && tempBoard[row - 1, j] == 0)
                        {
                            tempBoard[row - 1, j] = tempBoard[row, j];
                            tempBoard[row, j] = 0;
                            row--;
                        }
                        if (row > 0 && tempBoard[row - 1, j] == tempBoard[row, j])
                        {
                            tempBoard[row - 1, j] *= 2;
                            tempBoard[row, j] = 0;
                        }
                    }
                }
            }
        }


        private bool CanMoveDown()
        {
            var temp = new int[boardLength, boardLength];
            var changed = false;
            for (var i = 0; i < boardLength; i++)
            {
                for (var j = 0; j < boardLength; j++)
                {
                    temp[i, j] = Board[i, j];
                }
            }
            MoveDown();
            for (var i = 0; i < boardLength; i++)
            {
                for (var j = 0; j < boardLength; j++)
                {
                    if (temp[i, j] != Board[i, j])
                    {
                        changed = true;
                        break;
                    }
                }
            }
            for (var i = 0; i < boardLength; i++)
            {
                for (var j = 0; j < boardLength; j++)
                {
                    Board[i, j] = temp[i, j];
                }
            }
            return changed;
        }

        private bool CanMoveLeft()
        {
            var temp = new int[boardLength, boardLength];
            var changed = false;
            for (var i = 0; i < boardLength; i++)
            {
                for (var j = 0; j < boardLength; j++)
                {
                    temp[i, j] = Board[i, j];
                }
            }
            MoveLeft();
            for (var i = 0; i < boardLength; i++)
            {
                for (var j = 0; j < boardLength; j++)
                {
                    if (temp[i, j] != Board[i, j])
                    {
                        changed = true;
                        break;
                    }
                }
            }
            for (var i = 0; i < boardLength; i++)
            {
                for (var j = 0; j < boardLength; j++)
                {
                    Board[i, j] = temp[i, j];
                }
            }
            return changed;
        }


        private bool CanMoveRight()
        {
            var temp = new int[boardLength, boardLength];
            var changed = false;
            for (var i = 0; i < boardLength; i++)
            {
                for (var j = 0; j < boardLength; j++)
                {
                    temp[i, j] = Board[i, j];
                }
            }
            MoveRight();
            for (var i = 0; i < boardLength; i++)
            {
                for (var j = 0; j < boardLength; j++)
                {
                    if (temp[i, j] != Board[i, j])
                    {
                        changed = true;
                        break;
                    }
                }
            }
            for (var i = 0; i < boardLength; i++)
            {
                for (var j = 0; j < boardLength; j++)
                {
                    Board[i, j] = temp[i, j];
                }
            }
            return changed;
        }

        #endregion
        #region ReadMoves
        public void Keys(ConsoleKeyInfo keyInfo)
        {
            IsGameEnded();
            if (CanMove == false) return;
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (CanMoveUp())
                    {
                        MoveUp();
                        RandomSpawner();
                        PrintBoard();
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (CanMoveDown())
                    {
                        RandomSpawner();
                        MoveDown();
                        PrintBoard();
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (CanMoveLeft())
                    {
                        RandomSpawner();
                        MoveLeft();
                        PrintBoard();
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (CanMoveRight())
                    {
                        RandomSpawner();
                        MoveRight();
                        PrintBoard();
                    }
                    break;
                case ConsoleKey.Escape:
                    PrintGameEnds();
                    break;
                default:
                    break;
            }

        }
        #endregion
        #region RandomSpawner
        private int NumOfPlacesToSpawn()
        {
            int counter = 0;
            foreach(var Places in Board)
            {
                if (Places == 0) counter++;
            }
            return counter;
        }
        private int Generate2Or4()
        {
            Random rnd = new Random();
            int temp = rnd.Next(1,101);
            if (temp > 90) return 4;
            else return 2;
        }
        private void firstCon()
        {
            for (var i = 0; i < boardLength; i++)
            {
                for (var j = 0; j < boardLength; j++)
                {
                    Board[i, j] = 0;
                }
            }
        }
        private void RandomSpawner(int NumOfSpawns)
        {
            var NumOfPlaces = NumOfPlacesToSpawn();
             Random rnd = new();
            for (var z = 0; z < NumOfSpawns; z++)
            {
                if (NumOfPlaces == 0) return;
                int spawn = rnd.Next(0, NumOfPlaces);
                for (var i = 0; i < boardLength; i++)
                {
                    for (var j = 0; j < boardLength; j++)
                    {
                        if (spawn == 0 && Board[i,j] == 0) { Board[i, j] = Generate2Or4(); spawn--; break;}
                        else if(Board[i,j] == 0) spawn--;
                    }
                }
                NumOfPlaces--;
            }
        }
        private void RandomSpawner()
        {
            var NumOfPlaces = NumOfPlacesToSpawn();
            Random rnd = new();

            if (NumOfPlaces == 0) return;
            int spawn = rnd.Next(0, NumOfPlaces);
            for (var i = 0; i < boardLength; i++)
            {
                for (var j = 0; j < boardLength; j++)
                {
                    if (spawn == 0 && Board[i, j] == 0) { Board[i, j] = Generate2Or4(); spawn--; break; }
                    else if (Board[i, j] == 0) spawn--;
                }
            }
            NumOfPlaces--;

        }
        #endregion
        #region Moves
        private void MoveUp()
        {
            for (var z = 0; z < boardLength - 1; z++)
            {
                for (var i = 1; i < boardLength; i++)
                {
                    for (var j = 0; j < boardLength; j++)
                    {
                        if(Board[i-1,j] == 0){ Board[i-1,j] = Board[i, j]; Board[i, j] = 0; }
                        if (Board[i-1,j] == Board[i,j]) { Board[i-1,j] = Board[i,j] * 2; Board[i,j] = 0; }
                    }
                }
            }
        }
        private void MoveDown()
        {
            for (var z = 0; z < boardLength - 1; z++)
            {
                for (var i = 0; i < boardLength -1; i++)
                {
                    for (var j = 0; j < boardLength; j++)
                    {
                        if (Board[i + 1, j] == 0) { Board[i + 1, j] = Board[i, j]; Board[i, j] = 0; }
                        if (Board[i + 1, j] == Board[i, j]) { Board[i + 1, j] = Board[i, j] * 2; Board[i, j] = 0; }
                    }
                }
            }
        }
        private void MoveLeft()
        {
            for (var z = 0; z < boardLength - 1; z++)
            {
                for (var i = 0; i < boardLength; i++)
                {
                    for (var j = 1; j < boardLength ; j++)
                    {
                        if (Board[i , j - 1] == 0) { Board[i, j - 1] = Board[i, j]; Board[i, j] = 0; }
                        if (Board[i , j - 1] == Board[i, j]) { Board[i , j - 1] = Board[i, j ] * 2; Board[i, j] = 0; }
                    }
                }
            }
        }
        private void MoveRight()
        {
            for (var z = 0; z < boardLength - 1; z++)
            {
                for (var i = 0; i < boardLength; i++)
                {
                    for (var j = 0; j < boardLength-1; j++)
                    {
                        if (Board[i, j + 1] == 0) { Board[i, j + 1] = Board[i, j]; Board[i, j] = 0; }
                        if (Board[i, j + 1] == Board[i, j]) { Board[i, j + 1] = Board[i, j] * 2; Board[i, j] = 0; }
                    }
                }
            }
        }

        #endregion
        #region overrides
        public override string ToString()
        {
            string m = "";
            Console.Clear();
            var s = FindS();
            m += s + "\n";
            for (var j = 0; j < boardLength; j++)
            {
                for (var i = 0; i < boardLength; i++)
                {
                    m += $"|  {Board[j, i]}  ";
                }
                m += "|" + "\n";
                m += s + "\n";
            }
            return m;
        }
        #endregion
    }
}
