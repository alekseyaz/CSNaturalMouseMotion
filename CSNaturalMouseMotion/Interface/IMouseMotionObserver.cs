namespace NaturalMouseMotion.Interface
{
    /// <summary>
    /// Use to observe mouse movement in MouseMotion
    /// </summary>
    public interface IMouseMotionObserver
    {
        void Observe(int xPos, int yPos);
    }
}
