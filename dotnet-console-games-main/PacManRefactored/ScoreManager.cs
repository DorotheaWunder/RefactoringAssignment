namespace PacMan_Refactored;

using static PlayerManager;
using static EventManager;

public static class ScoreManager
{
	public static int score;
	
	public static void Initialize()
	{
		EventManager.OnGameEvent += OnEvent;
	}
	
	private static void OnEvent(EventManager.GameEvent gameEvent)
	{
		switch (gameEvent)
		{
			case EventManager.GameEvent.OnDotEaten:
				Add(1);
				break;
			case EventManager.GameEvent.OnSpecialDotEaten:
				Add(3);
				break;
		}
	}
	
	public static void Add(int points)
	{
		score += points;
	}
	
	public static void Reset()
	{
		score = 0;
	}
	
	public static int GetScore()
	{
		return score;
	}
}
