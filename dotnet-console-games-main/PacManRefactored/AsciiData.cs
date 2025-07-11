﻿namespace PacMan_Refactored;

public static class AsciiData
{
	#region Ascii

// ╔═══════════════════╦═══════════════════╗
// ║ · · · · · · · · · ║ · · · · · · · · · ║
// ║ · ╔═╗ · ╔═════╗ · ║ · ╔═════╗ · ╔═╗ · ║
// ║ + ╚═╝ · ╚═════╝ · ╨ · ╚═════╝ · ╚═╝ + ║
// ║ · · · · · · · · · · · · · · · · · · · ║
// ║ · ═══ · ╥ · ══════╦══════ · ╥ · ═══ · ║
// ║ · · · · ║ · · · · ║ · · · · ║ · · · · ║
// ╚═════╗ · ╠══════   ╨   ══════╣ · ╔═════╝
//       ║ · ║                   ║ · ║
// ══════╝ · ╨   ╔════---════╗   ╨ · ╚══════
//         ·     ║ █ █   █ █ ║     ·        
// ══════╗ · ╥   ║           ║   ╥ · ╔══════
//       ║ · ║   ╚═══════════╝   ║ · ║
//       ║ · ║       READY       ║ · ║
// ╔═════╝ · ╨   ══════╦══════   ╨ · ╚═════╗
// ║ · · · · · · · · · ║ · · · · · · · · · ║
// ║ · ══╗ · ═══════ · ╨ · ═══════ · ╔══ · ║
// ║ + · ║ · · · · · · █ · · · · · · ║ · + ║
// ╠══ · ╨ · ╥ · ══════╦══════ · ╥ · ╨ · ══╣
// ║ · · · · ║ · · · · ║ · · · · ║ · · · · ║
// ║ · ══════╩══════ · ╨ · ══════╩══════ · ║
// ║ · · · · · · · · · · · · · · · · · · · ║
// ╚═══════════════════════════════════════╝

	public static readonly string WallsString =
	"╔═══════════════════╦═══════════════════╗\n" +
	"║                   ║                   ║\n" +
	"║   ╔═╗   ╔═════╗   ║   ╔═════╗   ╔═╗   ║\n" +
	"║   ╚═╝   ╚═════╝   ╨   ╚═════╝   ╚═╝   ║\n" +
	"║                                       ║\n" +
	"║   ═══   ╥   ══════╦══════   ╥   ═══   ║\n" +
	"║         ║         ║         ║         ║\n" +
	"╚═════╗   ╠══════   ╨   ══════╣   ╔═════╝\n" +
	"      ║   ║                   ║   ║      \n" +
	"══════╝   ╨   ╔════   ════╗   ╨   ╚══════\n" +
	"              ║           ║              \n" +
	"══════╗   ╥   ║           ║   ╥   ╔══════\n" +
	"      ║   ║   ╚═══════════╝   ║   ║      \n" +
	"      ║   ║                   ║   ║      \n" +
	"╔═════╝   ╨   ══════╦══════   ╨   ╚═════╗\n" +
	"║                   ║                   ║\n" +
	"║   ══╗   ═══════   ╨   ═══════   ╔══   ║\n" +
	"║     ║                           ║     ║\n" +
	"╠══   ╨   ╥   ══════╦══════   ╥   ╨   ══╣\n" +
	"║         ║         ║         ║         ║\n" +
	"║   ══════╩══════   ╨   ══════╩══════   ║\n" +
	"║                                       ║\n" +
	"╚═══════════════════════════════════════╝";

	public static readonly string GhostWallsString =
	"╔═══════════════════╦═══════════════════╗\n" +
	"║█                 █║█                 █║\n" +
	"║█ █╔═╗█ █╔═════╗█ █║█ █╔═════╗█ █╔═╗█ █║\n" +
	"║█ █╚═╝█ █╚═════╝█ █╨█ █╚═════╝█ █╚═╝█ █║\n" +
	"║█                                     █║\n" +
	"║█ █═══█ █╥█ █══════╦══════█ █╥█ █═══█ █║\n" +
	"║█       █║█       █║█       █║█       █║\n" +
	"╚═════╗█ █╠══════█ █╨█ █══════╣█ █╔═════╝\n" +
	"██████║█ █║█                 █║█ █║██████\n" +
	"══════╝█ █╨█ █╔════█ █════╗█ █╨█ █╚══════\n" +
	"             █║█         █║█             \n" +
	"══════╗█  ╥█ █║███████████║█ █╥█ █╔══════\n" +
	"██████║█  ║█ █╚═══════════╝█ █║█ █║██████\n" +
	"██████║█  ║█                 █║█ █║██████\n" +
	"╔═════╝█  ╨█ █══════╦══════█ █╨█ █╚═════╗\n" +
	"║█                 █║█                 █║\n" +
	"║█ █══╗█ █═══════█ █╨█ █═══════█ █╔══█ █║\n" +
	"║█   █║█                         █║█   █║\n" +
	"╠══█ █╨█ █╥█ █══════╦══════█ █╥█ █╨█ █══╣\n" +
	"║█       █║█       █║█       █║█       █║\n" +
	"║█ █══════╩══════█ █╨█ █══════╩══════█ █║\n" +
	"║█                                     █║\n" +
	"╚═══════════════════════════════════════╝";

	public static readonly string DotsString =
	"                                         \n" +
	"  · · · · · · · · ·   · · · · · · · · ·  \n" +
	"  ·     ·         ·   ·         ·     ·  \n" +
	"  +     ·         ·   ·         ·     +  \n" +
	"  · · · · · · · · · · · · · · · · · · ·  \n" +
	"  ·     ·   ·               ·   ·     ·  \n" +
	"  · · · ·   · · · ·   · · · ·   · · · ·  \n" +
	"        ·                       ·        \n" +
	"        ·                       ·        \n" +
	"        ·                       ·        \n" +
	"        ·                       ·        \n" +
	"        ·                       ·        \n" +
	"        ·                       ·        \n" +
	"        ·                       ·        \n" +
	"        ·                       ·        \n" +
	"  · · · · · · · · ·   · · · · · · · · ·  \n" +
	"  ·     ·         ·   ·         ·     ·  \n" +
	"  + ·   · · · · · ·   · · · · · ·   · +  \n" +
	"    ·   ·   ·               ·   ·   ·    \n" +
	"  · · · ·   · · · ·   · · · ·   · · · ·  \n" +
	"  ·               ·   ·               ·  \n" +
	"  · · · · · · · · · · · · · · · · · · ·  \n" +
	"                                         ";

	public static readonly string[] PacManAnimations =
[
	"\"' '\"",
	"n. .n",
	")>- ->",
	"(<- -<",
];

#endregion
}