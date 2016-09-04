﻿using System;
using Dark_Launcher.Constants;
using System.Xml;
using Dark_Launcher.Settings;

namespace Dark_Launcher.Management
{
    internal class LauncherConfigurationsManager
    {
        public void LoadInternalConfigs(string xml)
        {
            try
            {
                XMLManager xmlManager = new XMLManager();
                xmlManager.InitializeFromFile(xml);

                XmlNode internalConfigsNode = xmlManager.GetNode("internalConfigs");
                FTPSettings.LauncherVersion = internalConfigsNode.SelectSingleNode("version").InnerText;
                FTPSettings.UpdaterURL = internalConfigsNode.SelectSingleNode("launcherUpdateLink").InnerText;
                FTPSettings.ClientMirrorURL = internalConfigsNode.SelectSingleNode("clientMirror").InnerText;
                FTPSettings.FileListURL = internalConfigsNode.SelectSingleNode("fileListURL").InnerText;

                XmlNode newsNode = internalConfigsNode.SelectSingleNode("news");
                FTPSettings.ForumURL = newsNode.SelectSingleNode("forumURL").InnerText;
                FTPSettings.XMLNewsURL = newsNode.SelectSingleNode("xmlNewsUrl").InnerText;


                LauncherSettings.IsOnMaintenance = bool.Parse(internalConfigsNode.SelectSingleNode("maintenance").InnerText);

                if (!HasLoadedAllInternalConfigs(FTPSettings.LauncherVersion, FTPSettings.UpdaterURL, FTPSettings.ClientMirrorURL, FTPSettings.FileListURL, FTPSettings.ForumURL, FTPSettings.XMLNewsURL))
                {
                    LogManager.WriteLog("Not all internal configs was loaded correctly.");
                }

#if DEBUG
                System.Diagnostics.Debug.Print("Internal configs loaded sucessfully");
#endif
            }
            catch (Exception er)
            {
#if DEBUG
                System.Diagnostics.Debug.Print(er.Message);
#endif
                LogManager.WriteLog("Error on load internal configs: " + er.Message);
            }

        }

        private bool HasLoadedAllInternalConfigs(params string[] strs)
        {
            foreach (string str in strs)
            {
                if (str == string.Empty || str == null)
                {
                    LogManager.WriteLog($"Internal config {str} is empty or null. ", LogManager.LogType.WARN);
                    return false;
                }
            }

            return true;

        }


    }
}