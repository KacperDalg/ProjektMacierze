using Macierze.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using static Macierze.MatrixOperations.MatrixModelGenerator;
using static Macierze.MatrixOperations.MatrixModelDeserializer;
using static Macierze.MatrixOperations.MatrixToListConverter;
using static Macierze.MatrixOperations.MatrixOperationsProvider;
using static Macierze.FileOperations.MatrixToTxtFileConverter;
using static Macierze.FileOperations.TxtFileToMatrixConverter;
using static Macierze.FileOperations.XlsxFileToMatrixConverter;
using static Macierze.FileOperations.CsvFileToMatrixConverter;

namespace Macierze.Controllers;

public class OperationsController : Controller
{
    [HttpPost]
    public RedirectToActionResult MatrixCheck(IFormCollection collection)
    {
        List<string> matrixNumbersArray = ConvertMatrixToList(collection);

        if (matrixNumbersArray.Count == 0)
        {
            return RedirectToAction("Index", "Home");
        }
        else if (matrixNumbersArray.Count < 4 || matrixNumbersArray.Count > 100)
        {
            return RedirectToAction("ErrorMatrixSize", "Operations");
        }
        else
        {
            return RedirectToAction("Options", "Operations", GenerateMatrixModel(matrixNumbersArray));
        }
    }

    [HttpPost]
    public IActionResult UploadFile(IFormFile file)
    {
        List<string> matrixNumbersArray = new List<string>();

        if (file.FileName.Contains(".txt"))
        {
            ReadTxtFile(matrixNumbersArray, file);
        }
        else if (file.FileName.Contains(".csv"))
        {
            ReadCsvFile(matrixNumbersArray, file);
        }
        else if (file.FileName.Contains(".xlsx"))
        {
            ReadXlsxFile(matrixNumbersArray, file);
        }
        else
        {
            return View("~/Views/Home/Index.cshtml");
        }

        if (Math.Sqrt(Convert.ToDouble(matrixNumbersArray.Count)) % 1 != 0 || matrixNumbersArray.Count > 100 || matrixNumbersArray.Count < 4)
        {
            return View("ErrorMatrixSize");
        }

        return View("Options", GenerateMatrixModel(matrixNumbersArray));
    }

    public IActionResult Options(MatrixModel model)
    {
        return View(model);
    }

    public RedirectToActionResult RedirectToOptions(string serializedMatrix)
    {
        return RedirectToAction("Options", DeserializeMatrixModel(serializedMatrix));
    }

    public IActionResult ErrorMatrixSize()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Print(string serializedMatrix)
    {
        return View(DeserializeMatrixModel(serializedMatrix));
    }

    [HttpPost]
    public IActionResult SumDiagonal(string serializedMatrix)
    {
        MatrixModel model = DeserializeMatrixModel(serializedMatrix);
        model.Sum = SumFromDiagonal(model);
        return View(model);
    }

    [HttpPost]
    public IActionResult MatrixRow(string serializedMatrix)
    {
        return View(DeserializeMatrixModel(serializedMatrix));
    }

    public IActionResult SumRow(string row, string serializedMatrix)
    {
        MatrixModel model = DeserializeMatrixModel(serializedMatrix);
        if (row == null || int.Parse(row) > model.MatrixSize || int.Parse(row) < 1)
        {
            return View("ErrorRow", model);
        }
        else
        {
            model.Sequence = row;
            model.Sum = SumFromRow(model, row);
            return View(model);
        }
    }

    [HttpPost]
    public IActionResult MatrixColumn(string serializedMatrix)
    {
        return View(DeserializeMatrixModel(serializedMatrix));
    }

    public IActionResult SumColumn(string column, string serializedMatrix)
    {
        MatrixModel model = DeserializeMatrixModel(serializedMatrix);
        if (column == null || int.Parse(column) > model.MatrixSize || int.Parse(column) < 1)
        {
            return View("ErrorColumn", model);
        }
        else
        {
            model.Sequence = column;
            model.Sum = SumFromColumn(model, column);
            return View(model);
        }
    }

    [HttpPost]
    public FileResult Save(string serializedMatrix)
    {
        return File(Encoding.ASCII.GetBytes(ConvertMatrixToString(DeserializeMatrixModel(serializedMatrix))), "text/plain", "moja_macierz.txt");
    }
}