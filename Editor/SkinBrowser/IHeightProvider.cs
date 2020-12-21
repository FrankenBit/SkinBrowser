// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHeightProvider.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     Provides the expected height of an element to be drawn.
    /// </summary>
    internal interface IHeightProvider
    {
        /// <summary>
        ///     Gets the expected height of the content to be drawn.
        /// </summary>
        float Height { get; }
    }
}