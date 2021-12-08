using UnityEngine;

namespace Log
{
    public static class D
    {
        public static bool IsDebug;

#if !UNITY_EDITOR && !LOGLEVEL3
        [System.Diagnostics.Conditional("DEV_BUILD")]
#endif
        public static void Info(string message, params object[] args)
        {
            if (!IsDebug)
            {
                return;
            }

            Info(message, Color.white, args);
        }

#if !UNITY_EDITOR && !LOGLEVEL3
        [System.Diagnostics.Conditional("DEV_BUILD")]
#endif
        public static void Info(string message, Color color, params object[] args)
        {
            if (!IsDebug)
            {
                return;
            }

            if (args.Length < 1)
            {
                Debug.Log(Format(message, color));
                return;
            }

            Debug.LogFormat(Format(message, color), args);
        }

#if !UNITY_EDITOR && !LOGLEVEL2 && !LOGLEVEL3
        [System.Diagnostics.Conditional("DEV_BUILD")]
#endif
        public static void Warn(string message, params object[] args)
        {
            if (!IsDebug)
            {
                return;
            }

            Warn(message, Color.yellow, args);
        }

#if !UNITY_EDITOR && !LOGLEVEL2 && !LOGLEVEL3
        [System.Diagnostics.Conditional("DEV_BUILD")]
#endif
        public static void Warn(string message, Color color, params object[] args)
        {
            if (!IsDebug)
            {
                return;
            }

            if (args.Length < 1)
            {
                Debug.LogWarning(Format(message, color));
                return;
            }

            Debug.LogWarningFormat(Format(message, color), args);
        }

#if !UNITY_EDITOR && !LOGLEVEL1 && !LOGLEVEL2 && !LOGLEVEL3
        [System.Diagnostics.Conditional("DEV_BUILD")]
#endif
        public static void Error(string message, params object[] args)
        {
            if (!IsDebug)
            {
                return;
            }

            Error(message, Color.red, args);
        }

#if !UNITY_EDITOR && !LOGLEVEL1 && !LOGLEVEL2 && !LOGLEVEL3
        [System.Diagnostics.Conditional("DEV_BUILD")]
#endif
        public static void Error(string message, Color color, params object[] args)
        {
            if (!IsDebug)
            {
                return;
            }

            if (args.Length < 1)
            {
                Debug.LogError(Format(message, color));
                return;
            }

            Debug.LogErrorFormat(Format(message, color), args);
        }

        private static string Format(string message, Color color)
        {
#if DEV_BUILD
            return $"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{message}</color>";
#endif
            return message;
        }
    }
}