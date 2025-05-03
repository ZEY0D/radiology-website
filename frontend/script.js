// Selecting elements
const formOpenBtn = document.querySelector("#form-open"),
  home = document.querySelector(".home"),
  formContainer = document.querySelector(".form_container"),
  formCloseBtn = document.querySelector(".form_close"),
  signupBtn = document.querySelector("#signup"),
  loginBtn = document.querySelector("#login"),
  pwShowHide = document.querySelectorAll(".pw_hide"),
  birthDateInput = document.querySelector('input[type="date"]'),
  genderInputs = document.querySelectorAll('input[name="gender"]'),
  roleSelect = document.querySelector('.login_form select'), // only login form has role select
  loginForm = document.querySelector(".login_form form"),
  signupForm = document.querySelector(".signup_form form");

// Axios base URL
const baseUrl = "http://localhost:5204/api/Auth";

// Toggle form visibility
formOpenBtn.addEventListener("click", () => home.classList.add("show"));
formCloseBtn.addEventListener("click", () => home.classList.remove("show"));

// Toggle password visibility
pwShowHide.forEach((icon) => {
  icon.addEventListener("click", () => {
    const input = icon.parentElement.querySelector("input");
    input.type = input.type === "password" ? "text" : "password";
    icon.classList.toggle("uil-eye");
    icon.classList.toggle("uil-eye-slash");
  });
});

// Switch forms
signupBtn.addEventListener("click", (e) => {
  e.preventDefault();
  formContainer.classList.add("active");
});
loginBtn.addEventListener("click", (e) => {
  e.preventDefault();
  formContainer.classList.remove("active");
});

// Login API call
loginForm.addEventListener("submit", async (e) => {
  e.preventDefault();

  const role = roleSelect.value.toLowerCase(); // Doctor or Patient
  const email = loginForm.querySelector('input[type="email"]').value;
  const password = loginForm.querySelector('input[type="password"]').value;

  try {
    const res = await axios.post(`${baseUrl}/login`, {
      email,
      password,
      role
    });

    alert(`Login successful as ${res.data.role}!`);
    console.log("User Data:", res.data);
    home.classList.remove("show");
    window.location.href = '../patient/App.html';
    // Store user data in localStorage/sessionStorage if needed
  } catch (err) {
    alert("Login failed: Invalid credentials or mismatched role.");
    console.error(err);
  }
});

// Signup API call
signupForm.addEventListener("submit", async (e) => {
  e.preventDefault();

  const name = signupForm.querySelector('input[type="text"]').value;
  const email = signupForm.querySelector('input[type="email"]').value;
  const password = signupForm.querySelectorAll('input[type="password"]')[0].value;
  const confirmPassword = signupForm.querySelectorAll('input[type="password"]')[1].value;
  const dateOfBirth = signupForm.querySelector('input[type="date"]').value;
  const gender = [...genderInputs].find(g => g.checked)?.id.replace("check-", "");

  if (password !== confirmPassword) {
    alert("Passwords do not match.");
    return;
  }

  try {
    const res = await axios.post(`${baseUrl}/signup`, {
      name,
      email,
      password,
      gender,
      dateOfBirth
    });

    alert("Signup successful! You can now log in.");
    console.log(res.data);
    formContainer.classList.remove("active"); // Switch to login form
  } catch (err) {
    alert("Signup failed. Email may already be registered.");
    console.error(err);
  }
});
