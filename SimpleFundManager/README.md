# README.md — SimpleFundManager

**SimpleFundManager**  
Konsolowa aplikacja do zarządzania funduszami oraz transakcjami, stworzona jako projekt edukacyjny w języku C# (.NET).

## 1. Opis projektu

SimpleFundManager to prosta aplikacja uruchamiana w trybie konsolowym, umożliwiająca:

- Dodawanie funduszy wraz z wartością początkową.
- Dodawanie transakcji powiązanych z wybranym funduszem.
- Automatyczne aktualizowanie wartości funduszu na podstawie wprowadzonych transakcji.
- Wyświetlanie listy funduszy.
- Wyświetlanie transakcji (z paginacją, filtrowaniem i sortowaniem – jeśli zostało dodane).
- Zapisywanie oraz wczytywanie danych z pliku JSON.
- Zapisywanie zdarzeń i błędów w pliku logów.
- Walidację danych wejściowych.

Projekt został podzielony na warstwy, aby zachować czytelność i dobre praktyki programistyczne.

## 2. Wymagania środowiskowe

- **.NET 8.0** lub nowszy
- System **Windows**, **Linux** lub **macOS**
- Dowolne IDE obsługujące C#, np. **Visual Studio 2022** lub **JetBrains Rider**

## 3. Struktura projektu

SimpleFundManager/
│
├── Controller/
│ └── ActionController.cs
│
├── Models/
│ ├── Fund.cs
│ ├── Portfolio.cs
│ └── Transaction.cs
│
├── Services/
│ ├── FundManager.cs
│ ├── LogService.cs
│ ├── ValidationService.cs
│ └── ConsoleColorService.cs
│
├── Program.cs
└── README.md


**Opis najważniejszych elementów:**
- **Models** – klasy danych (Fundusz, Transakcja, Portfel).
- **Services** – logika biznesowa, walidacja, zapisywanie logów.
- **Controller** – obsługa przepływu użytkownika i interakcji.
- **Program.cs** – punkt wejścia aplikacji.

## 4. Funkcjonalności

### 4.1. Zarządzanie funduszami
- Dodawanie nowego funduszu
- Lista istniejących funduszy wraz z bieżącą wartością

### 4.2. Transakcje
- Dodawanie transakcji (wpływy/odpływy)
- Aktualizacja wartości funduszu
- Podgląd transakcji:
  - Ostatnie 20 transakcji
  - Sortowanie od najnowszych
  - Filtrowanie po nazwie funduszu
  - Indeksowanie transakcji

### 4.3. Zapisywanie i odczyt danych
- Zapis do pliku `portfolio.json`
- Automatyczne wczytywanie danych przy starcie

### 4.4. Logowanie
- Plik `log.txt`
- Zapis zdarzeń i błędów z datą i godziną

### 4.5. Walidacja danych wejściowych
- Sprawdzanie poprawności nazw
- Sprawdzanie liczb
- Obsługa błędów z komunikatami dla użytkownika

## 5. Uruchomienie projektu

**Opcja A – Visual Studio**
1. Otwórz rozwiązanie w Visual Studio
2. Upewnij się, że ustawiony jest projekt startowy SimpleFundManager
3. Kliknij **Start (F5)**

**Opcja B – z konsoli**
W katalogu projektu:
dotnet run

## 6. Pliki danych

- **`portfolio.json`**  
  Zawiera zapisane fundusze oraz transakcje w formacie JSON.

- **`log.txt`**  
  Zawiera historię zdarzeń i komunikatów aplikacji:  
  - operacje użytkownika  
  - błędy oraz wyjątki  
  - znaczniki czasowe

## 7. Przykład działania (fragment)

Do you want to add a fund (f), add a transaction (t), show funds (s), show transactions (x), or quit (q)?
Enter fund name:
TechGrowth
Enter starting value:
10000

Fund added.

## 8. Rozszerzenia i rozwój projektu

**Możliwe przyszłe ulepszenia:**
- Eksport danych do CSV
- Interfejs webowy (np. ASP.NET MVC/Blazor)
- Raporty wzrostu funduszy
- Wykresy zmian wartości

## 9. Autor

**Wiktor Staniszewski**  
Projekt edukacyjny wykonany w ramach zajęć programowania w C#.
