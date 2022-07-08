/*
 Author:du
 Time:2016.11.8
*/

using System.IO;
using UnityEditor;
using UnityEngine;

namespace GameEditor
{
    public static class AssetsSyncTool
    {
        [MenuItem("[GameProject]/Res/AssetsSync/同步2D骨骼动画", false, 13)]
        public static void SyncSkeleton()
        {
            string formDir = Application.dataPath + "/GameMain/SpineAssets";
            string toDir = "Assets" + "/GameMain/Spine/";
            CopyPrefabsToDir(formDir, toDir);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("[AssetsSyncTool]同步骨骼动画完成");
        }

        public static void CopyPrefabsToDir(string formDir, string toDir)
        {
            ClearDirectory(toDir);
            DirectoryInfo formfolder = new DirectoryInfo(formDir);
            FileSystemInfo[] folders = formfolder.GetFileSystemInfos();
            foreach (FileSystemInfo info in folders)
            {
                if (info is DirectoryInfo)
                {
                    FileSystemInfo[] assetFiles = (info as DirectoryInfo).GetFileSystemInfos();
                    foreach (FileSystemInfo assetfile in assetFiles)
                    {
                        CopyPrefabToDir(assetfile, toDir);
                    }
                }
                else
                {
                    CopyPrefabToDir(info, toDir);
                }
            }
            AssetDatabase.Refresh();
        }

        public static void ClearDirectory(string dir)
        {
            if (Directory.Exists(dir))
                Directory.Delete(dir, true);
            Directory.CreateDirectory(dir);
        }

        public static void CopyPrefabToDir(FileSystemInfo info, string toDir)
        {
            if (!(info is DirectoryInfo) && info.Name.EndsWith(".prefab"))
            {
                File.Copy(info.FullName, toDir + "/" + info.Name, true);
            }
        }
    }
}