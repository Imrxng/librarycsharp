using System.Collections.Concurrent;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;

namespace Bib_Imran_Ghaddoura
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Library myLibrary = new Library("Bibliotheek 't Apeke");
            Book.DeserialiseBooks(@"C:\Users\imgha\Documents\AP Hogeschool\OOP\Bib_Imran_Ghaddoura\boeken.csv", myLibrary);
            
            string[] menuOptions = {"Voeg een boek toe", "Voeg informatie toe aan een boek",
                                "Toon de informatie van een boek", "Zoek een boek op", "Verwijder een boek", "Toon alle boeken", "Voeg een krant toe", "Voeg een maandblad toe", "Toon alle kranten", "Toon alle maandbladen", "Toon alle aanwinsten van de leeszaal van de dag", "Ontleen een boek" , "Retourneer een boek.", "Exit" };
            Welcome(myLibrary);
            int menuChoice = 0;
            do
            {
                Console.WriteLine("Gelieve een cijfer van 1 tot 7 te kiezen uit de keuzelijst.");
                for (int i = 0; i < menuOptions.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {menuOptions[i]}");
                }
                try
                {
                    menuChoice = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    ErrorMessage("De ingevoerde waarde was niet in de correcte formaat.");
                }
                Console.Clear();
                switch (menuChoice)
                {
                    case 1:
                        Console.WriteLine("VOEG EEN NIEUWE BOEK TOE AAN DE BIBLIOTHEEK\n");
                        Console.Write("Geef de titel in van het boek: ");
                        string addTitle = Console.ReadLine();
                        Console.Write("Geef de naam van de auteur op: ");
                        string addAuthor = Console.ReadLine();
                        Console.WriteLine();
                        try
                        {
                            Book newbook = new(addTitle, addAuthor, myLibrary);
                        }
                        catch (ArgumentException auEx)
                        {
                            ErrorMessage(auEx.Message);
                        }
                        NextKeyEnterMessage();
                        break;
                    case 2:
                        Console.WriteLine("INFORMATIE TOEVOEGEN AAN EEN BOEK\n");
                        Console.Write("Geef de titel in van het boek: ");
                        string editTitle = Console.ReadLine();
                        Console.Write("Geef de naam van de auteur op: ");
                        string editAuthor = Console.ReadLine();
                        Book editBook = myLibrary.SearchBookWithTitleAndAuthor(editTitle, editAuthor);                        
                        EditBook(editBook);
                        Console.Clear();
                        break;
                    case 3:
                        Console.WriteLine("TOON ALLE INFORMATIE VAN EEN BOEK\n");
                        Console.Write("Geef de titel in van het boek: ");
                        string showTitle = Console.ReadLine();
                        Console.Write("Geef de naam van de auteur op: ");
                        string showAuthor = Console.ReadLine();
                        Book showBook = myLibrary.SearchBookWithTitleAndAuthor(showTitle, showAuthor);
                        if (showBook is not null)
                        {
                            Console.Clear();
                            showBook.ShowInfoBook();
                        }
                        else
                        {
                            ErrorMessage("Boek is niet gevonden");
                        }
                        NextKeyEnterMessage();
                        break;
                    case 4: 
                        SearchBooks(myLibrary);
                        break;
                    case 5:
                        Console.Write("Geef de titel in van het boek: ");
                        string deleteTitle = Console.ReadLine();
                        Console.Write("Geef de naam van de auteur op: ");
                        string deleteAuthor = Console.ReadLine();
                        Console.WriteLine();
                        Book deleteBook = myLibrary.SearchBookWithTitleAndAuthor(deleteTitle, deleteAuthor);
                        myLibrary.DeleteBook(deleteBook);
                        NextKeyEnterMessage();
                        break;
                    case 6:
                        Console.WriteLine("Alle boeken van 't apeke\n");
                        myLibrary.ShowBooks();
                        NextKeyEnterMessage();
                        break;
                    case 7:
                        try
                        {
                            myLibrary.AddNewspaper();
                            Console.Clear();
                            Console.WriteLine("Krant is succesvol toegevoegd aan de leeszaal");
                        }
                        catch (FormatException)
                        {
                            ErrorMessage("De ingevoerde waarde was niet in de correcte formaat.");
                        }
                        finally
                        {
                            NextKeyEnterMessage();
                        }
                        break;
                    case 8:
                        try
                        {
                            myLibrary.AddMagazine();
                            Console.WriteLine("Maandblad is succesvol toegevoegd aan de leeszaal");
                        }
                        catch (FormatException)
                        {
                            ErrorMessage("De ingevoerde waarde was niet in de correcte formaat.");
                        }
                        finally
                        {
                            NextKeyEnterMessage();
                        }                        
                        break;
                    case 9:
                        myLibrary.ShowAllNewspapers();
                        NextKeyEnterMessage();
                        break;
                    case 10:
                        myLibrary.ShowAllMagazines();
                        NextKeyEnterMessage();
                        break;
                    case 11:
                        myLibrary.AcquisitionsReadingRoomToday();
                        NextKeyEnterMessage();
                        break;
                    case 12:
                        Console.WriteLine("LENEN VAN EEN BOEK\n");
                        Console.Write("Geef de titel in van het boek: ");
                        string borrowTitle = Console.ReadLine();
                        Console.Write("Geef de naam van de auteur op: ");
                        string borrowAuthor = Console.ReadLine();
                        Book borrowBook = myLibrary.SearchBookWithTitleAndAuthor(borrowTitle, borrowAuthor);
                        Console.Clear();
                        if (borrowBook is not null)
                        {
                            borrowBook.Borrow();                            
                        }
                        else
                        {
                            ErrorMessage("Boek is niet gevonden");
                        }
                        NextKeyEnterMessage();
                        break;
                    case 13:
                        Console.WriteLine("RETOURNEER EEN BOEK\n");
                        Console.Write("Geef de titel in van het boek: ");
                        string returnTitle = Console.ReadLine();
                        Console.Write("Geef de naam van de auteur op: ");
                        string returnAuthor = Console.ReadLine();
                        Book returnBook = myLibrary.SearchBookWithTitleAndAuthor(returnTitle, returnAuthor);
                        Console.Clear();
                        if (returnBook is not null)
                        {
                            returnBook.Return();
                        }
                        else
                        {
                            ErrorMessage("Boek is niet gevonden");
                        }
                        NextKeyEnterMessage();
                        break;
                    case 14:
                        Console.WriteLine("Bedankt voor uw bezoek aan bibliotheek 'tapeke. Tot ziens!");
                        Console.WriteLine(new string('\n', 20));
                        break;
                    default:
                        ErrorMessage("Incorrecte ingave");
                        NextKeyEnterMessage();
                        break;
                }
            } while (menuChoice != 14);
        }


       
        public static void Welcome(Library myLibrary)
        {
            Console.Write("Hallo! Voer je naam in: ");
            string nameClient = Console.ReadLine();

            while (nameClient.Length < 3)
            {
                Console.Clear();
                ErrorMessage("Verkeerde ingave geef een echte naam in!");
                Console.Write("\nVoer je naam in: ");
                nameClient = Console.ReadLine();
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            string welcomeText = $"Dag {nameClient}, welkom bij {myLibrary.Name} - De Bibliotheek van Verhalen en Kennis!";
            Console.WriteLine(welcomeText);
            Console.ResetColor();
            Console.WriteLine(new string('=', welcomeText.Length) + "\n");
        }


        public static void EditBook(Book editBook)
        {
            string[] propertyOptions = { "ISBN", "Publicatiedatum", "Genre", "Aantal pagina's", "Prijs", "Beschikbaarheid" };
            string again = "";
            do
            {
                if (editBook is not null)
                {
                    Console.Clear();
                    Console.WriteLine("Welke eigenschap wil je toevoegen?");
                    for (int i = 0; i < propertyOptions.Length; i++)
                    {
                        Console.WriteLine($"{i + 1}. {propertyOptions[i]}");
                    }
                    int propertyChoice = 0;
                    try
                    {
                        propertyChoice = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        ErrorMessage("De ingevoerde waarde was niet in de correcte formaat.");
                        NextKeyEnterMessage();
                    }
                    Console.ResetColor();
                    bool failed = false;
                    Console.Clear();
                    switch (propertyChoice)
                    {
                        case 1:
                            try
                            {
                                Console.Write("Gelieve de ISBN nummer in te geven: ");
                                long isbn = Convert.ToInt64(Console.ReadLine());
                                editBook.Isbn = isbn;
                            }
                            catch (FormatException)
                            {
                                ErrorMessage("De ingevoerde waarde was niet in de correcte formaat.");
                            }
                            catch (WrongIsbnException isbnEx)
                            {
                                ErrorMessage(isbnEx.Message);
                            }
                            break;
                        case 2:
                            try
                            {
                                Console.Write("Gelieve de publicatie jaar in te geven: ");
                                int year = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Gelieve de publicatie maand in te geven: ");
                                int month = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Gelieve de publicatie dag in te geven: ");
                                int day = Convert.ToInt32(Console.ReadLine());
                                editBook.PublicationDate = new DateTime(year, month, day);
                            }
                            catch (FormatException)
                            {
                                ErrorMessage("De ingevoerde waarde was niet in de correcte formaat.");
                            }
                            catch (WrongPublicationDateException pubExc)
                            {
                                ErrorMessage(pubExc.Message);
                            }
                            break;
                        case 3:
                            try
                            {
                                ShowAllGenres();
                                Console.Write("\nGelieve de genre in te geven: ");
                                int genreInput = Convert.ToInt32(Console.ReadLine()) - 1;
                                editBook.Genre = (Genres)genreInput;
                            }
                            catch (FormatException)
                            {
                                ErrorMessage("De ingevoerde waarde was niet in de correcte formaat.");
                            }
                            catch (InvalidGenreException gnEx)
                            {
                                ErrorMessage(gnEx.Message);
                            }
                            break;
                        case 4:
                            try
                            {
                                Console.Write("Gelieve de aantal pagina's in te voeren: ");
                                int AmountOfPages = Convert.ToInt32(Console.ReadLine());
                                editBook.PageCount = AmountOfPages;
                            }
                            catch (FormatException)
                            {
                                ErrorMessage("De ingevoerde waarde was niet in de correcte formaat.");
                            }
                            catch (ArgumentException argEx)
                            {
                                ErrorMessage(argEx.Message);
                            }
                            break;
                        case 5:
                            try
                            {
                                Console.Write("Gelieve de prijs van het boek in te geven: ");
                                double priceBook = Convert.ToDouble(Console.ReadLine());
                                editBook.Price = priceBook;
                            }
                            catch (FormatException)
                            {
                                ErrorMessage("De ingevoerde waarde was niet in de correcte formaat.");
                            }
                            break;
                        case 6:
                            Console.Write("Gelieve de beschikbaarheid van het boek in te geven (ja / nee): ");
                            string availability = Console.ReadLine();
                            if (availability.ToLower() == "ja")
                            {
                                editBook.IsAvailable = true;
                            }
                            else if (availability.ToLower() == "nee")
                            {
                                editBook.IsAvailable = false;
                            }
                            else
                            {
                                ErrorMessage("Incorrecte ingave");
                            }
                            break;
                        default:
                            failed = true;
                            break;
                    }
                    if (!failed)
                    {
                        Console.WriteLine();
                        int counter = 0;
                        do
                        {
                            if (counter > 0)
                            {
                                ErrorMessage("Incorrecte ingave");
                            }
                            Console.Write("Wil je nog eens proberen / iets aanpassen? (ja / nee): ");
                            again = Console.ReadLine();
                            counter++;
                            Console.Clear();
                        } while (again.ToLower() != "ja" && again.ToLower() != "nee");
                    }
                }
                else
                {
                    ErrorMessage("Boek is niet gevonden.");
                    NextKeyEnterMessage();
                }
            } while (again.ToLower() == "ja");
        }

        public static void SearchBooks(Library myLibrary)
        {
            string[] searchOptions = {"Zoek volgens titel en auteur" ,"Zoek volgens ISBN nummer",
                                                   "Zoek volgens genre", "Zoek volgens auteur " };
            string again = "";
            do
            {
                Console.Clear();
                Console.WriteLine($"Gelieve een cijfer van 1 tot {searchOptions.Length} te kiezen uit de keuzelijst.");
                for (int i = 0; i < searchOptions.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {searchOptions[i]}");
                }
                int searchChoice = 0;
                try
                {
                    searchChoice = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    ErrorMessage("De ingevoerde waarde was niet in de correcte formaat.");
                }
                Book searchBook;
                List<Book> books = new List<Book>();
                Console.Clear();
                switch (searchChoice)
                {
                    case 1:
                        Console.Write("Geef de titel in van het boek: ");
                        string searchTitle = Console.ReadLine();
                        Console.Write("Geef de naam van de auteur op: ");
                        string searchAuthor = Console.ReadLine();
                        searchBook = myLibrary.SearchBookWithTitleAndAuthor(searchTitle, searchAuthor);
                        Console.Clear();
                        if (searchBook is not null)
                        {
                            Console.WriteLine($"GEVONDEN BOEK\n\nTitel:\t{searchBook.Title}\nAuteur:\t{searchBook.Author}\n");
                        }
                        else
                        {
                            ErrorMessage("Boek niet gevonden");
                        }
                        NextKeyEnterMessage();
                        break;
                    case 2:
                        try
                        {
                            Console.Write("Geef de ISBN in: ");
                            long searchIsbn = Convert.ToInt64(Console.ReadLine());
                            searchBook = myLibrary.SearchBookWithIsbn(searchIsbn);
                            if (searchBook is not null)
                            {
                                Console.WriteLine($"GEVONDEN BOEK\n\nTitel:\t{searchBook.Title}\nAuteur:\t{searchBook.Author}\n");
                            }
                            else
                            {
                                ErrorMessage("Boek niet gevonden");
                            }
                        }
                        catch (FormatException)
                        {
                            ErrorMessage("De ingevoerde waarde was niet in de correcte formaat.");
                        }
                        finally
                        {
                            NextKeyEnterMessage();
                        }
                        Console.Clear();
                        break;
                    case 3:
                        ShowAllGenres();
                        try
                        {
                            Console.Write("\nGeef de genre in: ");
                            int genreInput = Convert.ToInt32(Console.ReadLine()) - 1;
                            Console.Clear();
                            if (Enum.IsDefined(typeof(Genres), genreInput))
                            {
                                books = myLibrary.SearchAllBooksWithGenre((Genres)genreInput);
                                Console.WriteLine($"Alle boeken van het genre {(Genres)genreInput}\n");
                                for (int i = 0; i < books.Count; i++)
                                {
                                    Console.WriteLine($"Boek {i + 1}\nTitel:\t{books[i].Title}\nAuteur:\t{books[i].Author}\n");
                                }
                            }
                            else
                            {
                                throw new InvalidGenreException("Genre is incorrect gelieve deze na te kijken.");
                            }
                        }
                        catch (FormatException)
                        {
                            ErrorMessage("De ingevoerde waarde was niet in de correcte formaat.");
                        }
                        catch (InvalidGenreException geEx)
                        {
                            ErrorMessage(geEx.Message);
                        }
                        finally
                        {
                            NextKeyEnterMessage();
                        }
                        break;
                    case 4:
                        Console.Write("Geef de naam van de auteur op: ");
                        string searchAuthorCase2 = Console.ReadLine();
                        books = myLibrary.SearchAllBooksWithAuthor(searchAuthorCase2);
                        Console.Clear();
                        if (books is not null)
                        {
                            Console.WriteLine($"Alle boeken van {searchAuthorCase2}\n");
                            for (int i = 0; i < books.Count; i++)
                            {
                                Console.WriteLine($"Boek {i + 1}:\nTitel:\t{books[i].Title}\nAuteur:\t{books[i].Author}\n");
                            }
                        }
                        else
                        {
                            ErrorMessage($"Geen boeken gevonden van de opgegeven auteur");
                        }
                        NextKeyEnterMessage();
                        break;
                    default:
                        ErrorMessage("Incorrecte ingave");
                        NextKeyEnterMessage();
                        break;
                }
                int counter = 0;
                do
                {
                    if (counter > 0)
                    {
                        ErrorMessage("Incorrecte ingave");
                    }
                    Console.Write("Wil je nog eens zoeken? (ja / nee): ");
                    again = Console.ReadLine();
                    counter++;
                    Console.Clear();
                } while (again.ToLower() != "ja" && again.ToLower() != "nee");

            } while (again.ToLower() == "ja");
        }

        public static void ErrorMessage(string message)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void ShowAllGenres()
        {
            Genres[] all = (Genres[])Enum.GetValues(typeof(Genres));
            Console.WriteLine("Beschikbare genres in ons bibliotheek:");
            for (int i = 0; i < all.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {all[i]}");
            }
        }

        public static void NextKeyEnterMessage()
        {
            string nextKeyEnter = "Druk op enter om terug naar de hoofdscherm te gaan";
            string starDecoration = (new string('*', nextKeyEnter.Length));
            Console.WriteLine($"\n{starDecoration}\n{nextKeyEnter}\n{starDecoration}");
            Console.ReadLine();
            Console.Clear();
        }

    }
}
