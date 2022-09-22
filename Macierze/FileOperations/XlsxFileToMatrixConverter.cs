using ExcelDataReader;
using System.Data;
using System.Text;

namespace Macierze.FileOperations;
public class XlsxFileToMatrixConverter
{
    public static List<string> ReadXlsxFile(List<string> numbers, IFormFile file)
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
