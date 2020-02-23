using System;
using Xunit;
using FluentAssertions;

namespace DotnetPepper.StringExtensions.Tests
{
    public class IsNullOrWhitespaceTests
    {
        [Fact]
        public void GivenNull_ReturnTrue()
        {
            string nullString = null;
            nullString.IsNullOrWhiteSpace().Should().BeTrue();
        }

        [Fact]
        public void GivenEmpty_ReturnTrue() => "".IsNullOrWhiteSpace().Should().BeTrue();

        [Fact]
        public void GivenWhiteSpace_ReturnTrue() => "     ".IsNullOrWhiteSpace().Should().BeTrue();

        [Fact]
        public void GivenANonEmptyString_ReturnFalse() => "Hi".IsNullOrWhiteSpace().Should().BeFalse();
    }
}
