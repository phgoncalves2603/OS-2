using System.Collections.Generic;
using System.Windows.Forms;

public static class TestData
{
    public static bool IsTestMode { get; set; } = false;
    public static Queue<string> TestInputs { get; set; } = new Queue<string>();
    public static Queue<DialogResult> TestDialogResults { get; set; } = new Queue<DialogResult>();

    public static string GetInput(string prompt, string title)
    {
        if (IsTestMode && TestInputs.Count > 0)
        {
            return TestInputs.Dequeue();
        }
        return Microsoft.VisualBasic.Interaction.InputBox(prompt, title, "", -1, -1);
    }

    public static DialogResult ShowDialog(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
    {
        if (IsTestMode && TestDialogResults.Count > 0)
        {
            return TestDialogResults.Dequeue();
        }
        return MessageBox.Show(text, caption, buttons, icon);
    }
}