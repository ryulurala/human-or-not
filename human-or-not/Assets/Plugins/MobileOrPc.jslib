var MobileOrPc = {
    isMobile: function()
    {
        return /iPhone|iPad|iPod|Android/i.test(window.navigator.userAgent);
    }
};

mergeInto(LibraryManager.library, MobileOrPc);