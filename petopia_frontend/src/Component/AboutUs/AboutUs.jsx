import React from 'react';
import { useInView } from 'react-intersection-observer';
import './AboutUs.css';
import Stats from '../Stats/Stats'; 

const AboutUs = () => {
  const { ref, inView } = useInView({
    triggerOnce: true,
    threshold: 0.5, 
  });

  return (
    <section id="about" className="about-us-container" ref={ref}> 
      <h1 className={`about-title ${inView ? 'fade-in' : ''}`}>About Us</h1>
      <div className="about-content"> 
        <p className={`about-text ${inView ? 'fade-in' : ''}`}>
          Welcome to our project! We are dedicated to bringing you the best services and experiences. 
          Our team is composed of skilled professionals who are passionate about what they do.
        </p>
        <p className={`about-text ${inView ? 'fade-in' : ''}`}>
          Our mission is to provide top-quality solutions that meet the needs of our users. 
          We believe in innovation, excellence, and commitment to our clients.
        </p>
        <p className={`about-text ${inView ? 'fade-in' : ''}`}>
          Thank you for visiting our PawPetopia site. We hope you find everything you're looking for. 
          If you have any questions or need further assistance, please don't hesitate to contact us.
        </p>
      </div>
      <Stats /> 
    </section>
  );
};

export default AboutUs;