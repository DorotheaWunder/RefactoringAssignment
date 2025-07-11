namespace PacMan_Refactored;

using System;
using System.Collections.Generic;
using Towel;
using static Towel.Statics;
using static AsciiData;
using static PlayerManager;


public static class GhostManager
{
	public const int GhostWeakTime = 200; 
	public static (int X, int Y)[] Locations = GetLocations();
	public static Ghost[] ghosts;
	
	
	public static void InitializeGhosts()
	{
		ghosts = new Ghost[]
		{
			GhostFactory.CreateGhost(GhostFactory.GhostType.GhostA, (16, 10)),
			GhostFactory.CreateGhost(GhostFactory.GhostType.GhostB, (18, 10)),
			GhostFactory.CreateGhost(GhostFactory.GhostType.GhostC, (22, 10)),
			GhostFactory.CreateGhost(GhostFactory.GhostType.GhostD, (24, 10))
		};
	}
	
	public static Ghost[] GetGhosts() => ghosts;
	
	public static void UpdateGhosts()
	{
		foreach (Ghost ghost in ghosts)
		{
			ghost.Update!();
		}
	}

	public static void SharedUpdateLogic(Ghost ghost)
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
		
		if (ghost.UpdateFrame < ghost.FramesToUpdate)
		{
			ghost.UpdateFrame++;
			return;
		}
		
		Console.SetCursorPosition(ghost.Position.X, ghost.Position.Y);
		Console.Write(' ');
		
		var target = ghost.Weak
			? GetRandomLocation() 
			: ghost.Destination ?? PacManPosition;

		ghost.Position = GetGhostNextMove(ghost.Position, target);
		ghost.UpdateFrame = 0;
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

public abstract class Ghost
{
	public (int X, int Y) StartPosition { get; set; }
	public (int X, int Y) Position { get; set; }
	public bool Weak { get; set; }
	public int WeakTime { get; set; }
	public ConsoleColor Color { get; set; }
	public Action? Update { get; set; }
	public int UpdateFrame { get; set; }
	public int FramesToUpdate { get; set; }
	public (int X, int Y)? Destination { get; set; }
	
	
	public Ghost((int X, int Y) startPosition)
	{
		StartPosition = startPosition;
		Position = startPosition;
		Update = () => UpdateGhost();
	}

	public abstract void UpdateGhost();
}

public static class GhostFactory
{
	public enum GhostType
	{
		GhostA,
		GhostB,
		GhostC,
		GhostD
	}

	public static Ghost CreateGhost(GhostType type, (int X, int Y) startPosition)
	{
		return type switch
		{
			GhostType.GhostA => new GhostA(startPosition),
			GhostType.GhostB => new GhostB(startPosition),
			GhostType.GhostC => new GhostC(startPosition),
			GhostType.GhostD => new GhostD(startPosition),
			_ => throw new ArgumentException("Invalid ghost type"),
		};
	}
}

public class GhostA : Ghost
{
	public GhostA((int X, int Y) startPosition) : base(startPosition)
	{
		Color = ConsoleColor.Red;
		FramesToUpdate = 6;
	}

	public override void UpdateGhost()
	{
		GhostManager.SharedUpdateLogic(this);
	}
}

public class GhostB : Ghost
{
	public GhostB((int X, int Y) startPosition) : base(startPosition)
	{
		Color = ConsoleColor.DarkGreen;
		FramesToUpdate = 6;
		Destination = GhostManager.GetRandomLocation();
	}

	public override void UpdateGhost()
	{
		GhostManager.SharedUpdateLogic(this);
	}
}

public class GhostC : Ghost
{
	public GhostC((int X, int Y) startPosition) : base(startPosition)
	{
		Color = ConsoleColor.Magenta;
		FramesToUpdate = 12;
		Destination = GhostManager.GetRandomLocation();
	}

	public override void UpdateGhost()
	{
		GhostManager.SharedUpdateLogic(this);
	}
}

public class GhostD : Ghost
{
	public GhostD((int X, int Y) startPosition) : base(startPosition)
	{
		Color = ConsoleColor.Cyan;
		FramesToUpdate = 12;
	}

	public override void UpdateGhost()
	{
		GhostManager.SharedUpdateLogic(this);
	}
}