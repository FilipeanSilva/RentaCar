using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PWEB.Models
{
    public static class RoleName
    {
        public const string Admin = "admin";
        public const string Company = "company";
        public const string User = "user";
        public const string Worker = "worker";

        public const string AdminOrUser = Admin + "," + User;
        public const string AdminOrCompany = Admin + "," + Company;
        public const string AdminOrWorker = Admin + "," + Worker;
    }
}