// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowserWindow.StyleWithStateFilter.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using JetBrains.Annotations;

using UnityEngine;

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     Nested <see cref="StyleWithStateFilter" /> class of the <see cref="BrowserWindow" /> class.
    /// </summary>
    public partial class BrowserWindow
    {
        private sealed class StyleWithStateFilter : ICollectionProvider<StyleWithState>
        {
            [NotNull]
            private readonly ICollectionProvider<GUIStyle> _skinFilter;

            [NotNull]
            private readonly ICurrentValueProvider<bool> _stateProvider;

            internal StyleWithStateFilter(
                [NotNull] ICollectionProvider<GUIStyle> skinFilter,
                [NotNull] ICurrentValueProvider<bool> stateProvider )
            {
                _skinFilter = skinFilter ?? throw new ArgumentNullException( nameof( skinFilter ) );
                _stateProvider = stateProvider ?? throw new ArgumentNullException( nameof( stateProvider ) );
                _skinFilter.Changed += HandleFilterChange;
            }

            public event Action<IEnumerable<StyleWithState>> Changed;

            public IEnumerable<StyleWithState> Current =>
                _skinFilter.Current.Select( ApplyState );

            public int CurrentCount =>
                _skinFilter.CurrentCount;

            public StyleWithState this[ int index ] =>
                ApplyState( _skinFilter[ index ] );

            private StyleWithState ApplyState( GUIStyle style ) =>
                new StyleWithState( style, _stateProvider.Current );

            private void HandleFilterChange( IEnumerable<GUIStyle> styles ) =>
                Changed?.Invoke( styles.Select( ApplyState ) );
        }
    }
}