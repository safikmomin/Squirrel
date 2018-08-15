using System;
using System.Collections.Generic;
using System.Drawing;
using CoreGraphics;
using Foundation;
using Squirrel;
using Squirrel.Controller.Register;
using UIKit;

namespace Squirrel
{
    public partial class ViewController : SquirrelUIViewController, IUISearchResultsUpdating
    {
        private UIStackView TopRightStackView;
        private UIStackView LeftStackView;
        //private UIStackView SearchStackView;
        private UIStackView RightStackView;
        //private UIStackView FullStackView;
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
            RightTableView = new UITableView
            {
                Source = new SideTableViewSource(null, this)
            };
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
            ItemCollectionView CVSource = new ItemCollectionView(100);
            CollectionView = new UICollectionView(UIScreen.MainScreen.Bounds, CollectionViewlayout);
            CollectionView.ContentSize = View.Frame.Size;
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
            ClearButton.SetTitle("Clear Order", UIControlState.Normal);
            BackButton.SetImage(UIImage.FromBundle("Cancel").ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate), UIControlState.Normal);
            BackButton.SetTitleColor(UIColor.White, UIControlState.Normal);
            ClearButton.SetTitleColor(UIColor.White, UIControlState.Normal);
            BackButton.SetTitleColor(UIColor.Green, UIControlState.Focused);
            ClearButton.SetTitleColor(UIColor.Green, UIControlState.Focused);
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
    }

    #region
    public class SideTableViewSource : UITableViewSource
    {
        public string CellIdentifier = "OrderItemCell";
        Dictionary<string, string> OrderedItem;
        ViewController MainController;

        public SideTableViewSource(Dictionary<string, string> items, ViewController mainController)
        {
            this.MainController = mainController;

            if (items != null)
            {
                OrderedItem = items;
            }
            else
            {
                OrderedItem = new Dictionary<string, string>();
            }
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CellIdentifier, indexPath) as SideTableViewCell;
            string item = OrderedItem[indexPath.Row.ToString()];

            //if there are no cells to reuse, create a new one
            if (cell == null)
            { cell = (SideTableViewCell)new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier); }
            //cell.TextLabel.Text = item;
            //cell.SetItemName = item;
            //cell.SetPriceName = "$" + item;

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            //return OrderedItem.Count;
            return 0;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            //MainController.DeleteRow(indexPath.Row);
        }

        public Dictionary<string, string> ItemDictonary
        {
            get { return OrderedItem; }
            set { OrderedItem = value; }
        }

    }

    public sealed class PaddedLabel : UILabel
    {
        private UIEdgeInsets EdgeInsets { get; set; }

        public PaddedLabel()
        {
            EdgeInsets = new UIEdgeInsets(10, 15, 0, 0);
        }

        public override void DrawText(CoreGraphics.CGRect rect)
        {
            base.DrawText(EdgeInsets.InsetRect(rect));
        }
    }

    public class SideTableViewCell : UITableViewCell
    {
        UILabel headingLabel, subheadingLabel;
        UIImageView imageView;

        public SideTableViewCell(NSString cellId) : base(UITableViewCellStyle.Default, cellId)
        {
            SelectionStyle = UITableViewCellSelectionStyle.Gray;
            ContentView.BackgroundColor = UIColor.FromRGB(218, 255, 127);
            imageView = new UIImageView();
            headingLabel = new UILabel()
            {
                Font = UIFont.FromName("Cochin-BoldItalic", 22f),
                TextColor = UIColor.FromRGB(127, 51, 0),
                BackgroundColor = UIColor.Clear
            };
            subheadingLabel = new UILabel()
            {
                Font = UIFont.FromName("AmericanTypewriter", 12f),
                TextColor = UIColor.FromRGB(38, 127, 0),
                TextAlignment = UITextAlignment.Center,
                BackgroundColor = UIColor.Clear
            };
            ContentView.AddSubviews(new UIView[] { headingLabel, subheadingLabel, imageView });

        }
        public void UpdateCell(string caption, string subtitle, UIImage image)
        {
            imageView.Image = image;
            headingLabel.Text = caption;
            subheadingLabel.Text = subtitle;
        }
        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            imageView.Frame = new CGRect(ContentView.Bounds.Width - 63, 5, 33, 33);
            headingLabel.Frame = new CGRect(5, 4, ContentView.Bounds.Width - 63, 25);
            subheadingLabel.Frame = new CGRect(100, 18, 100, 20);
        }
    }


    #endregion
}
