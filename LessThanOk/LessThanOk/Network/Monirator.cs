using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using LessThanOk.Network.Commands;
using LessThanOk.GameData;

/******************** MONIRATOR *****************************
* This class is used for processing new requests from       *
* players.  Monirator is also responsible for checking if   *
* requests can/should be granted.  Once the command is      *
* granted additional commmands may need to be created.      *
************************************************************/
namespace LessThanOk.Network
{
    public class Monirator
    {
        private Queue<Command> grants;
        private Queue<Command> requests;
        /// <summary>
        /// Default constructor that sets the queue lengths to 100
        /// </summary>
        public Monirator()
        {
            grants = new Queue<Command>(100);
            requests = new Queue<Command>(100);
        }
        /// <summary>
        /// Itterates through all commands in the request queue, and checks if
        /// the command should be granted. If the command validates it is added
        /// to the granted queue.
        /// </summary>
        /// <param name="gameTime"> Current GameTime</param>
        public void proccessReq(GameTime gameTime)
        {
            Command nextReq;
            while (requests.Count != 0)
            {
                nextReq = requests.Dequeue();
                // First check if request is valid

                //!!!!!!!!!!!For now all requests are granted!!!!!!!!!!!

                if (checkRequest(nextReq))
                {
                    nextReq.grant();
                    grants.Enqueue(nextReq);
                }
                else
                {
                    // Deny 
                    // TODO construct a notify 
                }

            }
        }
        /// <summary>
        /// Checks if a request for validity
        /// </summary>
        /// <param name="n_req">Command to be checked</param>
        /// <returns>True if command is valid, false otherwise.</returns>
        private Boolean checkRequest(Command n_req)
        {
            return true;
        }
        /// <summary>
        /// Returns a Queue of all granted commands.
        /// </summary>
        /// <returns>Queue of all granted commands.</returns>
        public Queue<Command> getGrants() { return grants; }
        /// <summary>
        /// Itterates through a list of changes and constructs commands that will
        /// automaticly be granted.
        /// </summary>
        /// <param name="changes">List of changes to be processed</param>
        public void processChanges() 
        { 
        
        }
        /// <summary>
        /// Tests for Debugging.
        /// </summary>
        /// <param name="test">Wich test to run</param>
        /// <param name="timestamp">Current GameTime</param>
        /// <returns>Test result to be printed.</returns>
        public string COMMANDTEST(int test, GameTime timestamp)
        {
            String retval;
            Command command_t;
            Command_Add add_t;
            Command_Set set_t;
            Command_Note note_t;

            if (test == 0)
            {
                command_t = new Command();
                add_t = new Command_Add(0,0,0, timestamp.TotalGameTime);
                set_t = new Command_Set(0,0,0, timestamp.TotalGameTime);
                note_t = new Command_Note(0, timestamp.TotalGameTime);

                return "Test 0 Passed\n";
            }
            else if (test == 1)
            {
                add_t = new Command_Add(0x0001, 0x0002, 0x0003, 
                    timestamp.TotalGameTime);
                if (add_t.getBuilder() != 0x0001)
                    return "add_t.getBuilder() != 0x0001 :Test 1\n";
                else if (add_t.getBuilt() != 0x0002)
                    return "add_t.getbuilt() != 0x0002 :Test 1\n";
                else if (add_t.getType() != 0x0003)
                    return "add_t.getType() != 0x0003 :Test 1\n";
                else if (add_t.getTimeStamp() != timestamp.TotalGameTime.Ticks)
                    return "add_t.getType() != ticks :Test 1\n";
                else if (add_t.getCommandType() != Command.T_COMMAND.ADD)
                    return "add_t.getType() != Command.T_COMMAND.ADD :Test 1\n";
                return "Test 1 Passed\n";
            }
            
            else if (test == 2)
            {
                set_t = new Command_Set(0x00001, 0x02, 0x00000003, 
                    timestamp.TotalGameTime);
                if (set_t.getID() != 0x0001)
                    return "set_t.getID() != 0x0001 :Test 2\n";
                else if (set_t.getKey() != 0x02)
                    return "set_t.getKey() != 0x02 :Test 2\n";
                else if (set_t.getValue() != 0x00000003)
                    return "set_t.getValue() != 0x00000003 :Test 2\n";
                else if (set_t.getTimeStamp() != timestamp.TotalGameTime.Ticks)
                    return "set_t.getTimeStamp() != ticks :Test 2\n";
                else if (set_t.getCommandType() != Command.T_COMMAND.SET)
                    return "set_t.getCommandType() != Command.T_COMMAND.SET :Test 2\n";
                return "Test 2 Passed\n";
            }
            else if (test == 3)
            {
                note_t = new Command_Note(0x000E, timestamp.TotalGameTime);
                if (note_t.getError() != 0x000E)
                    return "note_t.getError() != 0x000E :Test 3\n";
                else if (note_t.getTimeStamp() != timestamp.TotalGameTime.Ticks)
                    return "note_t.getTimeStamp() != ticks :Test 3\n";
                else if (note_t.getCommandType() != Command.T_COMMAND.ERROR)
                    return "note_t.getCommandType() != Command.T_COMMAND.ERROR :Test 3\n";
                return "Test 3 Passed\n";
            }
            return "Test "+ test +" faild\n";

        }

    }
}
