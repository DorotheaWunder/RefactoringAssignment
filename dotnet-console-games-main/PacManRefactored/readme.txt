Original code: https://github.com/dotnet/dotnet-console-games/tree/main/Projects/PacMan
From the Console Game Repository: https://github.com/dotnet/dotnet-console-games

1. Used the Command Pattern for player input because
    - it decouples input from game logic (= player behavior)
    - replacing the switch statements with a dictionary makes things more scalable and easier to change
   
2. Used the State Pattern for player movement directions because:
    - more streamlined and less cluttered UpdatePacMan() method
    - replacing the if statements with states to divide movement logic and movement behavior
    
3. Used the Factory Patten for the ghosts because
    - it decouples initialization from behavior logic
    - the original initialization method had a lot of repetition
    - each ghost type has its own separated class now
    - creating new ghost/enemy types would be more scalable and streamlined
    
4. Used the Observer Patten for the updates because
    - it decouples the trigger (collision) from the effect
    - makes the updates more streamlines
    - it's easier to expand if I wanted to add more events or tie additional effects (like SFX) to an event