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
        public void AddWord_should_ignore_when_word_is_empty()
        {
            var root = new Root();
            
            root.AddWord("", 0);

            root.Variants.Nodes.Should().BeEmpty();
        }

        [Test]
        public void AddWord_should_fail_when_word_already_added()
        {
            var root = new Root();
            root.AddWord("a", 5);

            Action action = () => root.AddWord("a", 5);

            action.ShouldThrow<Exception>();
        }

        [Test]
        public void AddWord_should_create_node_when_not_found()
        {
            var root = new Root();

            root.AddWord("a", 5);

            root.Variants.Nodes.Single(n => n.Symbol == 'a').VariantsWeight.Should().Be(5);
            root.Variants.Nodes.Single(n => n.Symbol == 'a').WordWeight.Should().Be(5);
        }

        [Test]
        public void AddWord_should_add_weight_when_node_found()
        {
            var root = new Root();
            root.AddWord("a", 5);

            root.AddWord("ab", 10);

            root.Variants.Nodes.Single(n => n.Symbol == 'a').VariantsWeight.Should().Be(10);
            root.Variants.Nodes.Single(n => n.Symbol == 'a').WordWeight.Should().Be(5);
        }

        [Test]
        public void AddWord_should_add_next_nodes()
        {
            var root = new Root();
            root.AddWord("a", 5);

            root.AddWord("abc", 10);

            var a = root.Variants.Nodes.Single(n => n.Symbol == 'a');
            a.VariantsWeight.Should().Be(10);
            a.WordWeight.Should().Be(5);

            var ab = a.Variants.Nodes.Single(n => n.Symbol == 'b');
            ab.VariantsWeight.Should().Be(10);
            ab.WordWeight.Should().Be(0);

            var abc = ab.Variants.Nodes.Single(n => n.Symbol == 'c');
            abc.VariantsWeight.Should().Be(10);
            abc.WordWeight.Should().Be(10);
            abc.Variants.Nodes.Should().BeEmpty();
        }

        [Test]
        public void FindNode_should_return_node()
        {
            var root = new Root();
            root.AddWord("abc", 5);
            var expectedNode = root
                .Variants.Nodes.Single(n => n.Symbol == 'a')
                .Variants.Nodes.Single(n => n.Symbol == 'b');

            var actualNode = root.FindNodeForPrefix("ab");

            actualNode.Should().Be(expectedNode);
        }

        [Test]
        [Sequential]
        public void FindNode_should_return_null_when_node_not_found([Values("ad", "abce")] string prefix)
        {
            var root = new Root();
            root.AddWord("abc", 5);

            var nodeForPrefix = root.FindNodeForPrefix(prefix);

            nodeForPrefix.Should().Be(null);
        }
    }
}