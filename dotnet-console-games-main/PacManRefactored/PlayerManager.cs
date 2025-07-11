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
	public static Direction? PacManMovingDirection = default;
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
		if (PacManMovingDirection.HasValue)
		{
			if ((PacManMovingDirection == Direction.Left || PacManMovingDirection == Direction.Right) && PacManMovingFrame >= FramesToMoveHorizontal ||
			    (PacManMovingDirection == Direction.Up || PacManMovingDirection == Direction.Down) && PacManMovingFrame >= FramesToMoveVertical)
			{
				PacManMovingFrame = 0;
				int x_adjust =
					PacManMovingDirection == Direction.Left ? -1 :
					PacManMovingDirection == Direction.Right ? 1 :
					0;
				int y_adjust =
					PacManMovingDirection == Direction.Up ? -1 :
					PacManMovingDirection == Direction.Down ? 1 :
					0;

				Console.SetCursorPosition(PacManPosition.X, PacManPosition.Y);
				Console.Write(" ");
				PacManPosition = (PacManPosition.X + x_adjust, PacManPosition.Y + y_adjust);

				if (PacManPosition.X < 0)
				{
					PacManPosition.X = 40;
				}
				else if (PacManPosition.X > 40)
				{
					PacManPosition.X = 0;
				}

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

				if (!CanMove(PacManPosition.X, PacManPosition.Y, PacManMovingDirection.Value))
				{
					PacManMovingDirection = null;
				}
			}
			else
			{
				PacManMovingFrame++;
			}
		}
	}
	
	
	public static bool GetStartingDirectionInput()
	{
		return WaitForFirstDirection() == null;
	}
	
	public static bool HandleInput() => InputManager.HandleInput();

	public static void TrySetPacManDirection(Direction direction)
	{
		if (PacManMovingDirection != direction &&
		    CanMove(PacManPosition.X, PacManPosition.Y, direction))
		{
			PacManMovingDirection = direction;
			PacManMovingFrame = 0;
		}
	}
}