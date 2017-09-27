using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids
{
    class Laser : GameSprite
    {
        private static int laserSpeed = 10;
        private double angle;

        public Laser(ContentManager contentManager, string spriteName, int x, int y,
            int windowWidth, int windowHeight, double angle) 
            : base(contentManager, spriteName, x, y, windowWidth, windowHeight)
        {
            this.velocity.X = (int)(laserSpeed * Math.Cos(angle));
            this.velocity.Y = (int)(laserSpeed * Math.Sin(angle));
            this.angle = angle;
        }

        public override void Update()
        {
            drawRectangle.X += (int)velocity.X;
            drawRectangle.Y += (int)velocity.Y;
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, drawRectangle, null, Color.White, (float)(angle + Math.PI / 2),
                new Vector2(sprite.Width / 2, sprite.Height / 2), SpriteEffects.None, 0f);
        }
    }
}
