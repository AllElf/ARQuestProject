using UnityEngine;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UI;

[System.Serializable]
public class LetterColorCategory
{
    public string letters;   // сюда в инспекторе пишешь буквы, например "ахе"
    public Color color;      // цвет выбираешь через палитру
}

public class UniversalLetterColorizer : MonoBehaviour
{
    public Text textField;        // или UnityEngine.UI.Text
    [TextArea] public string inputText; // текст для обработки
    public List<LetterColorCategory> categories; // список категорий букв и цветов

    private void Start()
    {
        ColorText();
    }
    public void ColorText()
    {
        textField.text = ColorizeText(textField.text);
        inputText = textField.text;
    }

    string ColorizeText(string input)
    {
        StringBuilder sb = new StringBuilder();

        foreach (char c in input)
        {
            string colorTag = GetColorForLetter(c);
            if (colorTag != null)
                sb.Append($"<color={colorTag}>{c}</color>");
            else
                sb.Append(c);
        }

        return sb.ToString();
    }

    string GetColorForLetter(char c)
    {
        foreach (var cat in categories)
        {
            if (cat.letters.Contains(c.ToString()))
            {
                // Конвертируем Unity Color в HEX
                Color32 col32 = cat.color;
                return $"#{col32.r:X2}{col32.g:X2}{col32.b:X2}";
            }
        }
        return null;
    }
}