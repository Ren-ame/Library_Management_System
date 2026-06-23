using Library_Management_System.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.Datebase
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

        public static List<Book> GetAllBooks()
        {
            var books = new List<Book>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
            }
        }
    }

    
}
