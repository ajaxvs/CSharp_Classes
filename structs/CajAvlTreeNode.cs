/*
 * User: ajaxvs
 * Date: 13.04.2019
 * Time: 23:17
 */
using System;
using System.Collections.Generic;

/*
 
AVL tree.
Port from c++. Should be more optimized. 
Tests: see CajStructTests.

 */

namespace ajClasses.structs
{
    public class CajAvlTreeNode {
        //================================================================================
        public bool isUniqueKeys = true;
        //================================================================================
        public long key = 0;
        public byte height = 1;        
        public CajAvlTreeNode left = null;
        public CajAvlTreeNode right = null;
        //================================================================================
        public CajAvlTreeNode() {
            
        }
        //================================================================================
        public CajAvlTreeNode(long key) {
            this.key = key;
        }
        //================================================================================
        public CajAvlTreeNode(CajAvlTreeNode copy) {
            CopyFrom(copy);
        }
        //================================================================================
        public void Insert(long key) {
            var res = Insert(key, this);
            CopyFrom(res);
        }        
        //================================================================================
        public void Delete(int key) {
            var res = Delete(key, this);
            CopyFrom(res);
        }
        //================================================================================
        public void CopyFrom(CajAvlTreeNode node) {
            CajAvlTreeNode newLeft = null;
            if (node.left != null) {
                newLeft = new CajAvlTreeNode();
                newLeft.CopyFrom(node.left);
            }

            CajAvlTreeNode newRight = null;
            if (node.right != null) {
                newRight = new CajAvlTreeNode();
                newRight.CopyFrom(node.right);
            }
            
            this.key = node.key;            
            this.height = node.height;
            this.left = newLeft;
            this.right = newRight;
        }
        //================================================================================
        ///Left - CenterRoot - Right
        public void TraverseLCR(Action<CajAvlTreeNode> callback) {
            TraverseLCR(callback, this);
        }
        //================================================================================
        ///Left - CenterRoot - Right, all nodes
        public List<CajAvlTreeNode> TraverseLCR() {
            List<CajAvlTreeNode> aNodes = new List<CajAvlTreeNode>();

            Action<CajAvlTreeNode> callback = node => aNodes.Add(node);
            
            TraverseLCR(callback, this);
            
            return aNodes;
        }        
        //================================================================================
        ///most left 
        public CajAvlTreeNode GetMin(CajAvlTreeNode startNode = null) {
            if (startNode == null) {
                return (left != null ? GetMin(left) : this);
            }
            
            return (startNode.left != null ? GetMin(startNode.left) : startNode);    
        }    
        //================================================================================
        ///most right
        public CajAvlTreeNode GetMax(CajAvlTreeNode startNode = null) {
            if (startNode == null) {
                return (right != null ? GetMax(right) : this);
            }
            
            return (startNode.right != null ? GetMax(startNode.right) : startNode);
        }
        //================================================================================
        ///creates an incorrect tree. Useless and absurd. Just for tests and Google Team: 
        public void Inverse(CajAvlTreeNode startNode = null) {
            if (startNode == null) {
                Inverse(this);
                return;
            }
            
            if (startNode.left != null) {
                Inverse(startNode.left);
            }
            if (startNode.right != null) {
                Inverse(startNode.right);
            }
            
            CajAvlTreeNode tmp = startNode.right;
            startNode.right = startNode.left;
            startNode.left = tmp;
        }
        //================================================================================
        override public string ToString() {
            return this.key.ToString();
        }
        //================================================================================
        /// <returns>right.height - left.height</returns>
        private int GetBalanceFactor() {
            return (right != null ? right.height : 0) - (left != null ? left.height : 0);
        }
        //================================================================================
        private void FixHeight() {
            const byte noHeight = 0;
            
            byte leftH = left != null ? left.height : noHeight;
            byte rightH = right != null ? right.height : noHeight;

            byte newHeight = 1;
            if (leftH > rightH) {
                newHeight += leftH;
            } else {
                newHeight += rightH;
            }
            
            this.height = newHeight;
        }
        //================================================================================
        private CajAvlTreeNode RotateLeft() {
            CajAvlTreeNode p = this.right;
            this.right = p.left;
            p.left = this;
            
            this.FixHeight();
            p.FixHeight();

            return p;
        }          
        //================================================================================
        private CajAvlTreeNode RotateRight() {
            CajAvlTreeNode q = this.left;
            this.left = q.right;
            q.right = this;
            
            this.FixHeight();
            q.FixHeight();

            return q;
        }   
        //================================================================================
        private CajAvlTreeNode Balance() {
            this.FixHeight();
            
            int bf = GetBalanceFactor();
            
            CajAvlTreeNode rotated = null;
            if (bf == 2) {
                if (right.GetBalanceFactor() < 0 ) {
                    right = right.RotateRight();
                }                    
                rotated = this.RotateLeft();
            } else if (bf == -2) {
                if (left.GetBalanceFactor() > 0) {
                    left = left.RotateLeft();
                }
                rotated = this.RotateRight();
            }
            
            if (rotated != null) {
                return rotated;
            }
            
            return this;
        } 
        //================================================================================
        private CajAvlTreeNode Insert(long key, CajAvlTreeNode node) {
            if (node == null) {
                CajAvlTreeNode newNode = new CajAvlTreeNode();
                newNode.key = key;
                return newNode;
            }
            
            if (key < node.key) {
                node.left = Insert(key, node.left);
            } else {
                if (isUniqueKeys && key == node.key) {
                    //throw exception?
                } else {
                    node.right = Insert(key, node.right);
                }
            }
            
            node = node.Balance();
            return node;
        }        
        //================================================================================
        private CajAvlTreeNode DeleteMin(CajAvlTreeNode node) {
            if (node.left == null) {
                return node.right;
            }
            node.left = DeleteMin(node.left);
            
            node = node.Balance();
            return node;
        }
        //================================================================================
        private CajAvlTreeNode Delete(int key, CajAvlTreeNode node) {
            if (node == null) return null;
    
            if (key < node.key) {
                node.left = Delete(key, node.left);
            } else if (key > node.key) {
                node.right = Delete(key, node.right);    
            } else {
                CajAvlTreeNode q = node.left;
                CajAvlTreeNode r = node.right;
                
                if (r == null) return q;
                
                CajAvlTreeNode min = r.GetMin();
                min.right = r.DeleteMin(r);
                min.left = q;
                
                min = min.Balance();
                return min;
            }
            
            node = node.Balance();
            return node;
        }     
        //================================================================================        
        private void TraverseLCR(Action<CajAvlTreeNode> callback, CajAvlTreeNode node) {
            if (node.left != null) {
                TraverseLCR(callback, node.left);
            }
            
            callback(node);
            
            if (node.right != null) {
                TraverseLCR(callback, node.right);
            }
        }
        //================================================================================
    }
}
