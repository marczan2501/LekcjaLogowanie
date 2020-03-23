using System.Security.Cryptography;
using System.Text;

namespace LogowanieWPFNazwisko
{
    class Szyfrowanie
    {
        public string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Konwertuj ciąg wejściowy na tablicę bajtów i oblicz skrót.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            // Utwórz nowy Stringbuilder aby zebrać bajty
            // i utwórz string.
            StringBuilder sBuilder = new StringBuilder();
            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}