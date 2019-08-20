/*
 * User: ajaxvs
 * Date: 15.04.2019
 * Time: 17:03
 */
using System;
using System.Collections.Generic;

namespace ajClasses.structs 
{
    public class CajLinkedList<T> where T : IEquatable<T> {
        //================================================================================
        public class Node<M> where M : IEquatable<M> {
            //=====
            public M Value;
            public Node<M> Next;
            //=====
        }
        //================================================================================
        private Node<T> mFirst;
        private Node<T> mLast;
        //================================================================================
        public CajLinkedList() {            

        }
        //================================================================================
        public Node<T> First {
            get {
                return mFirst;
            }
        }
        //================================================================================
        public long Count {
            get {
                long amount = 0;
                Node<T> node = mFirst;
                while (node != null) {
                    amount++;
                    node = node.Next;
                }
                return amount;
            }
        }
        //================================================================================
        public void AddFirst(T value) {
            var node = new Node<T>();
            node.Value = value;
            
            if (mFirst == null) {
                mFirst = node;
                mLast = node;
            } else {
                node.Next = mFirst;
                mFirst = node;
            }
        }
        //================================================================================
        public void AddLast(T value) {
            var node = new Node<T>();
            node.Value = value;
            
            if (mFirst == null) {
                mFirst = node;
            }
            if (mLast != null) {
                mLast.Next = node;
            }
            mLast = node;
        }
        //================================================================================
        public void Remove(T value, bool removeAll = true) {
            Node<T> current = mFirst;
            Node<T> prev = null;
            
            while (current != null) {
                if (current.Value.Equals(value)) {
                    if (current == mFirst) {
                        mFirst = current.Next;                        
                    }
                    if (prev != null) {
                        prev.Next = current.Next;
                    }
                    if (current == mLast) {
                        mLast = prev;
                    }
                    if (!removeAll) {
                        return;
                    }
                }
                prev = current;
                current = current.Next;
            }
        }
        //================================================================================
        public void Clear() {
            mFirst = null;
            mLast = null;
        }
        //================================================================================
        public T[] ToArray() {
            List<T> a = new List<T>();
            
            Node<T> current = mFirst;
            while (current != null) {
                a.Add(current.Value);
                current = current.Next;
            }
            
            return a.ToArray();
        }
        //================================================================================
        public void Reverse() {
            Node<T> current = mFirst;
            Node<T> prev = null;
            
            mLast = mFirst;
            
            while (current != null) {
                var next = current.Next;
                current.Next = prev;
                
                prev = current;
                current = next;                
            }
            
            mFirst = prev;
        }
        //================================================================================
    }
}
