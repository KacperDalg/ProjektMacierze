namespace Macierze.MatrixOperations;
public class SumRow
{
    public static int SumRowFunction(List<string> list, int size, string row)
    {
        int sum = 0;
        for (int i = (int.Parse(row) - 1) * size; i < (int.Parse(row) - 1) * size + size; i++)
        {
            sum += int.Parse(list[i]);
        }
        return sum;
    }
}
