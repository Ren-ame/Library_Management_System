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
│   └── Loan.cs         ✅ 완료
├── Database/
│   └── DbHelper.cs     ✅ 완료 (Book/Member CRUD, Loan 메서드 미작성)
├── Views/
│   ├── BooksView.xaml + .cs    ✅ 완료
│   ├── MembersView.xaml + .cs  ✅ 완료
│   └── LoansView               ❌ 미작성
├── App.xaml.cs         ✅ SQLitePCL.Batteries_V2.Init() + InitializeDatabase() 호출
└── MainWindow.xaml     ✅ TabControl 3탭 (MembersView 연결 확인 필요)
```

## DB 테이블 구조
- Book: BookId(PK AUTO), Title, Author, ISBN, TotalQuantity, AvailableQuantity
- Member: MemberId(PK AUTO), Name, Phone, Email
- Loan: LoanId(PK AUTO), BookId(FK), MemberId(FK), LoanDate, DueDate, ReturnDate(nullable)

## 공통 패턴 (BooksView/MembersView 동일 구조)
- `LoadXxx()` — DB에서 목록 불러와 DataGrid에 바인딩
- `FillInputs(item)` — 선택된 행 → 텍스트박스 채우기
- `ClearInputs()` — 텍스트박스 초기화
- 수정 버튼: "수정" ↔ "완료" 토글 방식
- SelectionChanged: 완료 모드일 때만 FillInputs 호출
- 삭제: MessageBox YesNo 확인 후 삭제

## 다음 할 일
1. MainWindow.xaml에 MembersView 연결 확인
2. LoansView 생성 및 구현 (대출/반납)
3. DbHelper에 Loan CRUD 추가
4. 검색 기능 추가

## 코딩 스타일
- 코드를 대신 작성하지 말고 설명 후 본인이 직접 작성하도록 유도
- 너무 잘게 쪼개지 말고 적당한 덩어리로 진행
- 파일은 항상 직접 읽어서 확인
- string 초기화는 string.Empty
- 모든 모델/뷰 클래스는 public
