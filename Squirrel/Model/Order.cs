using System;
using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace POSUP.Model
{
    public class Order
    {
        [PrimaryKey, AutoIncrement]
        public int ID
        {
            get;
            set;
        }

        [ForeignKey(typeof(Customer))] 
        public int CustomerId
        {
            get;
            set;
        }

        public string OrderNumber
        {
            get;
            set;
        }

        public DateTime Date
        {
            get;
            set;
        }

        public string Status
        {
            get;
            set;
        }

        public decimal SubTotal
        {
            get;
            set;
        }

        public decimal Tax
        {
            get;
            set;
        }

        public decimal Total
        {
            get;
            set;
        }

        public decimal Change
        {
            get;
            set;
        }

        public decimal RemainingAmount
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
        public Customer Customers
        {
            get;
            set;
        }

        [OneToMany]
        public List<OrderItem> Items
        {
            get;
            set;
        }

    }
}
