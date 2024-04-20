using Microsoft.Extensions.Configuration;

namespace MSConfiguration
{
    public interface IMSConfigManager
    {
        string MSCS { get; }

        string EncryptionDecryptionAlgorithm { get; }

        string EncryptionDecryptionKey { get; }

        string EmailID { get; }

        string AccountKey { get; }

        string GetConnectionString(string connectionName);

        IConfigurationSection GetConfigurationSection(string Key);
    }
}