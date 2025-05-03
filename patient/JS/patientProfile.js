document.addEventListener("DOMContentLoaded", function () {
  const patientId = localStorage.getItem("patientId");

  if (!patientId) {
    alert("No patient ID found. Please log in first.");
    return;
  }

  fetch(`http://localhost:5000/api/patient/${patientId}/profile`)
    .then(response => {
      if (!response.ok) {
        throw new Error("Failed to fetch profile data");
      }
      return response.json();
    })
    .then(data => {
      document.getElementById("p_name").textContent = data.user?.name || "";
      document.getElementById("p_email").textContent = data.user?.email || "";
      document.getElementById("phone").textContent = data.user?.phone || "";
      document.getElementById("address").textContent = data.user?.address || "";
      document.getElementById("p_gender").textContent = data.gender || "";
      document.getElementById("p_dob").textContent = new Date(data.dateOfBirth).toLocaleDateString();
      document.getElementById("p_bloodType").textContent = data.bloodType || "";
    })
    .catch(error => {
      console.error("Error fetching patient profile:", error);
      alert("Could not load profile.");
    });
});
