namespace HANS.NetCore.Jwt
{
    public class JsonWebTokenSetting
    {
        /// <summary>
        /// 失效分钟
        /// </summary>
        public int ExpiresMinute { get; set; } = 30;

        /// <summary>
        /// 加密秘钥
        /// </summary>
        public string SecurityKey { get; set; }
    }
}