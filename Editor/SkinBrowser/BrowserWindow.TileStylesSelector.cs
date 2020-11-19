// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowserWindow.TileStylesSelector.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

using JetBrains.Annotations;

using UnityEngine;

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     Nested <see cref="TileStylesSelector" /> class of the <see cref="BrowserWindow" /> class.
    /// </summary>
    public sealed partial class BrowserWindow
    {
        /// <summary>
        ///     A tile styles selector that tracks the selection of the current <see cref="GUIStyle" /> by the user.
        /// </summary>
        private sealed class TileStylesSelector
        {
            [NotNull]
            private readonly SelectableTileStyles _tileStyles;

            [CanBeNull]
            private GUIStyle _selected;

            /// <summary>
            ///     Initializes a new instance of the <see cref="TileStylesSelector"/> class.
            /// </summary>
            /// <param name="tileStyles">
            ///     Selectable tile styles to be controlled by the selector.
            /// </param>
            internal TileStylesSelector( [NotNull] SelectableTileStyles tileStyles )
            {
                _tileStyles = tileStyles ?? throw new ArgumentNullException( nameof( tileStyles ) );
            }

            /// <summary>
            ///     Prepare tile styles to draw a tile for the supplied <paramref name="style" />.
            /// </summary>
            /// <param name="style">
            ///     The <see cref="GUIStyle" /> that will be drawn next using the tile styles.
            /// </param>
            internal void PrepareStyle( [NotNull] GUIStyle style ) =>
                _tileStyles.SetSelected( style == _selected );

            /// <summary>
            ///     Change currently selected <paramref name="style" /> to the supplied one.
            /// </summary>
            /// <param name="style">
            ///     A <see cref="GUIStyle" /> to be set as currently selected.
            /// </param>
            internal void SetSelectedStyle( [CanBeNull] GUIStyle style ) =>
                _selected = style;
        }
    }
}