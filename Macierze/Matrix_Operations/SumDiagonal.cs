using static Macierze.Controllers.OperationsController;

namespace Macierze.Matrix_Operations;
public class SumDiagonal
{
    public static int SumDiagonalFunction()
    {
        var size = Model.MatrixSize;
        var List = Model.FormList;
        int sum = 0;
        for (int i = 0; i < List.Count; i++)
        {
            sum += int.Parse(List[i]);
            i += size;
        }
        return sum;
    }
}
