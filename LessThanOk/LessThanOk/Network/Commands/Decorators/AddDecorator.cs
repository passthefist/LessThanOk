using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LessThanOk.Network.Commands.Decorators
{
    class AddDecorator: CommandDecorator
    {
        public AddDecorator(Command cmd): base(cmd){ }
        public AddDecorator(UInt16 parentID, UInt16 newID, UInt16 type, TimeSpan time, Command cmd)
            : base(cmd)
        {
            _command[0] |= ((UInt64)T_COMMAND.ADD << 56);
            _command[0] |= ((UInt64)parentID << 24);
            _command[0] |= ((UInt64)newID << 8);
            _command[0] |= ((UInt64)type << 40);
            _command[1] = (UInt64)time.Ticks;
        }
        public override UInt16 ParentID { get { return (UInt16)(_command[0] >> 24); } }

        public override UInt16 ChildID { get { return (UInt16)(_command[0] >> 8); } }

        public override UInt16 Type { get { return (UInt16)(_command[0] >> 40); } }
    }
}
