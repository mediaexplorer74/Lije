
// Type: Geex.Run.GeexContentManager
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;


namespace Geex.Run
{
    internal class GeexContentManager : ContentManager
    {
        internal bool IsLoading;
        private Dictionary<string, object> loadedAssets = new Dictionary<string, object>();
        private Dictionary<string, IDisposable> loadedDisposableAssets = new Dictionary<string, IDisposable>();
        private Dictionary<string, int> loadedAssetReferenceCount = new Dictionary<string, int>();

        public GeexContentManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override T Load<T>(string assetName) => this.Load<T>(assetName, (short)0);

        public T Load<T>(string assetName, short loadType)
        {
            this.IsLoading = true;
            if (loadType != (short)1 && this.loadedAssets.ContainsKey(assetName))
            {
                ++this.loadedAssetReferenceCount[assetName];
                this.IsLoading = false;
                return (T)this.loadedAssets[assetName];
            }
            T obj = this.ReadAsset<T>(assetName, new Action<IDisposable>(this.RecordDisposableAsset));
            this.loadedAssets[assetName] = (object)obj;
            this.loadedAssetReferenceCount[assetName] = 1;
            if ((object)obj is IDisposable)
                this.loadedDisposableAssets[assetName] = (object)obj as IDisposable;
            this.IsLoading = false;
            return obj;
        }

        public T ForcedLoad<T>(string assetName) => this.Load<T>(assetName, (short)1);

        public override void Unload()
        {
            foreach (IDisposable disposable in this.loadedDisposableAssets.Values)
                disposable.Dispose();
            this.loadedAssets.Clear();
            this.loadedDisposableAssets.Clear();
            this.loadedAssetReferenceCount.Clear();
        }

        public bool Unload(string assetName)
        {
            if (!this.loadedAssetReferenceCount.ContainsKey(assetName))
                return false;
            if (this.loadedAssetReferenceCount[assetName] == 1 && this.loadedDisposableAssets.ContainsKey(assetName))
            {
                this.loadedDisposableAssets[assetName].Dispose();
                this.loadedAssets.Remove(assetName);
                this.loadedDisposableAssets.Remove(assetName);
                this.loadedAssetReferenceCount.Remove(assetName);
                return true;
            }
            --this.loadedAssetReferenceCount[assetName];
            return false;
        }

        private void RecordDisposableAsset(IDisposable disposable)
        {
        }
    }
}
