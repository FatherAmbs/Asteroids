using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Asteroids
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Ship ship;
        List<Rock> rockList;
        List<Laser> laserList;

        private double rockTimer = 0;

        private static int score;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = GameConstants.WindowHeight;
            graphics.PreferredBackBufferWidth = GameConstants.WindowWidth;

            score = 0;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            laserList = new List<Laser>();
            rockList = new List<Rock>();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ship = new Ship(Content, @"graphics\ship", GameConstants.WindowWidth / 2, GameConstants.WindowHeight / 2, GameConstants.WindowWidth, GameConstants.WindowHeight);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            rockTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            Console.WriteLine("Score: " + score);
            if (rockTimer > 1000)
            {
                rockTimer = 0;
                if (rockList.Count < GameConstants.RockCount)
                {
                    rockList.Add(Logic.SpawnRock(Content));
                }
            }

            for (int i = rockList.Count - 1; i >= 0; i--)
            {
                rockList[i].Update();
            }
            ship.Update();
            Logic.UpdateLasers(Content, ship, laserList);
            if (Logic.CollisionCheck(laserList, rockList, ship))
            {
                Console.WriteLine("You lose!");
                Exit();
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            ship.Draw(spriteBatch);
            foreach (Rock r in rockList)
            {
                r.Draw(spriteBatch);
            }
            foreach (Laser l in laserList)
            {
                l.Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public static void IncreaseScore()
        {
            score++;
        }
    }
}
