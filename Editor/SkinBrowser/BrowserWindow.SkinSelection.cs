// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowserWindow.SkinSelection.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

using JetBrains.Annotations;

using UnityEditor;

using UnityEngine;

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     Nested <see cref="SkinSelection" /> class of the <see cref="BrowserWindow" /> class.
    /// </summary>
    public sealed partial class BrowserWindow
    {
        /// <summary>
        ///     A popup used to select a GUI editor skin.
        /// </summary>
        private sealed class SkinSelection : ICurrentValueProvider<GUISkin>, ILayoutDrawable
        {
            [NotNull]
            private readonly ILayoutDrawableCurrentValueProvider<EditorSkin> _editorSkinSelection;

            /// <summary>
            ///     Initializes a new instance of the <see cref="SkinSelection"/> class.
            /// </summary>
            /// <param name="editorSkinSelection">
            ///     The selection of the editor skin.
            /// </param>
            internal SkinSelection( [NotNull] ILayoutDrawableCurrentValueProvider<EditorSkin> editorSkinSelection )
            {
                _editorSkinSelection =
                    editorSkinSelection ?? throw new ArgumentNullException( nameof( editorSkinSelection ) );
                _editorSkinSelection.Changed += HandleSelectionChange;
            }

            /// <inheritdoc />
            public event Action<GUISkin> Changed;

            /// <inheritdoc />
            [NotNull]
            public GUISkin Current =>
                GetSkin( _editorSkinSelection.Current );

            /// <inheritdoc />
            public void Draw() =>
                _editorSkinSelection.Draw();

            [NotNull]
            private static GUISkin GetSkin( EditorSkin skin ) =>
                EditorGUIUtility.GetBuiltinSkin( skin );

            private void HandleSelectionChange( EditorSkin skin ) =>
                Changed?.Invoke( GetSkin( skin ) );
        }
    }
}