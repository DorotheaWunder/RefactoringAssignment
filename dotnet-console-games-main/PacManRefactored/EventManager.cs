namespace PacMan_Refactored;

using System;

public class EventManager
{
	public enum GameEvent
	{
		OnDotEaten,
		OnSpecialDotEaten,
		OnGhostCollision
	}

	public static event Action<GameEvent> OnGameEvent;

	public static void Broadcast(GameEvent gameEvent)
	{
		OnGameEvent?.Invoke(gameEvent);
	}
}