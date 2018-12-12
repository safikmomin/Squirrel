using System;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace POSUP.Model
{
    public class OrderItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID
        {
            get;
            set;
        }

        [ForeignKey(typeof(Order))]
        public int OrderID
        {
            get;
            set;
        }

        [ForeignKey(typeof(Item))]
        public int ItemId
        {
            get;
            set;
        }

        public string ItemName
        {
            get;
            set;
        }

        public decimal Price
        {
            get;
            set;
        }

        public decimal Quantity
        {
            get;
            set;
        }

        public decimal UnitPrice
        {
            get;
            set;
        }

        public decimal Tax
        {
            get;
            set;
        }

        public bool IsDeleted
        {
            get;
            set;
        }

        public int ServerId
        {
            get;
            set;
        }

        public string SyncType
        {
            get;
            set;
        }

        public string LastModifiedBy
        {
            get;
            set;
        }

        public DateTime LastModificationTime
        {
            get;
            set;
        }

        public DateTime LastModificationClientTime
        {
            get;
            set;
        }

        [ManyToOne]
        public Item Item
        {
            get;
            set;
        }

        [ManyToOne]
        public Order Order
        {
            get;
            set;
        }

    }
}
