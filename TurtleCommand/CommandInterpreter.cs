using System;
using System.Collections.Generic;
using TurtleCommand.Model;

namespace TurtleCommand
{
    public class CommandInterpreter : ICommandInterpreter
    {
        private readonly IHandler _handler;

        public CommandInterpreter(IHandler inputHandler)
        {
            _handler = inputHandler;
        }

        public void Execute(string cmd)
        {
            var args = cmd.Split(' ');
            if (args.Length == 0)
                return;

            var command = args[0].ToUpper();
            switch (command)
            {
                case "PLACE":
                    {
                        if (args.Length != 2)
                            return;
                        if (string.IsNullOrEmpty(args[1]))
                            return;
                        var param = args[1].Split(new char[] { ',' });
                        if (param.Length != 3) return;
                        var x = int.Parse(param[0]);
                        var y = int.Parse(param[1]);
                        var strFace = param[2].ToUpper();
                        var currentFace = GetCurrentFace(strFace);
                        _handler.Place(x, y, currentFace);

                        break;

                    }
                case "MOVE":
                    {
                        _handler.Move();
                        break;

                    }
                case "LEFT":
                    {
                        _handler.RotateLeft();
                        break;

                    }
                case "RIGHT":
                    {
                        _handler.RotateRight();
                        break;
                    }
                case "REPORT":
                    {
                        _handler.Report();
                        break;
                    }
                default:
                    break;

            }
        }
        
        private FaceEnum? GetCurrentFace(string strFace)
        {
            var direction = new List<string>()
            {
                "NORTH",
                "SOUTH",
                "EAST",
                "WEST"

            };
            if (!direction.Contains(strFace)) return null;
            FaceEnum? currentFace = null;
            switch (strFace)
            {
                case "WEST":
                    {
                        currentFace = FaceEnum.West;
                        break;
                    }
                case "SOUTH":
                    {
                        currentFace = FaceEnum.South;
                        break;

                    }
                case "EAST":
                    {
                        currentFace = FaceEnum.East;
                        break;
                    }
                case "NORTH":
                    {
                        currentFace = FaceEnum.North;
                        break;
                    }
            }
            return currentFace;
        }
    }
}
