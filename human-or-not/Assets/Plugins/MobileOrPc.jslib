var MobileOrPc = {
    isMobile: function()
    {
        return UnityLoader.SystemInfo.mobile;
    }
};

mergeInto(LibraryManager.library, MobileOrPc);