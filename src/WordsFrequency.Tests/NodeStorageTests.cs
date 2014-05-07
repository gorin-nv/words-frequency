using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using WordsFrequency.Impl;
using WordsFrequency.WordTree;

namespace WordsFrequency.Tests
{
    [TestFixture]
    public class NodeStorageTests
    {
        [Test]
        public void Add_should_add_when_limit_not_exhausted()
        {
            var nodeStorage = new LimittedNodeStorage(3);
            var node = new LetterNode('x');
            
            nodeStorage.Add(node);

            nodeStorage.Words.Should().Contain(node);
        }

        [Test]
        public void Add_should_replace_node_with_minimal_word_weight_when_limit_exhausted()
        {
            var nodes = new int[] {11, 2, 7, 5, 3}
                .Select(x =>
                            {
                                var node = new LetterNode('x');
                                node.DeclareWord(x);
                                return node;
                            });
            var nodeStorage = new LimittedNodeStorage(5);
            foreach (var node in nodes)
            {
                nodeStorage.Add(node);
            }

            var newNode = new LetterNode('x');
            newNode.DeclareWord(13);
            nodeStorage.Add(newNode);

            nodeStorage.Words.Select(x => x.WordWeight)
                .Should().BeEquivalentTo(new int[] {11, 13, 7, 5, 3});
        }
    }
}