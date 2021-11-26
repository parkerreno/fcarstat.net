# fcarstat.net
fcarstat is a .NET Standard library designed to make it easier to consume telemetry data from Forza Horizon 5 (and possibly 4, currently untested). 
*Please note that while this is a .NET Standard library, it requires that the platform you use it on is compatible with the UDP Client.  Some platforms (e.g. Blazor WASM) will not work.*

## Versioning/ Changes
Until version 1.0.0, breaking changes may be shipped in any version.  While I will try to avoid this, it may happen due to rapid development and changes in strategy.
After version 1.0.0, standard versioning practices will be followed with 0.0.x changes having patches 0.x.0 changes containing additive changes, and x.0.0 changes containing breaking changes.
Please use caution when upgrading to preview packages and to major versions.  

## Usage
`ForzaHorizonClient` implements IDisposable, plan accordingly.

```C#
public void Main()
{
    using ForzaHorizonClient client = new ForzaHorizonClient();
    client.ReceivedTelemetryData += Client_ReceivedTelemetryData;
    // If using something like a console app, you will need to add something (like a loop) to keep your code running while the client listens for new data asynchronously.
 }


private void Client_ReceivedTelemetryData(object sender, Fcarstat.TelemetryPacket packet)
{
    // Process new data here 
}
```