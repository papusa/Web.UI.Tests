using TechTalk.SpecFlow;

namespace Web.UI.Tests.Steps
{
    [Binding]
    public sealed class StepArgumentsTransforms
    {
        [StepArgumentTransformation(@"(available|not available)")]
        public bool Availability(string availability)
        {
            return availability == "available";
        }
    }
}
