using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Threading;
using Avalonia.Controls.Shapes;

namespace PacMan;

public partial class MainWindow : Window
{
    private const int CellSize = 28;

    private int[,] map = new int[1, 1];

    private int pacRow;
    private int pacCol;

    private int score;
    private int lives = 3;
    private int level = 1;

    private readonly DispatcherTimer gameTimer;
    private readonly Random random = new Random();
    private readonly List<Ghost> ghosts = new();

    public MainWindow()
    {
        InitializeComponent();

        LoadLevel();

        gameTimer = new DispatcherTimer();
        gameTimer.Interval = TimeSpan.FromMilliseconds(350);
        gameTimer.Tick += GameLoop;
        gameTimer.Start();

        Opened += (_, _) => Focus();
    }

    private void KeyPressed(object? sender, KeyEventArgs e)
    {
        int rowMove = 0;
        int colMove = 0;

        if (e.Key == Key.Up || e.Key == Key.W)
            rowMove = -1;

        if (e.Key == Key.Down || e.Key == Key.S)
            rowMove = 1;

        if (e.Key == Key.Left || e.Key == Key.A)
            colMove = -1;

        if (e.Key == Key.Right || e.Key == Key.D)
            colMove = 1;

        if (rowMove != 0 || colMove != 0)
            MovePacman(rowMove, colMove);
    }

    private void MovePacman(int rowMove, int colMove)
    {
        int newRow = pacRow + rowMove;
        int newCol = pacCol + colMove;

        if (!CanMove(newRow, newCol))
            return;

        pacRow = newRow;
        pacCol = newCol;

        if (map[pacRow, pacCol] == 2)
        {
            map[pacRow, pacCol] = 0;
            score += 10;
        }

        CheckCollision();

        if (lives <= 0)
            return;

        if (AllPointsCollected())
        {
            level++;

            if (level > 3)
            {
                gameTimer.Stop();
                InfoText.Text = "Du hast alle 3 Levels geschafft!";
                DrawGame();
                UpdateText();
                return;
            }

            LoadLevel();
            InfoText.Text = "Level " + level;
        }

        DrawGame();
        UpdateText();
    }

    private void GameLoop(object? sender, EventArgs e)
    {
        foreach (Ghost ghost in ghosts)
        {
            MoveGhost(ghost);
        }

        CheckCollision();
        DrawGame();
        UpdateText();
    }

    private void MoveGhost(Ghost ghost)
    {
        List<(int row, int col)> possibleMoves = new();

        int[,] directions =
        {
            { -1, 0 },
            { 1, 0 },
            { 0, -1 },
            { 0, 1 }
        };

        for (int i = 0; i < 4; i++)
        {
            int rowDirection = directions[i, 0];
            int colDirection = directions[i, 1];

            int newRow = ghost.Row + rowDirection;
            int newCol = ghost.Col + colDirection;

            if (!CanMove(newRow, newCol))
                continue;

            // Wenn möglich, nicht direkt wieder umdrehen.
            if (rowDirection == -ghost.RowDirection &&
                colDirection == -ghost.ColDirection &&
                possibleMoves.Count > 0)
            {
                continue;
            }

            possibleMoves.Add((rowDirection, colDirection));
        }

        // Falls nur der Rückweg möglich ist.
        if (possibleMoves.Count == 0)
        {
            int backRow = ghost.Row - ghost.RowDirection;
            int backCol = ghost.Col - ghost.ColDirection;

            if (CanMove(backRow, backCol))
            {
                ghost.RowDirection *= -1;
                ghost.ColDirection *= -1;
            }
            else
            {
                return;
            }
        }
        else
        {
            var direction = possibleMoves[random.Next(possibleMoves.Count)];
            ghost.RowDirection = direction.row;
            ghost.ColDirection = direction.col;
        }

        ghost.Row += ghost.RowDirection;
        ghost.Col += ghost.ColDirection;
    }

    private void CheckCollision()
    {
        foreach (Ghost ghost in ghosts)
        {
            if (ghost.Row == pacRow && ghost.Col == pacCol)
            {
                lives--;

                if (lives <= 0)
                {
                    lives = 0;
                    gameTimer.Stop();
                    InfoText.Text = "Game Over";
                    return;
                }

                ResetPositions();
                InfoText.Text = "Du wurdest erwischt!";
                return;
            }
        }
    }

    private bool CanMove(int row, int col)
    {
        if (row < 0 || col < 0)
            return false;

        if (row >= map.GetLength(0) || col >= map.GetLength(1))
            return false;

        return map[row, col] != 1;
    }

    private bool AllPointsCollected()
    {
        foreach (int field in map)
        {
            if (field == 2)
                return false;
        }

        return true;
    }

    private void LoadLevel()
    {
        if (level == 1)
        {
            map = CreateMap(new[]
            {
                "111111111111111111111",
                "122222222212222222221",
                "121111121212121111121",
                "122222121222121222221",
                "121121121111121121121",
                "122122222222222212221",
                "112121111211111212111",
                "122222222222222222221",
                "121111211111112111121",
                "122222212222212222221",
                "121121212111212112121",
                "122121222212222212221",
                "121111111212111111121",
                "122222222222222222221",
                "111111111111111111111"
            });
        }
        else if (level == 2)
        {
            map = CreateMap(new[]
            {
                "111111111111111111111",
                "122222222212222222221",
                "121111122212121111121",
                "122222121222121222221",
                "121121122222121121121",
                "122122222222222212221",
                "112122222212222212111",
                "122222222222222222221",
                "121111222222222111121",
                "122222212222212222221",
                "121121212111212112121",
                "122121222212222212221",
                "121111111212111111121",
                "122222222222222222221",
                "111111111111111111111"
            });
        }
        else
        {
            map = CreateMap(new[]
            {
                "111111111111111111111",
                "122222122222222122221",
                "121112121111121211121",
                "122212122222221212221",
                "112121111211111212111",
                "122122222212222212221",
                "121111121212121111121",
                "122222121222121222221",
                "121121111211111121121",
                "121222222222222222121",
                "121211111212111112121",
                "122212222212222212221",
                "121111121212121111121",
                "122222222222222222221",
                "111111111111111111111"
            });
        }

        ResetPositions();
        DrawGame();
        UpdateText();
    }

    private void ResetPositions()
    {
        pacRow = 13;
        pacCol = 1;

        ghosts.Clear();

        ghosts.Add(new Ghost(1, 19, 0, -1));
        ghosts.Add(new Ghost(7, 10, 0, 1));
        ghosts.Add(new Ghost(13, 19, 0, -1));
    }

    private int[,] CreateMap(string[] rows)
    {
        int[,] newMap = new int[rows.Length, rows[0].Length];

        for (int row = 0; row < rows.Length; row++)
        {
            for (int col = 0; col < rows[row].Length; col++)
            {
                newMap[row, col] = rows[row][col] - '0';
            }
        }

        return newMap;
    }

    private void DrawGame()
    {
        GameCanvas.Children.Clear();

        for (int row = 0; row < map.GetLength(0); row++)
        {
            for (int col = 0; col < map.GetLength(1); col++)
            {
                if (map[row, col] == 1)
                {
                    Rectangle wall = new Rectangle
                    {
                        Width = CellSize - 2,
                        Height = CellSize - 2,
                        Fill = Brushes.DarkBlue
                    };

                    Canvas.SetLeft(wall, col * CellSize + 1);
                    Canvas.SetTop(wall, row * CellSize + 1);
                    GameCanvas.Children.Add(wall);
                }

                if (map[row, col] == 2)
                {
                    Ellipse point = new Ellipse
                    {
                        Width = 6,
                        Height = 6,
                        Fill = Brushes.White
                    };

                    Canvas.SetLeft(point, col * CellSize + 11);
                    Canvas.SetTop(point, row * CellSize + 11);
                    GameCanvas.Children.Add(point);
                }
            }
        }

        Ellipse pacman = new Ellipse
        {
            Width = 22,
            Height = 22,
            Fill = Brushes.Yellow
        };

        Canvas.SetLeft(pacman, pacCol * CellSize + 3);
        Canvas.SetTop(pacman, pacRow * CellSize + 3);
        GameCanvas.Children.Add(pacman);

        IBrush[] ghostColors =
        {
            Brushes.Red,
            Brushes.Pink,
            Brushes.Cyan
        };

        for (int i = 0; i < ghosts.Count; i++)
        {
            Rectangle ghostShape = new Rectangle
            {
                Width = 20,
                Height = 20,
                Fill = ghostColors[i]
            };

            Canvas.SetLeft(ghostShape, ghosts[i].Col * CellSize + 4);
            Canvas.SetTop(ghostShape, ghosts[i].Row * CellSize + 4);
            GameCanvas.Children.Add(ghostShape);
        }
    }

    private void UpdateText()
    {
        ScoreText.Text = "Punkte: " + score;
        LivesText.Text = "Leben: " + lives;
        LevelText.Text = "Level: " + level;
    }
}

public class Ghost
{
    public int Row;
    public int Col;
    public int RowDirection;
    public int ColDirection;

    public Ghost(int row, int col, int rowDirection, int colDirection)
    {
        Row = row;
        Col = col;
        RowDirection = rowDirection;
        ColDirection = colDirection;
    }
}
