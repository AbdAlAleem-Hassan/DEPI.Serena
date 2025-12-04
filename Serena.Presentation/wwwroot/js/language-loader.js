// language-loader.js - تحديث الكود
(function() {
    'use strict';
    
    // Get language preference immediately
    const savedLang = localStorage.getItem('lang') || 'en';
    
    // Apply language to document element immediately - BEFORE any content renders
    document.documentElement.setAttribute('data-lang', savedLang);
    
    if (savedLang === 'ar') {
        document.documentElement.classList.add('rtl');
        document.documentElement.setAttribute('dir', 'rtl');
        document.documentElement.setAttribute('lang', 'ar');
    } else {
        document.documentElement.classList.remove('rtl');
        document.documentElement.setAttribute('dir', 'ltr');
        document.documentElement.setAttribute('lang', 'en');
    }
})();