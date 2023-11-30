
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LJA.Game;

public class Entity
{
    public Texture2D Sprite { get; set; }
    private Vector2 _position;

    public float PosY
    {
        get => _position.Y;
        set => _position.Y = value;
    }

    public float PosX
    {
        get => _position.X;
        set => _position.X = value;
    }

    public Vector2 Position
    {
        get => _position;
        set => _position = value;
    }

    public Vector2 Scale { get; set; }

    public Entity(Texture2D sprite)
    {
        Sprite = sprite;
        Scale = new Vector2(1f, 1f);
        Position = new Vector2(0f, 0f);
    }

    public Vector2 GetGameAccuratePosition() => new(Position.X - Sprite.Width / 2, Position.Y - Sprite.Height / 2);
}


