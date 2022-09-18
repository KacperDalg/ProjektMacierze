using static Macierze.Controllers.OperationsController;

namespace Macierze.File_Operations;
public class SaveFile
{
    public static string MatrixToStringFunction()
    {
        string s = "";
        int i = 0;
        int x = 1;
        foreach (var item in Model.FormList)
        {
            s += item + " \t\t";
            i += 1;
            if (Model.MatrixSize * x == i)
            {
                s += Environment.NewLine;
                x += 1;
            }
        }
        return s;
    }
}
