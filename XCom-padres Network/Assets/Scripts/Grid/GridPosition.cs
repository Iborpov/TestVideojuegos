using System;
using Unity.Netcode;

public struct GridPosition : IEquatable<GridPosition>, INetworkSerializable
{
    public int x;
    public int z;

    public GridPosition(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    public override string ToString()
    {
        return $"x: {x}; z: {z}";
    }

    public static bool operator ==(GridPosition a, GridPosition b)
    {
        return a.x == b.x && a.z == b.z;
    }

    public static bool operator !=(GridPosition a, GridPosition b)
    {
        return !(a == b);
    }

    public override bool Equals(object obj)
    {
        if (obj is GridPosition)
        {
            GridPosition other = (GridPosition)obj;
            return this == other;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return x.GetHashCode() ^ z.GetHashCode();
    }

    public bool Equals(GridPosition other)
    {
        return this.Equals((object)other);
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer)
        where T : IReaderWriter
    {
        serializer.SerializeValue(ref x);
        serializer.SerializeValue(ref z);
    }

    public static GridPosition operator +(GridPosition a, GridPosition b)
    {
        return new GridPosition(a.x + b.x, a.z + b.z);
    }

    public static GridPosition operator -(GridPosition a, GridPosition b)
    {
        return new GridPosition(a.x - b.x, a.z - b.z);
    }
}
