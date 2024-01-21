using APIpetshop.Models;
using Google.Cloud.Kms.V1;
using Google.Protobuf;
using System.Text;
using System.Text.Json;

public class KmsService
{
    private readonly KeyManagementServiceClient _kmsClient;
    private readonly IHostEnvironment _hostEnvironment;

    public KmsService(IHostEnvironment hostEnvironment)
    {
        _hostEnvironment = hostEnvironment;
        InitializeEnvironmentVariableForCredentials();
        _kmsClient = KeyManagementServiceClient.Create();
    }


    private void InitializeEnvironmentVariableForCredentials()
    {
        // Obtén la ruta del directorio del proyecto.
        var projectDirectory = _hostEnvironment.ContentRootPath;

        // Construye la ruta al archivo de credenciales JSON.
        var credentialsPath = Path.Combine(projectDirectory, "Classes", "application_default_credentials.json");

        // Establece la variable de entorno con la ruta completa al archivo de credenciales.
        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialsPath);
    }

    public string EncryptText(string plaintext)
    {
        Console.WriteLine("Entraaaaa a encriptara plaintext: " + plaintext);
        var keyName = "projects/animated-joy-406200/locations/global/keyRings/keyring-Guru/cryptoKeys/key-Guru";
        var encryptRequest = new EncryptRequest
        {
            Name = keyName,
            Plaintext = ByteString.CopyFromUtf8(plaintext)
        };

        var encryptResponse = _kmsClient.Encrypt(encryptRequest);
        Console.WriteLine("ToHex(encryptResponse.Ciphertext): " + ToHex(encryptResponse.Ciphertext));
        return ToHex(encryptResponse.Ciphertext);
    }

    public static string ToHex(ByteString byteString)
    {
        StringBuilder hex = new StringBuilder(byteString.Length * 2);
        foreach (byte b in byteString)
            hex.AppendFormat("{0:x2}", b);
        return hex.ToString();
    }



    public static byte[] FromHexString(string hex)
    {
        int NumberChars = hex.Length;
        byte[] bytes = new byte[NumberChars / 2];
        for (int i = 0; i < NumberChars; i += 2)
            bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
        return bytes;
    }
    public string DecryptText(string encryptedMessage)
    {
        System.Diagnostics.Debug.WriteLine("Entraaaaa a desencriptar");
        Console.WriteLine("Entraaaaa a desencriptara: " + encryptedMessage);
        var keyName = "projects/animated-joy-406200/locations/global/keyRings/keyring-Guru/cryptoKeys/key-Guru";
        var ciphertext = ByteString.CopyFrom(FromHexString(encryptedMessage));
        var decryptRequest = new DecryptRequest
        {
            Name = keyName,
            Ciphertext = ciphertext
        };

        var decryptResponse = _kmsClient.Decrypt(decryptRequest);
        Console.WriteLine("Desencriptado: " + decryptResponse.Plaintext.ToStringUtf8());
        return decryptResponse.Plaintext.ToStringUtf8();
    }

    public string ConvertListToJson(List<ContactoUsuario> contactosUsuarios)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
        };

        var jsonString = JsonSerializer.Serialize(contactosUsuarios, options);

        return jsonString;
    }



}