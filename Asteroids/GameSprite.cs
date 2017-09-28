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

        public bool Collision(GameSprite gs)
        {
            if (this.drawRectangle.Intersects(gs.GetRect()))
            {
                return true;
            }
            return false;
        }

        public Rectangle GetRect()
        {
            return this.drawRectangle;
        }     
        
        public Vector2 GetVelocity()
        {
            return this.velocity;
        } 

        public int GetXVel()
        {
            return (int)velocity.X;
        }
        
        public int GetYVel()
        {
            return (int)velocity.Y;
        }

        public void SetVelocity(int xVel, int yVel)
        {
            velocity.X = xVel;
            velocity.Y = yVel;
        }

        public void SetVelocity(Vector2 velocity)
        {
            this.velocity = velocity;
        }


    }
}
