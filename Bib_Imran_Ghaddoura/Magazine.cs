using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bib_Imran_Ghaddoura
{
    internal class Magazine: ReadingRoomItem
    {
		private byte month;

		public byte Month
		{
			get { return month; }
			set 
			{
				if (value > 12)
				{
					Console.WriteLine("De maand is maximaal 12");
				}
				else
				{
					month = value;
				}
			}
		}

		private uint year;

		public uint Year
		{
			get { return year; }
			set 
			{
				if (value > 2500)
				{
					Console.WriteLine("Het jaartal is maximaal 2500");
				}
				else
				{
					year = value;
				}
			}
		}

        public override string Identification
		{
            get
            {
                string letters;
                switch (this.Title.ToLower())
                {
                    case "national geographic":
                        letters = "NG";
                        break;
                    case "time magazin":
                        letters = "TM";
                        break;
                    case "scientific american":
                        letters = "SA";
                        break;
                    case "harvard business review":
                        letters = "HBR";
                        break;
                    default:
                        letters = this.Title.Substring(0, 2).ToUpper();
                        break;
                }
				string correctedMonth = this.month < 10 ? $"0{this.month}" : $"{this.month}";
                return $"{letters}{correctedMonth}{this.year}";
            }
        }

        public override string Categorie
		{
			get { return "Maandblad"; }
		}

        public Magazine(string title, string publisher, byte month, uint year) :base(title, publisher)
		{
			this.Month = month;
			this.Year = year;
		}
    }
}
