using System.IO;
using System.Reflection;
using UnityEngine;

namespace Modding.Humankind.DevTools.DeveloperTools
{
    public class AssetLoader
    {
        public string ManifestResourceName { get; set; }
        
        public Assembly Assembly { get; set; }
        
        private AssetBundle _assetBundle;

        public AssetBundle AssetBundle
        {
            get
            {
                if (_assetBundle == null)
                    _assetBundle = CreateAssetBundleFrom(ManifestResourceName, Assembly);

                return _assetBundle;
            }
        }

        public AssetLoader()
        {
            
        }
        
        public T Load<T>(string name) where T : UnityEngine.Object
        {
            T asset = AssetBundle.LoadAsset<T>(name);
            asset.hideFlags = HideFlags.HideAndDontSave;
            UnityEngine.Object.DontDestroyOnLoad(asset);
            
            return asset;
        }

        public void Unload(bool unloadAllLoadedObjects)
        {
            if (_assetBundle != null) {
                AssetBundle.Unload(unloadAllLoadedObjects);
            }
        }

        private static AssetBundle CreateAssetBundleFrom(string resourceName, Assembly assembly) =>
            AssetBundle.LoadFromMemory(ReadFully(assembly.GetManifestResourceStream(resourceName)));

        private static byte[] ReadFully(Stream input)
        {
            using (var ms = new MemoryStream())
            {
                byte[] buffer = new byte[81920];
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) != 0)
                    ms.Write(buffer, 0, read);
                return ms.ToArray();
            }
        }
    }

    
}
