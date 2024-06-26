using System.Collections;
using System.Numerics;

namespace DTicTacToe.Core;

public static class Extensions {
	public static Vector2 Modify(this Vector2 v, Func<Vector2, Vector2> func) {
		return func(v);
	}

	public static Vector2 Modify(this Vector2 v, Func<float, float> func) {
		return Modify(v, func, func);
	}

	public static Vector2 Modify(this Vector2 v, Func<float, float> func1, Func<float, float> func2) {
		return new(func1(v.X), func2(v.Y));
	}

	public static Vector2 Floor(this Vector2 v) {
		return new((int)v.X, (int)v.Y);
	}

	public static T GetByVec2<T>(this T[,] arr, Vector2 v) {
		return arr[(int)v.X, (int)v.Y];
	}

	public static void SetByVec2<T>(this T[,] arr, T value, Vector2 v) {
		arr[(int)v.X, (int)v.Y] = value;
	}

	public static void Print(this BitArray arr, int size) {
		for (int i = 0; i < arr.Length; ++i) {
			Console.Write(
				Convert.ToInt32(arr[i])
			);
			if ((i + 1) % size == 0) {	
				Console.WriteLine();
			}
		}
	}
}