namespace KCStreamer.Models.ble
{
    /// <summary>
    /// Flags für die Cycling Power Measurement Characteristic (0x2A63) gemäß Bluetooth-Spezifikation.
    /// Diese Flags geben an, welche Daten im Leistungsdatenpaket enthalten sind.
    /// </summary>
    [Flags]
    public enum CyclingPowerFlags : ushort
    {
        None = 0,
        PedalPowerBalancePresent = 1 << 0,
        PedalPowerBalanceReference = 1 << 1,

        AccumulatedTorquePresent = 1 << 2,     
        AccumulatedTorqueSource = 1 << 3,

        WheelRevolutionDataPresent = 1 << 4,   

        CrankRevolutionDataPresent = 1 << 5, 

        ExtremeForceMagnitudesPresent = 1 << 6,
        ExtremeAnglesPresent = 1 << 7,

        TopDeadSpotAnglePresent = 1 << 8,
        BottomDeadSpotAnglePresent = 1 << 9,

        AccumulatedEnergyPresent = 1 << 10,

        OffsetIndicator = 1 << 11
    }
}
