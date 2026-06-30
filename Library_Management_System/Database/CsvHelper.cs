using Library_Management_System.Models;
using System.Collections.Generic;
using System.IO;


namespace Library_Management_System.Database
{
    public static class CsvHelper
    {
        public static List<Book> ImportBooksFromCsv(string filePath)
        {
            var books = new List<Book>();

            using (var reader = new StreamReader(filePath))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    var book = new Book
                    {
                        Title = values[0],
                        Author = values[1],
                        ISBN = values[2],
                        TotalQuantity = int.Parse(values[3].Trim()),
                        AvailableQuantity = int.Parse(values[3].Trim())
                    };
                    if (DbHelper.GetBookByISBN(book.ISBN) == null)
                    {
                        books.Add(book); 
                    }
                }
            }

            return books;
        }
    }
}