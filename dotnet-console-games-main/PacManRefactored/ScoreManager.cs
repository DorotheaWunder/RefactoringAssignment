namespace PacMan_Refactored;
public static class ScoreManager
{
	public static int score;
	
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
