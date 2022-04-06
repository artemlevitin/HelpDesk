using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelpDesk_AdminLte.Utilites
{
    public static class Utils
    {
        public static SelectList AddFirstItem(SelectList origList)
        {
            var firstItem = new SelectListItem() { Value = "", Text = "Выберите из списка" };
            List<SelectListItem> newList = origList.ToList();
            newList.Insert(0, firstItem);

            var selectedItem = newList.FirstOrDefault(item => item.Selected);
            var selectedItemValue = String.Empty;
            if (selectedItem != null)
            {
                selectedItemValue = selectedItem.Value;
            }

            return new SelectList(newList, "Value", "Text", selectedItemValue);
        }
    }
}