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
using System.Windows.Shapes;
using Library_Management_System.Database;
using Library_Management_System.Models;

namespace Library_Management_System.Views
{
    
    public partial class LoanDialog : Window
    {
        public LoanDialog()
        {
            InitializeComponent();

            DpLoanDate.SelectedDate = DateTime.Today;
            DpDueDate.SelectedDate = DateTime.Today.AddDays(7);
        }

        private void TxtMemberName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TxtMemberName.Text == "회원명을 입력해주세요")
            {
                TxtMemberName.Text = string.Empty;
                TxtMemberName.Foreground = Brushes.Black;
            }
        }

        private void TxtBookName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TxtBookTitle.Text == "도서명을 입력해주세요")
            {
                TxtBookTitle.Text = string.Empty;
                TxtBookTitle.Foreground = Brushes.Black;
            }
        }

        private void DpLoanDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DpLoanDate.SelectedDate.HasValue)
            {
                DpDueDate.SelectedDate = DpLoanDate.SelectedDate.Value.AddDays(7);
            }
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtMemberName.Text) || string.IsNullOrWhiteSpace(TxtBookTitle.Text))
            {
                MessageBox.Show("회원명과 도서명을 입력해주세요.", "입력 오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var member = DbHelper.GetMemberByName(TxtMemberName.Text);
            var book = DbHelper.GetBookByTitle(TxtBookTitle.Text);

            if (member == null)
            {
                MessageBox.Show("존재하지 않는 회원입니다.", "입력 오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (book == null)
            {
                MessageBox.Show("존재하지 않는 도서입니다.", "입력 오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (book.AvailableQuantity <= 0)
            {
                MessageBox.Show("해당 도서는 현재 대출할 수 없습니다.", "대출 불가", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var loan = new Loan
            {
                BookId = book.BookId,
                MemberId = member.MemberId,
                LoanDate = DpLoanDate.SelectedDate.Value,
                DueDate = DpDueDate.SelectedDate.Value
            };

            DbHelper.AddLoan(loan);
            this.DialogResult = true;
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
