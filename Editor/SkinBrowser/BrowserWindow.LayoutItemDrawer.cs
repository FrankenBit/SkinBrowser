// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowserWindow.LayoutItemDrawer.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

using JetBrains.Annotations;

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     Nested <see cref="LayoutItemDrawer{T}" /> class of the <see cref="BrowserWindow" /> class.
    /// </summary>
    public partial class BrowserWindow
    {
        private class LayoutItemDrawer<T> : ILayoutDrawable
        {
            [NotNull]
            private readonly IDrawable<T> _itemDrawer;

            [NotNull]
            private readonly ILayoutDrawable<IDrawable<T>> _layoutDrawer;

            public LayoutItemDrawer(
                [NotNull] ILayoutDrawable<IDrawable<T>> layoutDrawer,
                [NotNull] IDrawable<T> itemDrawer )
            {
                _layoutDrawer = layoutDrawer ?? throw new ArgumentNullException( nameof( layoutDrawer ) );
                _itemDrawer = itemDrawer ?? throw new ArgumentNullException( nameof( itemDrawer ) );
            }

            public void Draw() =>
                _layoutDrawer.Draw( _itemDrawer );
        }
    }
}