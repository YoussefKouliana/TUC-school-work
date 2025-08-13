using Individuellt_programmeringsprojekt;
using System.Data.Entity.Migrations.Model;
using System.Threading.Channels;
using static System.Reflection.Metadata.BlobBuilder;

namespace Individuellt_programmeringsprojekt
{
    public class Program
    {
        private static void Main(string[] args)
        {
            AddSampleBooks(); 

            //Creating the Main Meenu
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n\t***************************************");
                Console.WriteLine("\t*       Welcome to TUC Library        *");
                Console.WriteLine("\t***************************************");
                Console.WriteLine("\n\tPlease select an option from the menu:");
                Console.WriteLine("\n\t1. Register");
                Console.WriteLine("\t2. Login");
                Console.WriteLine("\t3. Exit");
                Console.Write("\n\tEnter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RegisterUser();
                        break;
                    case "2":
                        User loggedInUser = Login();
                        if (loggedInUser != null)
                        {
                            if (loggedInUser.IsAdmin)
                                AdminMenu();
                            else
                                UserMenu(loggedInUser);
                        }
                        break;
                    case "3":
                        Console.WriteLine("\tExiting the system. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("\tInvalid choice. Please try again.");
                        break;
                }
            }
        }

        //Admin Menu
        public static void AdminMenu()
        {
            using (var context = new LibraryDbContext())
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("\n\t***************************************");
                    Console.WriteLine("\t*           Admin Menu                *");
                    Console.WriteLine("\t***************************************");
                    Console.WriteLine("\n\tSelect an option:");
                    Console.WriteLine("\n\t1. Add Book");
                    Console.WriteLine("\t2. Add Multiple Books");
                    Console.WriteLine("\t3. Remove Book");
                    Console.WriteLine("\t4. View All Books");
                    Console.WriteLine("\t5. View All Users");
                    Console.WriteLine("\t6. Group Books By Genre");
                    Console.WriteLine("\t7. View Books by Genre");
                    Console.WriteLine("\t8. Logout");
                    Console.Write("\n\tEnter your choice: ");
                    string choice = Console.ReadLine();


                    switch (choice)
                    {
                        case "1":
                            AddBook(context);
                            break;
                        case "2":
                            AddMultipleBooks(context);
                            break;
                        case "3":
                            RemoveBook(context);
                            break;
                        case "4":
                            ViewAllBooks(context);
                            break;
                        case "5":
                            ViewAllUsers(context);
                            break;
                        case "6":
                            SortBooksByGenre();
                            break;
                        case "7":
                            DisplayBooksByGenre();
                            break;
                        case "8":
                            Console.WriteLine("\tLogging out...");
                            return; 
                        default:
                            Console.WriteLine("\tInvalid choice. Please try again.");
                            break;
                    }

                    Console.WriteLine("\nPress any key to return to the menu...");
                    Console.ReadKey();
                }
            }
        }






        //User Menu
        private static void UserMenu(User currentUser)
        {
            using (var context = new LibraryDbContext())
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("\n\t***************************************");
                    Console.WriteLine($"\t*  Welcome, {currentUser.FirstName} {currentUser.LastName}!  *");
                    Console.WriteLine("\t***************************************");
                    Console.WriteLine("\n\tSelect an option:");
                    Console.WriteLine("\n\t1. Borrow Book");
                    Console.WriteLine("\t2. Return Book");
                    Console.WriteLine("\t3. View Available Books");
                    Console.WriteLine("\t4. Group Books by Genre");
                    Console.WriteLine("\t5. View Books by Genre");
                    Console.WriteLine("\t6. Logout");
                    Console.Write("\n\tEnter your choice: ");
                    
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            BorrowBook(context, currentUser);
                            break;
                        case "2":
                            ReturnBook(context, currentUser);
                            break;
                        case "3":
                            ViewAvailableBooks();
                            break;
                        case "4":
                            SortBooksByGenre();
                            break;
                        case "5":
                            DisplayBooksByGenre();
                            break;
                        case "6":
                            Console.WriteLine("\tLogging out...");
                            return;
                        default:
                            Console.WriteLine("\tInvalid choice. Please try again.");
                            break;
                    }

                    Console.WriteLine("\nPress any key to return to the menu...");
                    Console.ReadKey();
                }
            }
        }


        // Admin methods
        //View all users
        private static void ViewAllUsers(LibraryDbContext context)
        {
            Console.WriteLine("\tAll Registered Users:");

            foreach (var user in context.Users)
            {
                Console.WriteLine($"\tUsername: {user.Username}, Is Admin: {user.IsAdmin}");
            }
        }




        //view all books 
        private static void ViewAllBooks(LibraryDbContext context)
        {
            Console.WriteLine("\tAll Books:");
            foreach (var book in context.Books)
            {
                Console.WriteLine($"\tTitle: {book.Title}, Author: {book.Author}, Genre: {book.Genre}, Available: {book.IsAvailable}");
            }
        }




        //remove books
        private static void RemoveBook(LibraryDbContext context)
        {
            Console.WriteLine("\tEnter the title of the book to remove:");
            string title = Console.ReadLine();

            Book bookToRemove = null;
            foreach (var book in context.Books)
            {
                if (book.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    bookToRemove = book;
                    break;
                }
            }

            if (bookToRemove != null)
            {
                context.Books.Remove(bookToRemove);
                context.SaveChanges();
                Console.WriteLine($"\tBook '{title}' removed successfully.");
            }
            else
            {
                Console.WriteLine("\tBook not found.");
            }
        }


        //Add many books once.
        private static void AddMultipleBooks(LibraryDbContext context)
        {
            Console.WriteLine("\tHow many books do you want to add?");
            if (int.TryParse(Console.ReadLine(), out int count) && count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine($"\t\nAdding book {i + 1} of {count}:");
                    AddBook(context);
                }
            }
            else
            {
                Console.WriteLine("\tInvalid input. Returning to menu.");
            }
        }


        //add a single book
        private static void AddBook(LibraryDbContext context)
        {
            Console.Write("\tEnter book title:");
            string title = Console.ReadLine();

            foreach (var book in context.Books)
            {
                if (book.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"\tA book with the title '{title}' already exists.");
                    return;
                }
            }

            Console.Write("\tEnter book author:");
            string author = Console.ReadLine();

            Console.Write("\tEnter book genre:");
            string genre = Console.ReadLine();

            context.Books.Add(new Book(title, author, genre, true));
            context.SaveChanges();

            Console.WriteLine($"\tBook '{title}' by {author} added successfully.");
        }




        //User methods
        //View the available boooks
        private static void ViewAvailableBooks()
        {
            using (var context = new LibraryDbContext())
            {
                Console.WriteLine("\tAvailable Books:");

                foreach (var book in context.Books.Where(b => b.IsAvailable))
                {
                    Console.WriteLine($"\tTitle: {book.Title}, Author: {book.Author}, Genre: {book.Genre}");
                }
            }
        }



        //return book
        private static void ReturnBook(LibraryDbContext context, User currentUser)
        {
            Console.Write("\tEnter the title of the book to return: ");
            string title = Console.ReadLine();

            Book bookToReturn = context.Books.FirstOrDefault(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase) && !b.IsAvailable);
            if (bookToReturn != null)
            {
                bookToReturn.IsAvailable = true;
                context.SaveChanges();
                Console.WriteLine($"\tThank you, {currentUser.Username}. You returned '{bookToReturn.Title}'.");
            }
            else
            {
                Console.WriteLine("\tBook not found or is already available.");
            }
        }


        //borrow a book
        private static void BorrowBook(LibraryDbContext context, User currentUser)
        {
            Console.Write("\tEnter the title of the book to borrow: ");
            string title = Console.ReadLine();

            Book bookToBorrow = context.Books.FirstOrDefault(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase) && b.IsAvailable);
            if (bookToBorrow != null)
            {
                bookToBorrow.IsAvailable = false;
                context.SaveChanges();
                Console.WriteLine($"\tThank you, {currentUser.Username}. You borrowed '{bookToBorrow.Title}' by {bookToBorrow.Author}.");
            }
            else
            {
                Console.WriteLine("\tBook not found or is not available.");
            }
        }




        //Search or sort books by title
        public static void SearchBookByTitle(List<Book> books)
        {
            Console.Write("\tEnter the title to search:");
            string title = Console.ReadLine();

            var foundBooks = books.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
            if (foundBooks.Count > 0)
            {
                foreach (var book in foundBooks)
                {
                    Console.WriteLine($"\tTitle: {book.Title}, Author: {book.Author}, Available: {book.IsAvailable}");
                }
            }
            else
            {
                Console.WriteLine("\tNo books found.");
            }
        }

        





        //-------------------------------------------------------------------------

        //Registration Method
        private static void RegisterUser()
        {
            using (var context = new LibraryDbContext())
            {
                string firstName = isnotEmpty("\tEnter your first name: ");
                string lastName = isnotEmpty("\tEnter your last name: ");

                // Check for username
                string username;
                while (true)
                {

                    username = isnotEmpty("\tEnter a username: ");

                    // Check if username exists
                    bool usernameExists = false;
                    foreach (var user in context.Users)
                    {
                        if (user.Username.Equals(username, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("\tUsername already exists. Please try again with a different Username");
                            usernameExists = true;
                            break;
                        }
                    }

                    if (!usernameExists)
                    {
                        break; 
                    }
                }

                Console.Write("\tEnter a password:");
                string password = Console.ReadLine();

                Console.Write("\tIs this an admin account? (yes/no)");
                bool isAdmin = Console.ReadLine().Trim().ToLower() == "yes";

                // Save the new user
                context.Users.Add(new User(firstName, lastName, username, password, isAdmin));
                context.SaveChanges();

                Console.WriteLine($"\tUser '{firstName} {lastName}' registered successfully.");
            }
        }


        //method for avoid empty string
        private static string isnotEmpty(string prompt)
        {
            string input;
            do
            {
                Console.Write(prompt); 
                input = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Input cannot be empty. Please try again.");
                }
            } while (string.IsNullOrEmpty(input));

            return input;
        }



        //log in method for both admin and user
        private static User Login()
        {
            using (var context = new LibraryDbContext())
            {
                string username = isnotEmpty("\tEnter your username: ");
                string password = isnotEmpty("\tEnter your password: ");

                // Check if the user exists with the provided username and password
                var user = context.Users
                    .FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && u.Password == password);

                if (user == null)
                {
                    Console.WriteLine("\tUsername or password is not correct.");
                    Console.WriteLine("\n\tPress any key to return to the main menu...");
                    Console.ReadKey();
                    return null;
                }

                // Valid login
                Console.WriteLine($"\tWelcome, {user.FirstName} {user.LastName}!");
               
                return user;
            }
        }


        // adding some books manually to database
        private static void AddSampleBooks()
        {
            using (var context = new LibraryDbContext())
            {
              
                if (!context.Books.Any())
                {
                    
                     var sampleBooks = new List<Book>
            {
                 new Book { Title = "1984", Author = "George Orwell", Genre = "Dystopian", IsAvailable = true },
                 new Book { Title = "To Kill a Mockingbird", Author = "Harper Lee", Genre = "Classic", IsAvailable = true },
                 new Book { Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Genre = "Classic", IsAvailable = true },
                 new Book { Title = "Moby Dick", Author = "Herman Melville", Genre = "Adventure", IsAvailable = true },
                 new Book { Title = "Pride and Prejudice", Author = "Jane Austen", Genre = "Romance", IsAvailable = true },
                 new Book { Title = "The Girl with the Dragon Tattoo", Author = "Stieg Larsson", Genre = "Thriller", IsAvailable = true },
                 new Book { Title = "Gone Girl", Author = "Gillian Flynn", Genre = "Mystery", IsAvailable = true },
                 new Book { Title = "The Da Vinci Code", Author = "Dan Brown", Genre = "Thriller", IsAvailable = true },
                 new Book { Title = "The Da Vinci Code", Author = "Dan Brown", Genre = "Thriller", IsAvailable = true },
                 new Book { Title = "The Book Thief", Author = "Markus Zusak", Genre = "Historical Fiction", IsAvailable = true },
                 new Book { Title = "All the Light We Cannot See", Author = "Anthony Doerr", Genre = "Historical Fiction", IsAvailable = true },
                 new Book { Title = "The Shining", Author = "Stephen King", Genre = "Horror", IsAvailable = true },
                 new Book { Title = "Dracula", Author = "Bram Stoker", Genre = "Horror", IsAvailable = true },
                 new Book { Title = "Frankenstein", Author = "Mary Shelley", Genre = "Horror", IsAvailable = true },   
                 new Book { Title = "Sapiens: A Brief History of Humankind", Author = "Yuval Noah Harari", Genre = "Non-Fiction", IsAvailable = true },
                 new Book { Title = "Educated", Author = "Tara Westover", Genre = "Memoir", IsAvailable = true },
                 new Book { Title = "The Diary of a Young Girl", Author = "Anne Frank", Genre = "Memoir", IsAvailable = true },
                 new Book { Title = "Thinking, Fast and Slow", Author = "Daniel Kahneman", Genre = "Psychology", IsAvailable = true },
                 new Book { Title = "The Notebook", Author = "Nicholas Sparks", Genre = "Romance", IsAvailable = true },
                 new Book { Title = "Me Before You", Author = "Jojo Moyes", Genre = "Romance", IsAvailable = true },
             };

                    foreach (var book in sampleBooks)
                    {
                        // Check if the book already exists in the database
                        bool bookExists = context.Books.Any(b =>
                            b.Title == book.Title &&
                            b.Author == book.Author &&
                            b.Genre == book.Genre);

                        if (!bookExists)
                        {
                            context.Books.Add(book);
                        }
                    }

                    context.SaveChanges();
                    Console.WriteLine("Books have been seeded successfully (if not already present).");
                }
                else
                {
                    Console.WriteLine("\n\tBooks already exist in the database.");
                }
            }
        }
        private static void DisplayBooksByGenre()
        {
            using (var context = new LibraryDbContext())
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("\n\t***************************************");
                    Console.WriteLine("\t*        Select a Genre to View       *");
                    Console.WriteLine("\t***************************************");
                    Console.WriteLine("\n\t1. Romance");
                    Console.WriteLine("\t2. Classic");
                    Console.WriteLine("\t3. Science Fiction");
                    Console.WriteLine("\t4. Fantasy");
                    Console.WriteLine("\t5. Dystopian");
                    Console.WriteLine("\t6. Adventure");
                    Console.WriteLine("\t7. Thriller");
                    Console.WriteLine("\t8. Mystery");
                    Console.WriteLine("\t9. Horror");
                    Console.WriteLine("\t10. Historical Fiction");
                    Console.WriteLine("\t11. Non-Fiction");
                    Console.WriteLine("\t12. Memoir");
                    Console.WriteLine("\t13. Psychology");
                    Console.WriteLine("\t14. Exit");
                    Console.Write("\n\tEnter your choice: ");

                    string choice = Console.ReadLine();
                    string genre = null;

                    switch (choice)
                    {
                        case "1":
                            genre = "Romance";
                            break;
                        case "2":
                            genre = "Classic";
                            break;
                        case "3":
                            genre = "Science Fiction";
                            break;
                        case "4":
                            genre = "Fantasy";
                            break;
                        case "5":
                            genre = "Dystopian";
                            break;
                        case "6":
                            genre = "Adventure";
                            break;
                        case "7":
                            genre = "Thriller";
                            break;
                        case "8":
                            genre = "Mystery";
                            break;
                        case "9":
                            genre = "Horror";
                            break;
                        case "10":
                            genre = "Historical Fiction";
                            break;
                        case "11":
                            genre = "Non-Fiction";
                            break;
                        case "12":
                            genre = "Memoir";
                            break;
                        case "13":
                            genre = "Psychology";
                            break;
                        case "14":
                            Console.WriteLine("\tReturning to the main menu...");
                            return;
                        default:
                            Console.WriteLine("\tInvalid choice. Please try again.");
                            Console.ReadKey();
                            continue;
                    }

                    
                    var books = context.Books
                        .Where(b => b.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase))
                        .ToList();

                    if (!books.Any())
                    {
                        Console.WriteLine($"\n\tNo books found in the {genre} genre.");
                    }
                    else
                    {
                        Console.WriteLine($"\n\tBooks in the {genre} genre:\n");

                        foreach (var book in books)
                        {
                            Console.WriteLine($"\tTitle: {book.Title}");
                            Console.WriteLine($"\tAuthor: {book.Author}");
                            Console.WriteLine($"\tAvailable: {(book.IsAvailable ? "Yes" : "No")}");
                            Console.WriteLine("\t------------------------------");
                        }
                    }

                    Console.WriteLine("\n\tPress any key to return to the genre menu...");
                    Console.ReadKey();
                }
            }
        }

        private static void SortBooksByGenre()
        {
            using (var context = new LibraryDbContext())
            {
                var booksByGenre = context.Books
                    .OrderBy(b => b.Genre)
                    .ToList();

                if (!booksByGenre.Any())
                {
                    Console.WriteLine("\n\tNo books found in the database.");
                    return;
                }

                Console.WriteLine("\n\tBooks Grouped by Genre:\n");


                // Group books by genre
                foreach (var group in booksByGenre.GroupBy(b => b.Genre))
                {
                    Console.WriteLine($"\tGenre: {group.Key}");
                    Console.WriteLine("\t------------------------------");

                    foreach (var book in group)
                    {
                        Console.WriteLine($"\tTitle: {book.Title}");
                        Console.WriteLine($"\tAuthor: {book.Author}");
                        Console.WriteLine($"\tAvailable: {(book.IsAvailable ? "Yes" : "No")}");
                        Console.WriteLine("\t------------------------------");
                    }

                    Console.WriteLine(); 
                }

                Console.WriteLine("\n\tPress any key to return to the menu...");
                Console.ReadKey();
            }
        }



    }
}