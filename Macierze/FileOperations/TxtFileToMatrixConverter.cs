namespace Macierze.FileOperations;

public class TxtFileToMatrixConverter
{
    public static List<string> ReadTxtFile(List<string> numbers, IFormFile file)
    {
        using (var stream = new StreamReader(file.OpenReadStream()))
        {
            var data = stream.ReadToEnd();
            List<string> content = data.Replace("\n", " ").Replace("\r", "").Split(" ").ToList();
            foreach (var item in content)
            {
                if (item != null && item != "" && item.Length > 0)
                {
                    numbers.Add(item);
                }
            }
            return numbers;
        }
    }
}