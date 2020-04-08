using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace SSS.CommonUtilityService
{
    public static class CommonUtilityManager
    {
        private static IUnityContainer container;
        public static IUnityContainer UnityContainer
        {
            get
            {
                if (container == null)
                {
                    container = new UnityContainer();
                }
                return container;
            }
        }

    }
}
