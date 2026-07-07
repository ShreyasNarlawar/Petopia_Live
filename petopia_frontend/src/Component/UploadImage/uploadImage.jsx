import React, { useState } from "react";
import axios from "axios";
import { useParams, useNavigate } from "react-router-dom";
import "./UploadImage.css";

const UploadImages = () => {
  const { petId } = useParams();
  const navigate = useNavigate();
  const [images, setImages] = useState([]);
  const [message, setMessage] = useState("");
  const [loading, setLoading] = useState(false);

  const handleImageChange = (e) => {
    setImages([...e.target.files]);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    setMessage("");

    const formData = new FormData();
    formData.append("PetId", petId);
    for (let i = 0; i < images.length; i++) {
      formData.append("Images", images[i]);
    }

    try {
      const response = await axios.post(
        "https://localhost:44395/api/PetImage/upload",
        formData,
        {
          headers: {
            "Content-Type": "multipart/form-data",
          },
        }
      );
      setMessage(response.data);
      navigate("/adopt"); // Redirect to adopt page after success
    } catch (error) {
      console.error("Error uploading images:", error);
      if (error.response) {
        setMessage(`Error: ${error.response.data}`);
      } else {
        setMessage("Error uploading images. Please try again.");
      }
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="upload-images-container">
      <h2>Upload Images for Pet ID: {petId}</h2>
      {message && <div className="alert alert-info">{message}</div>}
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label>Select Image:</label>
          <input
            type="file"
            multiple
            accept="image/*"
            onChange={handleImageChange}
            required
          />
        </div>
        <button type="submit" className="btn btn-primary" disabled={loading}>
          {loading ? "Uploading..." : "Upload Images"}
        </button>
      </form>
    </div>
  );
};

export default UploadImages;
