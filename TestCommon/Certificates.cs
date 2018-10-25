using System.Security.Cryptography.X509Certificates;

namespace TestCommon
{
    public class Certificates
    {
        /// <summary>
        /// Accepts a thumbprint and returns the corresponding certificate
        /// from the local store or null if it does not exist
        /// </summary>
        /// <param name="thumbprint"></param>
        /// <returns></returns>
        public static X509Certificate2 get_SDK_cert(string thumbprint)
        {
            string thumbPrintUpper = thumbprint.Trim().ToUpper();

            X509Certificate2 certificate = new X509Certificate2();
            X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);

            try
            {
                foreach (X509Certificate2 x509Cert in store.Certificates)
                {
                    if (x509Cert.Thumbprint == thumbPrintUpper)
                        certificate = x509Cert;
                }

                store.Close();
                return certificate;
            }
            catch
            {
                store.Close();
                certificate = null;
                return certificate;
            }

        }

        /// <summary>
        /// Gets a certificate from password and file path or
        /// returns new cert if it fails
        /// </summary>
        /// <param name="password"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static X509Certificate2 GetCertificateFromPath(string password, string path)
        {            
            try
            {
                X509Certificate2 cert = new X509Certificate2(path, password);
                return cert;
            }
            catch
            {
                X509Certificate2 cert = new X509Certificate2();
                return cert;
            }
        }
    }
}
