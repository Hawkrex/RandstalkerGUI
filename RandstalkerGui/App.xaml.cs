using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;

namespace RandstalkerGui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static App Instance;
        public static string AssemblyDirectory;
        public event EventHandler LanguageChangedEvent;

        public App()
        {
            // Initialize static variables
            Instance = this;
            AssemblyDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            // Load the Localization Resource Dictionary based on OS language
            foreach (string filePath in GetLocXAMLFilePath(CultureInfo.CurrentCulture.Name))
            {
                SetLanguageResourceDictionary(filePath);
            }
        }

        /// <summary>
        /// Dynamically load a Localization ResourceDictionary from a file
        /// </summary>
        public void SwitchLanguage(string inFiveCharLang)
        {
            if (CultureInfo.CurrentCulture.Name.Equals(inFiveCharLang))
            {
                return;
            }

            var ci = new CultureInfo(inFiveCharLang);
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            foreach (string filePath in GetLocXAMLFilePath(inFiveCharLang))
            {
                SetLanguageResourceDictionary(filePath);
            }
            LanguageChangedEvent?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Returns the path to the ResourceDictionary file based on the language character string.
        /// </summary>
        /// <param name="inFiveCharLang"></param>
        /// <returns></returns>
        private IEnumerable<string> GetLocXAMLFilePath(string inFiveCharLang)
        {
            var files = Directory.GetFiles(Path.Combine(AssemblyDirectory, "Resources/Localisations"));
            return files.Where(file => file.Contains(inFiveCharLang));
        }

        /// <summary>
        /// Sets or replaces the ResourceDictionary by dynamically loading
        /// a Localization ResourceDictionary from the file path passed in.
        /// </summary>
        /// <param name="inFile"></param>
        private void SetLanguageResourceDictionary(string inFile)
        {
            if (File.Exists(inFile))
            {
                // Read in ResourceDictionary File
                var languageDictionary = new ResourceDictionary();
                languageDictionary.Source = new Uri(inFile);

                // Remove any previous Localization dictionaries loaded
                int langDictId = -1;
                for (int i = 0; i < Resources.MergedDictionaries.Count; i++)
                {
                    var md = Resources.MergedDictionaries[i];
                    // Make sure your Localization ResourceDictionarys have the ResourceDictionaryName
                    // key and that it is set to a value starting with "[something]-".
                    if (md.Contains("ResourceDictionaryName"))
                    {
                        if (md["ResourceDictionaryName"].ToString().StartsWith(languageDictionary["ResourceDictionaryName"].ToString().Split('-')[0]))
                        {
                            langDictId = i;
                            break;
                        }
                    }
                }
                if (langDictId == -1)
                {
                    // Add in newly loaded Resource Dictionary
                    Resources.MergedDictionaries.Add(languageDictionary);
                }
                else
                {
                    // Replace the current langage dictionary with the new one
                    Resources.MergedDictionaries[langDictId] = languageDictionary;
                }
            }
        }
    }
}
