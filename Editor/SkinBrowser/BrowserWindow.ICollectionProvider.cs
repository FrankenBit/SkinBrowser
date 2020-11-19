// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowserWindow.ICollectionProvider.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

using JetBrains.Annotations;

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     Nested <see cref="ICollectionProvider{T}" /> of the <see cref="BrowserWindow" /> class.
    /// </summary>
    public sealed partial class BrowserWindow
    {
        /// <summary>
        ///     A service providing a collection of values of some kind.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the provided values.
        /// </typeparam>
        private interface ICollectionProvider<out T> : ICurrentValueProvider<IEnumerable<T>>
        {
            /// <summary>
            ///     Gets the current count of values.
            /// </summary>
            int CurrentCount { get; }

            /// <summary>
            ///     Gets an item at specified <paramref name="index" />.
            /// </summary>
            /// <param name="index">
            ///     The index of the item to be retrieved.
            /// </param>
            /// <returns>
            ///     The item at the specified <paramref name="index" />.
            /// </returns>
            [NotNull]
            T this[ int index ] { get; }
        }
    }
}