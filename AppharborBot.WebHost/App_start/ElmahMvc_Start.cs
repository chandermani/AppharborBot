[assembly: WebActivator.PreApplicationStartMethod(typeof(AppharborBot.WebHost.App_Start.ElmahMvc), "Start")]
namespace AppharborBot.WebHost.App_Start
{
    public class ElmahMvc
    {
        public static void Start()
        {
            Elmah.Mvc.Bootstrap.Initialize();
        }
    }
}