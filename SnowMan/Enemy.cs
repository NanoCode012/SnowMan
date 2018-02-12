using System;
namespace SnowMan
{
    class Enemy 
    {
		char sprite;
        bool isAlive;
        int posX;
        int posY;
        Random random = new Random();
        Level level;
        Player player;
        AStarSearch aStar;

        public Enemy(Level lvl, Player ply,char character = '☻')
        {
            isAlive = true;
            level = lvl;
            sprite = character;
            player = ply;
            SetEnemyPos();

            aStar = new AStarSearch(lvl);
        }

        private void SetEnemyPos()
        {
            bool success = false;
            while (!success)
            {
                posX = random.Next(1, level.GetLength() - 1);
                posY = random.Next(1, level.GetLength() - 1);
                if (level.IsWhitespace(posY, posX) && !level.IsGoal(posY, posX)) success = true;
            }
            level.SetCharacter(sprite, posY, posX);
        }

        public void Move(){
            var playerPos = player.GetPosition();
            var newPos = aStar.FindClosest(new Node(posY, posX), new Node(playerPos[1], playerPos[0]));
            level.MoveCharacter(sprite, posY, posX, newPos[1], newPos[0]);
            posX = newPos[0];
            posY = newPos[1];
            if (playerPos[0] == posX && playerPos[1] == posY) player.Die();

        }

        public int[] GetPosition(){
            return new int[] { posX, posY };
        }

        /*
        int[] FindClosestDirection()
        {
            int[] playerPos = player.GetPosition();
            int shortestDist = 1000;
            int[] newPos = new int[2];
            int tempDist, tempPosX, tempPosY;
            for (int x = -1; x <= 1; x++)
            {
				tempPosX = posX + x;
                for (int y = -1; y <= 1; y++)
                {
                    tempPosY = posY + y;
                    tempDist = level.GetDistanceBetweenTwoPoints(tempPosX, tempPosY, playerPos[0], playerPos[1]);
                    if (shortestDist > tempDist)
                    {
                        if (level.IsWhitespace(tempPosY, tempPosX))
                        {
							shortestDist = tempDist;
                            newPos[0] = tempPosX;
                            newPos[1] = tempPosY;

                        }else if (tempPosX == playerPos[0] && tempPosY == playerPos[1])
                        {
                            player.Die();
                            newPos[0] = tempPosX;
                            newPos[1] = tempPosY;
                            break;
                        }
                    }
                }
            }
            return newPos;
        }
        */

        public bool IsAlive() {
            return isAlive;
        }


    }
}
