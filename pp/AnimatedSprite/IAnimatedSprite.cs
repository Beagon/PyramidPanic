using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace pp
{
    public interface IAnimatedSprite
    {
        Vector2 Location { get; set; }
        Texture2D Texture { get; }
        int Columns { get; }
        int Rows { get; }
        float FrameLength { get; }
        PyramidPanic Game { get; }
        float OffsetHeight { get; }
        float OffsetWidth { get; }
    }
}
