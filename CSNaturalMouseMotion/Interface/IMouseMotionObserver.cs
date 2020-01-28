namespace NaturalMouseMotion.Interface
{
	/// <summary>
	/// Use to observe mouse movement in MouseMotion
	/// </summary>
	public interface IMouseMotionObserver
	{
		void observe(int xPos, int yPos);
	}
}
