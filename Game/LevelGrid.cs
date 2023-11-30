using System;
using System.Runtime.InteropServices.ComTypes;
using LJA.Game.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LJA.Game
{
    public class LevelGrid : Entity, IDrawableEntity
    {
        public Rectangle LevelBounds { get; private set; }
        public Entity[][] Grid { get; private set; }

        public LevelGrid() : base(null) { }

        public void CreateGrid(int rows, int columns, GraphicsDevice graphicsDevice)
        {
            var grid = new Entity[rows][];
            var tex = new Texture2D(graphicsDevice, 2, 2);

            for (int row = 0; row < rows; row++)
            {
                grid[row] = new Entity[columns];

                for (int col = 0; col < columns; col++)
                {
                    grid[row][col] = new Entity(tex) { Position = new Vector2(row, col)};
                }
            }

            Grid = grid;
            LevelBounds = new Rectangle(0, 0, rows, columns);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Grid.Length; i++)
            {
                for (int j = 0; j < Grid[i].Length; j++)
                {
                    Entity currentTile = Grid[i][j];
                    Vector2 position = new(j * currentTile.Sprite.Width * Scale.X, i * currentTile.Sprite.Height * Scale.Y);

                    spriteBatch.Draw(currentTile.Sprite, position, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
                }
            }
        }

    }
}