using UnityEngine;

namespace IngameConsole
{
    public class ConsoleWriter : BaseWriter
    {
        private Color _errorColor = Color.red;
        private Color _infoColor = new Color32(30, 98, 206, 255);
        private Color _warningColor = new Color32(170, 135, 30, 255);

        public ConsoleWriter(BaseConsoleIO consoleIO) : base(consoleIO) { }

        public override void WriteLine(string text)
        {
            Write("\n" + text);
        }

        public override void Write(string text)
        {
            _consoleIO.AppendToOutput(text);
        }

        public override void WriteLineItalic(string text)
        {
            WriteLine(string.Format("<i>{0}</i>", text));
        }

        public override void WriteItalic(string text)
        {
            Write(string.Format("<i>{0}</i>", text));
        }

        public override void WriteLineBold(string text)
        {
            WriteLine(string.Format("<b>{0}</b>", text));
        }

        public override void WriteBold(string text)
        {
            Write(string.Format("<b>{0}</b>", text));
        }

        public override void WriteError(string text)
        {
            OpenColor(_errorColor);
            WriteLine(text);
            CloseColor();
        }

        public override void WriteInfo(string text)
        {
            OpenColor(_infoColor);
            WriteLine(text);
            CloseColor();
        }

        public override void WriteWarning(string text)
        {
            OpenColor(_warningColor);
            WriteLine(text);
            CloseColor();
        }

        public override void OpenBold()
        {
            Write("<b>");
        }

        public override void CloseBold()
        {
            Write("</b>");
        }

        public override void OpenColor(Color color)
        {
            string hexCol = ColorUtility.ToHtmlStringRGB(color);
            Write(string.Format("<color=#{0}>", hexCol));
        }

        public override void CloseColor()
        {
            Write("</color>");
        }

        public override void NextLine()
        {
            Write("\n");
        }
    }
}