namespace ASPNET_WebApp.Core.Constants
{
    public static class DataValidationConstants
    {
        public const string MustBeBetween = "{0} must be between {2} and {1} characters.";

        public const int AnimeNameMaxLength = 120;
        public const int AnimeNameMinLength = 8;

        public const int ImageUrlMaxLength = 250;
        public const int ImageUrlMinLength = 20;

        public const int DescriptionAnimeMaxLength = 2000;
        public const int DescriptionAnimeMinLength = 20;

        public const int MaxLength2000 = 2000;
        public const int MaxLength200 = 200;
        public const int MinLength10 = 10;
    }
}
