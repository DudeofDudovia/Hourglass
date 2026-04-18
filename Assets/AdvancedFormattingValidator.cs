using System;
using UnityEngine;
namespace TMPro
{
    [Serializable]
    [CreateAssetMenu(fileName = "AdvancedFormattingValidator", menuName = "ScriptableObjects/AdvancedFormattingValidator", order = 1)]
    public class AdvancedFormattingValidator : TMP_InputValidator
    {
        public string[] AllowedVals = new string[10];
        public override char Validate(ref string text, ref int pos, char ch)
        {
            if ((ch >= '0' && ch <= '9') || ch == '.' || ch == 'S' || ch == 's' || ch == 'M' || ch == 'm' || ch == 'H' || ch == 'h' || ch == '-' || ch == '+' || ch == ' ' || ch == ':')
            {
                text = text.Insert(pos, ch.ToString());
                pos++;
                return ch;
            }
            //return '\0';
            return (char)0;
        }
    }
}