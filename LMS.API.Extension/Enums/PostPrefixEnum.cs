using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.API.Extensions.Enums
{
    [DescriptiveEnumEnforcement(DescriptiveEnumEnforcementAttribute.EnforcementTypeEnum.ThrowException)]

    public enum PostPrefixEnum
    {
        [Description("00115$$Extended$$")]
        ExtendedException_IELogsAPI,
        [Description("$$Extended$$")]
        ExtendedException,

        [Description("{")]
        RightSideSymbol,
        [Description("}")]
        LeftSideSymbol,

        [Description("StagingDBSentError")]
        ERRORStagingDBInvoke,

        [Description("StagingDBSentError_RuntimeStagingDBSentError_Runtime")]
        ERRORStagingDBInvokeFromRuntime,

        [Description("StagingDBError_ClientFileUpload")]
        ERRORStagingDBInvokeFileUpload,

        [Description("Runtime Event ID::")]
        Runtime_Event_ID,

        [Description(" ")]
        PhoneCodeSeparator,

        [Description("(")]
        leftParanthesis,
        [Description(")")]
        rightParanthesis

    }
}
