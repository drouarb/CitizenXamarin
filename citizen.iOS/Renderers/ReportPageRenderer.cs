using System;
using citizen.iOS.Renderers;
using citizen.Views;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ReportPage), typeof(ReportPageRenderer))]
namespace citizen.iOS.Renderers
{
    public class ReportPageRenderer : PageRenderer
    {
        private bool savedBounds;
        private Rectangle bounds;
        NSObject _keyboardShowObserver;
        NSObject _keyboardHideObserver;
        
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            
            if (e.NewElement != null)
            {
                RegisterForKeyboardNotifications();
            }

            if (e.OldElement != null)
            {
                UnregisterForKeyboardNotifications();
            }
        }
        
        void RegisterForKeyboardNotifications()
        {
            if (_keyboardShowObserver == null)
                _keyboardShowObserver = UIKeyboard.Notifications.ObserveWillShow(OnKeyboardShow);
            if (_keyboardHideObserver == null)
                _keyboardHideObserver = UIKeyboard.Notifications.ObserveWillHide(OnKeyboardHide);
        }

        void OnKeyboardShow(object sender, UIKeyboardEventArgs args)
        {
            NSValue result = (NSValue)args.Notification.UserInfo.ObjectForKey(new NSString(UIKeyboard.FrameEndUserInfoKey));
            
            Console.WriteLine("Show Keyboard " + result);
            CGSize keyboardSize = result.RectangleFValue.Size;
            Console.WriteLine(keyboardSize);

            if (savedBounds == false)
            {
                savedBounds = true;
                bounds = Element.Bounds;
            }
            
            (Element as ReportPage).KeyboardChangeHandler(true);

            var newBounds = new Rectangle(bounds.Left, bounds.Top, bounds.Width, App.Current.MainPage.Height - keyboardSize.Height - 100);
            Element.Layout(newBounds);
        }

        void OnKeyboardHide(object sender, UIKeyboardEventArgs args)
        {
            NSValue result = (NSValue)args.Notification.UserInfo.ObjectForKey(new NSString(UIKeyboard.FrameEndUserInfoKey));

            Console.WriteLine("Hide Keyboard " + result);
            (Element as ReportPage).KeyboardChangeHandler(false);

            Element.Layout(bounds);
        }
        
        void UnregisterForKeyboardNotifications()
        {
            if (_keyboardShowObserver != null)
            {
                _keyboardShowObserver.Dispose();
                _keyboardShowObserver = null;
            }

            if (_keyboardHideObserver != null)
            {
                _keyboardHideObserver.Dispose();
                _keyboardHideObserver = null;
            }
        }
    }
}