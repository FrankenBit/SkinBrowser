// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowserWindow.SelectableTileStyles.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

using JetBrains.Annotations;

using UnityEngine;

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     Nested <see cref="SelectableTileStyles" /> class of the <see cref="BrowserWindow" /> class.
    /// </summary>
    public sealed partial class BrowserWindow
    {
        /// <summary>
        ///     A definition of styles to be used to draw a style tile that can be selected by the user.
        /// </summary>
        private sealed class SelectableTileStyles : ITileStyles
        {
            [NotNull]
            private readonly ITileStyles _regular;

            [NotNull]
            private readonly ITileStyles _selected;

            private ITileStyles _current;

            /// <summary>
            ///     Initializes a new instance of the <see cref="SelectableTileStyles"/> class.
            /// </summary>
            /// <param name="regular">
            ///     The styles to be used to draw a non-selected tile.
            /// </param>
            /// <param name="selected">
            ///     The styles to be used to draw a selected tile.
            /// </param>
            internal SelectableTileStyles( [NotNull] ITileStyles regular, [NotNull] ITileStyles selected )
            {
                _regular = regular ?? throw new ArgumentNullException( nameof( regular ) );
                _selected = selected ?? throw new ArgumentNullException( nameof( selected ) );
                _current = _regular;
            }

            /// <inheritdoc />
            public Color BackgroundColor =>
                _current.BackgroundColor;

            /// <inheritdoc />
            public DeferredStyle FrameStyle =>
                _current.FrameStyle;

            /// <inheritdoc />
            public DeferredStyle LabelStyle =>
                _current.LabelStyle;

            /// <summary>
            ///     Specify whether the <paramref name="selected"/> style should be used.
            /// </summary>
            /// <param name="selected">
            ///     <see langword="true" /> to use the selected tile style,
            ///     <see langword="false" /> to use the regular tile style.
            /// </param>
            internal void SetSelected( bool selected ) =>
                _current = selected ? _selected : _regular;
        }
    }
}