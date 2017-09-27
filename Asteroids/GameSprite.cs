using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    abstract class GameSprite
    {
        protected Texture2D sprite;
        protected Rectangle drawRectangle;
        protected Vector2 velocity = new Vector2(0, 0);
        protected ContentManager contentManager;

        protected int windowWidth, windowHeight;

        protected GameSprite(ContentManager contentManager, string spriteName, int x, int y,
            int windowWidth, int windowHeight)
        {
            this.contentManager = contentManager;
            this.windowHeight = windowHeight;
            this.windowWidth = windowWidth;

            LoadContent(contentManager, spriteName, x, y);
        }

        public abstract void Update();

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, drawRectangle, Color.White);
        }

        private void LoadContent(ContentManager contentManager, string SpriteName, int x, int y)
        {
            sprite = contentManager.Load<Texture2D>(SpriteName);
            drawRectangle = new Rectangle(x - sprite.Width / 2, y - sprite.Height / 2,
                sprite.Width, sprite.Height);
        }

        protected Vector2 getVelocity()
        {
            return this.velocity;
        }

        protected void BounceTopBottom()
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

        protected void BounceLeftRight()
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

        public bool IsAbsorbed()
        {
            return (drawRectangle.X < 0 || drawRectangle.Y < 0 || drawRectangle.X > GameConstants.WindowWidth || drawRectangle.Y > GameConstants.WindowHeight);
        }
    }
}
