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

        static Random rand = new Random();
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
            //move rocks
            drawRectangle.X += (int)velocity.X;
            drawRectangle.Y += (int)velocity.Y;

            BounceTopBottom();
            BounceLeftRight();
        }
    }
}
