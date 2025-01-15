namespace tg117.Domain
{
    public static class roles
    {
        public const string RoleSuperAdmin = "SuperAdmin";
        public const string RoleBasic = "Basic";
        public const string RoleAdmin = "Admin";
    }

    public enum Roles
    {
        SuperAdmin,
        Admin,
        Basic
    }
}