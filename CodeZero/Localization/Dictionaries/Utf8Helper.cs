//  <copyright file="Utf8Helper.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.IO;
using System.Text;
using CodeZero.IO.Extensions;

namespace CodeZero.Localization.Dictionaries
{
    internal static class Utf8Helper
    {
        public static string ReadStringFromStream(Stream stream)
        {
            var bytes = stream.GetAllBytes();
            var skipCount = HasBom(bytes) ? 3 : 0;
            return Encoding.UTF8.GetString(bytes, skipCount, bytes.Length - skipCount);
        }

        private static bool HasBom(byte[] bytes)
        {
            if (bytes.Length < 3)
            {
                return false;
            }

            if (!(bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF))
            {
                return false;
            }

            return true;
        }
    }
}
