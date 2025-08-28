using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
	static class Gra
	{
		static bool graTrwa = true;
		static DateTime start = DateTime.Now;
		static Nagroda nagroda = new Nagroda();		
		static int punkty = 0;		
		private static readonly int poczatkowaSzybkoscWeza = 300;
		public static int aktualnySpeedWeza = poczatkowaSzybkoscWeza;
		private static readonly int refreshTimeNagrodyPoczatkowy = 25; // s
		public static int aktualnyRefreshTimeNagrody = refreshTimeNagrodyPoczatkowy;

		public static void NowaGra()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.White;
			graTrwa = true;
			punkty = 0;
			aktualnySpeedWeza = poczatkowaSzybkoscWeza;
			aktualnyRefreshTimeNagrody = refreshTimeNagrodyPoczatkowy;
			Plansza.RysujPlansze();
			Waz.StworzWeza();
			nagroda.PokazNagrode();
			Graj();
		}

		public static void Graj()
		{
			while (graTrwa)
			{
				WczytajKlawisz();
				if(!graTrwa) break;
				NowaNagroda();
				CzyWazSieZjadl();

				// można by to też wywolac w ten sposób
				//if (Waz.CzyWazSieZjadl())
				//{
				//	GameOver();
				//}

				if (!graTrwa) break;
				WazIdzie();
				if (!graTrwa) break;
				ZjadlNagrode();
				System.Threading.Thread.Sleep(aktualnySpeedWeza);
			}
		}

		private static void NowaNagroda()
		{
			if (start <= DateTime.Now.Subtract(TimeSpan.FromSeconds(refreshTimeNagrodyPoczatkowy)))
			{
				nagroda.WyczyscNagrode();
				start = DateTime.Now;
				nagroda = new Nagroda();
				nagroda.PokazNagrode();
			}
		}

		private static void ZjadlNagrode()
		{
			if (Waz.CzyJestTu(nagroda.Pozycja))
			{
				if (nagroda.WartoscNagrody == 0)   // czyli kara
				{
					Waz.WyczyscWeza();
					Waz.StworzWeza();
					punkty = 0;
					aktualnySpeedWeza = poczatkowaSzybkoscWeza;
					aktualnyRefreshTimeNagrody = refreshTimeNagrodyPoczatkowy;
				}
				else                        // faktyczna nagroda
				{
					punkty += nagroda.WartoscNagrody;
					Waz.WydluzanieWeza();
					if (aktualnySpeedWeza > 25)  // zwiekszenie predkosci węża 
						aktualnySpeedWeza -= 25;
					if (aktualnySpeedWeza < 25)  // ograniczenie maksymalnej szybkości węża
						aktualnySpeedWeza = 25;

					if (aktualnyRefreshTimeNagrody > 2) // skróćenie czasu odświeżania nagrody
						aktualnyRefreshTimeNagrody -= 2;
					if (aktualnyRefreshTimeNagrody < 2) // ograniczenie minimalnego czasu odświeżania nagrody
						aktualnyRefreshTimeNagrody = 2;
				}
				Plansza.PiszWynik(punkty);
				nagroda = new Nagroda();
				nagroda.PokazNagrode();
			}
		}

		private static void WazIdzie()
		{
			if (!Waz.PoruszWeza())
			{
				GameOver();
			}
		}

		private static void WczytajKlawisz()
		{
			if (Console.KeyAvailable)   // Coś jest w strumieniu wejściowym
			{
				ConsoleKeyInfo klawisz = Console.ReadKey(true);   // Wczytujemy wciśnięcie klawisza w trybie "true" - nie pokazuje wciśniętego klawisza na ekranie

				// Wciśnięto "Lewo". Jeśli wąż nie szedł w prawo, to pozwalamy mu skręcić w lewo
				if (klawisz.Key == ConsoleKey.LeftArrow &&
					Waz.gdzieSkrecic != Kierunek.prawo)
				{
					Waz.gdzieSkrecic = Kierunek.lewo;
				}
				// Wciśnięto "Prawo". Jeśli wąż nie szedł w lewo to pozwalamy mu skręcić w prawo
				if (klawisz.Key == ConsoleKey.RightArrow &&
					Waz.gdzieSkrecic != Kierunek.lewo)
				{
					Waz.gdzieSkrecic = Kierunek.prawo;
				}
				// Wciśnięto "W górę". Jeśli wąż nie szedł w dół, to pozwalamy mu skręcić w górę
				if (klawisz.Key == ConsoleKey.UpArrow &&
					Waz.gdzieSkrecic != Kierunek.dol)
				{
					Waz.gdzieSkrecic = Kierunek.gora;
				}
				// Wciśnięto "W dół". Jeśli wąż nie szedł w górę, to pozwalamy mu skręcić w dół
				if (klawisz.Key == ConsoleKey.DownArrow &&
					Waz.gdzieSkrecic != Kierunek.gora)
				{
					Waz.gdzieSkrecic = Kierunek.dol;
				}
				// Wciśnięto Escape - koniec gry
				if (klawisz.Key == ConsoleKey.Escape)
				{
					GameOver();
				}
				// Wyczyszczenie bufora strumienia wejściowego
				while (Console.KeyAvailable) { Console.ReadKey(true); }
			}			
		}

		private static void CzyWazSieZjadl()
		{
			if (Waz.CzyWazSieZjadl())
			{
				GameOver();
			}
		}

		private static void GameOver()
		{
			graTrwa = false;
			ConsoleColor kolor = Console.ForegroundColor;
			Console.SetCursorPosition(Plansza.minKolumna + 34, Plansza.minWiersz + 11);
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write("Game over!!!");
			Console.ForegroundColor = kolor;
			Console.ReadKey();
			Waz.WyczyscWeza();
		}
	}
}
