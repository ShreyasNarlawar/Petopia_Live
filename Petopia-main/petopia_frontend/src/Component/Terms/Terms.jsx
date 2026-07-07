import React from 'react';
import './Terms.css'; // Import your custom CSS

const Terms = () => {
  return (
    <div className="terms-container">
        <h1 className="text-center">Terms and Conditions for Pet Adoption</h1>

        <section>
            <h2>Introduction</h2>
            <p>
                Welcome to our pet adoption site. By using this site, you agree to comply with and be bound by the following terms and conditions.
            </p>
        </section>

        <section>
            <h2>User Responsibilities</h2>
            <ul>
                <li>Provide accurate information during the adoption process.</li>
                <li>Follow all local laws and regulations regarding pet ownership.</li>
                <li>Ensure a safe and loving environment for the adopted pet.</li>
            </ul>
        </section>

        <section>
            <h2>Adoption Process</h2>
            <p>
                The adoption process includes filling out an application, paying any applicable fees, and meeting with our adoption team. 
                Please ensure you have all necessary documentation ready.
            </p>
        </section>

        <section>
            <h2>Liability Disclaimer</h2>
            <p>
                We are not liable for any issues that may arise from the adoption of a pet. 
                This includes but is not limited to health issues, behavioral problems, or any other concerns.
            </p>
        </section>

        <section>
            <h2>Contact Information</h2>
            <p className="contact-info">
                If you have any questions or concerns regarding these terms, please contact us at:
                <br />
                Email: petadoption@email.com
            </p>
        </section>
    </div>
  );
};

export default Terms;