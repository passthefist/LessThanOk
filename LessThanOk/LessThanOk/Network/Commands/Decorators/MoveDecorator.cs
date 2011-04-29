using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LessThanOk.Network.Commands.Decorators
{
    class MoveDecorator : CommandDecorator
    {
        public MoveDecorator(Command cmd) : base(cmd) { }
        public MoveDecorator(UInt16 unit, UInt16 x, UInt16 y, float time, Command cmd)
            : base(cmd)
        {
            _command[0] |= ((UInt64)T_COMMAND.MOVE << 56);
            _command[0] |= ((UInt64)unit << 32);
            _command[0] |= ((UInt64)x << 16);
            _command[0] |= ((UInt64)y);
            _command[1] = (UInt64)time;
        }
        
        public override UInt16 UnitID { get { return (UInt16)(_command[0] >> 32); } }
        public override UInt16 X { get { return (UInt16)(_command[0] >> 16); } }
        public override UInt16 Y { get { return (UInt16)(_command[0]); } }
    }
}
