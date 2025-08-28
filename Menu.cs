using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
	static class Menu
	{
		public static void StartMenu()
		{
			//int largestWindowWidth = Console.LargestWindowWidth;
			//int largestWindowHeight = Console.LargestWindowHeight;
					
			Console.Title = "Snake Game";

			// najpierw zmniejsz okno do minimum (1x1), żeby nie blokowało
			Console.SetWindowSize(1, 1);
			Console.SetBufferSize(Plansza.maxKolumna + 2, Plansza.maxWiersz + 2 );
			Console.SetWindowSize(Plansza.maxKolumna, Plansza.maxWiersz + 1);

			while (true)
			{
				Console.Clear();
				Console.CursorVisible = false;
				Console.ForegroundColor = ConsoleColor.DarkCyan;
				Console.SetCursorPosition(Plansza.minKolumna +35, Plansza.minWiersz + 1);
				Console.Write("=== Witaj w grze Snake! ===");
				Console.ForegroundColor = ConsoleColor.Gray;
				Console.SetCursorPosition(Plansza.minKolumna + 42, Plansza.minWiersz + 10);
				Console.Write("1 - Start gry");
				Console.SetCursorPosition(Plansza.minKolumna + 42, Plansza.minWiersz + 20);
				Console.Write("2 - Wyjście");
				Console.ForegroundColor = ConsoleColor.White;
				ConsoleKeyInfo klawisz = Console.ReadKey(true); // nie pokaże znaku w konsoli

				switch (klawisz.Key)
				{
					case ConsoleKey.D1:
						Console.Clear();
						Gra.NowaGra();
						break;
					case ConsoleKey.Escape:
					case ConsoleKey.D2:
						Console.Clear();
						return;
					default:
						break;
				}
			}
		}		
	}	
}
