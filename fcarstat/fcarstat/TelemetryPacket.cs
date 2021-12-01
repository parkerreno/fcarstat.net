namespace Fcarstat
{
    using Fcarstat;
    using System.IO;
    using System.Numerics;

    public class TelemetryPacket
    {
        /// <summary>
        /// A sometimes accurate flag that represents when you are in game/ controlling a car.
        /// </summary>
        public bool InGame { get; set; } // src: int32

        /// <summary>
        /// Timestamp in milliseconds.  Can rollover.
        /// </summary>
        public uint Timestamp { get; set; } // src: uint32

        /// <summary>
        /// Engine RPM data for the current car.
        /// </summary>
        public EngineRpmData RpmData { get; set; } // src: floats

        /// <summary>
        /// Acceleration data - X, Y, Z
        /// </summary>
        public Vector3 Acceleration { get; set; } // src floats

        /// <summary>
        /// Velocity data in meters per second.
        /// X = left/right, Y = up/down, Z = forwards/backwards
        /// </summary>
        public Vector3 Velocity { get; set; } // src: floats

        /// <summary>
        /// Angular velocity data - X, Y, Z
        /// </summary>
        public Vector3 AngularVelocity { get; set; } // src: floats

        /// <summary>
        /// X = Yaw, Y = Pitch, Z = Roll
        /// </summary>
        public Vector3 YPR { get; set; } // src: floats

        /// <summary>
        /// 0: stretched, 1: compressed
        /// </summary>
        public WheelProperty<float> SuspensionTravel { get; set; }

        /// <summary>
        /// 0: grip
        /// </summary>
        public WheelProperty<float> TireSlipRatio { get; set; }

        /// <summary>
        /// Wheel rotation speed in [TODO: units]
        /// </summary>
        public WheelProperty<float> WheelRotationSpeed { get; set; }

        /// <summary>
        /// True when wheel is on rumble strip.
        /// </summary>
        public WheelProperty<bool> OnRumbleStrip { get; set; }

        /// <summary>
        /// True when wheel is in a puddle.
        /// </summary>
        public WheelProperty<bool> InPuddle { get; set; }

        public WheelProperty<float> SurfaceRumble { get; set; }

        /// <summary>
        /// Tire slip angle (I believe this is in radians).
        /// </summary>
        public WheelProperty<float> TireSlipAngle { get; set; }

        public WheelProperty<float> TireCombinedSlip { get; set; }

        public WheelProperty<float> SuspensionTravelMeters { get; set; }

        /// <summary>
        /// Information about the car currently being driven.
        /// </summary>
        public CarData CarData { get; set; }

        /// <summary>
        /// Unknown property.  Unknown if being parsed to correct data type.  
        /// If you know what this does, please open an issue and let me know - 
        /// https://github.com/parkerreno/fcarstat.net/issues/new
        /// </summary>
        /// <remarks>You should not use this property as it may be changed/ removed in the future.</remarks>
        public int Unknown1 { get; set; }

        /// <summary>
        /// Unknown property.  Unknown if being parsed to correct data type.  
        /// If you know what this does, please open an issue and let me know - 
        /// https://github.com/parkerreno/fcarstat.net/issues/new
        /// </summary>
        /// <remarks>You should not use this property as it may be changed/ removed in the future.</remarks>
        public int Unknown2 { get; set; }

        /// <summary>
        /// Car's current position.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// Speed in meters per second.
        /// </summary>
        public float Speed { get; set; }

        /// <summary>
        /// Power in watts.
        /// </summary>
        public float Power { get; set; }

        public float Torque { get; set; }

        public WheelProperty<float> TireTemperature { get; set; }

        public float Boost { get; set; }

        public float Fuel { get; set; }

        public float DistanceTravelled { get; set; }

        public float BestLap { get; set; }

        public float LastLap { get; set; }

        public float CurrentLap { get; set; }

        public float CurrentRaceTime { get; set; }

        public uint LapNumber { get; set; } //src:uint16

        public uint RacePosition { get; set; } //src:uint8

        public InputData InputData { get; set; }

        public int Gear { get; set; } //src:uint8 - set after handbrake, before steer

        public int NormalizedDrivingLine { get; set; } //src:int8

        public int NormalizedAiBrakeDifference { get; set; } //src:int8

        public TelemetryPacket(byte[] rawData)
        {
            using (MemoryStream stream = new MemoryStream(rawData))
            using (BinaryReader reader = new BinaryReader(stream))
            {
                InGame = reader.ReadInt32() == 1;
                Timestamp = reader.ReadUInt32();
                RpmData = new EngineRpmData()
                {
                    Maximum = reader.ReadSingle(),
                    Idle = reader.ReadSingle(),
                    Current = reader.ReadSingle(),
                };
                Acceleration = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                Velocity = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                AngularVelocity = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                YPR = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                SuspensionTravel = new WheelProperty<float>(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                TireSlipRatio = new WheelProperty<float>(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                WheelRotationSpeed = new WheelProperty<float>(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                OnRumbleStrip = new WheelProperty<bool>(reader.ReadInt32() > 0, reader.ReadInt32() > 0, reader.ReadInt32() > 0, reader.ReadInt32() > 0);
                InPuddle = new WheelProperty<bool>(reader.ReadInt32() > 0, reader.ReadInt32() > 0, reader.ReadInt32() > 0, reader.ReadInt32() > 0);
                SurfaceRumble = new WheelProperty<float>(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                TireSlipAngle = new WheelProperty<float>(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                TireCombinedSlip = new WheelProperty<float>(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                SuspensionTravelMeters = new WheelProperty<float>(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                CarData = new CarData
                {
                    CarId = reader.ReadInt32(),
                    CarClassRaw = reader.ReadInt32(),
                    CarPerformanceIndex = reader.ReadInt32(),
                    DrivetrainRaw = reader.ReadInt32(),
                    CylinderCount = reader.ReadInt32(),
                    CarCategory = reader.ReadInt32(),
                };
                Unknown1 = reader.ReadInt32();
                Unknown2 = reader.ReadInt32();
                Position = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                Speed = reader.ReadSingle();
                Power = reader.ReadSingle();
                Torque = reader.ReadSingle();
                TireTemperature = new WheelProperty<float>(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                Boost = reader.ReadSingle();
                Fuel = reader.ReadSingle();
                DistanceTravelled = reader.ReadSingle();
                BestLap = reader.ReadSingle();
                LastLap = reader.ReadSingle();
                CurrentLap = reader.ReadSingle();
                CurrentRaceTime = reader.ReadSingle();
                LapNumber = reader.ReadUInt16();
                RacePosition = reader.ReadByte();
                InputData = new InputData()
                {
                    Throttle = reader.ReadByte(),
                    Brake = reader.ReadByte(),
                    Clutch = reader.ReadByte(),
                    Handbrake = reader.ReadByte(),
                };
                Gear = reader.ReadByte();
                InputData.Steer = reader.ReadSByte();
                NormalizedDrivingLine = reader.ReadSByte();
                NormalizedAiBrakeDifference = reader.ReadSByte();
            }
        }
    }
}