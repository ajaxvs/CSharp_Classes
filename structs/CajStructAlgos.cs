/*
 * User: ajaxvs
 * Date: 21.04.2019
 * Time: 20:52
 */

 /*

Just for tests.
Sort functions: use quickSort if need, it's usually the fastest one.

Unit tests: check CajStructTests.

*/

namespace ajClasses.structs 
{
    public class CajStructAlgos {
        //================================================================================
        private CajStructAlgos() {
            
        }
        //================================================================================
        static public void Shuffle(ref int[] a) {
            int max = a.Length;
            for (int i = 0; i < max / 2; i++) {
                int i2 = CajFuns.rnd(max / 2, max - 1);
                int tmp = a[i];
                a[i] = a[i2];
                a[i2] = tmp;
            }            
        }
        //================================================================================
        static public void BubbleSort(ref int[] a) {
            int count = a.Length;
            if (count < 2) return;
            
            for (int i = 0; i < count - 1; i++) {
                bool sorted = true;
                for (int j = i; j < count; j++) {
                    if (a[i] > a[j]) {
                        int tmp = a[j];
                        a[j] = a[i];
                        a[i] = tmp;
                        sorted = false;
                    }
                }
                if (sorted) {
                    break;
                }
            }
        }
        //================================================================================
        static public void SelectionSort(ref int[] a) {
            int count = a.Length;
            if (count < 2) return;
            
            int index = 0;
            while (index < count) {
                int minValue = a[index];
                int minIndex = index;
                for (int i = index + 1; i < count; i++) {
                    if (a[i] < minValue) {
                        minValue = a[i];
                        minIndex = i;
                    }
                }
                if (minIndex != index) {
                    a[minIndex] = a[index];
                    a[index] = minValue;
                }
                index++;
            }
        }
        //================================================================================
        static private void QuickSort(ref int[] a, int startIndex = 0, int endIndex = -1) {
            //check bounds:
            if (endIndex < 0) {
                endIndex = a.Length;
            }
            int count = endIndex - startIndex;
            if (count < 2) return;
            
            //split array to the two parts:
            int centerIndex = startIndex + count / 2;
            
            for (;;) {
                //find max value from the left part:                 
                int leftIndex = startIndex;
                int leftValue = a[leftIndex];                
                for (int i = startIndex; i < centerIndex; i++) {
                    if (a[i] > leftValue) {
                        leftIndex = i;
                        leftValue = a[i];
                    }
                }
                
                //find min value from the right part:
                int rightIndex = centerIndex;
                int rightValue = a[rightIndex];
                for (int i = rightIndex; i < endIndex; i++) {
                    if (a[i] < rightValue) {
                        rightIndex = i;
                        rightValue = a[i];
                    }
                }
                
                //swap if need:
                if (leftValue > rightValue) {
                    a[rightIndex] = leftValue;
                    a[leftIndex] = rightValue;
                } else {
                    break;
                }
            }
            
            //divide array and repeat split for left and right parts:
            QuickSort(ref a, startIndex, centerIndex);
            QuickSort(ref a, centerIndex, endIndex);
        }
        //================================================================================
        static public void QuickSort(ref int[] a) {
            if (a == null) return;
            QuickSort(ref a, 0, a.Length);
        }
        //================================================================================
        static public long Factorial(long n) {
            if (n <= 1) {
                return 1;
            } else {
                return n * Factorial(n - 1);
            }
        }
        //================================================================================
        //max for long:  92 = 4660046610375530309
        //so it's possible to create an array[93] for O(1) access.
        static public long Fibonacci(long n) {
            long prev = 0;
            long next = 1;
            long tmp;
            for (long i = 0; i < n; i++) {
                tmp = next;
                next = next + prev;
                prev = tmp;
            }
            return prev;
        }
        //================================================================================
        static private void FibonacciRecursion(ref long prev, ref long next, long n) {
            if (n > 0) {
                long tmp = next;
                next += prev;
                prev = tmp;
                FibonacciRecursion(ref prev, ref next, n - 1);
            }
        }        
        //================================================================================
        static public long FibonacciRecursion(long n) {
            long prev = 0;
            long next = 1;
            FibonacciRecursion(ref prev, ref next, n);
            return prev;
        }
        //================================================================================
        /*
        static private long FibonacciRecursion_Bad(long n) {            
            //i mean really bad. too much time if n > 40. nn:
            if (n <= 1) {
                return n;
            } else {
                return FibonacciRecursion_Bad(n - 1) + FibonacciRecursion_Bad(n - 2);
            }
        }
        */
        //================================================================================
    }
}
