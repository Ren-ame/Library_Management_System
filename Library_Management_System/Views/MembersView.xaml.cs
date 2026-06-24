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
    public partial class MembersView : UserControl
    {
        public MembersView()
        {
            InitializeComponent();
            LoadMembers();
        }

        public void LoadMembers()
        {
            var members = DbHelper.GetAllMembers();

            MembersGrid.ItemsSource = members;
        }

        public void FillInputs(Member selectedMember)
        {
            TxtName.Text = selectedMember.Name;
            TxtPhone.Text = selectedMember.Phone;
            TxtEmail.Text = selectedMember.Email;
        }

        public void ClearInputs()
        {
            TxtName.Text = string.Empty;
            TxtPhone.Text = string.Empty;
            TxtEmail.Text = string.Empty;
        }

        private void MembersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedMember = (Member)MembersGrid.SelectedItem;

            if (selectedMember != null && BtnEdit.Content.ToString() == "완료")
            {
                FillInputs(selectedMember);
            }
            if (selectedMember == null)
            {
                ClearInputs();
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtName.Text) || string.IsNullOrWhiteSpace(TxtPhone.Text))
            {
                MessageBox.Show("회원명과 전화번호는 필수 입력 항목입니다.");
                return;
            }

            var newMember = new Member
            {
                Name = TxtName.Text,
                Phone = TxtPhone.Text,
                Email = TxtEmail.Text
            };

            DbHelper.AddMember(newMember);
            LoadMembers();
            ClearInputs();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (BtnEdit.Content.ToString() == "수정")
            {
                var selectedMember = (Member)MembersGrid.SelectedItem;
                if (selectedMember == null)
                {
                    MessageBox.Show("수정할 회원을 선택하세요.");
                    return;
                }

                FillInputs(selectedMember);

                BtnEdit.Content = "완료";
            }
            else
            {
                var selectedMember = (Member)MembersGrid.SelectedItem;

                if (selectedMember != null)
                {
                    if (string.IsNullOrWhiteSpace(TxtName.Text) || string.IsNullOrWhiteSpace(TxtPhone.Text))
                    {
                        MessageBox.Show("회원명과 전화번호는 필수 입력 항목입니다.");
                        return;
                    }

                    var UpdatedMember = new Member
                    {
                        MemberId = selectedMember.MemberId,
                        Name = TxtName.Text,
                        Phone = TxtPhone.Text,
                        Email = TxtEmail.Text
                    };

                    DbHelper.UpdateMember(UpdatedMember);
                    LoadMembers();
                    ClearInputs();

                }

                BtnEdit.Content = "수정";
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedMember = (Member)MembersGrid.SelectedItem;

            if (selectedMember != null)
            {
                var result = MessageBox.Show($"정말로 {selectedMember.Name} 회원을 삭제하시겠습니까?", "회원 삭제", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    DbHelper.DeleteMember(selectedMember.MemberId);
                    LoadMembers();
                    ClearInputs();
                }
            }
            else
            {
                MessageBox.Show("삭제할 회원을 선택하세요.");
            }
        }
    }
}
