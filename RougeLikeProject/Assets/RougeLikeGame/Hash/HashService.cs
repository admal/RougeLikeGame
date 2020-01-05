using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;

namespace Hash
{
    public class HashService
    {
        private static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = SHA256.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        private string GetHashString(string inputString)
        {
            var sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }

        public string InitWithRandomSeed()
        {
            //Get random number
            var randomNumber = Random.Range(0, 100000);

            //Hash random number
            var hash = GetHashString(randomNumber.ToString());

            //Init Unity.Random with hash
            InitWithSeed(hash);
            return hash;
        }

        public void InitWithSeed(string hash)
        {
            Random.InitState(hash.GetHashCode());
        }
    }
}