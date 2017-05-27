using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System;
using System.Text;
using UnityEngine;

public static class GeekyMonkeyCompressionExtension
{
    /// <summary>
    /// Converts UTF8 String to gzip-compressed base64 string
    /// </summary>
    /// <param name="toCompress"></param>
    /// <returns></returns>
    public static string ZipCompress(string toCompress)
    {
        using (MemoryStream output = new MemoryStream())
        {
            using (GZipStream compression = new GZipStream(output, CompressionMode.Compress))
            {
                using (StreamWriter writer = new StreamWriter(compression))
                {
                    writer.Write(toCompress);
                }
            }
            return Convert.ToBase64String(output.ToArray());
        }
    }

    /// <summary>
    /// Converts Gzip-compressed base64 string to UTF8 string
    /// </summary>
    /// <param name="toDecompress"></param>
    /// <returns></returns>
    public static string ZipDecompress(string toDecompress)
    {
        using (MemoryStream input = new MemoryStream(Convert.FromBase64String(toDecompress)))
        {
            using (GZipStream compression = new GZipStream(input, CompressionMode.Decompress))
            {
                using (MemoryStream output = new MemoryStream())
                {
                    compression.CopyTo(output);
                    return Encoding.UTF8.GetString(output.ToArray());
                }
            }
        }
    }

    public static void CopyTo(this Stream input, Stream output)
    {
        byte[] buffer = new byte[16 * 1024]; // Fairly arbitrary size
        int bytesRead;

        while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
        {
            output.Write(buffer, 0, bytesRead);
        }
    }
}
