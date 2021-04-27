
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MVPTicTacToe;
using TicTacToeGuiMVP;

namespace TicTacTests
{
    [TestClass]
    public class UnitTest1
    {
        public class TestUIInterface : UIInterface
        {
            public bool resetBoardCalled = false;
            public string updateStatusString = "";
            private bool showErrorCalled = false;
            private Boolean throwOnNextPlayer = false;
            private int v1;
            private bool v2;

            public TestUIInterface()
            {

            }

            public string PlayStatus { get; set; }

            void UIInterface.DrawTicTacToeSymbol(int player, int row, int col)
            {
            }

            void UIInterface.ResetBoard()
            {
                resetBoardCalled = true;
            }

            void UIInterface.ShowError(string v)
            {
            }

            void UIInterface.ShowNextPlayer(int nextPlayer)
            {
            }

            void UIInterface.UpdateGameFinished(string v)
            {
            }

            void UIInterface.UpdateStatus(string v)
            {
                updateStatusString = v;
            }
        }

        [TestMethod]
        public void Test_StartGame()
        {
            //Arrange
            TestUIInterface theTestedUI = new TestUIInterface();
            Presenter thePresenter = new Presenter(theTestedUI);

            //Act
            thePresenter.StartGame();

            //Assert
            Assert.IsTrue(theTestedUI.resetBoardCalled);
            Assert.AreEqual("status: ready", theTestedUI.updateStatusString);
        }

        [TestMethod]
        public void Test_OnCellClicked()
        {

        }

        [TestMethod]
        public void Test_OnCellClicked_OutOfRangeCell_ShowErrorThrows()
        {
            // Arrange

            // Act

            // Assert

        }

        [TestMethod]
        public void Test_OnCellClicked_OutOfRangeCell_ShowErrorDoesNotThrow()
        {

        }
    }
}
