namespace TheBlindEye.Utility.LoaderObject
{
    public class Error01 : ErrorMessage
    {
        #if UNITY_EDITOR
            protected override bool IsLogError { get; } = false;
            protected sealed override string Message { get; set; }
        #endif
    
        public Error01(string gameObjectName) : base(AssetName.LoaderObject)
        {
            #if UNITY_EDITOR
                Message = $"{COLOR_RED}No Data found to load.{COLOR_END} " +
                          $"{COLOR_YELLOW}LoadData function needs to be called on the {gameObjectName} gameObject. (in Awake){COLOR_END}";
            #endif
        }
    }
}