using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
using UnityEngine.Events;
namespace Project.TofuGirl.UI
{
    public class FWindowManager
    {
        private static Dictionary<string, Window> m_WindowDic = new Dictionary<string, Window>();

        public static void AddWindow(string url, UnityAction<Window> instanceCallback = null)
        {
            if (!m_WindowDic.ContainsKey(url))
            {
                m_WindowDic.Add(url, new Window());
            }
            instanceCallback?.Invoke(m_WindowDic[url]);
        }

        public static void RemoveWindow(string url)
        {
            if (m_WindowDic.TryGetValue(url, out Window window))
            {
                window.Dispose();
                m_WindowDic.Remove(url);
            }
        }

        public static Window GetWindow(string url)
        {
            if (m_WindowDic.ContainsKey(url))
            {
                return m_WindowDic[url];
            }
            return null;
        }
    }
}
