
namespace SimpleMvc.Framework
{
    public class MvcContext
    {
        private static MvcContext instance;

        private MvcContext()
        {
        }

        public static MvcContext Get
        {
            //=> instance == null ? (instance = new MvcContext()) :instance;
            get
            {
                if (instance == null)
                {
                    instance = new MvcContext();
                }
                return instance;
            }           
        }

        public string AssemblyName { get; set; }

        public string ControllersFolder { get; set; } = "Controllers";

        public string ContrellerSuffix { get; set; } = "Controller";

        public string ViewFolders { get; set; } = "Views";

        public string ModelsFolder { get; set; } = "Models";
    }
}
