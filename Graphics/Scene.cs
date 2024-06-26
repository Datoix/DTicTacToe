using System.Numerics;
using DTicTacToe.Core;
using SDL2;

namespace DTicTacToe.Graphics;

public abstract class Scene {
    protected readonly GameManager _gameManager;

    public Vector2 Position { get; set; }

	public List<Scene> Children { get; } = new();

	public float X(float x) => x + Position.X;
	public float Y(float y) => y + Position.Y;

	public Scene(GameManager gameManager) {
		_gameManager = gameManager;
	}

	public virtual void Update() {
		Children.ForEach(ch => ch.Update());
	}

	public virtual void Render(nint sdlRenderer) {
		Children.ForEach(ch => ch.Render(sdlRenderer));
	}

	public virtual void MouseMotion(SDL.SDL_Event motionEvent) {
		Children.ForEach(ch => ch.MouseMotion(motionEvent));
	}

	public virtual void MouseButtonDown(SDL.SDL_Event clickEvent) {
		Children.ForEach(ch => ch.MouseButtonDown(clickEvent));
	}
}