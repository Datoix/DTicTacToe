using System.Numerics;
using System.Runtime;
using DTicTacToe.Graphics;
using SDL2;

namespace DTicTacToe.Core;

public class SmallBoard : Board<int> {
	public int? Owner { get; set; } = null;
	public Vector2 Coord { get; }

	public SmallBoard(GameManager gameManager, Vector2 coord, Vector2 position, Vector2 size, int n)
	: base(gameManager, position, size, n) {
		Coord = coord;
	}


	// Only responsible for tiles (line rendered in BigBoard)
	public override void Render(nint sdlRenderer) {
		base.Render(sdlRenderer);

		if (Owner == null) {
			RenderTiles(sdlRenderer);
		} else {
			RenderOwner(sdlRenderer);
		}
	}
	
	public void RenderTiles(nint sdlRenderer) {
		for (int i = 0; i < N; ++i) {
			for (int j = 0; j < N; ++j) {
				if (Data[i, j] == 0) continue;

				switch (Data[i, j]) {
					case 1:
						Colors.LightBlue(sdlRenderer);
						break;
					case -1:
						Colors.LightRed(sdlRenderer);
						break;
				}

				Shapes.Rect(
					sdlRenderer,
					position: new(
						X(j * _cellSize.X),
						Y(i * _cellSize.Y)
					),
					size: _cellSize
				);
			}
		}
	}
	
	public void RenderOwner(nint sdlRenderer) {
		switch (Owner!) {
			case 1:
				Colors.LightBlue(sdlRenderer);
				break;
			case -1:
				Colors.LightRed(sdlRenderer);
				break;
		}

		Shapes.Rect(
			sdlRenderer,
			position: Position,
			size: _size
		);
	}

}