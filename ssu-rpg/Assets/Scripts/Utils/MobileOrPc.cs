
using System.Runtime.InteropServices;

public class MobileOrPc
{
    [DllImport("__Internal")]
    private static extern bool isMobile();

    public static bool IsMobile()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
            return isMobile();
#endif
        return false;
    }
}
