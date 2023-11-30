using Microsoft.Xna.Framework.Input;

namespace LJA.Game;

public class ControlRegistry
{
    private static KeyboardState _kState;

    public static void Start()
    {
        _kState = Keyboard.GetState();
    }

    public static bool Left =>
        _kState.IsKeyDown(Keys.A);
    

    public static bool Jump =>
        _kState.IsKeyDown(Keys.Space) || _kState.IsKeyDown(Keys.W);
    

    public static bool Right =>
        _kState.IsKeyDown(Keys.D);
    

    public static bool Down =>
        _kState.IsKeyDown(Keys.S);

    private ControlRegistry() {}
}



