using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using LessThanOk.Network.Commands;
using LessThanOk.Network.Commands.Events;

namespace LessThanOk.Input
{
    class ReplayReader : InputSource
    {
        public event EventHandler<NewCommandEventArgs> NewCommandEvent;

        private BinaryReader fileReader;
        private int lastCmd;

        public ReplayReader(String XMLDocument)
        {
            String path = XMLDocument;
            //path = XMLDocument.getReplayFile()
            FileStream source = new FileStream(path, new FileMode());
            fileReader = new BinaryReader(source);
            lastCmd = 0;
        }

        public void updateBuffer(GameTime elps)
        {
            byte[] rawCmd = new byte[16];
            
            fileReader.Read(rawCmd, lastCmd, 16);
            lastCmd++;

            ulong first = 0;
            for (int i = 0; i < 8; i++)
            {
                first <<= 8;
                first |= rawCmd[i];
            }
            ulong second = 0;
            for (int i = 0; i < 8; i++)
            {
                second <<= 8;
                first |= rawCmd[i+8];
            }

            ulong [] cmdDat = new ulong[2];
            cmdDat[0] = first;
            cmdDat[1] = second;

            Command cmd = new Command(cmdDat);
            if (NewCommandEvent != null)
                NewCommandEvent.Invoke(this, new NewCommandEventArgs(cmd));
        }
    }
}
