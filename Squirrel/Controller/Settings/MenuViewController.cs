using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace Squirrel
{
    public partial class MenuViewController : SquirrelUIViewController
    {
        private UIButton CancelButton;
        private UIStackView LeftStackView;
        private UIStackView RightStackView;
        private UIStackView FullStackView;
        private UIStoryboard MainBoard;

        private static List<string> buttonList = new List<string>() { "Register", "Orders",
            "Reports", "Customer", "Inventory", "Settings"};

        public MenuViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            //View.Opaque = false;
            MainBoard = UIStoryboard.FromName("Main", null);
            this.NavigationController.SetNavigationBarHidden(true, false);
            View.BackgroundColor = UIColor.FromWhiteAlpha(0.0f, 0.9f);
            View.Alpha = 0.9f;
            setupView();
            base.ViewDidLoad();
        }

        private void setupView()
        {
            var margins = View.LayoutMarginsGuide;
            CancelButton = new UIButton()
            {
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            View.Add(CancelButton);
            //CancelButton.SetTitle("Close", UIControlState.Normal);
            CancelButton.SetImage(UIImage.FromBundle("Cancel").
                                   ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate),
                                   UIControlState.Normal);
            CancelButton.TintColor = UIColor.White;
            CancelButton.TopAnchor.ConstraintEqualTo(margins.TopAnchor).Active = true;
            CancelButton.LeadingAnchor.ConstraintEqualTo(margins.LeadingAnchor).Active = true;
            CancelButton.TrailingAnchor.ConstraintEqualTo(margins.TrailingAnchor).Active = true;
            CancelButton.HeightAnchor.ConstraintEqualTo(50).Active = true;
            CancelButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
            CancelButton.TouchUpInside += CancelButton_TouchUpInside;
            CancelButton.BackgroundColor = UIColor.Clear;
            int count = 0;
            LeftStackView = new UIStackView
            {
                Axis = UILayoutConstraintAxis.Vertical,
                Alignment = UIStackViewAlignment.Fill,
                Distribution = UIStackViewDistribution.FillEqually,
                Spacing = 10,
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            RightStackView = new UIStackView
            {
                Axis = UILayoutConstraintAxis.Vertical,
                Alignment = UIStackViewAlignment.Fill,
                Distribution = UIStackViewDistribution.FillEqually,
                Spacing = 10,
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            foreach(var i in buttonList)
            {
                UIButton SettingsButton = new UIButton
                {
                    TranslatesAutoresizingMaskIntoConstraints = false
                };
                SettingsButton.TitleLabel.Font = UIFont.BoldSystemFontOfSize(25);
                CancelButton.BackgroundColor = UIColor.Clear;
                SettingsButton.TouchUpInside += SettingsButton_TouchUpInside;
                SettingsButton.SetTitle(i, UIControlState.Normal);

                if(count < 3)
                {
                    LeftStackView.AddArrangedSubview(SettingsButton);
                }
                else
                {
                    RightStackView.AddArrangedSubview(SettingsButton);
                }
                count++;
            }
            FullStackView = new UIStackView
            {
                Axis = UILayoutConstraintAxis.Horizontal,
                Alignment = UIStackViewAlignment.Fill,
                Distribution = UIStackViewDistribution.FillEqually,
                Spacing = 10,
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            FullStackView.AddArrangedSubview(LeftStackView);
            FullStackView.AddArrangedSubview(RightStackView);
            View.Add(FullStackView);
            FullStackView.TopAnchor.ConstraintEqualTo(CancelButton.BottomAnchor, 20).Active = true;
            FullStackView.BottomAnchor.ConstraintEqualTo(margins.BottomAnchor, -200).Active = true;
            FullStackView.LeadingAnchor.ConstraintEqualTo(margins.LeadingAnchor, 100).Active = true;
            FullStackView.TrailingAnchor.ConstraintEqualTo(margins.TrailingAnchor, -100).Active = true;
        }

        void CancelButton_TouchUpInside(object sender, EventArgs e)
        {
            //this.DismissViewController(true, null);
            this.NavigationController.SetNavigationBarHidden(false, false);
            this.NavigationController.PopViewController(false);
        }

        void SettingsButton_TouchUpInside(object sender, EventArgs e)
        {
            var button = sender as UIButton;
            this.NavigationController.SetNavigationBarHidden(false, false);

            UIViewController controller = MainBoard.InstantiateViewController("CheckOutViewController") as CheckOutViewController;
            if(button.TitleLabel.Text == "Register")
            {
                controller = MainBoard.InstantiateViewController("CheckOutViewController") as CheckOutViewController;
            }
            else
            {
                controller = MainBoard.InstantiateViewController("ViewController") as ViewController;
            }
            this.NavigationController.PopToRootViewController(true);
            //this.NavigationController.PushViewController(controller, true);
            NavigationController.ViewControllers = new UIViewController[] { controller };
        }

        public override bool PrefersStatusBarHidden()
        {
            return true;
        }

    }
}