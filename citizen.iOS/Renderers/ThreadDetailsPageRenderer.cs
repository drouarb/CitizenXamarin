using System;
using citizen.iOS.Renderers;
using citizen.Views;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ThreadDetailsPage), typeof(ThreadDetailsPageRenderer))]
namespace citizen.iOS.Renderers
{
    public class ThreadDetailsPageRenderer : PageRenderer
    {
        NSObject _keyboardShowObserver;
        NSObject _keyboardHideObserver;
        private bool savedBounds = false;
        private Rectangle bounds;
        
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
            ThreadDetailsPage threadDetailsPage = Element as ThreadDetailsPage;
            if (threadDetailsPage == null)
                return;

            NSValue result = args.Notification.UserInfo.ObjectForKey(new NSString(UIKeyboard.FrameEndUserInfoKey)) as NSValue;
            if (result == null)
                return;
            
            Console.WriteLine("Show Keyboard " + result);
            CGSize keyboardSize = result.RectangleFValue.Size;
            Console.WriteLine(keyboardSize);

            if (savedBounds == false)
            {
                savedBounds = true;
                bounds = Element.Bounds;
            }
            
            var newBounds = new Rectangle(bounds.Left, bounds.Top, bounds.Width, App.Current.MainPage.Height - keyboardSize.Height - 100);
            Element.Layout(newBounds);
        }
        
        void OnKeyboardHide(object sender, UIKeyboardEventArgs args)
        {
            ThreadDetailsPage threadDetailsPage = Element as ThreadDetailsPage;
            if (threadDetailsPage == null)
                return;

            NSValue result = (NSValue)args.Notification.UserInfo.ObjectForKey(new NSString(UIKeyboard.FrameEndUserInfoKey));
            Console.WriteLine("Hide Keyboard " + result);

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