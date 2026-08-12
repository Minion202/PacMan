# Pac-Man mit C# und Avalonia

Dies ist eine einfache Pac-Man-Version, die mit C# und Avalonia erstellt wurde.

## Funktionen

- 3 Levels
- 3 Leben
- Punkte sammeln
- 3 Geister
- Steuerung mit WASD oder Pfeiltasten
- automatische Geisterbewegung mit festen Regeln
- automatischer Wechsel zum nächsten Level

## Starten

1. Den Ordner `PacManSimple` in JetBrains Rider öffnen.
2. Warten, bis Rider die NuGet-Pakete geladen hat.
3. Oben auf den grünen Play-Button drücken.

Es sind keine weiteren Einstellungen nötig.

## Aufbau

Das Projekt wurde absichtlich einfach gehalten.

- `Program.cs` startet das Programm.
- `App.axaml` enthält die Avalonia-Einstellungen.
- `MainWindow.axaml` enthält das Aussehen des Fensters.
- `MainWindow.axaml.cs` enthält die komplette Spiellogik.

## Spielfeld

Das Spielfeld wird mit Zahlen gespeichert.

- `0` bedeutet leer
- `1` bedeutet Wand
- `2` bedeutet Punkt

Dadurch können Levels direkt im Code geändert werden.

## Geister

Die Geister verwenden keine künstliche Intelligenz und keinen Zufall. Sie bewegen sich mit festen Richtungsregeln durch das Labyrinth. Wenn vor ihnen eine Wand ist, nehmen sie die nächste mögliche Richtung.

## Ziel

Das Ziel ist es, alle Punkte in allen drei Levels einzusammeln, ohne alle drei Leben zu verlieren.
