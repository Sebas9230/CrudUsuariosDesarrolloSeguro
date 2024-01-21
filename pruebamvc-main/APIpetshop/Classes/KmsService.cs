using Google.Cloud.Kms.V1;
using Google.Protobuf;

public class KmsService
{
    private readonly KeyManagementServiceClient _kmsClient;

    public KmsService()
    {
        _kmsClient = KeyManagementServiceClient.Create();
    }

    public string EncryptText(string plaintext)
    {
        var keyName = "projects/animated-joy-406200/locations/global/keyRings/keyring-Guru/cryptoKeys/key-Guru";
        var encryptRequest = new EncryptRequest
        {
            Name = keyName,
            Plaintext = ByteString.CopyFromUtf8(plaintext)
        };

        var encryptResponse = _kmsClient.Encrypt(encryptRequest);
        return encryptResponse.Ciphertext.ToBase64();
    }

    public string DecryptText(string encryptedMessage)
    {
        var keyName = "projects/animated-joy-406200/locations/global/keyRings/keyring-Guru/cryptoKeys/key-Guru";
        var ciphertext = ByteString.FromBase64(encryptedMessage);
        var decryptRequest = new DecryptRequest
        {
            Name = keyName,
            Ciphertext = ciphertext
        };

        var decryptResponse = _kmsClient.Decrypt(decryptRequest);
        return decryptResponse.Plaintext.ToStringUtf8();
    }
}