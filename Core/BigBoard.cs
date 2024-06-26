using System.Numerics;
using DTicTacToe.Graphics;
using SDL2;

namespace DTicTacToe.Core;

public class BigBoard : Board<SmallBoard> { 
	public Vector2? HoveredTile { get; set; }

    public BigBoard(GameManager gameManager, Vector2 position, Vector2 size, int n)
	: base(gameManager, position, size, n) {
		
		for (int i = 0; i < N; ++i) {
			for (int j = 0; j < N; ++j) { 
				SmallBoard newBoard = new(
					_gameManager,
					coord: new(i, j),
					position: (new Vector2(j, i) * _cellSize).Modify(X, Y),
					size: _cellSize,
					n: N
				);

				Children.Add(newBoard);
				Data[i, j] = newBoard;
			}
		}
	}

    public override void Update() {
		base.Update();
	}
	
    public override void Render(nint sdlRenderer) {
		base.Render(sdlRenderer);

		RenderGrid(sdlRenderer);
		RenderTiles(sdlRenderer);
	}

	private void RenderGrid(nint sdlRenderer) {

		// Big Board

		Colors.Black(sdlRenderer);

		for (int i = 0; i <= N; ++i) {
			// Rows
			float rowY = Y(i * _cellSize.Y);
			SDL.SDL_RenderDrawLineF(sdlRenderer, X(0), rowY, X(_size.X), rowY);
		
			// Cols
			float colX = X(i * _cellSize.X);
			SDL.SDL_RenderDrawLineF(sdlRenderer, colX, Y(0), colX, Y(_size.Y));
		}

		Colors.Gray(sdlRenderer);

		// Small Board
		for (int i = 0; i <= N * N; ++i) {
			// Rows
			float rowY = Y(i * _smallBoardCellSize.Y);
			SDL.SDL_RenderDrawLineF(sdlRenderer, X(0), rowY, X(_size.X), rowY);
		
			// Cols
			float colX = X(i * _smallBoardCellSize.X);
			SDL.SDL_RenderDrawLineF(sdlRenderer, colX, Y(0), colX, Y(_size.Y));
		}
	}

	private void RenderTiles(nint sdlRenderer) {
		if (HoveredTile is Vector2 hoveredTile) {
			int row = (int)hoveredTile.X;
			int col = (int)hoveredTile.Y;

			Colors.Gray(sdlRenderer);
			Shapes.Rect(
				sdlRenderer,
				position: new(
					X(col * _smallBoardCellSize.X),
					Y(row * _smallBoardCellSize.Y)
				),
				size: new(_smallBoardCellSize.X, _smallBoardCellSize.Y)
			);
		}
		
		Colors.LightGray(sdlRenderer);
		if (_gameManager.ActiveBigCoord is Vector2 activeBigBoard) {
			int row = (int)activeBigBoard.X;
			int col = (int)activeBigBoard.Y;

			Shapes.Rect(
				sdlRenderer,
				position: new(
					X(col * _cellSize.X),
					Y(row * _cellSize.Y)
				),
				size: _cellSize
			);
		} else {
			foreach (var smallBoard in Data) {
				if (smallBoard.Owner != null) continue;

				Shapes.Rect(
					sdlRenderer,
					position: smallBoard.Position,
					size: _cellSize
				);
			}
		}
	}
	
    public override void MouseMotion(SDL.SDL_Event e) {
		Vector2 mousePosition = new(e.motion.x, e.motion.y);

		// If mouse inside
		if (
			mousePosition.X > Position.X &&
			mousePosition.X < Position.X + _size.X &&
			mousePosition.Y > Position.Y &&
			mousePosition.Y < Position.Y + _size.Y
		) {
			var localMousePosition = mousePosition - Position;

			// when 3x3x3 1-9
			var hoveredTile = (localMousePosition / _smallBoardCellSize)
				.Modify(v => new(v.Y, v.X)); // row is X, col Y but in graphics opposite

			var bigCoords = (hoveredTile / N).Floor();
			var localSmallCords = hoveredTile.Modify(a => a % N);
		
			var smallBoard = Data.GetByVec2(bigCoords);

			bool isEmptyCondition = smallBoard.Data.GetByVec2(localSmallCords) == 0;
			bool isActiveCondition = bigCoords == _gameManager.ActiveBigCoord;
			
			if (
				smallBoard.Owner == null
				&& isEmptyCondition
				&& (isActiveCondition || _gameManager.ActiveBigCoord == null)
			) {
				HoveredTile = hoveredTile;
			} else {
				HoveredTile = null;
			}
		} else {
			HoveredTile = null;
		}
    }

	public override void MouseButtonDown(SDL.SDL_Event e) {
		if (HoveredTile is Vector2 hoveredTile) {		
			_gameManager.SubmitMove(hoveredTile);
		}
	}
}