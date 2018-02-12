namespace SnowMan
{
    class Player : Character
    {
        char sprite;
        int posX = 1;
        int posY = 1;
        bool isAlive;
        Level level;

        public Player(Level lvl, char character = '☃')
        {
			isAlive = true;
            level = lvl;
            sprite = character;
            level.SetCharacter(sprite, posY, posX);
        }

        public override void Move(char opt){
            if (CheckIfWhitespace(opt) || CheckIfIsGoal(opt) ){//Player cannot eat enemy
                if (CheckIfIsGoal(opt)) level.SetWin(true);
				int tempX = posX;
                int tempY = posY;
                switch (opt)
                {
                    case 'W':
                        posY -= 1;
                        break;
                    case 'A':
                        posX -= 1;
                        break;
                    case 'S':
                        posY += 1;
                        break;
                    case 'D':
                        posX += 1;
                        break;
                }
                level.MoveCharacter(sprite, tempY, tempX, posY, posX);

            }
        }

        public bool CheckIfWhitespace(char opt){
            switch(opt)
            {
                case 'W':
                    return level.IsWhitespace(posY - 1, posX);
                case 'A':
                    return level.IsWhitespace(posY, posX - 1);
                case 'S':
                    return level.IsWhitespace(posY + 1, posX);
                case 'D':
                    return level.IsWhitespace(posY, posX + 1);
                default:
                    return false;
            }
        }

        public bool CheckIfIsGoal(char opt){
            switch (opt)
            {
                case 'W':
                    return level.IsGoal(posY - 1, posX);
                case 'A':
                    return level.IsGoal(posY, posX - 1);
                case 'S':
                    return level.IsGoal(posY + 1, posX);
                case 'D':
                    return level.IsGoal(posY, posX + 1);
                default:
                    return false;
            }
        }

        public bool IsAlive(){
            return (isAlive);
        }

        public void Die(){
            isAlive = false;
        }

        public int[] GetPosition(){
            return new int[] { posX, posY };
        }
    }
}
