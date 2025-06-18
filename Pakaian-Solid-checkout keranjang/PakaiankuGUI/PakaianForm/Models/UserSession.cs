using System;

namespace PakaianForm.Models
{
    /// <summary>
    /// Static class untuk manage user session
    /// </summary>
    public static class UserSession
    {
        private static string _currentUser;
        private static UserRole _role;
        private static DateTime _loginTime;

        public static string CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                if (!string.IsNullOrEmpty(value))
                {
                    _loginTime = DateTime.Now;
                }
            }
        }

        public static UserRole Role
        {
            get => _role;
            set => _role = value;
        }

        public static DateTime LoginTime => _loginTime;

        public static bool IsLoggedIn => !string.IsNullOrEmpty(_currentUser);

        public static bool IsAdmin => _role == UserRole.Admin;

        public static bool IsCustomer => _role == UserRole.Customer;

        /// <summary>
        /// Clear current session
        /// </summary>
        public static void Logout()
        {
            _currentUser = null;
            _role = UserRole.Customer; // default
            _loginTime = DateTime.MinValue;
        }

        /// <summary>
        /// Get session duration
        /// </summary>
        public static TimeSpan SessionDuration
        {
            get
            {
                if (!IsLoggedIn) return TimeSpan.Zero;
                return DateTime.Now - _loginTime;
            }
        }
    }

    /// <summary>
    /// Enum untuk role user
    /// </summary>
    public enum UserRole
    {
        Customer = 0,
        Admin = 1
    }
}