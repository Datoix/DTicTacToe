using SDL2;

namespace DTicTacToe.Graphics;

public static class Colors {
	public static void White(nint sdlRenderer) {
		SDL.SDL_SetRenderDrawColor(sdlRenderer, 255, 255, 255, 255);
	}

	public static void Black(nint sdlRenderer) {
		SDL.SDL_SetRenderDrawColor(sdlRenderer, 0, 0, 0, 255);
	}

	public static void Gray(nint sdlRenderer) {
		SDL.SDL_SetRenderDrawColor(sdlRenderer, 0, 0, 0, 50);
	}

	public static void LightGray(nint sdlRenderer) {
		SDL.SDL_SetRenderDrawColor(sdlRenderer, 0, 0, 0, 25);		
	}

	public static void LightRed(nint sdlRenderer) {
		//SDL.SDL_SetRenderDrawColor(sdlRenderer, 255, 0, 102, 255);
		SDL.SDL_SetRenderDrawColor(sdlRenderer, 255, 255, 0, 255);
	
	}

	public static void LightBlue(nint sdlRenderer) {
		SDL.SDL_SetRenderDrawColor(sdlRenderer, 128, 128, 255, 255);
	}
}