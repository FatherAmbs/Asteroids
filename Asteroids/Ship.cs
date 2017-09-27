using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Ship : GameSprite
    {
        private KeyboardState oldKeyState;
        private KeyboardState newKeyState;

        private Vector2 direction;
        private Vector2 position;

        private double angle = -Math.PI / 2;
        private bool hasFired;
        private int speed = 5;

        public Ship(ContentManager contentManager, string spriteName, int x, int y,
            int windowWidth, int windowHeight)
            : base(contentManager, spriteName, x, y, windowWidth, windowHeight)
        {
            position = new Vector2(x - sprite.Width / 2, y - sprite.Height / 2);
            direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            oldKeyState = Keyboard.GetState();
            newKeyState = Keyboard.GetState();
        }

        public override void Update()
        {
            newKeyState = Keyboard.GetState();
            hasFired = false;
            UpdateInput();
            UpdateMovement();
         
            drawRectangle.X += (int)velocity.X;
            drawRectangle.Y += (int)velocity.Y;

            if (drawRectangle.X < 0)
            {
                drawRectangle.X += windowWidth - sprite.Width / 2;
            }

            if (drawRectangle.Y < 0)
            {
                drawRectangle.Y += windowHeight - sprite.Height / 2;
            }

            drawRectangle.X = drawRectangle.X % (windowWidth - sprite.Width / 2);
            drawRectangle.Y = drawRectangle.Y % (windowHeight - sprite.Height / 2);
            position.X = sprite.Width / 2;
            position.Y = sprite.Height / 2;
            oldKeyState = newKeyState;
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, drawRectangle, null, Color.White, (float)(angle + Math.PI/2), position, SpriteEffects.None, 0f);
        }


        private void UpdateInput()
        {
            if (newKeyState.IsKeyDown(Keys.Space))
            {
                if (oldKeyState.IsKeyUp(Keys.Space))
                {
                    hasFired = true;
                }
            }
        }

        private void UpdateMovement()
        {
            //Vertical movement
            if (newKeyState.IsKeyDown(Keys.W))
            {
                velocity = speed * direction;
                
            } else if (newKeyState.IsKeyDown(Keys.S))
            {
                velocity = -speed * direction;
            }

            //Horizontal movement
            if (newKeyState.IsKeyDown(Keys.A))
            {
                
                angle -= Math.PI / 60;
                while (angle < -Math.PI) angle += 2 * Math.PI;
                direction.X = (float)(Math.Cos(angle));
                direction.Y = (float)(Math.Sin(angle));
               
            }

            else if (newKeyState.IsKeyDown(Keys.D))
            {
                angle += Math.PI / 60;
                while (angle > Math.PI) angle -= 2 * Math.PI;
                direction.X = (float)(Math.Cos(angle));
                direction.Y = (float)(Math.Sin(angle));
            }

            //Clearing old movement
            if (newKeyState.IsKeyUp(Keys.W))
            {
                if (oldKeyState.IsKeyDown(Keys.W))
                {
                    velocity = Vector2.Zero;
                }
            }

            if (newKeyState.IsKeyUp(Keys.S))
            {
                if (oldKeyState.IsKeyDown(Keys.S))
                {
                    velocity = Vector2.Zero;
                }
            }
        }

        public bool HasFired()
        {
            return hasFired;
        }


        //Getters & Setters
        public int GetX()
        {
            return drawRectangle.X;
        }

        public int GetY()
        {
            return drawRectangle.Y;
        }

        public double GetAngle()
        {
            return angle;
        }

        public Vector2 GetDirection()
        {
            return direction;
        }
    }
}