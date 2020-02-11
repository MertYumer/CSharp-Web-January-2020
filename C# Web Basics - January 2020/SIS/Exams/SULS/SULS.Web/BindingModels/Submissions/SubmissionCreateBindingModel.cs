﻿namespace SULS.Web.BindingModels.Submissions
{
    using SIS.MvcFramework.Attributes.Validation;

    public class SubmissionCreateBindingModel
    {
        private const string CodeErrorMessage = "Code must be between than 50 and 800.";

        [RequiredSis]
        [StringLengthSis(30, 800, CodeErrorMessage)]
        public string Code { get; set; }
    }
}
