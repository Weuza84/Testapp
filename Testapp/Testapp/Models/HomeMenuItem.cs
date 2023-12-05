using System;
using System.Collections.Generic;
using System.Text;

namespace Testapp.Models
{
    public enum MenuItemType
    {
        Browse,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }
        public string Text { get; set; }
    }
}
