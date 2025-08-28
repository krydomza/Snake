using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
	public class Punkt
	{
		public int X { get; set; }
		public int Y { get; set; }

		public Punkt(int x, int y)
		{
			X = x;
			Y = y;
		}

		public Punkt(){ }
	}
}
