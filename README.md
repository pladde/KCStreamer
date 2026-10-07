# KCStreamer

Eine .NET MAUI App, um das Bluetooth-Dilemma mit Smart-Trainern zu lösen.

## Das Problem & die Idee

Wenn man einen Smart-Trainer (wie den Wahoo KICKR Core) per Bluetooth Low Energy (BLE) verbindet, blockiert die Verbindung meistens sofort. Man kann den Trainer dann nicht gleichzeitig in mehreren Apps oder Tools auf einem Gerät nutzen.

KCStreamer klinkt sich als Brücke dazwischen: Die App verbindet sich mit dem Trainer, liest die Sensordaten (Leistung in Watt, Trittfrequenz) in Echtzeit aus und macht sie für parallele Anwendungen (wie Zwift oder Rouvy) verfügbar.

## Technischer Stand

* **BLE-Scanner:** Sucht im Hintergrund gezielt nach dem KICKR Core über den standardisierten Cycling Power Service.
* **Gatt-Verbindung:** Baut die Verbindung auf, aktiviert die Notifications und parst die ankommenden Rohdaten-Bytes in nutzbare Wattzahlen. 
