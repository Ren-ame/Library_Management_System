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

---

## 2026-06-25

### 작업 내용
- Loan.cs: BookTitle, MemberName, MemberPhone 필드 추가, Status 프로퍼티 추가 (실시간 계산)
- DbHelper.cs Loan 영역 추가:
  - GetAllLoans() — JOIN으로 회원명/도서명 포함
  - AddLoan() — AvailableQuantity -1 포함
  - ReturnLoan(loanId, bookId, returnDate) — AvailableQuantity +1 포함
  - GetLoansByMemberName(), GetLoansByBookTitle() — LIKE 검색
  - GetLoanStateByMemberName() — 대출중인 것만 (반납 팝업용)
  - GetMemberByName(), GetBookByTitle() — 대출 등록 시 ID 조회용
- LoansView.xaml: 검색 행 + DataGrid + 오른쪽 버튼 구성 완료
- LoansView.xaml.cs: LoadLoans(), BtnSearch_Click(), BtnLoan_Click() 완료
- MainWindow.xaml: 3탭 모두 연결 완료
- LoanDialog.xaml + .cs: 대출 등록 팝업 완료 (placeholder, DatePicker +7일 자동, 유효성검사)

### 이슈 및 해결
- e_sqlite3.dll 위치: bin\Debug\runtimes\win-x86\native\ 에 수동 복사 필요
- git index.lock 충돌 → 수동 삭제 필요

### 다음 작업
- ReturnDialog.xaml + .cs 작성
- LoansView.xaml.cs BtnReturn_Click 연결
- 빌드 및 전체 테스트

---

## 2026-06-26

### 작업 내용
- ReturnDialog.xaml: ComboBox(IsEditable+IsTextSearchEnabled), DataGrid(CheckBox+도서명+대출일), DatePicker, 버튼 2개 구성 완료
- ReturnDialog.xaml.cs 작성:
  - LoadMembers(): GetAllMembers().Select(m => m.MemberDisplayName) → CmbMemberName.ItemsSource
  - 생성자: LoadMembers() + DpReturnDate.SelectedDate = DateTime.Today
  - CmbMemberName_SelectionChanged: Text.Split('(')[0]으로 이름 추출 → GetLoanStateByMemberName() → DgLoans.ItemsSource
  - BtnConfirm_Click: DgLoans.ItemsSource as List<Loan> → IsSelected 필터 → ReturnLoan(loanId, bookId, returnDate)
  - BtnCancel_Click: this.Close()
- Member.cs: MemberDisplayName 프로퍼티 추가 (Name + Phone 뒷 4자리)
- Loan.cs: IsSelected 프로퍼티 추가 (체크박스 바인딩용)

### 이슈 및 해결
- using System.Windows.Forms 충돌 → 삭제
- DataGrid x:Name이 Dg가 아닌 DgLoans → 코드 수정
- GetLoansByMemberName 대신 GetLoanStateByMemberName 사용 (대출중인 것만 조회)
- ReturnLoan 시그니처: (loanId, bookId, returnDate) — bookId 필수

### 다음 작업
- LoansView.xaml.cs BtnReturn_Click 연결 (ReturnDialog 열고 닫힌 후 LoadLoans())
- 빌드 및 전체 테스트

---

## 2026-06-29

### 작업 내용
- ReturnDialog.xaml.cs 완성:
  - using System.Windows.Forms 제거 (WPF 충돌)
  - _allMemberNames 필드 추가, TextChanged 자동완성 검색 구현 (AddHandler + IsDropDownOpen)
  - GetLoansByMemberName → GetLoanStateByMemberName 수정 (대출중인 것만 표시)
  - DataGridCheckBoxColumn Mode=TwoWay, UpdateSourceTrigger=PropertyChanged 추가 (다중 반납 수정)
  - AutoGenerateColumns="False" 추가 (불필요한 컬럼 제거)
- LoansView.xaml.cs BtnReturn_Click 완성 (ReturnDialog 열고 LoadLoans())
- MainWindow.xaml TabControl_SelectionChanged 추가 (탭 전환 시 BooksView 갱신)
- BooksView.xaml.cs Reload() 추가

### 이슈 및 해결
- 다중 반납 안 되는 문제 → DataGridCheckBoxColumn 바인딩에 TwoWay + UpdateSourceTrigger 추가
- 반납완료 도서가 반납 팝업에 뜨는 문제 → GetLoanStateByMemberName으로 교체
- AutoGenerateColumns 기본값 True → False로 수정
- TextBox 모호한 참조 오류 → using System.Windows.Forms 제거

### 다음 작업
- BooksView CSV 불러오기 기능 (불러오기 버튼 + OpenFileDialog + CSV 파싱)
- exe 변환 (Release 빌드 또는 Publish)

---

## 2026-06-30

### 작업 내용
- BooksView.xaml: 불러오기 버튼 추가 (삭제 버튼 오른쪽, Margin으로 간격)
- BooksView.xaml.cs: BtnImport_Click 구현 (OpenFileDialog → CsvHelper → AddBook → LoadBooks)
- Database/CsvHelper.cs 신규 작성:
  - ImportBooksFromCsv(string filePath): CSV 파싱 → List<Book> 반환
  - ISBN 중복 체크: DbHelper.GetBookByISBN() == null 인 것만 추가
- DbHelper.cs: GetBookByISBN(string isbn) 추가
- books.csv 샘플 파일 생성 (도서 10권, 제목/저자/ISBN/수량)

### 이슈 및 해결
- CSV 중복 추가 문제 → ISBN 기준 중복 체크로 해결

### 다음 작업
- exe 변환 (Release 빌드 또는 Publish)
- 전체 테스트
