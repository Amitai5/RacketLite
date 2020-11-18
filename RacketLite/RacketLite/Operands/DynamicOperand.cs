using RacketLite.Exceptions;
using RacketLite.Oporators;
using System;

namespace RacketLite.Operands
{
    public class DynamicOperand : IOperable
    {
        public bool? Inexact
        {
            get
            {
                return OperableValue.Type switch
                {
                    RacketOperandType.Number => ((NumberOperand)OperableValue).Inexact,
                    RacketOperandType.Natural => ((NaturalOperand)OperableValue).Inexact,
                    RacketOperandType.Integer => ((IntegerOperand)OperableValue).Inexact,
                    _ => null,
                };
            }
        }
        public bool? Irrational
        {
            get
            {
                return OperableValue.Type switch
                {
                    RacketOperandType.Number => ((NumberOperand)OperableValue).Irrational,
                    _ => null,
                };
            }
        }
        private IOperable OperableValue { get; set; }
        public new RacketOperandType Type => OperableValue.Type;

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
                    DynamicOperand expressionValue = EvaluateExpressionOperand();
                    if (expressionValue.Type == RacketOperandType.Boolean)
                    {
                        return expressionValue.GetBooleanValue();
                    }
                    throw new TypeConversionException(expressionValue.Type, RacketOperandType.Boolean);

                default:
                    throw new TypeConversionException(Type, RacketOperandType.Boolean);
            }
        }

        public double GetNumberValue()
        {
            switch (Type)
            {
                case RacketOperandType.Number:
                    return ((NumberOperand)OperableValue).NumberValue;
                case RacketOperandType.Integer:
                    return ((IntegerOperand)OperableValue).IntegerValue;
                case RacketOperandType.Natural:
                    return ((NaturalOperand)OperableValue).NaturalValue;
                case RacketOperandType.Expression:
                    DynamicOperand expressionValue = EvaluateExpressionOperand();
                    return expressionValue.GetNumberValue();

                default:
                    throw new TypeConversionException(Type, RacketOperandType.Number);
            }
        }

        public long GetIntegerValue()
        {
            switch (Type)
            {
                //Try convert Number
                case RacketOperandType.Number:
                    if(long.TryParse(GetNumberValue().ToString(), out long parsedNumber))
                    {
                        return parsedNumber;
                    }
                    throw new TypeConversionException(Type, RacketOperandType.Integer);

                case RacketOperandType.Integer:
                    return ((IntegerOperand)OperableValue).IntegerValue;
                case RacketOperandType.Natural:
                    return (long)((NaturalOperand)OperableValue).NaturalValue;
                case RacketOperandType.Expression:
                    DynamicOperand expressionValue = EvaluateExpressionOperand();
                    return expressionValue.GetIntegerValue();

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
                    if (ulong.TryParse(GetNumberValue().ToString(), out ulong parsedNumber) && parsedNumber != 0)
                    {
                        return parsedNumber;
                    }
                    throw new TypeConversionException(Type, RacketOperandType.Natural);

                case RacketOperandType.Natural:
                    return ((NaturalOperand)OperableValue).NaturalValue;
                case RacketOperandType.Expression:
                    DynamicOperand expressionValue = EvaluateExpressionOperand();
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
                    return ((StringOperand)OperableValue).StringValue;

                case RacketOperandType.Expression:
                    DynamicOperand expressionValue = EvaluateExpressionOperand();
                    return expressionValue.GetStringValue();

                default:
                    throw new TypeConversionException(Type, RacketOperandType.String);
            }
        }

        public RacketExpression GetExpressionValue()
        {
            if (Type != RacketOperandType.Expression)
            {
                throw new TypeConversionException(Type, RacketOperandType.Expression);
            }
            return (RacketExpression)OperableValue;
        }


        public object GetUnknownValue()
        {
            return Type switch
            {
                RacketOperandType.Unknown => ((UnknownOperand)OperableValue).OperandValue,
                _ => throw new TypeConversionException(Type, RacketOperandType.Unknown)
            };
        }

        public DynamicOperand EvaluateExpressionOperand()
        {
            RacketExpression racketExpression = GetExpressionValue();
            return racketExpression.Evaluate();
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
                otherDynOperand = otherDynOperand.EvaluateExpressionOperand();
            }

            //Evaluate this if it is an expression
            if (Type == RacketOperandType.Expression)
            {
                OperableValue = EvaluateExpressionOperand().OperableValue;
            }
            return OperableValue.CompareTo(otherDynOperand);
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
                otherDynOperand = otherDynOperand.EvaluateExpressionOperand();
            }

            //Evaluate this if it is an expression
            if (Type == RacketOperandType.Expression)
            {
                OperableValue = EvaluateExpressionOperand().OperableValue;
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
