using System;
using System.Collections.Generic;

namespace BO
{
    public partial class Account
    {
        public int AccountId { get; set; }
        public string FullName { get; set; }
        public int RoleId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string HomeAddress { get; set; }
        public string AccountEmail { get; set; }
        public string AccountPassword { get; set; }
        public bool? AccountStatus { get; set; }

        public virtual Role Role { get; set; }
    }
}
