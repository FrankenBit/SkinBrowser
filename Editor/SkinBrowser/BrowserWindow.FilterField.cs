// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowserWindow.FilterField.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

using JetBrains.Annotations;

using UnityEditor.IMGUI.Controls;

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     Nested <see cref="FilterField" /> class of the <see cref="BrowserWindow" /> class.
    /// </summary>
    public sealed partial class BrowserWindow
    {
        /// <summary>
        ///     Wrapper for a search field used to enter a filter to apply to an enumeration of entities.
        /// </summary>
        private sealed class FilterField : ILayoutDrawable, IStringFilter
        {
            [NotNull]
            private readonly SearchField _searchField;

            [CanBeNull]
            private string _currentFilter;

            [CanBeNull]
            private string _lowerCaseFilter;

            /// <summary>
            ///     Initializes a new instance of the <see cref="FilterField"/> class.
            /// </summary>
            /// <param name="searchField">
            ///     The search field used to draw the input field where the user can enter the filter string.
            /// </param>
            internal FilterField( [NotNull] SearchField searchField )
            {
                _searchField = searchField ?? throw new ArgumentNullException( nameof( searchField ) );
            }

            /// <summary>
            ///     Occurs when the filter has been changed.
            /// </summary>
            internal event Action Changed;

            /// <inheritdoc />
            public void Draw() =>
                UpdateLowerCaseFilter( UpdateCurrentFilter() );

            /// <inheritdoc />
            public bool Matches( string entry ) =>
                _lowerCaseFilter == null || DoesContainFilter( entry, _lowerCaseFilter );

            private static bool DoesContainFilter( [NotNull] string entry, [NotNull] string filter ) =>
                entry.ToLowerInvariant().Contains( filter );

            private void ChangeLowerCaseFilter( [CanBeNull] string filter )
            {
                _lowerCaseFilter = filter;
                Changed?.Invoke();
            }

            private void ChangeLowerCaseFilterIfDifferent( [CanBeNull] string filter )
            {
                if ( !string.Equals( filter, _lowerCaseFilter ) ) ChangeLowerCaseFilter( filter );
            }

            [CanBeNull]
            private string UpdateCurrentFilter() =>
                _currentFilter = _searchField.OnToolbarGUI( _currentFilter );

            private void UpdateLowerCaseFilter( [CanBeNull] string filter ) =>
                ChangeLowerCaseFilterIfDifferent( filter?.ToLowerInvariant() );
        }
    }
}