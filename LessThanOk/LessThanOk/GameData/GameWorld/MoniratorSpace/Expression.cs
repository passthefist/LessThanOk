/*---------------------------------------------------------------------------*\
*                         LessThanOK Engine                                 *
*                                                                           *
*          Copyright (C) 2011-2012 by Robert Goetz, Anthony Lobono          *
*                                                                           *
*          authors:  Anthony LoBono (ajlobono@gmail.com)                    *
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
 *                                                                           *
 * Expression is a representation of a logic expressiont in Reverse Polish   *
 * Notation. File also contains the Dependancy class.  A Dependancy contains *
 * a list of Expressions that should be evaluated in order.  If the          *
 * Expression List evaluates to true the Dependancy should return its String *
 * Otherwise it returns null.                                                *
 *                                                                           *
\*---------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LessThanOk.GameData.GameWorld.MoniratorSpace
{
    /// <summary>
    /// Supported logical operaters.
    /// </summary>
    public enum OPERATOR
    {
        AND,
        OR
    }
  
    public class Dependency
    {
        private List<Expression> _dep;
        private String _retval;
        /// <summary>
        /// Contructor for a Dependancy.
        /// </summary>
        /// <param name="retval">String to return if the Expression List evaluates to true.</param>
        /// <param name="dep">Expression List to check thruth of Dependancy.</param>
        public Dependency(String retval, List<Expression> dep)
        {
            _dep = dep;
            _retval = retval;
        }
        /// <summary>
        /// Evaluate a dependancy.
        /// </summary>
        /// <param name="map">String to bool map.  Defines if a paramater is ture.</param>
        /// <returns>Name of Unit for the dependancy.</returns>
        public String Evaluate(Dictionary<String, bool> map)
        {
            bool retval = true;
            foreach (Expression e in _dep)
            {
                retval = e.EvaluateExpression(retval, map);
            }
            if (retval)
                return _retval;
            return null;
        }
    }

    public class Expression
    {
        private List<String> _truthValues;
        private OPERATOR _op;
        /// <summary>
        /// Constructor for an Expression
        /// </summary>
        /// <param name="truthValues">Strings representing the paramaters for evaluation</param>
        /// <param name="op">Logical Operater for the Expression.</param>
        public Expression(List<String> truthValues, OPERATOR op)
        {
            _truthValues = truthValues;
            _op = op;
        }
        /// <summary>
        /// Method for evaluating an expression. Calls private functions based on logical operater.
        /// </summary>
        /// <param name="input">Input to being expression with.</param>
        /// <param name="values">Map of String paramaters to their truth value.</param>
        /// <returns></returns>
        public bool EvaluateExpression(bool input, Dictionary<String, bool> values)
        {
            switch (_op)
            {
                case OPERATOR.AND:
                    return evalAnd(input, values);
                case OPERATOR.OR:
                    return evalOr(input, values);
                default:
                    return false;
            }
        }
        /// <summary>
        /// Method for evaluating an OR Expression
        /// </summary>
        /// <param name="input">Input into the evaluation.</param>
        /// <param name="values">Map of paramater names to their truth value.</param>
        /// <returns>True if epression is true. False otherwise.</returns>
        private bool evalOr(bool input, Dictionary<String, bool> values)
        {
            // This is a logical or so if the input is true than entire expression evalutates to true
            if (input)
                return true;
            bool temp;

            foreach (String s in _truthValues)
            {
                if (!values.TryGetValue(s, out temp))
                    return false;
                // Return true on first paramater that evaluates to true.
                if(temp)
                    return true;
            }
            // All paramaters where false if you are outside of the foreach loop. Return false.
            return false;
        }
        /// <summary>
        /// Method for evaluating a logical AND
        /// </summary>
        /// <param name="input">Input in to the expression</param>
        /// <param name="values">Map of String paramaters to their thruth values.</param>
        /// <returns>True of all values are true. False otherwise.</returns>
        private bool evalAnd(bool input, Dictionary<String, bool> values)
        {
            // Logical AND means if input is false the entire expression evaluates to false.
            if (!input)
                return false;

            bool temp;

            foreach (String s in _truthValues)
            {
                if (!values.TryGetValue(s, out temp))
                    return false;
                if (!temp)
                    return false;
            }
            return true;
        }

    }
}
