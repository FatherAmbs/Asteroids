using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Asteroids
{
    class Rock : GameSprite
    {

        private static Random rand = new Random();
        private int speed;
        private double angle;

        public Rock(ContentManager contentManager, string spriteName, int x, int y, int speed, double angle,
            int windowWidth, int windowHeight) 
            : base(contentManager, spriteName, x, y, windowWidth, windowHeight)
        {

            this.speed = speed;
            this.angle = angle;

            velocity.X = (float)(Math.Cos(angle) * speed);
            velocity.Y = (float)(Math.Sin(angle) * speed);

        }

        public override void Update()
        {
            drawRectangle.X += (int)velocity.X;
            drawRectangle.Y += (int)velocity.Y;

            BounceTopBottom();
            BounceLeftRight();
        }

        private void BounceTopBottom()
        {
            if (drawRectangle.Y < 0)
            {
                //Bounce off top
                drawRectangle.Y = 0;
                velocity.Y *= -1;
            }

            else if ((drawRectangle.Y + drawRectangle.Height) > windowHeight)
            {
                //Bounce off bottom
                drawRectangle.Y = windowHeight - drawRectangle.Height;
                velocity.Y *= -1;
            }
        }

        private void BounceLeftRight()
        {
            if (drawRectangle.X < 0)
            {
                //Bounce off left
                drawRectangle.X = 0;
                velocity.X *= -1;
            }

            else if ((drawRectangle.X + drawRectangle.Width) > windowWidth)
            {
                //Bounce off left
                drawRectangle.X = windowWidth - drawRectangle.Width;
                velocity.X *= -1;
            }
        }
    }
}
