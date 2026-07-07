import React from 'react';
import './Blog.css';
//import Image from 

const Blog = () => {
  return (
    <div className="blog-page">

      <header className="blog-header">
          <img src="/Header-photo.jpg" alt="" className ="Header-image"/>
        <h1 className="blog-title">Adopt, Don’t Shop: Give a Shelter Pet a Forever Home</h1>
        <p className="blog-subtitle">Every pet deserves a loving family. Adopt today and make a difference!</p>
      </header>
      <section className="content">
        <div>
          <h2 className="section-title">Welcome to Blog of Petopia!</h2>
          <p>At Bog of Petopia, we connect loving homes with unique pets in need of a fresh start.
             Whether you're looking for a playful pup, a curious cat, or an exotic companion, 
             our mission is to ensure every animal finds the love and care they deserve.
              Explore our diverse adoption options and help give a pet a forever home today!</p>
          <h3 className="section-title">Why Adopt a Pet?</h3>
          <p className="section-text">
            Adopting a pet is one of the most fulfilling experiences you can have. Not only do you save a life,
            but you gain a loyal companion who will bring joy, love, and laughter to your home. Thousands of pets
            are waiting in shelters for someone like you to offer them a second chance at happiness.
          </p>
          <div className="pet-benefits">
          <h3 className="section-title">Benefits of Adopting</h3>
          <ul className="benefit-list">
            <li className="benefit-item">🐾 Save a life by giving a shelter pet a loving home.</li>
            <li className="benefit-item">🐾 Adopted pets often come vaccinated and neutered/spayed.</li>
            <li className="benefit-item">🐾 Shelters match you with pets based on your lifestyle and needs.</li>
            <li className="benefit-item">🐾 Pets from shelters are grateful and provide unconditional love.</li>
          </ul>
        </div>
          <h3 className="section-title">Pet Registration in India: A Step Towards Responsible Ownership</h3>
          <p>Pet registration in India is becoming increasingly vital for responsible ownership and public safety.
             Many municipalities now mandate pet registration, especially for dogs, to ensure proper identification,
              public health, and community well-being. This process helps reunite lost pets with their owners, facilitates 
              vaccination tracking, and provides legal proof of ownership. Registration also aids in municipal pet population management and ensures pets are vaccinated against diseases like rabies. While registration is compulsory for dogs in most urban areas, cats and exotic pets may not always require it, though it is highly recommended.

The registration process typically involves visiting the local municipal corporation office or using online portals, where pet owners must submit documents such as proof of vaccination, pet photographs, and owner identification. Some cities also require sterilization certificates. After completing the process and paying a nominal fee (ranging from ₹500 to ₹1000 annually), owners receive a unique registration number and pet tag, which must be attached to the pet's collar. Major cities like Delhi, Mumbai, Bangalore, and Chennai have well-established pet registration systems, managed by municipal bodies such as the MCD, BMC, BBMP, and GCC.

Despite its benefits, pet registration faces challenges like low awareness, inconsistent enforcement, and complex procedures in certain areas. However, as awareness grows and processes simplify, pet registration is expected to play a significant role in reducing stray populations, enhancing public safety, and fostering a culture of responsible pet ownership across India.  </p>
       

        
        </div>
      </section>
        <section  className="content">
           <div>
           <h3 className="section-title"> Make a Lifelong Connection </h3>
        <p>  
        

        Adopting a pet is one of the most rewarding decisions you can make.
         At Petopia, we help connect compassionate families with animals who need them most. 
         With every adoption, you not only change a pet's life, but you also gain a loyal companion. 
         Join us in making the world a better place for pets in need—adopt today!
        </p>

           </div>

        </section>
      <footer className="blog-footer">
        <p className="footer-text">
          &copy; 2024 Petopia. All Rights Reserved.
        </p>
      </footer>
    </div>
  );
};

export default Blog;