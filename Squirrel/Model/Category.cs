using System;
using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace POSUP.Model
{
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int ID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string DoNotShow
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public string Color
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
        public List<Order> Orders
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
