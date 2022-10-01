using Macierze.Models;
using Newtonsoft.Json;

namespace Macierze.MatrixOperations;

public class MatrixModelDeserializer
{
    public static MatrixModel DeserializeMatrixModel(string serializedMatrix)
    {
        MatrixModel model = JsonConvert.DeserializeObject<MatrixModel>(serializedMatrix);
        return model;
    }
}