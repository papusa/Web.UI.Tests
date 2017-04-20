using System.Text.RegularExpressions;

namespace Web.UI.Tests.Helpers {
    class RegexHelper {
        public static string FindIgnoringCase(string pattern, string text) {
            return Find(pattern, text, RegexOptions.IgnoreCase);
        }

        public static string Find(string pattern, string text, RegexOptions option = RegexOptions.None) {
            var regex = new Regex(pattern, option);
            var match = regex.Match(text);
            return match.Success ? match.Value : null;
        }
    }
}
