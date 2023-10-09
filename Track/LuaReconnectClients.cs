using AssettoServer.Shared.Network.Packets;
using AssettoServer.Shared.Network.Packets.Outgoing;

namespace nvrlift.AssettoServer.Track;

public class LuaReconnectClients : IOutgoingNetworkPacket
{
    public void ToWriter(ref PacketWriter writer)
    {
        writer.Write<byte>(0xAB);
        writer.Write<byte>(0x03);
        writer.Write<byte>(255);
        writer.Write<ushort>(60000);
        writer.Write(0xC9F693DA);
        writer.WriteUTF8String("ReconnectClients");
    }
}
