namespace ApiTemplate.Constants
{
    public static class PaidPackagesControllerRoute
    {
        public const string GetPaidPackage = ControllerName.PaidPackage + nameof(GetPaidPackage);
        public const string GetPublicPaidPackage = ControllerName.PaidPackage + nameof(GetPublicPaidPackage);
        public const string GetAllPaidPackage = ControllerName.PaidPackage + nameof(GetAllPaidPackage);
        public const string GetAllPublicPaidPackage = ControllerName.PaidPackage + nameof(GetAllPublicPaidPackage);
        public const string PostPaidPackage = ControllerName.PaidPackage + nameof(PostPaidPackage);
        public const string PutPaidPackage = ControllerName.PaidPackage + nameof(PutPaidPackage);
        public const string DeletePaidPackage = ControllerName.PaidPackage + nameof(DeletePaidPackage);
    }
}