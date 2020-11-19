// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtensionsForRect.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     The extension methods for <see cref="Rect" />.
    /// </summary>
    internal static class ExtensionsForRect
    {
        /// <summary>
        ///     Get the bottom part from the specified <paramref name="rectangle" />
        ///     of the specified <paramref name="height" />.
        /// </summary>
        /// <param name="rectangle">
        ///     The rectangle to get its bottom part from.
        /// </param>
        /// <param name="height">
        ///     The height of the bottom part to get.
        /// </param>
        /// <returns>
        ///     A rectangle with only the bottom part of the specified original rectangle.
        /// </returns>
        internal static Rect BottomPart( this Rect rectangle, float height ) =>
            new Rect( rectangle.x, rectangle.yMax - height, rectangle.width, height );

        /// <summary>
        ///     Trim the bottom of the specified <paramref name="rectangle" /> by <paramref name="offset" /> pixels
        ///     at its bottom.
        /// </summary>
        /// <param name="rectangle">
        ///     The rectangle to trim its bottom side.
        /// </param>
        /// <param name="offset">
        ///     The offset in pixels to trim the rectangle at its bottom side.
        /// </param>
        /// <returns>
        ///     The trimmed rectangle.
        /// </returns>
        internal static Rect TrimBottomBy( this Rect rectangle, float offset ) =>
            new Rect( rectangle.x, rectangle.y, rectangle.width, rectangle.height - offset );

        /// <summary>
        ///     Trim the specified <paramref name="rectangle"/> by <paramref name="offset" /> pixels at each side.
        /// </summary>
        /// <param name="rectangle">
        ///     The rectangle to be trimmed.
        /// </param>
        /// <param name="offset">
        ///     The offset in pixels to trim the rectangle on each side.
        /// </param>
        /// <returns>
        ///     The trimmed rectangle.
        /// </returns>
        internal static Rect TrimBy( this Rect rectangle, float offset ) =>
            Rect.MinMaxRect( rectangle.x + offset, rectangle.y + offset, rectangle.xMax - offset, rectangle.yMax - offset );
    }
}