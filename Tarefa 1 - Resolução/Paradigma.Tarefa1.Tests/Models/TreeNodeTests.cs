using Paradigma.Tarefa1.Exceptions;
using Paradigma.Tarefa1.Exceptions.SpecificExceptions;
using Paradigma.Tarefa1.Models;

namespace Paradigma.Tarefa1.Tests
{
    public class TreeNodeRepresentationTests
    {
        [Theory]
        [InlineData("A,B;A,C;B,G;C,H;E,F;B,D;C,E", "A", "B", "C")] // Exemplo 1
        [InlineData("B,D;D,E;A,B;C,F;E,G;A,C", "A", "B", "C")] // Exemplo 2
        public void Build_ShouldCreateTree_WhenValidPairsAreProvided(string pairsData, string expectedRootValue, string expectedChild1Value, string expectedChild2Value)
        {
            // Arrange
            var pairs = pairsData.Split(';')
                .Select(pair =>
                {
                    var nodes = pair.Split(',');
                    return (nodes[0], nodes[1]);
                })
                .ToArray();

            // Act
            var root = TreeNodeRepresentation.Build(pairs);

            // Assert
            Assert.Equal(expectedRootValue, root.Value);
            Assert.Equal(TreeNodeRepresentation.MAX_CHILDREN, root.Children.Count);
            Assert.Equal(expectedChild1Value, root.Children[0].Value);
            Assert.Equal(expectedChild2Value, root.Children[1].Value);
        }

        [Fact]
        public void Build_ShouldThrowE1TooManyChildrenException_WhenMaxChildrenExceeded()
        {
            // Arrange
            var pairs = new (string, string)[]
            {
                ("A", "B"),
                ("A", "C"),
                ("A", "D")
            };

            // Act & Assert
            var exception = Assert.Throws<E1TooManyChildrenException>(() => TreeNodeRepresentation.Build(pairs));
        }

        [Fact]
        public void Build_ShouldThrowE2PresentCycleException_WhenCycleExists()
        {
            // Arrange
            var pairs = new (string, string)[]
            {
                ("A", "B"),
                ("B", "A")
            };

            // Act & Assert
            Assert.Throws<E2PresentCycleException>(() => TreeNodeRepresentation.Build(pairs));
        }

        [Fact]
        //Exemplo 3
        public void Build_ShouldThrowE3MultipleRootsException_WhenMultipleRootsExist()
        {
            // Arrange
            var pairs = new (string, string)[]
            {
                ("A", "B"),
                ("A", "C"),
                ("B", "D"),
                ("D", "C")
            };

            // Act & Assert
            Assert.Throws<E3MultipleRootsException>(() => TreeNodeRepresentation.Build(pairs));
        }

        [Fact]
        public void Build_ShouldThrowE4GeneralTreeException_WhenUnexpectedErrorOccurs()
        {
            // Arrange
            var pairs = new (string, string)[]
            {
                (null, "B"),
            };

            // Act & Assert
            var exception = Assert.Throws<E4GeneralTreeException>(() =>
                TreeNodeRepresentation.Build(pairs));

            Assert.IsType<ArgumentNullException>(exception.InnerException);
        }
    }
}
