<div>

# KCStreamer

[![Status: Alpha](https://img.shields.io/badge/Status-Alpha-orange.svg)]()
[![.NET MAUI](https://img.shields.io/badge/.NET%20MAUI%208.0-512BD4?style=flat&logo=.net&logoColor=white)](https://dotnet.microsoft.com/)
[![Platform: Windows](https://img.shields.io/badge/Platform-Windows-0078D4?style=flat&logo=windows&logoColor=white)](https://www.microsoft.com/windows)
[![Bluetooth LE](https://img.shields.io/badge/Bluetooth-LE-0082FC?style=flat&logo=bluetooth&logoColor=white)](https://www.bluetooth.com/)
[![License: Proprietary](https://img.shields.io/badge/License-Proprietary-red.svg)]()

</div>

Eine .NET MAUI App, um das Bluetooth-Problem mit Smart-Trainern zu lösen.

> 💻 **Hinweis (Windows-First):** Auch wenn .NET MAUI cross-platform-fähig ist, wird das Projekt zunächst **Windows-First** entwickelt. Erst wenn die Kernfunktionalität komplett und fehlerfrei läuft, wird das Projekt auf andere Betriebssysteme ausgeweitet.

---

## 💡 Das Problem & die Vision

Wenn du einen Smart-Trainer (wie den Wahoo KICKR Core) per BLE verbindet, blockiert die Verbindung meistens sofort. Das bedeutet: Du kannst den Trainer nicht gleichzeitig in mehreren Apps oder Tools auf demselben Gerät nutzen. Das nervt und ist unpraktisch!

  ```text
+-----------------------+
|   Smart-Trainer       |
|   (z.B. Wahoo KICKR)  |
+-----------------------+
           |
           | BLE (exklusive Verbindung)
           v
+-----------------------+
|     KCStreamer        |
|     (.NET MAUI)       |
+-----------------------+
           |
           +-----------------------+
           |                       |
           v                       v
+-----------------------+   +-----------------------+
|   Zwift               |   |   Rouvy               |
|   (Parallele App 1)   |   |   (Parallele App 2)   |
+-----------------------+   +-----------------------+
```


**KCStreamer** klinkt sich als Brücke bzw. Proxy dazwischen. Die App verbindet sich mit dem Trainer, liest die Sensordaten (Leistung, Trittfrequenz etc.) in Echtzeit aus und stellt sie für parallele Anwendungen (wie Zwift oder Rouvy) zur Verfügung. So kannst du auf mehreren Plattformen gleichzeitig unterwegs sein.

> ⚠️ **Wichtig für Strava-Nutzer:** Vergiss nicht, dass sich beide Plattformen mit Strava synchronisieren und du im Zweifel doppelt so viele Kilometer sammelst! ***Stelle sicher, dass du nur eine Plattform zu Strava synchronisierst.*** Wenn du am Ende des Jahres auf deine gefahrenen Kilometer schaust, möchtest du schließlich einen korrekten Wert haben, oder?

---

## ▶️ Status & geplante Features

***🚧 Aktueller Stand:***
*Die technische Basis steht, aber für den Endanwender gibt es noch keine grafische Oberfläche oder fertige Steuerung.*

- [x] Verbindung des BLE-Devices
- [x] Anzeigen der Leistungsdaten in einem minimalistischen UI
- [ ] Bereitstellen der Daten für die Indoor-Cycling-Platform
- [ ] Steuerung der Leistung durch ausgewählte Plattform

*Optionale Features*
- [ ] Einen optionalen Launcher mit Shortcuts der Plattformen

---

## 🛠️ Technischer Stand

- BLE-Suche und Filterung nach dem KICKR Core
- Aufbau der GATT-Verbindung und Daten-Parsing
- Die Leistunsgdaten (Watt) werden nun angezeigt

***FIXMEs:***
- Aktuell gibt es immer wieder "Freezes" wenn man nicht tritt. Ich vermute dass das mit dem BLE-Stack zusammenhängt, der die Verbindung verliert und nicht automatisch wiederherstellt.

  
---

## 📄 Lizenz & Nutzungsbedingungen

KCStreamer ist **kostenlose Freeware**. 
* Du darfst die Anwendung privat und für deine Indoor-Cycling-Setups kostenlos nutzen.
* **Der Quellcode ist urheberrechtlich geschützt.** Das Kopieren, Modifizieren, Dekompilieren oder Weiterverbreiten des Codes ohne ausdrückliche Erlaubnis des Autors ist untersagt.

---

## ☕ Support

Wenn dir das Projekt gefällt oder es dir bei deinem Setup hilft, kannst du mich gerne unterstützen: 

<a href="https://www.paypal.com/donate/?hosted_button_id=LLBVA5BDSQKVY" target="_blank"><img src="https://img.shields.io/badge/PayPal-Spenden-blue?style=for-the-badge&logo=paypal" alt="PayPal Spenden"></a>

<a href="https://www.buymeacoffee.com/pladde" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/v2/default-yellow.png" alt="Buy Me A Coffee" height="30" width="130"></a>
