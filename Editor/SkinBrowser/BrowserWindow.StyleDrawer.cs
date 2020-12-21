// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowserWindow.StyleDrawer.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

using JetBrains.Annotations;

using UnityEngine;

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     Nested <see cref="StyleDrawer" /> of the <see cref="BrowserWindow" /> class.
    /// </summary>
    public partial class BrowserWindow
    {
        private sealed class StyleDrawer : IDrawable<StyleWithState>
        {
            [NotNull]
            private readonly IDrawable<StyleWithState> _drawer;

            [NotNull]
            private readonly TileStylesSelector _selector;

            internal StyleDrawer( [NotNull] IDrawable<StyleWithState> drawer, [NotNull] TileStylesSelector selector )
            {
                _drawer = drawer ?? throw new ArgumentNullException( nameof( drawer ) );
                _selector = selector ?? throw new ArgumentNullException( nameof( selector ) );
            }

            /// <inheritdoc />
            public void Draw( Rect rectangle, StyleWithState item )
            {
                _selector.PrepareStyle( item.Style );
                _drawer.Draw( rectangle, item );
            }
        }
    }
}