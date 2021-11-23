namespace Fcarstat
{
    /// <summary>
    /// An object representing the current input to control the car.
    /// </summary>
    public class InputData
    {
        /// <summary>
        /// 0-255.
        /// </summary>
        public int Throttle { get; set; } //src:uint8 0-255

        /// <summary>
        /// 0-255.
        /// </summary>
        public int Brake { get; set; } //src:uint 0-255

        /// <summary>
        /// 0-255.
        /// </summary>
        public int Clutch { get; set; } //src:uint 0-255

        /// <summary>
        /// 0-255.
        /// </summary>
        public int Handbrake { get; set; } //src:uint 0-255

        /// <summary>
        /// Ranges from -127 to 127.  Negative is left, positive is right.
        /// </summary>
        public int Steer { get; set; }
    }
}
