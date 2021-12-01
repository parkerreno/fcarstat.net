namespace Fcarstat
{
    /// <summary>
    /// A class to represents a property with distinct values for each wheel or tire position.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WheelProperty<T>
    {
        /// <summary>
        /// Creates an instance of <see cref="WheelProperty{T}"/>
        /// </summary>
        /// <param name="frontLeft">Value for front Left wheel/tire.</param>
        /// <param name="frontRight">Value for front right wheel/ tire.</param>
        /// <param name="rearLeft">Value for rear left wheel/ tire.</param>
        /// <param name="rearRight">Value for rear right wheel/tire.</param>
        public WheelProperty(T frontLeft, T frontRight, T rearLeft, T rearRight)
        {
            this.FrontLeft = frontLeft;
            this.FrontRight = frontRight;
            this.RearLeft = rearLeft;
            this.RearRight = rearRight;
        }

        /// <summary>
        /// Data related to the front left wheel or tire.
        /// </summary>
        public T FrontLeft { get; private set; }

        /// <summary>
        /// Data related to the front right wheel or tire. 
        /// </summary>
        public T FrontRight { get; private set; }

        /// <summary>
        /// Data related to the rear left wheel or tire.
        /// </summary>
        public T RearLeft { get; private set; }

        /// <summary>
        /// Data related to the rear right wheel or tire.
        /// </summary>
        public T RearRight { get; private set; }
    }
}
