// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowserWindow.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

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

        private ICurrentValueProvider<float> _scaleSlider;

        private ILayoutDrawable _scrollView;

        private ICurrentValueProvider<GUISkin> _skinSelection;

        private SkinStyleFilter _skinFilter;

        private ICurrentValueProvider<bool> _stateProvider;

        private Toolbar _header;

        private Toolbar _footer;

        [NotNull]
        private static SkinSelection SetupSkinSelection( [NotNull] SkinStyleFilter skinFilter )
        {
            var skinSelection = new SkinSelection(
                new EditorGUILayoutStyledEnumPopup<EditorSkin>( new DeferredStyle( new StyleFactory( "ToolbarPopup" ) ) )
                {
                    Current = EditorSkin.Scene
                } );
            skinSelection.Changed += skinFilter.ChangeSkin;
            return skinSelection;
        }

        [NotNull]
        private static StyleDrawer SetupTileDrawer(
            [NotNull] ICurrentValueProvider<GUIContent> contentProvider,
            [NotNull] StyleNameLabel selectedStyleLabel,
            Color selectedColor )
        {
            SelectableTileStyles styles = SetupTileStyles( selectedColor );
            var styleSelector = new TileStylesSelector( styles );

            var tile = new StyleTile( styles, contentProvider, new TileGeometry( 16, 8, 4 ) );
            tile.Selected += styleSelector.SetSelectedStyle;
            tile.Selected += selectedStyleLabel.ChangeStyle;

            return new StyleDrawer( tile, styleSelector );
        }

        [NotNull]
        private static SelectableTileStyles SetupTileStyles( Color selectedColor )
        {
            var regularStyles = new TileStyles(
                new DeferredStyle( new StyleFactory( "ProjectBrowserTextureIconDropShadow" ) ),
                new DeferredStyle( new StyleFactory( "ProjectBrowserGridLabel" ) ),
                Color.white );

            var selectedStyles = new TileStyles(
                new DeferredStyle( new StyleFactory( @"ProjectBrowserPreviewBg" ) ),
                new DeferredStyle( new StyleFactory( @"ProjectBrowserHeaderBgMiddle" ) ),
                selectedColor );

            return new SelectableTileStyles( regularStyles, selectedStyles );
        }

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
            _scrollView.Draw();
            _footer.Draw();
        }

        private void Setup()
        {
            var textField = new TextContentField(
                new TextField( new DeferredStyle( new StyleFactory( "ToolbarTextField" ) ) )
                {
                    Current = "Text"
                } );

            var selectedStyleLabel =
                new StyleNameLabel( new ClickableSpringLabel( new DeferredStyle( new StyleFactory( "label" ) ) ) );

            _skinFilter = new SkinStyleFilter( new FilterField( new SearchField() ) );

            SkinSelection skinSelection = SetupSkinSelection( _skinFilter );
            _skinSelection = skinSelection;

            SetupBars( textField, selectedStyleLabel, skinSelection );
            _scrollView = SetupTileScroll( textField, selectedStyleLabel );
        }

        private void SetupBars(
            [NotNull] ILayoutDrawable textField,
            [NotNull] ILayoutDrawable selectedStyleLabel,
            [NotNull] SkinSelection skinSelection )
        {
            // ReSharper disable once StringLiteralTypo
            var toolbarButtonStyle = new DeferredStyle( new StyleFactory( @"toolbarbutton" ) );
            var stateProvider = new ToggleControl( toolbarButtonStyle, new GUIContent( "Active" ) );
            _stateProvider = stateProvider;

            var toolbarStyle = new DeferredStyle( new StyleFactory( "Toolbar" ) );
            var leftSlot = new CompositeLayoutDrawable( skinSelection, textField, stateProvider );
            _header = new Toolbar( toolbarStyle, leftSlot, _skinFilter );

            CompositeLayoutDrawable scaleSlot = SetupScaleSlot();
            _footer = new Toolbar( toolbarStyle, selectedStyleLabel, scaleSlot );
        }

        private GridLayout<StyleWithState> SetupGridLayout()
        {
            var tileSize = new StyleTileSize( _scaleSlider, new Vector2( 192, 64 ), new Vector2( 256, 276 ) );
            var skinFilter = new StyleWithStateFilter( _skinFilter, _stateProvider );
            return new GridLayout<StyleWithState>( skinFilter, tileSize );
        }

        private CompositeLayoutDrawable SetupScaleSlot()
        {
            var scaleSlider = new ScaleSlider( 55, 0.2f ); 
            if ( BrowserSettings.TryGetScale( out float scale ) ) scaleSlider.Current = scale;

            _scaleSlider = scaleSlider;
            return new CompositeLayoutDrawable( scaleSlider, new LayoutSpace( 16 ) );
        }

        [NotNull]
        private ScrollView SetupTileScroll(
            [NotNull] TextContentField textField,
            [NotNull] StyleNameLabel selectedStyleLabel )
        {
            StyleDrawer tileDrawer =
                SetupTileDrawer( textField, selectedStyleLabel, new Color32( 0x7F, 0xD6, 0xFD, 0xFF ) );

            GridLayout<StyleWithState> gridLayout = SetupGridLayout();
            var gridTileDrawer = new LayoutItemDrawer<StyleWithState>( gridLayout, tileDrawer );
            var pageStyle = new DeferredStyle( new StyleFactory( @"ProjectBrowserIconAreaBg" ) );
            var verticalLayout = new StyledVerticalLayout( pageStyle, gridTileDrawer );
            return new ScrollView( new ScrollableContent( verticalLayout, gridLayout ) );
        }

        private void UpdateFilterSkinIfRequired()
        {
            if ( !_skinFilter.HasSkin ) _skinFilter.ChangeSkin( _skinSelection.Current );
        }
    }
}