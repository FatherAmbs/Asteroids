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

        Random rand;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = GameConstants.WindowHeight;
            graphics.PreferredBackBufferWidth = GameConstants.WindowWidth;

            rand = new Random();
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

            rockTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            System.Diagnostics.Debug.WriteLine("Game Time: " + gameTime.ElapsedGameTime.TotalMilliseconds + ", Rock Timer: " + rockTimer);

            if (rockTimer > 1000)
            {
                rockTimer = 0;
                if (rockList.Count < 50)
                {
                    SpawnRock();
                    
                }
            }
            // TODO: Add your update logic here
            UpdateRocks();
            ship.Update();
            UpdateLasers(ship);

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

        private void UpdateLasers(Ship ship)
        {
            if (ship.HasFired())
            {
                if (laserList.Count < 5)
                {
                    laserList.Add(new Laser(Content, @"graphics\laser", ship.GetX(), ship.GetY(),
                        GameConstants.WindowWidth, GameConstants.WindowHeight, ship.GetAngle()));
                }
               
            }
            for (int i = laserList.Count - 1; i >= 0; i--)
            {
                laserList[i].Update();
                if (laserList[i].IsAbsorbed())
                {
                    laserList.RemoveAt(i);
                }
            }
        }

      
        private void UpdateRocks()
        {
            for (int i = rockList.Count - 1; i >= 0; i--)
            {
                rockList[i].Update();
            }
        }
        private Vector2 RockStart()
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

        private void SpawnRock()
        {
            Vector2 rockPosition = RockStart();
            rockList.Add(new Rock(Content, @"graphics\rock",
                            (int)rockPosition.X, (int)rockPosition.Y,
                            rand.Next(5) + 2, 2 * Math.PI * rand.NextDouble(),
                            GameConstants.WindowWidth, GameConstants.WindowHeight));
        }
    }    
}
