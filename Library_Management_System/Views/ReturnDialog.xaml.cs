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
    public partial class ReturnDialog : Window
    {
        private List<string> _allMemberNames;
        private Dictionary<string, Member> _memberMap;
        private bool _isUpdating = false;

        public ReturnDialog()
        {
            InitializeComponent();
            LoadMembers();
            DpReturnDate.SelectedDate = DateTime.Today;
        }

        private void LoadMembers()
        {
            _memberMap = DbHelper.GetAllMembers()
                .ToDictionary(m => m.MemberDisplayName, m => m);
            _allMemberNames = _memberMap.Keys.ToList();
            CmbMemberName.ItemsSource = _allMemberNames;
            CmbMemberName.AddHandler(TextBox.TextChangedEvent, new TextChangedEventHandler(OnMemberTextChanged));
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

        private void CmbMemberName_DropDownClosed(object sender, EventArgs e)
        {
            var selectedName = CmbMemberName.SelectedItem as string;
            if (selectedName != null && _memberMap != null && _memberMap.ContainsKey(selectedName))
            {
                var member = _memberMap[selectedName];
                DgLoans.ItemsSource = DbHelper.GetLoanStateByMemberId(member.MemberId);
            }
            else
            {
                DgLoans.ItemsSource = null;
            }
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            var loans = DgLoans.ItemsSource as List<Loan>;
            var selectedLoans = loans.Where(l => l.IsSelected).ToList();
            var returnDate = DpReturnDate.SelectedDate ?? DateTime.Today;

            foreach (var loan in selectedLoans)
            {
                DbHelper.ReturnLoan(loan.LoanId, loan.BookId, returnDate);
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
