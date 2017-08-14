﻿using DocumentFormat.Pdf.IO;
using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DocumentFormat.Pdf.Objects
{
    /// <summary>
    /// Represents a Pdf Name Object
    /// </summary>
    public class NameObject : PdfObject
    {
        /// <summary>
        /// NameObject's start token
        /// </summary>
        public const char StartToken = '/';

        private string value;

        /// <summary>
        /// Instanciates a new NameObject
        /// </summary>
        /// <param name="value">The object's value</param>
        public NameObject(string value)
        {
            this.value = value;
        }

        /// <summary>
        /// Gets the object's value
        /// </summary>
        public string Value => value;

        /// <summary>
        /// Creates a Name object from PdfReader.
        /// Read stream must start with '/' delimiter.
        /// </summary>
        /// <param name="reader">The <see cref="PdfReader"/> to use</param>
        /// <returns>Created NameObject</returns>
        public static NameObject FromReader(PdfReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            if (reader.Read() != StartToken)
                throw new FormatException("A name object was expected.");

            var readChars = reader.ReadWhile(c => !Chars.IsDelimiterOrWhiteSpace(c));

            if(readChars.Length == 0)
            {
                return new NameObject("");
            }

            var sb = new StringBuilder();
            for (var i = 0; i < readChars.Length; i++)
            {
                if(readChars[i] == '#')
                {
                    if(i < readChars.Length - 2)
                    {
                        var hex = new char[] { readChars[i + 1], readChars[i + 2] };
                        sb.Append((char)byte.Parse(new string(hex), NumberStyles.HexNumber));
                        i += 2;
                    }
                }
                else
                {
                    sb.Append(readChars[i]);
                }
            }

            return new NameObject(sb.ToString());
        }
    }
}