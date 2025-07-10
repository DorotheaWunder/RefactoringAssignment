namespace PacMan_Refactored;

using System;
using static AsciiData;
using static PlayerManager;
public static class LevelManager
{
	public static char BoardAt(int x, int y) => WallsString[y * 42 + x];

	public static bool IsWall(int x, int y) => BoardAt(x, y) is not ' ';

	public static bool CanMove(int x, int y, Direction direction) => direction switch
	{
		Direction.Up =>
			!IsWall(x - 1, y - 1) &&
			!IsWall(x, y - 1) &&
			!IsWall(x + 1, y - 1),
		Direction.Down =>
			!IsWall(x - 1, y + 1) &&
			!IsWall(x, y + 1) &&
			!IsWall(x + 1, y + 1),
		Direction.Left =>
			x - 2 < 0 || !IsWall(x - 2, y),
		Direction.Right =>
			x + 2 > 40 || !IsWall(x + 2, y),
		_ => throw new NotImplementedException(),
	};
	
}