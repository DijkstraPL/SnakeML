namespace SnakeML.GameEngine
{
    internal abstract class BoardObject
    {
        internal abstract bool IsAt(int x, int y);

        internal abstract BoardObject Clone();
    }
}
