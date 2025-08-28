using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
	class Nagroda // domyślnie internal, czyli dostępna w obrębie tego samego assembly/projektu
	{
		public int WartoscNagrody { get; private set; }
		public Punkt Pozycja = new Punkt();

		public Nagroda()
		{
			Random generator = new Random();
			WartoscNagrody = generator.Next(1, 6); // 0 to kara, 1-5 to nagroda
			Pozycja.X = generator.Next(Plansza.minKolumna + 1, Plansza.maxKolumna - 2); // pozycja nagrody w poziomie
			Pozycja.Y = generator.Next(Plansza.minWiersz + 1, Plansza.maxWiersz - 2); // pozycja nagrody w pionie
		}

		public void PokazNagrode()
		{
			ConsoleColor kolor = Console.ForegroundColor; // dla podtrzymania akutalnego koloru
			Console.SetCursorPosition(Pozycja.X, Pozycja.Y);
			if (WartoscNagrody == 0)
			{
				Console.ForegroundColor = ConsoleColor.Red;
			}
			else 
				Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write(WartoscNagrody);
			Console.ForegroundColor = kolor; // powrot do poprzedniego koloru
		}

		public void WyczyscNagrode()
		{
			Console.SetCursorPosition(Pozycja.X, Pozycja.Y);
			Console.Write(" ");
		}

	}
}
