﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MbUnit.Framework;

namespace AshMind.Extensions.Tests {
    [TestFixture]
    public class StringExtensionsTests {
        [Test]
        [Row("ab18ba18", "18",  "ab")]
        [Row("abcdefgh", "x",   "abcdefgh")]
        [Row("abcdefgh", "a",   "")]
        [Row("abcdefgh", "fg",  "abcde")]
        public void TestSubstringBefore(string value, string delimiter, string expectedResult) {
            Assert.AreEqual(expectedResult, value.SubstringBefore(delimiter));
        }

        [Test]
        [Row("ab18ba18", "18",  "ab18ba")]
        [Row("abcdefgh", "x",   "abcdefgh")]
        [Row("abcdefgh", "a",   "")]
        [Row("abcdefgh", "fg",  "abcde")]
        public void TestSubstringBeforeLast(string value, string delimiter, string expectedResult) {
            Assert.AreEqual(expectedResult, value.SubstringBeforeLast(delimiter));
        }

        [Test]
        [Row("abcdabcd", "bc",  "dabcd")]
        [Row("abcdefgh", "x",   "abcdefgh")]
        [Row("abcdefgh", "h",   "")]
        [Row("abcdefgh", "cd",  "efgh")]
        public void TestSubstringAfter(string value, string delimiter, string expectedResult) {
            Assert.AreEqual(expectedResult, value.SubstringAfter(delimiter));
        }

        [Test]
        [Row("abcdabcd", "bc",  "d")]
        [Row("abcdefgh", "x",   "abcdefgh")]
        [Row("abcdefgh", "h",   "")]
        [Row("abcdefgh", "cd",  "efgh")]
        public void TestSubstringAfterLast(string value, string delimiter, string expectedResult) {
            Assert.AreEqual(expectedResult, value.SubstringAfterLast(delimiter));
        }

        [Test]
        [Row("abcdabcd", "ab",       "cdabcd")]
        [Row("abcdabcd", "xy",       "abcdabcd")]
        [Row("abcdabcd", "abcdabcd", "")]
        public void TestRemoveStart(string value, string prefix, string expectedResult) {
            Assert.AreEqual(expectedResult, value.RemoveStart(prefix));
        }


        [Test]
        [Row("abcdabcd", "cd",       "abcdab")]
        [Row("abcdabcd", "xy",       "abcdabcd")]
        [Row("abcdabcd", "abcdabcd", "")]
        public void TestRemoveEnd(string value, string suffix, string expectedResult) {
            Assert.AreEqual(expectedResult, value.RemoveEnd(suffix));
        }
    }
}
