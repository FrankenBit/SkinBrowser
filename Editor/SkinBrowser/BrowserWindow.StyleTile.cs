// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowserWindow.StyleTile.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

using JetBrains.Annotations;

using UnityEngine;

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     Nested <see cref="StyleTile" /> class of the <see cref="BrowserWindow" /> class.
    /// </summary>
    public sealed partial class BrowserWindow
    {
        /// <summary>
        ///     Draws a <see cref="GUIStyle" /> preview tile in the specified destination rectangle.
        /// </summary>
        private sealed class StyleTile : IDrawable<StyleWithState>
        {
            private readonly TileGeometry _geometry;

            [NotNull]
            private readonly ITileStyles _styles;

            [CanBeNull]
            private GUIContent _demoContent;

            /// <summary>
            ///     Initializes a new instance of the <see cref="StyleTile"/> class.
            /// </summary>
            /// <param name="styles">
            ///     The styles to be used to draw the tile.
            /// </param>
            /// <param name="demoProvider">
            ///     A provider of the demo content to be used to demonstrate the supplied style item inside the tile.
            /// </param>
            /// <param name="geometry">
            ///     The drawing geometry of the tile.
            /// </param>
            /// <exception cref="ArgumentNullException">
            ///     <paramref name="styles" /> or <paramref name="demoProvider" /> is <see langword="null" />.
            /// </exception>
            internal StyleTile(
                [NotNull] ITileStyles styles,
                [NotNull] ICurrentValueProvider<GUIContent> demoProvider,
                TileGeometry geometry )
            {
                if ( demoProvider == null ) throw new ArgumentNullException( nameof( demoProvider ) );

                _styles = styles ?? throw new ArgumentNullException( nameof( styles ) );
                _geometry = geometry;

                demoProvider.Changed += HandleChangedDemoContent;
                HandleChangedDemoContent( demoProvider.Current );
            }

            /// <summary>
            ///     Occurs when the style item was selected by the user.
            /// </summary>
            internal event Action<GUIStyle> Selected;

            /// <inheritdoc />
            public void Draw( Rect rectangle, StyleWithState item )
            {
                GUI.backgroundColor = _styles.BackgroundColor;

                Rect frameRectangle = rectangle.TrimBottomBy( _geometry.LabelHeight + _geometry.Spacing );
                if ( GUI.Button( frameRectangle, GUIContent.none, _styles.FrameStyle ) ) Selected?.Invoke( item.Style );

                Rect labelRectangle = rectangle.BottomPart( _geometry.LabelHeight );
                GUI.Label( labelRectangle, item.Name, _styles.LabelStyle );

                GUI.backgroundColor = Color.white;
                Rect itemRectangle = frameRectangle.TrimBy( _geometry.Margin );
                GUI.Toggle( itemRectangle, item.State, _demoContent, item.Style );
            }

            private void HandleChangedDemoContent( [CanBeNull] GUIContent label ) =>
                _demoContent = label;
        }
    }
}