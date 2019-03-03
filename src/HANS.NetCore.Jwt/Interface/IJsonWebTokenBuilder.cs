using System.Collections.Generic;

namespace HANS.NetCore.Jwt.Interface
{
    public interface IJsonWebTokenBuilder
    {
        string CreateJsonWebToken(Dictionary<string, string> payLoad);
    }
}