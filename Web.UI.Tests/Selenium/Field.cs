using OpenQA.Selenium;

namespace Web.UI.Tests.Selenium {
    internal enum ElementType {
        Input,
        Select,
        Checkbox,
        Label,
        RadioButton,
        Calendar,
        Button
    }

    internal class Field {
        public ElementType FieldType { get; set; }

        public string Name { get; set; }

        public By Locator { get; set; }
    }
}
