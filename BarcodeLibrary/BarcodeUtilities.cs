﻿using BarcodeLibrary;
using System.Diagnostics;
using System.Text;

namespace BarcodeLibrary
{
    // переименовать тоже
    internal static class BarcodeUtilities
    {
        public static string GenerateBarcode(string inputText)
        {
            StringBuilder barcodeBinary = new(_Frame);
            IList<int> patternList = [];
            bool isNumericMode = false;

            if (isNumericMode && !IsNumericSequence(inputText, 0, 2) || !isNumericMode && IsNumericSequence(inputText, 0, 4))
            {
                isNumericMode = !isNumericMode;
                AppendBinaryCode(barcodeBinary, patternList, _PatternStartNumbers);
            }
            else
            {
                AppendBinaryCode(barcodeBinary, patternList, _PatternStartText);
            }

            for (int index = 0; index < inputText.Length;/**/)
            {
                SelectModeAndTransform(inputText, barcodeBinary, patternList, ref isNumericMode, ref index);
            }

            AppendBinaryCode(barcodeBinary, patternList, CalculateControlPattern(patternList));
            AppendBinaryCode(barcodeBinary, patternList, _PatternStop);

            barcodeBinary.Append(_Frame);

            StringBuilder outputBarcode = new StringBuilder();

            foreach (var item in barcodeBinary.ToString().GetBinarySubstrings(2))
            {
                outputBarcode.Append(BinaryToBarcodeSymbol(item));
            }
            // dshfdybdfybt gj wtynhe 
            var finalBarcode = new StringBuilder();
            string rawBarcode = outputBarcode.ToString();
            string borderPadding = "\n".PadLeft(rawBarcode.Length + 1, AllowedBars[0]);

            finalBarcode.Append(borderPadding);

            for (int i = 0; i < _BarcodeHeight; ++i)
            {
                finalBarcode.Append(rawBarcode).Append('\n');
            }
            finalBarcode.Append(borderPadding);
            
            return finalBarcode.ToString();
        }

        private static void SelectModeAndTransform(string inputText, StringBuilder binaryCode, IList<int> patternList, ref bool isNumber, ref int index)
        {
            if ((isNumber && !IsNumericSequence(inputText, index, 2)) || (!isNumber && IsNumericSequence(inputText, index, 4)))
            {
                isNumber = !isNumber;
                AppendBinaryCode(binaryCode, patternList, isNumber ? _SwitchModeToNumbers : _SwitchModeToText);
            }
            Transform(ref index, patternList, isNumber, inputText, binaryCode);
        }

        private static void Transform(ref int index, IList<int> patternList, bool isNumber, string inputText, StringBuilder binaryCode)
        {
            if (isNumber)
            {
                AppendBinaryCode(binaryCode, patternList, Array.IndexOf(AllowedPairsOfNumberSymbols, inputText.Substring(index, 2)));
                index += 2;
            }
            else
            {
                AppendBinaryCode(binaryCode, patternList, Array.IndexOf(AllowedTextSymbols, inputText.Substring(index, 1)));
                ++index;
            }
        }


        private static void AppendBinaryCode(StringBuilder binaryCode, IList<int> patternList, int indexOfSubstring)
        {
            binaryCode.Append(AllowedBinaryPatterns[indexOfSubstring]);
            patternList.Add(indexOfSubstring);
        }

        private static bool IsNumericSequence(string inputText, int startIndex, int length)
        {
            IEnumerable<char> chars = inputText.Skip(startIndex).Take(length);
            return chars.Count() == length && chars.All(x => char.IsDigit(x));
        }

        private static int CalculateControlPattern(IList<int> patternList)
        {
            var sum = patternList[0];

            for (var i = 1; i < patternList.Count; ++i)
            {
                sum += i * patternList[i];
            }

            sum %= 103;

            return sum;
        }

        private static char BinaryToBarcodeSymbol(string binaryCode)
            => AllowedBars[Convert.ToInt32(binaryCode, 2)];

        private static IEnumerable<string> GetBinarySubstrings(this string a, int b)
        {
            return Enumerable.Range(0, a.Length / b).Select(i => a.Substring(i * b, b));
        }

        public static int BarcodeHeight => _BarcodeHeight;
        private const int _BarcodeHeight = 10;
        private const string _Frame = "0000";
        private const int _SwitchModeToNumbers = 99;
        private const int _SwitchModeToText = 100;
        private const int _PatternStartText = 104;
        private const int _PatternStartNumbers = 105;
        private const int _PatternStop = 108;
        
        private static readonly char[] AllowedBars = { '█', '▌', '▐', ' ' };

        private static readonly string[] AllowedBinaryPatterns = {
        "11011001100", "11001101100", "11001100110", "10010011000", "10010001100",
        "10001001100", "10011001000", "10011000100", "10001100100", "11001001000",
        "11001000100", "11000100100", "10110011100", "10011011100", "10011001110",
        "10111001100", "10011101100", "10011100110", "11001110010", "11001011100",
        "11001001110", "11011100100", "11001110100", "11101101110", "11101001100",
        "11100101100", "11100100110", "11101100100", "11100110100", "11100110010",
        "11011011000", "11011000110", "11000110110", "10100011000", "10001011000",
        "10001000110", "10110001000", "10001101000", "10001100010", "11010001000",
        "11000101000", "11000100010", "10110111000", "10110001110", "10001101110",
        "10111011000", "10111000110", "10001110110", "11101110110", "11010001110",
        "11000101110", "11011101000", "11011100010", "11011101110", "11101011000",
        "11101000110", "11100010110", "11101101000", "11101100010", "11100011010",
        "11101111010", "11001000010", "11110001010", "10100110000", "10100001100",
        "10010110000", "10010000110", "10000101100", "10000100110", "10110010000",
        "10110000100", "10011010000", "10011000010", "10000110100", "10000110010",
        "11000010010", "11001010000", "11110111010", "11000010100", "10001111010",
        "10100111100", "10010111100", "10010011110", "10111100100", "10011110100",
        "10011110010", "11110100100", "11110010100", "11110010010", "11011011110",
        "11011110110", "11110110110", "10101111000", "10100011110", "10001011110",
        "10111101000", "10111100010", "11110101000", "11110100010", "10111011110",
        // 100+
        "10111101110", "11101011110", "11110101110", "11010000100", "11010010000",
        "11010011100", "11000111010", "11010111000", "1100011101011"};

        private static readonly string[] AllowedTextSymbols = {
        " ","!","\"","#","$","%","&","'","(",")",
        "*","+",",","-",".","/","0","1","2","3",
        "4","5","6","7","8","9",":",";","<","=",
        ">","?","@","A","B","C","D","E","F","G",
        "H","I","J","K","L","M","N","O","P","Q",
        "R","S","T","U","V","W","X","Y","Z","[",
        "\\","]","^","_","`","a","b","c","d","e",
        "f","g","h","i","j","k","l","m","n","o",
        "p","q","r","s","t","u","v","w","x","y",
        "z","{","|","|","~"
    };

        private static readonly string[] AllowedPairsOfNumberSymbols = {
        "00","01","02","03","04","05","06","07","08","09",
        "10","11","12","13","14","15","16","17","18","19",
        "20","21","22","23","24","25","26","27","28","29",
        "30","31","32","33","34","35","36","37","38","39",
        "40","41","42","43","44","45","46","47","48","49",
        "50","51","52","53","54","55","56","57","58","59",
        "60","61","62","63","64","65","66","67","68","69",
        "70","71","72","73","74","75","76","77","78","79",
        "80","81","82","83","84","85","86","87","88","89",
        "90","91","92","93","94","95","96","97","98","99"
    };

    }
}