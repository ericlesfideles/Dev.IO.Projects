using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevIO.Bussiness.Models.Validations.Documents
{
    public class ValidateCPF
    {
        public const int cpfLength = 11;

        public static bool Validate(string cpf)
        {
            var cpfNumber = Utils.OnlyNumbers(cpf);

            if (!ValidateLength(cpfNumber)) return false;
            return !HasRepeatedDigit(cpfNumber) && HasValidDigits(cpfNumber);
        }

        private static bool ValidateLength(string value)
        {
            return value.Length == cpfLength;
        }

        private static bool HasRepeatedDigit(string cpf)
        {
            string[] invalidNumbers =
            {
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "99999999999"
            };
            return invalidNumbers.Contains(cpf);
        }

        private static bool HasValidDigits(string value)
        {
            var number = value.Substring(0, cpfLength - 2);
            var verigyingDigit = new VerifyingDigit(number)
                .WithMultiplier(2, 11)
                .Replacement("0", 10, 11);
            var firstDigit = verigyingDigit.CalculateDigit();
            verigyingDigit.AddDigit(firstDigit);
            var secondDigit = verigyingDigit.CalculateDigit();

            return string.Concat(firstDigit, secondDigit) == value.Substring(cpfLength - 2, 2);
        }
    }

    public class ValidateCNPJ
    {
        public const int cnpjLength = 14;

        public static bool Validate(string cpnj)
        {
            var cnpjNumbers = Utils.OnlyNumbers(cpnj);

            if (!HasValidLength(cnpjNumbers)) return false;
            return !HasRepeatedDigit(cnpjNumbers) && HasValidDigits(cnpjNumbers);
        }

        private static bool HasValidLength(string value)
        {
            return value.Length == cnpjLength;
        }

        private static bool HasRepeatedDigit(string value)
        {
            string[] invalidNumbers =
            {
                "00000000000000",
                "11111111111111",
                "22222222222222",
                "33333333333333",
                "44444444444444",
                "55555555555555",
                "66666666666666",
                "77777777777777",
                "88888888888888",
                "99999999999999"
            };
            return invalidNumbers.Contains(value);
        }

        private static bool HasValidDigits(string value)
        {
            var number = value.Substring(0, cnpjLength - 2);

            var verifyingDigit = new VerifyingDigit(number)
                .WithMultiplier(2, 9)
                .Replacement("0", 10, 11);
            var firstDigit = verifyingDigit.CalculateDigit();
            verifyingDigit.AddDigit(firstDigit);
            var secondDigit = verifyingDigit.CalculateDigit();

            return string.Concat(firstDigit, secondDigit) == value.Substring(cnpjLength - 2, 2);
        }
    }

    public class VerifyingDigit
    {
        private string _Number;
        private const int Module = 11;
        private readonly List<int> _Multipliers = new List<int> { 2, 3, 4, 5, 6, 7, 8, 9 };
        private readonly IDictionary<int, string> _Replacement = new Dictionary<int, string>();
        private bool _ModuleComplement = true;

        public VerifyingDigit(string number)
        {
            _Number = number;
        }

        public VerifyingDigit WithMultiplier(int firtMultiplier, int lastMultiplier)
        {
            _Multipliers.Clear();
            for (var i = firtMultiplier; i <= lastMultiplier; i++)
                _Multipliers.Add(i);

            return this;
        }

        public VerifyingDigit Replacement(string substitute, params int[] digits)
        {
            foreach (var i in digits)
            {
                _Replacement[i] = substitute;
            }
            return this;
        }

        public void AddDigit(string digit)
        {
            _Number = string.Concat(_Number, digit);
        }

        public string CalculateDigit()
        {
            return !(_Number.Length > 0) ? "" : GetDigitSum();
        }

        private string GetDigitSum()
        {
            var sum = 0;
            for (int i = _Number.Length - 1, m = 0; i >= 0; i--)
            {
                var produt = (int)char.GetNumericValue(_Number[i]) * _Multipliers[m];
                sum += produt;

                if (++m >= _Multipliers.Count) m = 0;
            }

            var mod = (sum % Module);
            var result = _ModuleComplement ? Module - mod : mod;

            return _Replacement.ContainsKey(result) ? _Replacement[result] : result.ToString();
        }
    }

    public class Utils
    {
        public static string OnlyNumbers(string value)
        {
            var onlyNumber = "";
            foreach (var s in value)
            {
                if (char.IsDigit(s))
                {
                    onlyNumber += s;
                }
            }
            return onlyNumber.Trim();
        }
    }
}
