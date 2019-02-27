using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace ECommerce.Models
{
    public class User : IUser
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public bool Verified { get; set; }
        public DateTime UtcDateExpire { get; set; }
        string IUser.Id
        {
            get { return UserId.ToString(); }
        }
    }
    }
