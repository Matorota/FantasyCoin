# FantasyCoin

A simple 3D isometric platforming game made in Unity. Your goal is to collect all the coins on the map before the time runs out. Be careful not to fall off the edge!

## Movement 
You can switch between the two movement systems in the Unity Editor by selecting the necessary script.
 - Tile movement ( default )
 - Non tile movement system

## How to Play
- Collect every coin to win the game.
- If the countdown timer hits zero, you lose.
- If you fall off the map, you die.

## Controls
- **W, A, S, D :** Move around
- **Spacebar:** Jump
- **R:** Restart the level
- **Esc:** Return to the Main Menu


## Comment: 
Toward the end of development, I noticed an issue with the colliders and object positioning. I tried multiple approaches to fix it, but a bug remained where objects wouldn't fall off as intended. As a workaround, I added invisible collider walls and manually adjusted the positioning of few props and walls to contain the problem.
That is why some areas may appear slightly uneven.
