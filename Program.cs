using System;

namespace Iban
{
    class Program
    {
        static void Main(string[] args)
        {
            string accountNumber = GenerateAccountNumber();
            var iban = GenerateIban("2927", "00882", "0", accountNumber);
            var result = ValidateIban(iban);
            if (result == true)
            {
                Console.WriteLine(iban);
            }
            else
            {
                Console.WriteLine("İban doğrulanmadı");
            }
            Console.ReadKey();
        }

        public static string GenerateIban(string countryCode, string bankIdent, string rezervArea, string accountNumber)
        {
            string ibanNumber;
            string ControlNumber = "00";
            string step1 = countryCode + ControlNumber + bankIdent + rezervArea + accountNumber;
            string step1Iban = bankIdent + rezervArea + accountNumber + countryCode + ControlNumber;
            var beforeControlNumber = Convert.ToDecimal(step1Iban) % 97;
            ControlNumber = Convert.ToString(98 - beforeControlNumber);
            var sayi = Math.Abs(Convert.ToDecimal(ControlNumber));
            int basamak = 0;
            while (sayi >= 1)
            {
                sayi /= 10;
                basamak++;
            }
            if (basamak <= 1)
            {
                var ibanStep3 = countryCode + "0" + ControlNumber + bankIdent + rezervArea + accountNumber;
                ibanNumber = ibanStep3.Replace(countryCode, "TR");
            }
            else
            {
                var ibanStep3 = countryCode + ControlNumber + bankIdent + rezervArea + accountNumber;
                ibanNumber = ibanStep3.Replace(countryCode, "TR");
            }
            return ibanNumber;
        }
        public static bool ValidateIban(string ibanNumber)
        {
            var validateStep1 = ibanNumber.Replace("TR", "2927");
            var validateStep2 = validateStep1.Remove(0, 6) + validateStep1.Substring(0, 6);
            var result = Convert.ToDecimal(validateStep2) % 97;
            if (result != 1 || validateStep2.Length != 28)
            {
                return false;
            }
            return true;

        }
        public static string GenerateAccountNumber()
        {

            return "0000000000000001";

        }
    }
}
