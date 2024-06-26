using System.Numerics;
using DTicTacToe.Core;
using SDL2;

namespace DTicTacToe.Graphics;

public class Window : IDisposable {
    private readonly App _app;
    private readonly SDL.SDL_WindowFlags _flags;
	
    public nint SdlWindow { get; }
	public nint SdlRenderer { get; }

	public Vector2 Size { get; private set; }
	public GameManager GameManager { get; }

	public Window(
		App app,
		string title,
		Vector2 size,
		SDL.SDL_WindowFlags sdlWindowFlags,
		SDL.SDL_RendererFlags sdlRendererFlags
	) {
		_app = app;
		_flags = sdlWindowFlags;
		Size = size;

		SdlWindow = SDL.SDL_CreateWindow(
			title: title,
			x: SDL.SDL_WINDOWPOS_CENTERED,
			y: SDL.SDL_WINDOWPOS_CENTERED,
			w: (int)Size.X,
			h: (int)Size.Y,
			flags: sdlWindowFlags
		);

		SdlRenderer = SDL.SDL_CreateRenderer(
			window: SdlWindow,
			index: -1,
			flags: sdlRendererFlags
		);

		GameManager = new(_app, this);

		// For transparency
		SDL.SDL_SetRenderDrawBlendMode(SdlRenderer, SDL.SDL_BlendMode.SDL_BLENDMODE_BLEND);
	}


	public void Update() {
		GameManager.Update();
	}

	public void Render() {
		GameManager.Render(SdlRenderer);
	}

	// IDisposable implementation


	public void Dispose() {
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing) {
		/*
		if (disposing) {
			// Managed objects
			// (none)
		}
		*/
		
		// free unmanaged
		SDL.SDL_DestroyWindow(SdlWindow);
		SDL.SDL_DestroyRenderer(SdlWindow);
	}
}