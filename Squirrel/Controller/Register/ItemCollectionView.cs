using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Squirrel.Controller.Register
{
    public class ItemCollectionView : UICollectionViewSource
    {
        List<string> list = new List<string>();
        ViewController MainController;


        public ItemCollectionView(int num, ViewController MainController)
        {
            this.MainController = MainController;
            for (int i = 0; i < num; i++)
            {
                list.Add(i.ToString());
            }
        }

        public void ChangeList(int num)
        {
            list = new List<string>();
            for (int i = 0; i < num; i++)
            {
                list.Add(i.ToString());
            }
        }

        public override nint NumberOfSections(UICollectionView collectionView)
        {
            return 1;
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return list.Count;
        }

        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            UITapGestureRecognizer gestureTap = new UITapGestureRecognizer((obj) => {
                var name = obj.View as ItemCell;
            });
            var item = (ItemCell)collectionView.DequeueReusableCell(ItemCell.CellId, indexPath);
            item.AddGestureRecognizer(gestureTap);
            item.TopLabel = list[indexPath.Row] + " Chicken Tikka with chicken gravey";
            item.BottomLabel = "$1.99";
            item.idLabel = list[indexPath.Row];
            return item;
        }

        private void ButtonCliclk(UITapGestureRecognizer obj)
        {
            var x = obj.View as ItemCell;
            //MainController.AddItemInRow((int)x.Tag);
            //Console.WriteLine("I clicked: " + x.CurrentTitle);
        }

        public override bool CanMoveItem(UICollectionView collectionView, NSIndexPath indexPath)
        {
            // We can always move items
            return false;
        }

        public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
        {
            Console.WriteLine("Row {0} selected", indexPath.Row);
            list = new List<string>();
            MainController.UpdateView();
        }

        public override void ItemHighlighted(UICollectionView collectionView, NSIndexPath indexPath)
        {
            //var cell = collectionView.CellForItem(indexPath) as ItemCell;
            //cell.ContentView.BackgroundColor = UIColor.Yellow;
        }

        public override bool ShouldHighlightItem(UICollectionView collectionView, NSIndexPath indexPath)
        {
            return true;
        }

        void HandleAction(object sender, EventArgs e)
        {

        }

    }

    public class ItemCell : UICollectionViewCell
    {
        UILabel MainLabel;
        UILabel SecondLabel;
        UILabel IDLabel;
        UIStackView stackView;
        public static readonly NSString CellId = new NSString("TextCell");

        [Export("initWithFrame:")]
        public ItemCell(CGRect frame) : base(frame)
        {
            stackView = new UIStackView(ContentView.Frame)
            {
                Axis = UILayoutConstraintAxis.Vertical,
                Alignment = UIStackViewAlignment.Fill,
                Distribution = UIStackViewDistribution.Fill,
                //Spacing = 10,
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            MainLabel = new UILabel
            {
                TextColor = UIColor.DarkGray,
                TextAlignment = UITextAlignment.Center,
                TranslatesAutoresizingMaskIntoConstraints = false
            };

            //MainLabel.LineBreakMode = UILineBreakMode.WordWrap;
            //MainLabel.MinimumScaleFactor = 1;
            //MainLabel.AdjustsFontSizeToFitWidth = true;
            MainLabel.Lines = 2;

            SecondLabel = new UILabel
            {
                BackgroundColor = UIColor.FromRGB(66, 165, 245),
                TextColor = UIColor.White,
                TextAlignment = UITextAlignment.Center,
                TranslatesAutoresizingMaskIntoConstraints = false,
            };

            IDLabel = new UILabel
            {
                BackgroundColor = UIColor.FromRGB(66, 165, 245),
                TextColor = UIColor.White,
                TextAlignment = UITextAlignment.Center,
                TranslatesAutoresizingMaskIntoConstraints = false,
                Hidden = true
            };

            MainLabel.Layer.CornerRadius = 10.0f;
            SecondLabel.Layer.CornerRadius = 10.0f;

            stackView.AddArrangedSubview(SecondLabel);
            stackView.AddArrangedSubview(MainLabel);
            stackView.AddArrangedSubview(IDLabel);
            ContentView.AddSubview(stackView);
            SecondLabel.HeightAnchor.ConstraintEqualTo(20).Active = true;
            SecondLabel.LeadingAnchor.ConstraintEqualTo(ContentView.LeadingAnchor).Active = true;
            SecondLabel.BottomAnchor.ConstraintEqualTo(ContentView.BottomAnchor).Active = true;
            SecondLabel.TrailingAnchor.ConstraintEqualTo(ContentView.TrailingAnchor).Active = true;

            MainLabel.LeadingAnchor.ConstraintEqualTo(ContentView.LeadingAnchor).Active = true;
            MainLabel.TrailingAnchor.ConstraintEqualTo(ContentView.TrailingAnchor).Active = true;
            MainLabel.TopAnchor.ConstraintEqualTo(ContentView.TopAnchor).Active = true;
            MainLabel.BottomAnchor.ConstraintEqualTo(SecondLabel.TopAnchor).Active = true;

            ContentView.Layer.BorderColor = UIColor.Gray.CGColor;
            ContentView.Layer.BorderWidth = 1.0f;
            //ContentView.Layer.CornerRadius = 10.0f;
        }

        public string TopLabel
        {
            get { return MainLabel.Text; }
            set { MainLabel.Text = value; }
        }

        public string BottomLabel
        {
            get { return SecondLabel.Text; }
            set { SecondLabel.Text = value; }
        }

        public string idLabel
        {
            get { return IDLabel.Text; }
            set { IDLabel.Text = value; }
        }



    }

}
