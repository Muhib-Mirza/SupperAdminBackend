using LMS.API.Extensions.Enums;

using System;
using System.Collections.Generic;
using System.Text;

namespace APL.API.Extensions.Enums
{
    [DescriptiveEnumEnforcement(DescriptiveEnumEnforcementAttribute.EnforcementTypeEnum.ThrowException)]

    public enum ApplicationSettingsEnum
    {
        [Description("AppSettings:Token")]
        TOKEN

    }
}
