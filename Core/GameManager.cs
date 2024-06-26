using System.Numerics;
using DTicTacToe.Graphics;
using SDL2;

namespace DTicTacToe.Core;

public class GameManager {
    private readonly App _app;
    private readonly Window _window;
	private readonly BigBoard _bigBoard;
    
    private int _moveCounter = 0;

    public Vector2? ActiveBigCoord { get; set; }
	public int CurrentPlayer { get; set; } = 1;

	public List<Scene> Scenes { get; }

    public GameManager(App app, Window window) {
		_app = app;
		_window = window;
		
		Vector2 screenMiddle = _window.Size / 2;
		Vector2 bigBoardSize = new(500, 500);

		_bigBoard = new(
			gameManager: this,
			position: screenMiddle - bigBoardSize / 2,
			size: bigBoardSize,
			n: 3
		);

		Scenes = [_bigBoard];
	}


    public void Update() {
		Scenes.ForEach(s => s.Update());
    }

	public void Render(nint sdlRenderer) {
		Scenes.ForEach(s => s.Render(sdlRenderer));

		SDL.SDL_RenderPresent(sdlRenderer);
		Colors.White(sdlRenderer); // background color
		SDL.SDL_RenderClear(sdlRenderer);
    }


	public void SubmitMove(Vector2 tile) {
		Vector2 bigCoords = (tile / _bigBoard.N).Floor();
		Vector2 localSmallCords = tile.Modify(a => a % _bigBoard.N);
			
		var clickedBoard = _bigBoard.Data.GetByVec2(bigCoords);

		if (ActiveBigCoord != null && bigCoords != ActiveBigCoord) return;
		if (clickedBoard.Owner != null) return;
	
		if (clickedBoard.Data.GetByVec2(localSmallCords) != 0) return;

		clickedBoard.Data.SetByVec2(CurrentPlayer, localSmallCords);
		CurrentPlayer = -CurrentPlayer;
		
		CheckBoard();
		
		if (_bigBoard.Data.GetByVec2(localSmallCords).Owner == null) {
			ActiveBigCoord = localSmallCords.Floor();
		} else {
			ActiveBigCoord = null;
		}

		++_moveCounter;
		if (_moveCounter == Math.Pow(_bigBoard.N, 4)) {
			_app.Quit();
		}

	}

	public void CheckBoard() {
		foreach (var smallBoard in _bigBoard.Data) {
			if (smallBoard.Owner != null) continue;
			
			int smallWinner = smallBoard.VerifyWins(v => v, -1, 1);
			if (smallWinner != 0) {
				smallBoard.Owner = smallWinner;
				_bigBoard.HoveredTile = null;
			}
		}		

		int bigWinner = _bigBoard.VerifyWins(v => v.Owner ?? 0, -1, 1);
		if (bigWinner != 0) {
			_app.Quit();
		}
	}
}