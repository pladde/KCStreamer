# KCStreamer (Arbeitsname)

Eine .NET MAUI App, um das Bluetooth-Problem mit Smart-Trainern zu lösen.

> **Hinweis:** Auch wenn .NET MAUI cross-platform fähig ist, wird das Projekt **Windows-First** entwickelt. Erst wenn die Kernfunktionalität komplett und fehlerfrei läuft, werde ich das Projekt auf die anderen OS ausweiten.


## 💡 Das Problem & die Vision

Wenn man einen Smart-Trainer (wie den Wahoo KICKR Core) per BLE verbindet, blockiert die Verbindung meistens sofort. D.h. man kann den Trainer dann nicht gleichzeitig in mehreren Apps oder Tools auf einem Gerät nutzen. Das nervt und ist unpraktisch!

KCStreamer klinkt sich als Brücke/Proxy dazwischen. Die App verbindet sich mit dem Trainer, liest die Sensordaten (Leistung, Trittfrequenz etc) in Echtzeit aus und macht sie für parallele Anwendungen (wie Zwift oder Rouvy) verfügbar.

Somit kannst du auf mehreren Plattformen gleichzeitig unterwegs sein.

> **Falls du Strava nutzt:** Vergiss nicht, dass sich beide Plattformen mit Strava synchronisieren und du im Zweifel doppelt so viele KM sammelst! ***Stelle sicher, dass du nur eine Plattform zu Strava synchronisierst.*** Wenn du am Ende des Jahres auf deine gefahrenen Kilometer schaust, möchtest du doch keinen gelogenen Wert haben, oder?


## 🛠️ Technischer Stand

- **BLE-Scanner:** Sucht im Hintergrund gezielt nach dem KICKR Core über den standardisierten Cycling Power Service. 
- **GATT-Verbindung:** Baut die Verbindung auf, aktiviert die Notifications und parst die ankommenden Rohdaten-Bytes in nutzbare Wattzahlen. 
