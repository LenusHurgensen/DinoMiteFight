using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace DMF
{
    class Player
    {
        private Texture2D rightWalk, leftWallk, currentAnimation;
        private Vector2 position = new Vector2(65, 300);
        private Vector2 velocity;
        private Rectangle rectangle;
        private Rectangle sourceRectangle;
        private float elapsed;
        private float delay = 200f;
        private int frames = 0;

        private bool hasJumped = false;

        public Vector2 Position
        {
            get { return position; }
        }

        public Player() { }

        public void Load(ContentManager Content)
        {
            leftWallk = Content.Load<Texture2D>("LeftTextureP1");
            rightWalk = Content.Load<Texture2D>("RightTextureP1");

            currentAnimation = rightWalk;
        }

        private void Animate(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsed >= delay)
            {
                if (frames > 2)
                    frames = 0;
                else
                    frames++;
                elapsed = 0;
            }

            if (hasJumped == true)
                frames = 4;

            sourceRectangle = new Rectangle(32 * frames, 0, 32, 32);
        }

        public void Update(GameTime gameTime)
        {
            position += velocity;
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)32, (int)32);

            Input(gameTime);

            if (velocity.Y < 10)
                velocity.Y += 0.4f;
        }

        private void Input(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                velocity.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
                currentAnimation = rightWalk;
                Animate(gameTime);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                velocity.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
                currentAnimation = leftWallk;
                Animate(gameTime);
            }
            else
            {
                velocity.X = 0f;
                sourceRectangle = new Rectangle(0, 0, 32, 32);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hasJumped == false)
            {
                position.Y -= 5f;
                velocity.Y = -10f;
                hasJumped = true;
                Animate(gameTime);
            }
        }

        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (rectangle.TouchTopOf(newRectangle))
            {
                rectangle.Y = newRectangle.Y - rectangle.Height;
                velocity.Y = 0f;
                hasJumped = false;
            }

            if (rectangle.TouchLeftOf(newRectangle))
            {
                position.X = newRectangle.X - rectangle.Width - 2;
            }
            if (rectangle.TouchRightOf(newRectangle))
            {
                position.X = newRectangle.X + newRectangle.Width + 2;
            }
            if (rectangle.TouchBottomOf(newRectangle))
                velocity.Y = 1f;

            if (position.X < 0) position.X = 0;
            if (position.X > xOffset - rectangle.Width) position.X = xOffset - rectangle.Width;
            if (position.Y < 0) velocity.Y = 1f;
            if (position.Y > yOffset - rectangle.Height) position.Y = yOffset - rectangle.Height;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(currentAnimation, rectangle, sourceRectangle, Color.White);
        }
    }
}
