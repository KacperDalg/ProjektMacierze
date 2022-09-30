using Macierze.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using static Macierze.MatrixOperations.MatrixToListConverter;
using static Macierze.MatrixOperations.MatrixOperationsProvider;
using static Macierze.FileOperations.MatrixToTxtFileConverter;
using static Macierze.FileOperations.TxtFileToMatrixConverter;
using static Macierze.FileOperations.XlsxFileToMatrixConverter;
using static Macierze.FileOperations.CsvFileToMatrixConverter;
using static Macierze.MatrixOperations.MatrixModelGenerator;

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

    public RedirectToActionResult RedirectToOptions(string formList, int matrixSize)
    {
        return RedirectToAction("Options", RegenerateMatrixModel(formList, matrixSize));
    }

    public IActionResult ErrorMatrixSize()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Print(string formList, int matrixSize)
    {
        return View(RegenerateMatrixModel(formList, matrixSize));
    }

    [HttpPost]
    public IActionResult SumDiagonal(string formList, int matrixSize)
    {
        ViewBag.Sum = SumFromDiagonal(RegenerateMatrixModel(formList, matrixSize));
        return View(RegenerateMatrixModel(formList, matrixSize));
    }

    [HttpPost]
    public IActionResult MatrixRow(string formList, int matrixSize)
    {
        return View(RegenerateMatrixModel(formList, matrixSize));
    }

    public IActionResult SumRow(string row, string formList, int matrixSize)
    {
        var model = RegenerateMatrixModel(formList, matrixSize);
        if (row == null || int.Parse(row) > matrixSize || int.Parse(row) < 1)
        {
            ViewBag.Size = matrixSize;
            return View("ErrorRow", model);
        }
        else
        {
            ViewBag.Row = row;
            ViewBag.Sum = SumFromRow(model.FormList, matrixSize, row);
            return View(model);
        }
    }

    [HttpPost]
    public IActionResult MatrixColumn(string formList, int matrixSize)
    {
        return View(RegenerateMatrixModel(formList, matrixSize));
    }

    public IActionResult SumColumn(string column, string formList, int matrixSize)
    {
        var model = RegenerateMatrixModel(formList, matrixSize);
        if (column == null || int.Parse(column) > matrixSize || int.Parse(column) < 1)
        {
            ViewBag.Size = matrixSize;
            return View("ErrorColumn", model);
        }
        else
        {
            ViewBag.Column = column;
            ViewBag.Sum = SumFromColumn(model.FormList, matrixSize, column);
            return View(model);
        }
    }

    [HttpPost]
    public FileResult Save(string formList, int matrixSize)
    {
        return File(Encoding.ASCII.GetBytes(ConvertMatrixToString(RegenerateMatrixModel(formList, matrixSize))), "text/plain", "moja_macierz.txt");
    }
}