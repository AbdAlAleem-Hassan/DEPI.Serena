// theme-loader.js - Load this in HEAD before CSS
(function() {
    'use strict';
    
    // Get theme preference immediately
    const savedTheme = localStorage.getItem('theme');
    const systemPrefersDark = window.matchMedia('(prefers-color-scheme: dark)').matches;
    
    // Apply theme to document element immediately
    if (savedTheme === 'dark' || (!savedTheme && systemPrefersDark)) {
        document.documentElement.setAttribute('data-user-theme', 'dark');
        document.documentElement.classList.add('dark-mode');
    } else {
        document.documentElement.setAttribute('data-user-theme', 'light');
        document.documentElement.classList.remove('dark-mode');
    }
    
    // Apply language preference immediately
    const savedLang = localStorage.getItem('lang') || 'en';
    if (savedLang === 'ar') {
        document.documentElement.classList.add('rtl');
        document.documentElement.setAttribute('dir', 'rtl');
        document.documentElement.setAttribute('lang', 'ar');
    }
})();