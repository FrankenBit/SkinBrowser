// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowserWindow.ITileStyles.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using JetBrains.Annotations;

using UnityEngine;

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     Nested <see cref="ITileStyles" /> interface of the <see cref="BrowserWindow" /> class.
    /// </summary>
    public sealed partial class BrowserWindow
    {
        /// <summary>
        ///     A definition of styles to be used to draw a style tile.
        /// </summary>
        private interface ITileStyles
        {
            /// <summary>
            ///     Gets the background color to be used to draw the style tile.
            /// </summary>
            Color BackgroundColor { get; }

            /// <summary>
            ///     Gets the style to be used to draw the frame of the tile.
            /// </summary>
            [NotNull]
            DeferredStyle FrameStyle { get; }

            /// <summary>
            ///     Gets the style to be used to draw the label of the tile.
            /// </summary>
            [NotNull]
            DeferredStyle LabelStyle { get; }
        }
    }
}