﻿using RacketLite.Exceptions;
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
                case RacketOperandType.Integer:
                    return ((IntegerOperand)OperableValue).OperandValue;
                case RacketOperandType.Natural:
                    return ((NaturalOperand)OperableValue).OperandValue;
                case RacketOperandType.Expression:
                    DynamicOperand expressionValue = GetExpressionValue();
                    return expressionValue.GetDoubleValue();

                default:
                    throw new TypeConversionException(Type, RacketOperandType.Number);
            }
        }

        public long GetLongValue()
        {
            switch (Type)
            {
                //Try convert Number
                case RacketOperandType.Number:
                    if(long.TryParse(GetDoubleValue().ToString(), out long parsedNumber))
                    {
                        return parsedNumber;
                    }
                    throw new TypeConversionException(Type, RacketOperandType.Integer);

                case RacketOperandType.Integer:
                    return ((IntegerOperand)OperableValue).OperandValue;
                case RacketOperandType.Natural:
                    return (long)((NaturalOperand)OperableValue).OperandValue;
                case RacketOperandType.Expression:
                    DynamicOperand expressionValue = GetExpressionValue();
                    return expressionValue.GetLongValue();

                default:
                    throw new TypeConversionException(Type, RacketOperandType.Integer);
            }
        }

        public ulong GetNaturalValue()
        {
            switch (Type)
            {
                //Try convert Number & Integer
                case RacketOperandType.Number:
                case RacketOperandType.Integer:
                    if (ulong.TryParse(GetDoubleValue().ToString(), out ulong parsedNumber) && parsedNumber != 0)
                    {
                        return parsedNumber;
                    }
                    throw new TypeConversionException(Type, RacketOperandType.Natural);

                case RacketOperandType.Natural:
                    return ((NaturalOperand)OperableValue).OperandValue;
                case RacketOperandType.Expression:
                    DynamicOperand expressionValue = GetExpressionValue();
                    return expressionValue.GetNaturalValue();

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
                    return expressionValue.GetStringValue();

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
            return racketExpression.Evaluate();
        }

        public object GetUnknownValue()
        {
            return Type switch
            {
                RacketOperandType.Unknown => ((UnknownOperand)OperableValue).OperandValue,
                _ => throw new TypeConversionException(Type, RacketOperandType.Unknown)
            };
        }

        #region IOperable Overrides
        public override int CompareTo(object obj)
        {
            //Throw if the object is null
            if (obj is null)
            {
                throw new ArgumentNullException("CompareTo on 'DynamicOperand' does not support null.");
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
            return OperableValue.CompareTo(otherDynOperand.OperableValue);
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

        public override string ToString()
        {
            return OperableValue.ToString();
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, OperableValue);
        }
        #endregion IOperable Overrides

        #region Comparisons
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
        #endregion Comparisons

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

        public static implicit operator DynamicOperand(IntegerOperand integerOperand)
        {
            return new DynamicOperand(integerOperand);
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
    }
}
