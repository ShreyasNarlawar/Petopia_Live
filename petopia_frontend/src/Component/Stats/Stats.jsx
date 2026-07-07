import React from 'react';
import { useInView } from 'react-intersection-observer';
import CountUp from 'react-countup';
import './Stats.css';

const Stats = () => {
  const { ref, inView } = useInView({
    triggerOnce: true,
    threshold: 0.3,
  });

  const stats = [
    { label: "Happy Adoptions", value: 5, svg: "M10 20 L15 15 ...", color: "#007bff" },
    { label: "Active Volunteers", value: 5, svg: "M5 10 L10 15 ...", color: "#28a745" },
    { label: "Generous Donations", value: 10, svg: "M2 5 L7 10 ...", color: "#ffc107" },
  ];

  return (
    <div className="stats-section">
      <h2 className="stats-title">Our Impact</h2>
      <div className="stats-grid" ref={ref}>
        {stats.map((stat, index) => (
          <div key={index} className={`stat-card ${inView ? 'fade-in' : ''}`}>
            <svg
              className={`stat-icon stat-icon-${stat.label.toLowerCase().replace(/ /g, '-')}`}
              viewBox="0 0 20 20"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path d={stat.svg} fill={stat.color} />
            </svg>
            <h3 className="stat-value">
              {inView && <CountUp end={stat.value} duration={6} />}+
            </h3>
            <p className="stat-label">{stat.label}</p>
          </div>
        ))}
      </div>
    </div>
  );
};

export default Stats;
