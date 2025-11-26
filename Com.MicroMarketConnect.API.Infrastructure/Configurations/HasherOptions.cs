using System.Security.Cryptography;

namespace Com.MicroMarketConnect.API.Infrastructure.Configurations;

public class HasherOptions
{
    public static readonly string SectionName = "Hasher";

    public int SaltSize { get; set; } = 16;
    public int HashSize { get; set; } = 32;
    public int Iterations { get; set; } = 500000;
    public HashAlgorithmName Algorithm { get; set; } = HashAlgorithmName.SHA512;
}
