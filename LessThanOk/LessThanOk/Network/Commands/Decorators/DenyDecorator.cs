using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LessThanOk.Network.Commands.Decorators
{
    class DenyDecorator:CommandDecorator
    {
        public DenyDecorator(Command cmd): base(cmd){ }
        public DenyDecorator(UInt16 ID, long time, Command cmd)
            : base(cmd)
        {
            _command[0] |= ((UInt64)T_COMMAND.ERROR << 56);
            _command[0] |= ((UInt64)ID);
            _command[1] = (UInt64)time;
        }

        public override UInt16 UnitID { get { return (UInt16)(_command[0]); } }
    }
}
