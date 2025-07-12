namespace PacMan_Refactored;

using System;

public class MovementStates
{
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
}