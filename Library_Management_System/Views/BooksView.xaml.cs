using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Library_Management_System.Database;
using Library_Management_System.Models;

namespace Library_Management_System.Views
{
    public partial class BooksView : UserControl
    {
        public BooksView()
        {
            InitializeComponent();
            LoadBooks();
        }

        private void LoadBooks()
        {
            var books = DbHelper.GetAllBooks();

            BooksGrid.ItemsSource = books;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtTitle.Text) || string.IsNullOrWhiteSpace(TxtAuthor.Text))
            {
                MessageBox.Show("제목과 저자는 필수 입력 항목입니다.");
                return;
            }

            if (!int.TryParse(TxtQuantity.Text, out int quantity))
            {
                MessageBox.Show("수량은 숫자로 입력해주세요.");
                return;
            }

            var book = new Book
            {
                Title = TxtTitle.Text,
                Author = TxtAuthor.Text,
                ISBN = TxtISBN.Text,
                TotalQuantity = int.Parse(TxtQuantity.Text),
                AvailableQuantity = int.Parse(TxtQuantity.Text)
            };

            DbHelper.AddBook(book);
            LoadBooks();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedBook = (Book)BooksGrid.SelectedItem;

            if (selectedBook != null)
            {
                DbHelper.DeleteBook(selectedBook.BookId);
                LoadBooks();
            }
            else
            {
                MessageBox.Show("선택된 책이 없습니다.");
            }
        }

    }
}
