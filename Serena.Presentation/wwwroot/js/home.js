// Partners Carousel
function initializePartnersCarousel() {
    const partnersTrack = document.querySelector('.partners-track');
    if (!partnersTrack) return;
    
    const partnerLogos = [
        { name: 'MedTech Solutions', color: '#3B82F6' },
        { name: 'HealthInnovate', color: '#10B981' },
        { name: 'CareConnect', color: '#8B5CF6' },
        { name: 'MediSystem', color: '#F59E0B' },
        { name: 'HealthPlus', color: '#EF4444' },
        { name: 'Wellness Corp', color: '#06B6D4' }
    ];
    
    // Create two sets of logos for infinite scrolling
    for (let i = 0; i < 2; i++) {
        partnerLogos.forEach(logo => {
            const partnerLogo = document.createElement('div');
            partnerLogo.className = 'partner-logo';
            partnerLogo.innerHTML = `
                <div style="color: ${logo.color}; font-weight: 600; font-size: 1.1rem;">
                    ${logo.name}
                </div>
            `;
            partnersTrack.appendChild(partnerLogo);
        });
    }
}

function initializeHospitals() {
    const hospitalsGrid = document.querySelector('.hospitals-grid');
    if (!hospitalsGrid) return;

    // Sample hospital data
    const hospitals = [
        {
            id: 1,
            name: "Al-Salam International Hospital",
            location: "Cairo, Egypt",
            image: "https://images.unsplash.com/photo-1519494026892-80bbd2d6fd0d?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1000&q=80",
            rating: 4.8,
            departments: 25,
            doctors: 150,
            description: "Leading healthcare institution providing comprehensive medical services with state-of-the-art facilities."
        },
        {
            id: 2,
            name: "Dar Al Fouad Hospital",
            location: "Giza, Egypt",
            image: "https://images.unsplash.com/photo-1586773860418-d37222d8fce3?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1000&q=80",
            rating: 4.7,
            departments: 22,
            doctors: 120,
            description: "Premium medical care with international standards and expert medical professionals."
        },
        {
            id: 3,
            name: "Cairo Medical Center",
            location: "Cairo, Egypt",
            image: "https://images.unsplash.com/photo-1516549655669-df5c58b5d7d8?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1000&q=80",
            rating: 4.6,
            departments: 18,
            doctors: 95,
            description: "Modern healthcare facility offering specialized medical treatments and emergency services."
        }
    ];

    hospitalsGrid.innerHTML = '';
    
    hospitals.forEach(hospital => {
        const hospitalCard = createHospitalCard(hospital);
        hospitalsGrid.appendChild(hospitalCard);
    });
}

function createHospitalCard(hospital) {
    const card = document.createElement('div');
    card.className = 'hospital-card';
    card.setAttribute('data-hospital-id', hospital.id);
    
    card.innerHTML = `
        <div class="hospital-image">
            <img src="${hospital.image}" alt="${hospital.name}">
            <div class="hospital-rating">
                <i class="fas fa-star"></i>
                <span>${hospital.rating}</span>
            </div>
        </div>
        <div class="hospital-content">
            <h3 class="hospital-name">${hospital.name}</h3>
            <div class="hospital-location">
                <i class="fas fa-map-marker-alt"></i>
                <span>${hospital.location}</span>
            </div>
            <div class="hospital-stats">
                <div class="hospital-stat">
                    <div class="hospital-stat-value">${hospital.departments}</div>
                    <div class="hospital-stat-label" data-en="Departments" data-ar="الأقسام">Departments</div>
                </div>
                <div class="hospital-stat">
                    <div class="hospital-stat-value">${hospital.doctors}</div>
                    <div class="hospital-stat-label" data-en="Doctors" data-ar="الأطباء">Doctors</div>
                </div>
            </div>
            <div class="hospital-actions">
                <button class="hospital-btn view-hospital-btn">
                    <i class="fas fa-eye"></i>
                    <span data-en="View Details" data-ar="عرض التفاصيل">View Details</span>
                </button>
            </div>
        </div>
    `;
    
    // Add click event to redirect to hospital details page
    const viewBtn = card.querySelector('.view-hospital-btn');
    viewBtn.addEventListener('click', function(e) {
        e.stopPropagation();
        window.location.href = `hospital-details.html?id=${hospital.id}`;
    });
    
    // Also make the whole card clickable
    card.addEventListener('click', function() {
        window.location.href = `hospital-details.html?id=${hospital.id}`;
    });
    
    return card;
}

function initializeDoctors() {
    const doctorsGrid = document.querySelector('.doctors-grid');
    if (!doctorsGrid) return;

    // Sample doctor data
    const doctors = [
        {
            id: 1,
            name: "Dr. Ahmed Hassan",
            specialty: "Cardiologist",
            hospital: "Al-Salam International Hospital",
            image: "https://images.unsplash.com/photo-1612349317150-e413f6a5b16d?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1000&q=80",
            rating: 4.9,
            experience: "15+ years",
            fee: "$250",
            languages: ["English", "Arabic"],
            description: "Expert cardiologist with extensive experience in heart diseases and treatments."
        },
        {
            id: 2,
            name: "Dr. Sarah Mohamed",
            specialty: "Neurologist",
            hospital: "Dar Al Fouad Hospital",
            image: "https://images.unsplash.com/photo-1559839734-2b71ea197ec2?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1000&q=80",
            rating: 4.8,
            experience: "12+ years",
            fee: "$200",
            languages: ["English", "Arabic", "French"],
            description: "Specialized neurologist with focus on brain and nervous system disorders."
        },
        {
            id: 3,
            name: "Dr. Omar Khalid",
            specialty: "Orthopedic Surgeon",
            hospital: "Cairo Medical Center",
            image: "https://images.unsplash.com/photo-1582750433449-648ed127bb54?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1000&q=80",
            rating: 4.7,
            experience: "18+ years",
            fee: "$300",
            languages: ["English", "Arabic"],
            description: "Renowned orthopedic surgeon specializing in joint replacements and sports injuries."
        }
    ];

    doctorsGrid.innerHTML = '';
    
    doctors.forEach(doctor => {
        const doctorCard = createDoctorCard(doctor);
        doctorsGrid.appendChild(doctorCard);
    });
}

function createDoctorCard(doctor) {
    const card = document.createElement('div');
    card.className = 'doctor-card';
    card.setAttribute('data-doctor-id', doctor.id);
    
    card.innerHTML = `
        <div class="doctor-image">
            <img src="${doctor.image}" alt="${doctor.name}">
            <div class="doctor-rating">
                <i class="fas fa-star"></i>
                <span>${doctor.rating}</span>
            </div>
        </div>
        <div class="doctor-content">
            <h3 class="doctor-name">${doctor.name}</h3>
            <div class="doctor-specialty">
                <i class="fas fa-stethoscope"></i>
                <span>${doctor.specialty}</span>
            </div>
            <div class="doctor-location">
                <i class="fas fa-hospital"></i>
                <span>${doctor.hospital}</span>
            </div>
            <div class="doctor-fee">
                <span data-en="Consultation Fee:" data-ar="رسوم الاستشارة:">Consultation Fee:</span>
                <span class="fee-amount">${doctor.fee}</span>
            </div>
            <div class="doctor-stats">
                <div class="doctor-stat">
                    <div class="doctor-stat-value">${doctor.experience}</div>
                    <div class="doctor-stat-label" data-en="Experience" data-ar="الخبرة">Experience</div>
                </div>
                <div class="doctor-stat">
                    <div class="doctor-stat-value">${doctor.languages.length}</div>
                    <div class="doctor-stat-label" data-en="Languages" data-ar="اللغات">Languages</div>
                </div>
            </div>
            <div class="doctor-actions">
                <div class="top-action-buttons">
                    <button class="doctor-btn view-doctor-btn">
                        <i class="fas fa-eye"></i>
                        <span data-en="View Profile" data-ar="عرض الملف">View Profile</span>
                    </button>
                    <button class="doctor-btn book-appointment-btn">
                        <i class="fas fa-calendar-check"></i>
                        <span data-en="Book Now" data-ar="احجز الآن">Book Now</span>
                    </button>
                </div>
            </div>
        </div>
    `;
    
    // Add click events
    const viewBtn = card.querySelector('.view-doctor-btn');
    viewBtn.addEventListener('click', function(e) {
        e.stopPropagation();
        window.location.href = `doctor-details.html?id=${doctor.id}`;
    });
    
    const bookBtn = card.querySelector('.book-appointment-btn');
    bookBtn.addEventListener('click', function(e) {
        e.stopPropagation();
        window.location.href = `doctor-details.html?id=${doctor.id}&book=true`;
    });
    
    // Make the whole card clickable for viewing details
    card.addEventListener('click', function() {
        window.location.href = `doctor-details.html?id=${doctor.id}`;
    });
    
    return card;
}

// Reviews Data and Grid
function initializeReviews() {
    const reviewsGrid = document.querySelector('.reviews-grid');
    if (!reviewsGrid) return;
    
    const reviews = [
        {
            id: 1,
            name: 'Robert Johnson',
            nameAr: 'روبرت جونسون',
            role: 'Patient',
            roleAr: 'مريض',
            rating: 5,
            comment: 'Excellent service! The doctors were very professional and caring. The platform made it easy to find the right specialist for my needs.',
            commentAr: 'خدمة ممتازة! كان الأطباء محترفين ومهتمين للغاية. جعلت المنصة من السهل العثور على الاختصاصي المناسب لاحتياجاتي.',
            image: 'https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=500&q=80'
        },
        {
            id: 2,
            name: 'Maria Garcia',
            nameAr: 'ماريا غارسيا',
            role: 'Patient',
            roleAr: 'مريضة',
            rating: 4,
            comment: 'Great experience with Serena. The platform made it easy to find the right specialist and book appointments quickly.',
            commentAr: 'تجربة رائعة مع سيرينا. جعلت المنصة من السهل العثور على الاختصاصي المناسب وحجز المواعيد بسرعة.',
            image: 'https://images.unsplash.com/photo-1517841905240-472988babdf9?ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8OHx8cGVvcGxlfGVufDB8fDB8fHww&auto=format&fit=crop&q=60&w=600'
        },
        {
            id: 3,
            name: 'David Smith',
            nameAr: 'ديفيد سميث',
            role: 'Patient',
            roleAr: 'مريض',
            rating: 5,
            comment: 'The healthcare management system is revolutionary. Highly recommended for anyone looking for efficient medical services.',
            commentAr: 'نظام إدارة الرعاية الصحية ثوري. موصى به بشدة لأي شخص يبحث عن خدمات طبية فعالة.',
            image: 'https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=500&q=80'
        }
    ];
    
    reviewsGrid.innerHTML = '';
    
    reviews.forEach(review => {
        const reviewCard = document.createElement('div');
        reviewCard.className = 'review-card';
        
        let stars = '';
        for (let i = 0; i < 5; i++) {
            if (i < review.rating) {
                stars += '<i class="fas fa-star review-star"></i>';
            } else {
                stars += '<i class="far fa-star review-star"></i>';
            }
        }
        
        reviewCard.innerHTML = `
            <p class="review-text"><span data-en="${review.comment}" data-ar="${review.commentAr}">${review.comment}</span></p>
            <div class="review-rating">
                ${stars}
            </div>
            <div class="review-author">
                <div class="review-avatar">
                    <img src="${review.image}" alt="${review.name}">
                </div>
                <div class="review-author-info">
                    <div class="review-author-name" data-en="${review.name}" data-ar="${review.nameAr}">${review.name}</div>
                    <div class="review-author-role" data-en="${review.role}" data-ar="${review.roleAr}">${review.role}</div>
                </div>
            </div>
        `;
        
        reviewsGrid.appendChild(reviewCard);
    });
}

const loadmoreBtn = document.getElementsByClassName('view-all-btn');
if (loadmoreBtn.length > 0) {
    loadmoreBtn[0].addEventListener('click', () => {
        window.location.href = 'hospitals.html';
    });
    loadmoreBtn[1].addEventListener('click', () => {
        window.location.href = 'doctors.html';
    });
}

const aboutBtn = document.getElementsByClassName('about-btn');
if (aboutBtn) {
    aboutBtn[0].addEventListener('click', () => {
        window.location.href = 'about.html';
    });
}

// Interactive Map
// function initializeMap() {
//     const mapElement = document.getElementById('map');
//     if (!mapElement) return;
    
//     const map = L.map('map').setView([30.81, 30.99], 13); // New York coordinates
    
//     L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
//         attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
//     }).addTo(map);
    
//     // Add some marker examples
//     L.marker([40.7128, -74.0060]).addTo(map)
//         .bindPopup('Serena Headquarters')
//         .openPopup();
        
//     L.marker([40.7211, -74.0050]).addTo(map)
//         .bindPopup('MediCare General Hospital');
        
//     L.marker([40.7048, -74.0100]).addTo(map)
//         .bindPopup('City Health Center');
// }

// Initialize everything when DOM is loaded
document.addEventListener('DOMContentLoaded', () => {
    initializePartnersCarousel();
    initializeHospitals();
    initializeDoctors();
    initializeReviews();
    // Initialize home page map
    if (document.getElementById('map')) {
        initializeHomeMap();
    }
});