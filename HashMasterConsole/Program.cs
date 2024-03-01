using Konscious.Security.Cryptography;
using System;
using System.Text;

class Program
{
    static void Main()
    {
        try {
            // Parola
            string parola = "tatilyokCalis2000";
            // Salt oluştur
            byte[] salt = new byte[16];
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }


            // Salt'ı güvenli bir şekilde sakla (örneğin, veritabanında)
            string saklananSalt = Convert.ToBase64String(salt);

            // Parolayı UTF-8 encoding ile byte dizisine dönüştür
            byte[] parolaBytes = Encoding.UTF8.GetBytes(parola);


            //kullanılan salt saklanmalı

            // Argon2 hashleme işlemi
            var hasher = new Argon2id(parolaBytes)
            {
                Salt = salt,
                DegreeOfParallelism = 8,
                MemorySize = 65536,
                Iterations = 4
            };

            // Hash değerini al
            byte[] hashValue = hasher.GetBytes(32);

            // Hash değerini ekrana yazdır

            Console.WriteLine($"Hash Değeri: {Convert.ToBase64String(hashValue)}");
            Console.WriteLine($"Saklanan Salt: {saklananSalt}");
        }

        catch ( Exception ex )
        {
            Console.WriteLine($"Bir hata oluştu: {ex.Message}");
        }

        } 

     
}


