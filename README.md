## Cel i zakres projektu
Celem projektu jest utworzenie aplikacji webowej służącej jako portal do zamieszczania ogłoszeń przez schroniska, kociarnie, osoby prywatne, itp., z kotami do adopcji, które będą mogły przeglądać niezalogowane osoby prywatne zainteresowane adopcją. Dane kontaktowe umieszczone w ogłoszeniach pozwolą na pośredniczenie w procesie adopcji. 
Na stronie głównej każdego zalogowanego użytkownika wyświetlane będą podstawowe informacje na jego temat, które zostaną automatycznie uzupełnione w momencie utworzenia konta. Przypisana zostanie także rola „User”. Użytkownik będzie miał możliwość edycji danych, hasła oraz usunięcia konta poprzez prywatną podstronę „Manage”. 
Każdy z rolą użytkownika będzie miał możliwość dodawania nowych ogłoszeń z przeznaczonymi im osobnymi podstronami, którymi będzie mógł zarządzać (edytować, usuwać, tworzyć). 
Użytkownik z rolą „Manager” będzie miał możliwość edycji i usuwania podstron zamieszczonych przez użytkowników, a także edycji ich danych. Dostępne dla niego będą specjalne widoki z danymi zarejestrowanych użytkowników , przypisywanie i odbieranie ról użytkownikom, edycję ich danych oraz, oraz wszystkimi ogłoszeniami. Rola „Administrator” będzie umożliwiała usuwanie kont.
Każdy widok aplikacji będzie wyświetlany z uwzględnieniem rozmiaru ekranu urządzenia, na którym zostanie uruchomiony.
## Implementacja:
Aplikacja została stworzona za pomocą aplikacji Microsoft Visual Studio z wersją .Net Core . Wymagane pakiety posiadały wersję nie wyższą niż 6.13.
Aplikację, wraz z bazą danych opublikowano poprzez usługę chmurową Microsoft Azure.
Do stworzenia rejestracji, logowania itd. wykorzystano obiekt szkieletowy Tożsamość. Aby uzyskać tę opcję dodano pakiet Identity poprzez NuGet.
Aplikacja posiada tylko jeden zdefiniowany rodzaj użytkownika: ApplicationUser, który dziedziczy z wygenerowanej wcześniej klasy IdentityUser.
W celu przypisania funkcjonalności do odpowiednich użytkowników stworzono role: Administrator, Manager i User. Odpowiednie role przypisywane są przez administatora na stronie służącej również do zarządzania użytkownikami, do czego mają dostęp administrator i manager. Tę funkcjonalność uzyskano z wykorzystaniem dwóch wzorców projektowych: repozytorium – do zapisywania i wczytywania danych w bazie danych, oraz wzorca Unit of work - ten wzorzec pozwolił zachować pojedynczy kontekst bazy danych.
## Najważniejsze funkcjonalności użytkowników i ról:
1. Gość:
•	rejestracja,
•	logowanie,
•	przeglądanie ogłoszeń,
2. Użytkownik:
•	wylogowywanie,
•	zmiana hasła,
•	edycja danych konta,
•	usunięcie własnego ogłoszenia.
 - Użytkownik (rola User):
  •	dodawanie własnych ogłoszeń,
  •	edycja ogłoszenia,
  •	usunięcie ogłoszenia.
 - Manager (rola):
  •	edycja użytkowników,
  •	przypisywanie i odbieranie ról użytkownikom,
  •	edycja, usuwanie wszystkich ogłoszeń.
 - Administrator (rola):
  •	usuwanie użytkowników.
