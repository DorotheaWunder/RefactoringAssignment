namespace PacMan_Refactored;

using System;

public class GhostTypes
{
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
}