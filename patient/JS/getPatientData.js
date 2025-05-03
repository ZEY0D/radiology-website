// src/api/getPatientVitals.js
export const getPatientVitals = async () => {
    try {
      const response = await fetch('http://localhost:3000/api/vitals'); // Replace with your backend endpoint
      if (!response.ok) {
        throw new Error('Failed to fetch vitals');
      }
      const data = await response.json();
      return data;
    } catch (error) {
      console.error('Error fetching Data:', error);
      return [];
    }
  };