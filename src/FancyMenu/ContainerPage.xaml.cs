using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace FancyMenu
{
    [DesignTimeVisible(false)]
    public partial class ContainerPage : ContentPage
    {
        const float FlyoutCornerRadius = 25f;

        bool _isFlyoutOpen = false;
        double _scale;
        uint _flyoutSpeed = 200;
        double _pagePositionX;
        double _flyoutTranslationX;
        
        public ContainerPage()
        {
            InitializeComponent();

            // Set page scale for both content and flyout
            _scale = MainContent.Scale;
            
            _pagePositionX = MainContent.TranslationX;

            // Add event listeners for SizeChanged - Allows us to capture page values after it is rendered
            MainContent.SizeChanged += OnMainContentSizeChanged;
        }

        void OnMainContentSizeChanged(object sender, EventArgs e)
        {
            MainContent.SizeChanged -= OnMainContentSizeChanged;
            _flyoutTranslationX = MainContent.Width * .75;

            if (Flyout.Children.Count == 1 && Flyout.Children[0] is Layout menuPage)
            {
                var flyoutPadding = Flyout.Width - (Flyout.Width * .8);
                (Flyout.Children[0] as Layout).Padding = new Thickness(0, 0, flyoutPadding, 0);
            }
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
                MainContent.CornerRadius = FlyoutCornerRadius;
            }

            _isFlyoutOpen = !_isFlyoutOpen;
        }
    }
}
