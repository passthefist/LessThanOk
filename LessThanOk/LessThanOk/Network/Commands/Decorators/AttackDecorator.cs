using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LessThanOk.Network.Commands.Decorators
{
    class AttackDecorator: CommandDecorator
    {
        public AttackDecorator(Command cmd) : base(cmd) { }
        public AttackDecorator(UInt16 unit, UInt16 target, TimeSpan time, Command cmd)
            : base(cmd)
        {
            _command[0] |= ((UInt64)T_COMMAND.ATTACK << 56);
            _command[0] |= ((UInt64)unit << 16);
            _command[0] |= ((UInt64)target);
            _command[1] = (UInt64)time.Ticks;
        }
        
        public override UInt16 UnitID { get { return (UInt16)(_command[0] >> 16); } }
        public override UInt16 Target { get { return (UInt16)(_command[0]); } }
    }
}
