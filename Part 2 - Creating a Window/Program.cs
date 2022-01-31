using SDL2;
using System;

IntPtr window;
IntPtr renderer;
bool running = true;

Setup();

while (running)
{
    PollEvents();
    Render();
}

CleanUp();

/// <summary>
/// Setup all of the SDL resources we'll need to display a window.
/// </summary>
void Setup() 
{
    // Initilizes SDL.
    if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0)
    {
        Console.WriteLine($"There was an issue initializing SDL. {SDL.SDL_GetError()}");
    }

    // Create a new window given a title, size, and passes it a flag indicating it should be shown.
    window = SDL.SDL_CreateWindow(
        "SDL .NET 6 Tutorial",
        SDL.SDL_WINDOWPOS_UNDEFINED, 
        SDL.SDL_WINDOWPOS_UNDEFINED, 
        640, 
        480, 
        SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

    if (window == IntPtr.Zero)
    {
        Console.WriteLine($"There was an issue creating the window. {SDL.SDL_GetError()}");
    }

    // Creates a new SDL hardware renderer using the default graphics device with VSYNC enabled.
    renderer = SDL.SDL_CreateRenderer(
        window,
        -1,
        SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED |
        SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

    if (renderer == IntPtr.Zero)
    {
        Console.WriteLine($"There was an issue creating the renderer. {SDL.SDL_GetError()}");
    }
}

/// <summary>
/// Checks to see if there are any events to be processed.
/// </summary>
void PollEvents()
{
    // Check to see if there are any events and continue to do so until the queue is empty.
    while (SDL.SDL_PollEvent(out SDL.SDL_Event e) == 1)
    {
        switch (e.type)
        {
            case SDL.SDL_EventType.SDL_QUIT:
                running = false;
                break;
        }
    }
}

/// <summary>
/// Renders to the window.
/// </summary>
void Render()
{
    // Sets the color that the screen will be cleared with.
    SDL.SDL_SetRenderDrawColor(renderer, 135, 206, 235, 255);

    // Clears the current render surface.
    SDL.SDL_RenderClear(renderer);

    // Switches out the currently presented render surface with the one we just did work on.
    SDL.SDL_RenderPresent(renderer);
}

/// <summary>
/// Clean up the resources that were created.
/// </summary>
void CleanUp()
{
    SDL.SDL_DestroyRenderer(renderer);
    SDL.SDL_DestroyWindow(window);
    SDL.SDL_Quit();
}
