namespace PacMan_Refactored;

using System;
using static DotManager;
using static LevelManager;
using static GhostManager;
using static ScoreManager;
using static InputManager;
using static MovementStates;
using static GhostTypes;

public static class PlayerManager
{
	public static (int X, int Y) PacManPosition;
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
			EventManager.Broadcast(EventManager.GameEvent.OnDotEaten);
		}
		else if (currentDot == '+')
		{
			foreach (Ghost ghost in ghosts)
			{
				ghost.Weak = true;
				ghost.WeakTime = 0;
			}
			ClearDotAt(PacManPosition.X, PacManPosition.Y);
			EventManager.Broadcast(EventManager.GameEvent.OnSpecialDotEaten);
		}
		
		foreach (Ghost ghost in ghosts)
		{
			if (ghost.Position == PacManPosition)
			{
				if (ghost.Weak)
				{
					ghost.Position = ghost.StartPosition;
					EventManager.Broadcast(EventManager.GameEvent.OnGhostCollision);
				}
				else
				{
					EventManager.Broadcast(EventManager.GameEvent.OnGhostCollision);
				}
			}
		}
		
		if (!CanMove(PacManPosition.X, PacManPosition.Y, CurrentMovementState.Direction))
		{
			CurrentMovementState = null;
		}
	}
}