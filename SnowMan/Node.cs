using System;
using System.Collections.Generic;

namespace SnowMan
{
    class Node : IComparable , IEqualityComparer<Node>
    {
        public Node Parent { get; set; }

        public int CostDistFromStartNode { get; set; }
        public int CostEstDistToEndNode { get; set; }
        public int CostTotalDist 
        {
            get 
            {
                return CostEstDistToEndNode + CostDistFromStartNode;
            }
        }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int ID { get; set; }

        public Node(int row, int column, int length = 10)
        {
            PosY = row;
            PosX = column;
            ID = row * (length-2) + column;
        }

        public int CompareTo(object n)
        {
            if (n is Node)
            {
                return this.CostTotalDist - ((Node)n).CostTotalDist;
            }
            else throw new Exception("Cannot compare with Non-Node objects");
        }

        /// <summary>
        /// Return ID
        /// </summary>
        public int GetHashCode(Node n)
        {
            return this.ID;
        }

        public bool Equals(Node n)
        {
            return (this.ID == n.ID);
        }

        public bool Equals(Node a, Node b)
        {
            return (a.ID == b.ID);
        }




    }
}
