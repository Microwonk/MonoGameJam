using Apos.Camera;
using Microsoft.Xna.Framework;

namespace LJA.Game;

public class PlayerCamera : Camera
{
    private const float LerpFactor = 0.07f;

    public PlayerCamera(IVirtualViewport vp) : base(vp) { }

    public void Update(Player player, GameTime gameTime)
    {
        Vector2 targetPosition = player.Position;

        targetPosition.Y -= 70;

        // Use linear interpolation (lerp) for smooth camera movement
        XY = Vector2.Lerp(XY, targetPosition, LerpFactor);

        // Optionally, you can add damping for even smoother transitions
        XY = Vector2.Lerp(XY, targetPosition, LerpFactor * (float)gameTime.ElapsedGameTime.TotalSeconds);
    }

    public void ClampToBounds(Rectangle bounds) =>
        XY = new Vector2(
            MathHelper.Clamp(XY.X, bounds.Left, bounds.Right - VirtualViewport.Width), 
            MathHelper.Clamp(XY.Y, bounds.Top, bounds.Bottom - VirtualViewport.Height)
            );
}
