using Macierze.Models;

namespace Macierze.MatrixOperations;
public class MatrixModelGenerator
{
    public static MatrixModel RegenerateMatrixModel(string formList, int matrixSize)
    {
        MatrixModel model = new MatrixModel();
        var list = formList.Split(',').ToList();
        list.RemoveAt(list.Count - 1);
        model.FormList = list;
        model.MatrixSize = matrixSize;
        return model;
    }

    public static MatrixModel GenerateMatrixModel(List<string> matrixNumbersArray)
    {
        MatrixModel model = new MatrixModel();
        model.MatrixSize = Convert.ToInt32(Math.Sqrt(Convert.ToDouble(matrixNumbersArray.Count)));
        model.FormList = matrixNumbersArray;
        return model;
    }
}