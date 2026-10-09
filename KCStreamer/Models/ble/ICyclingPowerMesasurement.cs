using System;
using System.Collections.Generic;
using System.Text;

namespace KCStreamer.Models.ble
{
    internal interface ICyclingPowerMesasurement
    {
        /// <summary>
        /// Die im Datenpaket übergebenen Status-Flags.
        /// </summary>
        CyclingPowerFlags Flags { get; }

        /// <summary>
        /// Aktuelle Leistung in Watt.
        /// </summary>
        short InstantaneousPowerWatts { get; }

        /// <summary>
        /// Kumuliertes Drehmoment in 1/32 Nm (null, wenn Flag nicht gesetzt).
        /// </summary>
        ushort? AccumulatedTorque { get; }

        /// <summary>
        /// Gesamte Radumdrehungen seit Start (null, wenn Flag nicht gesetzt).
        /// </summary>
        uint? CumulativeWheelRevolutions { get; }

        /// <summary>
        /// Zeitstempel der letzten Radumdrehung in 1/2048 Sekunde (null, wenn Flag nicht gesetzt).
        /// </summary>
        ushort? LastWheelEventTime { get; }

        /// <summary>
        /// Gesamte Kurbelumdrehungen/Pedalumdrehungen seit Start (null, wenn Flag nicht gesetzt).
        /// </summary>
        ushort? CumulativeCrankRevolutions { get; }

        /// <summary>
        /// Zeitstempel der letzten Kurbelumdrehung in 1/1024 Sekunde (null, wenn Flag nicht gesetzt).
        /// </summary>
        ushort? LastCrankEventTime { get; }
    }
}
