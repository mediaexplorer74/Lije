
// Type: Geex.Run.Resources
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;


namespace Geex.Run
{

  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
    internal class Resources
    {
        private static ResourceManager resourceMan;
        private static CultureInfo resourceCulture;

        internal Resources()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager
        {
            get
            {
                if (resourceMan == null)
                    resourceMan = new ResourceManager("Geex.Run.Resources", typeof(Resources).Assembly);
                return resourceMan;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get => resourceCulture;
            set => resourceCulture = value;
        }

        internal static byte[] Arial
        {
            get
            {
                return (byte[])ResourceManager.GetObject(nameof(Arial), resourceCulture);
            }
        }

        internal static byte[] Geex
        {
            get
            {
                return (byte[])ResourceManager.GetObject(nameof(Geex), resourceCulture);
            }
        }

        internal static byte[] GeexLogo
        {
            get
            {
                return (byte[])ResourceManager.GetObject(nameof(GeexLogo), resourceCulture);
            }
        }

        internal static byte[] GeexShader
        {
            get
            {
                return (byte[])ResourceManager.GetObject(nameof(GeexShader), resourceCulture);
            }
        }

        internal static byte[] Transition
        {
            get
            {
                return (byte[])ResourceManager.GetObject(nameof(Transition), resourceCulture);
            }
        }
    }
}
