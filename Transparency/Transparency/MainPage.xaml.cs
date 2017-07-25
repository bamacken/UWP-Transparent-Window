using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Microsoft.Graphics.Canvas.Effects;
using System.Numerics;
using Windows.Graphics.Effects;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml.Hosting;
using Windows.UI.ViewManagement;
using Windows.ApplicationModel.Core;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Transparency
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Compositor _compositor;
        SpriteVisual _hostSprite;

        public MainPage()
        {
            this.InitializeComponent();
            MakePretty();
            _compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _hostSprite = _compositor.CreateSpriteVisual();
            _hostSprite.Size = new Vector2((float)BackgroundGrid.ActualWidth, (float)BackgroundGrid.ActualHeight);

            ElementCompositionPreview.SetElementChildVisual(BackgroundGrid, _hostSprite);

            UpdateEffect();
        }

        private void EffectSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_hostSprite != null)
            {
                UpdateEffect();
            }
        }

        private void BackgroundGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_hostSprite != null)
            {
                _hostSprite.Size = e.NewSize.ToVector2();
            }
        }

        void MakePretty()
        {
            ApplicationViewTitleBar formattableTitleBar = ApplicationView.GetForCurrentView().TitleBar;
            formattableTitleBar.ButtonBackgroundColor = Colors.Transparent;
            formattableTitleBar.ButtonForegroundColor = Colors.White;
            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
        }

        private void UpdateEffect()
        {
            //Matrix5x4 mat = new Matrix5x4()
            //{
            //    M11 = 1,
            //    M12 = 0f,
            //    M13 = 0f,
            //    M14 = 2,
            //    M21 = 0f,
            //    M22 = 1f,
            //    M23 = 0f,
            //    M24 = -1,
            //    M31 = 0f,
            //    M32 = 0f,
            //    M33 = 1f,
            //    M34 = -1,
            //    M41 = 0f,
            //    M42 = 0f,
            //    M43 = 0f,
            //    M44 = 0,
            //    M51 = 0,
            //    M52 = 0,
            //    M53 = 0,
            //    M54 = 0
            //};

            //IGraphicsEffect graphicsEffect = new ColorMatrixEffect()
            //{
            //    ColorMatrix = mat,
            //    Source = new CompositionEffectSourceParameter("ImageSource")
            //};


            //// Create the effect factory and instantiate a brush
            //CompositionEffectFactory _effectFactory = _compositor.CreateEffectFactory(graphicsEffect, null);
            //CompositionEffectBrush brush = _effectFactory.CreateBrush();

            //// Set the destination brush as the source of the image content
            //brush.SetSourceParameter("ImageSource", _compositor.CreateHostBackdropBrush());

            //_hostSprite.Brush = brush;
            //basic sampling of background for transparency
            _hostSprite.Brush = _compositor.CreateHostBackdropBrush();
        }
    }
}
