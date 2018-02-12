using System;
using System.Collections.Generic;

namespace SnowMan
{
    class AStarSearch
    {
        Node start;
        Node end;
        Node[,] grid;

        Level level;

        readonly int length;
        int diagonalMovementCost = 14;
        int horizontalMovementCost = 10;

        private HeapV3<Node> openList = new HeapV3<Node>();
        private Dictionary<int, Node> closedList = new Dictionary<int, Node>();
        public AStarSearch(Level lvl)
        {
            this.level = lvl;
            length = lvl.GetLength();
        }

        /// <summary>
        /// Finds the best adjacent Node.
        /// </summary>
        /// <returns>The new position(x, y) of the best node to approach.</returns>
        public int[] FindClosest(Node enemy, Node player)
        {
            this.start = enemy;
            this.end = player;

			SetGrid();
            Node tempNode = grid[enemy.PosY, enemy.PosX];
            int dist = tempNode.CostDistFromStartNode;
            AddIntoOpenList(tempNode, dist);
            while (openList.Count() > 0)
            {
                tempNode = PopSmallestTotalDist();
				dist = tempNode.CostDistFromStartNode;
                AddIntoClosedList(tempNode);
                if (tempNode.ID == player.ID) break;
                AddAdjacentNonExploredNodes(tempNode, dist);
            }

            while(tempNode.Parent.ID != enemy.ID)
            {
                tempNode = tempNode.Parent;
            }

            openList.Clear();
            closedList.Clear();
            return new int[] {tempNode.PosX, tempNode.PosY};

        }

        private void AddAdjacentNonExploredNodes(Node origin, int distTravelled)
        {
            foreach (var neighbour in GetNeighbours(origin))
            {
                if (neighbour != null) 
                {
                    neighbour.Parent = origin;
                    AddIntoOpenList(neighbour, distTravelled + GetDistanceBetweenTwoPoints(origin.PosX, origin.PosY, neighbour.PosX, neighbour.PosY));
                }
            }
        }

        private Node[,] GetNeighbours(Node origin)
        {
            Node[,] neighbours = new Node[3, 3];
            for (var row = origin.PosY - 1; row <= origin.PosY + 1; row++)
            {
                for (var column = origin.PosX - 1; column <= origin.PosX + 1; column++)
                {
                    Node current = grid[row, column];
                    if (current == null) continue;
                    if (current == origin) continue;
                    if (!closedList.ContainsKey(current.ID)){
                        neighbours[row - origin.PosY + 1, column - origin.PosX + 1] = current;
                    } 
                }
            }
            return neighbours;
        }

        private Node PopSmallestTotalDist()
        {
			//Node smallestCostNode = openList[0];
            //int smallestCost = smallestCostNode.CostTotalDist;
            //foreach (var node in openList)
            //{
            //    if (node.CostTotalDist < smallestCost)
            //    {
            //        smallestCost = node.CostTotalDist;
            //        smallestCostNode = node;
            //    }
            //}
            //openList.Remove(smallestCostNode);
            return openList.Pop();
        }

        private void AddIntoOpenList(Node tempNode, int distTravelled)
        {
            tempNode.CostDistFromStartNode = distTravelled;

            //Check in heap if tempNode exists

            if (openList.CheckIfExists(tempNode))
            {
                //Replace if tempNode's totalDist is smaller
                openList.Replace(tempNode);
            }
            else
            {
				openList.Push(tempNode);
            }

            //openList.Push(tempNode);
        }

        private void AddIntoClosedList(Node tempNode)
        {
            /*
            if (!closedList.ContainsKey(tempNode.ID))
            {
				closedList.Add(tempNode.ID, tempNode);
            }

            else
            {
                Node alreadyExistingNode;
                if (closedList.TryGetValue(tempNode.ID, out alreadyExistingNode))
                {
                    if (alreadyExistingNode.CostTotalDist > tempNode.CostTotalDist)
                    {
                        closedList.Remove(alreadyExistingNode.ID);
                        closedList.Add(tempNode.ID, tempNode);
                    }
                }
            }
			*/
            closedList.Add(tempNode.ID, tempNode);
        }

        private void SetGrid()
        {
            grid = new Node[length, length];
            for (var row = 1; row < length - 1; row++)
            {
                for (var column = 1; column < length - 1; column++)
                {
                    if (!level.IsObstacle(row, column))
                    {
						grid[row, column] = new Node(row, column);
                        SetDistToEnd(grid[row, column]);
                    }
                }
            }
        }

        private void SetDistToEnd(Node node)
        {
            node.CostEstDistToEndNode = GetDistanceBetweenTwoPoints(node.PosX, node.PosY, end.PosX, end.PosY);
        }

        /// <summary>
        /// Overwrite method for check Node's presence. For testing purposes.
        /// </summary>
        public void Overwrite(){
            for (var row = 1; row < length - 1; row++){
                for (var column = 1; column < length - 1; column++){
                    if (grid[row, column] != null) 
                    {
                        level.SetCharacter('a', row, column);
                        Console.WriteLine("At pos [{0},{1}], cost to endNode is {2}", row, column, grid[row, column].CostEstDistToEndNode);
                    }
                }
            }
        }

        private int GetDistanceBetweenTwoPoints(int startX, int startY, int endX, int endY)
        {
            int diffX = endX - startX;
            int diffY = endY - startY;

            int diagonalDist = 0;
            int horizontalAndVerticalDist;

            if (diffX >= 1 && diffY >= 1)
            {
                while (diffX >= 1 && diffY >= 1)
                {
                    diagonalDist += diagonalMovementCost;
                    diffX -= 1;
                    diffY -= 1;
                }
            }
            else if (diffX <= -1 && diffY <= -1)
            {
                while (diffX <= -1 && diffY <= -1)
                {
                    diagonalDist += diagonalMovementCost;
                    diffX += 1;
                    diffY += 1;
                }
            }
            else if (diffX <= -1 && diffY >= 1)
            {
                while (diffX <= -1 && diffY >= 1)
                {
                    diagonalDist += diagonalMovementCost;
                    diffX += 1;
                    diffY -= 1;
                }
            }
            else if (diffX >= 1 && diffY <= -1)
            {
                while (diffX >= 1 && diffY <= -1)
                {
                    diagonalDist += diagonalMovementCost;
                    diffX -= 1;
                    diffY += 1;
                }
            }
            horizontalAndVerticalDist = (Math.Abs(diffX) + Math.Abs(diffY))*horizontalMovementCost;
            return horizontalAndVerticalDist + diagonalDist;
        }
    }
}
