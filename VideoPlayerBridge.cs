using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class VideoPlayerBridge : MonoBehaviour
{
    #region P/Invoke - Call Swift Functions

    [DllImport("__Internal")]
    private static extern void SL_loadVideo(string url);

    [DllImport("__Internal")]
    private static extern void SL_play();

    [DllImport("__Internal")]
    private static extern void SL_pause();

    [DllImport("__Internal")]
    private static extern void SL_stop();

    [DllImport("__Internal")]
    private static extern void SL_seekForward(double seconds);

    [DllImport("__Internal")]
    private static extern void SL_seekBackward(double seconds);

    [DllImport("__Internal")]
    private static extern void SL_seekTo(double value);

    [DllImport("__Internal")]
    private static extern void SL_cleanup();

    [DllImport("__Internal")]
    private static extern void SL_setURLS(IntPtr urlArray, int count);

    [DllImport("__Internal")]
    private static extern void SL_setShowForwardButton([MarshalAs(UnmanagedType.I1)] bool visible);

    [DllImport("__Internal")]
    private static extern void SL_setShowBackwordButton([MarshalAs(UnmanagedType.I1)] bool visible);

    [DllImport("__Internal")]
    private static extern void SL_setShowBack10Button([MarshalAs(UnmanagedType.I1)] bool visible);

    [DllImport("__Internal")]
    private static extern void SL_setShowFor10Button([MarshalAs(UnmanagedType.I1)] bool visible);

    [DllImport("__Internal")]
    private static extern void SL_setShowPlayPauseButton([MarshalAs(UnmanagedType.I1)] bool visible);

    [DllImport("__Internal")]
    private static extern void SL_setShowBackButton([MarshalAs(UnmanagedType.I1)] bool visible);

    [DllImport("__Internal")]
    private static extern void SL_setShowLogo([MarshalAs(UnmanagedType.I1)] bool visible);

    [DllImport("__Internal")]
    private static extern void SL_setShowSeekbar([MarshalAs(UnmanagedType.I1)] bool visible);

    [DllImport("__Internal")]
    private static extern void SL_setShowTimeDuration([MarshalAs(UnmanagedType.I1)] bool visible);

    #endregion

    #region Wrapper Functions

    public void LoadVideo(string url)
    {
#if UNITY_IOS && !UNITY_EDITOR
        SL_loadVideo(url);
#endif
    }

    public void Play()
    {
#if UNITY_IOS && !UNITY_EDITOR
        SL_play();
#endif
    }

    public void Pause()
    {
#if UNITY_IOS && !UNITY_EDITOR
        SL_pause();
#endif
    }

    public void Stop()
    {
#if UNITY_IOS && !UNITY_EDITOR
        SL_stop();
#endif
    }

    public void SeekForward(double seconds)
    {
#if UNITY_IOS && !UNITY_EDITOR
        SL_seekForward(seconds);
#endif
    }

    public void SeekBackward(double seconds)
    {
#if UNITY_IOS && !UNITY_EDITOR
        SL_seekBackward(seconds);
#endif
    }

    public void SeekTo(double value)
    {
#if UNITY_IOS && !UNITY_EDITOR
        SL_seekTo(value);
#endif
    }

    public void Cleanup()
    {
#if UNITY_IOS && !UNITY_EDITOR
        SL_cleanup();
#endif
    }

    public void SetURLs(string[] urls)
    {
        if (urls == null || urls.Length == 0) return;
#if UNITY_IOS && !UNITY_EDITOR
        IntPtr urlArray = MarshalArray(urls);
        try
        {
            SL_setURLS(urlArray, urls.Length);
        }
        finally
        {
            FreeArray(urlArray, urls.Length);
        }
#endif
    }

    public void ShowForwardButton(bool visible)
    {
#if UNITY_IOS && !UNITY_EDITOR
        SL_setShowForwardButton(visible);
#endif
    }

    public void ShowBackwardButton(bool visible)
    {
#if UNITY_IOS && !UNITY_EDITOR
        SL_setShowBackwordButton(visible);
#endif
    }

    public void ShowBack10Button(bool visible)
    {
#if UNITY_IOS && !UNITY_EDITOR
        SL_setShowBack10Button(visible);
#endif
    }

    public void ShowForward10Button(bool visible)
    {
#if UNITY_IOS && !UNITY_EDITOR
        SL_setShowFor10Button(visible);
#endif
    }

    public void ShowPlayPauseButton(bool visible)
    {
#if UNITY_IOS && !UNITY_EDITOR
        SL_setShowPlayPauseButton(visible);
#endif
    }

    public void ShowBackButton(bool visible)
    {
#if UNITY_IOS && !UNITY_EDITOR
        SL_setShowBackButton(visible);
#endif
    }

    public void ShowLogo(bool visible)
    {
#if UNITY_IOS && !UNITY_EDITOR
        SL_setShowLogo(visible);
#endif
    }

    public void ShowSeekbar(bool visible)
    {
#if UNITY_IOS && !UNITY_EDITOR
        SL_setShowSeekbar(visible);
#endif
    }

    public void ShowTimeDuration(bool visible)
    {
#if UNITY_IOS && !UNITY_EDITOR
        SL_setShowTimeDuration(visible);
#endif
    }

    #endregion

    #region Helper Method

    private IntPtr MarshalArray(string[] array)
    {
        IntPtr ptr = Marshal.AllocHGlobal(IntPtr.Size * array.Length);
        for (int i = 0; i < array.Length; i++)
        {
            IntPtr stringPtr = Marshal.StringToHGlobalAnsi(array[i]);
            Marshal.WriteIntPtr(ptr, i * IntPtr.Size, stringPtr);
        }
        return ptr;
    }

    private void FreeArray(IntPtr arrayPtr, int count)
    {
        for (int i = 0; i < count; i++)
        {
            IntPtr strPtr = Marshal.ReadIntPtr(arrayPtr, i * IntPtr.Size);
            if (strPtr != IntPtr.Zero) Marshal.FreeHGlobal(strPtr);
        }
        Marshal.FreeHGlobal(arrayPtr);
    }

    #endregion
}
