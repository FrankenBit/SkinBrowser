// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowserWindow.TileStyles.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

using JetBrains.Annotations;

using UnityEngine;

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     Nested <see cref="TileStyles" /> class of the <see cref="BrowserWindow" /> class.
    /// </summary>
    public sealed partial class BrowserWindow
    {
        /// <summary>
        ///     A definition of styles to be used to draw a style tile.
        /// </summary>
        private sealed class TileStyles : ITileStyles
        {
            /// <summary>
            ///     Initializes a new instance of the <see cref="TileStyles"/> class.
            /// </summary>
            /// <param name="frameStyle">
            ///     The style to be used to draw the frame of the tile.
            /// </param>
            /// <param name="labelStyle">
            ///     The style to be used to draw the label of the tile.
            /// </param>
            /// <param name="backgroundColor">
            ///     The background color to be used to draw the style tile.
            /// </param>
            internal TileStyles(
                [NotNull] DeferredStyle frameStyle,
                [NotNull] DeferredStyle labelStyle,
                Color backgroundColor )
            {
                LabelStyle = labelStyle ?? throw new ArgumentNullException( nameof( labelStyle ) );
                FrameStyle = frameStyle ?? throw new ArgumentNullException( nameof( frameStyle ) );
                BackgroundColor = backgroundColor;
            }

            /// <inheritdoc />
            public Color BackgroundColor { get; }

            /// <inheritdoc />
            public DeferredStyle FrameStyle { get; }

            /// <inheritdoc />
            public DeferredStyle LabelStyle { get; }
        }
    }
}