using System;
using System.Collections.Generic;

namespace Misc
{
	public static class ArrayExtension
	{
		public static T[] SubArray<T>(this T[] data, int startIndex)
		{
			T[] result = new T[data.Length - startIndex];
			Array.Copy(data, startIndex, result, 0, data.Length - startIndex);
			return result;
		}

		public static T[] Next<T>(this T[] data){
			return data.SubArray(1);
		}

		private static Random rng = new Random();

		public static void Shuffle<T>(this IList<T> list)
		{
			int n = list.Count;
			while (n > 1)
			{
				n--;
				int k = rng.Next(n + 1);
				T value = list[k];
				list[k] = list[n];
				list[n] = value;
			}
		}
	}

	
}
