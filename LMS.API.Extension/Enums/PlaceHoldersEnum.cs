using System;
using System.Collections.Generic;
using System.Text;
    
namespace LMS.API.Extensions.Enums
{
    [DescriptiveEnumEnforcement(DescriptiveEnumEnforcementAttribute.EnforcementTypeEnum.ThrowException)]

    public enum PlaceHoldersEnum
    {


        [Description("Data")]
        Data,
        [Description("")]
        Default,
        [Description("")]
        CompleteMessage,
        [Description("User")]
        User,
        [Description("Organization")]
        Organization,
        [Description("Farm")]
        Farm,
        [Description("Colormap")]
        Colormap,
        [Description("Image Process Status")]
        ImageProcess,
        [Description("Metadata For Crop Health")]
        CropMetaData,
        [Description("Control Parameters")]
        ControlParameters,
        [Description("Crop Polygon")]
        CropPolygon,
        [Description("Crop Sub Area Ridges")]
        CropSubAreaRidge,
        [Description("Crop Health Details")]
        CropHealthDetails,
        [Description("Germination Details")]
        GerminationDetails,
        [Description("Farm Event Dropdowns")]
        EventDropdowns,
        [Description("Farm Feature")]
        FarmFeature,

        [Description("Soil Test Event")]
        SoilLabResultEvent,
        [Description("Weed Control Event")]
        WeedControl,
        [Description("Soil Amendments Event")]
        SoilAmendments,
        [Description("Crop Ridge Event")]
        CropRidge,
        [Description("Irrigation Event")]
        Irrigation,
        [Description("Pest Control Event")]
        PestControl,
        [Description("Disease Control Event")]
        DiseaseControl,
        [Description("Metadata Event")]
        Metadata,
        [Description("Harvest Event")]
        HarvestEvent,
        [Description("Micronutrients Event")]
        Micronutrients,
        [Description("Fertilizer Event")]
        Fertilizer,
        [Description("Plant Population Event")]
        PlantPopulation,
        [Description("Crop Health Event")]
        CropHealth,
        [Description("Land Preparation Event")]
        LandPreparation,
        [Description("Water Test Event")]
        WaterTest,
        [Description("Pre-land Preparation Event")]
        Prelandprep,








        [Description("Potato Event")]
        PotatoEvent,
        [Description("Maize Seed Test Event")]
        MaizeSeedTest,


        [Description("PlantCount")]
        PlantCount,

        [Description("PotatoSample")]
        Potato,
        [Description("SoilTest List")]
        SoilTestList,
        [Description("SoilTest List cannot be deleted")]
        SoilTestLisdeleted,
        [Description("SoilTest Sample")]
        SoilSample,
        [Description("SoilTest Result")]
        SoilTestResult,
        [Description("SoilTest Scan")]
        SoilTestScan,
        [Description("SoilLabTest List")]
        SoilLabTestList,
        [Description("SoilLab Sample")]
        SoilLabSample,
        [Description("SoilLab Result")]
        SoilLabResult,
        [Description("SoilLab Scan")]
        SoilLabScan,
        [Description("PestDetector Sample")]
        Pest,
        [Description("Pest List")]
        Pestist,
        [Description("Moisture Test Sample")]
        MoistureTest,
        [Description("Moisture Test Scan")]
        MoistureTestScan,
        [Description("CropLabelling Task")]
        CroplabelTask,
        [Description("Crop")]
        Crop,
        [Description("Role")]
        Role,
        [Description("Rights")]
        RoleRights,
        [Description("Data template")]
        DataTemplate,
        [Description("Treatment")]
        Treatment,
        [Description("Feed")]
        Feed,
        [Description("Client mapping")]
        ClientMapping,
        [Description("Site")]
        Site,
        [Description("Report Configuration")]
        ConfigureReport,
        [Description("Report Group")]
        ReportGroup,
        [Description("Report access rights")]
        ReportAccessRights,
        [Description("Alert Configuration")]
        ConfigureAlert,
        
        [Description("Alert Settings")]
        PiperAlertSetting,
        [Description("Complex cycling configuration")]
        ComplexCyclingConfiguration,
        [Description("Installation Run")]
        InstallationRun,
        [Description("Listeria Config")]
        ListeriaConfig,
        [Description("Intervention Treatment")]
        Intervention,
        [Description("Intervention Feed")]
        InterventionFeed,
        [Description("Vaccine")]
        Vaccine,
        [Description("Feed Program")]
        FeedProgram,
        [Description("Administration Method")]
        AdministrationMethod,
        [Description("Active Ingredients")]
        ActiveIngredients,
        [Description("Intervention Manufacturer")]
        InterventionManufacturer,
        [Description("Brand Name")]
        BrandName,
        [Description("Category")]
        Category,
        [Description("Feed Type")]
        FeedType,
        [Description("Feeding Program")]
        FeedingProgram,

        [Description("Starter")]
        Starter,
        [Description("Grower")]
        Grower,
        [Description("Finisher")]
        Finisher,
        [Description("WD1")]
        WD1,
        [Description("WD2")]
        WD2,




        [Description("MPN Settings")]
        MpnSettings,
        [Description("Threshold Settings")]
        thresholdSettings,
        [Description("Manage UserApp Improc")]
        manageUserAppImproc,
        [Description("Sample Matrix")]
        sampleMatrix,
        #region Messages 
        [Description("Errors found in ")]
        DataUploadERRORMSG01,

        [Description("User agreement")]
        Eula


        #endregion


    }
}
