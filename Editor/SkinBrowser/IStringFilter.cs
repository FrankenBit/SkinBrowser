// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStringFilter.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using JetBrains.Annotations;

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     A filter that is used to match supplied strings.
    /// </summary>
    internal interface IStringFilter
    {
        /// <summary>
        ///     Check if the filter matches the specified <paramref name="entry" />.
        /// </summary>
        /// <param name="entry">
        ///     The entry to be matched against the filter.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the filter matches the specified <paramref name="entry" />,
        ///     <see langword="false" /> if not.
        /// </returns>
        bool Matches( [NotNull] string entry );
    }
}