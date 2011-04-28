using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace LessThanOk.States
{
    interface State
    {
        void Initialize();
        void LoadContent(ContentManager Content);
        void Update(GameTime time);
        void Draw(SpriteBatch batch);
        void UnloadContent(ContentManager Content);
        void UnInitialize();
    }
}
