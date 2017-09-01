﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class User
    {
        public User()
        {
            Role = new List<Role>();
        }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsFirstTimeLogin { get; set; }
        public int AccessFailedCount { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }
        public List<Role> Role { get; set; }

        public Department department { get; set; }
    }

    public class Department
    {
        public int DepartId { get; set; }

        public string DepartName { get; set; }
    }
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
    public class Customer
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsFirstTimeLogin { get; set; }
        public int AccessFailedCount { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }
        public Role Role { get; set; }
    }


}
