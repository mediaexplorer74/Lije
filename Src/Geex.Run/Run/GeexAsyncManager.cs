
// Type: Geex.Run.GeexAsyncManager
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using System.Collections.Generic;


namespace Geex.Run
{
  public sealed class GeexAsyncManager
  {
    private static List<GeexAsyncManager.Asset> aSyncLoadQueue = new List<GeexAsyncManager.Asset>();

    public static void InvokeAsyncMethod(
      GeexAsyncManager.GeexAsyncMethod method,
      params object[] args)
    {
      method.DynamicInvoke((object) args);
    }

    public static void AddAsyncMethod(GeexAsyncManager.GeexAsyncMethod f, params object[] args)
    {
      GeexAsyncManager.aSyncLoadQueue.Add(new GeexAsyncManager.Asset(f, args));
    }

    internal static void UpdateLoading()
    {
      while (GeexAsyncManager.aSyncLoadQueue.Count > 0)
      {
        GeexAsyncManager.InvokeAsyncMethod(GeexAsyncManager.aSyncLoadQueue[0].Method, GeexAsyncManager.aSyncLoadQueue[0].Args);
        GeexAsyncManager.aSyncLoadQueue.RemoveAt(0);
      }
    }

    public delegate void GeexAsyncMethod(params object[] args);

    private class Asset
    {
      public GeexAsyncManager.GeexAsyncMethod Method;
      public object[] Args;

      public Asset(GeexAsyncManager.GeexAsyncMethod method, params object[] args)
      {
        this.Method = method;
        this.Args = args;
      }
    }
  }
}
