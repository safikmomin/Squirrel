using System;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace POSUP.Model
{
    public class ItemBarcode
    {
        [PrimaryKey, AutoIncrement]
        public int ID
        {
            get;
            set;
        }

        public string Barcode
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
    }
}
