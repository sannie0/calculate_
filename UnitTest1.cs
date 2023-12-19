namespace MyPars
{
    /*public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }*/

    using FluentAssertions;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;

    namespace Calculation_Parser
    {
        /*public class Calculator_Test
        {
            Calculator _calculator;
            [SetUp]
            public void Setup()
            {
                _calculator = new Calculator();
            }

            [Test]
            public void Return_Result_IfArithmeticExpression1()
            {
                string test = "1,2+1,2";
                Assert.That(_calculator.StartParse(test), Is.EqualTo("2,4"));
            }

            [Test]
            public void Return_Result_IfArithmeticExpression2()
            {
                string test = "(1+8)/3";
                Assert.That(_calculator.StartParse(test), Is.EqualTo("3"));
            }

            [Test]
            public void Return_Result_IfArithmeticExpression3()
            {
                string test = "((1+8)/3)-2*3";
                Assert.That(_calculator.StartParse(test), Is.EqualTo("-3"));
            }

            [Test]
            public void Return_Result_IfArithmeticExpression4()
            {
                string test = "((1+8)/3)-2*3*(7-2)";
                Assert.That(_calculator.StartParse(test), Is.EqualTo("-27"));
            }

            [Test]
            public void Return_Result_IfArithmeticExpression5()
            {
                string test = "((17333-333)*(500-499))/1000";
                Assert.That(_calculator.StartParse(test), Is.EqualTo("17"));
            }

            [Test]
            public void Return_Result_IfArithmeticExpression6()
            {
                string test = "(((17333-333)*(500-499))/1000)*(((17333-333)*(500-499))/1000)*(((17333-333)*(500-499))/1000)";
                Assert.That(_calculator.StartParse(test), Is.EqualTo("4913"));
            }

            [Test]
            public void Return_Result_IfArithmeticExpression7()
            {
                string test = "100000*10000000000000";
                Assert.That(_calculator.StartParse(test), Is.EqualTo("1E+18"));
            }

            [Test]
            public void Return_Result_IfArithmeticExpression9()
            {
                string test = "(1,2+1,2)*(1,2+1,2)*(1,2+1,2)";
                Assert.That(_calculator.StartParse(test), Is.EqualTo("13,824"));
            }

            [Test]
            public void Return_Result_IfArithmeticExpression10()
            {
                string test = "1+2+3+4+5+6+7+3";
                Assert.That(_calculator.StartParse(test), Is.EqualTo("31"));
            }

            [Test]
            public void Return_Result_IfArithmeticExpression12()
            {
                string test = "(-4-4)*(-5)";
                Assert.That(_calculator.StartParse(test), Is.EqualTo("40"));
            }
            [Test]
            public void Return_Result_IfArithmeticExpression13()
            {
                string test = "(-4-4)*(-4-(-7)+(-8--5))";
                Assert.That(_calculator.StartParse(test), Is.EqualTo("-0"));
            }

            [Test]
            public void Return_Result_IfArithmeticExpression14()
            {
                string test = "(-4-4)*(-4-(-7)+(-8--5))+(-7)";
                Assert.That(_calculator.StartParse(test), Is.EqualTo("-7"));
            }

            [Test]
            public void Return_Result_IfArithmeticExpression15()
            {
                string test = "(-4-a)*(-5)";
                Assert.That(_calculator.StartParse(test), Is.EqualTo("Error"));
            }

            [Test]
            public void Return_Result_IfArithmeticExpression16()
            {
                string test = "0,4-0,4";
                Assert.That(_calculator.StartParse(test), Is.EqualTo("0"));
            }

            [Test]
            public void Return_Result_IfArithmeticExpression17()
            {
                string test = "0.1+0.1";
                Assert.That(_calculator.StartParse(test), Is.EqualTo("0,2"));
            }

            [Test]
            public void Return_Result_IfArithmeticExpression18()
            {
                string test = ".1+0.1";
                Assert.That(_calculator.StartParse(test), Is.EqualTo("0,2"));
            }

            [Test]
            public void Return_Result_IfArithmeticExpression19()
            {
                string test = ".a1a+0.1";
                Assert.That(_calculator.StartParse(test), Is.EqualTo("0,2"));
            }

            [Test]
            public void Return_Result_IfArithmeticExpression20()
            {
                string test = ".12,1,565,145";
                Assert.That(_calculator.StartParse(test), Is.EqualTo("0,121565145"));
            }

            [Test]
            public void Return_Result_IfArithmeticExpression21()
            {
                string test = "12,3-3,2";
                Assert.That(_calculator.StartParse(test), Is.EqualTo("9,1"));
            }
            [Test]
            public void Return_Result_IfArithmeticExpression22()
            {
                string test = "6/0";
                Assert.That(_calculator.StartParse(test), Is.EqualTo("∞"));
            }

            [Test]
            public void Return_Result_IfArithmeticExpression23()
            {
                string test = "/6";
                Assert.That(_calculator.StartParse(test), Is.EqualTo("6"));
            }
            [Test]
            public void Return_Result_IfArithmeticExpression24()
            {
                string test = "-6";
                Assert.That(_calculator.StartParse(test), Is.EqualTo("-6"));
            }

            [Test]
            public void Return_Result_IfArithmeticExpression25()
            {
                string test = "0,,,,,,3";
                Assert.That(_calculator.StartParse(test), Is.EqualTo("0,3"));
            }

            [Test]
            public void Return_Result_IfArithmeticExpression27()
            {
                string test = "45,33658 - 68.99999";
                Assert.That(_calculator.StartParse(test), Is.EqualTo("-23,66341"));
            }

            [Test]
            public void Return_Result_IfArithmeticExpression28()
            {
                string test = "253.1-0.0099";
                Assert.That(_calculator.StartParse(test), Is.EqualTo("253,0901"));
            }

            [Test]
            public void Return_Result_IfArithmeticExpression29()
            {
                string test = "2,365897445 - 1, 3698524";
                Assert.That(_calculator.StartParse(test), Is.EqualTo("0,996045045"));
            }

            [Test]
            public void Return_Result_IfArithmeticExpression30()
            {
                string test = "2/3";
                Assert.That(_calculator.StartParse(test), Is.EqualTo("0,6666666667"));
            }

            [Test]
            public void Return_ShouldReturn_IfTheNumberIsPositive()
            {
                string test = "5";
                int result = _calculator.ReturnP(test);
                result.Should().BePositive();
            }

            [Test]
            public void Return_ShouldReturn_IfTheNumberNegitive()
            {
                string test = "-5";
                int result = _calculator.ReturnN(test);
                result.Should().BeNegative();
            }

            [Test]
            public void Return_ShouldReturn_IfTheNumberDouble()
            {
                string test = "4,2";
                double result = _calculator.ReturnD(test);
                Assert.That(result, Is.EqualTo(4.2));
            }

            [Test]
            public void Return_ShouldReturn_IfEmpty()
            {
                string test = "  ";
                string result = _calculator.ReturnE(test);
                result.Should().BeEmpty();
            }
        }*/

        public class Calculator
        {
            private string InputString;

            private bool invalid = false;
            private int symbol, index = 0;
            private void GetSymbol()
            {
                if (index < InputString.Length)
                {
                    symbol = InputString[index];
                    index++;
                }
                else
                    symbol = '\0';
            }
            private string CheckInput(string input)
            {
                string temp = string.Empty;
                string res = string.Empty;
                bool isComma = false;
                bool isDigit = false;
                foreach (char symbol in input)
                {
                    if (char.IsDigit(symbol))
                    {
                        isDigit = true;
                        temp += symbol;
                    }

                    else if (symbol == '.' || symbol == ',')
                    {
                        if (!isDigit)
                        {
                            if (!isComma)
                            {
                                isComma = true;
                                isDigit = true;
                                temp += '0';
                                temp += ',';
                            }
                        }
                        else
                        {
                            if (!isComma)
                            {
                                isComma = true;
                                temp += ',';
                            }
                        }

                    }
                    else if ("()/*-+".Contains(symbol))
                    {
                        isComma = false;
                        temp += symbol;
                    }
                }
                return temp;
            }

            public string StartParse(string input)
            {

                InputString = input ?? "0";
                invalid = false;
                if (InputString == "0")
                    return InputString;

                InputString = CheckInput(input);

                index = 0;
                GetSymbol();
                double result = ProcE();
                string _result = result.ToString();
                if (_result == "E") _result = result.ToString("F8").TrimEnd('0').TrimEnd(',');
                if (invalid)
                    return "Error";
                else if (_result == double.NaN.ToString())
                    return "∞";
                return _result;
            }

            private double ProcE()
            {
                double x = ProcT();
                while (symbol == '+' || symbol == '-')
                {
                    char c = (char)symbol;
                    GetSymbol();
                    if (symbol == '\0')
                    {
                        if (c == '+')
                            x += x;
                        else
                            x -= x;
                    }
                    else if (c == '+')
                    {
                        x += ProcT();
                        x = Math.Round(x, 10);
                    }

                    else
                    {
                        x -= ProcT();
                        x = Math.Round(x, 10);
                    }
                }
                return x;
            }
            private double ProcT()
            {
                double x = ProcM();
                while (symbol == '*' || symbol == '/')
                {
                    char c = (char)symbol;
                    GetSymbol();
                    if (c == '*')
                    {
                        x *= ProcM();
                        x = Math.Round(x, 10);
                    }
                    else
                    {
                        x /= ProcM();
                        x = Math.Round(x, 10);
                    }
                }
                return x;
            }
            private double ProcM()
            {
                double x = 0;
                if (symbol == '(')
                {
                    GetSymbol();
                    x = ProcE();
                    if (symbol != ')')
                    {
                        invalid = true;
                        return 0;
                    }
                    GetSymbol();
                    if (symbol >= '0' && symbol <= '9')
                    {
                        invalid = true;
                        return 0;
                    }
                }
                else
                {
                    if (symbol == '-')
                    {
                        GetSymbol();
                        x = -ProcM();
                    }
                    else if (symbol == '/' || symbol == '*' || symbol == '+')
                    {
                        GetSymbol();
                        x = ProcC();
                    }
                    else if (symbol >= '0' && symbol <= '9')
                        x = ProcC();
                    else if (char.IsLetter((char)symbol))
                    {
                        invalid = true;
                        return 0;
                    }
                    else if (symbol == '(' || symbol == ')')
                    {
                        invalid = true;
                        return 0;
                    }

                }
                return x;
            }
            private double ProcC()
            {
                string x = "";
                bool iscomma = false;
                char temp = (char)symbol;
                while (symbol >= '0' && symbol <= '9')
                {
                    x += (char)symbol;
                    GetSymbol();
                    if (symbol == '.' || symbol == ',')
                    {
                        if (iscomma)
                        {
                            invalid = true;
                            return 0;
                        }
                        x += ',';
                        GetSymbol();
                        iscomma = true;
                    }
                    else if (symbol == 'E')
                    {
                        x += (char)symbol;
                        GetSymbol();
                        x += (char)symbol;
                        GetSymbol();
                    }
                }
                if (x.LastOrDefault() == ',')
                {
                    x += '0';
                }
                if (x == String.Empty)
                {
                    invalid = true;
                    x = "0";
                }
                return double.Parse(x);
            }

            public string? StartParsing()
            {
                throw new NotImplementedException();
            }
        }
    }
}

