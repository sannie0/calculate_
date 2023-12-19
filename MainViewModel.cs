using MyPars.Calculation_Parser;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace calculate_;
class MainViewModel : INotifyPropertyChanged, IDataErrorInfo
{
    private Calculator _calculator;
    private bool _isCalculate = true;
    private string _prevOperation = string.Empty;
    private string _inputTxt;
    private string _result;

    private readonly IMemory _memory;

    public MainViewModel(IMemory memory) // внедряем зависимость
    {
        _memory = memory;
        _calculator = new Calculator();

        InputTxt = "0";

        PrevOperation = string.Empty;

        Result = string.Empty;

        ButtonClicked = new RelayCommand<string>(OnDigitButton);

        ButtonCalculation = new RelayCommand<string>(Calculation, CanExecuteCalculation);

        ButtonRemove = new RelayCommand(() =>
        {
            if (InputTxt.Length > 0)
            {
                InputTxt = InputTxt.Substring(0, InputTxt.Length - 1);
                if (InputTxt == string.Empty)
                    {
                        InputTxt = "0";
                    }
                OnPropertyChanged(nameof(InputTxt));
                PrevOperation = InputTxt[InputTxt.Length - 1].ToString();
                OnPropertyChanged(nameof(PrevOperation));
            }
        });

        CleanCommand = new RelayCommand(() =>
        {
            InputTxt = "0";
            Result = string.Empty;
            PrevOperation = string.Empty;
            OnPropertyChanged(nameof(Result));
            OnPropertyChanged(nameof(InputTxt));
        });

        AddToMemory = new RelayCommand<string>(x => //memory RAM
        {
            if (_isCalculate && Result != string.Empty)
            {
                x = InputTxt;
                _memory.PushElement(x);
            }
            return;
        });

        GetLastElement = new RelayCommand<string>(x =>
        {
            string temp = _memory.GetLastElement();
            InputTxt = temp;
            if (InputTxt == string.Empty)
            {
                InputTxt = "history empty";
                OnPropertyChanged(nameof(InputTxt));
                return;
            }
            OnPropertyChanged(nameof(InputTxt));
            Result = _calculator.StartParse(InputTxt);
            OnPropertyChanged(nameof(Result));
        });


    }

    private string PrevOperation
    {
        get { return _prevOperation; }
        set
        {
            if (_prevOperation != value)
            {
                _prevOperation = value;
                OnPropertyChanged(nameof(PrevOperation));
            }
        }
    }

    private void OnDigitButton(string digit)
    {
        if (InputTxt == "0")
        {
            if ("/*-+,".Contains(digit))
            {
                InputTxt += digit;
                OnPropertyChanged(nameof(InputTxt));
                PrevOperation = digit;
                return;
            }
            else
                InputTxt = String.Empty;
        }
        
        if (digit != string.Empty)
        {
            if(digit == "," && PrevOperation == string.Empty && InputTxt != "history empty")
            {
                InputTxt += "0";
                PrevOperation = "0";
                OnPropertyChanged(nameof(InputTxt));
                OnPropertyChanged(nameof(PrevOperation));
            }
            if (InputTxt.Length > 0)
            {
                if (",".Contains(digit) && "/*-+".Contains(PrevOperation) && InputTxt != "history empty")
                {
                    InputTxt += "0";
                    
                    InputTxt += digit;
                    OnPropertyChanged(nameof(InputTxt));
                    PrevOperation = digit;
                    return;
                }
                else if ("0123456789".Contains(PrevOperation) && "(".Contains(digit) && InputTxt != "history empty")
                {
                    InputTxt += "*" + digit;
                    OnPropertyChanged(nameof(InputTxt));
                    PrevOperation = digit;
                    return;
                }
                else if (")".Contains(PrevOperation) && "(".Contains(digit) && InputTxt != "history empty")
                {
                    InputTxt += "*" + digit;
                    OnPropertyChanged(nameof(InputTxt));
                    PrevOperation = digit;
                    return;
                }
                else if ("/*-+".Contains(digit))
                {
                    if ("/*-+".Contains(PrevOperation))
                    {
                        InputTxt = InputTxt.Substring(0, InputTxt.Length - 1) + digit;
                        OnPropertyChanged(nameof(InputTxt));
                        PrevOperation = digit;
                        return;
                    }

                }
                else if (",".Contains(digit) && ",".Contains(PrevOperation) && InputTxt != "history empty")
                {
                    return;
                }
                else if (",".Contains(digit) && "0".Contains(PrevOperation) && InputTxt[InputTxt.Length - 2] == ',')
                {
                    return;
                }
            }
            if (InputTxt == "history empty")
            {
                if ("0123456789/*-+()".Contains(digit))
                {
                    InputTxt = digit;
                    OnPropertyChanged(nameof(InputTxt));
                    PrevOperation = digit;
                    return;
                }
                else if (",".Contains(digit))
                {
                    InputTxt = "0" + digit;
                    OnPropertyChanged(nameof(InputTxt));
                    PrevOperation = digit;
                    return;
                }
            }
            InputTxt += digit;
            OnPropertyChanged(nameof(InputTxt));
            PrevOperation = digit;
        }
    }

    public bool IsCalculate
    {
        get { return _isCalculate;}
        set
        {
            if (_isCalculate != value)
            {
                _isCalculate = value;
                OnPropertyChanged(nameof(IsCalculate));
                OnPropertyChanged(nameof(ButtonCalculation)); 
            }
        }
    }

    private bool CanExecuteCalculation(string input)
    {
        return IsCalculate; 
    }

    private void Calculation(string digit)
    {
        Result = _calculator.StartParse(_inputTxt);
        OnPropertyChanged(nameof(Result));
    }

    public string InputTxt //validation
    {
        get { return _inputTxt; }
        set
        {

            _inputTxt = value;
            OnPropertyChanged(nameof(InputTxt));

            if (string.IsNullOrEmpty(_inputTxt))
            {
                _errors.Clear();
                IsCalculate = false;
                Result = String.Empty;
            }
            /*if (string.IsNullOrWhiteSpace(value))
            {
                _errors[nameof(InputTxt)] = "пустая строка";
            }*/
            if (!string.IsNullOrEmpty(_inputTxt))
            {
                char lastChar = InputTxt[InputTxt.Length - 1];
                if (!CheckStaples(_inputTxt))
                {
                    _errors[nameof(InputTxt)] = "Неправильно расставлены скобки";
                    IsCalculate = false;
                }
                    
                else if ("/*-+(".Contains(lastChar))
                {
                    _errors[nameof(InputTxt)] = "Введите полное арифметическое выражение";
                    IsCalculate = false;
                }
                else if (InputTxt.Any(char.IsLetter))
                {
                     _errors[nameof(InputTxt)] = "Выражение не должно содержать буквы";
                     IsCalculate = false;
                }

                else
                {
                    _errors[nameof(InputTxt)] = "";
                    IsCalculate = true;
                }
            }
            else
            {
                _errors[nameof(InputTxt)] = "";
                IsCalculate = true;
            }
        }
    }

    private bool CheckStaples(string input)
    {
        int openingBrackets = input.Count(c => (c == '('));
        int closingBrackets = input.Count(c => (c == ')'));
        if (openingBrackets == closingBrackets)
            return true;
        else
             return false;
    }

    public string Result
    {
        get { return _result; }
        set
        {
            if (_result != value)
            {
                _result = value;
                OnPropertyChanged(nameof(Result));
            }
        }

    }

    public RelayCommand<string> AddToMemory {  get; }

    public RelayCommand<string> GetLastElement { get; }
    /// <summary>
    /// MEMORY
    /// </summary>

    public RelayCommand CleanCommand { get; }
    public RelayCommand ButtonRemove { get; }
    public RelayCommand<string> ButtonClicked { get; }

    public RelayCommand<string> ButtonCalculation { get; private set; }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    private Dictionary<string, string> _errors = new Dictionary<string, string>();

    public string Error
    {
        get
        {
            return string.Join(Environment.NewLine, _errors);
        }
    }

    public string this[string columnName]
    {
        get
        {
            return _errors.TryGetValue(columnName, out var value) ? value : string.Empty;
        }
    }

}
