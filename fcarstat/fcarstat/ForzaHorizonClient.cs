namespace Fcarstat
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    public class ForzaHorizonClient : IDisposable
    {
        private struct UdpState
        {
            public UdpClient client;
            public IPEndPoint endpoint;
        }

        private UdpClient udpClient;
        private IPEndPoint ipEndpoint;
        private TelemetryPacket previousPacket;
        private bool continueListening;

        public event EventHandler<TelemetryPacket> ReceivedTelemetryData;

        public ForzaHorizonClient(int port = 5005)
        {
            ipEndpoint = new IPEndPoint(IPAddress.Any, port);
            udpClient = new UdpClient(ipEndpoint);
        }

        public void StartListening()
        {
            UdpState state = new UdpState()
            {
                client = udpClient,
                endpoint = ipEndpoint,
            };

            continueListening = true;

            udpClient.BeginReceive(new AsyncCallback(ReceiveCallback), state);
        }

        public void StopListening()
        {
            continueListening = false;
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            UdpClient client = ((UdpState)(ar.AsyncState)).client;
            IPEndPoint endpoint = ((UdpState)(ar.AsyncState)).endpoint;

            byte[] receiveBytes = client.EndReceive(ar, ref endpoint);
            
            if (continueListening)
            {
                client.BeginReceive(new AsyncCallback(ReceiveCallback), ar.AsyncState);
            }
            
            TelemetryPacket packet = new TelemetryPacket(receiveBytes);
            PublishTelemetryData(packet);

            SecondaryEventProcessing(packet, previousPacket);
            previousPacket = packet;
        }

        private void SecondaryEventProcessing(TelemetryPacket newPacket, TelemetryPacket previousPacket)
        {

        }

        private void PublishTelemetryData(TelemetryPacket packet)
        {
            EventHandler<TelemetryPacket> handler = ReceivedTelemetryData;
            if (handler != null)
            {
                handler(this, packet);
            }
        }

        public void Dispose()
        {
            udpClient?.Dispose();
        }
    }
}
