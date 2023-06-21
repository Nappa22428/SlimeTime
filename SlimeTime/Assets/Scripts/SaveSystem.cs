using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System;
using TMPro;

public class SimpleAES
{
    private const string initVector = "34er5t6y7u8jxcvb";
                                            
    private const int keysize = 256;

    public string EncryptString(string plainText, string passPhrase)
    {
        byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
        byte[] keyBytes = password.GetBytes(keysize / 8);
        RijndaelManaged symmetricKey = new RijndaelManaged();
        symmetricKey.Mode = CipherMode.CBC;
        ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
        MemoryStream memoryStream = new MemoryStream();
        CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
        cryptoStream.FlushFinalBlock();
        byte[] cipherTextBytes = memoryStream.ToArray();
        memoryStream.Close();
        cryptoStream.Close();
        return Convert.ToBase64String(cipherTextBytes);
    }

    public string DecryptString(string cipherText, string passPhase)
    {
        byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
        byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
        PasswordDeriveBytes password = new PasswordDeriveBytes(passPhase, null);
        byte[] keyBytes = password.GetBytes(keysize / 8);
        RijndaelManaged symmetricKey = new RijndaelManaged();
        symmetricKey.Mode = CipherMode.CBC;
        ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
        MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
        CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        byte[] plainTextBytes = new byte[cipherTextBytes.Length];
        int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
        memoryStream.Close();
        cryptoStream.Close();
        return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
    }


}

public class SaveSystem : MonoBehaviour
{
    //public TMP_InputField importValue;
    //public TMP_InputField exportValue;

    //public InputField encryptValue;
    //public InputField decryptValue;

    public static string json = "";

    protected static string encryptKey = "nkmca89jbfda2jkkopli0a2g";
    protected static string savePath = "/slime.save";
    protected static string savePathBackUP = "/slimebackup.save";

    public static int backUpCount = 0;

    public static void SavePlayer(PlayerData data)
    {
        var saveTo = backUpCount == 4 ? savePathBackUP : savePath;
        using (StreamWriter writer = new StreamWriter(Application.persistentDataPath + saveTo))
        {
            json = JsonUtility.ToJson(data);
            ConvertStringToBase64(writer, json);
            writer.Close();
            /*
            PlayerPrefs.SetString("OfflineTime", System.DateTime.Now.ToBinary().toString());
            data.OfflineProgressCheck = true;
            */
        }

        backUpCount = (backUpCount + 1) % 5;
    }

    public static string ConvertStringToBase64(StreamWriter writer, string x)
    {
        SimpleAES aes = new SimpleAES();
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(x);
        string stringTemp = Convert.ToBase64String(plainTextBytes);
        writer.WriteLine(aes.EncryptString(stringTemp, encryptKey));
        return stringTemp;
    }

    public static bool LoadSaveFile(ref PlayerData data, string path)
    {
        var success = false;
        try
        {
            using (StreamReader reader = new StreamReader(path))
            {
                json = ConvertBase64ToString(reader);
                data = JsonUtility.FromJson<PlayerData>(json);
                reader.Close();
                success = true;
            }
        }
#pragma warning disable
        catch (Exception ex)
        {
            Debug.Log("Load Save Failed");
            CreateFile();
        }
        return success;
#pragma warning enable
    }

    public static void CreateFile()
    {
        if(!File.Exists(Application.persistentDataPath + savePathBackUP))
        {
            File.CreateText(Application.persistentDataPath + savePathBackUP);
        }
        if(!File.Exists(Application.persistentDataPath + savePath))
        {
            File.CreateText(Application.persistentDataPath + savePath);
        }
    }

    public static void LoadPlayer(ref PlayerData data)
    {
        CreateFile();
        if (!LoadSaveFile(ref data, Application.persistentDataPath + savePath))
        {
            LoadSaveFile(ref data, Application.persistentDataPath + savePathBackUP);
        }
    }

    public static string ConvertBase64ToString(StreamReader reader)
    {
        SimpleAES aes = new SimpleAES();
        string stringConvert = reader.ReadLine();
        var base64EncodedBytes = Convert.FromBase64String(aes.DecryptString(stringConvert, encryptKey));
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }

    /*
    public void ImportPlayer2(IdleTutorialGame playerData)
    {
        using (StreamWriter writer = new StreamWriter(Application.persistentDataPath + savePath))
        {
            Debug.Log(importValue.text);
            writer.WriteLine(importValue.text);
            writer.Close();
            using (StreamReader reader = new StreamReader(Application.persistentDataPath + savePath))
            {
                json = ConvertBase64ToString(reader);
                playerData.d = JsonUtility.FromJson<PlayerData>(json);
                reader.Close();
            }
        }
        
    }

    public void ExportPlayer2()
    {
        using(StreamReader reader = new StreamReader(Application.persistentDataPath+savePath))
        {
            SimpleAES aes = new SimpleAES();
            string outputData = reader.ReadLine();
            reader.Close();
            exportValue.text = outputData;
            //decryptValue.text = aes.DecryptString(outputData, encryptKey);
            Debug.Log(outputData);
        }
    }

    //public void Encrypt()
    //{
        //SimpleAES aes = new SimpleAES();
        //String temp = encryptValue.text;
        //encryptValue.text = aes.EncryptString(temp, encryptKey);
    //}

    
    public void ClearFields()
    {
        exportValue.text = "";
        importValue.text = "";
    }
    */
}
