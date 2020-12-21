// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowserWindow.GridLayout.cs" company="FrankenBit">
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
    ///     Nested <see cref="GridLayout{T}" /> class of the <see cref="BrowserWindow" /> class.
    /// </summary>
    public partial class BrowserWindow
    {
        private sealed class GridLayout<T> : ILayoutDrawable<IDrawable<T>>, IHeightProvider
        {
            [NotNull]
            private readonly ICollectionProvider<T> _itemProvider;

            private Vector2 _tileSize;

            internal GridLayout(
                [NotNull] ICollectionProvider<T> itemProvider,
                [NotNull] StyleTileSize tileSize )
            {
                if ( tileSize == null ) throw new ArgumentNullException( nameof( tileSize ) );

                _itemProvider = itemProvider ?? throw new ArgumentNullException( nameof( itemProvider ) );
                _tileSize = tileSize.Current;
                tileSize.Changed += HandleTileSizeChange;
            }

            /// <inheritdoc />
            public float Height =>
                CalculatePageHeight( CalculateGridSize().y );

            /// <inheritdoc />
            public void Draw( IDrawable<T> drawable )
            {
                Vector2Int gridSize = CalculateGridSize();
                float pageHeight = CalculatePageHeight( gridSize.y );

                EditorGUILayout.BeginVertical( GUILayout.Height( pageHeight ) );
                GUILayout.Space( pageHeight );

                DrawItems( drawable, gridSize );

                EditorGUILayout.EndVertical();
            }

            /// <summary>
            ///     Calculate the number of available columns depending on specified <paramref name="pageWidth" />
            ///     and <paramref name="itemWidth" />.
            /// </summary>
            /// <param name="pageWidth">
            ///     The available width of the page.
            /// </param>
            /// <param name="itemWidth">
            ///     The width of a single item.
            /// </param>
            /// <returns>
            ///     The number of available columns that fit the specified <paramref name="pageWidth" />.
            /// </returns>
            /// <remarks>
            ///     PageWidth = Columns * ItemWidth + ( Columns - 1 ) * Spacing
            ///     PageWidth = Columns * ItemWidth + Columns * Spacing - Spacing
            ///     PageWidth = Columns * ( ItemWidth + Spacing ) - Spacing
            ///     Columns = ( PageWidth + Spacing ) / ( ItemWidth + Spacing )
            /// </remarks>
            private static int CalculateNumberOfColumns( float pageWidth, float itemWidth ) =>
                Mathf.FloorToInt( ( pageWidth + Spacing.x ) / ( itemWidth + Spacing.x ) );

            private static int CalculateSafeNumberOfColumns( float pageWidth, float itemWidth ) =>
                Mathf.Max( 1, CalculateNumberOfColumns( pageWidth, itemWidth ) );

            private Vector2Int CalculateGridSize()
            {
                int columns = CalculateSafeNumberOfColumns( EditorGUIUtility.currentViewWidth, _tileSize.x );
                int rows = Mathf.CeilToInt( (float)_itemProvider.CurrentCount / columns );
                return new Vector2Int( columns, rows );
            }

            private float CalculatePageHeight( float gridHeight) =>
                gridHeight * ( _tileSize.y + Spacing.y ) - Spacing.y;

            private void DrawItems( [NotNull] IDrawable<T> drawable, Vector2Int gridSize )
            {
                var i = 0;
                for ( var y = 0; y < gridSize.y; y++ )
                {
                    for ( var x = 0; x < gridSize.x; x++ )
                    {
                        Rect itemRect = CalculateItemRectangle( new Vector2( x, y ) );
                        T item = _itemProvider[ i++ ];

                        drawable.Draw( itemRect, item );

                        if ( i >= _itemProvider.CurrentCount ) return;
                    }
                }
            }

            private Rect CalculateItemRectangle( Vector2 position ) =>
                new Rect( Vector2.Scale( position, _tileSize + Spacing ), _tileSize );

            private void HandleTileSizeChange( Vector2 tileSize ) =>
                _tileSize = tileSize;
        }
    }
}