using System;
using System.IO;
using UnityEngine;


    public static class MyPaths
    {
        const string _GameData = "/gamedata";
        const string _UserData = "/userdata";
        const string _Animations = "/animations";
        const string _AnimationsBin = "/animations/binary";
        const string _Models = "/models";
        const string _Localizations = "/localizations";
        const string _Locations = "/locations";
        const string _Textures = "/textures";
        const string _TexturesFullscreen = "textures/fullscreen/";
        const string _Statistics = "/statistics";
        const string _News = "/news";
        const string _BundlesExternal = "/bundles";
        const string _Video = "/video";
        const string _Packs = "/packs.xml";

        const string _GUIResourse = "Assets/src/GUI/Resources";
        const string _ItemsTexture = "UI/Items/";
        const string _UsersTexture = "UI/Users/";
        const string _SkillsTexture = "UI/Skills/";
        public const string AchievementsTexture = "UI/Achievements/";
        public const string FullscreenUI = "UI/Fullscreen/";
        public const string Logos = "textures/Logos/";

        public static string CurrentResources = "";
        public static string CurrentStorage = "";
        public static string CurrentCachedStorage = "";
        public static bool UsingResources = true;


        static bool _IsInitialized = false;

        public static string GameData {
            get {
                return CurrentResources + _GameData;
            }
        }

        public static string ExternalCachedGameData {
            get {
#if UNITY_STANDALONE || UNITY_EDITOR
                return CurrentCachedStorage + "/cached_gamedata";
#else
                return CurrentCachedStorage + _GameData;
#endif
            }
        }

        public static string Animations {
            get {
                return GameData + _Animations;
            }
        }

        public static string AnimationBinary {
            get {
                return GameData + _AnimationsBin;
            }
        }

        public static string Models {
            get {
                return GameData + _Models;
            }
        }

        public static string Video
        {
            get
            {
                return GameData + _Video;
            }
        }

        public static string ItemsTexture
        {
            get
            {
                return _ItemsTexture;
            }
        }

        public static string Textures
        {
            get
            {
                return _Textures;
            }
        }

        public static string UsersTexture
        {
            get
            {
                return _UsersTexture;
            }
        }

        public static string SkillsTexture
        {
            get
            {
                return _SkillsTexture;
            }
        }

        public static string TexturesFullscreen
        {
            get
            {
                return _TexturesFullscreen;
            }
        }

        public static string Localizations
        {
            get {
                return GameData + _Localizations;
            }
        }

        public static string Locations
        {
            get {
                return GameData + _Locations;
            }
        }

        // UserData section;
        public static string StorageUserData {
            get {
                return CurrentStorage + _UserData;
            }
        }

        public static string Statistics
        {
            get
            {
                return ExternalCachedGameData + _Statistics;
            }
        }

        public static string News
        {
            get
            {
                return ExternalCachedGameData + _News;
            }
        }

        public static string BundlesExternal
        {
            get
            {
                return ExternalCachedGameData + _BundlesExternal;
            }
        }

        public static string Packs
        {
            get
            {
                return ExternalCachedGameData + _Packs;
            }
        }

        public static string GUIResourse
        {
            get
            {
                return _GUIResourse;
            }
        }

        // End UserData section;

        public static bool IsInitialized
        {
            get
            {
                return MyPaths._IsInitialized;
            }
        }

        public static void Init()
        {
            if (MyPaths._IsInitialized == true)
            {
                return;
            }

            MyPaths._IsInitialized = true;

#if UNITY_STANDALONE
            CurrentResources = "";
            CurrentStorage = System.IO.Directory.GetCurrentDirectory();
            CurrentCachedStorage = System.IO.Directory.GetCurrentDirectory();
            UsingResources = true;
#endif

#if UNITY_ANDROID
            CurrentResources     = "";
            CurrentStorage       = UnityEngine.Application.persistentDataPath;
            CurrentCachedStorage = UnityEngine.Application.persistentDataPath;
            UsingResources       = true;

            string nativePath = GetAndroidNativePath();

            //DebugUtils.Log("Path1:" + CurrentStorage);
            //DebugUtils.Log("Path2:" + nativePath);

            if (string.IsNullOrEmpty(CurrentStorage)) {
                CurrentStorage = CurrentCachedStorage = nativePath;
            }
            else {
                try {
                    if (!Directory.Exists(CurrentStorage + _UserData)) {
                        if (Directory.Exists(nativePath + _UserData)) {
                            CurrentStorage = CurrentCachedStorage = nativePath;
                        }
                    }
                }
                catch {
                    
                }
            }

#endif

#if UNITY_IPHONE
            CurrentResources     = "";
            CurrentStorage       = UnityEngine.Application.persistentDataPath;
            CurrentCachedStorage = UnityEngine.Application.temporaryCachePath;
            UsingResources       = true;
#endif

#if UNITY_WEBGL
            CurrentResources     = "";
            CurrentStorage       = UnityEngine.Application.persistentDataPath;
            CurrentCachedStorage = UnityEngine.Application.temporaryCachePath;
            UsingResources       = true;
#endif


#if !UNITY_WEBGL
            CreateExternalFolders();
#endif
        }

        public static void CreateExternalFolders()
        {
            if (!Directory.Exists(MyPaths.StorageUserData)) {
                Directory.CreateDirectory(MyPaths.StorageUserData);
            }

            if (!Directory.Exists(MyPaths.ExternalCachedGameData)) {
                Directory.CreateDirectory(MyPaths.ExternalCachedGameData);
            }
        }

        public static string CombineExternalCachedPath(string p_path) {
            if (p_path.Contains(CurrentCachedStorage))
                return p_path;

            return string.Format("{0}/{1}", CurrentCachedStorage, p_path);
        }

        public static void ResetBundlesCache()
        {
            if (Directory.Exists(MyPaths.BundlesExternal)) {
                Directory.Delete(MyPaths.BundlesExternal, true);
            }

            Directory.CreateDirectory(MyPaths.BundlesExternal);
        }

#if UNITY_ANDROID
        public static string GetAndroidNativePath() {
            string path = "";
            try {
                IntPtr obj_context = AndroidJNI.FindClass("android/content/ContextWrapper");
                IntPtr method_getFilesDir = AndroidJNIHelper.GetMethodID(obj_context, "getFilesDir", "()Ljava/io/File;");

                using (AndroidJavaClass cls_UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
                    using (AndroidJavaObject obj_Activity = cls_UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) {
                        IntPtr file = AndroidJNI.CallObjectMethod(obj_Activity.GetRawObject(), method_getFilesDir, new jvalue[0]);
                        IntPtr obj_file = AndroidJNI.FindClass("java/io/File");
                        IntPtr method_getAbsolutePath = AndroidJNIHelper.GetMethodID(obj_file, "getAbsolutePath", "()Ljava/lang/String;");   
    
                        path = AndroidJNI.CallStringMethod(file, method_getAbsolutePath, new jvalue[0]);                    

                        if(path == null) {
                            Debug.Log("Using fallback path");
                            path = "/data/data/com.nekki.shadowfight/files";
                        }
                    }
                }
            }
            catch(Exception e) {
                Debug.Log(e.ToString());
            }
            return path;
        }
#endif
    };
