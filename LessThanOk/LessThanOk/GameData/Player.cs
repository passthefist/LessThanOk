/*---------------------------------------------------------------------------*\
 *                         LessThanOK Engine                                 *
 *                                                                           *
 *          Copyright (C) 2011-2012 by Robert Goetz,                         *
 *                                                                           *
 *   authors:  Robert Goetz (rdgoetz@iastate.edu)                            *
 *                                                                           *
\*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*\
 *                                License                                    *
 *                                                                           *
 * This library is free software; you can redistribute it and/or modify it   *
 * under the terms of the MIT Liscense.                                      *
 *                                                                           *
 * This library is distributed in the hope that it will be useful, but       *
 * WITHOUT ANY WARRANTY; without even the implied warranty of                *
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.                      *
 *                                                                           *
 * You should have received a copy of the MIT Liscense with this library, if *
 * not, visit http://www.opensource.org/licenses/mit-license.php.            *
 *                                                                           *
\*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*\
 *                            Class Overview                                 *
\*---------------------------------------------------------------------------*/


using Microsoft.Xna.Framework;

namespace LessThanOk.GameData
{
    public class Player
    {
        private float power;
        private float maxPower;
        private float rate;

        public float Power
        {
            get { return power; }
        }

        private int playerID;
        public int PlayerID
        {
            get {return playerID;}
        }

        public Player()
        {
            power = 0.0f;
            maxPower = 1000.0f;
            rate = 1.0f;
        }

        public void update(GameTime elps)
        {
            power += rate;
            if(power > maxPower)
                power = maxPower;
        }

        public void assignId(int id)
        {
            playerID = id;
        }
    }
}