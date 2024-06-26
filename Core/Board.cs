using System.Numerics;
using DTicTacToe.Graphics;

namespace DTicTacToe.Core;

public class Board<T> : Scene {
	protected readonly Vector2 _size;
	
	protected readonly Vector2 _smallBoardCellSize;
    protected readonly Vector2 _cellSize;

	public T[,] Data { get; }
	public int N { get; }
	
    public Board(GameManager gameManager, Vector2 position, Vector2 size, int n)
	: base(gameManager) {
		
		Position = position;

		_size = size;
		N = n;

		_cellSize = _size / N;
		_smallBoardCellSize = _cellSize / n;

		Data = new T[N, N];
	}

	public int VerifyWins(Func<T, int> value, int valueFirst, int valueSecond) {
		// Rows
		int[] wins = [
			VerifyLines(value, 1, -1, true),
			VerifyLines(value, 1, -1, false),
			VerifyDiagonal(value, 1, -1, true),
			VerifyDiagonal(value, 1, -1, false)
		];

		foreach (var win in wins) {
			if (win != 0) return win;
		}

		return 0;
	}

	private int VerifyLines(Func<T, int> value, int valueFirst, int valueSecond, bool horizontal) {
		for (int i = 0; i < N; ++i) {
			int first = horizontal ? value(Data[i, 0]) : value(Data[0, i]);
			
			if (first != valueFirst && first != valueSecond) continue;
			
			bool win = true;

			for (int j = 1; j < N; ++j) {
				var nextValue = horizontal ? value(Data[i, j]) : value(Data[j, i]); 
				if (nextValue != first) {
					win = false;
					break;
				}
			}

			if (win) {
				return first;
			}
		}

		return 0;
	}

	private int VerifyDiagonal(Func<T, int> value, int valueFirst, int valueSecond, bool topDown) {
		if (topDown) {
			int first = value(Data[N - 1, 0]);

			int i = N - 2;
			int j = 1;
			
			int counter = 1;

			while (i >= 0 && j < N) { 
				if (first != valueFirst && first != valueSecond) {
					--i; ++j;
					continue;
				}

				if (value(Data[i, j]) != first) {
					break;
				}
				
				++counter;
				--i; ++j;
			}

			
			if (counter == N) {
				return first;
			}

		} else {
			int first = value(Data[0, 0]);

			int i = 1;
			int j = 1;

			int counter = 1;

			while (i < N && j < N) { 
				if (first != valueFirst && first != valueSecond) {
					++i; ++j;
					continue;
				}

			
				if (value(Data[i, j]) != first) {
					break;
				}				
				
				++counter;
				++i; ++j;
			}

			
			if (counter == N) {
				return first;
			}
		}
		return 0;
	}
}