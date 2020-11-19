// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowserWindow.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

using JetBrains.Annotations;

using UnityEditor;
using UnityEditor.IMGUI.Controls;

using UnityEngine;

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     Controls the skin browser window.
    /// </summary>
    public partial class BrowserWindow : EditorWindow
    {
        private static readonly Vector2 Spacing = new Vector2( 16, 16 );

        private GridLayout<GUIStyle> _gridLayout;

        private DeferredStyle _pageStyle;

        private ScaleSlider _scaleSlider;

        private Vector2 _scrollPosition;

        private SkinSelection _skinSelection;

        private SkinStyleFilter _skinFilter;

        private TextContentField _textField;

        private IDrawable<GUIStyle> _tileDrawer;

        private StyleTileSize _tileSize;

        private Toolbar _header;

        private Toolbar _footer;

        private static Vector2 BeginScrollView( Vector2 scrollPosition, float fullHeight ) =>
            NormalizeScrollPosition(
                EditorGUILayout.BeginScrollView( ScaleScrollPosition( scrollPosition, fullHeight ) ),
                fullHeight );

        private static Vector2 NormalizeScrollPosition( Vector2 scrollPosition, float fullHeight ) =>
            new Vector2( scrollPosition.x, scrollPosition.y / fullHeight );

        private static Vector2 ScaleScrollPosition( Vector2 scrollPosition, float fullHeight ) =>
            new Vector2( scrollPosition.x, scrollPosition.y * fullHeight );

        [MenuItem( "Window/Analysis/IMGUI Skin Browser", priority = 110 )]
        private static void ShowWindow() =>
            GetWindow<BrowserWindow>().Show();

        private void OnDisable() =>
            BrowserSettings.SetScale( _scaleSlider.Current );

        private void OnEnable()
        {
            titleContent = new GUIContent( "IMGUI Skin Browser" );
            Setup();
        }

        private void OnGUI()
        {
            UpdateFilterSkinIfRequired();
            _header.Draw();
            // _scrollPosition = BeginScrollView( _scrollPosition, fullHeight );
            _scrollPosition = EditorGUILayout.BeginScrollView( _scrollPosition );
            EditorGUILayout.BeginVertical( _pageStyle );
            _gridLayout.Draw( _tileDrawer );
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndScrollView();
            _footer.Draw();
        }

        private void Setup()
        {
            // TODO: move scrolling and tile grid control into their own components; maybe split up Setup into multiple smaller methods
            _scaleSlider = new ScaleSlider( 55, 0.2f );
            if ( BrowserSettings.TryGetScale( out float scale ) ) _scaleSlider.Current = scale;

            _skinFilter = new SkinStyleFilter( new FilterField( new SearchField() ) );
            _skinSelection = new SkinSelection(
                new EditorGUILayoutStyledEnumPopup<EditorSkin>(
                    new DeferredStyle( new StyleFactory( "ToolbarPopup" ) ) )
                {
                    Current = EditorSkin.Scene
                } );
            _skinSelection.Changed += _skinFilter.ChangeSkin;
            _tileSize = new StyleTileSize( _scaleSlider, new Vector2( 192, 64 ), new Vector2( 256, 276 ) );

            _textField = new TextContentField(
                new TextField( new DeferredStyle( new StyleFactory( "ToolbarTextField" ) ) )
                {
                    Current = "Text"
                } );

            var selectedStyle =
                new StyleNameLabel( new ClickableSpringLabel( new DeferredStyle( new StyleFactory( "label" ) ) ) );

            var toolbarStyle = new DeferredStyle( new StyleFactory( "Toolbar" ) );
            _header = new Toolbar( toolbarStyle, new CompositeLayoutDrawable( _skinSelection, _textField ), _skinFilter );
            _footer = new Toolbar( toolbarStyle, selectedStyle, new CompositeLayoutDrawable( _scaleSlider, new LayoutSpace( 16 ) ) );

            Color selectedColor = new Color32( 0x7F, 0xD6, 0xFD, 0xFF );

            var regularStyles = new TileStyles(
                new DeferredStyle( new StyleFactory( "ProjectBrowserTextureIconDropShadow" ) ),
                new DeferredStyle( new StyleFactory( "ProjectBrowserGridLabel" ) ),
                Color.white );

            var selectedStyles = new TileStyles(
                new DeferredStyle( new StyleFactory( "ProjectBrowserPreviewBg" ) ),
                new DeferredStyle( new StyleFactory( "ProjectBrowserHeaderBgMiddle" ) ),
                selectedColor );

            var styles = new SelectableTileStyles( regularStyles, selectedStyles );

            var styleSelector = new TileStylesSelector( styles );

            var tile = new StyleTile( styles, _textField, new TileGeometry( 16, 8, 4 ) );
            tile.Selected += styleSelector.SetSelectedStyle;
            tile.Selected += selectedStyle.ChangeStyle;

            _tileDrawer = new StyleDrawer( tile, styleSelector );
            _gridLayout = new GridLayout<GUIStyle>( _skinFilter, _tileSize );
            _pageStyle = new DeferredStyle( new StyleFactory( "ProjectBrowserIconAreaBg" ) );
        }

        private void UpdateFilterSkinIfRequired()
        {
            if ( !_skinFilter.HasSkin ) _skinFilter.ChangeSkin( _skinSelection.Current );
        }

        private sealed class GridLayout<T> : ILayoutDrawable<IDrawable<T>>
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
            public void Draw( IDrawable<T> drawable )
            {
                Vector2Int gridSize = CalculateGridSize();
                // Vector2 fullTileSize = _tileSize + Spacing;

                // float fullTileHeight = fullTileSize.y + LabelHeight;
                // float fullHeight = gridSize.y * fullTileHeight;

                // _scrollPosition = BeginScrollView( _scrollPosition, fullHeight );
                float pageHeight = gridSize.y * ( _tileSize.y + Spacing.y );
                pageHeight -= Spacing.y;

                EditorGUILayout.BeginVertical( GUILayout.Height( pageHeight ) );
                GUILayout.Space( pageHeight );

                DrawItems( drawable, gridSize );

                EditorGUILayout.EndVertical();
                // EditorGUILayout.EndScrollView();
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