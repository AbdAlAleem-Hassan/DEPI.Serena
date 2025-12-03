// Auth functionality
document.addEventListener('DOMContentLoaded', function () {

    // =======================
    // Role selection
    // =======================
    const roleInputs = document.querySelectorAll('input[name="Role"]');
    const doctorFields = document.getElementById('doctorFields');
    const patientFields = document.getElementById('patientFields');

    function toggleRoleFields() {
        const selectedRole = document.querySelector('input[name="Role"]:checked')?.value;

        if (selectedRole === 'Doctor') {
            doctorFields.style.display = 'block';
            patientFields.style.display = 'none';

            document.querySelectorAll('#doctorFields input, #doctorFields select')
                .forEach(input => {
                    if (input.name.includes('Specialization') || input.name.includes('LicenseNumber')) {
                        input.required = true;
                    }
                });

            document.querySelectorAll('#patientFields input')
                .forEach(input => input.required = false);

        } else {
            doctorFields.style.display = 'none';
            patientFields.style.display = 'block';

            document.querySelectorAll('#patientFields input')
                .forEach(input => {
                    if (input.name.includes('JobStatus')) {
                        input.required = true;
                    }
                });

            document.querySelectorAll('#doctorFields input, #doctorFields select')
                .forEach(input => input.required = false);
        }
    }

    roleInputs.forEach(input => input.addEventListener('change', toggleRoleFields));

    toggleRoleFields();


    // =======================
    // Form validation
    // =======================
    const form = document.querySelector('form');

    form.addEventListener('submit', function (e) {
        const requiredFields = form.querySelectorAll('[required]');
        const missing = [];

        requiredFields.forEach(field => {
            if (!field.value.trim()) {
                missing.push(field.name);
            }
        });

        const terms = form.querySelector('input[name="AgreeToTerms"]');
        if (terms && !terms.checked) missing.push("AgreeToTerms");

        if (missing.length > 0) {
            e.preventDefault();
            alert("Please fill all required fields: " + missing.join(", "));
        }
    });





        // =======================
        // Password Toggle
        // =======================
        const toggles = document.querySelectorAll(".password-toggle");

        toggles.forEach(toggle => {
            toggle.addEventListener("click", function (e) {

                const wrapper = e.target.closest(".password-input-wrapper");
                if (!wrapper) return;

                const input = wrapper.querySelector("input[type='password'], input[type='text']");
                if (!input) return;

                if (input.type === "password") {
                    input.type = "text";
                    toggle.innerHTML = `<i class="far fa-eye-slash"></i>`;
                    toggle.setAttribute("aria-label", "Hide password");
                } else {
                    input.type = "password";
                    toggle.innerHTML = `<i class="far fa-eye"></i>`;
                    toggle.setAttribute("aria-label", "Show password");
                }

                input.focus();
            });
        });


        // =======================
        // Password Strength System
        // =======================
        const passwordInput = document.getElementById("login-password");
        const strengthBar = document.getElementById("login-password-strength");
        const strengthText = document.getElementById("login-password-strength-text");

        const passwordStrengthTexts = {
            en: { weak: "Weak", medium: "Medium", strong: "Strong" },
            ar: { weak: "÷⁄Ì›", medium: "„ Ê”ÿ", strong: "ﬁÊÌ" }
        };

        function getCurrentLanguage() {
            return document.documentElement.getAttribute("lang") === "ar" ? "ar" : "en";
        }

        function getPasswordStrengthText(strength) {
            const lang = getCurrentLanguage();
            if (strength < 40) return passwordStrengthTexts[lang].weak;
            if (strength < 70) return passwordStrengthTexts[lang].medium;
            return passwordStrengthTexts[lang].strong;
        }

        function calculatePasswordStrength(password) {
            let strength = 0;

            if (password.length >= 8) strength += 25;
            if (/[a-z]/.test(password)) strength += 25;
            if (/[A-Z]/.test(password)) strength += 25;
            if (/[0-9]/.test(password)) strength += 15;
            if (/[^A-Za-z0-9]/.test(password)) strength += 10;

            return strength;
        }

        function updatePasswordStrength() {
            const password = passwordInput.value.trim();
            const strength = calculatePasswordStrength(password);

            strengthBar.className = "strength-bar";

            if (!password) {
                strengthBar.style.width = "0%";
                strengthText.textContent = "";
                return;
            }

            if (strength < 40) {
                strengthBar.classList.add("strength-weak");
                strengthText.textContent = getPasswordStrengthText(strength);
                strengthText.style.color = "#ef4444";
            } else if (strength < 70) {
                strengthBar.classList.add("strength-medium");
                strengthText.textContent = getPasswordStrengthText(strength);
                strengthText.style.color = "#f59e0b";
            } else {
                strengthBar.classList.add("strength-strong");
                strengthText.textContent = getPasswordStrengthText(strength);
                strengthText.style.color = "#10b981";
            }
        }

        passwordInput.addEventListener("input", function () {
            this.classList.add("interacted");
            updatePasswordStrength();
        });




});
