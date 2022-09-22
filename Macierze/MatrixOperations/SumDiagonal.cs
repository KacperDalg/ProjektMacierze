using static Macierze.Controllers.OperationsController;

namespace Macierze.MatrixOperations;
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
