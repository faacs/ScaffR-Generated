using System;

namespace DemoApplication.Models.Wizard
{
    using System.Web.Mvc;

    public class WizardAttribute : Attribute, IMetadataAware
    {
        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.TemplateHint = "Wizard";            
        }
    }

    public class WizardStepAttribute : Attribute, IMetadataAware
    {
        private string _title;

        public WizardStepAttribute(string title)
        {
            _title = title;
        }

        public string SomeOtherPropertyICareAbout { get; set;  }

        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.AdditionalValues.Add("WizardStep", new object());
            metadata.DisplayName = _title;
        }
    }

    [Wizard]
    public class SampleWizardModel
    {
        public SampleWizardModel()
        {
            Step1 = new WizardStep();
            Step2 = new WizardStep();
        }

        [WizardStep("Step 1")]
        public WizardStep Step1 { get; set; }

        [WizardStep("Step 2")]
        public WizardStep Step2 { get; set; }
    }

    public class WizardStep
    {
        public string Hi { get; set; }
    }
}