using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bib_Imran_Ghaddoura
{
    internal class NewsPaper: ReadingRoomItem
    {
		private DateTime date;

		public DateTime Date
		{
			get { return date; }
			set { date = value; }
		}

        public override string Identification
		{
			get 
			{
				string letters;
				switch (this.Title.ToLower())
				{
					case "gazet van antwerpen":
						letters = "GVA";
						break;
                    case "de standaard":
                        letters = "DS";
                        break;
                    case "het laatste nieuws":
                        letters = "HLN";
                        break;
                    case "de morgen":
                        letters = "DM";
                        break;
                    default:
                        letters = this.Title.Substring(0,2).ToUpper();
                        break;
				}
				return $"{letters}{this.date.ToString("dd")}{this.date.ToString("MM")}{this.date.Year}";
			}
		}

        public override string Categorie
		{
			get { return "Krant";}
		}

        public NewsPaper(string title, string publisher, DateTime date) :base(title, publisher)
		{
			this.date = date;
		}
    }
}
