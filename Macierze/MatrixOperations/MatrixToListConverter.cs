namespace Macierze.MatrixOperations;
public class MatrixToListConverter
{
    public static List<string> ConvertMatrixToList(IFormCollection collection)
    {
        List<string> list = new List<string>();
        foreach (var item in collection)
        {
            if (item.Value != "")
            {
                list.Add(item.Value);
            }
            else
            {
                return new List<string>();
            }
        }

        list.RemoveAt(list.Count - 1);
        return list;
    }
}