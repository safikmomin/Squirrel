using System;
using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace POSUP.Model
{
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int ID
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public decimal? Price
        {
            get;
            set;
        }

        public string Barcode
        {
            get;
            set;
        }

        public string Stock
        {
            get;
            set;
        }

        public string Taxable
        {
            get;
            set;
        }

        public string Image
        {
            get;
            set;
        }

        public bool ReduceStock
        {
            get;
            set;
        }

        public bool DoNotShow
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

        [OneToMany]
        public List<OrderItem> OrderItems
        {
            get;
            set;
        }

        [OneToMany]
        public List<ItemBarcode> ItemBarcode
        {
            get;
            set;
        }

        [OneToMany]
        public List<ItemCategory> ItemCategories
        {
            get;
            set;
        }

    }
}
