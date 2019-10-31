using System;
using System.Collections.Generic;

namespace Web.ViewModels
{
    public class ManageUsersViewModel
    {

        public IList<ManageUserViewModel> Users { get; set; }
        public IList<string> Roles { get; set; }
    }
}
