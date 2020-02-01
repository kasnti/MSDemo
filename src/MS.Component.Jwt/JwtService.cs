using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MS.Component.Jwt.UserClaim;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MS.Component.Jwt
{
    public class JwtService
    {
        private readonly JwtSetting _jwtSetting;
        private readonly TimeSpan _tokenLifeTime;

        public JwtService(IOptions<JwtSetting> options)
        {
            _jwtSetting = options.Value;
            _tokenLifeTime = TimeSpan.FromMinutes(options.Value.LifeTime);
        }
        /*
             iss (issuer)：签发人
             exp (expiration time)：过期时间
             sub (subject)：主题
             aud (audience)：受众
             nbf (Not Before)：生效时间
             iat (Issued At)：签发时间
             jti (JWT ID)：编号
             */

        /// <summary>
        /// 生成身份信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="roleName">登录时的角色</param>
        /// <returns></returns>
        public Claim[] BuildClaims(UserData userData)
        {
            // 配置用户标识
            var userClaims = new Claim[]
            {
                new Claim(UserClaimType.Id,userData.Id.ToString()),//id
                new Claim(UserClaimType.Account,userData.Account),//account
                new Claim(UserClaimType.Name,userData.Name),//name
                new Claim(UserClaimType.RoleName,userData.RoleName),//rolename
                new Claim(UserClaimType.RoleDisplayName,userData.RoleDisplayName),//roledisplayname
                new Claim(JwtRegisteredClaimNames.Jti,userData.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                //new Claim(JwtRegisteredClaimNames.Iss,_jwtSetting.Issuer),
                //new Claim(JwtRegisteredClaimNames.Aud,_jwtSetting.Audience),
                //new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                //这个就是过期时间，可自定义，注意JWT有自己的缓冲过期时间
                //new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.Add(_tokenLifeTime)).ToUnixTimeSeconds()}"),
            };
            return userClaims;
        }

        /// <summary>
        /// 生成jwt令牌
        /// </summary>
        /// <param name="claims">自定义的claim</param>
        /// <returns></returns>
        public string BuildToken(Claim[] claims)
        {
            var nowTime = DateTime.Now;
            var creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.SecurityKey)), SecurityAlgorithms.HmacSha256);
            JwtSecurityToken tokenkey = new JwtSecurityToken(
                issuer: _jwtSetting.Issuer,
                audience: _jwtSetting.Audience,
                claims: claims,
                notBefore: nowTime,
                expires: nowTime.Add(_tokenLifeTime),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(tokenkey);
        }
    }
}