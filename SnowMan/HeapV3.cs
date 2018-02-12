using System;

namespace SnowMan
{
    class HeapV3<T> where T:IComparable
    {
        T[] arr;
        int size;//Our visual of the BinaryTree's size
        int capacity;//The real capacity of the array

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Snowman.HeapV3"/> class.
        /// </summary>
        public HeapV3(int cap = 10)
        {
            if (cap <= 0) throw new Exception("Cannot initialize heap with cap 0 or less");
            arr = new T[cap];
            size = 0;
            capacity = cap;
        }

        /// <summary>
        /// Peek the lowest value in the heap, the root.
        /// </summary>
        public T Peek()
        {
            if (size == 0) throw new Exception("Size of array equal to 0. Capacity is not 0. Check variable 'size'. Cannot peek from empty.");
            return arr[0];
        }

        /// <summary>
        /// Pops the min, and adjust heap to maintain Min-Heap property.
        /// </summary>
        public T Pop()
        {
            if (size == 0) throw new Exception("Size of array equal to 0. Capacity is not 0. Check variable 'size'. Cannot pop from empty.");
            var min = arr[0];
            Swap(0, size - 1);//Swap last element with first
            size--;//To ignore the past-largest element
            var index = 0;
            while (GotLeftChild(index))
            {
                var smallestValIndex = GetLeftChildIndex(index);
                if (GotRightChild(index) && GetRightChildValue(index).CompareTo(GetLeftChildValue(index)) < 0)
                {
                    smallestValIndex = GetRightChildIndex(index);
                }
                if (arr[index].CompareTo(arr[smallestValIndex]) > 0)
                {
                    Swap(index, smallestValIndex);
                    index = smallestValIndex;
                }
                else break;
            }
            return min;
        }

        /// <summary>
        /// Swap the specified index1 and index2.
        /// </summary>
        private void Swap(int index1, int index2)
        {
            var temp = arr[index1];
            arr[index1] = arr[index2];
            arr[index2] = temp;
        }

        /// <summary>
        /// Multiplies the capacity of an array by a multiplier, default to 2.
        /// </summary>
        private void Expand(int multiplier = 2)
        {
            if (size == capacity)
            {
                Array.Resize(ref arr, capacity * multiplier);
                capacity *= multiplier;
            }
        }

        /// <summary>
        /// Push the specified val, and adjust heap to maintain Min-Heap property.
        /// </summary>
        public void Push(T val)
        {
            var index = size;
            if (size == 0)
            {
                arr[0] = val;
                size++;
            }
            else
            {
                Expand();
                arr[index] = val;
                while (GotParent(index) && GetParentValue(index).CompareTo(val) > 0)
                {
                    Swap(GetParentIndex(index), index);
                    index = GetParentIndex(index);
                }
                size++;
            }
        }

        /// <summary>
        /// Replace the specified oldValue if newValue is smaller
        /// </summary>
        public void Replace(T newValue)
        {
            int index = SearchForIndex(newValue);
            if (arr[index].CompareTo(newValue) > 0)
            {
                arr[index] = newValue;
            }
        }

        public bool CheckIfExists(T val)
        {
            return (SearchForIndex(val) != -1);
        }

        private int SearchForIndex(T val)
        {
            for (int i = 0; i < size; i++)
            {
                if (arr[i].Equals(val))
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Returns count of T.
        /// </summary>
        public int Count()
        {
            return size;
        }

        /// <summary>
        /// Clear array and reset size.
        /// </summary>
        public void Clear()
        {
            Array.Clear(arr,0,size);
            size = 0;
        }

        /// <summary>
        /// Check if got parent.
        /// </summary>
        private bool GotParent(int childIndex)
        {
            return (GetParentIndex(childIndex) >= 0 && childIndex != 0);
        }

        /// <summary>
        /// Check if got child by finding if left-child exists
        /// </summary>
        private bool GotLeftChild(int parentIndex)
        {
            return (GetLeftChildIndex(parentIndex) < size);
        }

        /// <summary>
        /// Check if got child by finding if right-child exists
        /// </summary>
        private bool GotRightChild(int parentIndex)
        {
            return (GetRightChildIndex(parentIndex) < size);
        }



        /// <summary>
        /// Gets the index of the parent from a given child index.
        /// </summary>
        private int GetParentIndex(int childIndex)
        {
            if (childIndex % 2 == 0)// is right-tree
            {
                return (childIndex - 2) / 2;
            }
            else return (childIndex - 1) / 2;
        }

        /// <summary>
        /// Gets the index of the left-child from a given parent index.
        /// </summary>
        private int GetLeftChildIndex(int parentIndex)
        {
            return (parentIndex * 2 + 1);
        }

        /// <summary>
        /// Gets the index of the right-child from a given parent index.
        /// </summary>
        private int GetRightChildIndex(int parentIndex)
        {
            return (parentIndex * 2 + 2);
        }



        /// <summary>
        /// Gets the parent value from a given child index.
        /// </summary>
        private T GetParentValue(int childIndex)
        {
            return arr[GetParentIndex(childIndex)];
        }

        /// <summary>
        /// Gets the left-child value from a given parent index.
        /// </summary>
        private T GetLeftChildValue(int parentIndex)
        {
            return arr[GetLeftChildIndex(parentIndex)];
        }

        /// <summary>
        /// Gets the right-child value from a given child index.
        /// </summary>
        private T GetRightChildValue(int parentIndex)
        {
            return arr[GetRightChildIndex(parentIndex)];
        }

    }
}
