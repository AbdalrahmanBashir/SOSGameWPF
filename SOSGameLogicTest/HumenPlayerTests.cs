using SOSGameLogic.Implementation;
using SOSGameLogic.Interfaces;


namespace SOSGameLogicTest
{
    public class HumenPlayerTests
    {
        [Fact]
        public void GetPlayerSymbolReturnsInitializedSymbol()
        {
            // Arrange
            char expectedSymbol = 'S';
            IPlayer player = new HumanPlayer(expectedSymbol);

            // Act
            char actualSymbol = player.GetPlayerSymbol();

            // Assert
            Assert.Equal(expectedSymbol, actualSymbol);
        }

        [Fact]
        public void GetScoreReturnsZeroInitially()
        {
            // Arrange
            IPlayer player = new HumanPlayer('S');

            // Act
            int score = player.GetScore();

            // Assert
            Assert.Equal(0, score);
        }

        [Fact]
        public void IncreaseScoreIncreasesScoreCorrectly()
        {
            // Arrange
            IPlayer player = new HumanPlayer('S');

            // Act
            player.IncreaseScore(10);
            int score = player.GetScore();

            // Assert
            Assert.Equal(10, score);
        }

        [Fact]
        public void SetPlayerSymbolSetsSymbolCorrectly()
        {
            // Arrange
            IPlayer player = new HumanPlayer('S');

            // Act
            player.SetPlayerSymbol('O');
            char symbol = player.GetPlayerSymbol();

            // Assert
            Assert.Equal('O', symbol);
        }

        
    }
}
