using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.API.Extensions.Enums
{
    [DescriptiveEnumEnforcement(DescriptiveEnumEnforcementAttribute.EnforcementTypeEnum.ThrowException)]
    public enum LookUpGroupEnum
    {

        [Description("00001")]
        OrganizationsTypes,
        [Description("00002")]
        SiteTypes,
        [Description("00004")]
        ColumnTypes,
        [Description("00005")]
        AlertTypeEnum,
        [Description("00006")]
        AlertInterfaceEnum,
        [Description("00008")]
        AlertImprocType,
        [Description("00009")]
        DataSourceType,
        [Description("00010")]
        AggregateCalFuncType,
        [Description("00011")]
        AlertModeType,
        [Description("00012")]
        AlertConditionType,
        [Description("00013")]
        AlertFrequencyType,
        [Description("00014")]
        alertReportTypes,
        [Description("00015")]
        alertThresholdTypes,
        [Description("00016")]
        absenceDelayFactor,

        [Description("00018")]
        userEulaStatus,


        [Description("00019")]
        mpnSampleMatrix,
        [Description("00024")]
        stringBasedAlert,
        [Description("00026")]
        opgType,
        [Description("00027")]
        cyclingInterval,
        [Description("00029")]
        coccInterventionVaccineLive,
        [Description("00082")]
        TehsilIds
    }

    public enum ColTypeEnum
    {
        [Description("100")]
        DateTime,
        [Description("101")]
        String,
        [Description("102")]
        Number,
        [Description("103")]
        Time,
        [Description("104")]
        Integer
    }
    public enum TemplateID
    {
        [Description("14")]
        SampleMetaData,
        [Description("17")]
        FlockMetaData,
        [Description("18")]
        WeeklyPerformance,
        [Description("39")]
        SiteBulkUpload
    }
    public enum AlertTypeEnum
    {
        [Description("105")]
        ContaiminationWarning,

    }
    public enum AlertInterfaceEnum
    {
        [Description("106")]
        Email,
    }
    public enum SiteTypeEnum
    {
        [Description("5")]
        CorporateOffice,
        [Description("6")]
        Farms,
        [Description("7")]
        Houses,
        [Description("8")]
        ProcessingPlants,
        [Description("9")]
        Integrator,
        [Description("10")]
        Region,
        [Description("11")]
        FeedMill,
        [Description("12")]
        Hatchery,
        [Description("13")]
        Laboratory,
        [Description("14")]
        Default
    }
    public enum ContaminationType
    {
        [Description("155")]
        Salmonella,
        [Description("156")]
        Coccidia
    }
    public enum stagingProc
    {
        [Description("stpInsertAlert")]
        InsertAlert,
        [Description("")]
        UpdateDataBI
    }

    public enum userAppImproc
    {
        [Description("702")]
        UserApp,
        [Description("703")]
        ImprocSalm,
        [Description("704")]
        ImprocCocc,
        [Description("705")]
        Listeria,
        [Description("ImprocSalm01")]
        Salmonella,
        [Description("ImprocCocc01")]
        Coccidia,
        [Description("PiperUserApp")]
        PiperUserApp
    }

    public enum piperStatus
    {
        [Description("Supported")]
        supported,
        [Description("Warning")]
        warning,
        [Description("Not Supported")]
        notSupported,
        [Description("Unknown")]
        unknown,
    }
    public enum numbers
    {
        [Description("1")]
        one,
        [Description("2")]
        two,

    }


    public enum AlertProc
    {
        [Description("stpStageAlertStatusUpdate")]
        StageAlertStatusUpdate,
        
    }

    public enum interventionTypeEnum
    {
        [Description("750")]
        feed,
        [Description("751")]
        treatment,
        [Description("752")]
        feedProgram,
        [Description("753")]
        vaccine
    }
    public enum eventType
    {
        [Description("user_deleted")]
        UserDeleted,
        [Description("user_made_inactive")]
        UserInactive,
        [Description("user_orgn_updated")]
        UserOrgnUpdated,
        [Description("client_orgn_updated")]
        ClientOrgUpdated,
        [Description("orgn_deleted")]
        OrgnDeleted,
        [Description("orgn_made_inactive")]
        OrgnInactive,
        [Description("site_deleted")]
        SiteDeleted,
        [Description("user_sites_updated")]
        SiteUpdated
    }
    public enum flockTemplateEnum
    {
        [Description("PLACEMENT_DATE")]
        PLACEMENT_DATE,
        [Description("PROCESSING_DATE")]
        PROCESSING_DATE,
        [Description("HATCH_DATE")]
        HATCH_DATE,
        [Description("NUM_BIRDS_PLACED")]
        NUM_BIRDS_PLACED
,
        [Description("FARM_SITE_ID")]
        SITE_ID,

        [Description("UNIQUE_FLOCK_ID")]
        UNIQUE_FLOCK_ID,
        
        [Description("PROCESSING_SITE_ID")]
        PROCESSING_SITE_ID,
        [Description("NUM_BIRDS_DOA_PLANT")]
        NUM_BIRDS_DOA_PLANT,
        [Description("NUM_BIRDS_PROCESSED")]
        NUM_BIRDS_PROCESSED,

        [Description("BIRD_WEIGHT_CONDEMNED_LB")]
        BIRD_WEIGHT_CONDEMNED_LB
,
        [Description("BIRD_WEIGHT_CONDEMNED_KG")]
        BIRD_WEIGHT_CONDEMNED_KG,
        [Description("PARTS_WEIGHT_CONDEMNED_LB")]
        PARTS_WEIGHT_CONDEMNED_LB,
        [Description("PARTS_WEIGHT_CONDEMNED_KG")]
        PARTS_WEIGHT_CONDEMNED_KG,

        [Description("TOTAL_WEIGHT_CONDEMNED_LB")]
        TOTAL_WEIGHT_CONDEMNED_LB,
        [Description("TOTAL_WEIGHT_CONDEMNED_KG")]
        TOTAL_WEIGHT_CONDEMNED_KG,
        [Description("17")]
        template_ID = 17,
        [Description("Flock Metadata")]
        flock_metadata,
        [Description("BIRD_BREED")]
        BIRD_BREED,
        [Description("BIRD_TYPE")]
        BIRD_TYPE,

        [Description("INTEGRATOR_FLOCK_ID")]
        INTEGRATOR_FLOCK_ID,

        [Description("OUT_TIME_DAYS")]
        OUT_TIME_DAYS,

        [Description("USDA_PLANT_ID")]
        USDA_PLANT_ID
    }

    public enum CropTemplateEnum
    {
        [Description("landPrepration")]
        LAND_PREPRATION,

        [Description("sowingMethod")]
        SOWING,
    }
    public enum bulkSiteUpload
    {
        [Description("PARENT_SITE_ID")]
        PARENT_SITE_ID,
        [Description("SITE_TYPE")]
        SITE_TYPE,
        [Description("SITE_NAME")]
        SITE_NAME,
        [Description("COUNTRY")]
        COUNTRY,
        [Description("STATE")]
        STATE,
        [Description("CITY")]
        CITY,
        [Description("39")]
        template_ID = 39,
    }
    public enum sitePerformanceTemplateEnum
    {
        [Description("SITE_ID")]
        SITE_ID,
        [Description("NUM_BIRDS_PLACED")]
        NUM_BIRDS_PLACED,
        [Description("NUM_BIRDS_SOLD")]
        NUM_BIRDS_SOLD,
        [Description("DOA_PLANT_PERC")]
        DOA_PLANT_PERC,
        [Description("MORTALITY_NUM_BIRDS")]
        MORTALITY_NUM_BIRDS,
        [Description("FCR")]
        FCR,
        [Description("NUM_BIRDS_CONDEMNED_WHOLE")]
        NUM_BIRDS_CONDEMNED_WHOLE,
        [Description("BIRD_WEIGHT_CONDEMNED_LB")]
        BIRD_WEIGHT_CONDEMNED_LB,
        [Description("BIRD_WEIGHT_CONDEMNED_KG")]
        BIRD_WEIGHT_CONDEMNED_KG,
        [Description("PART_WEIGHT_CONDEMNED_LB")]
        PART_WEIGHT_CONDEMNED_LB,
        [Description("PART_WEIGHT_CONDEMNED_KG")]
        PART_WEIGHT_CONDEMNED_KG,

        [Description("TOTAL_WEIGHT_CONDEMNED_LB")]
        TOTAL_WEIGHT_CONDEMNED_LB,
        [Description("TOTAL_WEIGHT_CONDEMNED_KG")]
        TOTAL_WEIGHT_CONDEMNED_KG,
        [Description("18")]
        template_ID = 18,

    }
    public enum sampleMetaDataTemplateEnum
    {
        [Description("SITE_ID")]
        SITE_ID,

        [Description("14")]
        template_ID = 14,

    }

    public enum BankingReportImageryDataEnum
    {
        [Description("16901")]
        InActive,

        [Description("16902")]
        Downloading,

        [Description("16903")]
        Ready,

        [Description("16904")]
        Processing,

        [Description("16905")]
        Processed,

        [Description("16906")]
        Fail
    }
}
