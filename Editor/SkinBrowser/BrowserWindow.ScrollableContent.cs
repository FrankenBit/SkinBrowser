// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowserWindow.ScrollableContent.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

using JetBrains.Annotations;

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     Nested <see cref="ScrollableContent" /> class of the <see cref="BrowserWindow" /> class.
    /// </summary>
    public partial class BrowserWindow
    {
        private sealed class ScrollableContent : IScrollableContent
        {
            [NotNull]
            private readonly ILayoutDrawable _drawable;

            [NotNull]
            private readonly IHeightProvider _heightProvider;

            internal ScrollableContent( [NotNull] ILayoutDrawable drawable, [NotNull] IHeightProvider heightProvider )
            {
                _drawable = drawable ?? throw new ArgumentNullException( nameof( drawable ) );
                _heightProvider = heightProvider ?? throw new ArgumentNullException( nameof( heightProvider ) );
            }

            public float Height =>
                _heightProvider.Height;

            public void Draw() =>
                _drawable.Draw();
        }
    }
}