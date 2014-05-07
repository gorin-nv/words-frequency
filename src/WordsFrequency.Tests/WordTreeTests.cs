using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using WordsFrequency.WordTree;

namespace WordsFrequency.Tests
{
    [TestFixture]
    public class WordTreeTests
    {
        [Test]
        public void AddWord_should_fail_when_word_already_added()
        {
            var root = new RootNode();
            root.AddWord(new WordIterator("a"), 5);

            Action action = () => root.AddWord(new WordIterator("a"), 5);

            action.ShouldThrow<Exception>();
        }

        [Test]
        public void AddWord_should_create_node_when_not_found()
        {
            var root = new RootNode();

            root.AddWord(new WordIterator("a"), 5);

            root.Nodes.Single(n => n.Symbol == 'a').VariantsWeight.Should().Be(5);
            root.Nodes.Single(n => n.Symbol == 'a').WordWeight.Should().Be(5);
        }

        [Test]
        public void AddWord_should_add_weight_when_node_found()
        {
            var root = new RootNode();
            root.AddWord(new WordIterator("a"), 5);

            root.AddWord(new WordIterator("ab"), 10);

            root.Nodes.Single(n => n.Symbol == 'a').VariantsWeight.Should().Be(10);
            root.Nodes.Single(n => n.Symbol == 'a').WordWeight.Should().Be(5);
        }

        [Test]
        public void AddWord_should_add_next_nodes()
        {
            var root = new RootNode();
            root.AddWord(new WordIterator("a"), 5);

            root.AddWord(new WordIterator("abc"), 10);

            var a = root.Nodes.Single(n => n.Symbol == 'a');
            a.VariantsWeight.Should().Be(10);
            a.WordWeight.Should().Be(5);

            var ab = a.Nodes.Single(n => n.Symbol == 'b');
            ab.VariantsWeight.Should().Be(10);
            ab.WordWeight.Should().Be(0);

            var abc = ab.Nodes.Single(n => n.Symbol == 'c');
            abc.VariantsWeight.Should().Be(10);
            abc.WordWeight.Should().Be(10);
            abc.Nodes.Should().BeEmpty();
        }

        [Test]
        public void FindNode_should_return_node()
        {
            var root = new RootNode();
            root.AddWord(new WordIterator("abc"), 5);
            var expectedNode = root
                .Nodes.Single(n => n.Symbol == 'a')
                .Nodes.Single(n => n.Symbol == 'b');

            var actualNode = root.FindNode(new WordIterator("ab"));

            actualNode.Should().Be(expectedNode);
        }

        [Test]
        [Sequential]
        public void FindNode_should_return_null_when_node_not_found([Values("ad", "abce")] string prefix)
        {
            var root = new RootNode();
            root.AddWord(new WordIterator("abc"), 5);

            var nodeForPrefix = root.FindNode(new WordIterator(prefix));

            nodeForPrefix.Should().Be(null);
        }
    }
}