// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TileGeometry.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     Describes the drawing geometry of a tile.
    /// </summary>
    internal struct TileGeometry : IEquatable<TileGeometry>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TileGeometry"/> struct.
        /// </summary>
        /// <param name="labelHeight">
        ///     The height of the tile label in pixels.
        /// </param>
        /// <param name="margin">
        ///     The margin of the actual style content to the frame of the tile in pixels.
        /// </param>
        /// <param name="spacing">
        ///     The spacing between the style frame and the label in pixels.
        /// </param>
        internal TileGeometry( float labelHeight, float margin, float spacing )
        {
            LabelHeight = labelHeight;
            Margin = margin;
            Spacing = spacing;
        }

        /// <summary>
        ///     Gets the height of the tile label in pixels.
        /// </summary>
        internal float LabelHeight { get; }

        /// <summary>
        ///     Gets the margin of the actual style content to the frame of the tile in pixels.
        /// </summary>
        internal float Margin { get; }

        /// <summary>
        ///     Gets the spacing between the style frame and the label in pixels.
        /// </summary>
        internal float Spacing { get; }

        /// <inheritdoc />
        public bool Equals( TileGeometry other ) =>
            LabelHeight.Equals( other.LabelHeight ) && Margin.Equals( other.Margin ) && Spacing.Equals( other.Spacing );

        /// <inheritdoc />
        public override bool Equals( object obj ) =>
            obj is TileGeometry other && Equals( other );

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = LabelHeight.GetHashCode();
                hashCode = ( hashCode * 397 ) ^ Margin.GetHashCode();
                hashCode = ( hashCode * 397 ) ^ Spacing.GetHashCode();
                return hashCode;
            }
        }
    }
}