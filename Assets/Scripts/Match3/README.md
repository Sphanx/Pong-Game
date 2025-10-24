# Match 3 Game - Candy Crush Style

A complete Match 3 game implementation for Unity, similar to Candy Crush.

## Features

- 8x8 grid board
- 6 different colored gems (Red, Blue, Green, Yellow, Purple, Orange)
- Swipe controls to swap adjacent gems
- Match detection for 3 or more gems in a row/column
- Automatic gem falling and refilling
- Cascading matches
- Score system

## Setup Instructions

### 1. Create the Match3 Scene

1. In Unity, create a new scene: `File > New Scene`
2. Save it as `Match3Scene` in `Assets/Scenes/`

### 2. Create the Gem Prefab

1. Create a new 2D Sprite GameObject: `GameObject > 2D Object > Sprite`
2. Rename it to "Gem"
3. Add the `Gem.cs` script component to it
4. Add a `Sprite Renderer` component (should be there by default)
5. Set the sprite to a circle or square sprite (Unity's built-in sprite or create your own)
6. Add a `Circle Collider 2D` or `Box Collider 2D` component
7. Drag the "Gem" GameObject into the `Assets/Scripts/Match3/` folder to create a prefab
8. Delete the Gem from the scene hierarchy

### 3. Setup the Board

1. Create an empty GameObject: `GameObject > Create Empty`
2. Rename it to "Board"
3. Add the `Board.cs` script component
4. In the Inspector, assign the Gem prefab you created to the "Gem Prefab" field
5. Set Width and Height to 8 (or your preferred size)

### 4. Create MatchFinder

1. Create an empty GameObject: `GameObject > Create Empty`
2. Rename it to "MatchFinder"
3. Add the `MatchFinder.cs` script component

### 5. Setup the UI

1. Create a Canvas: `GameObject > UI > Canvas`
2. Set Canvas Scaler to "Scale with Screen Size"
3. Create a Text element: `Right-click Canvas > UI > Text`
4. Rename it to "ScoreText"
5. Position it at the top of the screen
6. Set the text to "Score: 0"
7. Customize font size and color as desired

### 6. Create ScoreManager

1. Create an empty GameObject: `GameObject > Create Empty`
2. Rename it to "ScoreManager"
3. Add the `ScoreManager.cs` script component
4. Assign the ScoreText you created to the "Score Text" field in the Inspector

### 7. Setup Camera

1. Select the Main Camera
2. Set the Camera's position to approximately (3.5, 3.5, -10) to center the 8x8 board
3. Set Camera's Projection to Orthographic
4. Set Size to 5 or 6 to fit the board nicely

### 8. Configure Project Settings

1. Go to `Edit > Project Settings > Physics 2D`
2. Make sure the gravity is set to 0 (we don't want physics gravity)

## How to Play

1. Click and drag (or swipe) a gem in any direction (up, down, left, right)
2. The gem will swap with the adjacent gem in that direction
3. If the swap creates a match of 3 or more gems of the same color, they will disappear
4. New gems fall from the top to fill the empty spaces
5. Cascading matches are automatically detected and scored
6. Score increases by 10 points per matched gem

## Script Overview

- **Gem.cs**: Controls individual gem behavior, handles input (swipe detection), and manages gem movement
- **Board.cs**: Manages the game board, gem grid, swapping logic, match processing, and gem falling mechanics
- **MatchFinder.cs**: Detects all matches on the board (3+ gems in a row horizontally or vertically)
- **ScoreManager.cs**: Manages the player's score and updates the UI

## Customization

### Change Board Size
In the Board component, adjust the `Width` and `Height` values.

### Change Number of Gem Types
Edit the `GemType` enum in `Gem.cs` and update the random range in `Board.cs` SetUp() method.

### Change Colors
Modify the `SetColor()` method in `Gem.cs` to use different colors or custom sprites.

### Change Scoring
Modify the scoring logic in `Board.cs` DestroyMatches() method.

## Tips for Enhancement

1. **Add Sprites**: Replace the colored circles with custom candy/gem sprites
2. **Add Particle Effects**: Add particle systems when gems are destroyed
3. **Add Sound Effects**: Add audio for swaps, matches, and cascades
4. **Add Animations**: Use Unity's Animator to add pop/crush animations
5. **Add Special Gems**: Implement special gems (bombs, line clears, etc.) for 4+ matches
6. **Add Levels**: Create a level system with goals and move limits
7. **Add Power-ups**: Implement special abilities the player can activate

## Known Limitations

- Currently uses simple colored sprites (no custom artwork)
- Basic scoring system
- No level progression or goals
- No special gem types
- No animations or particle effects
- No sound effects

Enjoy your Match 3 game!
