using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Data;

namespace DirtyTricks
{
    public enum Direction
    {
        Up, Down, Left, Right
    };

    public enum Axis
    {
        XAxis, YAxis
    };

    class Player
    {
        #region Properties

        //Physics
        public Vector2 position, previousPosition;      // Exact position (float)
        Vector2 speed;                                  // Speed of player
        float maxSpeed;                                 // Max Speed of player
        float maxAcceleration;                          // Max Acceleration
        Vector2 acceleration;                           // Acceleration

        // Gfx
        Texture2D playerTexture;                        // PNG of player
        Texture2D playerHitboxTexture;                  // Hitbox of player
        Hitbox playerHitbox;                            // Hitbox rectangle
        Point playerSize;                               // Size of the sprite
        Rectangle drawingRectangle;                     // Player rectangle for drawing
        Direction direction = new Direction();          // Player direction

        // Animation
        bool animateOnce = false;                       // Trigger for animation speed
        int frameLine, frameRow;                        // Spritesheet
        int animationTimer;                             // Animation timer
        int animationSpeed;                             // Animation speed

        #endregion


        public Player()
        {
            playerTexture = Ressources.player;
            playerHitboxTexture = Ressources.playerHitbox;
            playerHitbox = Collision.CollisionMapSprite(playerHitboxTexture);

            position.X = Settings.Current.StartPositionX;
            position.Y = Settings.Current.StartPositionY;
            playerSize.X = Settings.Current.SpriteWidth;
            playerSize.Y = Settings.Current.SpriteHeight;
            maxSpeed = Settings.Current.MaxSpeed;
            speed.X = 0;
            speed.Y = 0;
            maxAcceleration = Settings.Current.Acceleration;
            acceleration.X = 0;
            acceleration.Y = 0;
            drawingRectangle = new Rectangle((int)position.X, (int)position.Y, playerSize.X, playerSize.Y);
            
            frameLine = 0;
            frameRow = 0;
            direction = Direction.Down;
            animationTimer = 0;
            animationSpeed = Settings.Current.AnimationSpeed;
        }

        //Methods
        public void AnimateFrame()
        {
            if (animationTimer == animationSpeed)
            {
                animationTimer = 0;
                frameLine++;
                if (frameLine > 1)
                    frameLine = 0;
            }
        }

        private void Move()
        {
            speed += acceleration;

            if (speed.Y > maxSpeed) speed.Y = maxSpeed;
            if (speed.Y < -maxSpeed) speed.Y = -maxSpeed;
            if (speed.X > maxSpeed) speed.X = maxSpeed;
            if (speed.X < -maxSpeed) speed.X = -maxSpeed;

            previousPosition = position;

            position.Y += speed.Y;
            position.Y = Collision.MoveSpriteOnMap(Axis.YAxis, previousPosition, position, playerSize, playerHitbox).Y;
            if (previousPosition.Y == position.Y)
            {
                speed.Y = 0;
                acceleration.Y = 0;
            }

            position.X += speed.X;
            position.X = Collision.MoveSpriteOnMap(Axis.XAxis, previousPosition, position, playerSize, playerHitbox).X;
            if (previousPosition.X == position.X)
            {
                acceleration.X = 0;
                speed.X = 0;
            }
            
            AnimateFrame();
            animateOnce = true;
        }


        //Update & Draw
        public void Update(MouseState mouse, KeyboardState keyboard, GamePadState gamePadState)
        {
            if ((keyboard.IsKeyDown(Keys.Up) && keyboard.IsKeyUp(Keys.Down)) || gamePadState.IsButtonDown(Buttons.DPadUp))
            {
                direction = Direction.Up;
                acceleration.Y = -maxAcceleration;
            }

            if ((keyboard.IsKeyDown(Keys.Down) && keyboard.IsKeyUp(Keys.Up)) || gamePadState.IsButtonDown(Buttons.DPadDown))
            {
                direction = Direction.Down;
                acceleration.Y = maxAcceleration;
            }

            if ((keyboard.IsKeyDown(Keys.Left) && keyboard.IsKeyUp(Keys.Right)) || gamePadState.IsButtonDown(Buttons.DPadLeft))
            {
                direction = Direction.Left;
                acceleration.X = -maxAcceleration;
            }

            if ((keyboard.IsKeyDown(Keys.Right) && keyboard.IsKeyUp(Keys.Left)) || gamePadState.IsButtonDown(Buttons.DPadRight))
            {
                direction = Direction.Right;
                acceleration.X = maxAcceleration;
            }

            if (keyboard.IsKeyUp(Keys.Up) && keyboard.IsKeyUp(Keys.Down) && gamePadState.IsButtonUp(Buttons.DPadUp) && gamePadState.IsButtonUp(Buttons.DPadDown))
            {
                if (speed.Y < -maxAcceleration)
                    acceleration.Y = maxAcceleration;
                else if (speed.Y > maxAcceleration)
                    acceleration.Y = -maxAcceleration;
                else
                {
                    acceleration.Y = 0;
                    speed.Y = 0;
                }
            }

            if (keyboard.IsKeyUp(Keys.Left) && keyboard.IsKeyUp(Keys.Right) && gamePadState.IsButtonUp(Buttons.DPadLeft) && gamePadState.IsButtonUp(Buttons.DPadRight))
            {
                if (speed.X < -maxAcceleration)
                    acceleration.X = maxAcceleration;
                else if (speed.X > maxAcceleration)
                    acceleration.X = -maxAcceleration;
                else
                {
                    acceleration.X = 0;
                    speed.X = 0;
                }
            }

            Move();

            if (animateOnce)
            {
                animationTimer++;
                animateOnce = false;
            }

            if (keyboard.IsKeyUp(Keys.Up) && keyboard.IsKeyUp(Keys.Down) && keyboard.IsKeyUp(Keys.Left) && keyboard.IsKeyUp(Keys.Right)
                && gamePadState.IsButtonUp(Buttons.DPadUp) && gamePadState.IsButtonUp(Buttons.DPadDown) && gamePadState.IsButtonUp(Buttons.DPadLeft) && gamePadState.IsButtonUp(Buttons.DPadRight))
            {
                animationTimer = 0;
                frameLine = 0;
            }

            drawingRectangle.X = (int)position.X;
            drawingRectangle.Y = (int)position.Y;

            switch (direction)
            {
                case Direction.Up: frameRow = 2; break;
                case Direction.Down: frameRow = 0; break;
                case Direction.Left: frameRow = 1; break;
                case Direction.Right: frameRow = 3; break;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerTexture, drawingRectangle,
                new Rectangle(frameRow * playerSize.X, frameLine * playerSize.Y, playerSize.X, playerSize.Y),
                Color.White);
        }

    }
}
