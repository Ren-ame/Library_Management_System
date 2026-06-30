# 도서관리 시스템

> C# WPF 기반 도서관리 데스크탑 앱 — 도서/회원/대출반납 관리 (SQLite)

> 작성일: 2026/06/30

## 📌 개요

C# WPF(.NET Framework 4.7.2)로 제작한 도서관 관리 시스템입니다. 도서 등록, 회원 관리, 대출/반납 처리를 하나의 데스크탑 앱에서 관리할 수 있습니다.

## ✨ 주요 기능

- **도서 관리**: 도서 추가/수정/삭제, CSV 파일 일괄 불러오기 (ISBN 중복 체크)
- **회원 관리**: 회원 추가/수정/삭제, 동명이인 구분 (전화번호 뒷 4자리 표시)
- **대출 관리**: 대출 등록, 회원명/도서명 검색, 대출 상태 실시간 표시 (대출중/연체/반납완료)
- **반납 처리**: 회원 자동완성 검색, 다중 도서 동시 반납 (체크박스 선택)

## 🛠 기술 스택

| 분류 | 기술 |
|------|------|
| 언어 | C# |
| UI 프레임워크 | WPF (.NET Framework 4.7.2) |
| 데이터베이스 | SQLite (Microsoft.Data.Sqlite v10.0.9) |
| 패키지 | SQLitePCLRaw.bundle_e_sqlite3 |

## 📁 프로젝트 구조

```
Library_Management_System/
├── Models/
│   ├── Book.cs
│   ├── Member.cs
│   └── Loan.cs              (Status 프로퍼티 실시간 계산)
├── Database/
│   ├── DbHelper.cs          (Book/Member/Loan CRUD)
│   └── CsvHelper.cs         (CSV 파싱 및 ISBN 중복 체크)
├── Views/
│   ├── BooksView.xaml + .cs
│   ├── MembersView.xaml + .cs
│   ├── LoansView.xaml + .cs
│   ├── LoanDialog.xaml + .cs
│   └── ReturnDialog.xaml + .cs
├── App.xaml.cs              (DB 초기화)
└── MainWindow.xaml          (TabControl 3탭)
```

## ⚙️ 설치 및 실행

```
1. Visual Studio에서 솔루션 열기
2. NuGet 패키지 복원
3. 프로젝트 속성 → 빌드 → 플랫폼 대상: x64 설정
4. 빌드 및 실행
```

## 📊 데이터베이스

- DB 파일: `library.db` (실행 경로 기준 자동 생성)

| 테이블 | 주요 컬럼 |
|--------|----------|
| Book | BookId, Title, Author, ISBN, TotalQuantity, AvailableQuantity |
| Member | MemberId, Name, Phone, Email |
| Loan | LoanId, BookId(FK), MemberId(FK), LoanDate, DueDate, ReturnDate |

## 📄 CSV 불러오기 형식

도서관리 탭의 **불러오기** 버튼으로 CSV 파일을 일괄 등록할 수 있습니다. ISBN이 이미 존재하는 도서는 자동으로 스킵됩니다.

```
제목,저자,ISBN,수량
채식주의자,한강,9788936434120,3
어린 왕자,생텍쥐페리,9788932917245,4
```

## 📝 개발 기간

| 날짜 | 내용 |
|------|------|
| 2026-06-24 | 프로젝트 생성, Models, DbHelper, BooksView, MembersView |
| 2026-06-25 | LoansView, LoanDialog, Loan DB 연동 |
| 2026-06-26 | ReturnDialog 구현 |
| 2026-06-29 | 반납 버그 수정, 자동완성 검색, 탭 갱신 |
| 2026-06-30 | CSV 불러오기 기능 추가 |
