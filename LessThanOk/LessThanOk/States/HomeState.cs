using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LessThanOk.Input;
using LessThanOk.Input.Events;
using LessThanOk.UI;
using LessThanOk.UI.Frames;
using LessThanOk.UI.Frames.UIElements;
using LessThanOk.UI.Events;
using Microsoft.Xna.Framework.Content;

namespace LessThanOk.States
{
    class HomeState : State
    {

        public HomeState(Frame frame)
        {
            Frame_Home HomeFrame;
            if (frame is Frame_Home)
                HomeFrame = ((Frame_Home)frame);

        }

        #region State Members

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void LoadContent(ContentManager Content)
        {
       
        }

        public void Update(Microsoft.Xna.Framework.GameTime time)
        {
        
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch batch)
        {
        
        }

        public void UnloadContent(ContentManager Content)
        {
            throw new NotImplementedException();
        }

        public void UnInitialize()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
