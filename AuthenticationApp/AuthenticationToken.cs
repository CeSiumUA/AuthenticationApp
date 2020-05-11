using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationApp
{
    public class AuthenticationToken
    {
        public const string Issuer = "Sample_Issuer"; //Issuer
        public const string Audience = "Sample_Audience"; //Audience
        public const string Key = "SampleSecurityKeyInHexadecimalFormat"; //Security key, used for encryption
        public const int TTL = 5; //Token lifetime
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}