using System;
using CoreGraphics;
using UIKit;

namespace Squirrel
{
    public class SquirrelUIViewController : UIViewController
    {
        private UIStoryboard MainBoard;
        private UIStoryboard SettingsBoard;
        public UIButton button;
        protected SquirrelUIViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.

        }

        public override void ViewDidLoad()
        {
            MainBoard = UIStoryboard.FromName("Main", null);
            SettingsBoard = UIStoryboard.FromName("Setting", null);
            if (NavigationItem.BackBarButtonItem != null)
            {
                NavigationItem.BackBarButtonItem.Enabled = false;
            }
            NavigationItem.HidesBackButton = true;
            button = new UIButton(UIButtonType.Custom);
            button.SetImage(UIImage.FromBundle("Menu").ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate), UIControlState.Normal);
            button.TintColor = UIColor.White;
            NavigationItem.TitleView = button;
            button.TouchUpInside += MenuView;
        }

        public void MenuView(object sender, EventArgs e)
        {
            var controller = SettingsBoard.InstantiateViewController("MenuViewController") as MenuViewController;
            //controller = this;
            controller.ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve;
            controller.ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext;

            //AddInventoryViewController addInventory = new AddInventoryViewController();
            //PresentViewController(controller, true, null);
            this.NavigationController.PushViewController(controller, false);
        }

        public override bool PrefersStatusBarHidden()
        {
            return true;
        }
    }
}
