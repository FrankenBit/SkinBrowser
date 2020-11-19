// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowserWindow.SkinStyleFilter.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;

using JetBrains.Annotations;

using UnityEngine;

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     Nested <see cref="SkinStyleFilter" /> class of the <see cref="BrowserWindow" /> class.
    /// </summary>
    public sealed partial class BrowserWindow
    {
        /// <summary>
        ///     A filter used to filter out styles from a <see cref="GUISkin" />.
        /// </summary>
        private sealed class SkinStyleFilter : ICollectionProvider<GUIStyle>, ILayoutDrawable
        {
            [NotNull]
            private readonly FilterField _filterField;

            private readonly List<GUIStyle> _styles = new List<GUIStyle>();

            [CanBeNull]
            private GUISkin _skin;

            /// <summary>
            ///     Initializes a new instance of the <see cref="SkinStyleFilter"/> class.
            /// </summary>
            /// <param name="filterField">
            ///     The filter field used to draw the input field where the user can enter the filter string.
            /// </param>
            internal SkinStyleFilter( [NotNull] FilterField filterField )
            {
                _filterField = filterField ?? throw new ArgumentNullException( nameof( filterField ) );
                _filterField.Changed += UpdateStyles;
            }

            /// <inheritdoc />
            public event Action<IEnumerable<GUIStyle>> Changed;

            /// <inheritdoc />
            public IEnumerable<GUIStyle> Current =>
                FilteredStyles;

            /// <inheritdoc />
            public int CurrentCount =>
                _styles.Count;

            /// <summary>
            ///     Gets a value indicating whether the filter has a valid skin set.
            /// </summary>
            internal bool HasSkin =>
                _skin != null;

            /// <summary>
            ///     Gets an enumeration of styles from the current skin matching the currently entered filter.
            /// </summary>
            private IEnumerable<GUIStyle> FilteredStyles =>
                _styles;

            /// <inheritdoc />
            public GUIStyle this[ int index ] =>
                _styles[ index ];

            /// <inheritdoc />
            public void Draw() =>
                _filterField.Draw();

            /// <summary>
            ///     Change the <paramref name="skin" /> to filter the styles from.
            /// </summary>
            /// <param name="skin">
            ///     The new GUI skin to filter the styles from.
            /// </param>
            internal void ChangeSkin( [CanBeNull] GUISkin skin )
            {
                _skin = skin ? skin : throw new ArgumentNullException( nameof( skin ) );
                UpdateStyles();
            }

            private static void UpdateStyles(
                [NotNull] GUISkin skin,
                [NotNull] IStringFilter filter,
                [NotNull] ICollection<GUIStyle> list )
            {
                IEnumerator enumerator = skin.GetEnumerator();
                while ( enumerator.MoveNext() )
                {
                    if ( enumerator.Current is GUIStyle style && filter.Matches( style.name ) ) list.Add( style );
                }
            }

            private void UpdateStyles()
            {
                _styles.Clear();
                if ( _skin != null ) UpdateStyles( _skin, _filterField, _styles );

                Changed?.Invoke( _styles );
            }
        }
    }
}