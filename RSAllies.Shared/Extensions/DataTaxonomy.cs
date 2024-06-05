using Microsoft.Extensions.Compliance.Classification;

namespace RSAllies.Shared.Extensions
{
    public static class DataTaxonomy
    {
        private static string TaxonomyName { get; } = typeof(DataTaxonomy).FullName!;
        public static DataClassification SensitiveData { get; } = new DataClassification(TaxonomyName, nameof(SensitiveData));
        public static DataClassification PiiData { get; } = new DataClassification(TaxonomyName, nameof(PiiData));
        public static DataClassification SystemData { get; } = new DataClassification(TaxonomyName, nameof(SystemData));
    }

    public class SensitiveDataAttribute() : DataClassificationAttribute(DataTaxonomy.SensitiveData);

    public class PersonalIdentifiableInformationAttribute() : DataClassificationAttribute(DataTaxonomy.PiiData);

    public class SystemDataAttribute() : DataClassificationAttribute(DataTaxonomy.SystemData);
}
