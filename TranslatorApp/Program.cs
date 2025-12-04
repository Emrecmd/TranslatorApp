using System;
using System.Threading.Tasks;
using Google.Cloud.Translation.V2;

namespace GoogleTranslatorApp
{
    class Program
    {
        private const string ApiKey = "";

        static void Main(string[] args)
        {
            TranslateText("Hello, I am using the official Google Translate API.", "tr").Wait();
            Console.WriteLine("\nÇıkmak için Enter'a basın...");
            Console.ReadLine();
        }

        public static async Task TranslateText(string text, string targetLanguage)
        {
            Environment.SetEnvironmentVariable("GOOGLE_API_KEY", ApiKey);

            Console.WriteLine("=== Google Cloud Translation API ===");
            Console.WriteLine($"Çevrilecek Metin: {text}");

            try
            {
                TranslationClient client = await TranslationClient.CreateAsync();

                var response = await client.TranslateTextAsync(
                    text: text,
                    targetLanguage: targetLanguage
                );

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n>>> SONUÇ ({targetLanguage.ToUpper()}): {response.TranslatedText}");
                Console.WriteLine($">>> Kaynak Dil: {response.DetectedSourceLanguage}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"HATA: Google API isteği başarısız oldu. Anahtar, Faturalandırma veya Ağ bağlantınızı kontrol edin.");
                Console.WriteLine($"Detay: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}