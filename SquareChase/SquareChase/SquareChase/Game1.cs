using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SquareChase
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random rand = new Random();
        Texture2D squareTexture;
        Rectangle currentSquare;
        int playerScore = 0;
        float timeRemaining = 0.0f;
        float TimePerSquare = 1.75f;
        float timeTaken = 0.0f;
        int squareSize = 25;
        Color[] colors = new Color[3] { Color.Red, Color.Green, Color.Blue };

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            this.IsMouseVisible = true;
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
            squareTexture = Content.Load<Texture2D>(@"SQUARE");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            if (timeRemaining == 0.0f)
            {
                if (timeTaken > 1.0f && squareSize < 25 && TimePerSquare < 1.75f)
                {
                    squareSize += 2;
                    timeTaken = 0;
                    TimePerSquare += 0.1f;
                }
                currentSquare = new Rectangle(
                    rand.Next(0, this.Window.ClientBounds.Width - 25),
                    rand.Next(0, this.Window.ClientBounds.Height - 25),
                    squareSize, squareSize);
                timeRemaining = TimePerSquare;
            }

            MouseState mouse = Mouse.GetState();

            if ((mouse.LeftButton == ButtonState.Pressed) &&
                (currentSquare.Contains(mouse.X, mouse.Y)))
            {
                playerScore++;
                if (squareSize > 10)
                {
                    squareSize -= 2;
                }
                TimePerSquare -= 0.1f;
                timeRemaining = 0.0f;
            }
            timeRemaining = MathHelper.Max(0, timeRemaining - (float)
                gameTime.ElapsedGameTime.TotalSeconds);
            timeTaken = MathHelper.Max(0, timeTaken + (float)
                gameTime.ElapsedGameTime.TotalSeconds);

            this.Window.Title = "Score : " + playerScore.ToString();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(
                squareTexture,
                currentSquare,
                colors[playerScore % 3]);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
