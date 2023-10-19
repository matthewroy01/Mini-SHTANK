using System;

namespace SHTANK.Data
{
    [Flags]
    public enum GridAreaFlags
    {
        Battlefield = 1 << 0,
        Outskirts = 1 << 1,
        NoMansLand = 1 << 2,
        EnemySpace = 1 << 3,
    }
}