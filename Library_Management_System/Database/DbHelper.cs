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
        #endregion
    }
}
