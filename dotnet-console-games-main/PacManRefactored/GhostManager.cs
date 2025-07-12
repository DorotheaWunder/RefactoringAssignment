namespace PacMan_Refactored;

using System;
using System.Collections.Generic;
using Towel;
using static Towel.Statics;
using static AsciiData;
using static PlayerManager;
using static GhostTypes;


public static class GhostManager
{
	public const int GhostWeakTime = 200; 
	public static (int X, int Y)[] Locations = GetLocations();
	public static Ghost[] ghosts;
	
	public static void Initialize()
	{
		EventManager.OnGameEvent += OnEvent;
	}
	
	private static void OnEvent(EventManager.GameEvent gameEvent)
	{
		if (gameEvent == EventManager.GameEvent.OnGhostCollision)
		{
			Console.SetCursorPosition(0, 24);
		}
	}
	
	
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
	
	public static GhostTypes.Ghost[] GetGhosts() => ghosts;
	
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
			HandleNeighbor(x - 1, y);
			HandleNeighbor(x, y + 1);
			HandleNeighbor(x + 1, y);
			HandleNeighbor(x, y - 1);
		}

		int Heuristic((int X, int Y) node)
		{
			int x = node.X - PacManPosition.X;
			int y = node.Y - PacManPosition.Y;
			return x * x + y * y;
		}

		Action<Action<(int X, int Y)>> path = SearchGraph(position, Neighbors, Heuristic, node => node == destination)!;
		(int X, int Y)[] array = path.ToArray();
		
		if (array.Length < 2)
			return position;
		
		return array[1];
	}
}