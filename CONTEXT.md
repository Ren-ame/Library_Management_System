# 도서관리 시스템 - Claude 컨텍스트

## 프로젝트 정보
- 과목: C# WPF 기말 프로젝트
- GitHub: https://github.com/Ren-ame/Library_Management_System

## 기술 스택
- C# WPF (.NET Framework)
- SQLite (Microsoft.Data.Sqlite v10.0.9)

## 프로젝트 구조
```
Library_Management_System/
├── Models/
│   ├── Book.cs         ✅ 완료
│   ├── Member.cs       ✅ 완료
│   └── Loan.cs         ✅ 완료
├── Datebase/           ⚠️ 폴더명 오타 (Database → Datebase) 그대로 유지
│   └── DbHelper.cs     🔄 진행중
├── Views/
│   ├── BooksView.xaml  🔄 진행중 (XAML 완료, 코드비하인드 미완)
│   ├── MembersView     ❌ 미작성
│   └── LoansView       ❌ 미작성
├── App.xaml.cs         ✅ OnStartup에서 DbHelper.InitializeDatabase() 호출
└── MainWindow.xaml     ✅ TabControl 3탭 (도서관리/회원관리/대출반납)
```

## DB 테이블 구조
- Book: BookId(PK AUTO), Title, Author, ISBN, TotalQuantity, AvailableQuantity
- Member: MemberId(PK AUTO), Name, Phone, Email
- Loan: LoanId(PK AUTO), BookId(FK), MemberId(FK), LoanDate, DueDate, ReturnDate(nullable)

## 다음 할 일
1. DbHelper.cs에 GetAllBooks() 메서드 추가 (List<Book> 반환)
2. DbHelper.cs에 AddBook(), UpdateBook(), DeleteBook() 추가
3. BooksView.xaml.cs 코드비하인드 작성 (목록 로드 + 버튼 이벤트)
4. MembersView 생성 및 구현
5. LoansView 생성 및 구현
6. 검색 기능 추가

## 코딩 스타일 참고
- 코드를 대신 작성하지 말고 설명 후 본인이 직접 작성하도록 유도
- 너무 잘게 쪼개지 말고 적당한 덩어리로 진행
- 막히면 파일 직접 읽어서 정확한 위치 알려줄 것
- string 초기화는 string.Empty 사용
- 모든 모델 클래스는 public
