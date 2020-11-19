// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowserWindow.EditorGUILayoutEnumPopupBase.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;

using JetBrains.Annotations;

using UnityEditor;

using UnityEngine;

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     Nested <see cref="EditorGUILayoutEnumPopupBase{T}" /> class of the <see cref="BrowserWindow" /> class.
    /// </summary>
    public sealed partial class BrowserWindow
    {
        /// <summary>
        ///     A <see langword="base" /> for an editor GUI layout popup
        ///     for an enumeration of type <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the enumeration to display a popup for.
        /// </typeparam>
        private abstract class EditorGUILayoutEnumPopupBase<T> : ILayoutDrawableCurrentValueProvider<T>
            where T : struct, Enum
        {
            private readonly GUIContent[] _names = Enum.GetNames( typeof( EditorSkin ) ).Select( WrapInContent )
                .ToArray();

            private readonly int[] _values = Enum.GetValues( typeof( EditorSkin ) ).Cast<int>().ToArray();

            /// <inheritdoc />
            public event Action<T> Changed;

            /// <inheritdoc />
            public T Current { get; internal set; }

            /// <inheritdoc />
            public void Draw() =>
                DrawAndUpdate( Convert.ToInt32( Current ) );

            protected abstract int Draw(
                int value,
                [ItemNotNull] [NotNull] GUIContent[] names,
                [NotNull] int[] values );

            [NotNull]
            private static GUIContent WrapInContent( [NotNull] string text ) =>
                new GUIContent( text );

            private void ChangeCurrentValue( int value ) =>
                ChangeCurrentValue( (T)Enum.ToObject( typeof( T ), value ) );

            private void ChangeCurrentValue( T value )
            {
                Current = value;
                Changed?.Invoke( value );
            }

            private void DrawAndUpdate( int currentValue ) =>
                UpdateCurrentValue( currentValue, Draw( currentValue, _names, _values ) );

            private void UpdateCurrentValue( int currentValue, int newValue )
            {
                if ( newValue != currentValue ) ChangeCurrentValue( newValue );
            }
        }
    }
}