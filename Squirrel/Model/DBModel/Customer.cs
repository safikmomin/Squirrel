using System;
using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace POSUP.Model
{
    public class Customer
    {
        [PrimaryKey, AutoIncrement]
        public int ID
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public string PhoneNumber
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public string Street
        {
            get;
            set;
        }

        public string City
        {
            get;
            set;
        }

        public string State
        {
            get;
            set;
        }

        public string ZipCode
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
    }
}
