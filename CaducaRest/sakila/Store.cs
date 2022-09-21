using System;
using System.Collections.Generic;

#nullable disable

namespace CaducaRest.sakila
{
    public partial class Store
    {
        public Store()
        {
            Customers = new HashSet<Customer>();
            Inventories = new HashSet<Inventory>();
            staff = new HashSet<staff>();
        }

        public int StoreId { get; set; }
        public byte ManagerStaffId { get; set; }
        public int AddressId { get; set; }
        public DateTime LastUpdate { get; set; }

        public virtual Address Address { get; set; }
        public virtual staff ManagerStaff { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<staff> staff { get; set; }
    }
}
