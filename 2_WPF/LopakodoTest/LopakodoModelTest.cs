using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lopakodo.Model;
using Lopakodo.Persistence;
using Moq;

namespace Lopakodo.Test
{
    [TestClass]
    public class LopakodoModelTest
    {
        private LopakodoModel _model;
        private LopakodoGameField _mockField;
        private Mock<ILopakodoData> _mock;

        [TestInitialize]
        public void Initialize()
        {
            _mockField = new LopakodoGameField(4);
            _mockField.SetValue(0, 0, 2); _mockField.SetValue(0, 1, 1); _mockField.SetValue(0, 2, 4); _mockField.SetValue(0, 3, 1);
            _mockField.SetValue(1, 0, 0); _mockField.SetValue(1, 1, 1); _mockField.SetValue(1, 2, 0); _mockField.SetValue(1, 3, 1);
            _mockField.SetValue(2, 0, 0); _mockField.SetValue(2, 1, 0); _mockField.SetValue(2, 2, 0); _mockField.SetValue(2, 3, 1);
            _mockField.SetValue(3, 0, 0); _mockField.SetValue(3, 1, 1); _mockField.SetValue(3, 2, 3); _mockField.SetValue(3, 3, 1);

            _mock = new Mock<ILopakodoData>();
            _mock.Setup(mock => mock.LoadAsync(It.IsAny<String>())).Returns(() => Task.FromResult(_mockField));

            //_model.MovePlayer(Direction.x);

            _model = new LopakodoModel(_mock.Object);
            _model.NewGame();

            _model.GameOver += new EventHandler<LopakodoEventArgs>(Test_GameOver);
        }


        [TestMethod]
        public void LopakodoModelMoveUpTest()
        {
            _model.MovePlayer(Direction.Up);
            _model.MovePlayer(Direction.Up);
            
            Assert.AreEqual(_model.GameField[1,2], 3);
        }

        // Did not move
        [TestMethod]
        public void LopakodoModelMoveIntoWallTest()
        {
            _model.MovePlayer(Direction.Left);
            _model.MovePlayer(Direction.Right);
            _model.MovePlayer(Direction.Down);

            Assert.AreEqual(_model.GameField[3, 2], 3);
        }

        // Reaching exit started new game
        [TestMethod]
        public void LopakodoModelExitTest()
        {
            _model.MovePlayer(Direction.Up);
            _model.MovePlayer(Direction.Up);
            _model.MovePlayer(Direction.Up);

            Assert.AreEqual(_model.GameField[3, 2], 3);
        }

        // Player was spotted
        [TestMethod]
        public void LopakodoModelSpottedTest()
        {
            _model.MovePlayer(Direction.Up);
            _model.MovePlayer(Direction.Left);
            _model.MovePlayer(Direction.Left);

            Assert.AreEqual(_model.GameField[3, 2], 3);
        }

        private void Test_GameOver(Object sender, LopakodoEventArgs e)
        {
            _mockField = new LopakodoGameField(4);
            _mockField.SetValue(0, 0, 2); _mockField.SetValue(0, 1, 1); _mockField.SetValue(0, 2, 4); _mockField.SetValue(0, 3, 1);
            _mockField.SetValue(1, 0, 0); _mockField.SetValue(1, 1, 1); _mockField.SetValue(1, 2, 0); _mockField.SetValue(1, 3, 1);
            _mockField.SetValue(2, 0, 0); _mockField.SetValue(2, 1, 0); _mockField.SetValue(2, 2, 0); _mockField.SetValue(2, 3, 1);
            _mockField.SetValue(3, 0, 0); _mockField.SetValue(3, 1, 1); _mockField.SetValue(3, 2, 3); _mockField.SetValue(3, 3, 1);

            _mock = new Mock<ILopakodoData>();
            _mock.Setup(mock => mock.LoadAsync(It.IsAny<String>())).Returns(() => Task.FromResult(_mockField));

            _model.NewGame();
        }
    }
}
