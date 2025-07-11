Original code: https://github.com/dotnet/dotnet-console-games/tree/main/Projects/PacMan
From the Console Game Repository: https://github.com/dotnet/dotnet-console-games

1. Used the Command Pattern for player input because
    - it decouples input from game logic (= player behavior)
    - replacing the switch statements with a dictionary makes things more scalable and easier to change
   
2. Used the State Pattern for player movement directions because:
    - more streamlined and less cluttered UpdatePacMan() method
    - replacing the if statements with states to divide movement logic and movement behavior