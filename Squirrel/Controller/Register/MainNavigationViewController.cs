using CoreGraphics;
using Foundation;
using System;
using UIKit;

namespace Squirrel
{
    public partial class MainNavigationViewController : UINavigationController
    {
        private UIStoryboard MainBoard;
        
        public MainNavigationViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            MainBoard = UIStoryboard.FromName("Main", null);
            SetupBar();
            var nextVC = Storyboard.InstantiateViewController("ViewController") as ViewController;
            this.ViewControllers = new UIViewController[] { nextVC };

        }

        private void SetupBar()
        {
            this.NavigationBar.BarTintColor = UIColor.Black;
        }

        public override bool PrefersStatusBarHidden()
        {
            return true;
        }

        public void ChangeRootViewController(UIViewController controller)
        {
            //foreach(var view in this.ViewControllers)
            //{
            //    view.DismissViewController(false, null);
            //}
            //this.ViewControllers = new UIViewController[] { controller };
            this.ViewControllers = new UIViewController[] { controller };
        }
    }
}