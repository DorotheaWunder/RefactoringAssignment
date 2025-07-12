using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Towel;
using static Towel.Statics;
using PacMan_Refactored;
using static PacMan_Refactored.AsciiData;
using static PacMan_Refactored.VisualManager;
using static PacMan_Refactored.DotManager;
using static PacMan_Refactored.ScoreManager;
using static PacMan_Refactored.GhostManager;
using static PacMan_Refactored.PlayerManager;
using static PacMan_Refactored.LevelManager;
using static PacMan_Refactored.ScoreManager;
using static PacMan_Refactored.GhostTypes;
//--------------------------------------------------------------------

int OriginalWindowWidth = Console.WindowWidth;
int OriginalWindowHeight = Console.WindowHeight;
ConsoleColor OriginalBackgroundColor = Console.BackgroundColor;
ConsoleColor OriginalForegroundColor = Console.ForegroundColor;
//--------------------------------------------------------------------

int Score = 0;

GhostManager.InitializeGhosts();

Ghost[] ghosts = GhostManager.GetGhosts();

Console.Clear();
try
{
	if (OperatingSystem.IsWindows())
	{
		Console.WindowWidth = 50;
		Console.WindowHeight = 30;
	}
	Console.CursorVisible = false;
	Console.BackgroundColor = ConsoleColor.Black;
	Console.ForegroundColor = ConsoleColor.White;
	ScoreManager.Reset();
NextRound:
	Console.Clear();
	SetUpDots(DotsString);
	PacManPosition = (20, 17);
//---------------------------------------------- put into game manager?
	
	
	//--------------------------------------------------------------------
	RenderWalls(WallsString);
	RenderGate();
	RenderDots(GetDots());
	RenderReady();
	RenderPacMan(PacManPosition,  CurrentMovementState?.Direction, PacManMovingFrame, PacManAnimations);
	RenderGhosts(ghosts);
	RenderScore(Score);
	if (GetStartingDirectionInput())
	{
		return; // user hit escape
	}
	PacManMovingFrame = 0;
	EraseReady();
	while (CountDots() > 0)
	{
		if (HandleInput())
		{
			return; // user hit escape
		}
		UpdatePacMan();
		UpdateGhosts();
		RenderScore(Score);
		RenderDots(GetDots());
		RenderPacMan(PacManPosition,  CurrentMovementState?.Direction, PacManMovingFrame, PacManAnimations);
		RenderGhosts(ghosts);
		foreach (Ghost ghost in ghosts)
		{
			if (ghost.Position == PacManPosition)
			{
				if (ghost.Weak)
				{
					ghost.Position = ghost.StartPosition;
					ghost.Weak = false;
					Add(10);
				}
				else
				{
					Console.SetCursorPosition(0, 24);
					Console.WriteLine("Game Over!");
					Console.WriteLine("Play Again [enter], or quit [escape]?");
				GetInput:
					switch (Console.ReadKey(true).Key)
					{
						case ConsoleKey.Enter: goto NextRound;
						case ConsoleKey.Escape: Console.Clear(); return;
						default: goto GetInput;
					}
				}
			}
		}
		Thread.Sleep(TimeSpan.FromMilliseconds(40));
	}
	goto NextRound;
}
finally
{
	Console.CursorVisible = true;
	if (OperatingSystem.IsWindows())
	{
		Console.WindowWidth = OriginalWindowWidth;
		Console.WindowHeight = OriginalWindowHeight;
	}
	Console.BackgroundColor = OriginalBackgroundColor;
	Console.ForegroundColor = OriginalForegroundColor;
}