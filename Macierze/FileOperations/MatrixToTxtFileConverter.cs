using static Macierze.Controllers.OperationsController;

namespace Macierze.FileOperations;
public class MatrixToTxtFileConverter
{
    public static string ConvertMatrixToString()
    {
        string content = "";
        int i = 0;
        int line = 1;
        foreach (var item in Model.FormList)
        {
            content += item + " \t\t";
            i += 1;
            if (Model.MatrixSize * line == i)
            {
                content += Environment.NewLine;
                line += 1;
            }
        }
        return content;
    }
}
