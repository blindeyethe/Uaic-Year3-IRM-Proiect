using System;

namespace IRM.PhotoSystem
{
    [Flags]
    public enum PhotoObject
    {
        None = 0,
        Ant = 1 << 0, 
        Squirrel = 1 << 1,
    }
}