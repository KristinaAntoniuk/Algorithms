using System;
static class ArrayExtension
{
    public static string StringOutput(this Array input)
    {
        string result = "";
        
        foreach (var item in input)
        {
            result += " " + item.ToString();
        }
        return result;
    }
}