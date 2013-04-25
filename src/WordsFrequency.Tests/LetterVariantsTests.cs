using System;
using FluentAssertions;
using NUnit.Framework;
using WordsFrequency.WordTree;

namespace WordsFrequency.Tests
{
    [TestFixture]
    public class LetterVariantsTests
    {
        [Test]
        public void AddWord_should_ignore_when_word_is_empty()
        {
            var letterVariants = new LetterVariants();
            
            letterVariants.AddWord("".GetEnumerator(), 0);

            letterVariants.IsEmpty.Should().BeTrue();
        }

        [Test]
        public void AddWord_should_fail_when_word_already_added()
        {
            var letterVariants = new LetterVariants();
            letterVariants.AddWord("abc".GetEnumerator(), 10);
            
            Action addNode = () => letterVariants.AddWord("abc".GetEnumerator(), 10);

            addNode.ShouldThrow<Exception>();
        }

        [Test]
        public void AddWord_should_create_node_when_not_found()
        {
            var letterVariants = new LetterVariants();

            letterVariants.AddWord("a".GetEnumerator(), 5);

            letterVariants.ContainsKey('a').Should().BeTrue();
            letterVariants['a'].Weight.Should().Be(5);
        }

        [Test]
        public void AddWord_should_add_weight_when_node_found()
        {
            var letterVariants = new LetterVariants();
            letterVariants.AddWord("a".GetEnumerator(), 5);
            
            letterVariants.AddWord("ab".GetEnumerator(), 10);

            letterVariants.ContainsKey('a').Should().BeTrue();
            letterVariants['a'].Weight.Should().Be(15);
        }

        [Test]
        public void AddWord_should_add_next_nodes()
        {
            var letterVariants = new LetterVariants();
            letterVariants.AddWord("a".GetEnumerator(), 5);
            
            letterVariants.AddWord("abc".GetEnumerator(), 10);

            letterVariants.ContainsKey('a').Should().BeTrue();
            var a = letterVariants['a'];
            a.Weight.Should().Be(15);

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