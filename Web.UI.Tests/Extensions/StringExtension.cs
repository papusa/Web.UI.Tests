using TechTalk.SpecFlow;

namespace Web.UI.Tests.Extensions {
    static class StringExtension {
        public static string ContactenateTimeStamp(this string str, ScenarioContext scenarioContext) {
            return $"{str} {scenarioContext.GetTimeStamp()}";
        }

        public static bool EmptyValueToEnter(this string str) {
            return str.ToLower() == "$empty.value";
        }

        public static bool ToBool(this string str) {
            return str.ToLower().Equals("true") || str.ToLower().Equals("x") || str.ToLower().Equals("yes");
        }
    }
}
