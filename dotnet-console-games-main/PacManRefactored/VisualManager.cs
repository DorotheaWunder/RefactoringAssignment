namespace PacMan_Refactored;
using System;
using static GhostManager;
using static PlayerManager;
using static GhostTypes;

public static class VisualManager
{
	public static void RenderReady()
	{
		Console.SetCursorPosition(18, 13);
		WithColors(ConsoleColor.White, ConsoleColor.Black, () =>
		{
			Console.Write("READY");
		});
	}

	public static void EraseReady()
	{
		Console.SetCursorPosition(18, 13);
		Console.Write("     ");
	}

	public static void RenderScore(int score)
	{
		Console.SetCursorPosition(0, 23);
		Console.Write("Score: " + score);
	}

	public static void RenderGate()
	{
		Console.SetCursorPosition(19, 9);
		WithColors(ConsoleColor.Magenta, ConsoleColor.Black, () =>
		{
			Console.Write("---");
		});
	}

	public static void RenderWalls(string wallsString)
	{
		Console.SetCursorPosition(0, 0);
		WithColors(ConsoleColor.Blue, ConsoleColor.Black, () =>
		{
			Render(AsciiData.WallsString, false);
		});
	}

	public static void RenderDots(char[,] dots)
	{
		Console.SetCursorPosition(0, 0);
		WithColors(ConsoleColor.DarkYellow, ConsoleColor.Black, () =>
		{
			for (int row = 0; row < dots.GetLength(1); row++)
			{
				for (int column = 0; column < dots.GetLength(0); column++)
				{
					if (!char.IsWhiteSpace(dots[column, row]))
					{
						Console.SetCursorPosition(column, row);
						Console.Write(dots[column, row]);
					}
				}
			}
		});
	}

	
	public static void RenderPacMan(
		(int X, int Y) position,
		Direction? movingDirection,
		int? movingFrame,
		string[] animations)
	{
		Console.SetCursorPosition(position.X, position.Y);
		WithColors(ConsoleColor.Black, ConsoleColor.Yellow, () =>
		{
			if (movingDirection.HasValue && movingFrame.HasValue)
			{
				int frame = (int)movingFrame % animations[(int)movingDirection].Length;
				Console.Write(animations[(int)movingDirection][frame]);
			}
			else
			{
				Console.Write(' ');
			}
		});
	}

	public static void RenderGhosts(Ghost[] ghosts)
	{
		foreach (Ghost ghost in ghosts)
		{
			Console.SetCursorPosition(ghost.Position.X, ghost.Position.Y);
			WithColors(ConsoleColor.White, ghost.Weak ? ConsoleColor.Blue : ghost.Color, () => Console.Write('"'));
		}
	}

	public static void WithColors(ConsoleColor foreground, ConsoleColor background, Action action)
	{
		ConsoleColor originalForeground = Console.ForegroundColor;
		ConsoleColor originalBackground = Console.BackgroundColor;
		try
		{
			Console.ForegroundColor = foreground;
			Console.BackgroundColor = background;
			action();
		}
		finally
		{
			Console.ForegroundColor = originalForeground;
			Console.BackgroundColor = originalBackground;
		}
	}

	public static void Render(string @string, bool renderSpace = true)
	{
		int x = Console.CursorLeft;
		int y = Console.CursorTop;
		foreach (char c in @string)
		{
			if (c is '\n')
			{
				Console.SetCursorPosition(x, ++y);
			}
			else if (c is not ' ' || renderSpace)
			{
				Console.Write(c);
			}
			else
			{
				Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
			}
		}
	}
}

//--------------------- have combo method that renders everything