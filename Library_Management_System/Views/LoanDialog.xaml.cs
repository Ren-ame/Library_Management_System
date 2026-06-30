using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Library_Management_System.Database;
using Library_Management_System.Models;

namespace Library_Management_System.Views
{
    public partial class LoanDialog : Window
    {
        private List<string> _allMemberNames;
        private Dictionary<string, Member> _memberMap;
        private List<Book> _allBooks;
        private bool _isUpdating = false;

        public LoanDialog()
        {
            InitializeComponent();
            LoadComboBoxes();
            DpLoanDate.SelectedDate = DateTime.Today;
            DpDueDate.SelectedDate = DateTime.Today.AddDays(7);
        }

        private void LoadComboBoxes()
        {
            // 회원: DisplayName → Member 매핑 (동명이인 구분)
            _memberMap = DbHelper.GetAllMembers()
                .ToDictionary(m => m.MemberDisplayName, m => m);
            _allMemberNames = _memberMap.Keys.ToList();
            CmbMemberName.ItemsSource = _allMemberNames;
            CmbMemberName.AddHandler(TextBox.TextChangedEvent, new TextChangedEventHandler(OnMemberTextChanged));

            // 도서: 대출 가능한 것만
            _allBooks = DbHelper.GetAllBooks().Where(b => b.AvailableQuantity > 0).ToList();
            DgBooks.ItemsSource = _allBooks;
        }

        private void OnMemberTextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isUpdating) return;
            _isUpdating = true;
            string input = CmbMemberName.Text;
            var filtered = _allMemberNames.Where(n => n.Contains(input)).ToList();
            CmbMemberName.ItemsSource = filtered;
            CmbMemberName.IsDropDownOpen = filtered.Count > 0;
            _isUpdating = false;
        }

        private void TxtSearchBook_TextChanged(object sender, TextChangedEventArgs e)
        {
            string input = TxtSearchBook.Text;
            var filtered = _allBooks.Where(b => b.Title.Contains(input)).ToList();
            DgBooks.ItemsSource = filtered;
        }

        private void DpLoanDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DpLoanDate.SelectedDate.HasValue && DpDueDate != null)
            {
                DpDueDate.SelectedDate = DpLoanDate.SelectedDate.Value.AddDays(7);
            }
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CmbMemberName.Text))
            {
                MessageBox.Show("회원명을 입력해주세요.", "입력 오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var selectedBooks = _allBooks.Where(b => b.IsSelected).ToList();
            if (selectedBooks.Count == 0)
            {
                MessageBox.Show("도서를 1권 이상 선택해주세요.", "입력 오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // DisplayName으로 Member 찾기 (동명이인 구분)
            Member member = null;
            if (_memberMap.ContainsKey(CmbMemberName.Text))
            {
                member = _memberMap[CmbMemberName.Text];
            }
            else
            {
                string name = CmbMemberName.Text.Split('(')[0];
                member = DbHelper.GetMemberByName(name);
            }

            if (member == null)
            {
                MessageBox.Show("존재하지 않는 회원입니다.", "입력 오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            foreach (var book in selectedBooks)
            {
                var loan = new Loan
                {
                    BookId = book.BookId,
                    MemberId = member.MemberId,
                    LoanDate = DpLoanDate.SelectedDate.Value,
                    DueDate = DpDueDate.SelectedDate.Value
                };
                DbHelper.AddLoan(loan);
            }

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
