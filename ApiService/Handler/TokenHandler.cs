using System.Security.Cryptography;
using System.Text;

namespace ApiService.Handler
{
    public class TokenHandler
    {
        private static readonly string FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "offlineToken.dat");
        public static string LoadToken()
        {
            if (!File.Exists(FilePath))
            {
                throw new FileNotFoundException("Token file not found.");
            }
            byte[] encryptedToken = File.ReadAllBytes(FilePath);
            byte[] decryptedToken = ProtectedData.Unprotect(encryptedToken, null, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(decryptedToken);
        }
    }
}
