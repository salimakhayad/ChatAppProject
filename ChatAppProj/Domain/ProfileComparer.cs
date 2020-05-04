using ChatApp.Models.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Domain
{
    public class ProfileComparer : IEqualityComparer<ProfileChatModel>, IComparer<ProfileChatModel>
    {
        public bool Equals(ProfileChatModel x, ProfileChatModel y)
        {
            return String.Equals(x.ProfileName, y.ProfileName);
        }

        public int GetHashCode(ProfileChatModel obj)
        {
            return obj.ProfileName.GetHashCode();
        }
        public int Compare(ProfileChatModel x, ProfileChatModel y)
        {
            return String.Compare(x.ProfileName, y.ProfileName);
        }
    }
}
