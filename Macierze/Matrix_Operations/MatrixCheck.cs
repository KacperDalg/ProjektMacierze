namespace Macierze.Matrix_Operations;
public class MatrixCheck
{
    public static int MatrixSizeCheckFunction(List<string> List)
    {
        int size = 0;

        for (int i = 2; i <= 10; i++)
        {
            if (i * i == List.Count)
            {
                size = i;
                break;
            }
        }
        return size;
    }

    public static List<string> MatrixToListFunction(IFormCollection collection)
    {
        List<string> List = new List<string>();
        foreach (var item in collection)
        {
            if (item.Value != "")
            {
                List.Add(item.Value);
            }
            else
            {
                return new List<string>();
            }
        }

        List.RemoveAt(List.Count - 1);
        return List;
    }
}
