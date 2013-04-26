using System;
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

            root.Variants.IsEmpty.Should().BeTrue();
        }

        [Test]
        public void AddWord_should_create_node_when_not_found()
        {
            var root = new Root();

            root.AddWord("a", 5);

            root.Variants.ContainsKey('a').Should().BeTrue();
            root.Variants['a'].Weight.Should().Be(5);
        }

        [Test]
        public void AddWord_should_add_weight_when_node_found()
        {
            var root = new Root();
            root.AddWord("a", 5);

            root.AddWord("ab", 10);

            root.Variants.ContainsKey('a').Should().BeTrue();
            root.Variants['a'].Weight.Should().Be(5+10);
        }

        [Test]
        public void AddWord_should_add_next_nodes()
        {
            var root = new Root();
            root.AddWord("a", 5);

            root.AddWord("abc", 10);

            root.Variants.ContainsKey('a').Should().BeTrue();
            var a = root.Variants['a'];
            a.Weight.Should().Be(5+10);

            a.Variants.ContainsKey('b').Should().BeTrue();
            var ab = a.Variants['b'];
            ab.Weight.Should().Be(10);

            ab.Variants.ContainsKey('c').Should().BeTrue();
            var abc = ab.Variants['c'];
            abc.Weight.Should().Be(10);
            abc.Variants.IsEmpty.Should().BeTrue();
        }
    }
}