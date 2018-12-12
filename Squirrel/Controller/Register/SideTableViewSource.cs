using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using POSUP.Model;
using UIKit;

namespace Squirrel.Controller.Register
{
    public class SideTableViewSource : UITableViewSource
    {

        public string CellIdentifier = "OrderItemCell";
        List<Item> OrderedItem;
        ViewController MainController;

        public SideTableViewSource(List<Item> items, ViewController mainController)
        {
            this.MainController = mainController;

            if (items != null)
            {
                OrderedItem = items;
            }
            else
            {
                OrderedItem = new List<Item>();
            }
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CellIdentifier, indexPath) as SideTableViewCell;
            Item item = OrderedItem[indexPath.Row];

            //if there are no cells to reuse, create a new one
            if (cell == null)
            { cell = (SideTableViewCell)new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier); }
            //cell.TextLabel.Text = item;
            //cell.SetItemName = item;
            //cell.SetPriceName = "$" + item;
            cell.UpdateCell(item.Name, item.Price.ToString());

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

        public List<Item> ItemDictonary
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
        UILabel ItemName, Price;

        public SideTableViewCell(NSString cellId) : base(UITableViewCellStyle.Default, cellId)
        {
            SelectionStyle = UITableViewCellSelectionStyle.Gray;
            ItemName = new UILabel()
            {
                Font = UIFont.FromName("Cochin-BoldItalic", 22f),
                TextColor = UIColor.FromRGB(127, 51, 0),
                BackgroundColor = UIColor.Clear
            };
            Price = new UILabel()
            {
                Font = UIFont.FromName("AmericanTypewriter", 12f),
                TextColor = UIColor.FromRGB(38, 127, 0),
                TextAlignment = UITextAlignment.Center,
                BackgroundColor = UIColor.Clear
            };
            ContentView.AddSubviews(new UIView[] { ItemName, Price });

        }
        public void UpdateCell(string caption, string subtitle)
        {
            ItemName.Text = caption;
            Price.Text = "$" + subtitle;
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            ItemName.Frame = new CGRect(5, 4, ContentView.Bounds.Width - 63, 25);
            Price.Frame = new CGRect(100, 18, 100, 20);
        }
    }
}
