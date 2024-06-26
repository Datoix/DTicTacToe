using System.Numerics;
using DTicTacToe.Core;
using DTicTacToe.Graphics;
using SDL2;

namespace DTicTacToe;

public class App : IDisposable {
	private static readonly Vector2 WindowSize = new(1200, 700);
    private readonly EventHandler _eventHandler;
	
	public Window Window { get; }
    public bool IsRunning { get; set; }

	public App() {
		var sdlWindowFlags = SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE;
		var sdlRendererFlags = SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED; 

		SDL.SDL_Init(SDL.SDL_INIT_VIDEO);

		Window = new(
			this,
			title: "DTicTacToe",
			size: WindowSize,
			sdlWindowFlags,
			sdlRendererFlags
		);

		_eventHandler = new(this);  
	}


	public void Run() {
		IsRunning = true;
		
		while (IsRunning) {
			_eventHandler.HandleEvents();
			Window.Update();
			Window.Render();
		}

		Quit();
	}

	public void Quit() {
		Window.Dispose();
		SDL.SDL_Quit();
	}
	

	// IDisposable implementation

	public void Dispose() {
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing) {
		if (disposing) {
			// Managed objects

			Window.Dispose();
		}

		SDL.SDL_Quit();
		// free unmanaged
		// (none)
	}

}