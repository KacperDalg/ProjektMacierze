namespace Macierze.Matrix_Operations;
public class SumColumn
{
    public static int SumColumnFunction(List<string> list, int size, string column)
    {
        int sum = 0;
        for (int i = int.Parse(column) - 1; i <= size * size - size + int.Parse(column); i += size)
        {
            sum += int.Parse(list[i]);
        }
        return sum;
    }
}
