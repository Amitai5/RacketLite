using System;
using System.Collections.Generic;
using System.Text;

namespace RacketVM
{
    public static class SyntaxValidators
    {
        public static int IsBalancedParenthesis(this string expression)
        {
            int parenthesis = 0;
            for(int i = 0; i < expression.Length; i++)
            {
                if(expression[i] == '(')
                {
                    parenthesis++;
                }
                else if(expression[i] == ')')
                {
                    parenthesis--;
                }
            }
            return parenthesis;
        }
    }
}
