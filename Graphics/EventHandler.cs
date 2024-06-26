using System.Text.RegularExpressions;
using DTicTacToe.Graphics;
using SDL2;

namespace DTicTacToe;

public class EventHandler {
    private readonly App _app;
    private readonly Dictionary<SDL.SDL_EventType, Action<SDL.SDL_Event>> _actions;

    public EventHandler(App app) {
		_app = app;
		_actions = new() {
			[SDL.SDL_EventType.SDL_QUIT] = Quit,
			[SDL.SDL_EventType.SDL_WINDOWEVENT] = WindowEvent,
			[SDL.SDL_EventType.SDL_MOUSEMOTION] = MouseMotion,
			[SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN] = MouseButtonDown
		};
	}

    public void HandleEvents() {
		SDL.SDL_Event e;

		while (SDL.SDL_PollEvent(out e) == 1) {
			Action<SDL.SDL_Event>? action;
			
			bool handled = _actions.TryGetValue(e.type, out action);
			
			if (handled) {
				action!.Invoke(e);
			}
		}
	}

	// Events

	public void Quit(SDL.SDL_Event _) {
		_app.IsRunning = false;
	}
	
	public void WindowEvent(SDL.SDL_Event e) {
	}

	public void MouseMotion(SDL.SDL_Event e) {
		_app.Window.GameManager.Scenes.ForEach(s => s.MouseMotion(e));	
	}

	public void MouseButtonDown(SDL.SDL_Event e) {
		_app.Window.GameManager.Scenes.ForEach(s => s.MouseButtonDown(e));
	}
}