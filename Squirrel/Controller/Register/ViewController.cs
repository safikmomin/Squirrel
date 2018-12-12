using System;
using System.Collections.Generic;
using System.Drawing;
using CoreGraphics;
using Foundation;
using POSUP.Model;
using Squirrel;
using Squirrel.Controller.Register;
using UIKit;

namespace Squirrel
{
    public partial class ViewController : SquirrelUIViewController, IUISearchResultsUpdating
    {
        private UIStackView TopRightStackView;
        private UIStackView LeftStackView;
        private UIStackView RightStackView;
        private UILabel TopRightLabel;
        private UIButton TopRightButton;
        private UITableView RightTableView;
        private UIButton CheckoutButton;
        private UISearchController SearchController;
        private UICollectionView CollectionView;
        private UICollectionViewFlowLayout CollectionViewlayout;
        private UIButton BackButton;
        private UIButton ClearButton;
        private UIStoryboard MainBoard;
        private UIStoryboard SettingsBoard;
        private MainNavigationViewController mainNavigation;
        private static NSString TabelCellId = new NSString("OrderItemCell");
        private List<Item> OrderedItem;

        protected ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            MainBoard = UIStoryboard.FromName("Main", null);
            SettingsBoard = UIStoryboard.FromName("Setting", null);
            SetupLeftView();
            base.ViewDidLoad();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        private void SetupLeftView()
        {
            //Right Area init
            var margins = View.LayoutMarginsGuide;
            TopRightLabel = new PaddedLabel
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Text = "Orders",
                
            };
            TopRightLabel.Font = UIFont.BoldSystemFontOfSize(25f);

            TopRightButton = new UIButton();
            TopRightButton.SetImage(UIImage.FromBundle("Customer").
                                   ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate),
                                   UIControlState.Normal);
            TopRightButton.ContentEdgeInsets = new UIEdgeInsets(10, 0, 0, 15);
            RightStackView = new UIStackView
            {
                Axis = UILayoutConstraintAxis.Vertical,
                Alignment = UIStackViewAlignment.Fill,
                Distribution = UIStackViewDistribution.Fill,
                Spacing = 10,
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            TopRightStackView = new UIStackView
            {
                Axis = UILayoutConstraintAxis.Horizontal,
                Alignment = UIStackViewAlignment.Fill,
                Distribution = UIStackViewDistribution.Fill,
                Spacing = 10,
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            TopRightStackView.AddArrangedSubview(TopRightLabel);
            TopRightStackView.AddArrangedSubview(TopRightButton);

            RightStackView.AddArrangedSubview(TopRightStackView);
            //View.AddConstraint(TopLeftStackView.TopAnchor.ConstraintEqualTo(this.View.TopAnchor, 100f));
            //TopLeftStackView.TopAnchor.ConstraintEqualTo(margins.TopAnchor).Active = true;
            OrderedItem = new List<Item>();
            RightTableView = new UITableView
            {
                Source = new SideTableViewSource(null, this)
            };
            RightTableView.RegisterClassForCellReuse(typeof(SideTableViewCell), TabelCellId);
            RightStackView.AddArrangedSubview(RightTableView);
            RightTableView.Layer.BorderColor = UIColor.LightGray.CGColor;
            RightTableView.Layer.BorderWidth = 1;

            CheckoutButton = new UIButton
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                BackgroundColor = UIColor.FromRGB(66, 165, 245),
            };

            //CheckoutButton.Layer.CornerRadius = 10;
            CheckoutButton.SetTitle("Pay", UIControlState.Normal);
            CheckoutButton.HeightAnchor.ConstraintEqualTo(70.0f).Active = true;
            RightStackView.AddArrangedSubview(CheckoutButton);

            UIView RightView = new UIView
            {
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            RightView.Layer.BorderColor = UIColor.LightGray.CGColor;
            RightView.Layer.BorderWidth = 1;
            //RightView.Layer.CornerRadius = 10.0f;
            View.Add(RightView);
            RightView.Add(RightStackView);


            RightView.TopAnchor.ConstraintEqualTo(margins.TopAnchor).Active = true;
            RightView.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor).Active = true;
            RightView.BottomAnchor.ConstraintEqualTo(View.BottomAnchor).Active = true;
            RightView.WidthAnchor.ConstraintEqualTo(300).Active = true;

            //View.Add(LeftStackView);
            RightStackView.TopAnchor.ConstraintEqualTo(RightView.TopAnchor).Active = true;
            RightStackView.TrailingAnchor.ConstraintEqualTo(RightView.TrailingAnchor).Active = true;
            RightStackView.BottomAnchor.ConstraintEqualTo(RightView.BottomAnchor).Active = true;
            RightStackView.LeadingAnchor.ConstraintEqualTo(RightView.LeadingAnchor).Active = true;

            //Right Area init
            LeftStackView = new UIStackView
            {
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            View.Add(LeftStackView);
            LeftStackView.TopAnchor.ConstraintEqualTo(margins.TopAnchor,5).Active = true;
            LeftStackView.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor,-15).Active = true;
            LeftStackView.BottomAnchor.ConstraintEqualTo(View.BottomAnchor).Active = true;
            LeftStackView.LeadingAnchor.ConstraintEqualTo(RightView.TrailingAnchor,15).Active = true;
            //float CVwidth = LeftStackView.SystemLayoutSizeFittingSize();
            CollectionViewlayout = new UICollectionViewFlowLayout
            {
                //SectionInset = new UIEdgeInsets(20, 5, 5, 5),
                MinimumInteritemSpacing = 20,
                MinimumLineSpacing = 20,
                ItemSize = new SizeF(150, 80),
                ScrollDirection = UICollectionViewScrollDirection.Vertical
            };
            ItemCollectionView CVSource = new ItemCollectionView(100,this);
            CollectionView = new UICollectionView(UIScreen.MainScreen.Bounds, CollectionViewlayout);
            //CollectionView.ContentSize = View.Frame.Size;
            CollectionView.RegisterClassForCell(typeof(ItemCell), ItemCell.CellId);
            CollectionView.BackgroundColor = UIColor.Clear;
            CollectionView.Source = CVSource;


            LeftStackView.AddArrangedSubview(CollectionView);

            //searchResultsController: null
            SearchController = new UISearchController((UIViewController)null)
            {
                DimsBackgroundDuringPresentation = true
            };
            SearchController.HidesNavigationBarDuringPresentation = false;
            SearchController.Active = false;

            SearchController.SearchBar.Frame = new CGRect(0, 0, 200, 44);
            SearchController.SearchBar.ShowsCancelButton = false;
            SearchController.ObscuresBackgroundDuringPresentation = false;
            SearchController.SearchBar.TextChanged += (s, e) =>
            {
                try
                {
                    string text = "100";
                    if(SearchController.SearchBar.Text.Trim().Length > 0)
                    {
                        text = SearchController.SearchBar.Text;
                    }
                    CVSource.ChangeList(int.Parse(text));
                    CollectionView.ReloadData();
                }
                catch(Exception ex)
                {
                    Console.Write(ex.Message);
                }
            };
            var bar = new UIView(SearchController.SearchBar.Frame);
            bar.BackgroundColor = UIColor.Clear;
            bar.AddSubview(SearchController.SearchBar);
            NavigationItem.RightBarButtonItem = new UIBarButtonItem(bar);
            NavigationItem.HidesSearchBarWhenScrolling = true;
            UITapGestureRecognizer gestureTap = new UITapGestureRecognizer(DismissKeyboard);
            View.AddGestureRecognizer(gestureTap);

            BackButton = new UIButton();
            ClearButton = new UIButton();

            //BackButton.SetTitle("<", UIControlState.Normal);
            ClearButton.SetTitle("Clear", UIControlState.Normal);
            BackButton.SetImage(UIImage.FromBundle("Cancel").ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate), UIControlState.Normal);
            BackButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
            ClearButton.SetTitleColor(UIColor.White, UIControlState.Normal);
            BackButton.SetTitleColor(UIColor.Green, UIControlState.Focused);
            ClearButton.SetTitleColor(UIColor.Black, UIControlState.Focused);
            ClearButton.TintColor = UIColor.White;
            BackButton.TintColor = UIColor.White;

            UIBarButtonItem backbarbutton = new UIBarButtonItem(BackButton);
            UIBarButtonItem clearbarbutton = new UIBarButtonItem(ClearButton);
            NavigationItem.LeftBarButtonItems = new UIBarButtonItem[] { backbarbutton, clearbarbutton};
            //BackButton.Hidden = true;

        }

        void DismissKeyboard()
        {
            SearchController.SearchBar.ResignFirstResponder();
            SearchController.DismissViewController(true, null);
        }

        public void UpdateSearchResultsForSearchController(UISearchController searchController)
        {
            //throw new NotImplementedException();
        }

        public MainNavigationViewController SetMainNavigation
        {
            get { return mainNavigation; }
            set { mainNavigation = value; }
        }

        public void UpdateView()
        {
            CollectionView.ReloadData();
        }

    }
}
