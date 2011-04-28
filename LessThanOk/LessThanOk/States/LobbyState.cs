using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.UI.Frames;
using LessThanOk.UI;

namespace LessThanOk.States
{
    class LobbyState : State
    {

        public LobbyState(Frame frame)
        {
            Frame_HostLobby LobbyFrame;
            if (frame is Frame_HostLobby)
                LobbyFrame = ((Frame_HostLobby)frame);
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
