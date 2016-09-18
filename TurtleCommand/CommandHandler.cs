using System;
using TurtleCommand.Model;

namespace TurtleCommand
{
    public class CommandHandler : IHandler
    {
        private readonly Turtle _turtle;
        private const int MaxX = 5;
        private const int MinX = 0;
        private const int MaxY = 5;
        private const int MinY = 0;

        public CommandHandler()
        {
            _turtle = new Turtle
            {
                X = -1,
                Y = -1,
                CurrentFace = null
            };
        }

        
        public void Place(int x, int y, FaceEnum? face)
        {
            if (x < MinX || x >= MaxX || y < MinY || y >= MaxY || face == null) return;
            _turtle.X = x;
            _turtle.Y = y;
            _turtle.CurrentFace = face;
        }

        public void Move()
        {
            if (!IsTurtlePlaced()) return;
            switch (_turtle.CurrentFace)
            {
                case FaceEnum.North:
                    {
                        //Increase Y with 1
                        if ((_turtle.Y + 1) < MaxY)
                            _turtle.Y += 1;
                        break;
                    }
                case FaceEnum.South:
                    {
                        //Decrease Y with 1
                        if ((_turtle.Y - 1) >= MinY)
                            _turtle.Y -= 1;
                        break;
                    }
                case FaceEnum.East:
                    {
                        //Increase x with 1
                        if ((_turtle.X + 1) < MaxX)
                            _turtle.X += 1;
                        break;

                    }

                case FaceEnum.West:
                    {
                        //Decrease x with 1
                        if ((_turtle.X - 1) >= MinX)
                            _turtle.X -= 1;
                        break;
                    }
                case null:
                    break;
            }
        }

        public void RotateLeft()
        {
            switch (_turtle.CurrentFace)
            {
                case FaceEnum.West:
                    _turtle.CurrentFace = FaceEnum.South;
                    break;
                case FaceEnum.North:
                    _turtle.CurrentFace = FaceEnum.West;
                    break;
                case FaceEnum.East:
                    _turtle.CurrentFace = FaceEnum.North;
                    break;
                case FaceEnum.South:
                    _turtle.CurrentFace = FaceEnum.East;
                    break;
                case null:
                    break;
            }
        }

        public void RotateRight()
        {
            if (!IsTurtlePlaced()) return;
            switch (_turtle.CurrentFace)
            {
                case FaceEnum.West:
                    _turtle.CurrentFace = FaceEnum.North;
                    break;
                case FaceEnum.North:
                    _turtle.CurrentFace = FaceEnum.East;
                    break;
                case FaceEnum.East:
                    _turtle.CurrentFace = FaceEnum.South;
                    break;
                case FaceEnum.South:
                    _turtle.CurrentFace = FaceEnum.West;
                    break;
                case null:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Report()
        {
            if (!IsTurtlePlaced()) return;
            var face = _turtle.CurrentFace;
            Console.WriteLine(string.Format("{0},{1},{2}", _turtle.X, _turtle.Y, face));
        }

        public Turtle GetTurtle()
        {
            return _turtle;
        }

        private bool IsTurtlePlaced()
        {
            return (_turtle.X != -1 || _turtle.Y != -1 || _turtle.CurrentFace != null);

        }
    }
}
