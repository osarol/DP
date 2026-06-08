using System;

namespace AutoShowroomApp
{
    public enum UserRole
    {
        Unknown,
        Admin,
        Seller
    }

    public static class UserSession
    {
        public static bool IsAuthenticated { get; set; } = false;
        public static int UserId { get; set; } = -1;
        public static string FullName { get; set; } = string.Empty;
        public static UserRole Role { get; set; } = UserRole.Unknown;
    }
}
