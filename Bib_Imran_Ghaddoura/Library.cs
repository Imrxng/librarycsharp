using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Bib_Imran_Ghaddoura
{
    internal class Library
    {

        private string name;

        public string Name
        {
            get { return name; }
            private set 
            {
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine("Mislukt om een naam te geven aan de bibliotheek. Gelieve een naam op te geven!");
                }
                else
                {
                    this.name = value;
                };
            }
        }

        private List<Book> books = new List<Book>();

        public List<Book> Books
        {
            get { return books; }
        }


        private Dictionary<DateTime, ReadingRoomItem> allReadingRoom = new Dictionary<DateTime, ReadingRoomItem>();

        public ImmutableDictionary<DateTime, ReadingRoomItem> AllReadingRoom
        {
            get { return allReadingRoom.ToImmutableDictionary(); }
        }


        public Library(string name)
        {
            this.Name = name;
        }

        public void AddBook(Book book)
        {
            this.books.Add(book);
        }

        public Book SearchBookWithTitleAndAuthor(string title, string author)
        {
            foreach (Book book in books)
            {

                if (book.Title.ToLower() == title.ToLower() && book.Author.ToLower() == author.ToLower())
                {
                    return book;
                }
            }
            return null;
        }

        public Book SearchBookWithIsbn(long isbn)
        {
            foreach (Book book in books)
            {
                if (book.Isbn == isbn)
                {
                    return book;
                }
            }
            return null;
        }

        public List<Book> SearchAllBooksWithAuthor(string author)
        {
            List<Book> allBooks = new List<Book>();
            foreach (Book book in books)
            {
                if (book.Author.ToLower() == author.ToLower())
                {
                    allBooks.Add(book);

                }
            }
            if (allBooks.Count > 0)
            {
                return allBooks;
            }
            return null;
        }

        public List<Book> SearchAllBooksWithGenre(Genres genre)
        {
            List<Book> allBooks = new List<Book>();
            foreach (Book book in books)
            {
                if (book.Genre == genre)
                {
                    allBooks.Add(book);
                }
            }
            if (allBooks.Count > 0)
            {
                return allBooks;
            }
            return null;
        }

        public void ShowBooks()
        {
            for (int i = 0; i < this.books.Count; i++)
            {
                Console.WriteLine($"Boek {i + 1}:\nTitel:\t{this.books[i].Title}\nAuteur:\t{this.books[i].Author}\n");
            }
        }

        public void DeleteBook(Book book)
        {
            if (this.Books.Contains(book))
            {
                this.books.Remove(book);
                Console.WriteLine("Verwijdering gelukt");
            }
            else
            {
                Console.WriteLine("Boek niet gevonden en kon niet verwijderd worden.");
            }
        }

        public void AddNewspaper()
        {
            Console.WriteLine("Wat is de naam van de krant?");
            string title = Console.ReadLine();
            Console.WriteLine("Wat is de datum van de krant?");
            DateTime date = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Wat is de uitgeverij van de krant?");
            string publisher = Console.ReadLine();
            NewsPaper newspaper = new NewsPaper(title, publisher, date);
            allReadingRoom.Add(DateTime.Now, newspaper);
        }

        public void AddMagazine()
        {
            Console.WriteLine("Wat is de naam van het maandblad?");
            string title = Console.ReadLine();
            Console.WriteLine("Wat is de maand van het maandblad? (numeriek)");
            byte month = Convert.ToByte(Console.ReadLine());
            Console.WriteLine("Wat is het jaar van het maandblad?");
            uint year = Convert.ToUInt32(Console.ReadLine());
            Console.WriteLine("Wat is de uitgeverij van het maandblad?");
            string publisher = Console.ReadLine();
            Magazine magazine = new Magazine(title, publisher, month, year);
            allReadingRoom.Add(DateTime.Now, magazine);
        }

        public void ShowAllMagazines()
        {
            bool found = false;
            foreach (var item in allReadingRoom)
            {
                if (item.Value is Magazine)
                {
                    found = true;
                }
            }

            if (found)
            {
                Console.WriteLine("Alle maandbladen uit de leeszaal:");
                foreach (var item in AllReadingRoom)
                {
                    if (item.Value is Magazine)
                    {
                        Magazine magazine = (Magazine)item.Value;
                        Console.WriteLine($"- {magazine.Title} van {magazine.Month}/{magazine.Year} van uitgeverij {magazine.Publisher}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Er zijn geen maandbladen beschikbaar in de leeszaal.");
            }
        }

        public void ShowAllNewspapers()
        {
            bool found = false;
            foreach (var item in allReadingRoom)
            {
                if (item.Value is NewsPaper)
                {
                    found = true;
                }
            }
            if (found)
            {
                Console.WriteLine("De kranten uit de leeszaal:");
                foreach (var item in AllReadingRoom)
                {
                    if (item.Value is NewsPaper)
                    {
                        NewsPaper newspaper = (NewsPaper)item.Value;
                        Console.WriteLine($"- {newspaper.Title} van {newspaper.Date.ToString("D")} van uitgeverij {newspaper.Publisher}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Er zijn geen kranten beschikbaar in de leeszaal.");
            }
        }

        public void AcquisitionsReadingRoomToday()
        {
            bool found = false;
            foreach (var item in allReadingRoom)
            {
                if (item.Key.Date == DateTime.Now.Date)
                {
                    found = true;
                }
            }

            if (found)
            {
                Console.WriteLine($"Aanwinsten in de leeszaal van {DateTime.Now.ToString("D")}:");
                foreach (var item in allReadingRoom)
                {
                    Console.WriteLine($"- {item.Value.Title} met id:{item.Value.Identification}");
                }
            } 
            else
            {
                Console.WriteLine("Er zijn geen aanwinsten vandaag.");
            }
        }
    }
}