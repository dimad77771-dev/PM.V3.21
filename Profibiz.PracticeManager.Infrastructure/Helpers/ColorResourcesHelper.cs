using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Infrastructure
{
	public static class ColorResourcesHelper
	{
		public static SpecialistColor[] GetSpecialistColorArray = new[]
		{
			new SpecialistColor("DDEBF7", "000000"),
			new SpecialistColor("5B9BD5", "FFFFFF"),
			new SpecialistColor("BDD7EE", "000000"),
			new SpecialistColor("9BC2E6", "FFFFFF"),

			new SpecialistColor("FCE4D6", "000000"),
			new SpecialistColor("ED7D31", "FFFFFF"),
			new SpecialistColor("F8CBAD", "000000"),
			new SpecialistColor("F4B084", "FFFFFF"),

			new SpecialistColor("EDEDED", "000000"),
			new SpecialistColor("A5A5A5", "FFFFFF"),
			new SpecialistColor("DBDBDB", "000000"),
			new SpecialistColor("C9C9C9", "FFFFFF"),

			new SpecialistColor("FFF2CC", "000000"),
			new SpecialistColor("FFC000", "FFFFFF"),
			new SpecialistColor("FFE699", "000000"),
			new SpecialistColor("FFD966", "FFFFFF"),

			new SpecialistColor("D9E1F2", "000000"),
			new SpecialistColor("4472C4", "FFFFFF"),
			new SpecialistColor("B4C6E7", "000000"),
			new SpecialistColor("8EA9DB", "FFFFFF"),

			new SpecialistColor("E2EFDA", "000000"),
			new SpecialistColor("70AD47", "FFFFFF"),
			new SpecialistColor("C6E0B4", "000000"),
			new SpecialistColor("A9D08E", "FFFFFF"),
		};

		//public static SpecialistColor[] GetSpecialistColorArray = new[]
		//{
		//	new SpecialistColor("EDEDED", "000000"),
		//	new SpecialistColor("A5A5A5", "000000"),
		//	new SpecialistColor("DBDBDB", "000000"),
		//	new SpecialistColor("C9C9C9", "000000"),

		//	new SpecialistColor("DDEBF7", "000000"),
		//	new SpecialistColor("5B9BD5", "000000"),
		//	new SpecialistColor("BDD7EE", "000000"),
		//	new SpecialistColor("9BC2E6", "000000"),

		//	new SpecialistColor("FCE4D6", "000000"),
		//	new SpecialistColor("ED7D31", "000000"),
		//	new SpecialistColor("F8CBAD", "000000"),
		//	new SpecialistColor("F4B084", "000000"),

		//	new SpecialistColor("FFF2CC", "000000"),
		//	new SpecialistColor("FFC000", "FFFFFF"),
		//	new SpecialistColor("FFE699", "000000"),
		//	new SpecialistColor("FFD966", "FFFFFF"),

		//	new SpecialistColor("D9E1F2", "000000"),
		//	new SpecialistColor("4472C4", "FFFFFF"),
		//	new SpecialistColor("B4C6E7", "000000"),
		//	new SpecialistColor("8EA9DB", "FFFFFF"),

		//	new SpecialistColor("E2EFDA", "000000"),
		//	new SpecialistColor("70AD47", "FFFFFF"),
		//	new SpecialistColor("C6E0B4", "000000"),
		//	new SpecialistColor("A9D08E", "FFFFFF"),
		//};
		public static SpecialistColor GetSpecialistColor(int npp)
		{
			if (npp == -1)
			{
				return new SpecialistColor("C6EFCE", "006100");
			}

			npp = npp % GetSpecialistColorArray.Length;
			return GetSpecialistColorArray[npp];
		}

		public class SpecialistColor
		{
			public SpecialistColor(String background, String foreground)
			{
				Background = "#" + background;
				Foreground = "#" + foreground;
			}
			public String Background { get; set; }
			public String Foreground { get; set; }
		}






	}
}

