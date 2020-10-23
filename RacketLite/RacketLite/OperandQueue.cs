using RacketLite.Exceptions;
using RacketLite.Operands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RacketLite
{
    public class OperandQueue  : IEnumerable<DynamicOperand>
    {
        private int TailIndex = -1;
        private const int HeadIndex = 0;

        public int Count => TailIndex + 1;
        private DynamicOperand[] queueData;
        public RacketOperandType[] OperandTypes => queueData[0..Count].Select(op => op.Type).ToArray();

        /// <summary>
        /// Creates an operand queue with a default intitial capacity of 10
        /// </summary>
        /// <param name="capacity">The initial size of the operand queue</param>
        public OperandQueue(int capacity = 10)
        {
            queueData = new DynamicOperand[capacity];
        }

        /// <summary>
        /// Creates an operand queue using the data provided form the queue array
        /// </summary>
        /// <param name="queue">The data to initialize the queue with</param>
        /// <param name="count">The amount of data preset into the array</param>
        public OperandQueue(DynamicOperand[] queue, int count)
        {
            queueData = queue;
            TailIndex = count - 1;
        }

        /// <summary>
        /// Replaces "unknown" operands found within the operand queue
        /// </summary>
        /// <param name="localVarValues">The values to set to each operand of type unknown</param>
        public void ReplaceUnknowns(Dictionary<string, DynamicOperand> localVarValues)
        {
            for(int i = 0; i < Count; i++)
            {
                if (queueData[i].Type == RacketOperandType.Unknown)
                {
                    //If now locals are not defined then we have a problem
                    string unknownValue = queueData[i].GetUnknownValue().ToString();
                    if (localVarValues == null || localVarValues.Count == 0)
                    {
                        throw new VariableNotFoundException(unknownValue);
                    }

                    //Check for local variable stored as Unknown
                    else if (localVarValues.ContainsKey(unknownValue))
                    {
                        queueData[i] = localVarValues[unknownValue];
                    }
                }
            }
        }

        /// <summary>
        /// Ensures that the operand queue does not contain any "unknown" operands (not including locals)
        /// Throws an error if any "unknowns" are found
        /// </summary>
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

        /// <summary>
        /// Looks for a number operand that is flagged as inexact
        /// </summary>
        /// <returns>Boolean, true if a value in the queue is flagged as inexact</returns>
        public bool ContainsInexact()
        {
            for (int i = 0; i < Count; i++)
            {
                if (queueData[i] != null)
                {
                    if (queueData[i].Type == RacketOperandType.Number
                        && ((NumberOperand)queueData[i].OperableValue).Inexact)
                    {
                        return true;
                    }
                    else if (queueData[i].Type == RacketOperandType.Integer
                        && ((IntegerOperand)queueData[i].OperableValue).Inexact)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if the queue needs to be resized, if needed, the queue will be resized to double that of before
        /// </summary>
        private void Resize()
        {
            if(TailIndex + 1 < queueData.Length)
            {
                return;
            }

            DynamicOperand[] newArray = new DynamicOperand[queueData.Length * 2];
            Array.Copy(queueData, 0, newArray, 0, queueData.Length);
            queueData = newArray;
        }

        /// <summary>
        /// Dequeues a number of operands, only returning the value of the last one
        /// </summary>
        /// <param name="dequeueCount">The amount of operands to dequeue</param>
        /// <returns>The last operand dequeued from the queue</returns>
        public DynamicOperand Dequeue(int dequeueCount)
        {
            DynamicOperand lastNodeOperand = Dequeue();
            for (int i = 1; i < dequeueCount; i++)
            {
                lastNodeOperand = Dequeue();
            }
            return lastNodeOperand;
        }

        /// <summary>
        /// Enqueues a collection of operands to the front of the queue
        /// </summary>
        /// <param name="operands">The collection of operands to enqueue</param>
        public void Enqueue(IEnumerable<DynamicOperand> operands)
        {
            foreach (DynamicOperand operand in operands)
            {
                Enqueue(operand);
            }
        }

        /// <summary>
        /// Removes the operand at the front of the queue
        /// </summary>
        /// <returns>The operand at the front of the queue</returns>
        public DynamicOperand Dequeue()
        {
            TailIndex--;
            DynamicOperand headValue = queueData[HeadIndex];
            Array.Copy(queueData, 1, queueData, 0, queueData.Length - 1);
            return headValue;
        }

        /// <summary>
        /// Enqueues an operand at the end of the queue.
        /// </summary>
        /// <param name="operand">The operand to enqeue</param>
        public void AddLast(DynamicOperand operand)
        {
            Resize();
            Array.Copy(queueData, 0, queueData, 1, queueData.Length - 1);
            queueData[0] = operand;
            TailIndex++;
        }

        /// <summary>
        /// Enqueues an operand into the queue of operands
        /// </summary>
        /// <param name="operand">The operand to enqueue</param>
        public void Enqueue(DynamicOperand operand)
        {
            Resize();
            TailIndex++;
            queueData[TailIndex] = operand;
        }

        /// <summary>
        /// Returns the array backing the operand queue
        /// </summary>
        /// <returns>Returns the the operands in the queue as an array</returns>
        public DynamicOperand[] ToArray()
        {
            return queueData[0..Count];
        }

        /// <summary>
        /// Selects the operand at the front of the queue
        /// </summary>
        /// <returns>The operand at the front of the queue</returns>
        public DynamicOperand Peek()
        {
            return queueData[HeadIndex];
        }

        #region IEnumerable Methods
        public IEnumerator<DynamicOperand> GetEnumerator()
        {
            return queueData[0..Count].AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return queueData[0..Count].GetEnumerator();
        }
        #endregion IEnumerable Methods
    }
}
