// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowserWindow.CompositeLayoutDrawable.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

using JetBrains.Annotations;

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     Nested <see cref="CompositeLayoutDrawable" /> class of the <see cref="BrowserWindow" /> class.
    /// </summary>
    public sealed partial class BrowserWindow
    {
        /// <summary>
        ///     A composite collection of multiple items implementing the <see cref="ILayoutDrawable" /> interface.
        /// </summary>
        private sealed class CompositeLayoutDrawable : ILayoutDrawable
        {
            [ItemNotNull]
            [NotNull]
            private readonly ILayoutDrawable[] _items;

            /// <summary>
            ///     Initializes a new instance of the <see cref="CompositeLayoutDrawable"/> class.
            /// </summary>
            /// <param name="items">
            ///     A collection of items to be drawn as part of this composite.
            /// </param>
            internal CompositeLayoutDrawable( [ItemNotNull] [NotNull] params ILayoutDrawable[] items )
            {
                _items = items ?? throw new ArgumentNullException( nameof( items ) );
            }

            /// <inheritdoc />
            public void Draw()
            {
                foreach ( ILayoutDrawable item in _items ) item.Draw();
            }
        }
    }
}