﻿using Macierze.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using static Macierze.MatrixOperations.SumDiagonal;
using static Macierze.MatrixOperations.SumRow;
using static Macierze.MatrixOperations.SumColumn;
using static Macierze.MatrixOperations.MatrixCheck;
using static Macierze.FileOperations.MatrixToTxtFileConverter;
using static Macierze.FileOperations.TxtFileToMatrixConverter;
using static Macierze.FileOperations.XlsxFileToMatrixConverter;
using static Macierze.FileOperations.CsvFileToMatrixConverter;

namespace Macierze.Controllers;
public class OperationsController : Controller
{
    public static MatrixModel Model = new MatrixModel();

    [HttpPost]
    public RedirectToActionResult MatrixCheck(IFormCollection collection)
    {
        List<string> matrixNumbersArray = MatrixToListFunction(collection);

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
            Model.MatrixSize = MatrixSizeCheckFunction(matrixNumbersArray);
            Model.FormList = matrixNumbersArray;
            return RedirectToAction("Options", "Operations");
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
        Model.FormList = matrixNumbersArray;
        Model.MatrixSize = Convert.ToInt32(Math.Sqrt(Convert.ToDouble(matrixNumbersArray.Count)));
        return View("Options");
    }

    public IActionResult Options()
    {
        return View();
    }

    public IActionResult ErrorMatrixSize()
    {
        return View();
    }

    public IActionResult Print()
    {
        ViewBag.Size = Model.MatrixSize;
        ViewBag.List = Model.FormList;
        return View();
    }

    public IActionResult SumDiagonal()
    {
        ViewBag.Sum = SumDiagonalFunction();
        return View();
    }

    public IActionResult MatrixRow()
    {
        return View();
    }

    public IActionResult SumRow(string row)
    {
        var list = Model.FormList;
        var size = Model.MatrixSize;
        if (row == null || int.Parse(row) > size || int.Parse(row) < 1)
        {
            ViewBag.Size = size;
            return View("ErrorRow");
        }
        else
        {
            ViewBag.Row = row;
            ViewBag.Sum = SumRowFunction(list, size, row);
            return View();
        }
    }

    public IActionResult MatrixColumn()
    {
        return View();
    }

    public IActionResult SumColumn(string column)
    {
        var list = Model.FormList;
        var size = Model.MatrixSize;
        if (column == null || int.Parse(column) > size || int.Parse(column) < 1)
        {
            ViewBag.Size = size;
            return View("ErrorColumn");
        }
        else
        {
            ViewBag.Column = column;
            ViewBag.Sum = SumColumnFunction(list, size, column);
            return View();
        }
    }

    public FileResult Save()
    {
        return File(Encoding.ASCII.GetBytes(ConvertMatrixToString()), "text/plain", "moja_macierz.txt");
    }
}