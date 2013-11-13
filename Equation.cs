using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DifferentialSolver
{
    class Equation
    {
        private string EquationString; //string with variables in brakets to be substituted, eg {A}^2-{B}*{C}+3
        private List<IVal> Variables = new List<IVal>();
        public Equation(string EquationString)
        {
            this.EquationString = EquationString;
        }
        public void addVariables(List<Variable> Variables)
        {
            List<IVal> variablesToAdd = new List<IVal>();
            foreach (IVal v in Variables)
            {
                if (EquationString.Contains(v.getName()))
                {
                    EquationString = EquationString.Replace(v.getName(), ("{" + v.getName() + "}"));
                    variablesToAdd.Add(v);
                }
            }
            this.Variables.AddRange(variablesToAdd);
        }
        public void addVariables(List<StaticVariable> Variables)
        {
            List<IVal> variablesToAdd = new List<IVal>();
            foreach (IVal v in Variables)
            {
                if (EquationString.Contains(v.getName()))
                {
                    EquationString = EquationString.Replace(v.getName(), ("{" + v.getName() + "}"));
                    variablesToAdd.Add(v);
                }
            }
            this.Variables.AddRange(variablesToAdd);
        }
        public double Val(double time)
        {
            string NumericalString = EquationString;
            foreach (Variable v in Variables)
	        {
                NumericalString = NumericalString.Replace("{" + v.getName()+ "}", v.Val(time).ToString());
	        }
            return Evaluate(NumericalString);
        }
        private static char[] NumericalCharacters = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.', '(', ')' };
        private string InsertParens(string s)
        {
            for (int i = s.Length - 1; i > 0; i--)
            {
                if (s[i] == '^')
                {
                    int Right = i + 1;
                    int Left = i - 1;

                    int ParenCount = 0;
                    while (Right < s.Length && (NumericalCharacters.Contains(s[Right]) || ParenCount != 0))
                    {
                        if (s[Right] == '(')
                            ParenCount++;
                        else if (s[Right] == ')')
                            ParenCount--;
                        Right++;
                    }
                    while (Left > -1 && (NumericalCharacters.Contains(s[Left]) || ParenCount != 0))
                    {
                        if (s[Left] == '(')
                            ParenCount++;
                        else if (s[Left] == ')')
                            ParenCount--;
                        Left--;
                    }
                    Left++;
                    s = s.Insert(Right, ")");
                    s = s.Insert(Left, "(");
                }
            }
            for (int i = s.Length - 1; i > 0; i--)
            {
                if (s[i] == '*')
                {
                    int Right = i + 1;
                    int Left = i - 1;

                    int ParenCount = 0;
                    while (Right < s.Length && (NumericalCharacters.Contains(s[Right]) || ParenCount != 0))
                    {
                        if (s[Right] == '(')
                            ParenCount++;
                        else if (s[Right] == ')')
                            ParenCount--;
                        Right++;
                    }
                    while (Left > -1 && (NumericalCharacters.Contains(s[Left]) || ParenCount != 0))
                    {
                        if (s[Left] == '(')
                            ParenCount++;
                        else if (s[Left] == ')')
                            ParenCount--;
                        Left--;
                    }
                    Left++;
                    s = s.Insert(Right, ")");
                    s = s.Insert(Left, "(");
                }
            }
            for (int i = s.Length - 1; i > 0; i--)
            {
                if (s[i] == '/')
                {
                    int Right = i + 1;
                    int Left = i - 1;

                    int ParenCount = 0;
                    while (Right < s.Length && (NumericalCharacters.Contains(s[Right]) || ParenCount != 0))
                    {
                        if (s[Right] == '(')
                            ParenCount++;
                        else if (s[Right] == ')')
                            ParenCount--;
                        Right++;
                    }
                    while (Left > -1 && (NumericalCharacters.Contains(s[Left]) || ParenCount != 0))
                    {
                        if (s[Left] == '(')
                            ParenCount++;
                        else if (s[Left] == ')')
                            ParenCount--;
                        Left--;
                    }
                    Left++;
                    s = s.Insert(Right, ")");
                    s = s.Insert(Left, "(");
                }
            }
            for (int i = s.Length - 1; i > 0; i--)
            {
                if (s[i] == '+')
                {
                    int Right = i + 1;
                    int Left = i - 1;

                    int ParenCount = 0;
                    while (Right < s.Length && (NumericalCharacters.Contains(s[Right]) || ParenCount != 0))
                    {
                        if (s[Right] == '(')
                            ParenCount++;
                        else if (s[Right] == ')')
                            ParenCount--;
                        Right++;
                    }
                    while (Left > -1 && (NumericalCharacters.Contains(s[Left]) || ParenCount != 0))
                    {
                        if (s[Left] == '(')
                            ParenCount++;
                        else if (s[Left] == ')')
                            ParenCount--;
                        Left--;
                    }
                    Left++;
                    s = s.Insert(Right, ")");
                    s = s.Insert(Left, "(");
                }
            }
            for (int i = s.Length - 1; i > 0; i--)
            {
                if (s[i] == '-')
                {
                    int Right = i + 1;
                    int Left = i - 1;

                    int ParenCount = 0;
                    while (Right < s.Length && (NumericalCharacters.Contains(s[Right]) || ParenCount != 0))
                    {
                        if (s[Right] == '(')
                            ParenCount++;
                        else if (s[Right] == ')')
                            ParenCount--;
                        Right++;
                    }
                    while (Left > -1 && (NumericalCharacters.Contains(s[Left]) || ParenCount != 0))
                    {
                        if (s[Left] == '(')
                            ParenCount++;
                        else if (s[Left] == ')')
                            ParenCount--;
                        Left--;
                    }
                    Left++;
                    s = s.Insert(Right, ")");
                    s = s.Insert(Left, "(");
                }
            }
            return s;
        }
        private double Evaluate(string NumericalString) //returns the value of an expression such as 4^2-6*2+3
        {
            int LeftParen = 0;
            bool FoundParen = false;
            while (LeftParen < NumericalString.Length)
            {
                if(NumericalString[LeftParen] == '(')
                {
                    FoundParen = true;
                    break;
                }
                LeftParen++;
            }
            while(FoundParen)
            {
                int RightParen = LeftParen;
                int parenCount = 0;
                while(parenCount > -1)
                {
                    RightParen++;
                    if (NumericalString[RightParen] == '(')
                        parenCount++;
                    else if (NumericalString[RightParen] == ')')
                        parenCount--;
                }
                string subString = NumericalString.Substring(LeftParen + 1, RightParen - LeftParen - 1);
                string subValue = Evaluate(subString).ToString();
                NumericalString = NumericalString.Remove(LeftParen, RightParen - LeftParen + 1);
                NumericalString = NumericalString.Insert(LeftParen, subValue);


                LeftParen = 0;
                FoundParen = false;
                while (LeftParen < NumericalString.Length)
                {
                    if (NumericalString[LeftParen] == '(')
                    {
                        FoundParen = true;
                        break;
                    }
                    LeftParen++;
                }
            }

            int operationLocation = -1;
            for (int i = 0; i < NumericalString.Length; i++)
            {
                if (!NumericalCharacters.Contains(NumericalString[i]))
                {
                    operationLocation = i;
                    string firstString = NumericalString.Substring(0, operationLocation);
                    double a = Double.Parse(firstString);
                    string secondstring = NumericalString.Substring(operationLocation + 1);
                    double b = Double.Parse(secondstring);
                    char operation = NumericalString[operationLocation];
                    if (operation == '^')
                        return Math.Pow(a, b);
                    if (operation == '*')
                        return a * b;
                    if (operation == '/')
                        return a / b;
                    if (operation == '+')
                        return a + b;
                    if (operation == '-')
                        return a - b;
                }
            }
            return Double.Parse(NumericalString);
        }
    }
}
