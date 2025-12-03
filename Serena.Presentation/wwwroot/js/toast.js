// toast.js
class ToastManager {
    constructor() {
        this.container = document.getElementById('toasterContainer');
        if (!this.container) {
            // Create container if it doesn't exist
            this.container = document.createElement('div');
            this.container.id = 'toasterContainer';
            this.container.className = 'toaster-container';
            document.body.appendChild(this.container);
        }
        this.initialize();
    }

    initialize() {
        // Check for TempData messages
        this.checkTempData();

        // Listen for toast events
        document.addEventListener('toast:show', (e) => {
            const { message, type, icon, duration } = e.detail;
            this.show(message, type, icon, duration);
        });
    }

    checkTempData() {
        // Check for hidden divs with temp data
        const messages = {
            'error': { type: 'error', icon: '❌' },
            'warning': { type: 'warning', icon: '⚠️' },
            'success': { type: 'success', icon: '✅' },
            'info': { type: 'info', icon: 'ℹ️' }
        };

        Object.keys(messages).forEach(key => {
            const element = document.querySelector(`[data-${key}]`);
            if (element && element.dataset[key]) {
                this.show(element.dataset[key], messages[key].type, messages[key].icon);
                // Remove the element after displaying
                element.remove();
            }
        });
    }

    show(message, type = 'info', icon = 'ℹ️', duration = 5000) {
        if (!message) return;

        // Create toast element
        const toast = document.createElement('div');
        toast.className = `toast ${type}`;
        toast.innerHTML = `
            <span class="toast-icon">${icon}</span>
            <div class="toast-message">${this.formatMessage(message)}</div>
            <button class="toast-close" aria-label="Close">
                <i class="fas fa-times"></i>
            </button>
        `;

        // Add close functionality
        const closeBtn = toast.querySelector('.toast-close');
        closeBtn.addEventListener('click', () => {
            this.removeToast(toast);
        });

        // Add to container
        this.container.appendChild(toast);

        // Auto remove after duration
        if (duration > 0) {
            setTimeout(() => {
                this.removeToast(toast);
            }, duration);
        }

        return toast;
    }

    removeToast(toast) {
        if (toast && toast.parentElement) {
            toast.style.animation = 'slideUp 0.3s ease-out';
            setTimeout(() => {
                if (toast.parentElement) {
                    toast.remove();
                }
            }, 300);
        }
    }

    formatMessage(message) {
        // Replace line breaks with <br> tags
        return message.replace(/\n/g, '<br>');
    }

    error(message, duration = 5000) {
        return this.show(message, 'error', '❌', duration);
    }

    warning(message, duration = 5000) {
        return this.show(message, 'warning', '⚠️', duration);
    }

    success(message, duration = 5000) {
        return this.show(message, 'success', '✅', duration);
    }

    info(message, duration = 5000) {
        return this.show(message, 'info', 'ℹ️', duration);
    }

    clearAll() {
        if (this.container) {
            this.container.innerHTML = '';
        }
    }
}

// Initialize when DOM is loaded
document.addEventListener('DOMContentLoaded', () => {
    window.toastManager = new ToastManager();
});

// Add slideUp animation to CSS
const style = document.createElement('style');
style.textContent = `
    @keyframes slideUp {
        from {
            opacity: 1;
            transform: translateY(0);
        }
        to {
            opacity: 0;
            transform: translateY(-20px);
        }
    }
`;
document.head.appendChild(style);