namespace Macierze.MatrixOperations;
public class MatrixCheckProvider
{
    public static int CheckMatrixSize(List<string> list)
    {
        int size = 0;

        for (int i = 2; i <= 10; i++)
        {
            if (i * i == list.Count)
            {
                size = i;
                break;
            }
        }
        return size;
    }
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