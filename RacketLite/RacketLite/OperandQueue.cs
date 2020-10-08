using RacketLite.Exceptions;
using RacketLite.Operands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RacketLite
{
    public class OperandQueue
    {
        private int TailIndex = -1;
        private const int HeadIndex = 0;

        public int Count => TailIndex + 1;
        private readonly DynamicOperand[] queueData;
        public RacketOperandType[] OperandTypes => queueData[0..Count].Select(op => op.Type).ToArray();

        public OperandQueue(int capacity = 10)
        {
            queueData = new DynamicOperand[capacity];
        }

        private OperandQueue(DynamicOperand[] queue, int tailIndex)
        {
            queueData = queue;
            TailIndex = tailIndex;
        }

        public OperandQueue ReplaceUnknowns(Dictionary<string, DynamicOperand> localVarValues)
        {
            //Create new copy of queueData
            DynamicOperand[] newQueueData = new DynamicOperand[queueData.Length];
            queueData.CopyTo(newQueueData, 0);

            for(int i = 0; i < newQueueData.Length; i++)
            {
                if (newQueueData[i] != null && newQueueData[i].Type == RacketOperandType.Unknown)
                {
                    //If now locals are defined then we have a problem
                    string unknownValue = newQueueData[i].GetUnknownValue().ToString();
                    if (localVarValues == null || localVarValues.Count == 0)
                    {
                        throw new VariableNotFoundException(unknownValue);
                    }

                    //Check for local variable stored as Unknown
                    else if (localVarValues.ContainsKey(unknownValue))
                    {
                        newQueueData[i] = localVarValues[unknownValue];
                    }
                }
            }
            return new OperandQueue(newQueueData, TailIndex);
        }

        public void ValidateOperandQueue()
        {
            for (int i = 0; i < queueData.Length; i++)
            {
                if (queueData[i] != null && queueData[i].Type == RacketOperandType.Unknown)
                {
                    //If now locals are defined then we have a problem
                    string unknownValue = queueData[i].GetUnknownValue().ToString();
                    if (StaticsManager.LocalStack == null || StaticsManager.LocalStack.Count == 0)
                    {
                        throw new VariableNotFoundException(unknownValue);
                    }

                    //Check for local variable stored as Unknown
                    else if (!StaticsManager.LocalStack.ContainsKey(unknownValue))
                    {
                        throw new VariableNotFoundException(unknownValue);
                    }
                }
            }
        }

        public DynamicOperand Dequeue(int dequeueCount)
        {
            DynamicOperand lastNodeOperand = Dequeue();
            for (int i = 1; i < dequeueCount; i++)
            {
                lastNodeOperand = Dequeue();
            }
            return lastNodeOperand;
        }

        public void Enqueue(IEnumerable<DynamicOperand> operands)
        {
            foreach (DynamicOperand operand in operands)
            {
                Enqueue(operand);
            }
        }

        public DynamicOperand Dequeue()
        {
            TailIndex--;
            DynamicOperand headValue = queueData[HeadIndex];
            Array.Copy(queueData, 1, queueData, 0, queueData.Length - 1);
            return headValue;
        }

        public void AddLast(DynamicOperand operand)
        {
            Array.Copy(queueData, 0, queueData, 1, queueData.Length - 1);
            queueData[0] = operand;
            TailIndex++;
        }

        public void Enqueue(DynamicOperand operand)
        {
            TailIndex++;
            queueData[TailIndex] = operand;
        }

        internal DynamicOperand[] ToArray()
        {
            return queueData;
        }

        public OperandQueue GetCopy()
        {
            return new OperandQueue(queueData, TailIndex);
        }

        public DynamicOperand Peek()
        {
            return queueData[HeadIndex];
        }
    }
}
