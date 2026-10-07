# KCStreamer (Arbeitsname)

Eine .NET MAUI App, um das Bluetooth-Problem mit Smart-Trainern zu lösen.

> 💻 **Hinweis (Windows-First):** Auch wenn .NET MAUI cross-platform-fähig ist, wird das Projekt zunächst **Windows-First** entwickelt. Erst wenn die Kernfunktionalität komplett und fehlerfrei läuft, wird das Projekt auf andere Betriebssysteme ausgeweitet.

---

## 💡 Das Problem & die Vision

Wenn du einen Smart-Trainer (wie den Wahoo KICKR Core) per BLE verbindet, blockiert die Verbindung meistens sofort. Das bedeutet: Du kannst den Trainer nicht gleichzeitig in mehreren Apps oder Tools auf demselben Gerät nutzen. Das nervt und ist unpraktisch!

**KCStreamer** klinkt sich als Brücke bzw. Proxy dazwischen. Die App verbindet sich mit dem Trainer, liest die Sensordaten (Leistung, Trittfrequenz etc.) in Echtzeit aus und stellt sie für parallele Anwendungen (wie Zwift oder Rouvy) zur Verfügung. So kannst du auf mehreren Plattformen gleichzeitig unterwegs sein.

> ⚠️ **Wichtig für Strava-Nutzer:** Vergiss nicht, dass sich beide Plattformen mit Strava synchronisieren und du im Zweifel doppelt so viele Kilometer sammelst! ***Stelle sicher, dass du nur eine Plattform zu Strava synchronisierst.*** Wenn du am Ende des Jahres auf deine gefahrenen Kilometer schaust, möchtest du schließlich einen korrekten Wert haben, oder?

---

## ▶️ Status & geplante Features

***🚧 Aktueller Stand:***
*Die technische Basis steht, aber für den Endanwender gibt es noch keine grafische Oberfläche oder fertige Steuerung.*

- [x] Verbindung des BLE-Devices
- [ ] Anzeigen der Leistungsdaten in einem minimalitischen UI.
- [ ] Bereitstellen der Daten für die Indoor-Cycling-Plattform
- [ ] Steuerung der Leistung durch ausgewählte Plattform

*optionale Features*
- [ ] Einen optionalen Launcher mit Shortcuts der Plattformen



---

## 🛠️ Technischer Stand

- BLE-Suche und Filterung nach dem KICKR Core
- Aufbau der GATT-Verbindung und Daten-Parsing
