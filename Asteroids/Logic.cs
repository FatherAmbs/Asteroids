using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Logic
    {
        private static Random rand = new Random();


        public static void UpdateLasers(ContentManager contentManager, Ship ship, List<Laser> laserList)
        {
            if (ship.HasFired())
            {
                if (laserList.Count < 5)
                {
                    laserList.Add(new Laser(contentManager, @"graphics\laser", ship.GetX(), ship.GetY(),
                        GameConstants.WindowWidth, GameConstants.WindowHeight, ship.GetAngle()));
                }

            }
            for (int i = laserList.Count - 1; i >= 0; i--)
            {
                laserList[i].Update();
                if (IsAbsorbed(laserList[i]))
                {
                    laserList.RemoveAt(i);
                }
            }
        }

        public static Rock SpawnRock(ContentManager contentManager)
        {
            Vector2 rockPosition = RockStart();
            return new Rock(contentManager, @"graphics\rock",
                            (int)rockPosition.X, (int)rockPosition.Y,
                            rand.Next(5) + 2, 2 * Math.PI * rand.NextDouble(),
                            GameConstants.WindowWidth, GameConstants.WindowHeight);
        }

        public static bool CollisionCheck(List<Laser> laserList, List<Rock> rockList, GameSprite ship)
        {
            //Check if lasers have hit rocks. If so, remove both sprites

            for (int i = laserList.Count - 1; i >= 0; i--)
            {
                for (int j = rockList.Count - 1; j >= 0; j--)
                {
                    if (laserList[i].Collision(rockList[j]))
                    {
                        Game1.IncreaseScore();
                        laserList.RemoveAt(i);
                        rockList.RemoveAt(j);
                        break;
                    }
                }
            }

            //Check if rocks hit each other.If so, bounce rocks
            for (int i = rockList.Count - 1; i >= 0; i--)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    if (rockList[i].Collision(rockList[j]))
                    {
                        Bounce(rockList[i], rockList[j]);
                        break;
                    }
                }
            }

            //Check if a rock has hit our ship. If so, a fatal collision has taken place
            foreach (Rock r in rockList)
            {
                if (ship.Collision(r))
                {
                    //return true;
                }
            }

            return false;
        }

        private static void Bounce(GameSprite gs1, GameSprite gs2)
        {
            // TODO: implement proper bouncing

            //gs1.SetVelocity(-gs1.GetVelocity());
            //gs2.SetVelocity(-gs2.GetVelocity());
        }
        
        private static Vector2 RockStart()
        {
            int startingWall = rand.Next(4);
            Vector2 rockPosition = new Vector2(0, 0);
            switch (startingWall)
            {
                case 0:
                    //Left wall
                    rockPosition.X = 0;
                    rockPosition.Y = rand.Next(GameConstants.WindowHeight);
                    break;
                case 1:
                    //Top wall
                    rockPosition.X = rand.Next(GameConstants.WindowWidth);
                    rockPosition.Y = 0;
                    break;
                case 2:
                    //Right wall
                    rockPosition.X = GameConstants.WindowWidth;
                    rockPosition.Y = rand.Next(GameConstants.WindowHeight);
                    break;
                case 4:
                    //Bottom wall
                    rockPosition.X = rand.Next(GameConstants.WindowWidth);
                    rockPosition.Y = GameConstants.WindowHeight;
                    break;
            }
            return rockPosition;
        }


        private static bool IsAbsorbed(GameSprite gs)
        {
            return (gs.GetRect().X < 0 || gs.GetRect().Y < 0 || gs.GetRect().X > GameConstants.WindowWidth || gs.GetRect().Y > GameConstants.WindowHeight);
        }
    }
}
