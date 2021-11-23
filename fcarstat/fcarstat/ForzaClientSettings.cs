namespace Fcarstat
{
    public class ForzaClientSettings
    {
        /// <summary>
        /// Whether the client fires events on <see cref="ForzaHorizonClient.ReceivedTelemetryData"/> when not in game.
        /// </summary>
        /// <remarks>Default: true</remarks>
        public bool FireEventsWhenNotInGame { get; set; } = true;
    }
}
