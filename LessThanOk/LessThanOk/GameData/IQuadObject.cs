using System;
using Microsoft.Xna.Framework;

namespace LessThanOk.GameData.GameWorld
{
    public interface IQuadObject
    {
        Rectangle Bounds();
        event EventHandler BoundsChanged;
    }
}