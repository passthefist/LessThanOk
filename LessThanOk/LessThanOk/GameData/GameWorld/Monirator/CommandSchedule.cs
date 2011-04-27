using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using LessThanOk.Network.Commands;
using LessThanOk.Network.Commands.Decorators;

namespace LessThanOk.GameData.GameWorld.Monirator
{
    class CommandSchedule
    {
        private Dictionary<UInt16, Queue<Command>> _unitQueues;

        public CommandSchedule()
        {
            _unitQueues = new Dictionary<ushort, Queue<Command>>();
        }

        internal void Schedule(Queue<Command> EvaluationResults)
        {
            UInt16 key;
            Queue<Command> actorsQueue;
            foreach (Command cmd in EvaluationResults)
            {
                key = cmd.Actor;
                if (_unitQueues.ContainsKey(key))
                {
                    if (_unitQueues.TryGetValue(key, actorsQueue))
                    {
                        actorsQueue.Enqueue(cmd);
                    }
                }
                else
                {
                    actorsQueue = new Queue<Command>();
                    actorsQueue.Enqueue(cmd);
                    _unitQueues.Add(key, actorsQueue);
                }
            }
        }

        internal void step(GameTime time, out Queue<Command> ScheduledCommands)
        {
            if(_ScheduledCommands == null)
                _ScheduledCommands = new Queue<Command>();
            else
                _ScheduledCommands.Clear();
         
            foreach (Queue<Command> q in _unitQueues)
            {
                foreach (Command cmd in q)
                {
                    if (cmd.TimeStamp > time.ElapsedGameTime.Ticks)
                        q.Dequeue();
                        
                    ScheduledCommands.Enqueue(q.Peek());
                }
            }
        }
    }
}
