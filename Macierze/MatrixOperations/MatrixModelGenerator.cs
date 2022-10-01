using Macierze.Models;
using Newtonsoft.Json;

namespace Macierze.MatrixOperations;

public class MatrixModelGenerator
{
    public static MatrixModel GenerateMatrixModel(List<string> matrixNumbersArray)
    {
        MatrixModel model = new MatrixModel();
        model.MatrixSize = Convert.ToInt32(Math.Sqrt(Convert.ToDouble(matrixNumbersArray.Count)));
        model.FormList = matrixNumbersArray;
        return model;
    }
}