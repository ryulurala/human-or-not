var MobileOrPc = {
    isMobile: function()
    {
        return (/iPad|iPhone|iPod/.test(navigator.platform) || (navigator.platform === 'MacIntel' && navigator.maxTouchPoints > 1)) && !window.MSStream;
    }
};

mergeInto(LibraryManager.library, MobileOrPc);