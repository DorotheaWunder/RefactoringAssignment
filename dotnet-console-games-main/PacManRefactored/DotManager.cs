namespace PacMan_Refactored;

public static class DotManager
{
	public static char[,] Dots;

	public static void SetUpDots(string dotString)
	{
		string[] rows = dotString.Split("\n");
		int rowCount = rows.Length;
		int columnCount = rows[0].Length;

		Dots = new char[columnCount, rowCount];

		for (int row = 0; row < rowCount; row++)
		{
			for (int column = 0; column < columnCount; column++)
			{
				Dots[column, row] = rows[row][column];
			}
		}
	}

	public static int CountDots()
	{
		int count = 0;
		int columnCount = Dots.GetLength(0);
		int rowCount = Dots.GetLength(1);

		for (int row = 0; row < rowCount; row++)
		{
			for (int column = 0; column < columnCount; column++)
			{
				if (!char.IsWhiteSpace(Dots[column, row]))
				{
					count++;
				}
			}
		}

		return count;
	}

	public static char[,] GetDots() => Dots;
	public static void ClearDotAt(int x, int y) => Dots[x, y] = ' ';
	public static char GetDotAt(int x, int y) => Dots[x, y];
}

//--------------------- have combo method that initializes all dots