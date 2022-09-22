using ExcelDataReader;
using System.Data;
using System.Text;

namespace Macierze.FileOperations;
public class UploadFile
{
    public static List<string> FileDataToMatrix(List<string> numbers, IFormFile file)
    {
        using (var stream = new StreamReader(file.OpenReadStream()))
        {
            var data = stream.ReadToEnd();
            List<string> content = file.FileName.Contains(".txt") ? data.Replace("\n", " ").Replace("\r", "").Split(" ").ToList() : data.Replace("\n", ";").Replace("\r", "").Split(";").ToList();
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
    public static List<string> ExcelDataToMatrix(List<string> numbers, IFormFile file)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        using (var stream = new StreamReader(file.OpenReadStream()))
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream.BaseStream))
            {
                var result = reader.AsDataSet();
                var table = result.Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    foreach (double item in row.ItemArray)
                    {
                        numbers.Add(item.ToString());
                    }
                }
            }
        }
        return numbers;
    }
}
