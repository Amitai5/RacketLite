using RacketVM.Exceptions;
using RacketVM.Operands.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace RacketVM.Operands
{
    public class DynamicOperand : IOperable
    {
        public IOperable OperableValue { get; private set; }
        public DynamicOperand(IOperable operableValue = null)
            : base(operableValue.Type)
        {
            OperableValue = operableValue;
        }

        #region Dynamic Casts

        public static implicit operator DynamicOperand(Expression expression)
        {
            return expression.Evaluate();
        }

        public static implicit operator DynamicOperand(Variable variableOperand)
        {
            return variableOperand.Evaluate();
        }

        public static implicit operator DynamicOperand(StringOperand stringOperand)
        {
            return new DynamicOperand(stringOperand);
        }

        public static implicit operator DynamicOperand(NumericOperand numericOperand)
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
            return new DynamicOperand(new NumericOperand(result));
        }
        public static DynamicOperand operator /(DynamicOperand dynamicOperand, DynamicOperand otherOperand)
        {
            double result = dynamicOperand.GetDoubleValue() / otherOperand.GetDoubleValue();
            return new DynamicOperand(new NumericOperand(result));
        }
        public static DynamicOperand operator +(DynamicOperand dynamicOperand, DynamicOperand otherOperand)
        {
            double result = dynamicOperand.GetDoubleValue() + otherOperand.GetDoubleValue();
            return new DynamicOperand(new NumericOperand(result));
        }
        public static DynamicOperand operator -(DynamicOperand dynamicOperand, DynamicOperand otherOperand)
        {
            double result = dynamicOperand.GetDoubleValue() - otherOperand.GetDoubleValue();
            return new DynamicOperand(new NumericOperand(result));
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

        public override bool Equals(object obj)
        {
            //Return false if the other is null
            if (obj is null)
            {
                return false;
            }

            //Check for same type
            DynamicOperand otherDynOperand = (DynamicOperand)obj;
            if (otherDynOperand.Type != OperableValue.Type)
            {
                throw new TypeConversionException(OperableValue.Type, otherDynOperand.Type);
            }
            return OperableValue.Equals(otherDynOperand.OperableValue);
        }


        public override int CompareTo(object obj)
        {
            //Throw if the object is null
            if (obj is null)
            {
                throw new ArgumentNullException("CompareTo on 'DynamicOperand' does not support null.");
            }

            //Check for same type
            DynamicOperand otherDynOperand = (DynamicOperand)obj;
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
                OperandType.Number => ((NumericOperand)OperableValue).OperandValue.ToString(),
                OperandType.Boolean => ((BooleanOperand)OperableValue).ToString(),
                OperandType.String => ((StringOperand)OperableValue).OperandValue,
                _ => throw new NotImplementedException()
            };
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, OperableValue);
        }

        #endregion Object Overrides

        public DynamicOperand EvaluateDynamic()
        {
            return Type switch
            {
                OperandType.Expression => ((Expression)OperableValue).Evaluate(),
                OperandType.Variable => ((Variable)OperableValue).Evaluate(),
                OperandType.Boolean => ((BooleanOperand)OperableValue),
                OperandType.Number => ((NumericOperand)OperableValue),
                OperandType.String => ((StringOperand)OperableValue),
                _ => throw new TypeConversionException(OperandType.Unknown, OperandType.Expression)
            };
        }

        public bool GetBooleanValue()
        {
            return Type switch
            {
                OperandType.Expression => ((Expression)OperableValue).Evaluate().GetBooleanValue(),
                OperandType.Boolean => ((BooleanOperand)OperableValue).OperandValue,
                _ => throw new TypeConversionException(Type, OperandType.Boolean)
            };
        }

        public double GetDoubleValue()
        {
            return Type switch
            {
                OperandType.Expression => ((Expression)OperableValue).Evaluate().GetDoubleValue(),
                OperandType.Variable => ((Variable)OperableValue).Evaluate().GetDoubleValue(),
                OperandType.Number => ((NumericOperand)OperableValue).OperandValue,
                _ => throw new TypeConversionException(Type, OperandType.Number)
            };
        }

        public string GetStringValue()
        {
            return Type switch
            {
                OperandType.String => ((StringOperand)OperableValue).OperandValue,
                _ => throw new TypeConversionException(Type, OperandType.String)
            };
        }
    }
}
