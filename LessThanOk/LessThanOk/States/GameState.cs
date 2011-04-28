using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.UI;
using LessThanOk.Network.Commands;
using LessThanOk.Selecter;

namespace LessThanOk.States
{
    class GameState: State
    {
        CommandRequester CMDRequester;
        ObjectSelector Selector;

        public GameState(Frame frame)
        {
            Frame_Game GameFrame;
            if (frame is Frame_Game)
                GameFrame = ((Frame_Game)frame);
            else
                throw new Exception("Wrong frame in GameState");
            GameFrame.AddUnitEvent += CMDRequester.AddButtonHandler;
        }


        #region State Members

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            throw new NotImplementedException();
        }

        public void Update(Microsoft.Xna.Framework.GameTime time)
        {
       
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch batch)
        {
            throw new NotImplementedException();
        }

        public void UnloadContent(Microsoft.Xna.Framework.Content.ContentManager Content)
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
