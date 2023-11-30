using LJA.Game.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace LJA.Game;

public class ParallaxLayer : Entity, IUpdatableEntity, IDrawableEntity
{
    public float ScrollSpeed { get; set; }
    public float RepeatX { get; set; }

    public ParallaxLayer(Texture2D sprite, float scrollSpeed, int repeatX) : base(sprite)
    {
        ScrollSpeed = scrollSpeed;
        RepeatX = repeatX;
    }

    public void Update(GameTime gameTime)
    {
        if (ControlRegistry.Jump)
        {
            PosY -= ScrollSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        if (ControlRegistry.Down)
        {
            PosY += ScrollSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        if (ControlRegistry.Left)
        {
            PosX -= ScrollSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        if (ControlRegistry.Right)
        {
            PosX += ScrollSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {

        for (int x = 0; x < RepeatX; x++)
        {
            Vector2 position = new(GetGameAccuratePosition().X + x * Sprite.Width * Scale.X, GetGameAccuratePosition().Y);
            spriteBatch.Draw(Sprite, position, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
        }
    }
}



