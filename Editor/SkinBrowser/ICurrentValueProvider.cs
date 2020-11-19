// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICurrentValueProvider.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     A service providing a current value of some kind.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the provided value.
    /// </typeparam>
    internal interface ICurrentValueProvider<out T>
    {
        /// <summary>
        ///     Occurs when the current value has been changed.
        /// </summary>
        event Action<T> Changed;

        /// <summary>
        ///     Gets the current value.
        /// </summary>
        T Current { get; }
    }
}