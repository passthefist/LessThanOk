using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LessThanOk.GameData.GameWorld.Monirator
{
    public enum OPERATOR
    {
        AND,
        OR
    }

    public class Dependency
    {
        private List<Expression> _dep;
        private String _retval;

        public Dependency(String retval, List<Expression> dep)
        {
            _dep = dep;
            _retval = retval;
        }
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
        public Expression(List<String> truthValues, OPERATOR op)
        {
            _truthValues = truthValues;
            _op = op;
        }
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

        private bool evalOr(bool input, Dictionary<String, bool> values)
        {
            if (input)
                return true;
            bool temp;

            foreach (String s in _truthValues)
            {
                if (!values.TryGetValue(s, out temp))
                    return false;
                if(temp)
                    return true;
            }
            return false;
        }

        private bool evalAnd(bool input, Dictionary<String, bool> values)
        {
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
