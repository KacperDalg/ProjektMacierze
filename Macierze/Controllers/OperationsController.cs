using ExcelDataReader;
using MacierzeTest.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting;

namespace MacierzeTest.Controllers
{
    public class OperationsController : Controller
    {
        public static MatrixModel Model = new MatrixModel();

        [HttpPost]
        public RedirectToActionResult MatrixCheck(IFormCollection collection)
        {
            List<string> List = new List<string>();

            //SPRAWDZENIE CZY NIE WYSTĘPUJE WARTOŚĆ NULL
            foreach (var item in collection)
            {
                if (item.Value != "")
                {
                    List.Add(item.Value);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            List.RemoveAt(List.Count - 1);

            //SPRAWDZENIE WYMIARÓW MACIERZY

            int size = 0;

            for (int i = 2; i <= 10; i++)
            {
                if (i * i == List.Count)
                {
                    size = i;
                    break;
                }
            }

            if (List.Count < 4 || List.Count > 100)
            {
                return RedirectToAction("ErrorMatrixSize", "Operations");
            }
            else
            {
                Model.MatrixSize = size;
                Model.FormList = List;
                return RedirectToAction("Options", "Operations");
            }
        }

        [HttpPost]
        public IActionResult UploadFile(IFormFile file)
        {
            List<string> numbers = new List<string>();

            if (file.FileName.Contains(".txt") || file.FileName.Contains(".csv"))
            {
                using (var stream = new StreamReader(file.OpenReadStream()))
                {
                    var data = stream.ReadToEnd();
                    List<string> content = (file.FileName.Contains(".txt")) ? data.Replace("\n", " ").Replace("\r", "").Split(" ").ToList() : data.Replace("\n", ";").Replace("\r", "").Split(";").ToList();
                    foreach (var item in content)
                    {
                        if (item != null && item != "" && item.Length > 0)
                        {
                            numbers.Add(item);
                        }
                    }
                    if (Math.Sqrt(Convert.ToDouble(numbers.Count)) % 1 != 0 || numbers.Count>100 || numbers.Count < 4)
                    {
                        return View("ErrorMatrixSize");
                    }
                }
                Model.FormList = numbers;
                Model.MatrixSize = Convert.ToInt32(Math.Sqrt(Convert.ToDouble(numbers.Count)));
                return View("Options");
            }
            return View("~/Views/Home/Index.cshtml");
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
            var size = Model.MatrixSize;
            var List = Model.FormList;
            int sum = 0;
            for (int i = 0; i < List.Count; i++)
            {
                sum += int.Parse(List[i]);
                i += size;
            }
            ViewBag.Sum = sum;
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
                int sum = 0;
                for (int i = (int.Parse(row) - 1) * size; i < (int.Parse(row) - 1) * size + size; i++)
                {
                    sum += int.Parse(list[i]);
                }
                ViewBag.Row = row;
                ViewBag.Sum = sum;
                return View();
            }

        }

        public IActionResult MatrixColumn()
        {
            return View();
        }

        public IActionResult SumColumn(string col)
        {
            var list = Model.FormList;
            var size = Model.MatrixSize;
            if (col == null || int.Parse(col) > size || int.Parse(col) < 1)
            {
                ViewBag.Col = col;
                ViewBag.Size = size;
                return View("ErrorColumn");
            }
            else
            {
                int sum = 0;
                for (int i = (int.Parse(col) - 1); i <= size * size - size + int.Parse(col); i += size)
                {
                    sum += int.Parse(list[i]);
                }

                ViewBag.Column = col;
                ViewBag.Sum = sum;
                return View();
            }
        }

        public FileResult Save()
        {
            string s = "";
            int i = 0;
            int x = 1;
            foreach (var item in Model.FormList)
            {
                s += item + " \t\t";
                i += 1;
                if (Model.MatrixSize * x == i)
                {
                    s += Environment.NewLine;
                    x += 1;
                }
            }

            return File(Encoding.ASCII.GetBytes(s), "text/plain", "moja_macierz.txt");
        }
    }
}
