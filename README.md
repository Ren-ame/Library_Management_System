# Library Management System

WPF(.NET Framework 4.7.2) + SQLite 기반 도서관리 시스템입니다.

## 현재 구현 상태 요약

- **도서 관리(BooksView)**: 도서 추가/수정/삭제, 목록 조회
- **회원 관리(MembersView)**: 회원 추가/수정/삭제, 목록 조회
- **대출 관리(LoansView)**:
  - 대출 목록 조회
  - 회원명/도서명 검색
  - `LoanDialog`를 통한 대출 등록
  - 반납 처리 버튼/다이얼로그는 아직 미완성 (`BtnReturn_Click`, `ReturnDialog` 비어 있음)

## 프로젝트 구조

```text
Library_Management_System/
├─ Library_Management_System/
│  ├─ Database/
│  │  └─ DbHelper.cs           # SQLite 초기화, Book/Member/Loan CRUD
│  ├─ Models/
│  │  ├─ Book.cs
│  │  ├─ Member.cs
│  │  └─ Loan.cs               # Status(대출중/연체/반납완료) 계산 프로퍼티
│  ├─ Views/
│  │  ├─ BooksView.xaml(.cs)
│  │  ├─ MembersView.xaml(.cs)
│  │  ├─ LoansView.xaml(.cs)
│  │  ├─ LoanDialog.xaml(.cs)
│  │  └─ ReturnDialog.xaml(.cs)
│  ├─ App.xaml.cs              # 앱 시작 시 SQLite 초기화 + DB 생성
│  └─ MainWindow.xaml          # 탭(도서/회원/대출반납) 구성
└─ Library_Management_System.sln
```

## 데이터베이스

- DB 파일: `library.db` (실행 경로 기준 생성)
- 테이블
  - `Book(BookId, Title, Author, ISBN, TotalQuantity, AvailableQuantity)`
  - `Member(MemberId, Name, Phone, Email)`
  - `Loan(LoanId, BookId, MemberId, LoanDate, DueDate, ReturnDate)`

## 핵심 동작

- 앱 시작 시 `DbHelper.InitializeDatabase()`로 테이블 자동 생성
- 대출 등록 시 `Loan` 추가 + `Book.AvailableQuantity` 1 감소
- 반납 처리 시 `Loan.ReturnDate` 업데이트 + `Book.AvailableQuantity` 1 증가
- `Loan.Status`는 DB 저장값이 아닌 날짜 기반 실시간 계산

## CONTEXT.md/DEVLOG.md 대비 코드 확인 결과

- 문서와 동일하게 대출 목록/검색/대출 등록까지 구현되어 있음
- 문서에서 진행중으로 표시된 반납 기능은 현재 코드에서도 미완성 상태임
- 현재 브랜치는 `origin/copilot/update-readme-with-code-explanations`와 동기화된 상태(로컬 변경 전 기준)

## 빌드 관련 메모 (현재 실행 환경 기준)

- `msbuild` 명령 미설치
- `dotnet msbuild` 사용 시:
  - `Any CPU`: sqlite 패키지 제약으로 실패
  - `x64`: .NET Framework 4.7.2 targeting pack 부재로 실패
