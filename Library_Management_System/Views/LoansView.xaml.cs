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
    public partial class LoansView : UserControl
    {
        public LoansView()
        {
            InitializeComponent();
            LoadLoans();
        }

        private void LoadLoans()
        {
            var loans = DbHelper.GetAllLoans();

            LoansGrid.ItemsSource = loans;
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TxtSearchMember.Text))
            {
                LoansGrid.ItemsSource = DbHelper.GetLoansByMemberName(TxtSearchMember.Text);
            }
            else if (!string.IsNullOrWhiteSpace(TxtSearchBook.Text))
            {
                LoansGrid.ItemsSource = DbHelper.GetLoansByBookTitle(TxtSearchBook.Text);
            }
            else
            {
                LoadLoans();
            }

            TxtSearchMember.Text = string.Empty;
            TxtSearchBook.Text = string.Empty;
        }

        private void BtnLoan_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new LoanDialog();
            dialog.Owner = Window.GetWindow(this);
            dialog.ShowDialog();
            LoadLoans();
        }

        private void BtnReturn_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void LoansGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
