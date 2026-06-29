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

        private void FillInputs(Book selectedBook)
        {
            TxtTitle.Text = selectedBook.Title;
            TxtAuthor.Text = selectedBook.Author;
            TxtISBN.Text = selectedBook.ISBN;
            TxtQuantity.Text = selectedBook.TotalQuantity.ToString();
        }

        private void ClearInputs()
        {
            TxtTitle.Text = string.Empty;
            TxtAuthor.Text = string.Empty;
            TxtISBN.Text = string.Empty;
            TxtQuantity.Text = string.Empty;
        }

        public void Reload() => LoadBooks();

        private void BooksGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedBook = (Book)BooksGrid.SelectedItem;

            if (selectedBook != null && BtnEdit.Content.ToString() == "완료")
            {
                FillInputs(selectedBook);
            }
            if (selectedBook == null)
            {
                ClearInputs();
            }
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
            ClearInputs();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (BtnEdit.Content.ToString() == "수정")
            {
                var selectedBook = (Book)BooksGrid.SelectedItem;
                if (selectedBook == null)
                {
                    MessageBox.Show("수정할 책을 선택해주세요.");
                    return;
                }
                
                FillInputs(selectedBook);

                BtnEdit.Content = "완료";
            }
            else
            {
                var selectedBook = (Book)BooksGrid.SelectedItem;

                if (selectedBook != null)
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

                    var updatedBook = new Book
                    {
                        BookId = selectedBook.BookId,
                        Title = TxtTitle.Text,
                        Author = TxtAuthor.Text,
                        ISBN = TxtISBN.Text,
                        TotalQuantity = int.Parse(TxtQuantity.Text),
                        AvailableQuantity = selectedBook.AvailableQuantity // 기존 AvailableQuantity 유지
                    };

                    DbHelper.UpdateBook(updatedBook);
                    LoadBooks();
                    ClearInputs();
                }

                BtnEdit.Content = "수정";
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedBook = (Book)BooksGrid.SelectedItem;

            if (selectedBook != null)
            {
                var result = MessageBox.Show("삭제하시겠습니까?", "확인", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result != MessageBoxResult.Yes) return;

                DbHelper.DeleteBook(selectedBook.BookId);
                LoadBooks();
                ClearInputs();
            }
            else
            {
                MessageBox.Show("선택된 책이 없습니다.");
            }
        }

    }
}
