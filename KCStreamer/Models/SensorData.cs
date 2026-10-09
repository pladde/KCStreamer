using System;
using KCStreamer.Models.ble;

namespace KCStreamer.Models
{
    /// <summary>
    ///  Das SensorData-Objekt repräsentiert die Leistungsdaten, die von einem Bluetooth Low Energy (BLE) Gerät, wie z.B. einem Smart Trainer, empfangen werden. 
    ///  Es implementiert das ICyclingPowerMesasurement-Interface und enthält Informationen über die aktuelle Leistung, kumuliertes Drehmoment, Rad- und Kurbelumdrehungen 
    ///  sowie Zeitstempel der letzten Ereignisse.
    ///     <para><b>Eigenschaften:</b></para>
    ///     <list type="bullet">
    ///         <item><description><see cref="Flags"/> - Die im Datenpaket übergebenen Status-Flags.</description></item>
    ///         <item><description><see cref="InstantaneousPowerWatts"/> - Aktuelle Leistung in Watt.</description></item>
    ///         <item><description><see cref="AccumulatedTorque"/> - Kumuliertes Drehmoment in 1/32 Nm (null, wenn Flag nicht gesetzt).</description></item>
    ///         <item><description><see cref="CumulativeWheelRevolutions"/> - Gesamte Radumdrehungen seit Start (null, wenn Flag nicht gesetzt).</description></item>
    ///         <item><description><see cref="LastWheelEventTime"/> - Zeitstempel der letzten Radumdrehung in 1/2048 Sekunde (null, wenn Flag nicht gesetzt).</description></item>
    ///         <item><description><see cref="CumulativeCrankRevolutions"/> - Gesamte Kurbelumdrehungen/Pedalumdrehungen seit Start (null, wenn Flag nicht gesetzt).</description></item>
    ///         <item><description><see cref="LastCrankEventTime"/> - Zeitstempel der letzten Kurbelumdrehung in 1/1024 Sekunde (null, wenn Flag nicht gesetzt).</description></item>
    ///     </list>
    /// </summary>
    public class SensorData : ICyclingPowerMesasurement
    {
        public CyclingPowerFlags Flags { get; set; }
        public short InstantaneousPowerWatts { get; set; }
        public ushort? AccumulatedTorque { get; set; }
        public uint? CumulativeWheelRevolutions { get; set; }
        public ushort? LastWheelEventTime { get; set; }
        public ushort? CumulativeCrankRevolutions { get; set; }
        public ushort? LastCrankEventTime { get; set; }
    }
}