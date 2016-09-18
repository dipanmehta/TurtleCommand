using TurtleCommand.Model;

namespace TurtleCommand
{
    public interface IHandler
    {
        void Place(int x, int y, FaceEnum? face);
        void Move();
        void RotateLeft();
        void RotateRight();
        void Report();
        Turtle GetTurtle();
    }
}
