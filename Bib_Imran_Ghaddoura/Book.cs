using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Bib_Imran_Ghaddoura
{
    internal class Book: ILendable
    {
        private long isbn;

        public long Isbn
        {
            get { return isbn; }
            set 
            {
                if (value.ToString().Length != 13 || string.IsNullOrEmpty(value.ToString()) ||
                (value.ToString().Substring(0, 3) != "978" && value.ToString().Substring(0, 3) != "979") ||
                (value.ToString().Substring(3, 2) != "90" && value.ToString().Substring(3, 2) != "94"))
                {
                    throw new WrongIsbnException("Mislukt! De opgegeven isbn nummer is incorrect, gelieve deze na te kijken.");
                }
                else
                {
                    this.isbn = value;
                }
            }
        }

        private string title;

        public string Title
        {
            get { return title; }
            private set 
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Mislukt! Gelieve een titel voor het boek in te geven.");
                }
                else
                {
                    this.title = value;
                }
            }
        }

        private string author;

        public string Author
        {
            get { return author; }
            private set 
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Mislukt! Gelieve de naam van de auteur in te geven.");
                }
                else
                {
                    this.author = value;
                }
            }
        }

        private DateTime? publicationDate;

        public DateTime? PublicationDate
        {
            get { return publicationDate; }
            set 
            {
                if (value > DateTime.Now)
                {
                    throw new WrongPublicationDateException("\nMislukt! Publicatiedatum van het boek kan niet in de toekomst zijn, gelieve een correcte datum op te geven.");
                }
                else
                {
                    this.publicationDate = value;
                }
            }
        }

        private Genres? genre;

        public Genres? Genre
        {
            get { return genre; }
            set
            {
                if (Enum.IsDefined(typeof(Genres), value))
                {
                    genre = value;
                }
                else
                {
                    throw new InvalidGenreException("Genre is incorrect gelieve deze na te kijken.");
                }
            }
        }

        private int pageCount;

        public int PageCount
        {
            get { return pageCount; }
            set 
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Aantal bladzijden kan niet kleiner of gelijk zijn aan 0");
                }
                pageCount = value; 
            }
        }

        private double price;

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        private bool isAvailable;

        public bool IsAvailable
        {
            get { return isAvailable; }
            set { isAvailable = value; }
        }

        private DateTime borrowingDate;

        public DateTime BorrowingDate
        {
            get { return borrowingDate; }
            set { borrowingDate = value; }
        }

        private int borrowDays;

        public int BorrowDays
        {
            get { return borrowDays; }
            set { borrowDays = value; }
        }


        public Book(string title, string author, Library library)
        {
            this.Title = title; 
            this.Author = author;
            this.Price = 0.0;
            this.IsAvailable = true;
            library.AddBook(this);
        }

        public void ShowInfoBook()
        {
            Console.ResetColor();
            string pattern = "";
            for (int i = 0; i < 120; i++)
            {
                pattern += "*";
            }
            Console.OutputEncoding = Encoding.UTF8;
            string outputInfo = $"{pattern}\n" +
                     $"Titel:".PadRight(25) + $"{this.Title}\n" +
                     $"Auteur:".PadRight(25) + $"{this.Author}\n";
            if (this.Isbn == 0)
            {
                outputInfo += $"ISBN-nummer:".PadRight(25) + $"Ongekend\n";
            }
            else
            {
                outputInfo += $"ISBN-nummer:".PadRight(25) + $"{this.Isbn}\n";
            }
            if (this.Genre is null)
            {
                outputInfo += $"Genre:".PadRight(25) + $"Ongekend\n";
            }
            else
            {
                outputInfo += $"Genre:".PadRight(25) + $"{this.Genre}\n";
            }
            if (this.PublicationDate is null)
            {
                outputInfo += $"Publicatiedatum:".PadRight(25) + $"Ongekend\n";
            }
            else
            {
                outputInfo += $"Publicatiedatum:".PadRight(25) + $"{this.PublicationDate.Value.ToString("dd MMMM yyyy")}\n";
            }
            if (this.PageCount == 0)
            {
                outputInfo += $"Bladzijdes:".PadRight(25) + $"Ongekend\n";
            }
            else
            {
                outputInfo += $"Bladzijdes:".PadRight(25) + $"{this.PageCount}\n";
            }
            outputInfo +=    $"Prijs per huurbeurt:".PadRight(25) + $"{this.Price.ToString("c")}\n" +
                             $"Beschikbaarheid:".PadRight(25);
            Console.Write(outputInfo);
            for (int i = 0; i < 7; i++)
            {
                if (this.isAvailable)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                }
                Console.Write(" ");
            }
            Console.ResetColor();
            Console.Write($"\n{pattern}\n");        
        }

        public static void DeserialiseBooks(string url, Library library)
        {
            string[] allLines = File.ReadAllLines($@"{url}");
            for (int i = 0; i < allLines.Length; i++)
            {
                string[] data = allLines[i].Split(";");
                Book book = new Book(data[0], data[1], library);
                book.Isbn = Convert.ToInt64(data[2]);
                if (Enum.TryParse(data[3], out Genres genre))
                {
                    book.Genre = genre;
                } else
                {
                    Console.WriteLine($"De genre van boek op lijn {i + 1} is incorrect gelieve deze na te kijken.");
                }
                book.PublicationDate = new DateTime(Convert.ToInt32(data[4]), Convert.ToInt32(data[5]), Convert.ToInt32(data[6]));
                book.PageCount = Convert.ToInt32(data[7]);
                book.price = Convert.ToDouble(data[8]);
                book.isAvailable = Convert.ToBoolean(data[9]);
            }
        }

        public void Borrow()
        {
            if (this.isAvailable)
            {
                this.borrowingDate = DateTime.Now;
                this.borrowDays = this.genre == Genres.Schoolboek ? 10 : 20;
                this.isAvailable = false;
                Console.WriteLine($"Het boek is uitgeleend. Het moet uiterlijk op {BorrowingDate.AddDays(BorrowDays).ToShortDateString()} worden teruggebracht.");
            }
            else
            {
                Console.WriteLine("Het boek is op het moment niet beschikbaar.");
            }
        }

        public void Return()
        {
            if (!this.isAvailable)
            {
                this.isAvailable = true;
                DateTime returnDate = DateTime.Now;
                DateTime borrowPeriod = this.borrowingDate.AddDays(this.borrowDays);
                if (returnDate > borrowPeriod)
                {
                    if (Convert.ToInt32(returnDate - borrowPeriod) == 1)
                    {
                        Console.WriteLine($"Het boek is {returnDate - borrowPeriod} dag te laat ingeleverd geweest.");
                    } 
                    else
                    {
                        Console.WriteLine($"Het boek is {returnDate - borrowPeriod} dagen te laat ingeleverd geweest.");
                    }
                }
                else
                {
                    Console.WriteLine("Het boek is op tijd ingeleverd geweest.");
                }
            }
            else
            {
                Console.WriteLine("Dit boek is al ingeleverd");
            }
        }
    }
}
