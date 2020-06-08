using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace FancyMenu
{
    [DesignTimeVisible(false)]
    public partial class ContainerPage : ContentPage
    {
        bool _isFlyoutOpen = false;
        double _scale;
        uint _flyoutSpeed = 200;
        double _pagePositionX;
        double _flyoutTranslationX;
        float _flyoutCornerRadius = 25f;

        public ContainerPage()
        {
            InitializeComponent();
            _scale = MainContent.Scale;
            _pagePositionX = MainContent.TranslationX;

            MainContent.SizeChanged += OnMainContentSizeChanged;
        }

        void OnMainContentSizeChanged(object sender, EventArgs e)
        {
            MainContent.SizeChanged -= OnMainContentSizeChanged;
            _flyoutTranslationX = MainContent.Width * .75;
        }

        void FlyoutClose(object sender, SwipedEventArgs e)
        {
            if (_isFlyoutOpen)
                ToggleFlyout();
        }

        void FlyoutOpen(object sender, SwipedEventArgs e)
        {
            if (!_isFlyoutOpen)
                ToggleFlyout();
        }

        void ToggleFlyout()
        {
            if (_isFlyoutOpen)
            {
                MainContent.ScaleTo(_scale, _flyoutSpeed);
                MainContent.TranslateTo(_pagePositionX, Flyout.TranslationY, _flyoutSpeed);
                MainContent.CornerRadius = 0;
            }
            else
            {
                MainContent.ScaleTo(_scale * .9, _flyoutSpeed);
                MainContent.TranslateTo(Flyout.TranslationX + _flyoutTranslationX, Flyout.TranslationY, _flyoutSpeed);
                MainContent.CornerRadius = _flyoutCornerRadius;
            }

            _isFlyoutOpen = !_isFlyoutOpen;
        }
    }
}
