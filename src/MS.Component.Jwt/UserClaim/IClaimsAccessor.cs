namespace MS.Component.Jwt.UserClaim
{
    public interface IClaimsAccessor
    {
        string UserName { get; }
        long UserId { get; }
        string UserAccount { get; }
        string UserRole { get; }
        string UserRoleDisplayName { get; }
    }
}