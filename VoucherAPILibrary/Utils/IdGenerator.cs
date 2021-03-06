﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherAPILibrary.Utils
{
    public class IdGenerator
    {
        private static Random random = new Random();
        public static string RandomGen(int Length)
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            return new string(Enumerable.Repeat(characters, Length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
