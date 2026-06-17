# DOKUMENTACJA PROJEKTU: ECHOES OF THE FOREST

## 1. WPROWADZENIE

Echoes of the Forest to gra przygodowa 3D stworzona w silniku Unity. Gracz wcielając się w głównego bohatera eksploruje otwarty las, rozwiązując puzzle i przetrwając spotkania z dzikimi zwierzętami. Gra stanowi połączenie mechanik survival, adventury i puzzle solvingu.

---

## 2. PRZEGLĄD GĘOWOŚCI

### 2.1 Główne Cechy Gry

- **Eksploracja otwarty świat**: Tereny naturalne do odkrycia
- **System dzień/noc**: Zmienia się zachowanie zwierząt i warunki gry
- **Inteligentna sztuczna inteligencja**: Zwierzęta behawioryzują się różnie w zależności od pory dnia
- **Système zbierania przedmiotów**: Klucze, zapalniczki, komórki energii
- **Puzzle logiczne**: Drzwi, bramy, obiekty interaktywne
- **Mini-gry**: Gra w kości, polowanie
- **System podpowiedzi**: Tekstowe wskazówki dla gracza

### 2.2 Cele Gracza

**Cel główny:**
- Dotarcie do portalu wyjścia (Victory Portal) w celu wygranej gry

**Wyzwania:**
- Unikanie ataków zwierząt w nocy
- Zebranie wymaganych kluczy do przejścia przez drzwi
- Zebranie zapalniczek do rozpalenia ogniska
- Rozwiązanie wszystkich zagadek
- Przetrwanie na otwartym terenie

### 2.3 Warunki Zwycięstwa i Przegranej

**Wygrana:**
- Wejście do Victory Portal (dotarcie do portalu wyjścia)

**Przegrana:**
- Atak zwierzęcia w nocy bez zapalonej pochodni
- Upadek z wysokości
- Wpadnięcie do pułapki (niebezpieczne triggery)

---

## 3. STRUKTURA PROJEKTU

### 3.1 Organizacja Katalogów

```
Gry/
├── Assets/
│   ├── Scripts/                              # Główne skrypty gry
│   │   ├── GameManager.cs                    # Zarządzanie stanem gry
│   │   ├── AnimalAI1.cs                      # AI zwierząt
│   │   ├── Inventory.cs                      # System ekwipunku
│   │   ├── PlayerCollisions.cs               # Kolizje gracza
│   │   ├── CampfireIgniter.cs                # Logika ogniska
│   │   ├── DoorController.cs                 # Kontrola drzwi
│   │   ├── VictoryPortal.cs                  # Portal zwycięstwa
│   │   ├── TimerManager.cs                   # Zarządzanie czasem
│   │   ├── LevelManager.cs                   # Zarządzanie poziomem
│   │   ├── HUDController.cs                  # Interfejs HUD
│   │   ├── Shooting.cs                       # Mechanika strzelania
│   │   ├── TreeHazard.cs                     # Niebezpieczeństwo drzewa
│   │   ├── MainMenuBtns.cs                   # Przyciski menu głównego
│   │   ├── LightFlicker.cs                   # Migotanie światła
│   │   ├── TriggerZone.cs                    # Strefy aktywacji
│   │   ├── TextHints.cs                      # System podpowiedzi
│   │   ├── PlayerPositionRestorer.cs         # Przywracanie pozycji gracza
│   │   ├── Pig game/                         # Podfolder mini-gier
│   │   └── Environment/                      # Skrypty środowiska
│   │
│   ├── Scenes/                               # Scene'y gry
│   │   ├── Start.unity                       # Menu główne
│   │   ├── SampleScene.unity                 # Scena główna/eksploracja
│   │   ├── Scene_HuntingGame_Intro.unity    # Intro gry polowania
│   │   ├── Scene_HuntingGame_Battle.unity   # Walka w grze polowania
│   │   ├── Scene_gambling_game.unity         # Gra w kości
│   │   ├── Scene_GameOver.unity              # Ekran przegranej
│   │   └── WinScene.unity                    # Ekran wygranej
│   │
│   ├── Animations/                           # Animacje i kontrolery
│   │   ├── DoorOpen.anim                     # Animacja otwierania drzwi
│   │   ├── DoorClose.anim                    # Animacja zamykania drzwi
│   │   ├── Idle.anim                         # Animacja stania
│   │   ├── Door04_pr.controller              # Kontroler animacji drzwi
│   │   └── Door04_pr 1.controller
│   │
│   ├── Prefabs/                              # Prefabykaty (gotowe obiekty)
│   │   └── campfire.prefab                   # Prefab ogniska
│   │
│   ├── Terrain/                              # Dane terenu
│   │   ├── New Terrain.asset
│   │   └── New Terrain 1.asset
│   │
│   ├── Materials/                            # Materiały i shadery
│   │   └── [różne materiały]
│   │
│   ├── Starter Assets/                       # Zasoby z Asset Store
│   │   └── [szablony postaci i animacji]
│   │
│   ├── InputSystem_Actions.inputactions      # Mapa wejść (kontrolki)
│   │
│   ├── UI Resources/                         # Zasoby interfejsu
│   │   ├── button.png                        # Tekstura przycisku
│   │   ├── start.png                         # Obraz ekranu startu
│   │   ├── win.png                           # Obraz ekranu wygranej
│   │   ├── śmierć.png                        # Obraz ekranu przegranej
│   │   ├── polowanie.png                     # Obraz polowania
│   │   └── klucz.png                         # Obraz klucza
│   │
│   ├── Hovl Studio/                          # Efekty specjalne
│   ├── Fantasy Forest Environment/           # Środowisko lasu
│   ├── Standard Assets/                      # Standardowe asety
│   ├── TextMesh Pro/                         # System tekstów
│   └── [inne Asset Packs]
│
├── ProjectSettings/                          # Ustawienia projektu Unity
├── Packages/                                 # Pakiety
└── Echoes of the Forest.slnx                 # Solution file Visual Studio
```

---

## 4. OPIS SYSTEMÓW GIER

### 4.1 System Zarządzania Grą (GameManager.cs)

**Odpowiedzialność:**
- Przechowywanie stanu globalnego gry
- Zarządzanie kluczami (huntingKey, gamblingKey, fallingKey)
- Zarządzanie stanem dnia/nocy
- Status pochodni
- Licznik aktualnych atakujących zwierząt
- Ładowanie scen po śmierci

**Kluczowe zmienne:**
```
- hasHuntingKey: bool           # Czy gracz posiada klucz do polowania
- hasGamblingKey: bool          # Czy gracz posiada klucz do gry w kości
- hasFallingKey: bool           # Czy gracz posiada klucz do upadku
- isDay: bool                   # Czy jest dzień czy noc
- isTorchBurning: bool          # Czy pochodnia jest zapalona
- currentAttackersCount: int     # Liczba aktualnie atakujących zwierząt
```

**Główne metody:**
- `UpdateDayStatus(bool)` - Zmienia stan dnia/nocy
- `UpdateTorchStatus(bool)` - Zmienia stan pochodni
- `CheckAndApplyTorchState()` - Sprawdza czy pochodnia powinna być zapalona
- `PlayerDeath()` - Obsługuje śmierć gracza

### 4.2 System AI Zwierząt (AnimalAI1.cs)

**Zachowanie dzień/noc:**

**Dzień:**
- Zwierzęta chodzą powoli (speed = 2)
- Patrulują losowe punkty (patrolRadius = 15)
- Pozostają w stanie Idling
- Nie atakują gracza
- Czuwają 2-6 sekund w każdym punkcie

**Noc:**
- Zwierzęta biegają szybko (speed = 5)
- Maksymalnie 2 zwierzęta mogą atakować jednocześnie
- Jeśli gracz ma zapaloną pochodnię - zwierzę się boi (Scared state, 20-30s)
- Jeśli gracz nie ma pochodni - natychmiast atakuje (Game Over)

**Stany zwierzęcia:**
- `Idling` - Czekanie, patrolowanie
- `Chasing` - Ściganie gracza
- `Scared` - Ucieczka przed pochodnią
- `GameOverTriggered` - Początek sekwencji śmierci

**Kolizje:**
- Trigger w nocy bez pochodni = śmierć (2s anim + Game Over)
- Trigger w nocy z pochodnią = strach zwierzęcia na 20-30s
- Trigger w dzień = brak efektu

### 4.3 System Ekwipunku (Inventory.cs)

**Przedmioty do zbierania:**
- **Power Cells**: Ładują generator (0-5 sztuk)
- **Matches**: Zapalniczki do rozpalenia ogniska (bool)
- **Klucze**: Różne klucze (tracked w GameManager)

**Interfejs HUD:**
- Wyświetlanie ilości baterii
- Wyświetlanie statusu zapalniczek
- Tekstury zmieniają się w zależności od ilości zebranych przedmiotów

**Funkcjonalność ogniska:**
- Gracz musi być w kontakcie z ogniskiem
- Musi mieć zapalniczki
- Po zapaleniu pochodnia palą się przez pozostałość gry

### 4.4 System Kolizji Gracza (PlayerCollisions.cs)

**Typy kolizji:**
- Pułapki (HazardTrigger) - Śmierć
- Zwierzęta (AnimalAI1) - Śmierć (noc bez pochodni)
- Przedmioty - Zbieranie
- Drzwi - Otwieranie/Zamykanie
- Portal - Wygrana

### 4.5 Kontrola Drzwi (DoorController.cs)

**Funkcjonalność:**
- Sprawdzenie czy gracz ma wymagany klucz
- Otwarcie animacji drzwi
- Zablokowanie/Odblokowienie przejścia

### 4.6 Portal Zwycięstwa (VictoryPortal.cs)

**Funkcjonalność:**
- Wejście do portalu = wygrana
- Przejście do sceny WinScene
- Efekty wizualne (cząsteczki, światło)

### 4.7 System Czasu i Dnia/Nocy (TimerManager.cs)

**Funkcjonalność:**
- Symulacja upływu czasu
- Przełączanie między dniem a nocą
- Zmiana oświetlenia
- Zmiana zachowania AI

### 4.8 Interfejs Użytkownika (HUDController.cs)

**Wyświetlane elementy:**
- Liczba baterii
- Status zapalniczek
- Pomoc tekstowe
- Status zdrowia (jeśli dotyczy)
- Czas pozostały

---

## 5. TECHNOLOGIE

### 5.1 Engine i Narzędzia

| Technologia | Wersja | Zastosowanie |
|---|---|---|
| Unity | 2021 LTS+ | Silnik gry |
| C# | .NET | Język programowania (73.9%) |
| Visual Studio | 2019+ | IDE |

### 5.2 Grafika i Rendering

| Technologia | Zastosowanie |
|---|---|
| ShaderLab | 18.1% kodu - Custom shadery |
| HLSL | 3% kodu - Pixel/Vertex shadery |
| URP | Universal Render Pipeline |
| Particle System | Efekty (ogień, dysk) |

### 5.3 Asset Packages

- **Starter Assets**: Postać i animacje gracza
- **Fantasy Forest Environment**: Tereny, rośliny, obiekty
- **TextMesh Pro**: Renderowanie tekstu
- **Hovl Studio Effects**: Efekty specjalne
- **Standard Assets**: Zasoby standardowe
- Różne modele 3D (zwierzęta, obiekty, drzewa)

### 5.4 System Wejścia

- **New Input System**: Nowoczesne API obsługi wejścia
- InputSystem_Actions - Mapowanie klawiszy
- Wsparcie klawiatury, myszy, kontrolerów

---

## 6. ARCHITEKTURA KODU

### 6.1 Wzorce Projektowe

1. **Singleton Pattern** (GameManager)
   - Zapewnia jedną instancję na całą grę
   - Dostęp z każdego skryptu przez `GameManager.instance`

2. **NavMesh Pattern** (AnimalAI1)
   - Nawigacja wzdłuż wyliczonej siatki
   - Automatyczne unikanie przeszkód

3. **State Machine Pattern** (AnimalAI1)
   - Różne stany zachowania (Idling, Chasing, Scared)
   - Przejścia warunkowe między stanami

4. **Event System** (Kolizje, Trigery)
   - OnTriggerEnter/OnCollisionEnter
   - Obsługa interakcji

5. **MVC dla UI** (HUDController)
   - Model - dane (inventory)
   - View - wyświetlenie (HUD GUI)
   - Controller - logika (HUDController)

### 6.2 Organizacja Kodu

- **Separation of Concerns**: Każdy skrypt odpowiada za jedną funkcję
- **DontDestroyOnLoad**: GameManager przetrwa zmianę sceny
- **Public serialized fields**: Konfiguracja z edytora
- **Debug.Log**: Logowanie dla debugowania

---

## 7. FLOW GRY

### 7.1 Sekwencja Startowa

1. Gracz uruchamia aplikację
2. Pojawia się Start.unity (menu główne)
3. Gracz klika "Play"
4. Ładuje się SampleScene.unity (główna mapa)
5. GameManager inicjalizuje stan gry
6. Gracz pojawia się w świecie

### 7.2 Pętla Gameplay'u

```
Inicjalizacja (dzień)
    ↓
Eksploracja, zbieranie przedmiotów
    ↓
Rozwiązywanie puzzli, otwieranie drzwi
    ↓
Upływ czasu
    ↓
Przełączenie na noc
    ↓
Zwierzęta zaczynają atakować
    ↓
Gracz szuka bezpiecznego miejsca
    ↓
Powrót do dnia
    ↓
Powtórz lub dotarcie do portalu (wygrana)
```

### 7.3 Sekwencja Śmierci

1. Zwierzę w nocy bez pochodni dotyka gracza
2. Animacja ataku zwierzęcia (2s)
3. Załadowanie Scene_GameOver
4. Wyświetlenie "YOU DIED" ekranu

### 7.4 Sekwencja Wygranej

1. Gracz dotyka Victory Portal
2. Efekty wizualne portalu
3. Załadowanie WinScene
4. Wyświetlenie "YOU WIN" ekranu

---

## 8. MECHANIKI GIER MINI

### 8.1 Gra Polowania (Hunting Game)

- Scene: Scene_HuntingGame_Intro -> Scene_HuntingGame_Battle
- Mechanika: Strzelanie do celu (zwierzęcia)
- Wymagany: HuntingKey
- Nagroda: Dostęp do dalszych obszarów

**Kontrola:**
- Mysz: Celowanie
- LPM: Strzelanie
- Klawiatura: Ruch

### 8.2 Gra w Kości (Gambling Game)

- Scene: Scene_gambling_game
- Mechanika: Rzut kostkami, gra hazardowa
- Wymagany: GamblingKey
- Nagroda: Dostęp do dalszych obszarów

---

## 9. INTERFEJS UŻYTKOWNIKA

### 9.1 Menu Główne (Start.unity)

**Przyciski:**
- Play - Startuje grę
- Settings - Ustawienia
- Quit - Wyjście z gry

**Wizualne:**
- Tło z grą
- Logo gry
- Przycisk Play wyróżniony

### 9.2 HUD w Grze (SampleScene.unity)

**Górny lewy róg:**
- Liczba zebranych baterii (ikona + liczba)
- Status zapalniczek (ikona)

**Górny prawy róg:**
- Czas pozostały do zmroku/świtu
- Status dnia/nocy (tekst)

**Centrum:**
- Podpowiedzi tekstowe
- Status interaktywnych obiektów

**Dół ekranu:**
- Przyciski Esc - Menu pauzy

### 9.3 Ekran Przegranej (Scene_GameOver.unity)

**Elementy:**
- "YOU DIED" tekst
- Przyczyna śmierci
- Przycisk "Restart"
- Przycisk "Main Menu"

### 9.4 Ekran Wygranej (WinScene.unity)

**Elementy:**
- "YOU WIN" tekst
- "Brawo! Udało ci się przetrwać i uciec!"
- Przycisk "Main Menu"
- Liczba zebranych punktów

---

## 10. DANE TECHNICZNE

### 10.1 Rozmiar Projektu

- **Całkowity rozmiar**: ~765 MB
- **Kod C#**: 73.9%
- **ShaderLab**: 18.1%
- **HLSL**: 3%
- **HTML**: 2.7%
- **Inne**: 2.3%

### 10.2 Wymagania Systemowe

**Minimalne:**
- OS: Windows 7 64-bit / macOS 10.12 / Linux
- Processor: Intel Core i5 lub równoważny
- RAM: 4 GB
- VRAM: 2 GB (karty graficzne obsługujące DirectX 11)
- Storage: 2 GB wolnego miejsca

**Rekomendowane:**
- Processor: Intel Core i7 / AMD Ryzen 5
- RAM: 8 GB
- VRAM: 4 GB (RTX 2060 lub lepsze)

### 10.3 Wydajność

- **Target FPS**: 60 FPS
- **Rozdzielczość**: 1920x1080 (skalowalna)
- **Optimize Settings**: URP z baking oświetlenia

---

## 11. WKŁAD ZESPOŁU

### 11.1 Zespół Projektowy

**Projekt realizowany przez 2 osoby:**
- tamarafyl (Tamara Fyl)
- Binni-pooh (Binny)

### 11.2 Podział Obowiązków

| Obszar | tamarafyl | Binni-pooh |
|---|:---:|:---:|
| Logika gry | 60% | 40% |
| System AI | 30% | 70% |
| Grafika/Shadery | 20% | 80% |
| Animacje | 10% | 90% |
| Design Środowiska | 50% | 50% |
| System Dnia/Nocy | 80% | 20% |
| Interfejs UI | 70% | 30% |
| Testing | 50% | 50% |

### 11.3 Główne Committy

**tamarafyl:**
- "Scenes, instructions" - Przygotowanie scen
- "logika pochodni, animacja dzików, logika animalAI dzień/noc" - Logika podstawowa
- "features" - Dodawanie funkcji
- "fixes" - Poprawki

**Binni-pooh:**
- "Portal and final fixes" - Portal i finały
- "Added Falling things" - Mechanika upadku
- "Added animatons with torch" - Animacje
- "Added pig game" - Mini gra polowania
- "Light and project structure ajustment" - Setup

---

## 12. WERSJONOWANIE

| Wersja | Data | Zmiany |
|---|---|---|
| 1.0 | 2026-06-11 | Release wersji finałowej |
| 0.9 | 2026-06-11 | Portal i finały |
| 0.8 | 2026-06-11 | Mechanika upadku |
| 0.7 | 2026-06-07 | Logika dnia/nocy |
| 0.1 | 2026-05-07 | Inicjalizacja projektu |

---

## 13. ROZSZERZENIA PRZYSZŁE

- Dodanie więcej poziomów i map
- Rozszerzenie AI (nowe zachowania)
- System zdrowia i zasobów
- Multiplayer
- Cross-platform support (mobile)
- Save/Load system
- Modyfikacja trudności
- Dodanie cutscen
- Sound design i muzyka
- Lokalizacja na więcej języków

---

## 14. ZNANE PROBLEMY

- Brak systemu zapisu gry
- Limitowana liczba zwierząt jednocześnie
- Performance na słabszych GPU
- Brak ustawień grafiki

---

## 15. WSKAZÓWKI GRANIA

- Zbieraj wszystkie klucze do otwarcia drzwi
- Zapalniczki znajdziesz w różnych miejscach
- Pochodnia chroni cię nocą
- Słuchaj podpowiedzi tekstowych
- Eksploruj wszystkie tereny
- Mini-gry dają dostęp do nowych obszarów

---

**Dokumentacja v1.0**
**Data: 2026-06-11**
**Autorzy: tamarafyl, Binni-pooh**
