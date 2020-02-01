namespace MS.Component.Jwt.UserClaim
{
    public class UserData
    {
        public long Id { get; set; }
        public string Account { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string RoleName { get; set; }
        public string RoleDisplayName { get; set; }

        public string Token { get; set; } 
    }
}
