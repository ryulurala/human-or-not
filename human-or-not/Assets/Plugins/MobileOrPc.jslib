var MobileOrPc = {
    isMobile: function()
    {
        return ((!/Win16|Win32|Win64|Mac|MacIntel/.test(navigator.userAgent)) || /iPad|iPhone|iPod/.test(navigator.userAgent) || (navigator.platform === 'MacIntel' && navigator.maxTouchPoints > 1))
    }
};

mergeInto(LibraryManager.library, MobileOrPc);