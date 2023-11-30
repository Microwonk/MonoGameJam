using LJA.Game.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LJA.Game;

public class Player : Entity, IUpdatableEntity
{
    public float Speed { get; set; }

    public Player(Texture2D sprite) : base(sprite) { }

    public void Update(GameTime gameTime)
    {
        Speed = Globals.CurrPlayerSpeed;
        if (ControlRegistry.Jump)
        {
            PosY -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        if (ControlRegistry.Down)
        {
            PosY += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        if (ControlRegistry.Left)
        {
            PosX -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        if (ControlRegistry.Right)
        {
            PosX += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}