import React, { useState } from 'react'; 
import './FAQ.css'; // Ensure to import your CSS for styling

const FAQ = () => {
    const faqs = [
        {
            question: "How do I adopt a pet?",
            answer: "To adopt a pet, visit our adoption page, select a pet, and fill out the adoption application form."
        },
        {
            question: "What is the adoption fee?",
            answer: "The adoption fee varies by pet and includes vaccinations, spaying/neutering, and a microchip."
        },
        {
            question: "Can I meet a pet before adopting?",
            answer: "Yes! We encourage potential adopters to meet pets in person. You can schedule a visit through our website."
        },
        {
            question: "What if I have other pets at home?",
            answer: "We recommend a meet-and-greet with your current pets to ensure compatibility."
        },
        {
            question: "What should I bring when adopting a pet?",
            answer: "Please bring a valid ID, proof of residence, and any necessary supplies for your new pet."
        },
    ];

    // State to manage which FAQ is open
    const [openIndex, setOpenIndex] = useState(null);

    const toggleFAQ = (index) => {
        // Toggle the FAQ open/close
        setOpenIndex(openIndex === index ? null : index);
    };

    return (
        <div className="faq-container">
            <h1>Frequently Asked Questions</h1>
            <div className="faq-list">
                {faqs.map((faq, index) => (
                    <div key={index} className="faq-item">
                        <div className="faq-question" onClick={() => toggleFAQ(index)}>
                            <h2>{faq.question}</h2>
                            <span className={`icon ${openIndex === index ? 'active' : ''}`}>▼</span>
                        </div>
                        {openIndex === index && (
                            <p className="faq-answer">{faq.answer}</p>
                        )}
                    </div>
                ))}
            </div>
        </div>
    );
};

export default FAQ;
