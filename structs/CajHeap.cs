/*
 * User: ajaxvs
 * Date: 16.04.2019
 * Time: 17:21
 */
using System;
using System.Collections.Generic;

/*
  
Binary max-heap.
 
 */

namespace ajClasses.structs 
{
    public class CajHeap<T> where T : IEquatable<T>, IComparable<T> {
        //================================================================================
        private readonly List<T> aValues = new List<T>();
        //================================================================================
        public CajHeap() {
            
        }
        //================================================================================
        public void CopyFrom(IEnumerable<T> aList) {
            aValues.AddRange(aList);
            for (int i = aValues.Count / 2; i >= 0; i--) {
                Correct(i);
            }
        }
        //================================================================================
        public void Clear() {
            aValues.Clear();
        }
        //================================================================================
        public void Add(T value) {
            aValues.Add(value);
            
            int index = aValues.Count - 1;
            T parentValue;
            
            while (index > 0) {
                int parentIndex = (index - 1) / 2;
                parentValue = aValues[parentIndex];
                
                if (value.CompareTo(parentValue) > 0) {
                    aValues[parentIndex] = value;
                    aValues[index] = parentValue;
                    
                    index = parentIndex;
                } else {
                    break;
                }
            }
        }
        //================================================================================
        public T GetMax() {
            //handle exception?
            //if (aValues.Count == 0) {} 
            
            return aValues[0];
        }
        //================================================================================
        public int Count {
            get {
                return aValues.Count;
            }
        }
        //================================================================================
        public void Remove(T value) {
            for (;;) {
                int index = aValues.IndexOf(value);
                if (index == -1) {
                    break;
                } else {
                    int lastIndex = aValues.Count - 1;
                    aValues[index] = aValues[lastIndex];
                    aValues.RemoveAt(lastIndex);                    
                    Correct(index);
                }
            }
        }
        //================================================================================
        private void Correct(int index) {
            int leftChildIndex;
            int rightChildIndex;
            int maxIndex;
            int count = aValues.Count;
            T tmp;
            
            for (;;) {
                maxIndex = index;
                leftChildIndex = 2 * index + 1;
                rightChildIndex = 2 * index + 2;
                
                if (leftChildIndex < count && 
                    aValues[leftChildIndex].CompareTo(aValues[maxIndex]) > 0) {
                    maxIndex = leftChildIndex;
                }                
                if (rightChildIndex < count && 
                    aValues[rightChildIndex].CompareTo(aValues[maxIndex]) > 0) {
                    maxIndex = rightChildIndex;
                }
                
                if (maxIndex == index) {
                    break;
                }
                
                tmp = aValues[index];
                aValues[index] = aValues[maxIndex];
                aValues[maxIndex] = tmp;
                index = maxIndex;
            }
        }
        //================================================================================
        public void Sort() {            
            int count = aValues.Count;
            if (count < 2) return;
            
            T tmp;
            List<T> newList = new List<T>();
            for (int i = 0; i < count; i++) {
                Correct(0);                
                tmp = aValues[0];
                newList.Add(tmp);
                Remove(tmp);
            }
            
            aValues.Clear();
            aValues.AddRange(newList);
        }
        //================================================================================
    }
}
