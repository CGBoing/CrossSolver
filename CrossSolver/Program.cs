using System;
using System.Collections.Generic;

namespace CrossSolver
{

    public struct Move
    {
        public int x;
        public int y;
        public String direction;

        public Move(int v1, int v2, String dir)
        {
            x = v1;
            y = v2;
            direction = dir;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("BoardSolver v0.1 | Jopi Mikkonen 2019\n");
            Console.WriteLine("Solving. This could take a while...\n");

            const int MAX_MOVES = 29;

            List<Move> result = new List<Move>();
            List<Move> possibleMoves = new List<Move>();
            int iterations = 0;
            int boardX;
            int boardY;
            int displayX;
            int displayY;
            Boolean full = false;
            Boolean deadEnd = false;
            Random rnd = new Random();

            do
            {
                iterations++;
                //Console.Write(iterations);
                result.Clear();


                // This represents the board with a border of -1s
                int[,] matrix = {
            { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            { -1, -1, -1, -1, 1, 1, 1, -1, -1, -1, -1},
            { -1, -1, -1, -1, 1, 0, 1, -1, -1, -1, -1},
            { -1, -1, 1, 1, 1, 1, 0, 0, 1, -1, -1},
            { -1, -1, 1, 1, 1, 1, 1, 1, 1, -1, -1},
            { -1, -1, 1, 1, 1, 1, 1, 1, 1, -1, -1},
            { -1, -1, -1, -1, 1, 1, 1, -1, -1, -1, -1},
            { -1, -1, -1, -1, 1, 1, 1, -1, -1, -1, -1},
            { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            };

                // Let's solve it!
                for (int i = 0; i < MAX_MOVES; i++)
                {
                    //Console.Write(".");
                    possibleMoves.Clear();
                    Move selectedMove;
                    deadEnd = true;
                    for (int y = 2; y < 9; y++)
                    {
                        for (int x = 2; x < 9; x++)
                        {
                            if (matrix[y, x] == 0)
                            {
                                boardX = x - 2;
                                boardY = y - 2;
                                displayX = boardX + 1;
                                displayY = boardY + 1;
                                // LEFT
                                if (matrix[y, x + 1] == 1 && matrix[y, x + 2] == 1)
                                {
                                    possibleMoves.Add(new Move(displayX + 2, displayY, "Left"));
                                    deadEnd = false;
                                }
                                // RIGHT
                                if (matrix[y, x - 1] == 1 && matrix[y, x - 2] == 1)
                                {
                                    possibleMoves.Add(new Move(displayX - 2, displayY, "Right"));
                                    deadEnd = false;
                                }
                                // UP
                                if (matrix[y + 1, x] == 1 && matrix[y + 2, x] == 1)
                                {
                                    possibleMoves.Add(new Move(displayX, displayY + 2, "Up"));
                                    deadEnd = false;
                                }
                                // DOWN
                                if (matrix[y - 1, x] == 1 && matrix[y - 2, x] == 1)
                                {
                                    possibleMoves.Add(new Move(displayX, displayY - 2, "Down"));
                                    deadEnd = false;
                                }
                            }
                        }
                    }

                    full = (i == MAX_MOVES - 1);
                    if (!deadEnd)
                    {
                        selectedMove = possibleMoves[rnd.Next(possibleMoves.Count)];
                        int x;
                        int y;

                        switch (selectedMove.direction)
                        {
                            case "Left":
                                y = selectedMove.y + 1;
                                matrix[y, selectedMove.x + 1] = 0;
                                matrix[y, selectedMove.x] = 0;
                                matrix[y, selectedMove.x - 1] = 1;
                                break;
                            case "Right":
                                y = selectedMove.y + 1;
                                matrix[y, selectedMove.x + 1] = 0;
                                matrix[y, selectedMove.x + 2] = 0;
                                matrix[y, selectedMove.x + 3] = 1;
                                break;
                            case "Up":
                                x = selectedMove.x + 1;
                                matrix[selectedMove.y + 1, x] = 0;
                                matrix[selectedMove.y, x] = 0;
                                matrix[selectedMove.y - 1, x] = 1;
                                break;
                            case "Down":
                                x = selectedMove.x + 1;
                                matrix[selectedMove.y + 1, x] = 0;
                                matrix[selectedMove.y + 2, x] = 0;
                                matrix[selectedMove.y + 3, x] = 1;
                                break;
                            default:
                                break;
                        }

                        result.Add(selectedMove);
                    }
                }
            } while (!full || deadEnd);

            Console.WriteLine("Finished after " + Math.Round((iterations / 1000000.0), 1) + " million iterations.\n");

            Console.WriteLine("X Y DIRECTION");
            Console.WriteLine("-------------");
            Console.WriteLine("4 2 Down");
            Console.WriteLine("6 3 Left");

            for (int i = 0; i < result.Count; i++)
            {
                if (i % 5 == 0)
                {
                    Console.WriteLine();
                }
                Console.WriteLine(result[i].x + " " + result[i].y + " " + result[i].direction);
            }
        }
    }
}
