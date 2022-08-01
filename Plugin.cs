using BepInEx;
using System.IO;
using HarmonyLib;
using System;

namespace JSONBossDialogue
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        private const string PluginGuid = "Kel.inscryption.jsonbossdialogue";
        private const string PluginName = "JSONBossDialogue";
        private const string PluginVersion = "1.0.0.0";

        private void Awake()
        {
            Logger.LogInfo($"Loaded {PluginName} successfully!");

            Harmony harmony = new Harmony("kel.harmony.jsonbossdialogue");
            harmony.PatchAll();

            // LOAD JSON
            string directory = FileHandler.GetDirectory();
            string[] getFile = FileHandler.SearchDirectory(directory);

            JSONHandler dialogueObj;

            if (!FileHandler.isArrayEmpty(getFile))
            {
                if (!FileHandler.isArrayMany(getFile)) {
                    string filename = Path.GetFileName(getFile[0]);
                    try
                    {
                        dialogueObj = FileHandler.JSONLoadIntoObject(FileHandler.ReadFile(getFile));

                        JSONInput.LoadJSON(dialogueObj);

                        Logger.LogInfo($"Loaded custom dialogue from file \"{filename}\" successfully!");
                    }
                    catch (Exception)
                    {
                        Logger.LogError($"Could not load JSON from \"{filename}\". Please double check your file. Make sure you didn't erase any commas.");
                        //throw;
                    }
                } else
                {
                    // Plans: Add KCM screen that lets you choose a dialogue file.
                    // Screen will display each file's name except for the "_bd.json" part. :D 
                    // So test_bd.json would be displayed as "test".

                    // When I do implenet it, I will comment out this error and log a warning or regular message in the console instead simply making it clear what happened.

                    Logger.LogError("Could not load custom dialogue: There's more than one \"_bd.json\" file in the plugin directory." + Environment.NewLine + "Please choose one custom dialogue file to keep in the plugin folder.");
                }
            } else {
                Logger.LogError("Could not load custom dialogue: There's no \"_bd.json\" file in the plugin directory. Please make sure your file's name ends in \"_bd.json\".");
            }

        }

    }
}
