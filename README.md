# MosquitoGame — стартовий скелет проєкту

Це не повністю готовий Unity-проєкт (я не можу запустити сам Unity Editor), а підготовлена структура папок, `Packages/manifest.json` і стартові C#-скрипти під архітектуру, яку ми обговорили в дизайн-документі. Unity Editor сам згенерує решту службових файлів (`Library/`, `Temp/`, `.sln`) при першому відкритті.

## Як відкрити

1. Встанови **Unity Hub**, якщо ще не встановлений.
2. У Unity Hub постав версію **Unity 6 LTS** (`6000.0.x`) — саме таку версію вказано в `ProjectSettings/ProjectVersion.txt`. Якщо в тебе інша LTS-версія 6000.x — Unity Hub запропонує або встановити вказану, або відкрити наявною (це нормально, зайде без проблем).
3. У Unity Hub → **Open** → вибери розпаковану папку `MosquitoGame`.
4. При першому відкритті Unity згенерує `Library/` і підтягне пакети з `Packages/manifest.json` (потрібен інтернет). Це може зайняти кілька хвилин.
5. Якщо Package Manager не знайде точні вказані версії пакетів — просто дозволь йому запропонувати найближчі сумісні; це не критично.

## Що вже є

- **Структура папок** — `Assets/_Project/{Scripts,Prefabs,Scenes,Materials,Audio,Animations,Art}` за архітектурою з дизайн-документа.
- **Скрипти-скелети** (з TODO-коментарями там, де потрібна конкретна реалізація руху/інпуту):
  - `Scripts/Player/MosquitoStats.cs` — резервуар, розмір, гучність.
  - `Scripts/Player/MosquitoController.cs` — стани руху (політ/посадка/повзання).
  - `Scripts/Systems/SoundEmitter.cs` — радіус чутності комара.
  - `Scripts/AI/HumanStateMachine.cs` — повна FSM людини (Сплять → Тривога → Наосліп б'ють / Полюють → Заспокоюється) зі стресом.
  - `Scripts/AI/HumanFieldOfView.cs` — конус зору для полювання зі світлом.
  - `Scripts/Systems/BodyDetectionMeter.cs` — шкала відчуття на тілі з 3-секундним безпечним вікном.
  - `Scripts/UI/ScreenGaugeUI.cs` — резервуар/стрес (Screen Space, `Image` Filled).
  - `Scripts/UI/BodyDetectionGaugeUI.cs` — World Space гейдж біля комара.
- **Пакети в manifest.json**: Input System, URP (для стилізованої мобільної графіки), AI Navigation (NavMesh для патрулювання/полювання людини), TextMeshPro, Timeline.
- `.gitignore`, готовий під стандартний Unity-воркфлоу (Git + опційно Git LFS для важких асетів).

## Що треба зробити тобі/в Unity Editor

- Створити сцену `Bedroom_Level1` в `Assets/_Project/Scenes/`.
- Налаштувати Render Pipeline Asset (URP) в `Assets/Settings/` — Unity Editor створить дефолтний при імпорті пакета URP, просто признач його в Graphics Settings.
- Прив'язати Input Actions (рух/політ/посадка/повзання) через новий Input System — наразі в `MosquitoController` лишені `TODO` під це.
- Імпортувати/змоделювати меблі спальні (ліжко, тумби, шафа, вентиляція) та підключити прості mesh-колайдери під схованки.
- Створити спрайти для fill-барів (суцільний червоний для резервуару, градієнтний зелений→жовтий→червоний для стресу).

## Технічні рішення (нагадування)

- Двигун: **Unity** (не Unreal) — Personal безкоштовний до $200k доходу/рік, Pro — $2,310/місце/рік.
- Платформи: PC (Steam) + Mobile (Android/iOS).
- Розробка: десктоп Ryzen 7 / RTX 5070 — основна машина; MacBook Air M1 — обов'язковий для iOS-білдів.

Повний опис механік і план робіт — дивись `mosquito-game-project-brief.md`, який я створив раніше.
