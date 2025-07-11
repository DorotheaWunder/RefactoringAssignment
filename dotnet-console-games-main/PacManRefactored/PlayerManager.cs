namespace PacMan_Refactored;

using System;
using static DotManager;
using static LevelManager;
using static GhostManager;
using static ScoreManager;
using static InputManager;

public static class PlayerManager
{
	public static (int X, int Y) PacManPosition;
	//public static Direction? PacManMovingDirection = default;
	public static IMovementState CurrentMovementState = null;
	public static int? PacManMovingFrame = default;
	
	const int FramesToMoveHorizontal = 6;
	const int FramesToMoveVertical = 6;
	
	public enum Direction
	{
		Up = 0,
		Down = 1,
		Left = 2,
		Right = 3,
	}
	
	public static void UpdatePacMan()
	{
		CurrentMovementState?.Move();
		
		if (CurrentMovementState != null &&
		    !CanMove(PacManPosition.X, PacManPosition.Y, CurrentMovementState.Direction))
		{
			CurrentMovementState = null;
		}
	}
	
	
	public static bool GetStartingDirectionInput()
	{
		return WaitForFirstDirection() == null;
	}
	
	public static bool HandleInput() => InputManager.HandleInput();

	public static void TrySetPacManDirection(Direction direction)
	{
		if (CurrentMovementState != null && CurrentMovementState.Direction == direction)
			return;
		
		if (CanMove(PacManPosition.X, PacManPosition.Y, direction))
		{
			CurrentMovementState = direction switch
			{
				Direction.Up => new MoveUpState(),
				Direction.Down => new MoveDownState(),
				Direction.Left => new MoveLeftState(),
				Direction.Right => new MoveRightState(),
				_ => CurrentMovementState
			};

			PacManMovingFrame = 0;
		}
	}
	
	public static void HandleCollision()
	{
		char currentDot = GetDotAt(PacManPosition.X, PacManPosition.Y);

		if (currentDot == '·')
		{
			ClearDotAt(PacManPosition.X, PacManPosition.Y);
			Add(1);
		}
		else if (currentDot == '+')
		{
			foreach (Ghost ghost in ghosts)
			{
				ghost.Weak = true;
				ghost.WeakTime = 0;
			}
			ClearDotAt(PacManPosition.X, PacManPosition.Y);
			Add(3);
		}

		if (!CanMove(PacManPosition.X, PacManPosition.Y, CurrentMovementState.Direction))
		{
			CurrentMovementState = null;
		}
	}
}

//-------------------------------------------------------- Movement State Directions Setup
public interface IMovementState
{
	void Move();
	PlayerManager.Direction Direction { get; }
}

public class MoveLeftState : IMovementState
{
	public PlayerManager.Direction Direction => PlayerManager.Direction.Left;

	public void Move()
	{
		if (PlayerManager.PacManMovingFrame >= 6)
		{
			PlayerManager.PacManMovingFrame = 0;
			Console.SetCursorPosition(PlayerManager.PacManPosition.X, PlayerManager.PacManPosition.Y);
			Console.Write(" ");
			PlayerManager.PacManPosition = (PlayerManager.PacManPosition.X - 1, PlayerManager.PacManPosition.Y);
			if (PlayerManager.PacManPosition.X < 0)
				PlayerManager.PacManPosition.X = 40;

			PlayerManager.HandleCollision();
		}
		else
		{
			PlayerManager.PacManMovingFrame++;
		}
	}
}

public class MoveRightState : IMovementState
{
	public PlayerManager.Direction Direction => PlayerManager.Direction.Right;
	
	public void Move()
	{
		if (PlayerManager.PacManMovingFrame >= 6)
		{
			PlayerManager.PacManMovingFrame = 0;
			Console.SetCursorPosition(PlayerManager.PacManPosition.X, PlayerManager.PacManPosition.Y);
			Console.Write(" ");
			PlayerManager.PacManPosition = (PlayerManager.PacManPosition.X + 1, PlayerManager.PacManPosition.Y);

			if (PlayerManager.PacManPosition.X > 40)
				PlayerManager.PacManPosition.X = 0;

			PlayerManager.HandleCollision();
		}
		else
		{
			PlayerManager.PacManMovingFrame++;
		}
	}
}

public class MoveUpState : IMovementState
{
	public PlayerManager.Direction Direction => PlayerManager.Direction.Up;
	
	public void Move()
	{
		if (PlayerManager.PacManMovingFrame >= 6)
		{
			PlayerManager.PacManMovingFrame = 0;
			Console.SetCursorPosition(PlayerManager.PacManPosition.X, PlayerManager.PacManPosition.Y);
			Console.Write(" ");
			PlayerManager.PacManPosition = (PlayerManager.PacManPosition.X, PlayerManager.PacManPosition.Y - 1);

			PlayerManager.HandleCollision();
		}
		else
		{
			PlayerManager.PacManMovingFrame++;
		}
	}
}

public class MoveDownState : IMovementState
{
	public PlayerManager.Direction Direction => PlayerManager.Direction.Down;
	
	public void Move()
	{
		if (PlayerManager.PacManMovingFrame >= 6)
		{
			PlayerManager.PacManMovingFrame = 0;
			Console.SetCursorPosition(PlayerManager.PacManPosition.X, PlayerManager.PacManPosition.Y);
			Console.Write(" ");
			PlayerManager.PacManPosition = (PlayerManager.PacManPosition.X, PlayerManager.PacManPosition.Y + 1);

			PlayerManager.HandleCollision();
		}
		else
		{
			PlayerManager.PacManMovingFrame++;
		}
	}
}