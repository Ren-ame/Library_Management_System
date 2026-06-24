# 개발 일지

---

## 2026-06-24

### 작업 내용
- 프로젝트 생성 (C# WPF, .NET Framework)
- NuGet 패키지 추가: Microsoft.Data.Sqlite, SQLitePCLRaw.bundle_e_sqlite3
- Models 작성: Book, Member, Loan
- DbHelper 작성: InitializeDatabase(), Book CRUD, Member CRUD
- App.xaml.cs: OnStartup에서 DB 초기화 호출
- MainWindow.xaml: TabControl 3탭 구성 (도서관리/회원관리/대출반납)
- BooksView 구현: XAML + 코드비하인드 (추가/수정/삭제, FillInputs/ClearInputs, 수정 토글)
- MembersView 구현: XAML + 코드비하인드 (추가/수정/삭제, 유효성 검사)

### 이슈 및 해결
- e_sqlite3.dll 누락 → SQLitePCLRaw.bundle_e_sqlite3 NuGet 패키지로 해결
- Platform target Any CPU 오류 → x64로 변경

### 다음 작업
- LoansView 구현 (대출/반납)
- 검색 기능
