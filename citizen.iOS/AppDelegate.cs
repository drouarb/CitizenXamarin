﻿using System;
using System.Collections.Generic;
using System.Linq;
using Flex;
using Foundation;
using Plugin.LocalNotification.Platform.iOS;
using UIKit;
using UserNotifications;

namespace citizen.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            FlexButton.Init();
            
            LocalNotificationService.Init();
 
            UIApplication.SharedApplication.SetMinimumBackgroundFetchInterval(UIApplication.BackgroundFetchIntervalMinimum);

            global::Xamarin.Forms.Forms.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
        
        // This method only requires for iOS 8 , 9
        public override void ReceivedLocalNotification(UIApplication application, UILocalNotification notification)
        {
            //Change UIApplicationState to suit different situations
            if (UIApplication.SharedApplication.ApplicationState != UIApplicationState.Active)
            {
                LocalNotificationService.NotifyNotificationTapped(notification);
            }
        }
        
        public override void PerformFetch(UIApplication application, Action<UIBackgroundFetchResult> completionHandler)
        {
            App.NotificationService.FetchNotifications().Wait();
            
            completionHandler (UIBackgroundFetchResult.NewData);
        }
    }
}
