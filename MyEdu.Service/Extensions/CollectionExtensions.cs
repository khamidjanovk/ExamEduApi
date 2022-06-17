using Microsoft.AspNetCore.Mvc;
using MyEdu.Domain.Configurations;
using MyEdu.Service.Helpers;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Linq;

namespace MyEdu.Service.Extensions
{
    public static class CollectionExtensions
    {
        public static IQueryable<T> ToPagedList<T>(this IQueryable<T> source, PaginationParams @params)
        {
            var metaData = new PaginationMetaData(source.Count(), @params);
            var json = JsonConvert.SerializeObject(metaData);

            if (HttpContextHelper.ResponseHeaders.Keys.Contains("X-Pagination"))
                HttpContextHelper.ResponseHeaders.Remove("X-Pagination");

            HttpContextHelper.Context.Response.Headers.Add("X-Pagination", json);

            return @params.PageSize > 0 && @params.PageIndex >= 0
                ? source.Skip(@params.PageSize * (@params.PageIndex - 1)).Take(@params.PageSize) : source;
        }

        public static FileStreamResult ExportToExcel<T>(this IQueryable<T> source)
        {
            var stream = new MemoryStream();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");

                workSheet.TabColor = System.Drawing.Color.Black;
                workSheet.CustomHeight = true;
                workSheet.DefaultColWidth = 20;
                
                workSheet.Row(1).Height = 20;
                workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(1).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                workSheet.Row(1).Style.Font.Bold = true;

                workSheet.Cells.LoadFromCollection(source, true);

                package.Save();
            }

            stream.Position = 0;

            string excelName = "Data.xlsx";

            var result = new FileStreamResult(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = excelName
            };

            return result;
        }
    }
}