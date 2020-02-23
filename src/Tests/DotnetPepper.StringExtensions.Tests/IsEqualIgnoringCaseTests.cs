using System;
using Xunit;
using FluentAssertions;

namespace DotnetPepper.StringExtensions.Tests
{
    public class IsEqualIgnoringCaseTests
    {
        [Fact]
        public void GivenSameString_ReturnTrue()
        {
            string nullString = null;
            nullString.EqualsIgnoringCase().Should().BeTrue();
        }

        [Fact]
        public void GivenEmpty_ReturnTrue() => "".IsNullOrWhiteSpace().Should().BeTrue();

        [Fact]
        public void GivenWhiteSpace_ReturnTrue() => "     ".IsNullOrWhiteSpace().Should().BeTrue();

        [Fact]
        public void GivenANonEmptyString_ReturnFalse() => "Hi".IsNullOrWhiteSpace().Should().BeFalse();
    }
}
