using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArbanFramework
{
    public class StringHelper
    {
        public static readonly string[] DigitSuffix =
        {
            "",
            "K" ,
            "M" ,
            "B" ,
            "T" ,
            "P" ,
            "AK",
            "AM",
            "AB",
            "AT",
            "AP",
            "BK",
            "BM",
            "BB",
            "BT",
            "BP",
            "CK",
            "CM",
            "CB",
            "CT",
            "CP",
        };

        private static double[] _digitSuffixValue = null;

        public static string ToDigitValue(double value)
        {
            if (_digitSuffixValue == null)
            {
                _digitSuffixValue = new double[DigitSuffix.Length];
                for (var i = 0; i < _digitSuffixValue.Length; ++i)
                {
                    _digitSuffixValue[i] = Math.Pow(10, i * 3);
                }
            }

            var suffixIndex = 1;
            for (suffixIndex = 1; suffixIndex < _digitSuffixValue.Length; ++suffixIndex)
            {
                if (_digitSuffixValue[suffixIndex] > value)
                    break;
            }

            var roundValue = value / _digitSuffixValue[suffixIndex - 1];
            roundValue = Math.Round(roundValue * 100) / 100;

            return roundValue.ToString() + DigitSuffix[suffixIndex - 1];
        }
    }
}
