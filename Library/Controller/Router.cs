using System;
using System.Collections;

namespace Library;
public class Router
{
    public static BankAccount? CurrentAccount;
    private static Stack<Page> Stack = new Stack<Page>();

    public static int GetCount()
    {
        return Stack.Count;
    }

    public static void ViewCurrentPage()
    {
        Console.Clear();
        GetCurrentPage().Display();
    }

    public static void PushPage(Page GoToPage)
    {
        Stack.Push(GoToPage);
    }

    public static void ChangePage(Page GoToPage)
    {
        Stack.Pop();
        Stack.Push(GoToPage);
    }

    public static Page GetCurrentPage()
    {
        return Stack.Peek();
    }

    public static void GoBack()
    {
        if (Stack.Count > 1)
        {
            Stack.Pop();
            ViewCurrentPage();
        }
    }

    public static void Pop()
    {
        if (Stack.Count > 1)
        {
            Stack.Pop();
        }
    }
}