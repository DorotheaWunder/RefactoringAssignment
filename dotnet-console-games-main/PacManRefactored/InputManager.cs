namespace PacMan_Refactored;


using System;
using System.Collections.Generic;
using static PlayerManager;

public static class InputManager
{
	private static readonly Dictionary<ConsoleKey, IInputCommand> keyBindings = new()
	{
		{ ConsoleKey.UpArrow, new MoveCommand(Direction.Up) },
		{ ConsoleKey.DownArrow, new MoveCommand(Direction.Down) },
		{ ConsoleKey.LeftArrow, new MoveCommand(Direction.Left) },
		{ ConsoleKey.RightArrow, new MoveCommand(Direction.Right) }
	};
	
	public static bool HandleInput()
	{
		while (Console.KeyAvailable)
		{
			var key = Console.ReadKey(true).Key;

			if (key == ConsoleKey.Escape)
			{
				Console.Clear();
				Console.Write("PacMan was closed.");
				return true;
			}

			if (keyBindings.TryGetValue(key, out var command))
			{
				command.Execute();
			}
		}

		return false;
	}
	
	public static Direction? WaitForFirstDirection()
	{
		while (true)
		{
			var key = Console.ReadKey(true).Key;
			if (keyBindings.TryGetValue(key, out var command))
			{
				command.Execute();
				return PacManMovingDirection;
			}
			else if (key == ConsoleKey.Escape)
			{
				Console.Clear();
				Console.Write("PacMan was closed.");
				return null;
			}
		}
	}
}

public interface IInputCommand
{
	void Execute();
}
	
public class MoveCommand : IInputCommand
{
	private readonly Direction direction;

	public MoveCommand(Direction direction)
	{
		this.direction = direction;
	}

	public void Execute()
	{
		TrySetPacManDirection(direction);
	}
}