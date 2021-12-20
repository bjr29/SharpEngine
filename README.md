[![Codacy Badge](https://app.codacy.com/project/badge/Grade/a026e682c2d54f69a9bc24a392bb3295)](https://www.codacy.com/gh/bjr29/SharpEngine/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=bjr29/SharpEngine&amp;utm_campaign=Badge_Grade)
# Sharp Engine
A 2D game engine made in C# with SDL2-CS: https://github.com/flibitijibibo/SDL2-CS

## Getting Started
```C#
// Assign a couple of events
Engine.Ready += Engine_Ready;
Engine.Draw += Engine_Draw;

// Starts the game engine and sets up the app if neccessary
Engine.Init();

static void Engine_Ready(object sender, EventArgs e) {
  // Create the window
  Engine.Window = new();
  
  // Do stuff before the first frame
}

static void Engine_Draw(object sender, EventArgs e) {
  // Do stuff every frame
}
```
