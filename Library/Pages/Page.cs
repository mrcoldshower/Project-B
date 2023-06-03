namespace Library;
using static System.Console;

public abstract class Page
{
    public virtual bool IsQuestionPage { get; set; } = false;
    public abstract void Display();
    public abstract Page ChoosePage(int input);
    public Dictionary<string, string> QuestionsAnswers { get; set; } = new Dictionary<string, string>();

    public int SelectedIndex;

    public virtual int Navigate(string prompt, string[] options, string before = "<< ", string after = " >>")
    {
        ConsoleKey keyPressed;
        do
        {
            Clear();
            DisplayOptions(prompt, options, before, after);

            ConsoleKeyInfo keyInfo = ReadKey(true);
            keyPressed = keyInfo.Key;

            if (keyPressed == ConsoleKey.UpArrow || keyPressed == ConsoleKey.W)
            {
                SelectedIndex--;
                if (SelectedIndex == -1)
                {
                    SelectedIndex = options.Length - 1;
                }
            }
            else if (keyPressed == ConsoleKey.DownArrow || keyPressed == ConsoleKey.S)
            {
                SelectedIndex++;
                if (SelectedIndex == options.Length)
                {
                    SelectedIndex = 0;
                }
            }
            else if (keyPressed == ConsoleKey.LeftArrow || keyPressed == ConsoleKey.A)
            {
                QuestionsAnswers.Clear();
                Router.GoBack();
                break;
            }
            else if (keyPressed == ConsoleKey.Escape)
            {
                Utils.ExitApplication();
            }
            else if (keyPressed == ConsoleKey.Enter || keyPressed == ConsoleKey.RightArrow || keyPressed == ConsoleKey.D)
            {
                if (IsQuestionPage == true && options[SelectedIndex][0] != '[') // if page is to input data, and the option is not a create/finish button do this
                {
                    string currentOption = options[SelectedIndex];
                    Console.SetCursorPosition(currentOption.Length + 10, SelectedIndex + 1);
                    if (QuestionsAnswers.ContainsKey(currentOption)) QuestionsAnswers[currentOption] = ReadLine() ?? "";
                    else QuestionsAnswers.Add(currentOption, ReadLine() ?? "");
                }
                else
                {
                    break;
                }
            }
        } while (true); // keyPressed != ConsoleKey.Enter && keyPressed != ConsoleKey.RightArrow && keyPressed != ConsoleKey.D

        return SelectedIndex;
    }

    public virtual void DisplayOptions(string prompt, string[] options, string before, string after) // before and after are what kind of decorations you want on the options menu
    {
        WriteLine(prompt);
        for (int i = 0; i < options.Length; i++)
        {
            string currentOption = options[i];
            string prefix;

            if (i == SelectedIndex)
            {
                prefix = "*";
                ForegroundColor = ConsoleColor.Black;
                BackgroundColor = ConsoleColor.White;

            }
            else
            {
                prefix = " ";
                ForegroundColor = ConsoleColor.White;
                BackgroundColor = ConsoleColor.Black;
            }

            Write($"{prefix} {before}{currentOption}{after}");
            if (IsQuestionPage && QuestionsAnswers.ContainsKey(currentOption))
            {
                ResetColor();
                Write($"  {QuestionsAnswers[currentOption]}");
            }
            WriteLine();
        }
        ResetColor();
    }

    // ======================================================================================


    public virtual Dictionary<string, string> DisplayAndInputOptions(string prompt, string[] options)
    {
        Dictionary<string, string> result = new();
        string answer = "";

        WriteLine(prompt);
        for (int i = 0; i < options.Length; i++)
        {
            string currentOption = options[i];
            string prefix;

            if (i == SelectedIndex)
            {
                prefix = "*";
                answer = DisplayElement(prompt, currentOption);
                result.Add(currentOption, answer);
                break;
            }
            else
            {
                prefix = " ";
                ForegroundColor = ConsoleColor.White;
                BackgroundColor = ConsoleColor.Black;
                WriteLine($"{prefix}    {currentOption}: ");
                ResetColor();
            }
        }
        return result;
    }


    private string DisplayElement(string prompt, string currentOption)
    {
        string str = "";
        while (true)
        {
            Console.Clear();
            Write($"{currentOption}: {str}");
            ConsoleKeyInfo cki = Console.ReadKey();
            if (cki.Key != ConsoleKey.Backspace && cki.Key != ConsoleKey.Enter)
            {
                str += cki.KeyChar.ToString() ?? "";
            }
            else
            {
                if (cki.Key == ConsoleKey.Backspace && str.Length > 0)
                {
                    str = str.Substring(0, str.Length - 1);
                }
                else if (cki.Key == ConsoleKey.Enter && str == "")
                {
                    break;
                }
                else if (cki.Key == ConsoleKey.Enter)
                {
                    break;
                }
            }
        }
        Console.Clear();
        return str;
    }


    public virtual void GetInput(string prompt, string[] options)
    {
        Dictionary<string, string> endResult = new(); // <question, answer>
        while (true)
        {
            Navigate(prompt, options, "", ": ");
            if (options[SelectedIndex][0] == '[') break;
            Dictionary<string, string> questionAndAnswer = DisplayAndInputOptions(prompt, options);
            endResult.Add(questionAndAnswer.Keys.First(), questionAndAnswer.Values.First());
        }
        foreach (var element in endResult)
        {
            Console.WriteLine($"{element.Key}: {element.Value}");
        }
        Router.GoBack();
    }
}