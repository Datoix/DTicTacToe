using System.Numerics;
using SDL2;

namespace DTicTacToe.Graphics;

public static class Shapes {
	public static void Rect(nint sdlRenderer, Vector2 position, Vector2 size) {
		SDL.SDL_FRect rect = new() {
			x = position.X,
			y = position.Y,
			w = size.X,
			h = size.Y
		};

		SDL.SDL_RenderFillRectF(sdlRenderer, ref rect);
	}
}