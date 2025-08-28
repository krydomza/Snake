using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
	public class Waz
	{
		/* LinkedList<T> to lista dwukierunkowa
		  Węzeł przechowuje:
			swoją wartość (Value),
			link do poprzedniego (Previous),
			link do następnego 
			Dzięki temu elementy są połączone "łańcuszkiem"
		*/
		private static LinkedList<Punkt> cialoWeza = new LinkedList<Punkt>();
		public static Kierunek gdzieSkrecic; // kierunek węża
		private static readonly int poczatkowaDlugoscWeza = 5;

		public static void StworzWeza()
		{
			for (int i = 1; i <= poczatkowaDlugoscWeza; i++)
			{
				cialoWeza.AddFirst(new Punkt(i, Plansza.minWiersz + 6)); // dodajemy do przodu
			}
			gdzieSkrecic = Kierunek.prawo; // początkowy kierunek węża
			RysujWeza();
		}

		public static void RysujPunkt(Punkt p)
		{
			Console.SetCursorPosition(p.X, p.Y);
			Console.Write("*");
		}

		public static void RysujWeza()
		{
			foreach (Punkt p in cialoWeza)
			{
				RysujPunkt(p);
			}		
		}

		public static void WyczyscPunkt(Punkt punkt)
		{
			Console.SetCursorPosition(punkt.X, punkt.Y);
			Console.Write(" ");
		}

		public static void WyczyscWeza()
		{
			foreach (Punkt p in cialoWeza)
			{
				WyczyscPunkt(p); // Czyszczenie węża na ekranie
			}
			cialoWeza.Clear();         // Zerowanie listy			
		}

		private static void UstalPrzesuniecieDlaGlowy(ref int wPionie,ref int wPoziomie)
		{
			switch(gdzieSkrecic)
			{
				case Kierunek.prawo:
					wPoziomie = 1;
					break;
				case Kierunek.lewo:
					wPoziomie = -1;
					break;
				case Kierunek.gora:
					wPionie = -1;
					break;
				case Kierunek.dol:
					wPionie = 1;
					break;
			}
		}

		private static void WykonajRuch()
		{
			int przesunWPionie = 0;
			int przesunWPoziomie = 0;

			WyczyscPunkt(cialoWeza.Last.Value); // Czyszczenie ostatniego punktu węża (' ' na ekranie)
			cialoWeza.RemoveLast();         // Usuwanie ostatniego punktu węża z listy LinkedList
			UstalPrzesuniecieDlaGlowy(ref przesunWPionie, ref przesunWPoziomie);
			Punkt punkt = new Punkt();
			punkt.X = cialoWeza.First.Value.X + przesunWPoziomie; // Nowa pozycja X głowy węża
			punkt.Y = cialoWeza.First.Value.Y + przesunWPionie;   // Nowa pozycja Y głowy węża

			cialoWeza.AddFirst(punkt);      // Dodawanie nowej głowy węża na początek listy LinkedList
			RysujPunkt(punkt);              // Rysowanie nowej głowy węża na ekranie
		}

		public static bool PoruszWeza()
		{
			bool ruchDozwolony = true;
			// Sprawdzanie kolizji z krawędziami planszy
			if ((cialoWeza.First.Value.X <= Plansza.minKolumna + 1
				&& gdzieSkrecic == Kierunek.lewo)
				|| (cialoWeza.First.Value.X >= Plansza.maxKolumna - 1
					&& gdzieSkrecic == Kierunek.prawo)
				|| (cialoWeza.First.Value.Y <= Plansza.minWiersz + 1
					&& gdzieSkrecic == Kierunek.gora)
				|| (cialoWeza.First.Value.Y >= Plansza.maxWiersz - 1 
				&& gdzieSkrecic == Kierunek.dol))
			{
				ruchDozwolony = false; // Przekroczył zakres planszy gry
			}
			else
			{
				WykonajRuch();
			}
			return ruchDozwolony;
		}

		private static void UstalPrzesuniecieDlaOgona(ref int wPionie, ref int wPoziomie)
		{
			UstalPrzesuniecieDlaGlowy(ref wPionie, ref wPoziomie);
			wPionie = -wPionie; // Na odwrot niż dla głowy
			wPoziomie = -wPoziomie; // Na odwrot niż dla głowy
		}

		public static void WydluzanieWeza()
		{
			int przesunWPionie = 0;
			int przesunWPoziomie = 0;
			Punkt punkt = new Punkt();
			UstalPrzesuniecieDlaOgona(ref przesunWPionie, ref przesunWPoziomie);
			punkt.X = cialoWeza.Last.Value.X + przesunWPoziomie; // Nowa pozycja X ogona węża
			punkt.Y = cialoWeza.Last.Value.Y + przesunWPionie;   // Nowa pozycja Y ogona węża
			cialoWeza.AddLast(punkt);      // Dodawanie nowego ogona węża na koniec listy LinkedList
			RysujPunkt(punkt);              // Rysowanie nowego ogona węża na ekranie
		}

		public static bool CzyJestTu(Punkt punktNagrody)
		{
			bool wynik = false;
			foreach (Punkt punktWeza in cialoWeza)
			{
				if (punktWeza.X == punktNagrody.X &&
					punktWeza.Y == punktNagrody.Y)
				{
					wynik = true;
					break;
				}
			}
			return wynik;
		}

		internal static bool CzyWazSieZjadl()
		{
			// Sprawdzimy czy głowa (First) jest w kolizji z którymś z pozostałych punktów węża
			bool wynik = false;
			bool czyPierwszyPunkt = true;
			foreach (Punkt punkt in cialoWeza)
			{
				if (!czyPierwszyPunkt)
				{
					if (cialoWeza.First.Value.X == punkt.X &&
						cialoWeza.First.Value.Y == punkt.Y)
					{
						wynik = true;
						break;
					}
				}
				czyPierwszyPunkt = false;
			}
			return wynik;
		}
	}
}
