namespace Fcarstat
{
    /// <summary>
    /// An object containing data about the car that is currently being driven.
    /// </summary>
    public class CarData
    {
        public enum CarClass
        {
            Unknown = -1,
            D = 0,
            C = 1,
            B = 2,
            A = 3,
            S1 = 4,
            S2 = 5,
            S3 = 6,
            X = 7,
        };

        public enum DrivetrainType
        {
            Unknown = -1,
            FWD = 0,
            RWD = 1,
            AWD = 2,
        };

        /// <summary>
        /// Unique ID of the car.
        /// </summary>
        public int CarId { get; set; }

        /// <summary>
        /// Raw int parsed from data packet for car class - 0:D, 1:C, etc.
        /// </summary>
        public int CarClassRaw { get; set; }

        /// <summary>
        /// Car class parsed into <see cref="CarClass"/>.
        /// </summary>
        public CarClass CarClassParsed
        {
            get
            {
                if (CarClassRaw > (int)CarClass.X || CarClassRaw < (int)CarClass.D)
                {
                    return CarClass.Unknown;
                }
                
                return (CarClass)CarClassRaw;
            }
        }

        /// <summary>
        /// Performance index.  Lower numbers are slower, higher are faster.
        /// </summary>
        public int CarPerformanceIndex { get; set; }

        /// <summary>
        /// 0 = FWD, 1 = RWD, 2 = AWD
        /// </summary>
        public int DrivetrainRaw { get; set; }

        /// <summary>
        /// Drivetrain type parsed into <see cref="DrivetrainType"/>.
        /// </summary>
        public DrivetrainType DrivetrainTypeParsed
        {
            get
            {
                if (DrivetrainRaw > (int)DrivetrainType.AWD || DrivetrainRaw < (int)DrivetrainType.FWD)
                {
                    return DrivetrainType.Unknown;
                }

                return (DrivetrainType)DrivetrainRaw;
            }
        }

        /// <summary>
        /// Cylinder count of the car's engine
        /// </summary>
        public int CylinderCount { get; set; }

        /// <summary>
        /// 🤷‍
        /// </summary>
        public int CarCategory { get; set; }
    }
}
