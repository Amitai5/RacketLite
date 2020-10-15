using RacketLite.Exceptions;
using RacketLite.Oporators;
using System;

namespace RacketLite.Operands
{
    public class DynamicOperand : IOperable
    {
        public IOperable OperableValue { get; private set; }
        public DynamicOperand(IOperable operableValue = null)
            : base(operableValue.Type)
        {
            OperableValue = operableValue;
        }

        #region Implicit Casts From Other Operands

        public static implicit operator DynamicOperand(RacketExpression expressionOperand)
        {
            return new DynamicOperand(expressionOperand);
        }

        public static implicit operator DynamicOperand(UnknownOperand unknownOperand)
        {
            return new DynamicOperand(unknownOperand);
        }

        public static implicit operator DynamicOperand(StringOperand stringOperand)
        {
            return new DynamicOperand(stringOperand);
        }

        public static implicit operator DynamicOperand(NaturalOperand naturalOperand)
        {
            return new DynamicOperand(naturalOperand);
        }

        public static implicit operator DynamicOperand(NumberOperand numericOperand)
        {
            return new DynamicOperand(numericOperand);
        }

        public static implicit operator DynamicOperand(BooleanOperand booleanOperand)
        {
            return new DynamicOperand(booleanOperand);
        }

        #endregion Dynamic Casts

        #region Oporators

        //Numeric Oporators
        public static DynamicOperand operator *(DynamicOperand dynamicOperand, DynamicOperand otherOperand)
        {
            double result = dynamicOperand.GetDoubleValue() * otherOperand.GetDoubleValue();
            return new DynamicOperand(new NumberOperand(result));
        }
        public static DynamicOperand operator /(DynamicOperand dynamicOperand, DynamicOperand otherOperand)
        {
            double result = dynamicOperand.GetDoubleValue() / otherOperand.GetDoubleValue();
            return new DynamicOperand(new NumberOperand(result));
        }
        public static DynamicOperand operator +(DynamicOperand dynamicOperand, DynamicOperand otherOperand)
        {
            double result = dynamicOperand.GetDoubleValue() + otherOperand.GetDoubleValue();
            return new DynamicOperand(new NumberOperand(result));
        }
        public static DynamicOperand operator -(DynamicOperand dynamicOperand, DynamicOperand otherOperand)
        {
            double result = dynamicOperand.GetDoubleValue() - otherOperand.GetDoubleValue();
            return new DynamicOperand(new NumberOperand(result));
        }

        //Boolean Oporators
        public static DynamicOperand operator !(DynamicOperand dynamicOperand)
        {
            bool value1 = dynamicOperand.GetBooleanValue();
            return new DynamicOperand(new BooleanOperand(!value1));
        }

        public static DynamicOperand operator >(DynamicOperand dynamicOperand, DynamicOperand otherOperand)
        {
            bool result = dynamicOperand.CompareTo(otherOperand) > 0;
            return new DynamicOperand(new BooleanOperand(result));
        }

        public static DynamicOperand operator <(DynamicOperand dynamicOperand, DynamicOperand otherOperand)
        {
            bool result = dynamicOperand.CompareTo(otherOperand) < 0;
            return new DynamicOperand(new BooleanOperand(result));
        }

        public static DynamicOperand operator >=(DynamicOperand dynamicOperand, DynamicOperand otherOperand)
        {
            bool greaterThan = dynamicOperand.CompareTo(otherOperand) > 0;
            bool equal = dynamicOperand.Equals(otherOperand);
            return new DynamicOperand(new BooleanOperand(greaterThan || equal));
        }

        public static DynamicOperand operator <=(DynamicOperand dynamicOperand, DynamicOperand otherOperand)
        {
            bool lessThan = dynamicOperand.CompareTo(otherOperand) < 0;
            bool equal = dynamicOperand.Equals(otherOperand);
            return new DynamicOperand(new BooleanOperand(lessThan || equal));
        }

        public static DynamicOperand operator |(DynamicOperand dynamicOperand, DynamicOperand otherOperand)
        {
            bool value1 = otherOperand.GetBooleanValue();
            bool value2 = dynamicOperand.GetBooleanValue();
            return new DynamicOperand(new BooleanOperand(value1 || value2));
        }

        public static DynamicOperand operator &(DynamicOperand dynamicOperand, DynamicOperand otherOperand)
        {
            bool value1 = otherOperand.GetBooleanValue();
            bool value2 = dynamicOperand.GetBooleanValue();
            return new DynamicOperand(new BooleanOperand(value1 && value2));
        }

        #endregion Oporators

        #region Object Overrides

        public override int CompareTo(object obj)
        {
            //Throw if the object is null
            if (obj is null)
            {
                throw new ArgumentNullException("CompareTo on 'DynamicOperand' does not support null.");
            }

            //Evaluate the operand if it is an expression
            DynamicOperand otherDynOperand = (DynamicOperand)obj;
            if(otherDynOperand.Type == RacketOperandType.Expression)
            {
                otherDynOperand = otherDynOperand.GetExpressionValue();
            }

            //Evaluate this if it is an expression
            if (Type == RacketOperandType.Expression)
            {
                OperableValue = GetExpressionValue().OperableValue;
            }

            //Check for same type
            if (otherDynOperand.Type != OperableValue.Type)
            {
                throw new TypeConversionException(OperableValue.Type, otherDynOperand.Type);
            }
            return OperableValue.CompareTo(otherDynOperand.OperableValue);
        }

        public override string ToString()
        {
            return Type switch
            {
                RacketOperandType.Expression => GetExpressionValue().ToString(),
                RacketOperandType.Boolean => GetBooleanValue().ToString(),
                RacketOperandType.Number => GetDoubleValue().ToString(),
                RacketOperandType.String => GetStringValue().ToString(),
                RacketOperandType.Natural => GetLongValue().ToString(),
                _ => throw new NotImplementedException()
            };
        }

        public override bool Equals(object obj)
        {
            //Return false if the other is null
            if (obj is null)
            {
                return false;
            }

            //Evaluate the operand if it is an expression
            DynamicOperand otherDynOperand = (DynamicOperand)obj;
            if (otherDynOperand.Type == RacketOperandType.Expression)
            {
                otherDynOperand = otherDynOperand.GetExpressionValue();
            }

            //Evaluate this if it is an expression
            if (Type == RacketOperandType.Expression)
            {
                OperableValue = GetExpressionValue().OperableValue;
            }

            //Check for same type
            if (otherDynOperand.Type != OperableValue.Type)
            {
                throw new TypeConversionException(OperableValue.Type, otherDynOperand.Type);
            }
            return OperableValue.Equals(otherDynOperand.OperableValue);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, OperableValue);
        }

        #endregion Object Overrides

        public bool GetBooleanValue()
        {
            switch (Type)
            {
                case RacketOperandType.Boolean:
                    return ((BooleanOperand)OperableValue).OperandValue;

                case RacketOperandType.Expression:
                    DynamicOperand expressionValue = GetExpressionValue();
                    if (expressionValue.Type == RacketOperandType.Boolean)
                    {
                        return expressionValue.GetBooleanValue();
                    }
                    throw new TypeConversionException(expressionValue.Type, RacketOperandType.Boolean);

                default:
                    throw new TypeConversionException(Type, RacketOperandType.Boolean);
            }
        }

        public double GetDoubleValue()
        {
            switch (Type)
            {
                case RacketOperandType.Number:
                    return ((NumberOperand)OperableValue).OperandValue;
                case RacketOperandType.Natural:
                    return ((NaturalOperand)OperableValue).OperandValue;

                case RacketOperandType.Expression:
                    DynamicOperand expressionValue = GetExpressionValue();
                    if (expressionValue.Type == RacketOperandType.Number)
                    {
                        return expressionValue.GetDoubleValue();
                    }
                    else if(expressionValue.Type == RacketOperandType.Natural)
                    {
                        return expressionValue.GetLongValue();
                    }
                    throw new TypeConversionException(expressionValue.Type, RacketOperandType.Number);

                default:
                    throw new TypeConversionException(Type, RacketOperandType.Number);
            }
        }

        public long GetLongValue()
        {
            switch (Type)
            {
                case RacketOperandType.Natural:
                    return ((NaturalOperand)OperableValue).OperandValue;

                case RacketOperandType.Expression:
                    DynamicOperand expressionValue = GetExpressionValue();
                    if (expressionValue.Type == RacketOperandType.Natural)
                    {
                        return expressionValue.GetLongValue();
                    }
                    throw new TypeConversionException(expressionValue.Type, RacketOperandType.Natural);

                default:
                    throw new TypeConversionException(Type, RacketOperandType.Natural);
            }
        }

        public string GetStringValue()
        {
            switch (Type)
            {
                case RacketOperandType.String:
                    return ((StringOperand)OperableValue).OperandValue;

                case RacketOperandType.Expression:
                    DynamicOperand expressionValue = GetExpressionValue();
                    if (expressionValue.Type == RacketOperandType.String)
                    {
                        return expressionValue.GetStringValue();
                    }
                    throw new TypeConversionException(expressionValue.Type, RacketOperandType.String);

                default:
                    throw new TypeConversionException(Type, RacketOperandType.String);
            }
        }

        public DynamicOperand GetExpressionValue()
        {
            if (Type != RacketOperandType.Expression)
            {
                throw new TypeConversionException(Type, RacketOperandType.Expression);
            }

            RacketExpression racketExpression = (RacketExpression)OperableValue;
            if (racketExpression.Oporator.Type == RacketOporatorType.UserDefinedFunction)
            {
                UserDefinedExpression userDefinedExpression = StaticsManager.UserDefinedExpressions[racketExpression.RacketOporatorSignature];
                if (racketExpression.UDEOperands != null)
                {
                    return userDefinedExpression.Evaluate(racketExpression.UDEOperands);
                }
                return userDefinedExpression.Evaluate(racketExpression.Operands.GetCopy());
            }
            else
            {
                return racketExpression.Evaluate();
            }
        }

        public object GetUnknownValue()
        {
            return Type switch
            {
                RacketOperandType.Unknown => ((UnknownOperand)OperableValue).OperandValue,
                _ => throw new TypeConversionException(Type, RacketOperandType.Unknown)
            };
        }
    }
}
