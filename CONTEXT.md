# 도서관리 시스템 - Claude 컨텍스트

> 이 파일은 Claude가 다른 세션/컴퓨터에서 프로젝트 상황을 파악하기 위한 파일입니다.

## 프로젝트 정보
- 과목: C# WPF 기말 프로젝트
- GitHub: https://github.com/Ren-ame/Library_Management_System
- 로컬 경로: `D:\2026_Hitech\github\C# WPF 기말\Library_Management_System`

## 기술 스택
- C# WPF (.NET Framework)
- SQLite (Microsoft.Data.Sqlite v10.0.9 + SQLitePCLRaw.bundle_e_sqlite3)

## 프로젝트 구조
```
Library_Management_System/
├── Models/
│   ├── Book.cs         ✅ 완료
│   ├── Member.cs       ✅ 완료
│   └── Loan.cs         ✅ 완료 (Status 프로퍼티 포함, 실시간 계산)
├── Database/
│   └── DbHelper.cs     ✅ 완료
│       - Book CRUD + GetBookByTitle()
│       - Member CRUD + GetMemberByName()
│       - Loan: GetAllLoans(), AddLoan(), ReturnLoan()
│       - Loan 검색: GetLoansByMemberName(), GetLoansByBookTitle()
│       - 반납팝업용: GetLoanStateByMemberName()
├── Views/
│   ├── BooksView.xaml + .cs    ✅ 완료
│   ├── MembersView.xaml + .cs  ✅ 완료
│   ├── LoansView.xaml + .cs    🔄 진행중 (BtnReturn_Click 미완)
│   ├── LoanDialog.xaml + .cs   ✅ 완료
│   └── ReturnDialog.xaml + .cs ✅ 완료
├── App.xaml.cs         ✅ SQLitePCL.Batteries_V2.Init() + InitializeDatabase() 호출
└── MainWindow.xaml     ✅ TabControl 3탭 모두 연결 완료
```

## DB 테이블 구조
- Book: BookId(PK AUTO), Title, Author, ISBN, TotalQuantity, AvailableQuantity
- Member: MemberId(PK AUTO), Name, Phone, Email
- Loan: LoanId(PK AUTO), BookId(FK), MemberId(FK), LoanDate, DueDate, ReturnDate(nullable)

## Loan 특이사항
- Status는 DB 저장 안 함. Loan.cs에서 ReturnDate/DueDate 기준 실시간 계산
- AddLoan() 시 AvailableQuantity -1, ReturnLoan() 시 +1 자동 처리
- 회원명 동명이인 구분: MemberPhone 뒷 4자리 표시 (예: 김기범(5673))
- 날짜 표시: yy-MM-dd 포맷

## 다음 할 일
1. BooksView.xaml — 삭제 버튼 오른쪽에 불러오기 버튼 추가 (Margin으로 간격)
2. BooksView.xaml.cs — BtnImport_Click 구현 (OpenFileDialog → CSV 파싱 → AddBook → LoadBooks)
   - CSV 형식: 제목,저자,ISBN,수량 (첫 줄 헤더 스킵)
   - using Microsoft.Win32 추가 필요
3. exe 변환: 빌드 → Release 모드 → bin\Release 폴더에 exe 생성 (또는 Publish로 단일 파일)

## 코딩 스타일
- 코드를 대신 작성하지 말고 설명 후 본인이 직접 작성하도록 유도
- 너무 잘게 쪼개지 말고 적당한 덩어리로 진행
- 파일은 항상 직접 읽어서 확인
- string 초기화는 string.Empty
- 모든 모델/뷰 클래스는 public
