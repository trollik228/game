using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class Cryptography : MonoBehaviour
{
    private readonly static string PATH_TO_DIRECTORY = 
        Application.persistentDataPath + $"\\Levels\\";


    /// <summary>
    /// Шифровка файла и запись зашифрованой информации в ТОТ ЖЕ файл
    /// </summary>
    /// <param name="pathOrFile"></param>
    /// <param name="str"></param>
    /// <param name="isFullPath"></param>
    public static void FileEncryption(string pathOrFile,string str, bool isFullPath)
    {
        string path = isFullPath ? pathOrFile : PATH_TO_DIRECTORY + pathOrFile;

        try
        {
            using (FileStream fileStream = new(path, FileMode.Open))
            {
                using (Aes aes = Aes.Create())
                {
                    byte[] key =
                    {
                0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
                    };
                    aes.Key = key;

                    byte[] iv = aes.IV;
                    fileStream.Write(iv, 0, iv.Length);

                    using (CryptoStream cryptoStream = new(
                        fileStream,
                        aes.CreateEncryptor(),
                        CryptoStreamMode.Write))
                    {
                        using (StreamWriter encryptWriter = new(cryptoStream))
                        {
                            encryptWriter.Write(str);
                        }
                    }
                }
            }
        }
        catch { }

    }

    /// <summary>
    /// Душифровка файла и возврат строки(если последний параметр = false,
    /// иначе создаться новый файл)
    /// </summary>
    /// <param name="pathOrFile"></param>
    /// <param name="isFullPath"></param>
    /// <returns></returns>
    public static string FileDecryption(string pathOrFile,bool isFullPath, bool isReturnString = false)
    {
        string path = PATH_TO_DIRECTORY;
        if (!isFullPath) path += pathOrFile;
        else path = pathOrFile;
        try
        {
            using (FileStream fileStream = new(path, FileMode.Open))
            {
                using (Aes aes = Aes.Create())
                {
                    byte[] iv = new byte[aes.IV.Length];
                    int numBytesToRead = aes.IV.Length;
                    int numBytesRead = 0;
                    while (numBytesToRead > 0)
                    {
                        int n = fileStream.Read(iv, numBytesRead, numBytesToRead);
                        if (n == 0) break;

                        numBytesRead += n;
                        numBytesToRead -= n;
                    }

                    byte[] key =
                    {
                0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
                    };
                    string decryptedMessage;
                    using (CryptoStream cryptoStream = new(fileStream,aes.CreateDecryptor(key, iv),
                       CryptoStreamMode.Read))
                    {
                        using (StreamReader decryptReader = new(cryptoStream))
                        {
                            decryptedMessage =  decryptReader.ReadToEnd();
                        }
                    }
                    return isReturnString ? 
                        SaveLoadSystem.SaveJsonString(decryptedMessage) : 
                        decryptedMessage;

                }
            }
        }
        catch { }
        return "ERROR";
    }
}
