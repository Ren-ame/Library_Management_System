using Library_Management_System.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.Database
{
    public class DbHelper
    {
        private static string _connectionString = "Data Source=library.db";

        public static void InitializeDatabase()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var command = new SqliteCommand(
                    @"CREATE TABLE IF NOT EXISTS Book(
                        BookId INTEGER PRIMARY KEY AUTOINCREMENT,
                        Title TEXT NOT NULL,
                        Author TEXT NOT NULL,
                        ISBN TEXT,
                        TotalQuantity INTEGER,
                        AvailableQuantity INTEGER
                    )", connection);
                command.ExecuteNonQuery();

                command = new SqliteCommand(
                    @"CREATE TABLE IF NOT EXISTS Member(
                        MemberId INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Phone TEXT,
                        Email TEXT
                    )", connection);
                command.ExecuteNonQuery();

                command = new SqliteCommand(
                    @"CREATE TABLE IF NOT EXISTS Loan(
                        LoanId INTEGER PRIMARY KEY AUTOINCREMENT,
                        BookId INTEGER NOT NULL,
                        MemberId INTEGER NOT NULL,
                        LoanDate TEXT NOT NULL,
                        DueDate TEXT NOT NULL,
                        ReturnDate TEXT
                    )", connection);
                command.ExecuteNonQuery();
            } 
        }

        #region Book
        public static List<Book> GetAllBooks()
        {
            var books = new List<Book>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var command = new SqliteCommand("SELECT * FROM Book", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        books.Add(new Book
                        {
                            BookId = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Author = reader.GetString(2),
                            ISBN = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                            TotalQuantity = reader.GetInt32(4),
                            AvailableQuantity = reader.GetInt32(5)
                        });
                    }
                }
            }

            return books;
        }

        public static void AddBook(Book book)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var command = new SqliteCommand(
                    @"INSERT INTO Book (Title, Author, ISBN, TotalQuantity, AvailableQuantity)
                      VALUES (@Title, @Author, @ISBN, @TotalQuantity, @AvailableQuantity)",
                    connection);

                command.Parameters.AddWithValue("@Title", book.Title);
                command.Parameters.AddWithValue("@Author", book.Author);
                command.Parameters.AddWithValue("@ISBN", book.ISBN);
                command.Parameters.AddWithValue("@TotalQuantity", book.TotalQuantity);
                command.Parameters.AddWithValue("@AvailableQuantity", book.AvailableQuantity);

                command.ExecuteNonQuery();
            }
        }

        public static void UpdateBook(Book book)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var command = new SqliteCommand(
                    @"UPDATE Book 
                      SET Title = @Title, Author = @Author, ISBN = @ISBN, 
                      TotalQuantity = @TotalQuantity, AvailableQuantity = @AvailableQuantity
                      WHERE BookId = @BookId",
                    connection);

                command.Parameters.AddWithValue("@Title", book.Title);
                command.Parameters.AddWithValue("@Author", book.Author);
                command.Parameters.AddWithValue("@ISBN", book.ISBN);
                command.Parameters.AddWithValue("@TotalQuantity", book.TotalQuantity);
                command.Parameters.AddWithValue("@AvailableQuantity", book.AvailableQuantity);
                command.Parameters.AddWithValue("@BookId", book.BookId);

                command.ExecuteNonQuery();
            }
        }

        public static void DeleteBook(int BookId)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var command = new SqliteCommand(
                    @"DELETE FROM Book WHERE BookId = @BookId",
                    connection);

                command.Parameters.AddWithValue("@BookId", BookId);

                command.ExecuteNonQuery();
            }
        }

        public static Book GetBookByTitle(string title)
        {
            Book book = null;
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = new SqliteCommand(
                    @"SELECT * FROM Book WHERE Title = @Title",
                    connection);
                command.Parameters.AddWithValue("@Title", title);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        book = new Book
                        {
                            BookId = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Author = reader.GetString(2),
                            ISBN = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                            TotalQuantity = reader.GetInt32(4),
                            AvailableQuantity = reader.GetInt32(5)
                        };
                    }
                }
            }
            return book;
        }

        public static Book GetBookByISBN(string isbn)
        {
            Book book = null;
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = new SqliteCommand(
                    @"SELECT * FROM Book WHERE ISBN = @ISBN",
                    connection);
                command.Parameters.AddWithValue("@ISBN", isbn);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        book = new Book
                        {
                            BookId = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Author = reader.GetString(2),
                            ISBN = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                            TotalQuantity = reader.GetInt32(4),
                            AvailableQuantity = reader.GetInt32(5)
                        };
                    }
                }
            }
            return book;
        }
        #endregion

        #region Member
        public static List<Member> GetAllMembers()
        {
            var members = new List<Member>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var command = new SqliteCommand("SELECT * FROM Member", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        members.Add(new Member
                        {
                            MemberId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Phone = reader.GetString(2),
                            Email = reader.IsDBNull(3) ? string.Empty : reader.GetString(3)
                        });
                    }
                }
            }

            return members;
        }

        public static void AddMember(Member member)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = new SqliteCommand(
                    @"INSERT INTO Member (Name, Phone, Email)
                      VALUES (@Name, @Phone, @Email)",
                    connection);

                command.Parameters.AddWithValue("@Name", member.Name);
                command.Parameters.AddWithValue("@Phone", member.Phone);
                command.Parameters.AddWithValue("@Email", member.Email);

                command.ExecuteNonQuery();
            }
        }

        public static void UpdateMember(Member member)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var command = new SqliteCommand(
                    @"UPDATE Member
                      SET Name = @Name, Phone = @Phone, Email = @Email
                      WHERE MemberId = @MemberId",
                    connection);

                command.Parameters.AddWithValue("@Name", member.Name);
                command.Parameters.AddWithValue("@Phone", member.Phone);
                command.Parameters.AddWithValue("@Email", member.Email);
                command.Parameters.AddWithValue("@MemberId", member.MemberId);

                command.ExecuteNonQuery();
            }
        }

        public static void DeleteMember(int MemberId)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = new SqliteCommand(
                    @"DELETE FROM Member WHERE MemberId = @MemberId",
                    connection);

                command.Parameters.AddWithValue("@MemberId", MemberId);

                command.ExecuteNonQuery();
            }
        }

        public static Member GetMemberByName(string name)
        {
            Member member = null;
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = new SqliteCommand(
                    @"SELECT * FROM Member WHERE Name = @Name",
                    connection);
                command.Parameters.AddWithValue("@Name", name);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        member = new Member
                        {
                            MemberId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Phone = reader.GetString(2),
                            Email = reader.IsDBNull(3) ? string.Empty : reader.GetString(3)
                        };
                    }
                }
            }
            return member;
        }
        #endregion

        #region Loan
        public static List<Loan> GetAllLoans()
        {
            var loans = new List<Loan>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var command = new SqliteCommand(
                    @"SELECT Loan.*, Book.Title, Member.Name, Member.Phone
                      FROM Loan
                      JOIN Book ON Loan.BookId = Book.BookId
                      JOIN Member ON Loan.MemberId = Member.MemberId",
                    connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        loans.Add(new Loan
                        {
                            LoanId = reader.GetInt32(0),
                            BookId = reader.GetInt32(1),
                            MemberId = reader.GetInt32(2),
                            LoanDate = DateTime.Parse(reader.GetString(3)),
                            DueDate = DateTime.Parse(reader.GetString(4)),
                            ReturnDate = reader.IsDBNull(5) ? (DateTime?)null : DateTime.Parse(reader.GetString(5)),
                            BookTitle = reader.GetString(6),
                            MemberName = reader.GetString(7),
                            MemberPhone = reader.GetString(8)
                        });
                    }
                }
            }

            return loans;
        }

        public static void AddLoan(Loan loan)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var command = new SqliteCommand(
                    @"INSERT INTO Loan (BookId, MemberId, LoanDate, DueDate, ReturnDate)
                      VALUES (@BookId, @MemberId, @LoanDate, @DueDate, @ReturnDate)",
                    connection);

                command.Parameters.AddWithValue("@BookId", loan.BookId);
                command.Parameters.AddWithValue("@MemberId", loan.MemberId);
                command.Parameters.AddWithValue("@LoanDate", loan.LoanDate.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@DueDate", loan.DueDate.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@ReturnDate", loan.ReturnDate.HasValue ? loan.ReturnDate.Value.ToString("yyyy-MM-dd") : (object)DBNull.Value);

                command.ExecuteNonQuery();

                var updateCommand = new SqliteCommand(
                    @"UPDATE Book SET AvailableQuantity = AvailableQuantity - 1
                      WHERE BookId = @BookId",
                    connection);

                updateCommand.Parameters.AddWithValue("@BookId", loan.BookId);

                updateCommand.ExecuteNonQuery();
            }
        }

        public static void ReturnLoan(int loanId, int bookId, DateTime returnDate)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var command = new SqliteCommand(
                    @"UPDATE Loan SET ReturnDate = @ReturnDate
                      WHERE LoanId = @LoanId",
                    connection);

                command.Parameters.AddWithValue("@ReturnDate", returnDate.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@LoanId", loanId);

                command.ExecuteNonQuery();

                var updateCommand = new SqliteCommand(
                    @"UPDATE Book SET AvailableQuantity = AvailableQuantity + 1
                      WHERE BookId = @BookId",
                    connection);

                updateCommand.Parameters.AddWithValue("@BookId", bookId);

                updateCommand.ExecuteNonQuery();
            }
        }


        public static List<Loan> GetLoansByMemberName(string name)
        {
            var loans = new List<Loan>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var command = new SqliteCommand(
                    @"SELECT Loan.*, Book.Title, Member.Name, Member.Phone
                      FROM Loan
                      JOIN Book ON Loan.BookId = Book.BookId
                      JOIN Member ON Loan.MemberId = Member.MemberId
                      WHERE Member.Name LIKE @Name",
                    connection);

                command.Parameters.AddWithValue("@Name", "%" + name + "%");

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        loans.Add(new Loan
                        {
                            LoanId = reader.GetInt32(0),
                            BookId = reader.GetInt32(1),
                            MemberId = reader.GetInt32(2),
                            LoanDate = DateTime.Parse(reader.GetString(3)),
                            DueDate = DateTime.Parse(reader.GetString(4)),
                            ReturnDate = reader.IsDBNull(5) ? (DateTime?)null : DateTime.Parse(reader.GetString(5)),
                            BookTitle = reader.GetString(6),
                            MemberName = reader.GetString(7),
                            MemberPhone = reader.GetString(8)
                        });
                    }
                }
            }

            return loans;
        }

        public static List<Loan> GetLoansByBookTitle(string title)
        {
            var loans = new List<Loan>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = new SqliteCommand(
                    @"SELECT Loan.*, Book.Title, Member.Name, Member.Phone
                      FROM Loan
                      JOIN Book ON Loan.BookId = Book.BookId
                      JOIN Member ON Loan.MemberId = Member.MemberId
                      WHERE Book.Title LIKE @Title",
                    connection);
                command.Parameters.AddWithValue("@Title", "%" + title + "%");
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        loans.Add(new Loan
                        {
                            LoanId = reader.GetInt32(0),
                            BookId = reader.GetInt32(1),
                            MemberId = reader.GetInt32(2),
                            LoanDate = DateTime.Parse(reader.GetString(3)),
                            DueDate = DateTime.Parse(reader.GetString(4)),
                            ReturnDate = reader.IsDBNull(5) ? (DateTime?)null : DateTime.Parse(reader.GetString(5)),
                            BookTitle = reader.GetString(6),
                            MemberName = reader.GetString(7),
                            MemberPhone = reader.GetString(8)
                        });
                    }
                }
            }

            return loans;
        }

        public static List<Loan> GetLoanStateByMemberName(string name)
        {
            var loans = new List<Loan>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = new SqliteCommand(
                    @"SELECT Loan.*, Book.Title, Member.Name, Member.Phone
                      FROM Loan
                      JOIN Book ON Loan.BookId = Book.BookId
                      JOIN Member ON Loan.MemberId = Member.MemberId
                      WHERE Member.Name LIKE @Name AND Loan.ReturnDate IS NULL",
                    connection);
                command.Parameters.AddWithValue("@Name", "%" + name + "%");
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        loans.Add(new Loan
                        {
                            LoanId = reader.GetInt32(0),
                            BookId = reader.GetInt32(1),
                            MemberId = reader.GetInt32(2),
                            LoanDate = DateTime.Parse(reader.GetString(3)),
                            DueDate = DateTime.Parse(reader.GetString(4)),
                            ReturnDate = reader.IsDBNull(5) ? (DateTime?)null : DateTime.Parse(reader.GetString(5)),
                            BookTitle = reader.GetString(6),
                            MemberName = reader.GetString(7),
                            MemberPhone = reader.GetString(8)
                        });
                    }
                }
            }

            return loans;
        }

        public static List<Loan> GetLoanStateByMemberId(int memberId)
        {
            var loans = new List<Loan>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = new SqliteCommand(
                    @"SELECT Loan.*, Book.Title, Member.Name, Member.Phone
                      FROM Loan
                      JOIN Book ON Loan.BookId = Book.BookId
                      JOIN Member ON Loan.MemberId = Member.MemberId
                      WHERE Loan.MemberId = @MemberId AND Loan.ReturnDate IS NULL",
                    connection);
                command.Parameters.AddWithValue("@MemberId", memberId);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        loans.Add(new Loan
                        {
                            LoanId = reader.GetInt32(0),
                            BookId = reader.GetInt32(1),
                            MemberId = reader.GetInt32(2),
                            LoanDate = DateTime.Parse(reader.GetString(3)),
                            DueDate = DateTime.Parse(reader.GetString(4)),
                            ReturnDate = reader.IsDBNull(5) ? (DateTime?)null : DateTime.Parse(reader.GetString(5)),
                            BookTitle = reader.GetString(6),
                            MemberName = reader.GetString(7),
                            MemberPhone = reader.GetString(8)
                        });
                    }
                }
            }

            return loans;
        }
        #endregion
    } 
}