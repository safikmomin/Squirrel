using Foundation;
using UIKit;

namespace Squirrel
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations

        public override UIWindow Window
        {
            get;
            set;
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // Override point for customization after application launch.
            // If not required for your application you can safely delete this method
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTMyMjRAMzEzNjJlMzIyZTMwYjc2T1d4eUN3ZzN6TkJrbjBhTmtMeVAwQ2ZSbnRFVWVXakpWZkxtVXY2OD0=;MTMyM" +
                                                                           "jVAMzEzNjJlMzIyZTMwZFdDUTFWTUJaa3o1SnR0SkpQS1B1Qzd3NWhld1ZvY29jdXU3UFU5Rm5kQT0=;MTMyMjZAMzEzN" +
                                                                           "jJlMzIyZTMwY2dpV0NYRHJjaUJMSDN4ZHhlTUxxT0tMTVFRTVg0REJHYmNKM0QySGZ4UT0=;MTMyMjdAMzEzNjJlMzIyZTM" +
                                                                           "wTjdXY0dMMkJWUS8yTk5rdUdHcjk4TG5ZVlZnc3RaUlNoc2kyc0VMZklzOD0=;MTMyMjhAMzEzNjJlMzIyZTMwU0hOUnk2c" +
                                                                           "WMzc2xhcElXOHFqbnMzL2Y4elZxRGdObnI3b3dlYTZ2dGxmUT0=;MTMyMjlAMzEzNjJlMzIyZTMwbVRBWmxnbnptVHIrZm9" +
                                                                           "qb05QVlV1SHNxTm5kQVhRcTlLMHBPKzBveDgydz0=;MTMyMzBAMzEzNjJlMzIyZTMwYzkvbnVEVWNrSThibldHdW9DelFNR" +
                                                                           "lU0MlIvcTBhM2xtcUJ4TVJsWEduYz0=;MTMyMzFAMzEzNjJlMzIyZTMwTjA4SGtZRlc2MWFOUDhOMzQ0bkdCVGZlMHk0QlE" +
                                                                           "1eVZxQ3JCY0lFbUhlRT0=;MTMyMzJAMzEzNjJlMzIyZTMwRmlsd0Y0YkdTdThzVjFYMEc5RlhZQU55NzdVQlJEMEcxN3BQOW5naGFCMD0=");
            return true;
        }

        public override void OnResignActivation(UIApplication application)
        {
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
        }

        public override void WillEnterForeground(UIApplication application)
        {
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        public override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive. 
            // If the application was previously in the background, optionally refresh the user interface.
        }

        public override void WillTerminate(UIApplication application)
        {
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
        }
    }
}

