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
├── Database/           ✅ (원래 Datebase 오타였으나 Database로 수정 완료)
│   └── DbHelper.cs     ✅ 완료
├── Views/
│   ├── BooksView.xaml  ✅ 완료 (XAML + 코드비하인드)
│   ├── MembersView     ❌ 미작성
│   └── LoansView       ❌ 미작성
├── App.xaml.cs         ✅ SQLitePCL.Batteries_V2.Init() + InitializeDatabase() 호출
└── MainWindow.xaml     ✅ TabControl 3탭 (도서관리/회원관리/대출반납)
```

## DB 테이블 구조
- Book: BookId(PK AUTO), Title, Author, ISBN, TotalQuantity, AvailableQuantity
- Member: MemberId(PK AUTO), Name, Phone, Email
- Loan: LoanId(PK AUTO), BookId(FK), MemberId(FK), LoanDate, DueDate, ReturnDate(nullable)

## 개발 내역
### 2026-06-24 (01:43 KST)
- DbHelper.cs: GetAllBooks(), AddBook(), UpdateBook(), DeleteBook() 구현 완료
- BooksView.xaml.cs: LoadBooks(), BtnAdd_Click(), BtnDelete_Click() 구현 완료 (BtnEdit_Click은 빈 상태)
- e_sqlite3.dll 누락 문제 해결 → bin\Debug\runtimes\win-x86\native\ 에 수동 복사
- 입력값 예외처리 추가 (int.TryParse, IsNullOrWhiteSpace)
- 도서 추가/삭제 동작 확인 완료

## 다음 할 일
1. BtnEdit_Click 구현 (선택한 행 → 텍스트박스 채우기 → 수정 저장)
2. MembersView 생성 및 구현
3. LoansView 생성 및 구현
4. 검색 기능 추가

## 코딩 스타일 참고
- 코드를 대신 작성하지 말고 설명 후 본인이 직접 작성하도록 유도
- 너무 잘게 쪼개지 말고 적당한 덩어리로 진행
- 막히면 파일 직접 읽어서 정확한 위치 알려줄 것
- string 초기화는 string.Empty 사용
- 모든 모델 클래스는 public
