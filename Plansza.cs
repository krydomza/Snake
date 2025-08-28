using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
	public static class Plansza
	{
		public static readonly int minWiersz = 0;
		public static readonly int maxWiersz = 30;
		public static readonly int minKolumna = 0;
		public static readonly int maxKolumna = 100;

		public static void RysujPlansze()
		{
			Console.BackgroundColor = ConsoleColor.DarkCyan;
			string spacje = String.Empty;
			Console.SetCursorPosition(minKolumna, minWiersz);
			Console.Write(spacje.PadLeft(maxKolumna)); // górna krawędź pustych spacji
			Console.SetCursorPosition(minKolumna, maxWiersz); 
			Console.Write(spacje.PadLeft(maxKolumna)); // dolna krawędź pustych spacji

			for (int i = 1; i <= maxWiersz; i++)
			{
				Console.SetCursorPosition(minKolumna, i);
				Console.Write(" "); // lewa krawędź
				Console.SetCursorPosition(maxKolumna - 1, i);
				Console.Write(" "); // prawa krawędź
			}
			PiszWynik(0);
			Console.SetWindowPosition(0,0);
		}

		public static void PiszWynik(int punkty)
		{
			Console.BackgroundColor = ConsoleColor.DarkCyan;
			Console.SetCursorPosition(minKolumna + 1, maxWiersz);
			Console.ForegroundColor = ConsoleColor.White;						
			Console.Write("Wynik: {0}",punkty);

			Console.SetCursorPosition(minKolumna + 10, maxWiersz);
			Console.Write("Speed: {0}", Gra.aktualnySpeedWeza);

			Console.SetCursorPosition(minKolumna + 25, maxWiersz);
			Console.Write("RefreshTime: {0}", Gra.aktualnyRefreshTimeNagrody);			
			Console.BackgroundColor= ConsoleColor.Black;
		}
	}
}
