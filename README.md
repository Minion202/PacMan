# Pac-Man mit C# und Avalonia

<img width="700" height="675" alt="Screenshot 2026-08-12 at 21 43 37" src="https://github.com/user-attachments/assets/40b6fff9-496c-486b-a57e-292da6746133" />


## Projektbeschreibung

In diesem Projekt habe ich eine einfache Version von Pac-Man mit **C#, .NET 10 und Avalonia** programmiert. Das Spiel besitzt drei Levels, drei Leben, Punkte und mehrere Geister.

## Spielfeld

Das Spielfeld habe ich mit einem zweidimensionalen Array erstellt. Dabei haben die Zahlen folgende Bedeutung:

```text
0 = leeres Feld
1 = Wand
2 = Punkt
```

So konnte ich die drei Levels einfach als unterschiedliche Labyrinthe erstellen.

## Bewegung

Pac-Man wird mit **WASD oder den Pfeiltasten** gesteuert. Vor jeder Bewegung überprüft das Programm, ob sich eine Wand auf dem nächsten Feld befindet.

Wenn Pac-Man einen Punkt einsammelt, erhält der Spieler 10 Punkte.

## Geister

Die Geister bewegen sich automatisch mit einem `DispatcherTimer`. An Kreuzungen können sie zufällig eine mögliche Richtung auswählen. Dadurch bewegen sie sich nicht immer genau gleich.

## Levels und Leben

Pac-Man startet mit drei Leben. Wenn er einen Geist berührt, verliert er ein Leben und wird zurückgesetzt.

Sobald alle Punkte eingesammelt wurden, startet das nächste Level. Nach dem dritten Level ist das Spiel geschafft.

## Was ich gelernt habe

Bei diesem Projekt konnte ich verschiedene Grundlagen von C# anwenden, besonders **Arrays, Methoden, Klassen, Listen, Bedingungen, Tastatureingaben und Timer**. Ausserdem habe ich gelernt, wie man mit Avalonia eine einfache Benutzeroberfläche für ein Spiel erstellt.

