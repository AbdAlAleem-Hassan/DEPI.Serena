// Nav links active state and dropdowns using toggle class
const navLinks = document.querySelectorAll('.nav-link');
const dropdowns = document.querySelectorAll('.dropdown-link');
const mobileNavLinks = document.querySelectorAll('.mobile-nav-link');
const mobileDropdownLinks = document.querySelectorAll('.mobile-dropdown-link');

// Global form interaction tracker
window.formHasInteracted = false;

// Function to get link identifier (text content without icons)
function getLinkIdentifier(link) {
    let text = link.textContent.trim();
    // Remove icon text if exists
    text = text.replace(/[\u25B2\u25BC▶◀→←]/g, '').trim();
    return text;
}

// Function to set active link and sync between mobile and desktop
function setActiveLink(clickedLink) {
    const linkIdentifier = getLinkIdentifier(clickedLink);

    console.log('Setting active for:', linkIdentifier);

    // Remove active class from all links
    navLinks.forEach(link => link.classList.remove('active'));
    dropdowns.forEach(link => link.classList.remove('active'));
    mobileNavLinks.forEach(link => link.classList.remove('active'));
    mobileDropdownLinks.forEach(link => link.classList.remove('active'));

    // Add active class to clicked link
    clickedLink.classList.add('active');

    // Find and activate corresponding links in other menus
    const allLinks = [...navLinks, ...dropdowns, ...mobileNavLinks, ...mobileDropdownLinks];

    allLinks.forEach(link => {
        if (link !== clickedLink && getLinkIdentifier(link) === linkIdentifier) {
            link.classList.add('active');
            console.log('Activated corresponding link:', getLinkIdentifier(link));
        }
    });

    // Close mobile menu after clicking (for mobile only)
    if (window.innerWidth <= 1000) {
        const mobileMenu = document.getElementById('mobileMenu');
        const mobileMenuBtn = document.getElementById('mobileMenuBtn');
        const icon = mobileMenuBtn?.querySelector('i');

        if (mobileMenu && mobileMenuBtn && icon) {
            mobileMenu.classList.remove('active');
            icon.classList.add('fa-bars');
            icon.classList.remove('fa-times');
        }
    }
}

// Desktop nav links
navLinks.forEach(link => {
    link.addEventListener('click', (e) => {
        if (!link.classList.contains('dropdown-toggle')) {
            setActiveLink(link);
        }
    });
});

// Desktop dropdown links
dropdowns.forEach(link => {
    link.addEventListener('click', () => {
        setActiveLink(link);
    });
});

// Mobile nav links
mobileNavLinks.forEach(link => {
    link.addEventListener('click', () => {
        if (!link.classList.contains('mobile-dropdown-toggle')) {
            setActiveLink(link);
        }
    });
});

// Mobile dropdown links
mobileDropdownLinks.forEach(link => {
    link.addEventListener('click', () => {
        setActiveLink(link);
    });
});

// ---------------------------------------------------------
// FIX: Dropdown chevron toggle for MULTIPLE dropdowns
// ---------------------------------------------------------
const dropdownToggles = document.querySelectorAll('.dropdown-toggle');

dropdownToggles.forEach(toggle => {
    // Find the parent .dropdown container for this specific toggle
    const dropdown = toggle.closest('.dropdown');

    // If we can't find the parent, skip this iteration
    if (!dropdown) return;

    // Find the specific content and icon within this dropdown
    const dropdownContent = dropdown.querySelector('.dropdown-content');
    const icon = toggle.querySelector('i');

    // Click event (primarily for Mobile or Tablet)
    toggle.addEventListener('click', (e) => {
        if (window.innerWidth <= 1000) {
            e.preventDefault();
            e.stopPropagation(); // Stop event from bubbling up

            // Optional: Close other dropdowns if you want only one open at a time
            /* document.querySelectorAll('.dropdown-content').forEach(c => {
                if (c !== dropdownContent) c.classList.remove('active');
            });
            */

            dropdownContent.classList.toggle('active');

            if (icon) {
                if (dropdownContent.classList.contains('active')) {
                    icon.classList.remove('fa-chevron-down');
                    icon.classList.add('fa-chevron-up');
                } else {
                    icon.classList.remove('fa-chevron-up');
                    icon.classList.add('fa-chevron-down');
                }
            }
        }
    });

    // Hover functionality for desktop (Attached to the container)
    dropdown.addEventListener('mouseenter', () => {
        if (window.innerWidth > 1000) {
            const dropdown = document.querySelector('.dropdown');
            dropdown.addEventListener('mouseenter', () => {
                dropdownContent.classList.add('active');
                if (icon) {
                    icon.classList.add('fa-chevron-up');
                    icon.classList.remove('fa-chevron-down');
                }
            }
        });

    dropdown.addEventListener('mouseleave', () => {
        if (window.innerWidth > 1000) {
            dropdownContent.classList.remove('active');
            if (icon) {
                icon.classList.remove('fa-chevron-up');
                icon.classList.add('fa-chevron-down');
            });
}
}
    });
});
// ---------------------------------------------------------
// END FIX
// ---------------------------------------------------------

// Theme Toggle
// Theme Toggle
const themeToggle = document.getElementById('themeToggle');
const mobileThemeBtn = document.getElementById('mobileThemeBtn');
const body = document.body;

// Enhanced theme initialization without FOUC
function initializeTheme() {
    // Theme is already set by theme-loader.js, just update dynamic elements
    updateThemeIcons();
    updateFloatingCards();
}

function toggleTheme() {
    const currentTheme = document.documentElement.getAttribute('data-user-theme');
    const newTheme = currentTheme === 'dark' ? 'light' : 'dark';

    // Update data attribute and class simultaneously
    document.documentElement.setAttribute('data-user-theme', newTheme);
    document.documentElement.classList.toggle('dark-mode');

    localStorage.setItem('theme', newTheme);
    updateThemeIcons();
    updateFloatingCards();
}

function updateThemeIcons() {
    // Update desktop theme icons
    const desktopIcons = document.querySelectorAll('.theme-toggle i');
    desktopIcons.forEach(icon => {
        if (document.documentElement.classList.contains('dark-mode')) {
            icon.classList.remove('fa-moon');
            icon.classList.add('fa-sun');
        } else {
            icon.classList.remove('fa-sun');
            icon.classList.add('fa-moon');
        }
    });

    // Update mobile theme icons
    const mobileIcons = document.querySelectorAll('#mobileThemeBtn i');
    mobileIcons.forEach(icon => {
        if (document.documentElement.classList.contains('dark-mode')) {
            icon.classList.remove('fa-moon');
            icon.classList.add('fa-sun');
        } else {
            icon.classList.remove('fa-sun');
            icon.classList.add('fa-moon');
        }
    });

    // Update mobile theme text
    const mobileThemeTexts = document.querySelectorAll('#mobileThemeBtn span:last-child');
    mobileThemeTexts.forEach(span => {
        if (document.documentElement.classList.contains('dark-mode')) {
            span.setAttribute('data-en', 'Light Mode');
            span.setAttribute('data-ar', 'الوضع الفاتح');
            if (body.classList.contains('rtl')) {
                span.textContent = span.getAttribute('data-ar');
            } else {
                span.textContent = span.getAttribute('data-en');
            }
        } else {
            span.setAttribute('data-en', 'Dark Mode');
            span.setAttribute('data-ar', 'الوضع الداكن');
            if (body.classList.contains('rtl')) {
                span.textContent = span.getAttribute('data-ar');
            } else {
                span.textContent = span.getAttribute('data-en');
            }
        }
    });
}

// Language Toggle
const langToggle = document.getElementById('langToggle');
const mobileLangBtn = document.getElementById('mobileLangBtn');

// Initialize language and prevent validation on initial load
function initializeLanguage() {
    const savedLang = localStorage.getItem('lang') || 'en';

    // Set initial language without triggering validation
    if (savedLang === 'ar') {
        body.classList.add('rtl');
        document.documentElement.setAttribute('dir', 'rtl');
        document.documentElement.setAttribute('lang', 'ar');
    } else {
        body.classList.remove('rtl');
        document.documentElement.setAttribute('dir', 'ltr');
        document.documentElement.setAttribute('lang', 'en');
    }

    updateLangButtons();
    updateTextContent(savedLang);

    // Reset form interaction state
    window.formHasInteracted = false;

    // Clear any existing validation messages on initial load
    clearAllValidationMessages();
}

function toggleLanguage() {
    const currentLang = body.classList.contains('rtl') ? 'ar' : 'en';
    const newLang = currentLang === 'ar' ? 'en' : 'ar';

    body.classList.toggle('rtl');
    document.documentElement.setAttribute('dir', newLang === 'ar' ? 'rtl' : 'ltr');
    document.documentElement.setAttribute('lang', newLang);

    localStorage.setItem('lang', newLang);
    updateLangButtons();
    updateTextContent(newLang);

    // Only update validation messages for interacted fields
    if (window.formHasInteracted) {
        updateValidationMessages();
    } else {
        // Clear only non-interacted validation messages
        clearNonInteractedValidationMessages();
    }
}

function updateLangButtons() {
    const isRTL = body.classList.contains('rtl');

    // Update desktop language buttons
    const desktopButtons = document.querySelectorAll('.lang-toggle');
    desktopButtons.forEach(button => {
        button.textContent = isRTL ? 'EN' : 'AR';
    });

    // Update mobile language buttons
    const mobileButtons = document.querySelectorAll('#mobileLangBtn span:first-child');
    mobileButtons.forEach(button => {
        button.textContent = isRTL ? 'EN' : 'AR';
    });

    // Update mobile language text
    const mobileLangTexts = document.querySelectorAll('#mobileLangBtn span:last-child');
    mobileLangTexts.forEach(span => {
        if (isRTL) {
            span.setAttribute('data-en', 'English');
            span.setAttribute('data-ar', 'الإنجليزية');
            span.textContent = span.getAttribute('data-ar');
        } else {
            span.setAttribute('data-en', 'Arabic');
            span.setAttribute('data-ar', 'العربية');
            span.textContent = span.getAttribute('data-en');
        }
    });
}

function updateTextContent(lang) {
    // Update all elements with data attributes
    const elements = document.querySelectorAll('[data-en], [data-ar]');
    elements.forEach(element => {
        // Preserve HTML structure (like icons) when updating text
        if (element.children.length > 0) {
            const spans = element.querySelectorAll('span');
            spans.forEach(span => {
                if (lang === 'ar' && span.hasAttribute('data-ar')) {
                    span.textContent = span.getAttribute('data-ar');
                } else if (lang === 'en' && span.hasAttribute('data-en')) {
                    span.textContent = span.getAttribute('data-en');
                }
            });
        } else {
            if (lang === 'ar' && element.hasAttribute('data-ar')) {
                element.textContent = element.getAttribute('data-ar');
            } else if (lang === 'en' && element.hasAttribute('data-en')) {
                element.textContent = element.getAttribute('data-en');
            }
        }
    });

    // Update placeholders
    const inputs = document.querySelectorAll('input, textarea');
    inputs.forEach(input => {
        if (lang === 'ar' && input.hasAttribute('data-ar')) {
            input.placeholder = input.getAttribute('data-ar');
        } else if (lang === 'en' && input.hasAttribute('data-en')) {
            input.placeholder = input.getAttribute('data-en');
        }
    });
}

// Track form interaction per field
function trackFormInteraction() {
    const formInputs = document.querySelectorAll('input, textarea, select');

    formInputs.forEach(input => {
        // Add interacted class when user interacts with the field
        const handleInteraction = () => {
            input.classList.add('interacted');
            window.formHasInteracted = true;
        };

        input.addEventListener('input', handleInteraction);
        input.addEventListener('change', handleInteraction);
        input.addEventListener('blur', handleInteraction);
    });
}

// Update validation messages when language changes - only for interacted fields
function updateValidationMessages() {
    // Only update interacted fields
    const interactedFields = document.querySelectorAll('.interacted[data-validation]');

    interactedFields.forEach(field => {
        if (typeof validateField === 'function') {
            validateField(field);
        }
    });

    // Update password strength texts for interacted fields
    const registerPassword = document.getElementById('register-password');
    if (registerPassword && registerPassword.classList.contains('interacted')) {
        const strengthBar = document.getElementById('register-password-strength');
        const strengthText = document.getElementById('register-password-strength-text');
        if (strengthBar && strengthText) {
            const { strength } = calculatePasswordStrength(registerPassword.value);
            strengthText.textContent = getPasswordStrengthText(strength);
        }
    }

    const loginPassword = document.getElementById('login-password');
    if (loginPassword && loginPassword.classList.contains('interacted')) {
        const strengthBar = document.getElementById('login-password-strength');
        const strengthText = document.getElementById('login-password-strength-text');
        if (strengthBar && strengthText) {
            const { strength } = calculatePasswordStrength(loginPassword.value);
            strengthText.textContent = getPasswordStrengthText(strength);
        }
    }
}

// Clear all validation messages
function clearAllValidationMessages() {
    const validationMessages = document.querySelectorAll('.validation-message');
    validationMessages.forEach(message => {
        message.innerHTML = '';
        message.className = 'validation-message';
    });

    const formInputs = document.querySelectorAll('.form-input');
    formInputs.forEach(input => {
        input.classList.remove('error', 'success');
    });

    const checkboxes = document.querySelectorAll('input[type="checkbox"]');
    checkboxes.forEach(checkbox => {
        checkbox.classList.remove('error', 'success');
    });

    // Clear password strength indicators
    const strengthBars = document.querySelectorAll('.strength-bar');
    strengthBars.forEach(bar => {
        bar.className = 'strength-bar';
        bar.style.width = '0%';
    });

    const strengthTexts = document.querySelectorAll('.strength-text');
    strengthTexts.forEach(text => {
        text.textContent = '';
    });
}

// Clear only non-interacted validation messages
function clearNonInteractedValidationMessages() {
    const nonInteractedFields = document.querySelectorAll('input[data-validation]:not(.interacted)');

    nonInteractedFields.forEach(field => {
        const validationMessage = field.parentElement.querySelector('.validation-message');
        if (validationMessage) {
            validationMessage.innerHTML = '';
            validationMessage.className = 'validation-message';
        }
        field.classList.remove('error', 'success');
    });

    // Clear non-interacted password strength indicators
    const nonInteractedPasswords = document.querySelectorAll('input[type="password"]:not(.interacted)');
    nonInteractedPasswords.forEach(password => {
        const strengthBar = document.getElementById(`${password.id}-strength`);
        const strengthText = document.getElementById(`${password.id}-strength-text`);
        if (strengthBar && strengthText) {
            strengthBar.className = 'strength-bar';
            strengthBar.style.width = '0%';
            strengthText.textContent = '';
        }
    });
}

// Update floating cards based on theme
function updateFloatingCards() {
    const floatingCards = document.querySelectorAll('.hero-floating-card, .about-floating-stat');
    floatingCards.forEach(card => {
        if (document.documentElement.classList.contains('dark-mode')) {
            card.style.backgroundColor = 'rgba(30, 41, 59, 0.95)';
            card.style.border = '1px solid rgba(255, 255, 255, 0.1)';
        } else {
            card.style.backgroundColor = 'rgba(255, 255, 255, 0.95)';
            card.style.border = '1px solid rgba(255, 255, 255, 0.2)';
        }
    });
}

// Mobile Menu Toggle
function initializeMobileMenu() {
    const mobileMenuBtn = document.getElementById('mobileMenuBtn');
    const mobileMenu = document.getElementById('mobileMenu');
    const icon = mobileMenuBtn?.querySelector('i');

    if (mobileMenuBtn && mobileMenu && icon) {
        mobileMenuBtn.addEventListener('click', (e) => {
            e.stopPropagation();
            mobileMenu.classList.toggle('active');
            // Toggle menu icon
            icon.classList.toggle('fa-bars');
            icon.classList.toggle('fa-times');
        });

        // Close mobile menu when clicking outside
        document.addEventListener('click', (e) => {
            if (!mobileMenu.contains(e.target) && !mobileMenuBtn.contains(e.target)) {
                mobileMenu.classList.remove('active');
                icon.classList.add('fa-bars');
                icon.classList.remove('fa-times');
            }
        });

        // Close mobile menu on window resize
        window.addEventListener('resize', () => {
            if (window.innerWidth > 1000) {
                mobileMenu.classList.remove('active');
                icon.classList.add('fa-bars');
                icon.classList.remove('fa-times');
            }
        });
    }

    // Mobile dropdown functionality
    const mobileDropdowns = document.querySelectorAll('.mobile-dropdown');
    mobileDropdowns.forEach(dropdown => {
        const toggle = dropdown.querySelector('.mobile-dropdown-toggle');
        if (toggle) {
            toggle.addEventListener('click', (e) => {
                e.stopPropagation();
                dropdown.classList.toggle('active');
                const toggleIcon = toggle.querySelector('i');
                if (toggleIcon) {
                    toggleIcon.classList.toggle('fa-chevron-down');
                    toggleIcon.classList.toggle('fa-chevron-up');
                }
            });
        }
    });
}

// Initialize common functionality
document.addEventListener('DOMContentLoaded', () => {
    // Theme is already initialized by theme-loader.js, just update dynamic elements
    initializeTheme();
    initializeLanguage();

    // Track form interaction
    trackFormInteraction();

    if (themeToggle) {
        themeToggle.addEventListener('click', toggleTheme);
    }
    if (mobileThemeBtn) {
        mobileThemeBtn.addEventListener('click', toggleTheme);
    }

    if (langToggle) {
        langToggle.addEventListener('click', toggleLanguage);
    }
    if (mobileLangBtn) {
        mobileLangBtn.addEventListener('click', toggleLanguage);
    }

    initializeMobileMenu();

    // Initialize other components if they exist
    if (typeof initializePartnersCarousel === 'function') {
        initializePartnersCarousel();
    }
    if (typeof initializeHospitals === 'function') {
        initializeHospitals();
    }
    if (typeof initializeDoctors === 'function') {
        initializeDoctors();
    }
    if (typeof initializeReviews === 'function') {
        initializeReviews();
    }
    if (typeof initializeMap === 'function') {
        initializeMap();
    }
});

// Handle system theme changes
window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', (e) => {
    // Only update if user hasn't set a preference manually
    if (!localStorage.getItem('theme')) {
        if (e.matches) {
            document.documentElement.setAttribute('data-user-theme', 'dark');
            document.documentElement.classList.add('dark-mode');
        } else {
            document.documentElement.setAttribute('data-user-theme', 'light');
            document.documentElement.classList.remove('dark-mode');
        }
        updateThemeIcons();
        updateFloatingCards();
    }
});

// Export functions for global access
window.mainModule = {
    clearAllValidationMessages,
    updateTextContent,
    getCurrentLanguage: () => body.classList.contains('rtl') ? 'ar' : 'en',
    updateValidationMessages,
    clearNonInteractedValidationMessages
};