//  <copyright file="FormatStringToken.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
namespace CodeZero.Text.Formatting
{
    internal class FormatStringToken
    {
        public string Text { get; private set; }

        public FormatStringTokenType Type { get; private set; }

        public FormatStringToken(string text, FormatStringTokenType type)
        {
            Text = text;
            Type = type;
        }
    }
}