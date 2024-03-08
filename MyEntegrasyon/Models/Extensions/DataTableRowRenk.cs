using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyEntegrasyon.Models.Extensions
{
    public static class DataTableRowRenk
    {
        public static string RenkClass(this IHtmlHelper htmlHelper, int TurId)
        {

            int guid_1 = 1;

            string Gonder = "";

            if (TurId == guid_1)  // Tanımsız
            {
                return "table-danger";
            }
            if (TurId == guid_1) // Akademi
            {
                return "table-danger";
            }
            if (TurId == guid_1) // İdari
            {
                return "table-info";
            }
            if (TurId == guid_1)  // Öğrenci
            {
                return "";
            }
            return Gonder;
        }
    }
}

