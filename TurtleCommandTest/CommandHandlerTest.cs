using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TurtleCommand;
using TurtleCommand.Model;

namespace TurtleCommandTest
{
    [TestClass]
    public class CommandHandlerTest
    {
        private IHandler handler;

        [TestInitialize]
        public void InitialiseCommandHandler()
        {
            handler = new CommandHandler();
        }

        [TestMethod]
        public void TestPlaceTurtleWithInvalidArgument()
        {
            try
            {
                var currentPlace = handler.GetTurtle(); //default position of turtle
                Assert.AreEqual(currentPlace.X, -1);
                Assert.AreEqual(currentPlace.Y, -1);
                Assert.IsNull(currentPlace.CurrentFace);

                handler.Place(-5, 0, null); // out of tabletop range
                var afterPlace = handler.GetTurtle();

                Assert.AreEqual(afterPlace.X, -1);
                Assert.AreEqual(afterPlace.Y, -1);
                Assert.IsNull(afterPlace.CurrentFace);

                handler.Place(5, 0, FaceEnum.North); //out of table top range
                afterPlace = handler.GetTurtle();

                Assert.AreEqual(afterPlace.X, -1);
                Assert.AreEqual(afterPlace.Y, -1);
                Assert.IsNull(afterPlace.CurrentFace);

                handler.Place(4, -1, FaceEnum.North); //out of table top range
                afterPlace = handler.GetTurtle();

                Assert.AreEqual(afterPlace.X, -1);
                Assert.AreEqual(afterPlace.Y, -1);
                Assert.IsNull(afterPlace.CurrentFace);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }


        }

        [TestMethod]
        public void TestPlaceWithValidArgument()
        {
            try
            {

                var currentTurtle = handler.GetTurtle();
                handler.Place(0, 0, FaceEnum.North);

                var afterPlace = handler.GetTurtle();
                Assert.AreEqual(afterPlace.X, 0);
                Assert.AreEqual(afterPlace.Y, 0);
                Assert.AreEqual(afterPlace.CurrentFace, FaceEnum.North);

                // Move One Step on North Side
                handler.Move();
                Assert.AreEqual(afterPlace.X, 0);
                Assert.AreEqual(afterPlace.Y, 1);
                Assert.AreEqual(afterPlace.CurrentFace, FaceEnum.North);

                //Move three step on North Side
                handler.Move();
                handler.Move();
                handler.Move();
                Assert.AreEqual(afterPlace.X, 0);
                Assert.AreEqual(afterPlace.Y, 4);
                Assert.AreEqual(afterPlace.CurrentFace, FaceEnum.North);

                //Prevent Turtle to Move out of table
                //Although Move command executed, Turtle will not move further.
                handler.Move();
                Assert.AreEqual(afterPlace.X, 0);
                Assert.AreEqual(afterPlace.Y, 4);
                Assert.AreEqual(afterPlace.CurrentFace, FaceEnum.North);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        [TestMethod]
        public void TestMoveCommand()
        {
            try
            {
            
                
                //Try to Move Turtle Without Placing on Table
                handler.Move();

                //Confirm, it hasn't been move
                var afterPlace = handler.GetTurtle();
                Assert.AreEqual(afterPlace.X, -1);
                Assert.AreEqual(afterPlace.Y, -1);
                Assert.IsNull(afterPlace.CurrentFace);

                //prevent turtle to fall
                handler.Place(0, 0, FaceEnum.West);
                handler.Move();
                
                afterPlace = handler.GetTurtle();
                Assert.AreEqual(afterPlace.X, 0);
                Assert.AreEqual(afterPlace.Y, 0);
                Assert.AreEqual(afterPlace.CurrentFace,FaceEnum.West);

                //Place turtle and move it to west
                handler.Place(2,3,FaceEnum.West);
                handler.Move();
                afterPlace = handler.GetTurtle();
                Assert.AreEqual(afterPlace.X, 1);
                Assert.AreEqual(afterPlace.Y, 3);
                Assert.AreEqual(afterPlace.CurrentFace, FaceEnum.West);

            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.Message);
            }
        }

        [TestMethod]
        public void TestLeftCommand()
        {
            try
            {
                handler.Place(2,2,FaceEnum.North);
                var afterPlace = handler.GetTurtle();

                handler.RotateLeft();
                Assert.AreEqual(afterPlace.CurrentFace,FaceEnum.West);

                handler.RotateLeft();
                Assert.AreEqual(afterPlace.CurrentFace, FaceEnum.South);

                handler.RotateLeft();
                Assert.AreEqual(afterPlace.CurrentFace, FaceEnum.East);

                handler.RotateLeft();
                Assert.AreEqual(afterPlace.CurrentFace, FaceEnum.North);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [TestMethod]
        public void TestRightCommand()
        {
            handler.Place(2, 2, FaceEnum.North);
            var afterPlace = handler.GetTurtle();

            handler.RotateRight();
            Assert.AreEqual(afterPlace.CurrentFace, FaceEnum.East);

            handler.RotateRight();
            Assert.AreEqual(afterPlace.CurrentFace, FaceEnum.South);

            handler.RotateRight();
            Assert.AreEqual(afterPlace.CurrentFace, FaceEnum.West);

            handler.RotateRight();
            Assert.AreEqual(afterPlace.CurrentFace, FaceEnum.North);
        }

        [TestMethod]
        public void TestMultipleCommands()
        {
            try
            {
                handler.Place(1, 2, FaceEnum.East);
                handler.Move();
                handler.Move();
                handler.RotateLeft();
                handler.Move();

                var afterPlace = handler.GetTurtle();
                Assert.AreEqual(afterPlace.X, 3);
                Assert.AreEqual(afterPlace.Y, 3);
                Assert.AreEqual(afterPlace.CurrentFace, FaceEnum.North);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


    }
}
