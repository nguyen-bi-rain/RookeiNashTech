using System;
using System.IO;

namespace Helper
{
    public static class FileHelper
    {
        private static readonly string LogFilePath = Path.Combine(Directory.GetCurrentDirectory(), "log.txt");

        public static async Task WriteLogAsync(string message)
        {
            try
            {
                await File.AppendAllTextAsync(LogFilePath,message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write log: {ex.Message}");
            }
        }
    }
}