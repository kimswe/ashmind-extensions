﻿using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Contracts = System.Diagnostics.Contracts;

namespace AshMind.Extensions {
    /// <summary>
    /// Provides a set of extension methods for operations on String.
    /// </summary>
    public static class StringExtensions {
        /// <summary>Indicates whether the specified string is <c>null</c> or an <see cref="string.Empty">Empty</see> string.</summary>
        /// <param name="value">The string to test.</param>
        /// <returns>
        ///    <c>true</c> if the <paramref name="value"/> is <c>null</c> or an empty string (""); otherwise, <c>false</c>.
        /// </returns>
        /// <seealso cref="IsNotNullOrEmpty" />
        [Contracts.Pure] [Pure]
        public static bool IsNullOrEmpty([CanBeNull] this string value) {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>Indicates whether the specified string is not <c>null</c> or an <see cref="string.Empty">Empty</see> string.</summary>
        /// <param name="value">The string to test.</param>
        /// <returns>
        ///    <c>true</c> if the <paramref name="value"/> is not <c>null</c> or an empty string (""); otherwise, <c>false</c>.
        /// </returns>
        /// <seealso cref="IsNullOrEmpty" />
        [Contracts.Pure] [Pure]
        [Obsolete("It is often hard to notice `Not` if you are not aware if it. Please use !x.IsNullOrEmpty() instead.")]
        public static bool IsNotNullOrEmpty([CanBeNull] this string value) {
            return !string.IsNullOrEmpty(value);
        }

        /// <summary>Return the specified string if it is not <see cref="string.Empty">Empty</see>, or <c>null</c> otherwise.</summary>
        /// <param name="value">The string to test.</param>
        /// <example> 
        /// <code>var displayName = name.NullIfEmpty() ?? "Unknown";</code>
        /// </example>
        /// <returns>
        ///    <paramref name="value"/> if it is an empty string (""); otherwise, <c>null</c>.
        /// </returns>
        [Contracts.Pure] [Pure] [CanBeNull]
        public static string NullIfEmpty([CanBeNull] this string value) {
            return !string.IsNullOrEmpty(value) ? value : null;
        }

        #if String_IsNullOrWhiteSpace
        /// <summary>
        /// Indicates whether a specified string is <c>null</c>, empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="value">A <see cref="String" /> value.</param>
        /// <returns>
        ///    <c>true</c> if the value parameter is <c>null</c> or <see cref="String.Empty" />, or if <paramref name="value"/> consists exclusively of white-space characters.
        /// </returns>
        /// <seealso cref="string.IsNullOrWhiteSpace" />
        [Contracts.Pure] [Pure]
        public static bool IsNullOrWhiteSpace([CanBeNull] this string value) {
            return string.IsNullOrWhiteSpace(value);
        }
        #endif

        /// <summary>
        /// Returns a string array that contains the substrings in this string that are delimited by a specified string.
        /// </summary>
        /// <param name="value">A <see cref="String" /> value to split.</param>
        /// <param name="separator">An string that delimits the substrings in this string, or a null reference (<c>Nothing</c> in Visual Basic).</param>
        /// <returns>
        ///     An array whose elements contain the substrings in this string that are delimited by <paramref name="separator" />.
        /// </returns>
        /// <seealso cref="string.Split(string[], System.StringSplitOptions)" />
        [Contracts.Pure] [Pure] [NotNull]
        public static string[] Split([NotNull] this string value, [CanBeNull] string separator) {
            return value.Split(separator, StringSplitOptions.None);
        }

        /// <summary>
        /// Returns a string array that contains the substrings in this string that are delimited by elements of a specified string array.
        /// </summary>
        /// <param name="value">A <see cref="String" /> value to split.</param>
        /// <param name="separator">An array of strings that delimit the substrings in this string, an empty array that contains no delimiters, or a null reference (<c>Nothing</c> in Visual Basic).</param>
        /// <returns>
        ///     An array whose elements contain the substrings in this string that are delimited by one or more strings in <paramref name="separator" />.
        /// </returns>
        /// <seealso cref="string.Split(string[], System.StringSplitOptions)" />
        [Contracts.Pure] [Pure] [NotNull]
        public static string[] Split([NotNull] this string value, [CanBeNull] params string[] separator) {
            return value.Split(separator, StringSplitOptions.None);
        }

        /// <summary>
        /// Returns a string array that contains the substrings in this string that are delimited by a specified string.
        /// </summary>
        /// <param name="value">A <see cref="String" /> value to split.</param>
        /// <param name="separator">An string that delimits the substrings in this string, or a null reference (<c>Nothing</c> in Visual Basic).</param>
        /// <param name="options"><see cref="StringSplitOptions.RemoveEmptyEntries" /> to omit empty array elements from the array returned; or <see cref="StringSplitOptions.None" /> to include empty array elements in the array returned. </param>
        /// <returns>
        ///     An array whose elements contain the substrings in this string that are delimited by <paramref name="separator" />.
        /// </returns>
        /// <seealso cref="string.Split(string[], System.StringSplitOptions)" />
        [Contracts.Pure] [Pure] [NotNull]
        public static string[] Split([NotNull]this string value, [CanBeNull] string separator, StringSplitOptions options) {
            var separators = separator != null ? new[] { separator } : new string[0];
            return value.Split(separators, options);
        }

        /// <summary>
        /// Returns a value indicating whether the specified String object occurs within this string.
        /// </summary>
        /// <param name="original">The <see cref="String" /> value to be analyzed.</param>
        /// <param name="value">The <see cref="String" /> object to seek.</param>
        /// <param name="comparisonType">One of the <see cref="StringComparison" /> values that determines how this string and value are compared.</param>
        /// <returns>
        ///     <c>true</c> if the value parameter occurs within this string, or if value is the empty string (""); otherwise, <c>false</c>.
        /// </returns>
        [Contracts.Pure] [Pure]
        public static bool Contains([NotNull] this string original, [NotNull] string value, StringComparison comparisonType) {
            return original.IndexOf(value, comparisonType) >= 0;
        }
        
        /// <summary>
        /// Returns a substring preceding the first occurence of a specified value.
        /// </summary>
        /// <param name="original">The <see cref="String" /> value to get substring from.</param>
        /// <param name="value">The <see cref="String" /> value following the substring.</param>
        /// <returns>
        ///     Substring preceding the first occurence of <paramref name="value" />, if found; otherwise, the <paramref name="original" /> string.
        /// </returns>
        [Contracts.Pure] [Pure] [NotNull]
        public static string SubstringBefore([NotNull] this string original, [NotNull] string value) {
            return original.SubstringBefore(original.IndexOf(value));
        }

        /// <summary>
        /// Returns a substring preceding the first occurence of a specified value.
        /// </summary>
        /// <param name="original">The <see cref="String" /> value to get substring from.</param>
        /// <param name="value">The <see cref="String" /> value following the substring.</param>
        /// <param name="comparisonType">One of the <see cref="StringComparison" /> values that determines how <paramref name="original" /> and <paramref name="value" /> are compared.</param>
        /// <returns>
        ///     Substring preceding the first occurence of <paramref name="value" />, if found; otherwise, the <paramref name="original" /> string.
        /// </returns>
        [Contracts.Pure] [Pure] [NotNull]
        public static string SubstringBefore([NotNull] this string original, [NotNull] string value, StringComparison comparisonType) {
            return original.SubstringBefore(original.IndexOf(value, comparisonType));
        }

        /// <summary>
        /// Returns a substring before the last occurence of a specified value.
        /// </summary>
        /// <param name="original">The <see cref="String" /> value to get substring from.</param>
        /// <param name="value">The <see cref="String" /> value following the substring.</param>
        /// <returns>
        ///     Substring before the last occurence of <paramref name="value" />, if found; otherwise, the <paramref name="original" /> string.
        /// </returns>
        [Contracts.Pure] [Pure] [NotNull]
        public static string SubstringBeforeLast([NotNull] this string original, [NotNull] string value) {
            return original.SubstringBefore(original.LastIndexOf(value));
        }

        /// <summary>
        /// Returns a substring preceding the last occurence of a specified value.
        /// </summary>
        /// <param name="original">The <see cref="String" /> value to get substring from.</param>
        /// <param name="value">The <see cref="String" /> value following the substring.</param>
        /// <param name="comparisonType">One of the <see cref="StringComparison" /> values that determines how <paramref name="original" /> and <paramref name="value" /> are compared.</param>
        /// <returns>
        ///     Substring preceding the last occurence of <paramref name="value" />, if found; otherwise, the <paramref name="original" /> string.
        /// </returns>
        [Contracts.Pure] [Pure] [NotNull]
        public static string SubstringBeforeLast([NotNull] this string original, [NotNull] string value, StringComparison comparisonType) {
            return original.SubstringBefore(original.LastIndexOf(value, comparisonType));
        }

        [NotNull]
        private static string SubstringBefore([NotNull] this string original, int index) {
            if (index < 0)
                return original;

            return original.Substring(0, index);
        }

        /// <summary>
        /// Returns a substring following the first occurence of a specified value.
        /// </summary>
        /// <param name="original">The <see cref="String" /> value to get substring from.</param>
        /// <param name="value">The <see cref="String" /> value preceding the substring.</param>
        /// <returns>
        ///     Substring following the first occurence of <paramref name="value" />, if found; otherwise, the <paramref name="original" /> string.
        /// </returns>
        [Contracts.Pure] [Pure] [NotNull]
        public static string SubstringAfter([NotNull] this string original, [NotNull] string value) {
            return original.SubstringAfter(original.IndexOf(value) + value.Length);
        }

        /// <summary>
        /// Returns a substring following the first occurence of a specified value.
        /// </summary>
        /// <param name="original">The <see cref="String" /> value to get substring from.</param>
        /// <param name="value">The <see cref="String" /> value preceding the substring.</param>
        /// <param name="comparisonType">One of the <see cref="StringComparison" /> values that determines how <paramref name="original" /> and <paramref name="value" /> are compared.</param>
        /// <returns>
        ///     Substring following the first occurence of <paramref name="value" />, if found; otherwise, the <paramref name="original" /> string.
        /// </returns>
        [Contracts.Pure] [Pure] [NotNull]
        public static string SubstringAfter([NotNull] this string original, [NotNull] string value, StringComparison comparisonType) {
            return original.SubstringAfter(original.IndexOf(value, comparisonType) + value.Length);
        }

        /// <summary>
        /// Returns a substring following the last occurence of a specified value.
        /// </summary>
        /// <param name="original">The <see cref="String" /> value to get substring from.</param>
        /// <param name="value">The <see cref="String" /> value preceding the substring.</param>
        /// <returns>
        ///     Substring following the last occurence of <paramref name="value" />, if found; otherwise, the <paramref name="original" /> string.
        /// </returns>
        [Contracts.Pure] [Pure] [NotNull]
        public static string SubstringAfterLast([NotNull] this string original, [NotNull] string value) {
            return original.SubstringAfter(original.LastIndexOf(value) + value.Length);
        }

        /// <summary>
        /// Returns a substring following the last occurence of a specified value.
        /// </summary>
        /// <param name="original">The <see cref="String" /> value to get substring from.</param>
        /// <param name="value">The <see cref="String" /> value preceding the substring.</param>
        /// <param name="comparisonType">One of the <see cref="StringComparison" /> values that determines how <paramref name="original" /> and <paramref name="value" /> are compared.</param>
        /// <returns>
        ///     Substring following the last occurence of <paramref name="value" />, if found; otherwise, the <paramref name="original" /> string.
        /// </returns>
        [Contracts.Pure] [Pure] [NotNull]
        public static string SubstringAfterLast([NotNull] this string original, [NotNull] string value, StringComparison comparisonType) {
            return original.SubstringAfter(original.LastIndexOf(value, comparisonType) + value.Length);
        }

        [NotNull]
        private static string SubstringAfter([NotNull] this string original, int index) {
            if (index < 0)
                return original;

            return original.Substring(index, original.Length - index);
        }

        /// <summary>
        /// Removes a leading occurence of the specified value, if present.
        /// </summary>
        /// <param name="original">The <see cref="String" /> value to remove from.</param>
        /// <param name="prefix">The <see cref="String" /> value to be removed if present.</param>
        /// <returns>
        ///     The string that remains after an occurrence of <paramref name="prefix" /> is removed from the start of <paramref name="original" /> string.
        /// </returns>
        [Contracts.Pure] [Pure] [NotNull]
        public static string RemoveStart([NotNull] this string original, [NotNull] string prefix) {
            if (!original.StartsWith(prefix))
                return original;

            return original.Substring(prefix.Length);
        }

        /// <summary>
        /// Removes a trailing occurence of the specified value, if present.
        /// </summary>
        /// <param name="original">The <see cref="String" /> value to remove from.</param>
        /// <param name="suffix">The <see cref="String" /> value to be removed if present.</param>
        /// <returns>
        ///     The string that remains after an occurrence of <paramref name="suffix" /> is removed from the end of <paramref name="original" /> string.
        /// </returns>
        [Contracts.Pure] [Pure] [NotNull]
        public static string RemoveEnd([NotNull] this string original, [NotNull] string suffix) {
            if (!original.EndsWith(suffix))
                return original;

            return original.Substring(0, original.Length - suffix.Length);
        }
    }
}