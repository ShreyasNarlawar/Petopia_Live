import React, { useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import "./Donate.css";

const Donate = () => {
  const [formData, setFormData] = useState({
    petName: "",
    age: "",
    houseTrained: false,
    size: "",
    gender: "",
    vaccinated: false,
    spayed: false,
    weight: "",
    category: "",
    breed: "",
    goodWithKids: false,
    goodWithOtherPets: false,
    health: "",
    needs: "",
    personality: "",
    monthlyExpense: "",
    userId: "", 
    isRegisteredWithGovt: "no" // Default value
  });

  const [loading, setLoading] = useState(false);
  const [message, setMessage] = useState("");
  const navigate = useNavigate();

  // Pet categories and their breeds
  const categories = {
    1: { name: "Dogs", breeds: ["Golden Retriever", "German Shepherd", "Bulldog", "Beagle"] },
    2: { name: "Cats", breeds: ["Persian", "Siamese", "Cancoon", "Bengal"] },
    3: { name: "Birds", breeds: ["Parakeet", "Cockateil", "Macaw", "Finch"] },
    5: { name: "Fish", breeds: ["Goldfish", "Guppy", "Angelfish"] },
    4: { name: "Rabbits", breeds: ["Netherland Dwarf", "Holland Lopt"] },
    6: { name: "Reptiles", breeds: ["Ball Python", "CornSnake"] },
  };

  // Handle category selection
  const handleCategoryChange = (e) => {
    setFormData({
      ...formData,
      category: Number(e.target.value), // Convert string to number
      breed: "", // Reset breed when category changes
    });
  };

  // Handle input changes
  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    setFormData({
      ...formData,
      [name]: type === "checkbox" ? checked : value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    setMessage("");

    // Basic validation
    if (!formData.petName || !formData.category || !formData.breed) {
      setMessage("Please fill out all required fields.");
      setLoading(false);
      return;
    }

    // Log request data before sending
    console.log("Submitting data:", JSON.stringify(formData, null, 2));

    try {
      const petProfileData = {
        Name: formData.petName,
        Age: formData.age,
        HouseTrained: formData.houseTrained,
        Size: formData.size,
        Gender: formData.gender,
        Weight: formData.weight,
        Vaccinated: formData.vaccinated,
        Spayed: formData.spayed,
        Health: formData.health,
        Needs: formData.needs,
        GoodWithKids: formData.goodWithKids,
        GoodWithPets: formData.goodWithOtherPets,
        Personality: formData.personality,
        MonthlyExpenses: formData.monthlyExpense,
        IsRegisteredWithGovt: formData.isRegisteredWithGovt,
        CategoryId: formData.category, // Ensure you set the CategoryId
        BreedId: categories[formData.category].breeds.indexOf(formData.breed) + 1, // Assuming breed IDs start from 1
        UserId: formData.userId,
      };
      console.log(petProfileData);

      // First, submit the pet profile data
      const petProfileResponse = await axios.post(
        "https://localhost:44395/api/PetProfile",
        petProfileData
      );

      setMessage("Pet donation submitted successfully!");

      // Redirect to upload images page with the created pet ID
      const createdPetId = petProfileResponse.data.petId; // Assuming API returns petId
      navigate(`/upload-images/${createdPetId}`);

      // Reset form
      setFormData({
        petName: "",
        age: "",
        houseTrained: false,
        size: "",
        gender: "",
        vaccinated: false,
        spayed: false,
        weight: "",
        category: "",
        breed: "",
        goodWithKids: false,
        goodWithOtherPets: false,
        health: "",
        needs: "",
        personality: "",
        monthlyExpense: "",
        userId: "",
        isRegisteredWithGovt: "no"
      });
    } catch (error) {
      console.error("Submission error:", error);

      if (error.response) {
        console.error("API Error Response:", error.response.data);
        setMessage(`Error: ${error.response.data.title || error.response.data}`);
      } else {
        setMessage("Error submitting pet donation. Please try again.");
      }
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="fullscreen-wrapper">
      <form onSubmit={handleSubmit} className="container mt-3 form-wrapper animate-fade-in">
        <h2>Pet Donation Form</h2>
        {message && <div className="alert alert-info">{message}</div>}

        <div className="mb-3">
          <label>Pet Name:</label>
          <input
            type="text"
            className="form-control"
            name="petName"
            value={formData.petName}
            onChange={handleChange}
            required
          />
        </div>
        <div className="mb-3">
          <label>Category:</label>
          <select
            className="form-control"
            name="category"
            value={formData.category}
            onChange={handleCategoryChange}
            required
          >
            <option value="">Select Category</option>
            {Object.entries(categories).map(([id, category]) => (
              <option key={id} value={id}>
                {category.name}
              </option>
            ))}
          </select>
        </div>
        <div className="mb-3">
          <label>Breed:</label>
          <select
            className="form-control"
            name="breed"
            value={formData.breed}
            onChange={handleChange}
            required
          >
            <option value="">Select Breed</option>
            {formData.category && categories[formData.category].breeds.map((breed) => (
              <option key={breed} value={breed}>
                {breed}
              </option>
            ))}
          </select>
        </div>
        <div className="mb-3">
          <label>Age:</label>
          <input
            type="number"
            className="form-control"
            name="age"
            value={formData.age}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Enter UserID</label>
          <input
            type="number"
            className="form-control"
            name="userId"
            value={formData.userId}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Size:</label>
          <select
            className="form-control"
            name="size"
            value={formData.size}
            onChange={handleChange}
            required
          >
            <option value="">Select Size</option>
            <option value="small">Small</option>
            <option value="medium">Medium</option>
            <option value="large">Large</option>
          </select>
        </div>
        <div className="mb-3">
          <label>Gender:</label>
          <select className="form-control"
            name="gender"
            value={formData.gender}
            onChange={handleChange}
            required
          >
            <option value="">Select Gender</option>
            <option value="Male">Male</option>
            <option value="Female">Female</option>
            <option value="Other">Other</option>
          </select>
        </div>
        <div className="mb-3">
          <label>Weight in gm:</label>
          <input
            type="number"
            className="form-control"
            name="weight"
            value={formData.weight}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>House Trained:</label>
          <input
            type="checkbox"
            name="houseTrained"
            checked={formData.houseTrained}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Vaccinated:</label>
          <input
            type="checkbox"
            name="vaccinated"
            checked={formData.vaccinated}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Spayed/Neutered:</label>
          <input
            type="checkbox"
            name="spayed"
            checked={formData.spayed}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Good with Kids:</label>
          <input
            type="checkbox"
            name="goodWithKids"
            checked={formData.goodWithKids}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Good with Other Pets:</label>
          <input
            type="checkbox"
            name="goodWithOtherPets"
            checked={formData.goodWithOtherPets}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Health:</label>
          <input
            type="text"
            className="form-control"
            name="health"
            value={formData.health}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Needs:</label>
          <input
            type="text"
            className="form-control"
            name="needs"
            value={formData.needs}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Personality:</label>
          <input
            type="text"
            className="form-control"
            name="personality"
            value={formData.personality}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Monthly Expense:</label>
          <input
            type="number"
            className="form-control"
            name="monthlyExpense"
            value={formData.monthlyExpense}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Is Government ID Registered?</label>
          <select
            className="form-control"
            name="isRegisteredWithGovt"
            value={formData.isRegisteredWithGovt}
            onChange={handleChange}
            required
          >
            <option value="">Select</option>
            <option value="yes">Yes</option>
            <option value="no">No</option>
          </select>
        </div>
        <button type="submit" className="btn btn-primary" disabled={loading}>
          {loading ? "Submitting..." : "Submit"}
        </button>
      </form>
    </div>
  );
};

export default Donate;