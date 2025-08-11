namespace Linn.Common.Configuration
{
    public static class AwsCredentialsConfiguration
    {
        public static string? AccessKey => ConfigurationManager.Configuration["awsAccessKey"];

        public static string? SecretKey => ConfigurationManager.Configuration["awsSecretKey"];

        public static string? Region => ConfigurationManager.Configuration["awsRegion"];
    }
}
