using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace mgdodge
{
    public class GameObject
    {
        private Texture2D texture;
        protected Dodge game;
        public Vector2 vector;
        public Boolean dead = false;

        public Rectangle BoundingBox
        {
            get
            {

                return new Rectangle(
                    (int) vector.X,
                    (int) vector.Y,
                    texture.Width,
                    texture.Height);
            }
        }


    public GameObject(Dodge game, string content, Vector2 starting_point)
        {
            this.game = game;
            texture = game.Content.Load<Texture2D>(content);
            vector = starting_point;
        }

        public virtual void Update()
        {

        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, vector, Color.White);
        }

        public virtual void Collision(GameObject col)
        {

        }
    }

    public class PlayerOne: GameObject
    {
        private int dir = -1;
        public PlayerOne(Dodge game) : base(game, "blue_square", new Vector2((game.GraphicsDevice.Viewport.Bounds.Width / 2) - 32, game.GraphicsDevice.Viewport.Bounds.Height - 64))
        {
            
        }

        public override void Update()
        {
            var mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                dir = -1;
            }
            if (mouseState.RightButton == ButtonState.Pressed)
            {
                dir = 1;
            }
            if (dir == -1 && vector.X <= 0)
            {
                dir = 0;
                vector.X = 0;
            }
            if (dir == 1 && vector.X >= (game.GraphicsDevice.Viewport.Bounds.Width - 64))
            {
                dir = 0;
                vector.X = game.GraphicsDevice.Viewport.Bounds.Width - 64;
            }
            vector.X = vector.X + (10 * dir);

            base.Update();
        }

        public override void Collision(GameObject col)
        {
            game.game_over();
            base.Collision(col);
        }
    }

    public class Ball : GameObject
    {
        public Ball(Dodge game, Vector2 starting_point) : base(game, "red_circle", starting_point)
        {

        }

        public override void Update()
        {
            if (vector.Y >= game.GraphicsDevice.Viewport.Bounds.Height)
            {
                vector.Y = -64;
                vector.X = game.player_one.vector.X;
            }
            vector.Y = vector.Y + 10;

            base.Update();
        }
    }
}
