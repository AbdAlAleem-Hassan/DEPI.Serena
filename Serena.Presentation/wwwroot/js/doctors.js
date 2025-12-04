// doctors.js
let doctors = [];
let filteredDoctors = [];
let currentDoctorView = 'grid';
let currentDoctorPage = 1;
const doctorsPerPage = 9;
let doctorMarkers = [];
let doctorMap;
let activeDoctorFilters = {};
let userLocation = null;
let locationCircle = null;
let userMarker = null;

// Toaster functions
function showToast(message, type = 'error') {
    const toasterContainer = document.getElementById('toasterContainer') || createToasterContainer();
    
    const toast = document.createElement('div');
    toast.className = `toast ${type}`;
    toast.innerHTML = `
        <div class="toast-icon">
            <i class="fas fa-${type === 'error' ? 'exclamation-triangle' : type === 'warning' ? 'exclamation-circle' : 'check-circle'}"></i>
        </div>
        <div class="toast-message">${message}</div>
        <button class="toast-close" onclick="this.parentElement.remove()">
            <i class="fas fa-times"></i>
        </button>
    `;
    
    toasterContainer.appendChild(toast);
    
    // Auto remove after 5 seconds
    setTimeout(() => {
        if (toast.parentElement) {
            toast.remove();
        }
    }, 5000);
}

function createToasterContainer() {
    const container = document.createElement('div');
    container.id = 'toasterContainer';
    container.className = 'toaster-container';
    document.body.appendChild(container);
    return container;
}

// Doctor data
const doctorData = [
    {
        id: 1,
        name: 'Dr. Sarah Johnson',
        nameAr: 'د. سارة جونسون',
        specialty: 'Cardiology',
        specialtyAr: 'أمراض القلب',
        subSpecialty: 'Interventional',
        rating: 4.9,
        reviews: 342,
        image: 'https://images.unsplash.com/photo-1612349317150-e413f6a5b16d?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1000&q=80',
        experience: 15,
        patients: 5200,
        languages: ['English', 'Arabic'],
        availableNow: true,
        consultationFee: 250,
        insurance: ['private'],
        contractStatus: 'accepted',
        hospitalId: 1,
        hospitalName: 'Cairo Medical Center',
        hospitalNameAr: 'مركز القاهرة الطبي',
        hospitalCoordinates: [30.0444, 31.2357],
        hospitalDistance: 5
    },
    {
        id: 2,
        name: 'Dr. Michael Chen',
        nameAr: 'د. مايكل تشين',
        specialty: 'Neurology',
        specialtyAr: 'علم الأعصاب',
        subSpecialty: 'Non-interventional',
        rating: 4.8,
        reviews: 278,
        image: 'https://images.unsplash.com/photo-1559839734-2b71ea197ec2?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1000&q=80',
        experience: 12,
        patients: 3800,
        languages: ['English', 'French'],
        availableNow: false,
        consultationFee: 300,
        insurance: ['private'],
        contractStatus: 'accepted',
        hospitalId: 2,
        hospitalName: 'Alexandria General Hospital',
        hospitalNameAr: 'مستشفى الإسكندرية العام',
        hospitalCoordinates: [31.2001, 29.9187],
        hospitalDistance: 12
    },
    {
        id: 3,
        name: 'Dr. Emily Rodriguez',
        nameAr: 'د. إيميلي رودريجيز',
        specialty: 'Pediatrics',
        specialtyAr: 'طب الأطفال',
        subSpecialty: 'Non-interventional',
        rating: 4.7,
        reviews: 195,
        image: 'https://plus.unsplash.com/premium_photo-1661580574627-9211124e5c3f?ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8OXx8ZG9jdG9yc3xlbnwwfHwwfHx8MA%3D%3D&auto=format&fit=crop&q=60&w=600',
        experience: 10,
        patients: 2900,
        languages: ['English', 'Spanish'],
        availableNow: true,
        consultationFee: 200,
        insurance: ['public', 'private'],
        contractStatus: 'accepted',
        hospitalId: 3,
        hospitalName: 'Giza Specialty Hospital',
        hospitalNameAr: 'مستشفى الجيزة التخصصي',
        hospitalCoordinates: [30.0131, 31.2089],
        hospitalDistance: 8
    },
    {
        id: 4,
        name: 'Dr. James Wilson',
        nameAr: 'د. جيمس ويلسون',
        specialty: 'Orthopedics',
        specialtyAr: 'جراحة العظام',
        subSpecialty: 'Interventional',
        rating: 4.8,
        reviews: 223,
        image: 'https://images.unsplash.com/photo-1612349317150-e413f6a5b16d?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1000&q=80',
        experience: 14,
        patients: 4100,
        languages: ['English'],
        availableNow: true,
        consultationFee: 350,
        insurance: ['private'],
        contractStatus: 'not-accepted',
        hospitalId: 4,
        hospitalName: 'Sharm El Sheikh Medical',
        hospitalNameAr: 'مستشفى شرم الشيخ الطبي',
        hospitalCoordinates: [27.9158, 34.3300],
        hospitalDistance: 25
    },
    {
        id: 5,
        name: 'Dr. Lisa Anderson',
        nameAr: 'د. ليزا أندرسون',
        specialty: 'Dermatology',
        specialtyAr: 'الأمراض الجلدية',
        subSpecialty: 'Non-interventional',
        rating: 4.9,
        reviews: 189,
        image: 'https://images.unsplash.com/photo-1559839734-2b71ea197ec2?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1000&q=80',
        experience: 11,
        patients: 3200,
        languages: ['English', 'Arabic'],
        availableNow: false,
        consultationFee: 180,
        insurance: ['public'],
        contractStatus: 'accepted',
        hospitalId: 5,
        hospitalName: 'Luxor International Hospital',
        hospitalNameAr: 'مستشفى الأقصر الدولي',
        hospitalCoordinates: [25.7000, 31.4000],
        hospitalDistance: 15
    },
    {
        id: 6,
        name: 'Dr. Ahmed Hassan',
        nameAr: 'د. أحمد حسن',
        specialty: 'Surgery',
        specialtyAr: 'الجراحة',
        subSpecialty: 'Interventional',
        rating: 4.6,
        reviews: 156,
        image: 'https://images.unsplash.com/photo-1612349317150-e413f6a5b16d?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1000&q=80',
        experience: 8,
        patients: 2100,
        languages: ['Arabic', 'English'],
        availableNow: true,
        consultationFee: 280,
        insurance: ['public', 'private'],
        contractStatus: 'accepted',
        hospitalId: 1,
        hospitalName: 'Cairo Medical Center',
        hospitalNameAr: 'مركز القاهرة الطبي',
        hospitalCoordinates: [30.0444, 31.2357],
        hospitalDistance: 5
    },
    {
        id: 7,
        name: 'Dr. Lisa Ann',
        nameAr: 'د. ليزا ان',
        specialty: 'Dermatology',
        specialtyAr: 'الأمراض الجلدية',
        subSpecialty: 'Non-interventional',
        rating: 4.9,
        reviews: 189,
        image: 'https://images.unsplash.com/photo-1559839734-2b71ea197ec2?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1000&q=80',
        experience: 11,
        patients: 3200,
        languages: ['Arabic'],
        availableNow: false,
        consultationFee: 180,
        insurance: ['public'],
        contractStatus: 'accepted',
        hospitalId: 5,
        hospitalName: 'Luxor International Hospital',
        hospitalNameAr: 'مستشفى الأقصر الدولي',
        hospitalCoordinates: [25.7000, 31.4000],
        hospitalDistance: 15
    },
    {
        id: 8,
        name: 'Dr. Ahmed zahran',
        nameAr: 'د. أحمد زهران',
        specialty: 'Surgery',
        specialtyAr: 'الجراحة',
        subSpecialty: 'Interventional',
        rating: 4.6,
        reviews: 156,
        image: 'https://images.unsplash.com/photo-1612349317150-e413f6a5b16d?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1000&q=80',
        experience: 8,
        patients: 2100,
        languages: ['Arabic', 'English'],
        availableNow: true,
        consultationFee: 280,
        insurance: ['public', 'private'],
        contractStatus: 'accepted',
        hospitalId: 1,
        hospitalName: 'Cairo Medical Center',
        hospitalNameAr: 'مركز القاهرة الطبي',
        hospitalCoordinates: [30.0444, 31.2357],
        hospitalDistance: 5
    },
    {
        id: 9,
        name: 'Dr. James Anderson',
        nameAr: 'د. جيمس اندرسون',
        specialty: 'Orthopedics',
        specialtyAr: 'جراحة العظام',
        subSpecialty: 'Interventional',
        rating: 4.8,
        reviews: 223,
        image: 'https://images.unsplash.com/photo-1612349317150-e413f6a5b16d?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1000&q=80',
        experience: 14,
        patients: 4100,
        languages: ['English'],
        availableNow: true,
        consultationFee: 350,
        insurance: ['private'],
        contractStatus: 'not-accepted',
        hospitalId: 4,
        hospitalName: 'Sharm El Sheikh Medical',
        hospitalNameAr: 'مستشفى شرم الشيخ الطبي',
        hospitalCoordinates: [27.9158, 34.3300],
        hospitalDistance: 25
    },
    {
        id: 10,
        name: 'Dr. JSarah Mohamed',
        nameAr: 'د. سارة محمد',
        specialty: 'Orthopedics',
        specialtyAr: 'جراحة العظام',
        subSpecialty: 'Interventional',
        rating: 4.8,
        reviews: 223,
        image: 'https://images.unsplash.com/photo-1612349317150-e413f6a5b16d?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1000&q=80',
        experience: 14,
        patients: 4100,
        languages: ['English'],
        availableNow: true,
        consultationFee: 350,
        insurance: ['private'],
        contractStatus: 'not-accepted',
        hospitalId: 4,
        hospitalName: 'Sharm El Sheikh Medical',
        hospitalNameAr: 'مستشفى شرم الشيخ الطبي',
        hospitalCoordinates: [27.9158, 34.3300],
        hospitalDistance: 25
    },
    {
        id: 11,
        name: 'Dr. JSarah Mohamed',
        nameAr: 'د. سارة محمد',
        specialty: 'Orthopedics',
        specialtyAr: 'جراحة العظام',
        subSpecialty: 'Interventional',
        rating: 4.8,
        reviews: 223,
        image: 'https://images.unsplash.com/photo-1612349317150-e413f6a5b16d?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1000&q=80',
        experience: 14,
        patients: 4100,
        languages: ['English'],
        availableNow: true,
        consultationFee: 350,
        insurance: ['private'],
        contractStatus: 'not-accepted',
        hospitalId: 4,
        hospitalName: 'Sharm El Sheikh Medical',
        hospitalNameAr: 'مستشفى شرم الشيخ الطبي',
        hospitalCoordinates: [27.9158, 34.3300],
        hospitalDistance: 25
    }
];

// Initialize doctors page
function initializeDoctorsPage() {
    doctors = doctorData.map(doctor => ({
        ...doctor,
        hospitalDistance: null
    }));
    filteredDoctors = [...doctors];

    initializeDoctorEventListeners();
    requestUserLocation();
    applyDoctorSorting();
    updateDoctorsResultsCount();
    updateActiveDoctorFiltersDisplay();
    initializeCustomDropdowns();
    
    // Force distance filter to be off initially
    const distanceToggle = document.getElementById('distanceToggle');
    if (distanceToggle) {
        distanceToggle.checked = false;
        updateDistanceFilterState(false);
    }

    // Add warning message
    addWarningMessage();
}

// Request user location
function requestUserLocation() {
    if (!navigator.geolocation) {
        showToast('Geolocation is not supported by this browser.', 'error');
        updateDistanceFilterState(false);
        return;
    }

    showToast('Requesting location access...', 'warning');
    
    navigator.geolocation.getCurrentPosition(
        (position) => {
            userLocation = {
                lat: position.coords.latitude,
                lng: position.coords.longitude
            };
            showToast('Location access granted! Calculating distances...', 'success');
            calculateDistances();
            if (doctorMap) {
                addUserLocationToMap();
            }
        },
        (error) => {
            handleLocationError(error);
            updateDistanceFilterState(false);
        },
        {
            enableHighAccuracy: true,
            timeout: 10000,
            maximumAge: 60000
        }
    );
}

// Handle location errors
function handleLocationError(error) {
    let message = 'Unable to retrieve your location. ';
    
    switch(error.code) {
        case error.PERMISSION_DENIED:
            message += 'Location access was denied.';
            break;
        case error.POSITION_UNAVAILABLE:
            message += 'Location information is unavailable.';
            break;
        case error.TIMEOUT:
            message += 'Location request timed out.';
            break;
        default:
            message += 'An unknown error occurred.';
            break;
    }
    
    showToast(message, 'error');
    console.error('Location error:', error);
}

// Calculate distances using Haversine formula
function calculateDistances() {
    if (!userLocation) return;

    doctors.forEach(doctor => {
        doctor.hospitalDistance = calculateDistance(
            userLocation.lat,
            userLocation.lng,
            doctor.hospitalCoordinates[0],
            doctor.hospitalCoordinates[1]
        );
    });

    filteredDoctors = [...doctors];
    applyDoctorFilters();
}

// Haversine formula to calculate distance between two coordinates
function calculateDistance(lat1, lon1, lat2, lon2) {
    const R = 6371; // Earth's radius in kilometers
    const dLat = (lat2 - lat1) * Math.PI / 180;
    const dLon = (lon2 - lon1) * Math.PI / 180;
    const a = 
        Math.sin(dLat/2) * Math.sin(dLat/2) +
        Math.cos(lat1 * Math.PI / 180) * Math.cos(lat2 * Math.PI / 180) *
        Math.sin(dLon/2) * Math.sin(dLon/2);
    const c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1-a));
    const distance = R * c;
    return Math.round(distance * 10) / 10; // Round to 1 decimal place
}

// Update distance filter state
function updateDistanceFilterState(enabled) {
    const distanceToggle = document.getElementById('distanceToggle');
    const distanceFilter = document.getElementById('distanceFilter');
    const distanceContainer = document.getElementById('distanceFilterContainer');
    
    if (enabled && userLocation) {
        distanceToggle.checked = true;
        distanceFilter.disabled = false;
        distanceContainer.classList.remove('distance-unavailable');
        // Update range value display
        const currentLang = document.body.classList.contains('rtl') ? 'ar' : 'en';
        const distanceValue = document.getElementById('distanceValue');
        distanceValue.textContent = `${distanceFilter.value} ${currentLang === 'ar' ? 'كم' : 'km'}`;
        updateRangeProgress(distanceFilter);
    } else {
        distanceToggle.checked = false;
        distanceFilter.disabled = true;
        distanceContainer.classList.add('distance-unavailable');
    }
}

// Add warning message
function addWarningMessage() {
    const resultsSection = document.querySelector('.results-section .container');
    const warningHTML = `
        <div class="warning-message">
            <div class="warning-icon">
                <i class="fas fa-exclamation-circle"></i>
            </div>
            <div class="warning-content">
                <h4 data-en="Important Notice" data-ar="إشعار هام">Important Notice</h4>
                <p data-en="This display shows the top doctors in our network. To access the complete list of doctors, please visit the hospital page to view their full doctors list." data-ar="هذا العرض يظهر أفضل الأطباء في شبكتنا. للوصول لجميع الأطباء، يرجى زيارة صفحة المستشفى لعرض قائمة الأطباء الكاملة الخاصة بها.">
                    This display shows the top doctors in our network. To access the complete list of doctors, please visit the hospital page to view their full doctors list.
                </p>
            </div>
        </div>
    `;
    
    resultsSection.insertAdjacentHTML('afterbegin', warningHTML);
}

// Initialize custom dropdowns for doctors
function initializeCustomDropdowns() {
    const dropdowns = document.querySelectorAll('.custom-dropdown');
    
    dropdowns.forEach(dropdown => {
        const select = dropdown.querySelector('.custom-dropdown-select');
        const options = dropdown.querySelector('.custom-dropdown-options');
        const optionsList = dropdown.querySelectorAll('.custom-dropdown-option');
        const hiddenInput = dropdown.querySelector('select');
        
        // Set initial value - select first option
        if (optionsList.length > 0) {
            const firstOption = optionsList[0];
            const value = firstOption.getAttribute('data-value');
            const text = firstOption.textContent;
            
            select.innerHTML = text + '<i class="fas fa-chevron-down"></i>';
            hiddenInput.value = value;
            
            // Add selected class to first option
            optionsList.forEach(opt => opt.classList.remove('selected'));
            firstOption.classList.add('selected');
        }
        
        // Toggle dropdown
        select.addEventListener('click', (e) => {
            e.stopPropagation();
            options.classList.toggle('active');
            
            // Close other dropdowns
            document.querySelectorAll('.custom-dropdown-options').forEach(otherOptions => {
                if (otherOptions !== options) {
                    otherOptions.classList.remove('active');
                }
            });
        });
        
        // Handle option selection
        optionsList.forEach(option => {
            option.addEventListener('click', () => {
                const value = option.getAttribute('data-value');
                const text = option.textContent;
                
                // Update select display
                select.innerHTML = text + '<i class="fas fa-chevron-down"></i>';
                
                // Update hidden input
                hiddenInput.value = value;
                
                // Close dropdown
                options.classList.remove('active');
                
                // Trigger change event
                hiddenInput.dispatchEvent(new Event('change'));
                
                // Remove selected class from all options
                optionsList.forEach(opt => opt.classList.remove('selected'));
                // Add selected class to current option
                option.classList.add('selected');
            });
        });
        
        // Close dropdown when clicking outside
        document.addEventListener('click', () => {
            options.classList.remove('active');
        });
    });
}

// Reset custom dropdown to first option
function resetCustomDropdown(dropdownId) {
    const dropdown = document.querySelector(`#${dropdownId}`).closest('.custom-dropdown');
    if (!dropdown) return;
    
    const select = dropdown.querySelector('.custom-dropdown-select');
    const options = dropdown.querySelectorAll('.custom-dropdown-option');
    const hiddenInput = dropdown.querySelector('select');
    
    if (options.length > 0) {
        const firstOption = options[0];
        const value = firstOption.getAttribute('data-value');
        const text = firstOption.textContent;
        
        select.innerHTML = text + '<i class="fas fa-chevron-down"></i>';
        hiddenInput.value = value;
        
        // Update selected class
        options.forEach(opt => opt.classList.remove('selected'));
        firstOption.classList.add('selected');
    }
}

// Initialize event listeners for doctors page
function initializeDoctorEventListeners() {
    // View toggle
    const viewButtons = document.querySelectorAll('.view-btn');
    viewButtons.forEach(btn => {
        btn.addEventListener('click', () => {
            viewButtons.forEach(b => b.classList.remove('active'));
            btn.classList.add('active');
            currentDoctorView = btn.getAttribute('data-view');
            localStorage.setItem('currentDoctorView', currentDoctorView);
            switchDoctorView(currentDoctorView);
        });
    });

    // Search functionality
    const searchInput = document.getElementById('doctorSearch');
    const searchBtn = document.querySelector('.search-btn');
    
    searchInput.addEventListener('input', handleDoctorSearch);
    searchBtn.addEventListener('click', handleDoctorSearch);

    // Filter functionality
    const filterSelects = document.querySelectorAll('.filter-select, .filter-checkbox input');
    filterSelects.forEach(filter => {
        filter.addEventListener('change', () => applyDoctorFilters());
    });

    // Distance filter toggle
    const distanceToggle = document.getElementById('distanceToggle');
    if (distanceToggle) {
        distanceToggle.addEventListener('change', () => {
            if (distanceToggle.checked && !userLocation) {
                // If toggle is on but no location, request location
                requestUserLocation();
            } else {
                updateDistanceFilterState(distanceToggle.checked && userLocation);
                applyDoctorFilters();
            }
        });
    }

    // Distance range filter
    const distanceFilter = document.getElementById('distanceFilter');
    const distanceValue = document.getElementById('distanceValue');

    // Initialize range progress
    updateRangeProgress(distanceFilter);

    distanceFilter.addEventListener('input', () => {
        updateRangeProgress(distanceFilter);
        const currentLang = document.body.classList.contains('rtl') ? 'ar' : 'en';
        distanceValue.textContent = `${distanceFilter.value} ${currentLang === 'ar' ? 'كم' : 'km'}`;
        // Only apply filters if distance toggle is actually on
        if (distanceToggle.checked && userLocation) {
            applyDoctorFilters();
        }
    });

    // Quick filter buttons
    const quickFilterBtns = document.querySelectorAll('.quick-filter-btn');
    quickFilterBtns.forEach(btn => {
        btn.addEventListener('click', () => {
            const filterType = btn.getAttribute('data-filter');
            applyDoctorQuickFilter(filterType);
        });
    });

    // More filters toggle
    const moreFiltersBtn = document.getElementById('moreDoctorFiltersBtn');
    const additionalFilters = document.getElementById('additionalDoctorFilters');
    
    if (moreFiltersBtn && additionalFilters) {
        moreFiltersBtn.addEventListener('click', () => {
            additionalFilters.classList.toggle('active');
            const icon = moreFiltersBtn.querySelector('i');
            icon.classList.toggle('fa-chevron-down');
            icon.classList.toggle('fa-chevron-up');
        });
    }

    // Clear filters
    const clearFiltersBtn = document.getElementById('clearDoctorFilters');
    if (clearFiltersBtn) {
        clearFiltersBtn.addEventListener('click', clearDoctorFilters);
    }

    // Sort functionality
    const sortSelect = document.getElementById('doctorSortBy');
    if (sortSelect) {
        sortSelect.addEventListener('change', applyDoctorSorting);
    }

    // Pagination
    updateDoctorPagination();
    setupDoctorPaginationEventListeners();
}

// Apply doctor quick filters
function applyDoctorQuickFilter(filterType) {
    // Reset all quick filter buttons
    document.querySelectorAll('.quick-filter-btn').forEach(btn => {
        btn.classList.remove('active');
    });
    
    // Activate the clicked button
    const clickedBtn = document.querySelector(`[data-filter="${filterType}"]`);
    if (clickedBtn) {
        clickedBtn.classList.add('active');
    }
    
    switch (filterType) {
        case 'available-now':
            document.getElementById('availabilityNowFilter').checked = true;
            break;
        case 'high-rating':
            document.getElementById('doctorRatingFilter').value = '4.5';
            updateCustomDropdownDisplay('doctorRatingFilter');
            break;
        case 'video-consultation':
            // This would require additional data field in doctor objects
            showToast('Video consultation filter applied', 'success');
            break;
        case 'english-speaking':
            document.getElementById('languageFilter').value = 'english';
            updateCustomDropdownDisplay('languageFilter');
            break;
    }
    
    applyDoctorFilters();
}

// Update custom dropdown display
function updateCustomDropdownDisplay(selectId) {
    const select = document.getElementById(selectId);
    const dropdown = select.closest('.custom-dropdown');
    const customSelect = dropdown.querySelector('.custom-dropdown-select');
    const options = dropdown.querySelectorAll('.custom-dropdown-option');
    
    const selectedOption = Array.from(options).find(option => 
        option.getAttribute('data-value') === select.value
    );
    
    if (selectedOption) {
        customSelect.innerHTML = selectedOption.innerHTML + '<i class="fas fa-chevron-down"></i>';
        options.forEach(opt => opt.classList.remove('selected'));
        selectedOption.classList.add('selected');
    }
}

// Distance range progress update
function updateRangeProgress(range) {
    const value = (range.value - range.min) / (range.max - range.min) * 100;
    range.style.setProperty('--range-progress', value + '%');
}

// Handle doctor search functionality
function handleDoctorSearch() {
    const searchTerm = document.getElementById('doctorSearch').value.toLowerCase();
    
    if (searchTerm.length >= 2) {
        showDoctorAutocompleteSuggestions(searchTerm);
    } else {
        hideDoctorAutocomplete();
    }
    
    applyDoctorFilters();
}

// Show doctor autocomplete suggestions
function showDoctorAutocompleteSuggestions(searchTerm) {
    const autocomplete = document.getElementById('doctorSearchAutocomplete');
    const suggestions = doctors.filter(doctor => 
        doctor.name.toLowerCase().includes(searchTerm) || 
        doctor.specialty.toLowerCase().includes(searchTerm)
    ).slice(0, 5);
    
    if (suggestions.length > 0) {
        autocomplete.innerHTML = suggestions.map(doctor => 
            `<div class="autocomplete-item" data-id="${doctor.id}">${doctor.name} - ${doctor.specialty}</div>`
        ).join('');
        autocomplete.classList.add('active');
        
        // Add click event to autocomplete items
        document.querySelectorAll('.autocomplete-item').forEach(item => {
            item.addEventListener('click', () => {
                document.getElementById('doctorSearch').value = item.textContent;
                hideDoctorAutocomplete();
                applyDoctorFilters();
            });
        });
    } else {
        hideDoctorAutocomplete();
    }
}

// Hide doctor autocomplete
function hideDoctorAutocomplete() {
    const autocomplete = document.getElementById('doctorSearchAutocomplete');
    autocomplete.classList.remove('active');
    autocomplete.innerHTML = '';
}

// Apply doctor filters
function applyDoctorFilters() {
    const searchTerm = document.getElementById('doctorSearch').value.toLowerCase();
    const specialtyFilter = document.getElementById('doctorSpecialtyFilter').value;
    const subSpecialtyFilter = document.getElementById('subSpecialtyFilter').value;
    const ratingFilter = parseFloat(document.getElementById('doctorRatingFilter').value);
    const languageFilter = document.getElementById('languageFilter').value;
    const availabilityFilter = document.getElementById('availabilityNowFilter')?.checked || false;
    const feeFilter = document.getElementById('feeFilter').value;
    const insuranceFilter = document.getElementById('doctorInsuranceFilter').value;
    const contractFilter = document.getElementById('doctorContractFilter').value;
    const distanceFilterValue = parseFloat(document.getElementById('distanceFilter').value);
    const distanceToggle = document.getElementById('distanceToggle');
    const isDistanceFilterActive = distanceToggle.checked && userLocation;
    
    // Update active filters
    activeDoctorFilters = {};
    if (searchTerm) activeDoctorFilters.search = searchTerm;
    if (specialtyFilter) activeDoctorFilters.specialty = specialtyFilter;
    if (subSpecialtyFilter) activeDoctorFilters.subSpecialty = subSpecialtyFilter;
    if (ratingFilter > 0) activeDoctorFilters.rating = ratingFilter;
    if (languageFilter) activeDoctorFilters.language = languageFilter;
    if (availabilityFilter) activeDoctorFilters.availability = true;
    if (feeFilter) activeDoctorFilters.fee = feeFilter;
    if (insuranceFilter) activeDoctorFilters.insurance = insuranceFilter;
    if (contractFilter) activeDoctorFilters.contract = contractFilter;
    if (isDistanceFilterActive) activeDoctorFilters.distance = distanceFilterValue;

    filteredDoctors = doctors.filter(doctor => {
        // Search filter
        const matchesSearch = !searchTerm || 
            doctor.name.toLowerCase().includes(searchTerm) ||
            doctor.specialty.toLowerCase().includes(searchTerm) ||
            doctor.hospitalName.toLowerCase().includes(searchTerm);
        
        // Specialty filter
        const matchesSpecialty = !specialtyFilter || 
            doctor.specialty.toLowerCase().includes(specialtyFilter);
        
        // Sub-specialty filter
        const matchesSubSpecialty = !subSpecialtyFilter || 
            doctor.subSpecialty.toLowerCase().includes(subSpecialtyFilter);
        
        // Rating filter
        const matchesRating = !ratingFilter || doctor.rating >= ratingFilter;
        
        // Distance filter
        const matchesDistance = !isDistanceFilterActive || 
                              (doctor.hospitalDistance !== null && doctor.hospitalDistance <= distanceFilterValue);

        // Language filter
        const matchesLanguage = !languageFilter || 
            doctor.languages.some(lang => lang.toLowerCase().includes(languageFilter));
        
        // Availability filter
        const matchesAvailability = !availabilityFilter || doctor.availableNow;
        
        // Fee filter
        const matchesFee = !feeFilter || (
            (feeFilter === 'low' && doctor.consultationFee <= 100) ||
            (feeFilter === 'medium' && doctor.consultationFee > 100 && doctor.consultationFee <= 300) ||
            (feeFilter === 'high' && doctor.consultationFee > 300)
        );
        
        // Insurance filter
        const matchesInsurance = !insuranceFilter || doctor.insurance.includes(insuranceFilter);
        
        // Contract filter
        const matchesContract = !contractFilter || doctor.contractStatus === contractFilter;
        
        return matchesSearch && matchesSpecialty && matchesSubSpecialty && matchesRating && 
               matchesDistance && matchesLanguage && matchesAvailability && matchesFee && matchesInsurance && matchesContract;
    });
    
    currentDoctorPage = 1;
    applyDoctorSorting();
    updateDoctorsResultsCount();
    renderDoctors();
    updateDoctorMap();
    updateActiveDoctorFiltersDisplay();
    updateDoctorPagination();
}

// Apply doctor sorting
function applyDoctorSorting() {
    const sortBy = document.getElementById('doctorSortBy').value;
    
    filteredDoctors.sort((a, b) => {
        switch (sortBy) {
            case 'name':
                return a.name.localeCompare(b.name);
            case 'rating':
                return b.rating - a.rating;
            case 'experience':
                return b.experience - a.experience;
            case 'fee':
                return a.consultationFee - b.consultationFee;
            case 'distance':
                if (a.hospitalDistance === null && b.hospitalDistance === null) return 0;
                if (a.hospitalDistance === null) return 1;
                if (b.hospitalDistance === null) return -1;
                return a.hospitalDistance - b.hospitalDistance;
            default:
                return 0;
        }
    });
    
    renderDoctors();
    updateDoctorMap();
}

// Update active filters display for doctors
function updateActiveDoctorFiltersDisplay() {
    const container = document.getElementById('activeDoctorFilters');
    if (!container) return;
    
    container.innerHTML = '';
    
    Object.entries(activeDoctorFilters).forEach(([key, value]) => {
        const filterText = getDoctorFilterDisplayText(key, value);
        if (filterText) {
            const tag = document.createElement('div');
            tag.className = 'active-filter-tag';
            tag.innerHTML = `
                <span>${filterText}</span>
                <button class="remove-filter" data-filter="${key}">
                    <i class="fas fa-times"></i>
                </button>
            `;
            container.appendChild(tag);
        }
    });
    
    // Add event listeners to remove buttons
    document.querySelectorAll('.remove-filter').forEach(btn => {
        btn.addEventListener('click', (e) => {
            const filterKey = e.currentTarget.getAttribute('data-filter');
            removeDoctorFilter(filterKey);
        });
    });
}

// Get display text for doctor filter
function getDoctorFilterDisplayText(key, value) {
    const currentLang = document.body.classList.contains('rtl') ? 'ar' : 'en';
    
    const texts = {
        search: { en: `Search: ${value}`, ar: `بحث: ${value}` },
        specialty: { en: `Specialty: ${value}`, ar: `التخصص: ${value}` },
        subSpecialty: {
            interventional: { en: 'Interventional', ar: 'تدخلي' },
            'non-interventional': { en: 'Non-interventional', ar: 'غير تدخلي' }
        },
        rating: { en: `Rating: ${value}+`, ar: `التقييم: ${value}+` },
        language: {
            arabic: { en: 'Arabic', ar: 'العربية' },
            english: { en: 'English', ar: 'الإنجليزية' },
            french: { en: 'French', ar: 'الفرنسية' }
        },
        availability: { en: 'Available Now', ar: 'متاح الآن' },
        fee: {
            low: { en: 'Fee: Low', ar: 'الرسوم: منخفض' },
            medium: { en: 'Fee: Medium', ar: 'الرسوم: متوسط' },
            high: { en: 'Fee: High', ar: 'الرسوم: مرتفع' }
        },
        insurance: {
            public: { en: 'Public Insurance', ar: 'تأمين حكومي' },
            private: { en: 'Private Insurance', ar: 'تأمين خاص' }
        },
        contract: {
            accepted: { en: 'Contract Accepted', ar: 'عقد مقبول' },
            'not-accepted': { en: 'Contract Not Accepted', ar: 'عقد غير مقبول' }
        },
        distance: { en: `Distance: ${value} km`, ar: `المسافة: ${value} كم` }
    };
    
    if (key === 'distance') {
        return texts.distance[currentLang];
    }
    if (key === 'subSpecialty' && texts.subSpecialty[value]) {
        return texts.subSpecialty[value][currentLang];
    }
    if (key === 'language' && texts.language[value]) {
        return texts.language[value][currentLang];
    }
    if (key === 'fee' && texts.fee[value]) {
        return texts.fee[value][currentLang];
    }
    if (key === 'insurance' && texts.insurance[value]) {
        return texts.insurance[value][currentLang];
    }
    if (key === 'contract' && texts.contract[value]) {
        return texts.contract[value][currentLang];
    }
    if (texts[key] && typeof texts[key] === 'object' && texts[key][currentLang]) {
        return texts[key][currentLang];
    }
    
    return null;
}

// Remove specific doctor filter
function removeDoctorFilter(filterKey) {
    switch (filterKey) {
        case 'search':
            document.getElementById('doctorSearch').value = '';
            break;
        case 'specialty':
            resetCustomDropdown('doctorSpecialtyFilter');
            break;
        case 'subSpecialty':
            resetCustomDropdown('subSpecialtyFilter');
            break;
        case 'rating':
            resetCustomDropdown('doctorRatingFilter');
            break;
        case 'distance':
            document.getElementById('distanceToggle').checked = false;
            updateDistanceFilterState(false);
            break;
        case 'language':
            resetCustomDropdown('languageFilter');
            break;
        case 'availability':
            const availabilityCheckbox = document.getElementById('availabilityNowFilter');
            if (availabilityCheckbox) availabilityCheckbox.checked = false;
            break;
        case 'fee':
            resetCustomDropdown('feeFilter');
            break;
        case 'insurance':
            resetCustomDropdown('doctorInsuranceFilter');
            break;
        case 'contract':
            resetCustomDropdown('doctorContractFilter');
            break;
    }
    
    // Reset quick filter buttons
    document.querySelectorAll('.quick-filter-btn').forEach(btn => {
        btn.classList.remove('active');
    });
    
    applyDoctorFilters();
}

// Clear all doctor filters
function clearDoctorFilters() {
    document.getElementById('doctorSearch').value = '';
    
    // Reset all dropdowns to first option
    resetCustomDropdown('doctorSpecialtyFilter');
    resetCustomDropdown('subSpecialtyFilter');
    resetCustomDropdown('doctorRatingFilter');
    resetCustomDropdown('languageFilter');
    resetCustomDropdown('feeFilter');
    resetCustomDropdown('doctorInsuranceFilter');
    resetCustomDropdown('doctorContractFilter');
    
    // Reset checkboxes and toggles
    document.getElementById('distanceToggle').checked = false;
    document.getElementById('distanceFilter').value = '50';
    
    const availabilityFilter = document.getElementById('availabilityNowFilter');
    if (availabilityFilter) availabilityFilter.checked = false;
    
    // Clear quick filters
    document.querySelectorAll('.quick-filter-btn').forEach(btn => {
        btn.classList.remove('active');
    });
    
    // Reset additional filters
    const additionalFilters = document.getElementById('additionalFilters');
    if (additionalFilters) {
        additionalFilters.classList.remove('active');
    }
    
    updateDistanceFilterState(false);
    updateRangeProgress(document.getElementById('distanceFilter'));
    hideDoctorAutocomplete();
    applyDoctorFilters();
}

// Switch between doctor views
function switchDoctorView(view) {
    document.querySelectorAll('.results-grid, .results-list, .results-map').forEach(el => {
        el.classList.remove('active');
    });
    
    document.getElementById(`doctors${capitalizeFirstLetter(view)}View`).classList.add('active');
    
    if (view === 'map') {
        if (!doctorMap) {
            initializeDoctorMap();
        } else {
            updateDoctorMap();
        }
    }
    
    renderDoctors();
}

function capitalizeFirstLetter(string) {
    return string.charAt(0).toUpperCase() + string.slice(1);
}

// Render doctors based on current view
function renderDoctors() {
    const startIndex = (currentDoctorPage - 1) * doctorsPerPage;
    const endIndex = startIndex + doctorsPerPage;
    const doctorsToShow = filteredDoctors.slice(startIndex, endIndex);
    
    switch (currentDoctorView) {
        case 'grid':
            renderDoctorsGridView(doctorsToShow);
            break;
        case 'list':
            renderDoctorsListView(doctorsToShow);
            break;
        case 'map':
            renderDoctorsMapView(doctorsToShow);
            break;
    }
}

// Render doctors grid view
function renderDoctorsGridView(doctorsToShow) {
    const container = document.getElementById('doctorsGrid');
    if (!container) return;
    
    if (doctorsToShow.length === 0) {
        container.style.display = 'grid';
        container.style.gridTemplateColumns = '1fr';
        container.innerHTML = getNoResultsHTML();
        return;
    } else {
        container.style.display = 'grid';
        container.style.gridTemplateColumns = 'repeat(auto-fill, minmax(300px, 1fr))';
    }
    
    container.innerHTML = doctorsToShow.map(doctor => `
        <div class="doctor-card" data-id="${doctor.id}">
            <div class="doctor-image">
                <img src="${doctor.image}" alt="${doctor.name}">
                <div class="doctor-rating">
                    <i class="fas fa-star"></i>
                    <span>${doctor.rating}</span>
                </div>
                ${doctor.availableNow ? '<div class="doctor-badge" data-en="Available" data-ar="متاح">Available</div>' : ''}
            </div>
            <div class="doctor-content">
                <h3 class="doctor-name" data-en="${doctor.name}" data-ar="${doctor.nameAr}">${doctor.name}</h3>
                <div class="doctor-specialty">
                    <i class="fas fa-stethoscope"></i>
                    <span data-en="${doctor.specialty}" data-ar="${doctor.specialtyAr}">${doctor.specialty}</span> - <span>${doctor.subSpecialty}</span>
                </div>
                <div class="doctor-location">
                    <i class="fas fa-hospital"></i>
                    <span data-en="${doctor.hospitalName}" data-ar="${doctor.hospitalNameAr}">${doctor.hospitalName}</span> - <span class="hospital-distance">${doctor.hospitalDistance !== null ? doctor.hospitalDistance : '--'} km</span>
                </div>
                <div class="doctor-languages">
                    ${doctor.languages.slice(0, 3).map(lang => `<span class="language-tag">${lang}</span>`).join('')}
                    ${doctor.languages.length > 3 ? `<span class="more-languages">+${doctor.languages.length - 3}</span>` : ''}
                </div>
                <div class="doctor-stats">
                    <div class="doctor-stat">
                        <div class="doctor-stat-value">${doctor.experience}+</div>
                        <div class="doctor-stat-label" data-en="Years Exp." data-ar="سنوات خبرة">Years Exp.</div>
                    </div>
                    <div class="doctor-stat">
                        <div class="doctor-stat-value">${doctor.patients}+</div>
                        <div class="doctor-stat-label" data-en="Patients" data-ar="مرضى">Patients</div>
                    </div>
                </div>
                <div class="doctor-fee">
                    <span class="fee-label" data-en="Consultation Fee:" data-ar="رسوم الاستشارة:">Consultation Fee:</span>
                    <span class="fee-amount">$${doctor.consultationFee}</span>
                </div>
                <div class="doctor-actions">
                    <div class="top-action-buttons">
                        <button class="doctor-btn book-appointment" data-id="${doctor.id}">
                            <i class="fas fa-calendar-check"></i>
                            <span data-en="Book Appointment" data-ar="حجز موعد">Book Appointment</span>
                        </button>
                        <button class="doctor-btn secondary view-profile" data-id="${doctor.id}">
                            <i class="fas fa-user"></i>
                            <span data-en="View Profile" data-ar="عرض الملف">View Profile</span>
                        </button>
                    </div>
                    <button class="doctor-btn secondary view-on-map" data-id="${doctor.id}">
                        <i class="fas fa-map-marker-alt"></i>
                        <span data-en="View on Map" data-ar="عرض على الخريطة">View on Map</span>
                    </button>
                </div>
            </div>
        </div>
    `).join('');
    
    // Add event listeners to buttons
    document.querySelectorAll('.book-appointment').forEach(btn => {
        btn.addEventListener('click', (e) => {
            const doctorId = e.currentTarget.getAttribute('data-id');
            bookAppointment(doctorId);
        });
    });
    
    document.querySelectorAll('.view-profile').forEach(btn => {
        btn.addEventListener('click', (e) => {
            const doctorId = e.currentTarget.getAttribute('data-id');
            viewDoctorProfile(doctorId);
        });
    });

    document.querySelectorAll('.view-on-map').forEach(btn => {
        btn.addEventListener('click', (e) => {
            e.preventDefault();
            e.stopPropagation();
            const doctorId = e.currentTarget.getAttribute('data-id');
            switchToMapView(doctorId);
        });
    });
}

// Render doctors list view
function renderDoctorsListView(doctorsToShow) {
    console.log(doctorsToShow);
    const container = document.getElementById('doctorsTable');
    console.log(container);
    if (!container) return;
    
    if (doctorsToShow.length === 0) {
        container.innerHTML = getNoResultsHTML();
        return;
    }
    
    container.innerHTML = `
        <table class="doctors-table">
            <thead>
                <tr>
                    <th data-en="Doctor" data-ar="الطبيب">Doctor</th>
                    <th data-en="Specialty" data-ar="التخصص">Specialty</th>
                    <th data-en="Rating" data-ar="التقييم">Rating</th>
                    <th data-en="Experience" data-ar="الخبرة">Experience</th>
                    <th data-en="Fee" data-ar="الرسوم">Fee</th>
                    <th data-en="Available" data-ar="متاح">Available</th>
                    <th data-en="Actions" data-ar="الإجراءات">Actions</th>
                </tr>
            </thead>
            <tbody>
                ${doctorsToShow.map(doctor => `
                    <tr data-id="${doctor.id}">
                        <td>
                            <div class="table-doctor-info">
                                <div class="table-doctor-image">
                                    <img src="${doctor.image}" alt="${doctor.name}">
                                </div>
                                <div class="table-doctor-details">
                                    <div class="table-doctor-name" data-en="${doctor.name}" data-ar="${doctor.nameAr}">${doctor.name}</div>
                                </div>
                            </div>
                        </td>
                        <td>
                            <div class="doctor-specialty-info">
                                <div class="specialty-main" data-en="${doctor.specialty}" data-ar="${doctor.specialtyAr}">${doctor.specialty}</div>
                                <div class="specialty-sub">${doctor.subSpecialty}</div>
                            </div>
                        </td>
                        <td>
                            <div class="doctor-rating-cell">
                                <i class="fas fa-star"></i>
                                <span>${doctor.rating}</span>
                                <span class="reviews">(${doctor.reviews})</span>
                            </div>
                        </td>
                        <td class="doctor-experience">${doctor.experience}+ years</td>
                        <td class="doctor-fee">$${doctor.consultationFee}</td>
                        <td>
                            <div class="availability-indicator ${doctor.availableNow ? 'available' : 'offline'}">
                                <div class="availability-dot"></div>
                                <span>${doctor.availableNow ? 'Yes' : 'No'}</span>
                            </div>
                        </td>
                        <td>
                            <div class="table-action-buttons">
                                <button class="table-btn primary book-appointment" data-id="${doctor.id}">
                                    <span data-en="Book" data-ar="حجز">Book</span>
                                </button>
                                <div class="top-action-buttons">
                                    <button class="table-btn secondary view-on-map" data-id="${doctor.id}">
                                        <i class="fas fa-map-marker-alt"></i>
                                    </button>
                                    <button class="table-btn secondary view-profile" data-id="${doctor.id}">
                                        <i class="fas fa-user"></i>
                                    </button>
                                </div>
                            </div>
                        </td>
                    </tr>
                `).join('')}
            </tbody>
        </table>
    `;
    
    // Add event listeners to buttons
    document.querySelectorAll('.book-appointment').forEach(btn => {
        btn.addEventListener('click', (e) => {
            const doctorId = e.currentTarget.getAttribute('data-id');
            bookAppointment(doctorId);
        });
    });
    
    document.querySelectorAll('.view-profile').forEach(btn => {
        btn.addEventListener('click', (e) => {
            const doctorId = e.currentTarget.getAttribute('data-id');
            viewDoctorProfile(doctorId);
        });
    });

    document.querySelectorAll('.view-on-map').forEach(btn => {
        btn.addEventListener('click', (e) => {
            e.preventDefault();
            e.stopPropagation();
            const doctorId = e.currentTarget.getAttribute('data-id');
            switchToMapView(doctorId);
        });
    });
}

// Render doctors map view
function renderDoctorsMapView(doctorsToShow) {
    const container = document.getElementById('mapDoctorsList');
    if (!container) return;
    
    if (doctorsToShow.length === 0) {
        container.innerHTML = getNoResultsHTML();
        return;
    }
    
    container.innerHTML = doctorsToShow.map(doctor => `
        <div class="map-doctor-item" data-id="${doctor.id}" onclick="focusOnDoctorMarker(${doctor.id})">
            <div class="map-doctor-image">
                <img src="${doctor.image}" alt="${doctor.name}" loading="lazy">
            </div>
            <div class="map-doctor-info">
                <h4>${doctor.name}</h4>
                <div class="map-doctor-details">
                    <div class="map-doctor-specialty">${doctor.specialty}</div>
                    <div class="map-doctor-stats">
                        <div class="map-doctor-rating">
                            <i class="fas fa-star"></i>
                            <span>${doctor.rating}</span>
                        </div>
                        <div class="map-doctor-distance">
                            <i class="fas fa-route"></i>
                            <span>${doctor.hospitalDistance !== null ? `${doctor.hospitalDistance} ${document.body.classList.contains('rtl') ? 'كم' : 'km'}` : '--'}</span>
                        </div>
                    </div>
                </div>
            </div>
            <button class="map-doctor-btn" onclick="event.stopPropagation(); viewDoctorProfile(${doctor.id})">
                <i class="fas fa-chevron-right"></i>
            </button>
        </div>
    `).join('');
}

// Get no results HTML
function getNoResultsHTML() {
    return `
        <div class="no-results">
            <i class="fas fa-user-md"></i>
            <h3 data-en="No doctors found" data-ar="لم يتم العثور على أطباء">No doctors found</h3>
            <p data-en="Try adjusting your filters or search terms" data-ar="حاول تعديل الفلاتر أو مصطلحات البحث">Try adjusting your filters or search terms</p>
        </div>
    `;
}

// Switch to map view and focus on doctor
function switchToMapView(doctorId) {
    // Switch to map view UI
    document.querySelectorAll('.view-btn').forEach(btn => btn.classList.remove('active'));
    document.querySelector('[data-view="map"]').classList.add('active');
    currentDoctorView = 'map';
    localStorage.setItem('currentDoctorView', currentDoctorView);
    
    // Switch the view
    switchDoctorView('map');
    
    // Use retry mechanism to ensure map is ready before focusing
    retryMapOperation(() => {
        focusOnDoctorMarker(doctorId);
    }).catch(error => {
        console.error('Failed to focus on doctor after retries:', error);
        showToast('Failed to load map view', 'error');
    });
}

// Check if map is ready for operations
function isMapReady() {
    return doctorMap && typeof doctorMap.setView === 'function';
}

// Retry mechanism for map operations
function retryMapOperation(operation, maxRetries = 3, delay = 200) {
    return new Promise((resolve, reject) => {
        let retries = 0;
        
        function attempt() {
            if (isMapReady()) {
                resolve(operation());
            } else if (retries < maxRetries) {
                retries++;
                setTimeout(attempt, delay);
            } else {
                reject(new Error('Map not ready after maximum retries'));
            }
        }
        
        attempt();
    });
}

// Update doctors results count
function updateDoctorsResultsCount() {
    const resultsCount = document.getElementById('doctorsResultsCount');
    const currentLang = document.body.classList.contains('rtl') ? 'ar' : 'en';
    
    if (currentLang === 'ar') {
        resultsCount.textContent = `عرض ${filteredDoctors.length} طبيب`;
    } else {
        resultsCount.textContent = `Showing ${filteredDoctors.length} doctors`;
    }
}

// Setup doctor pagination event listeners
function setupDoctorPaginationEventListeners() {
    document.addEventListener('click', (e) => {
        if (e.target.closest('.pagination-btn') && !e.target.closest('.pagination-btn').classList.contains('disabled')) {
            const page = parseInt(e.target.closest('.pagination-btn').getAttribute('data-page'));
            if (page) {
                currentDoctorPage = page;
                renderDoctors();
                updateDoctorPagination();
            }
        }
    });
}

// Update doctor pagination
function updateDoctorPagination() {
    const totalPages = Math.ceil(filteredDoctors.length / doctorsPerPage);
    const paginationContainer = document.getElementById('doctorPagination');
    
    if (!paginationContainer || totalPages <= 1) {
        if (paginationContainer) {
            paginationContainer.style.display = 'none';
        }
        return;
    }
    
    paginationContainer.style.display = 'flex';
    
    let paginationHTML = '';
    
    // Previous button
    if (currentDoctorPage > 1) {
        paginationHTML += `<button class="pagination-btn" data-page="${currentDoctorPage - 1}">
            <i class="fas fa-chevron-left"></i>
        </button>`;
    } else {
        paginationHTML += `<button class="pagination-btn disabled">
            <i class="fas fa-chevron-left"></i>
        </button>`;
    }
    
    // Page numbers
    const maxVisiblePages = 5;
    let startPage = Math.max(1, currentDoctorPage - Math.floor(maxVisiblePages / 2));
    let endPage = Math.min(totalPages, startPage + maxVisiblePages - 1);
    
    if (endPage - startPage + 1 < maxVisiblePages) {
        startPage = Math.max(1, endPage - maxVisiblePages + 1);
    }
    
    for (let i = startPage; i <= endPage; i++) {
        if (i === currentDoctorPage) {
            paginationHTML += `<button class="pagination-btn active" data-page="${i}">${i}</button>`;
        } else {
            paginationHTML += `<button class="pagination-btn" data-page="${i}">${i}</button>`;
        }
    }
    
    // Next button
    if (currentDoctorPage < totalPages) {
        paginationHTML += `<button class="pagination-btn" data-page="${currentDoctorPage + 1}">
            <i class="fas fa-chevron-right"></i>
        </button>`;
    } else {
        paginationHTML += `<button class="pagination-btn disabled">
            <i class="fas fa-chevron-right"></i>
        </button>`;
    }
    
    // Page info
    const startItem = (currentDoctorPage - 1) * doctorsPerPage + 1;
    const endItem = Math.min(currentDoctorPage * doctorsPerPage, filteredDoctors.length);
    const currentLang = document.body.classList.contains('rtl') ? 'ar' : 'en';
    const pageInfo = currentLang === 'ar' 
        ? `عرض ${startItem}-${endItem} من ${filteredDoctors.length}`
        : `Showing ${startItem}-${endItem} of ${filteredDoctors.length}`;
    
    paginationHTML += `<div class="pagination-info">${pageInfo}</div>`;
    
    paginationContainer.innerHTML = paginationHTML;
}

// Initialize doctor map
function initializeDoctorMap() {
    const mapElement = document.getElementById('doctorsMap');
    if (!mapElement) {
        console.error('Map element not found');
        return;
    }
    
    // Check if map already exists and remove it
    if (doctorMap) {
        doctorMap.remove();
        doctorMarkers = [];
    }

    try {
        doctorMap = L.map('doctorsMap').setView([30.0444, 31.2357], 10);
        
        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
        }).addTo(doctorMap);

        // Add user location to map if available
        if (userLocation) {
            addUserLocationToMap();
        }

        console.log('Doctor map initialized successfully');
        updateDoctorMap();

    } catch (error) {
        console.error('Error initializing doctor map:', error);
    }
}

// Add user location to map
function addUserLocationToMap() {
    if (!doctorMap || !userLocation) return;
    
    // Remove existing user marker and circle
    if (userMarker) {
        doctorMap.removeLayer(userMarker);
    }
    if (locationCircle) {
        doctorMap.removeLayer(locationCircle);
    }
    
    // Add user location marker
    userMarker = L.marker([userLocation.lat, userLocation.lng])
        .addTo(doctorMap)
        .bindPopup('Your Location')
        .openPopup();
    
    // Add circle around user location (50km radius)
    locationCircle = L.circle([userLocation.lat, userLocation.lng], {
        color: 'blue',
        fillColor: '#3388ff',
        fillOpacity: 0.1,
        radius: 50000 // 50km in meters
    }).addTo(doctorMap);
    
    // Adjust map bounds to show user location and hospitals
    const bounds = L.latLngBounds([[userLocation.lat, userLocation.lng]]);
    doctorMarkers.forEach(marker => bounds.extend(marker.getLatLng()));
    doctorMap.fitBounds(bounds, { padding: [20, 20] });
}

// Update doctor map markers
function updateDoctorMap() {
    if (!doctorMap) return;
    
    // Clear existing markers
    doctorMarkers.forEach(marker => doctorMap.removeLayer(marker));
    doctorMarkers = [];
    
    // Add markers for filtered doctors
    filteredDoctors.forEach(doctor => {
        const markerColor = getDoctorMarkerColor(doctor.specialty);
        const markerIcon = L.divIcon({
            className: 'doctor-marker',
            html: `<div class="marker-pin" style="background-color: ${markerColor};">
                     <i class="fas fa-user-md"></i>
                   </div>`,
            iconSize: [30, 42],
            iconAnchor: [15, 42]
        });

        const marker = L.marker(doctor.hospitalCoordinates, { icon: markerIcon })
            .addTo(doctorMap)
            .bindPopup(`
                <div class="doctor-popup">
                    <h3>${doctor.name}</h3>
                    <div class="popup-rating">
                        <i class="fas fa-star"></i>
                        <span>${doctor.rating} (${doctor.reviews} reviews)</span>
                    </div>
                    <div class="popup-specialty">
                        <strong>Specialty:</strong> ${doctor.specialty}
                    </div>
                    <div class="popup-location">
                        <i class="fas fa-hospital"></i>
                        <span>${doctor.hospitalName}</span>
                    </div>
                    <div class="popup-actions">
                        <button class="popup-btn primary" onclick="bookAppointment(${doctor.id})">
                            <i class="fas fa-calendar-check"></i> Book Appointment
                        </button>
                        <button class="popup-btn secondary" onclick="viewDoctorProfile(${doctor.id})">
                            <i class="fas fa-user"></i> Profile
                        </button>
                    </div>
                </div>
            `);
        
        doctorMarkers.push(marker);
    });
    
    // Adjust map bounds if there are doctors
    if (doctorMarkers.length > 0) {
        const group = new L.featureGroup(doctorMarkers);
        doctorMap.fitBounds(group.getBounds(), { padding: [20, 20] });
    }
}

// Get doctor marker color based on specialty
function getDoctorMarkerColor(specialty) {
    switch (specialty.toLowerCase()) {
        case 'cardiology': return '#EF4444';
        case 'neurology': return '#8B5CF6';
        case 'orthopedics': return '#F59E0B';
        case 'pediatrics': return '#10B981';
        case 'surgery': return '#3B82F6';
        case 'dermatology': return '#EC4899';
        default: return '#6B7280';
    }
}

// Focus on specific doctor on map
function focusOnDoctorMarker(doctorId) {
    if (!doctorMap) {
        console.error('Doctor map not initialized');
        return;
    }
    
    const doctor = filteredDoctors.find(d => d.id == doctorId);
    if (!doctor) {
        console.error('Doctor not found with ID:', doctorId);
        return;
    }
    
    // Set view to doctor coordinates with zoom level 15
    doctorMap.setView(doctor.hospitalCoordinates, 15);
    
    // Find and open the marker for this doctor
    let markerFound = false;
    doctorMarkers.forEach(marker => {
        const markerLatLng = marker.getLatLng();
        const doctorLat = doctor.hospitalCoordinates[0];
        const doctorLng = doctor.hospitalCoordinates[1];
        
        // Compare coordinates with small tolerance for floating point precision
        if (Math.abs(markerLatLng.lat - doctorLat) < 0.0001 && 
            Math.abs(markerLatLng.lng - doctorLng) < 0.0001) {
            marker.openPopup();
            markerFound = true;
        }
    });
    
    if (!markerFound) {
        console.warn('No marker found for doctor at coordinates:', doctor.hospitalCoordinates);
    }
}

// Book appointment (placeholder function)
function bookAppointment(doctorId) {
    // In a real application, this would open a booking modal or redirect to booking page
    alert(`Booking appointment with doctor ID: ${doctorId}`);
    console.log(`Opening booking modal for doctor ID: ${doctorId}`);
}

// View doctor profile (placeholder function)
function viewDoctorProfile(doctorId) {
    window.location.href = `doctor-details.html?id=${doctorId}`;
}

// Initialize page when DOM is loaded
document.addEventListener('DOMContentLoaded', function() {
    // Load saved view preference
    const savedView = localStorage.getItem('currentDoctorView') || 'grid';
    currentDoctorView = savedView;

    // Remove active class from all view buttons
    document.querySelectorAll('.view-btn').forEach(btn => btn.classList.remove('active'));
    
    // Set active view button
    const viewButton = document.querySelector(`[data-view="${savedView}"]`);
    if (viewButton) {
        viewButton.classList.add('active');
    }
    
    initializeDoctorsPage();
    switchDoctorView(currentDoctorView);
    
    // Update range filter progress on load
    const distanceFilter = document.getElementById('distanceFilter');
    if (distanceFilter) {
        updateRangeProgress(distanceFilter);
    }
});