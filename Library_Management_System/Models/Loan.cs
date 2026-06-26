using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.Models
{
    public class Loan
    {
        public int LoanId { get; set; }
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public string MemberName { get; set; } = string.Empty;
        public string MemberPhone { get; set; } = string.Empty;
    
        public string Status
        {
            get
            {
                if (ReturnDate.HasValue) return "반납완료";
                if (DateTime.Today > DueDate) return "연체";
                return "대출중";
            }
        }

        public string MemberDisplayName => $"{MemberName}({MemberPhone.Substring(MemberPhone.Length - 4)})";
        public bool IsSelected { get; set; }
    }
}
