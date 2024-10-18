using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.API.Extensions.Enums
{

    [DescriptiveEnumEnforcement(DescriptiveEnumEnforcementAttribute.EnforcementTypeEnum.ThrowException)]
    public enum ExceptionTypeEnum 
    {
        [Description("Information")]
        Information,
        [Description("Warning")]
        Warning,
        [Description("Business")]
        Business,
        [Description("Database")]
        Database,
        [Description("Authentication")]
        Authentication,
        [Description("Authorization")]
        Authorization,
        [Description("Communication")]
        Communication
    }
}
