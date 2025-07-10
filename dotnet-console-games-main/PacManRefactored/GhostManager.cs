namespace PacMan_Refactored;

using System;
using System.Collections.Generic;
using Towel;
using static Towel.Statics;
using static AsciiData;
using static PlayerManager;

public static class GhostManager
{
	public class Ghost
	{
		public (int X, int Y) StartPosition;
		public (int X, int Y) Position;
		public bool Weak;
		public int WeakTime;
		public ConsoleColor Color;
		public Action? Update;
		public int UpdateFrame;
		public int FramesToUpdate;
		public (int X, int Y)? Destination;
	}
	
	public const int GhostWeakTime = 200; 
	public static (int X, int Y)[] Locations = GetLocations();
	public static Ghost[] ghosts;

	public static void InitializeGhosts()
	{
		Ghost a = new();
		a.Position = a.StartPosition = (16, 10);
		a.Color = ConsoleColor.Red;
		a.FramesToUpdate = 6;
		a.Update = () => UpdateGhost(a);

		Ghost b = new();
		b.Position = b.StartPosition = (18, 10);
		b.Color = ConsoleColor.DarkGreen;
		b.Destination = GetRandomLocation();
		b.FramesToUpdate = 6;
		b.Update = () => UpdateGhost(b);

		Ghost c = new();
		c.Position = c.StartPosition = (22, 10);
		c.Color = ConsoleColor.Magenta;
		c.FramesToUpdate = 12;
		c.Update = () => UpdateGhost(c);

		Ghost d = new();
		d.Position = d.StartPosition = (24, 10);
		d.Color = ConsoleColor.DarkCyan;
		d.Destination = GetRandomLocation();
		d.FramesToUpdate = 12;
		d.Update = () => UpdateGhost(d);

		ghosts = new[] { a, b, c, d };
	}
	
	public static Ghost[] GetGhosts() => ghosts;
	
	public static void UpdateGhosts()
	{
		foreach (Ghost ghost in ghosts)
		{
			ghost.Update!();
		}
	}

	public static void UpdateGhost(Ghost ghost)
	{
		if (ghost.Destination.HasValue && ghost.Destination == ghost.Position)
		{
			ghost.Destination = GetRandomLocation();
		}
		if (ghost.Weak)
		{
			ghost.WeakTime++;
			if (ghost.WeakTime > GhostWeakTime)
			{
				ghost.Weak = false;
			}
		}
		else if (ghost.UpdateFrame < ghost.FramesToUpdate)
		{
			ghost.UpdateFrame++;
		}
		else
		{
			Console.SetCursorPosition(ghost.Position.X, ghost.Position.Y);
			Console.Write(' ');
			ghost.Position = GetGhostNextMove(ghost.Position, ghost.Destination ?? PacManPosition);
			ghost.UpdateFrame = 0;
		}
	}

	public static (int X, int Y)[] GetLocations()
	{
		List<(int X, int Y)> list = new();
		int x = 0;
		int y = 0;
		foreach (char c in GhostWallsString)
		{
			if (c is '\n')
			{
				x = 0;
				y++;
			}
			else
			{
				if (c is ' ')
				{
					list.Add((x, y));
				}
				x++;
			}
		}
		return list.ToArray();
	}

	public static (int X, int Y) GetRandomLocation() => Random.Shared.Choose(Locations);

	public static (int X, int Y) GetGhostNextMove((int X, int Y) position, (int X, int Y) destination)
	{
		HashSet<(int X, int Y)> alreadyUsed = new();

		char BoardAt(int x, int y) => GhostWallsString[y * 42 + x];

		bool IsWall(int x, int y) => BoardAt(x, y) is not ' ';

		void Neighbors((int X, int Y) currentLocation, Action<(int X, int Y)> neighbors)
		{
			void HandleNeighbor(int x, int y)
			{
				if (!alreadyUsed.Contains((x, y)) && x >= 0 && x <= 40 && !IsWall(x, y))
				{
					alreadyUsed.Add((x, y));
					neighbors((x, y));
				}
			}

			int x = currentLocation.X;
			int y = currentLocation.Y;
			HandleNeighbor(x - 1, y); // left
			HandleNeighbor(x, y + 1); // up
			HandleNeighbor(x + 1, y); // right
			HandleNeighbor(x, y - 1); // down
		}

		int Heuristic((int X, int Y) node)
		{
			int x = node.X - PacManPosition.X;
			int y = node.Y - PacManPosition.Y;
			return x * x + y * y;
		}

		Action<Action<(int X, int Y)>> path = SearchGraph(position, Neighbors, Heuristic, node => node == destination)!;
		(int X, int Y)[] array = path.ToArray();
		return array[1];
	}
}