using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bib_Imran_Ghaddoura
{
    internal abstract class ReadingRoomItem
    {
		private string title;

		public string Title
		{
			get { return title; }
		}

		private string publisher;

		public string Publisher	
		{
			get { return publisher; }
			set { publisher = value; }
		}


		public abstract string Identification{ get; }
		

		public abstract string Categorie { get; }


		public ReadingRoomItem(string title, string publisher)
		{
			this.title = title;
			this.publisher = publisher; 
		}
	}
}
