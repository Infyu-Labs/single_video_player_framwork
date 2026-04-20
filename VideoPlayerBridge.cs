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
    private static extern void SL_setShowForwardButton(bool visible);

    [DllImport("__Internal")]
    private static extern void SL_setShowBackwordButton(bool visible);

    [DllImport("__Internal")]
    private static extern void SL_setShowBack10Button(bool visible);

    [DllImport("__Internal")]
    private static extern void SL_setShowFor10Button(bool visible);

    [DllImport("__Internal")]
    private static extern void SL_setShowPlayPauseButton(bool visible);

    [DllImport("__Internal")]
    private static extern void SL_setShowBackButton(bool visible);

    [DllImport("__Internal")]
    private static extern void SL_setShowLogo(bool visible);

    [DllImport("__Internal")]
    private static extern void SL_setShowSeekbar(bool visible);

    [DllImport("__Internal")]
    private static extern void SL_setShowTimeDuration(bool visible);

    [DllImport("__Internal")]
    private static extern void SL_registerUnityCallback(UnityCallback callback);

    public delegate void UnityCallback(string message);

    [AOT.MonoPInvokeCallback(typeof(UnityCallback))]
    public static void OnSwiftEvent(string message)
    {
        Debug.Log("Video Event: " + message);
    }

    #endregion

    #region Wrapper Functions

    public void LoadVideo(string url)
    {
        SL_loadVideo(url);
    }

    public void Play()
    {
        SL_play();
    }

    public void Pause()
    {
        SL_pause();
    }

    public void Stop()
    {
        SL_stop();
    }

    public void SeekForward(double seconds)
    {
        SL_seekForward(seconds);
    }

    public void SeekBackward(double seconds)
    {
        SL_seekBackward(seconds);
    }

    public void SeekTo(double value)
    {
        SL_seekTo(value);
    }

    public void Cleanup()
    {
        SL_cleanup();
    }

    public void SetURLs(string[] urls)
    {
        IntPtr urlArray = MarshalArray(urls);
        SL_setURLS(urlArray, urls.Length);
        Marshal.FreeHGlobal(urlArray);
    }

    public void ShowForwardButton(bool visible)
    {
        SL_setShowForwardButton(visible);
    }

    public void ShowBackwordButton(bool visible)
    {
        SL_setShowBackwordButton(visible);
    }

    public void ShowBack10Button(bool visible)
    {
        SL_setShowBack10Button(visible);
    }

    public void ShowFor10Button(bool visible)
    {
        SL_setShowFor10Button(visible);
    }

    public void ShowPlayPauseButton(bool visible)
    {
        SL_setShowPlayPauseButton(visible);
    }

    public void ShowBackButton(bool visible)
    {
        SL_setShowBackButton(visible);
    }

    public void ShowLogo(bool visible)
    {
        SL_setShowLogo(visible);
    }

    public void ShowSeekbar(bool visible)
    {
        SL_setShowSeekbar(visible);
    }

    public void ShowTimeDuration(bool visible)
    {
        SL_setShowTimeDuration(visible);
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

    #endregion
}