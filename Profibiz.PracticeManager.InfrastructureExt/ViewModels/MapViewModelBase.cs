namespace DevExpress.DevAV.ViewModels {
    public abstract partial class MapViewModelBase {
        public const string WinBingKey = DevExpress.Map.Native.DXBingKeyVerifier.BingKeyWinOutlookInspiredApp;
        public const string WpfBingKey = DevExpress.Map.Native.DXBingKeyVerifier.BingKeyWpfOutlookInspiredApp;
        public virtual Address Address { get; set; }
        public static readonly Address DevAVHomeOffice = new Address
        {
            City = "Glendale",
            Line = "505 N. Brand Blvd",
            State = StateEnum.CA,
            ZipCode = "91203",
            Latitude = 34.1532866,
            Longitude = -118.2555815
        };
    }
}
